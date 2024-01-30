using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Common.UI.GoTo
{
    /// <summary>
    /// 读取Goto.xml文件配置信息
    /// </summary>
    public class GoToControlReadXml
    {
        public IClientServers IClientServers
        {
            get
            {
                return ServiceClient.GetService<IClientServers>();
            }
        }
        //读取模版的路径
        private readonly string _fileRootDirectory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "BusinessTemplates");
        //Xml的文件名称
        private const string TempalteFileName = "Goto.xml";

        /// <summary>
        /// 返回当前的配置文件中的所有信息
        /// </summary>
        /// <returns></returns>
        public List<GoToControl> GetGoToControlList()
        {
            var controls = new List<GoToControl>();
            //合并为完整的XML路径
            var templateFilePath = Path.Combine(_fileRootDirectory, TempalteFileName);
            var document = XDocument.Load(templateFilePath);
            var xmlitem = from item in document.Descendants(XName.Get("Item"))
                          select item;
            var xElements = xmlitem as IList<XElement> ?? xmlitem.ToList();
            if (xElements.Any())
            {
                foreach (var element in xElements)
                {
                    var go = new GoToControl();
                    var order = element.Attribute("Order");
                    var methods = element.Attribute("Methods");
                    var className = element.Attribute("ClassName");
                    var assembly = element.Attribute("Assembly");
                    var name = element.Attribute("Name");

                    if (order != null)
                    {
                        go.Order = int.Parse(order.Value);
                    }
                    if (methods != null)
                    {
                        go.Methods = methods.Value;
                    }
                    if (className != null)
                    {
                        go.ClassName = className.Value;
                    }
                    if (assembly != null)
                    {
                        go.Assembly = assembly.Value;
                    }
                    if (name != null)
                    {
                        go.Name = name.Value;
                    }
                    controls.Add(go);
                }
            }
            return controls;
        }
        /// <summary>
        /// 根据条件找到当前调用的方法，直接打开页面
        /// </summary>
        /// <param name="go">页面实体信息</param>
        /// <param name="name">控件的Name</param>
        public void Convertform(GoToObject go, string name)
        {
            //根据传的名称，找到节点的对应的信息内容
            var xml = GetGoToControlList().FirstOrDefault(n => n.Name == name);
            if (xml != null && go != null)
            {
                string assemblyDirectory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, xml.Assembly);
                Assembly assembly = Assembly.LoadFile(assemblyDirectory);
                Type type = assembly.GetType(xml.ClassName);
                if (type != null)
                {
                    var servertype = ServiceClient.GetClientService(type);
                    MethodInfo methodInfo = servertype.GetType().GetMethod(xml.Methods);
                    object[] oj = IClientServers.GetGotoparameters(go, xml.Methods, xml.Name);
                    if (oj.Any())
                    {
                        methodInfo.Invoke(servertype, oj);
                    }
                }
            }
        }


        //读取模版的路径
        private readonly string fileRootDirectory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "BusinessTemplates");
        //Xml的文件名称
        private const string tempalteFileName = "GotoSettingScope.xml";
        /// <summary>
        /// 记录查询范围
        /// </summary>
        /// <param name="SettingScope"></param>
        public void AddQueryConditions(string SettingScope)
        {

            var templateFilePath = Path.Combine(fileRootDirectory, tempalteFileName);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(templateFilePath);
            XmlNode root = xmlDoc.SelectSingleNode("Template");//查找<Template>   
            XmlElement xe1 = xmlDoc.CreateElement("Item");//创建一个<Item>节点   
            xe1.SetAttribute("UserId", LocalData.UserInfo.LoginID.ToString());//设置该节点Usrid属性   
            xe1.SetAttribute("SettingScope", SettingScope);//设置该节点stroriginal属性
            DirectoryInfo myDirectoryInfo = new DirectoryInfo(templateFilePath);
            myDirectoryInfo.Attributes &= ~FileAttributes.ReadOnly;
            if (root != null)
            {
                root.AppendChild(xe1);
                xmlDoc.Save(templateFilePath);
            }
        }
        /// <summary>
        /// 修改查询范围
        /// </summary>
        /// <param name="SettingScope"></param>
        public void UpdateQueryConditions(string SettingScope)
        {
            var templateFilePath = Path.Combine(fileRootDirectory, tempalteFileName);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(templateFilePath);
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("Template").ChildNodes;
            foreach (var xml in nodeList)
            {
                XmlElement xe = (XmlElement)xml;
                if (xe.GetAttribute("UserId") == LocalData.UserInfo.LoginID.ToString())
                {
                    xe.SetAttribute("SettingScope", SettingScope);
                }
            }
            xmlDoc.Save(templateFilePath);
        }
        /// <summary>
        /// 查找查询范围
        /// </summary>
        public string GetSettingScope()
        {
            string str = string.Empty;
            var templateFilePath = Path.Combine(fileRootDirectory, tempalteFileName);
            var document = XDocument.Load(templateFilePath);
            //根据当前的代码读取对应的XML段落
            var xmldocment = from ent in document.Descendants("Item") select ent;
            if (xmldocment.Any())
            {
                foreach (var xElement in xmldocment)
                {
                    Guid Userid = new Guid(xElement.Attribute("UserId").Value);
                    if (Userid == LocalData.UserInfo.LoginID)
                    {
                        str = xElement.Attribute("SettingScope").Value;
                    }
                }
            }
            return str;
        }
    }
}
