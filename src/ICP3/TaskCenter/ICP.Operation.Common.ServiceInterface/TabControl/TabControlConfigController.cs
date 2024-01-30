using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// TabControl配置控制器类（读取配置，创建具体页控件)
    /// </summary>
    public class TabControlConfigController
    {  
        private TabControlConfigController()
        {
         
        }
         
        public static WorkItem WorkItem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }
        private static TabControlConfigController controller = new TabControlConfigController();
        private static object synObj = new object();
        public static TabControlConfigController Current
        {
            get
            {
                return controller;
            }
        }
        //读取模版的路径
        private readonly string fileRootDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BusinessTemplates");
        //Xml的文件名称
        private const string TempalteFileName = "TabControlConfig.xml";

        /// <summary>
        /// 根据模板代码读取Tab子叶的配置信息列表
        /// </summary>
        /// <param name="code">代码名称</param>
        /// <returns></returns>
        public List<TabItemConfigInfo> GetTabItemList(string code)
        {

            var tabControls = new List<TabItemConfigInfo>();
            //合并为完整的XML路径
            var templateFilePath = Path.Combine(fileRootDirectory, TempalteFileName);
            var document = XDocument.Load(templateFilePath);
            //根据当前的代码读取对应的XML段落
            var xmlTabItemConfigs = document.Descendants(code);
            if (xmlTabItemConfigs.Any())
            {
                //读取Item段落的信息
                var xmlitem = from item in xmlTabItemConfigs.Descendants(XName.Get("Item"))
                              select item;
                foreach (var xElement in xmlitem)
                {
                    var tabItem = new TabItemConfigInfo();
                    tabItem.Order = Int32.Parse(xElement.Attribute("Order").Value);

                    tabItem.Selected = bool.Parse(xElement.Attribute("Selected").Value);
                    tabItem.CName = xElement.Attribute("Cname").Value;
                    tabItem.EName= xElement.Attribute("Ename").Value;
                    tabItem.ControlFullName = xElement.Attribute("ControlFullName").Value;
                    if (xElement.Attribute("ReadOnly") != null)
                    {
                        tabItem.ReadOnly = bool.Parse(xElement.Attribute("ReadOnly").Value);
                    }
                    else
                    {
                        tabItem.ReadOnly = true;
                    }
                    tabControls.Add(tabItem);
                }
            }
            document = null;
            return tabControls.OrderBy(n => n.Order).ToList();
        }
    }
}
