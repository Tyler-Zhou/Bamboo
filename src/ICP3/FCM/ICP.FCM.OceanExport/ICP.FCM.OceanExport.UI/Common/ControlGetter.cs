using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace ICP.FCM.OceanExport.UI.Common
{

    public class ControlGetter
        : IDisposable
    {
        static ControlGetter()
        {
            Current = new ControlGetter();
            if (string.IsNullOrEmpty(filePath))
            {
                GetFileFullPath();
            }
        }

        public static ControlGetter Current
        { get; set; }

        private static string filePath = string.Empty;

        private static void GetFileFullPath()
        {
            string fileRootDirectory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "BusinessTemplates");
            string tempalteFileName = "AutomaticTesting.xml";
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


        public List<ControlClass> Get()
        {
            XmlNode xmlNode = document.SelectSingleNode(string.Format("//{0}", "ControlConfig"));
            if (xmlNode == null)
                return null;

            XmlNodeList nodeList = xmlNode.ChildNodes;
            if (nodeList != null && nodeList.Count <= 0)
            {
                return null;
            }

            List<ControlClass> value = new List<ControlClass>();
            int count = nodeList.Count;
            for (int i = 0; i < count; i++)
            {
                XmlNode node = nodeList[i];
                if (node.NodeType != XmlNodeType.Comment)
                {
                    ControlClass info = new ControlClass();

                    info.ControlName = node.Attributes["ControlName"].Value.Trim();
                    info.ControlValue = node.Attributes["ControlValue"].Value.Trim();
                    info.ControlValueType = node.Attributes["ControlValueType"].Value.Trim();
                    value.Add(info);
                }
            }
            return value;
        }



        public void Dispose()
        {
            //if (DataList != null && DataList.Count > 0)
            //{
            //    DataList.Clear();
            //}
            //DataList = null;
        }

    }
}
