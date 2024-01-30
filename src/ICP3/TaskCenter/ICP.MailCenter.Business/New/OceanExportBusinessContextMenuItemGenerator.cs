using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.Utility;
namespace ICP.Operation.Common.ServiceInterface
{  
    /// <summary>
    /// 海出业务上下文菜单项产生器
    /// </summary>
   public class OceanExportBusinessContextMenuItemGenerator:IBusinessContextMenuItemGenerator
    {   
       private string registerUISiteName;
       

        public List<ContextMenuItemInfo> Get(DataRow row,string registerUISiteName)
        {
            this.registerUISiteName = registerUISiteName;
            List<ContextMenuItemInfo> items = new List<ContextMenuItemInfo>();
            if (row == null || !row.Table.Columns.Contains("BLNO"))//BLNO提单号
                return items;
            string refNo = row["BLNO"].ToString();
            ListDictionary<string, string> dicNoPair = MailBusinessHelper.ExtractNoPairs(refNo, Environment.NewLine.ToCharArray(), ':');
            if (dicNoPair.Count <= 0)
                return items;
            items = GetOEBusinessContextMenuItems(dicNoPair);
            return items;
        }

        public List<ContextMenuItemInfo> GetOEBusinessContextMenuItems(ListDictionary<string, string> dicNoPair)
        {
            string mblTypeName = "MBL";
            string hblTypeName = "HBL";
            List<string> mblNos = GetConcreateNos(mblTypeName, dicNoPair);
            List<string> hblNos = GetConcreateNos(hblTypeName, dicNoPair);
            List<ContextMenuItemInfo> mblItems = GetItemInfos(mblNos, mblTypeName);
            List<ContextMenuItemInfo> hblItems = GetItemInfos(hblNos, hblTypeName);
            List<ContextMenuItemInfo> list = hblItems.Concat(mblItems).ToList();

            //if (!IsCurrentBusinessRelated())
            //{
            //    string text = IsEnglish ? "Associate Current Shipment" : "关联当前业务";
            //    ContextMenuItemInfo item = InnerCreateItemInfo("MessageRelation", text, string.Format("Command_{0}EmailOperationMessageRelation", this.SourceType.ToString()), "EmailCenterContextMenu", "", "", string.Empty);
            //    list.Add(item);
            //}
            return list;



        }

        private List<ContextMenuItemInfo> GetItemInfos(List<string> nos, string typeName)
        {
            List<ContextMenuItemInfo> items = new List<ContextMenuItemInfo>();
            foreach (string mblNo in nos)
            {
                items.Add(CreateItemInfo(typeName, mblNo));
            }
            return items;
        }
        private ContextMenuItemInfo CreateItemInfo(string typeName, string no)
        {
            string text = (LocalData.IsEnglish ? string.Format("Open {0}", typeName) : string.Format("打开{0}", typeName)) + ":" + no;
            string name = string.Format("Command_CommunicationOpen{0}", typeName);
            string site = this.registerUISiteName;
            string id = Guid.NewGuid().ToString();
            return InnerCreateItemInfo(id, text, name, site, string.Empty, string.Empty, no);
        }
        public ContextMenuItemInfo InnerCreateItemInfo(string id, string text, string name, string site, string registerSite, string imageName, string businessNo)
        {
            ContextMenuItemInfo itemInfo = new ContextMenuItemInfo();
            itemInfo.Id = id;
            itemInfo.Name = name;
            itemInfo.ImageName = imageName;
            itemInfo.RegisterSite = registerSite;
            itemInfo.Site = site;
            itemInfo.Text = text;
            itemInfo.Type = ContextMenuItemType.MenuItem;
            itemInfo.BusinessNo = businessNo;
            return itemInfo;
        }
        private List<string> GetConcreateNos(string key, ListDictionary<string, string> pairs)
        {
            if (pairs.ContainsKey(key))
                return pairs[key];
            else
                return new List<string>();
        }

    }
}
