using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.MailCenter.Business.ServiceInterface
{ 
    /// <summary>
    /// 业务列表右键菜单项载入类
    /// </summary>
   public class ContextMenuFileLoader:IDisposable
    {
        private readonly string fileRootDirectory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "BusinessTemplates");
        private readonly string tempalteFileName = "ContextMenuTemplate.xml";
       private static ContextMenuFileLoader loader;
       private Dictionary<SectionKey, List<ContextMenuItemInfo>> dicItems = new Dictionary<SectionKey, List<ContextMenuItemInfo>>();
       private ContextMenuFileLoader() { }
       private static object synObj = new object();
       /// <summary>
       /// 唯一实例
       /// </summary>
       public static ContextMenuFileLoader Current
       {
           get {
               if (loader == null)
               {
                   lock (synObj.GetType())
                   {
                       if (loader == null)
                       {
                           loader = new ContextMenuFileLoader();
                       }
                   }
                  
               }
               return loader;
           }
       }
       public List<ContextMenuItemInfo> this[SectionKey key]
       {
           get {
               return GetContextMenuItems(key.SectionCode, key.Type);
           }
       }
       public List<ContextMenuItemInfo> GetContextMenuItems(string sectionCode, BusinessType type)
       {
          
           SectionKey key = new SectionKey {SectionCode=sectionCode,Type=type };
           if (dicItems.ContainsKey(key))
               return dicItems[key];
           List<ContextMenuItemInfo> items = GetItems(sectionCode, type);
           dicItems.Add(key, items);
           return items;
       }

       private List<ContextMenuItemInfo> GetItems(string sectionCode, BusinessType type)
       {
           string templateFilePath = Path.Combine(fileRootDirectory, tempalteFileName);
           XDocument document = XDocument.Load(templateFilePath);
           var elements = document.Descendants(XName.Get(sectionCode)).First().Elements(XName.Get(type.ToString())).Descendants(XName.Get("Item"));
           if (elements.Count() <= 0)
               return new List<ContextMenuItemInfo>();
           List<ContextMenuItemInfo> items = new List<ContextMenuItemInfo>();
           foreach (var element in elements)
           {
               ContextMenuItemInfo item = new ContextMenuItemInfo();
               item.Init(element);
               items.Add(item);
           }
           return items;
       }


       #region IDisposable 成员

       public void Dispose()
       {
           if (dicItems != null)
           {
               dicItems.Clear();
               dicItems = null;
           }
       }

       #endregion
    }
    /// <summary>
    /// 上下文菜单项缓存键
    /// </summary>
   public sealed class SectionKey:IEquatable<SectionKey>
   {
       public string SectionCode { get; set; }
       public BusinessType Type { get; set; }


       public bool Equals(SectionKey other)
       {
           return ((this.SectionCode.ToLower() == other.SectionCode.ToLower()) && (this.Type == other.Type));
       }
   }
}
