//-----------------------------------------------------------------------
// <copyright file="FormBuildService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.Controls
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Windows.Forms;
    using System.Xml;


    /// <summary>
    /// 根据xml生成表单服务
    /// </summary>
    public class FormBuildService
    {
        #region 生成运行时对象


        /// <summary>
        /// 创建一个对象从xml
        /// </summary>
        /// <param name="hst">宿主</param>
        /// <param name="fileName">文件名</param>
        /// <param name="errors">返回错误列表</param>
        /// <returns></returns>
        public static object CreateObjectFromFile(IServiceContainerManager container, string fileName, ArrayList errors)
        {
            StreamReader sr = new StreamReader(fileName);
            try
            {
                string cleandown = sr.ReadToEnd();
                cleandown = "<DOCUMENT_ELEMENT>" + cleandown + "</DOCUMENT_ELEMENT>";
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(cleandown);

                LWBaseForm wfForm = null;
                Dictionary<string, object> obs = new Dictionary<string, object>();
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    if (node.Name.Equals("Object"))
                    {
                        if (wfForm == null)
                        {
                            wfForm = (LWBaseForm)CreateObject(container,node, errors);
                            continue;
                        }

                        XmlAttribute nameAttr = node.Attributes["name"];
                        object ob = CreateObject(container,node, errors);
                        obs.Add(nameAttr.Value.ToString(), ob);
                    }
                    else
                    {
                        errors.Add(string.Format("Node type {0} is not allowed here.", node.Name));
                    }
                }

                if (wfForm != null)
                {
                    wfForm.BindingSources = obs;
                    return wfForm;
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex);
            }
            finally
            {
                sr.Close();
                sr.Dispose();
            }
            return null;
        }


        /// <summary>
        /// 创建一个对象从xml
        /// </summary>
        /// <param name="hst">宿主</param>
        /// <param name="fileName">文件名</param>
        /// <param name="errors">返回错误列表</param>
        /// <returns></returns>
        public static object CreateObjectFromXmlData(IServiceContainerManager container, string xmldata, ArrayList errors)
        {
            try
            {
                string cleandown = "<DOCUMENT_ELEMENT>" + xmldata + "</DOCUMENT_ELEMENT>";

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(cleandown);

                LWBaseForm wfForm = null;
                Dictionary<string, object> obs = new Dictionary<string, object>();
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    if (node.Name.Equals("Object"))
                    {
                        if (wfForm == null)
                        {
                            wfForm = (LWBaseForm)CreateObject(container,node, errors);
                            continue;
                        }

                        XmlAttribute nameAttr = node.Attributes["name"];
                        object ob = CreateObject(container,node, errors);
                        obs.Add(nameAttr.Value.ToString(), ob);
                    }
                    else
                    {
                        errors.Add(string.Format("Node type {0} is not allowed here.", node.Name));
                    }
                }

                if (wfForm != null)
                {
                    wfForm.BindingSources = obs;

                    if (wfForm is IServiceContainer)
                    {
                        IServiceContainer ct = wfForm as IServiceContainer;
                        if (ct != null)
                        {
                            ct.ServiceContainer = container;
                        }
                    }
                    return wfForm;
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex);
            }
          
            return null;
        }

        /// <summary>
        /// 生成运行时对象
        /// </summary>
        /// <param name="hst"></param>
        /// <param name="node"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        private static object CreateObject(IServiceContainerManager container, XmlNode node, ArrayList errors)
        {
            XmlAttribute typeAttr = node.Attributes["type"];

            if (typeAttr == null)
            {
                errors.Add("<Object> tag is missing required type attribute");
                return null;
            }

            Type type = Type.GetType(typeAttr.Value);
            if (type == null)
            {
                errors.Add(string.Format("Type {0} could not be loaded.", typeAttr.Value));
                return null;
            }


            XmlAttribute nameAttr = node.Attributes["name"];
            object instance = Activator.CreateInstance(type);
            if (instance is IServiceContainer)
            {
                IServiceContainer isv = instance as IServiceContainer;
                if (isv != null)
                {
                    isv.ServiceContainer = container;
                }
            }
            XmlAttribute childAttr = node.Attributes["children"];
            IList childList = null;

            if (childAttr != null)
            {
                PropertyDescriptor childProp = TypeDescriptor.GetProperties(instance)[childAttr.Value];

                if (childProp == null)
                {
                    errors.Add(string.Format("The children attribute lists {0} as the child collection but this is not a property on {1}", childAttr.Value, instance.GetType().FullName));
                }
                else
                {
                    childList = childProp.GetValue(instance) as IList;
                    if (childList == null)
                    {
                        errors.Add(string.Format("The property {0} was found but did not return a valid IList", childProp.Name));
                    }
                }
            }

            foreach (XmlNode childNode in node.ChildNodes)
            {
                if (childNode.Name.Equals("Cell"))
                {
                    LWTableLayoutPanel tableLayoutPanel = instance as LWTableLayoutPanel;
                    if (tableLayoutPanel != null && childList != null)
                    {
                        int row = int.Parse(childNode.Attributes["Row"].Value);
                        int rowSpan = int.Parse(childNode.Attributes["RowSpan"].Value);
                        int column = int.Parse(childNode.Attributes["Column"].Value);
                        int columnSpan = int.Parse(childNode.Attributes["ColumnSpan"].Value);

                        XmlNode ctrlNode = childNode.ChildNodes[0];
                        object childInstance = CreateObject(container,ctrlNode, errors);
                        childList.Add(childInstance);

                        Control ctrl = childInstance as Control;
                        if (ctrl != null)
                        {
                            tableLayoutPanel.SetRow(ctrl, row);
                            tableLayoutPanel.SetRowSpan(ctrl, rowSpan);
                            tableLayoutPanel.SetColumn(ctrl, column);
                            tableLayoutPanel.SetColumnSpan(ctrl, columnSpan);
                        }
                    }
                }
                else if (childNode.Name.Equals("Object"))
                {
                    if (childAttr == null)
                    {
                        errors.Add("Child object found but there is no children attribute");
                        continue;
                    }

                    if (childList != null)
                    {
                        object childInstance = CreateObject(container,childNode, errors);

                        childList.Add(childInstance);
                    }
                }
                else if (childNode.Name.Equals("Property"))
                {
                    CreateProperty(container, childNode, instance, errors);
                }
              
            }

            return instance;
        }

        private static void CreateProperty(IServiceContainerManager container, XmlNode node, object instance, ArrayList errors)
        {
            XmlAttribute nameAttr = node.Attributes["name"];

            if (nameAttr == null)
            {
                errors.Add("Property has no name");
                return;
            }

            PropertyDescriptor prop = TypeDescriptor.GetProperties(instance)[nameAttr.Value];

            if (prop == null)
            {
                errors.Add(string.Format("Property {0} does not exist on {1}", nameAttr.Value, instance.GetType().FullName));
                return;
            }


            bool isContent = prop.Attributes.Contains(DesignerSerializationVisibilityAttribute.Content);

            if (isContent)
            {
                object value = prop.GetValue(instance);


                if (value is IList)
                {
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        if (child.Name.Equals("Item"))
                        {
                            object item;
                            XmlAttribute typeAttr = child.Attributes["type"];

                            if (typeAttr == null)
                            {
                                errors.Add("Item has no type attribute");
                                continue;
                            }

                            Type type = Type.GetType(typeAttr.Value);

                            if (type == null)
                            {
                                errors.Add(string.Format("Item type {0} could not be found.", typeAttr.Value));
                                continue;
                            }

                            if (CreateValue(container,child, TypeDescriptor.GetConverter(type), errors, out item))
                            {
                                try
                                {
                                    ((IList)value).Add(item);
                                }
                                catch (Exception ex)
                                {
                                    errors.Add(ex.Message);
                                }
                            }
                        }
                        else
                        {
                            errors.Add(string.Format("Only Item elements are allowed in collections, not {0} elements.", child.Name));
                        }
                    }
                }
                else
                {
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        if (child.Name.Equals("Property"))
                        {
                            CreateProperty(container, child, value, errors);
                        }
                        else
                        {
                            errors.Add(string.Format("Only Property elements are allowed in content properties, not {0} elements.", child.Name));
                        }
                    }
                }
            }
            else
            {
                object value;

                if (CreateValue(container, node, prop.Converter, errors, out value))
                {

                    try
                    {
                        prop.SetValue(instance, value);
                    }
                    catch (Exception ex)
                    {
                        errors.Add(ex.Message);
                    }
                }
            }
        }

        private static bool CreateValue(IServiceContainerManager container, XmlNode node, TypeConverter converter, ArrayList errors, out object value)
        {
            try
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    if (child.NodeType == XmlNodeType.Text)
                    {
                        value = converter.ConvertFromInvariantString(node.InnerText);
                        return true;
                    }
                    else if (child.Name.Equals("Binary"))
                    {
                        byte[] data = Convert.FromBase64String(child.InnerText);
                        if (GetConversionSupported(converter, typeof(byte[])))
                        {
                            value = converter.ConvertFrom(null, CultureInfo.InvariantCulture, data);
                            return true;
                        }
                        else
                        {
                            BinaryFormatter formatter = new BinaryFormatter();
                            MemoryStream stream = new MemoryStream(data);

                            value = formatter.Deserialize(stream);
                            return true;
                        }
                    }
                    else if (child.Name.Equals("InstanceDescriptor"))
                    {
                        value = CreateInstanceDescriptor(container, child, errors);
                        return (value != null);
                    }
                    else if (child.Name.Equals("Object"))
                    {
                        //添加对于列表对象时候反序列化 
                        value = CreateObject(container,child, errors);
                        return (value != null);
                    }
                    else
                    {
                        errors.Add(string.Format("Unexpected element type {0}", child.Name));
                        value = null;
                        return false;
                    }
                }

                value = null;
                return true;
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                value = null;
                return false;
            }
        }

        private static object CreateInstanceDescriptor(IServiceContainerManager container, XmlNode node, ArrayList errors)
        {

            XmlAttribute memberAttr = node.Attributes["member"];

            if (memberAttr == null)
            {
                errors.Add("No member attribute on instance descriptor");
                return null;
            }

            byte[] data = Convert.FromBase64String(memberAttr.Value);
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(data);
            MemberInfo mi = (MemberInfo)formatter.Deserialize(stream);
            object[] args = null;


            if (mi is MethodBase)
            {
                ParameterInfo[] paramInfos = ((MethodBase)mi).GetParameters();

                args = new object[paramInfos.Length];

                int idx = 0;

                foreach (XmlNode child in node.ChildNodes)
                {
                    if (child.Name.Equals("Argument"))
                    {
                        object value;

                        if (!CreateValue(container, child, TypeDescriptor.GetConverter(paramInfos[idx].ParameterType), errors, out value))
                        {
                            return null;
                        }

                        args[idx++] = value;
                    }
                }

                if (idx != paramInfos.Length)
                {
                    errors.Add(string.Format("Member {0} requires {1} arguments, not {2}.", mi.Name, args.Length, idx));
                    return null;
                }
            }

            InstanceDescriptor id = new InstanceDescriptor(mi, args);
            object instance = id.Invoke();

            foreach (XmlNode prop in node.ChildNodes)
            {
                if (prop.Name.Equals("Property"))
                {
                    CreateProperty(container, prop, instance, errors);
                }
            }

            return instance;
        }


        private static bool GetConversionSupported(TypeConverter converter, Type conversionType)
        {
            return (converter.CanConvertFrom(conversionType) && converter.CanConvertTo(conversionType));
        }
        #endregion
    }
}
