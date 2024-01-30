using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Business.Common.UI.Document
{
    /// <summary>
    /// 自定义枚举获取器
    /// </summary>
    public class EnumGetter : IDisposable
    {
        static EnumGetter()
        {
            Current = new EnumGetter();
            if (string.IsNullOrEmpty(filePath))
            {
                GetFileFullPath();
            }
        }
        public static EnumGetter Current
        { get; set; }

        public List<CustomEnumInfo> this[Type enumType, bool isToolBar, OperationType operationType]
        {
            get { return Get(enumType, isToolBar, operationType); }

        }

        public Dictionary<OperationType, List<CustomEnumInfo>> DataList = new Dictionary<OperationType, List<CustomEnumInfo>>();

        private static string filePath = string.Empty;

        private static void GetFileFullPath()
        {
            string fileRootDirectory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "BusinessTemplates");
            string tempalteFileName = "EnumConfig.xml";
            filePath = Path.Combine(fileRootDirectory, tempalteFileName);
        }

        private static XmlDocument _document;
        public static XmlDocument document
        {
            get
            {
                if (_document == null)
                {
                    _document = new XmlDocument();
                    _document.Load(filePath);
                }

                return _document;
            }
        }

        public List<CustomEnumInfo> Get(Type enumType, bool isToolBar, OperationType operationType)
        {
            //if (DataList.ContainsKey(operationType))
            //{
            //    List<CustomEnumInfo> entity;
            //    DataList.TryGetValue(operationType, out entity);

            //    return entity;
            //}

            XmlNode xmlNode = document.SelectSingleNode(string.Format("//{0}", GetEnumTypeToString(enumType)));
            if (xmlNode == null)
                return null;

            XmlNodeList nodeList = xmlNode.ChildNodes;
            if (nodeList != null && nodeList.Count <= 0)
            {
                return null;
            }

            List<CustomEnumInfo> value = new List<CustomEnumInfo>();
            int count = nodeList.Count;
            for (int i = 0; i < count; i++)
            {
                XmlNode node = nodeList[i];
                if (node.NodeType != XmlNodeType.Comment)
                {
                    if (isToolBar)
                    {
                        if (node.HasChildNodes)
                        {
                            CustomEnumInfo info = new CustomEnumInfo(node.Attributes["cCaption"].Value.Trim(),
                                                                     node.Attributes["eCaption"].Value.Trim());
                            info.Etip = node.Attributes["cCaption"].Value.Trim();
                            info.Ctip = node.Attributes["eCaption"].Value.Trim();
                            if (node.Attributes["Tag"] != null)
                                info.Tag = node.Attributes["Tag"].Value.Trim();
                            string optype = node.Attributes["OperationType"].Value.Trim();
                            info.Type = (OperationType)Enum.Parse(typeof(OperationType), optype, false);
                            if (info.Type != OperationType.Unknown && info.Type != operationType)
                            {
                                continue;
                            }

                            info.HasChildNodes = true;
                            List<ChildrenEnumInfo> subList = new List<ChildrenEnumInfo>();
                            int subCount = node.ChildNodes.Count;
                            for (int c = 0; c < subCount; c++)
                            {
                                ChildrenEnumInfo childInfo = new ChildrenEnumInfo();
                                string chType = node.ChildNodes[c].Attributes["OperationType"].Value.Trim();

                                childInfo.Type = (OperationType)Enum.Parse(typeof(OperationType), chType, false);

                                if (childInfo.Type != OperationType.Unknown && childInfo.Type != operationType)
                                {
                                    continue;
                                }

                                childInfo.Caption = node.ChildNodes[c].Attributes["Caption"].Value.Trim();
                                childInfo.Etip = node.ChildNodes[c].Attributes["Etip"].Value.Trim();
                                childInfo.Ctip = node.ChildNodes[c].Attributes["Ctip"].Value.Trim();
                                subList.Add(childInfo);
                            }

                            info.ChildrenNodes = subList;
                            value.Add(info);
                        }
                        else
                        {
                            CustomEnumInfo info = new CustomEnumInfo();
                            info.HasChildNodes = false;
                            List<ChildrenEnumInfo> subList = new List<ChildrenEnumInfo>();
                            ChildrenEnumInfo subInfo = new ChildrenEnumInfo();

                            string optype = node.Attributes["OperationType"].Value.Trim();
                            info.Type = (OperationType)Enum.Parse(typeof(OperationType), optype, false);

                            if (info.Type != OperationType.Unknown && info.Type != operationType)
                            {
                                continue;
                            }

                            info.eCaption = info.cCaption = subInfo.Caption = node.Attributes["Caption"].Value.Trim();
                            info.Ctip = subInfo.Ctip = node.Attributes["Ctip"].Value.Trim();
                            info.Etip = subInfo.Etip = node.Attributes["Etip"].Value.Trim();
                            if (node.Attributes["Tag"] != null)
                                info.Tag = node.Attributes["Tag"].Value.Trim();
                            subList.Add(subInfo);
                            info.ChildrenNodes = subList;
                            value.Add(info);
                        }
                    }
                    else
                    {
                        if (node.HasChildNodes)
                        {
                            CustomEnumInfo item = new CustomEnumInfo(node.Attributes["cCaption"].Value.Trim(),
                                                                     node.Attributes["eCaption"].Value.Trim());

                            string optype = node.Attributes["OperationType"].Value.Trim();
                            item.Type = (OperationType)Enum.Parse(typeof(OperationType), optype, false);

                            if (item.Type != OperationType.Unknown && item.Type != operationType)
                            {
                                continue;
                            }

                            item.Etip = node.Attributes["eCaption"].Value.Trim();
                            item.Ctip = node.Attributes["cCaption"].Value.Trim();
                            item.HasChildNodes = true;
                            Guid parentID = new Guid(node.Attributes["ID"].Value.Trim());
                            if (node.Attributes["Tag"] != null)
                                item.Tag = node.Attributes["Tag"].Value.Trim();
                            item.ParentID = Guid.NewGuid();
                            item.ID = parentID;
                            value.Add(item);

                            int subCount = node.ChildNodes.Count;
                            for (int c = 0; c < subCount; c++)
                            {
                                CustomEnumInfo subItem =
                                    new CustomEnumInfo(node.ChildNodes[c].Attributes["Caption"].Value.Trim(),
                                                       node.ChildNodes[c].Attributes["Caption"].Value.Trim());

                                string chType = node.ChildNodes[c].Attributes["OperationType"].Value.Trim();

                                subItem.Type = (OperationType)Enum.Parse(typeof(OperationType), chType, false);

                                if (subItem.Type != OperationType.Unknown && subItem.Type != operationType)
                                {
                                    continue;
                                }

                                subItem.HasChildNodes = false;
                                subItem.Etip = node.ChildNodes[c].Attributes["Etip"].Value.Trim();
                                subItem.Ctip = node.ChildNodes[c].Attributes["Ctip"].Value.Trim();
                                if (node.ChildNodes[c].Attributes["Tag"] != null)
                                    subItem.Tag = node.ChildNodes[c].Attributes["Tag"].Value.Trim();

                                subItem.ParentID = parentID;
                                subItem.ID = Guid.NewGuid();
                                value.Add(subItem);
                            }
                        }
                        else
                        {
                            CustomEnumInfo subItem = new CustomEnumInfo(node.Attributes["Caption"].Value.Trim(),
                                                                        node.Attributes["Caption"].Value.Trim());

                            string optype = node.Attributes["OperationType"].Value.Trim();
                            subItem.Type = (OperationType)Enum.Parse(typeof(OperationType), optype, false);

                            if (subItem.Type != OperationType.Unknown && subItem.Type != operationType)
                            {
                                continue;
                            }

                            subItem.HasChildNodes = false;
                            Guid newID = Guid.NewGuid();
                            subItem.ID = newID;
                            subItem.ParentID = Guid.NewGuid();
                            subItem.Ctip = node.Attributes["Ctip"].Value.Trim();
                            subItem.Etip = node.Attributes["Etip"].Value.Trim();
                            if (node.Attributes["Tag"] != null)
                                subItem.Tag = node.Attributes["Tag"].Value.Trim();

                            value.Add(subItem);
                        }
                    }
                }
            }

            //DataList.Add(operationType, value);
            return value;

        }

        private string GetEnumTypeToString(Type enumType)
        {
            string strEnum = string.Empty;
            strEnum = string.Format("{0}Config", enumType.Name);

            return strEnum;
        }

        public void Remove(OperationType type)
        {
            if (DataList.ContainsKey(type))
            {
                DataList.Remove(type);
            }
        }

        public void Remove(Type type)
        {
            //if (DataList.ContainsKey(type))
            //{
            //    DataList.Remove(type);
            //}
        }

        #region IDisposable 成员

        public void Dispose()
        {
            if (DataList != null && DataList.Count > 0)
            {
                DataList.Clear();
            }
            DataList = null;
        }

        #endregion
    }
}
