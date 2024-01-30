using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.Practices.CompositeUI;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 读取Tab的XML配置文件的类
    /// </summary>
    public class TabControlReadXml
    {   
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        //读取模版的路径
        private readonly string _fileRootDirectory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "BusinessTemplates");
        //Xml的文件名称
        private const string TempalteFileName = "TabControlConfig.xml";

        /// <summary>
        /// 根据Code读取Xml的配置信息
        /// </summary>
        /// <param name="code">代码名称</param>
        /// <returns></returns>
        public List<TabControl> GetTabControlList(string code)
        {  
            
            var tabControls = new List<TabControl>();
            //合并为完整的XML路径
            var templateFilePath = Path.Combine(_fileRootDirectory, TempalteFileName);
            var document = XDocument.Load(templateFilePath);
            //根据当前的代码读取对应的XML段落
            var xmldocment = from ent in document.Descendants(code) select ent;
            if (xmldocment.Any())
            {
                //读取Item段落的信息
                var xmlitem = from item in xmldocment.Descendants(XName.Get("Item"))
                              select item;
                foreach (var xElement in xmlitem)
                {
                    var tab = new TabControl();
                    var order = xElement.Attribute("Order");
                    if (order != null)
                    {
                        tab.Order = int.Parse(order.Value);
                    }
                    var selected = xElement.Attribute("Selected");
                    if (selected != null)
                        switch (selected.Value)
                        {
                            case "true":
                                tab.Selected = true;
                                break;
                            default:
                                tab.Selected = false;
                                break;
                        }
                    var cname = xElement.Attribute("Cname");
                    if (cname != null)
                    {
                        tab.Cname = cname.Value;
                    }
                    var ename = xElement.Attribute("Ename");
                    if (ename != null)
                    {
                        tab.Ename = ename.Value;
                    }
                    var control = xElement.Attribute("Control").Value;
                    if (!string.IsNullOrEmpty(control))
                    {
                        var attribute = xElement.Attribute("Assembly").Value;
                        if (!string.IsNullOrEmpty(attribute))
                        {
                            var partType = Type.GetType(string.Format("{0}.{1},{0}", attribute, control));
                            if (partType != null)
                            {
                               // var assembly = Assembly.GetAssembly(partType);
                                tab.Control = this.WorkItem.Items.AddNew(partType) as UserControl;
                                //tab.Control = (UserControl)assembly.CreateInstance(attribute + "." + control);
                            }
                        }
                    }
                    var dictionary = new Dictionary<int, bool> { { tab.Order, false } };
                    tab.Dictionary = dictionary;
                    tabControls.Add(tab);
                }
            }
            return tabControls.OrderBy(n => n.Order).ToList();
        }
    }
}
