using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraGrid.Menu;
using System.Data;
using Microsoft.Practices.CompositeUI.Utility;
using Microsoft.Practices.CompositeUI;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using System.Windows.Forms;
using DevExpress.Utils.Menu;
using System.Reflection;

namespace ICP.Common.Business.ServiceInterface
{   
    /// <summary>
    /// 列表自定义右键菜单
    /// </summary>
    public class CustomGridMenu : GridViewMenu 
    {   
        public WorkItem RootWorkItem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }
        /// <summary>
        ///删除菜单拓展点名称。
        ///动态生成上下文菜单时，如果单击的时可拖入单元格，则需要动态生成删除文档菜单，删除菜单为二级结构，
        /// 即：删除+文档类型名称->具体文档名称
        /// 故需要动态添加删除具体文档菜单项的拓展点
        /// </summary>
        private string deleteAttachmentUISiteName = "DeleteAttachment_UISite";
        private int globalId = 1;
        public ListBaseBusinessPart CurrentPart
        {
            get;
            set;
        }
        public string TemplateCode
        {
            get;
            set;
        }
        public OperationType OperationType
        {
            get;
            set;
        }
        public BusinessContextMenuItemGeneratorFactory BusinessContextMenuItemGeneratorFactory
        {
            get
            {
                return ClientHelper.Get<BusinessContextMenuItemGeneratorFactory, BusinessContextMenuItemGeneratorFactory>();
            }
        }
        public override void Dispose()
        {
            if (this.CurrentPart != null)
            {
                this.CurrentPart = null;
            }
            base.Dispose();
        }
        /// <summary>
        /// 业务公共上下文菜单项
        /// </summary>
        private static ListDictionary<string,ContextMenuItemInfo> contextMenuCommonItemList=new ListDictionary<string,ContextMenuItemInfo>();
        private static object synObj=new object();
        private static bool IsCommonContextMenuItemExists(string templateCode)
        {
          lock(synObj.GetType())
          {
            return contextMenuCommonItemList.ContainsKey(templateCode);
          
          }
        }
        private List<ContextMenuItemInfo> GetCommonContextMenuItems(string templateCode)
        {
            if (IsCommonContextMenuItemExists(templateCode))
            {
                return contextMenuCommonItemList[templateCode];
            }
            else
            {
                return AddCommonContextMenuItems(templateCode);
            }
        }
        private List<ContextMenuItemInfo> AddCommonContextMenuItems(string templateCode)
        {   
            SectionKey key = new SectionKey { SectionCode = templateCode };

            List<ContextMenuItemInfo> items = ContextMenuFileLoader.Current[key]; ;
            lock (synObj)
            {
                contextMenuCommonItemList[templateCode].AddRange(items);
            }
            return items;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="view"></param>
        public CustomGridMenu(DevExpress.XtraGrid.Views.Grid.GridView view)
            : base(view)
        { 
           
        }
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="listPart"></param>
        /// <param name="row"></param>
        /// <param name="columnInfo"></param>
        public void InitData(ListBaseBusinessPart listPart, DataRow row, BusinessColumnInfo columnInfo,string templateCode)
        {
            this.CurrentPart = listPart;
            this.TemplateCode = templateCode;
            this.OperationType = listPart.OperationType;
            UnregisterDeleteAttachmentExtensionSite();
            //针对模板代码的公共右键菜单项
            List<ContextMenuItemInfo> commonItems = GetCommonContextMenuItems(this.TemplateCode);

            List<ContextMenuItemInfo> businessRelatedItems = GetBusinessRelatedContextMenuItems(row, columnInfo);

            InnerAddToolStripItems(commonItems, false);
            InnerAddToolStripItems(businessRelatedItems, true);
          

        }
        private void InnerAddToolStripItems(List<ContextMenuItemInfo> itemInfos, bool isBusiness)
        {

            foreach (ContextMenuItemInfo item in itemInfos)
            {

                MenuItemTag tag = new MenuItemTag(item.Name, string.IsNullOrEmpty(item.BusinessNo) ? item.Tag : item.BusinessNo);

                DXMenuItem menuItem = MenuItemFactory.GetMenuItem(item, tag);
                
                if (!string.IsNullOrEmpty(item.Name))
                {
                    menuItem.Click += new EventHandler(toolStripItem_Click);

                }
                if (!isBusiness)
                {

                    RootWorkItem.UIExtensionSites[item.Site].Add(menuItem);
                }
                else
                {
                    RootWorkItem.UIExtensionSites[item.Site].Insert(0, menuItem);
                }

                if (!string.IsNullOrEmpty(item.RegisterSite) && !RootWorkItem.UIExtensionSites.Contains(item.RegisterSite))
                {
                    RootWorkItem.UIExtensionSites.RegisterSite(item.RegisterSite, menuItem);
                }
            }
        }
        void toolStripItem_Click(object sender, EventArgs e)
        {
            if (RaiseClickEvent(sender, null)) return;
            DXMenuItem item = sender as DXMenuItem;
            RootWorkItem.State["CurrentBaseBusinessPart"] = this.CurrentPart;
            MenuItemTag tag=(MenuItemTag)item.Tag;
            this.CurrentPart.CurrentContextMenuItemTag = tag.Tag;
            string itemName = tag.Name;
            if (!this.CurrentPart.LocalCommands.Contains(itemName))
            {
                RootWorkItem.Commands[itemName].Execute();
            }
            else
            {
                MethodInfo method = this.CurrentPart.GetType().GetMethod(itemName);
                method.Invoke(this.CurrentPart, new object[] { sender, e });
            }
        }
        /// <summary>
        /// 1.根据点击所在的列，判断是否列为可拖入列，动态添加上传和删除附件菜单项
        ///2.根据业务类型和当前数据行添加业务相关的菜单项
        /// </summary>
        /// <param name="row"></param>
        /// <param name="columnInfo"></param>
        /// <returns></returns>
        private List<ContextMenuItemInfo> GetBusinessRelatedContextMenuItems(DataRow row, BusinessColumnInfo columnInfo)
        {
            //业务菜单项
            IBusinessContextMenuItemGenerator generator = BusinessContextMenuItemGeneratorFactory.Get(this.OperationType);
            string contextMenuStripUISiteName = this.CurrentPart.GetBusinessContextMenuStripUISiteName();
            List<ContextMenuItemInfo> businessItems = generator.Get(row, contextMenuStripUISiteName);
            //附件相关菜单项
            List<ContextMenuItemInfo> attachmentItems = new List<ContextMenuItemInfo>();

            //添加上传附件菜单项
            if (columnInfo != null)
            {
                string normalSeparator = CommonConstants.NormalSeparator;
                DocumentType documentType = columnInfo.DocumentType;
                string text = string.Format(LocalData.IsEnglish ? "Upload {0} Copy" : "上传{0} Copy", documentType);
                attachmentItems.Add(InnerCreateItemInfo((globalId++).ToString(), text, CommonConstants.Command_Add_Attachment_Name, contextMenuStripUISiteName, string.Empty, string.Empty,ContextMenuItemType.MenuItem, documentType.ToString()));

                //添加删除附件菜单项，二级结构：删除附件->附件名称列表
                string copyNames = row.Field<string>(columnInfo.FieldName);
                if (!string.IsNullOrEmpty(copyNames))
                {
                    List<string> listCopyName = copyNames.Split(normalSeparator.ToCharArray()).Where(copyName => !string.IsNullOrEmpty(copyName)).ToList();
                    //反转列表，以文档的显示顺序构造菜单
                    listCopyName.Reverse();
                    if (listCopyName.Count > 0)
                    {
                        string deleteAttachmentText = string.Format(LocalData.IsEnglish ? "Delete {0} Copy" : "删除 {0} Copy", documentType);
                        attachmentItems.Add(InnerCreateItemInfo((globalId++).ToString(), deleteAttachmentText, string.Empty, contextMenuStripUISiteName, deleteAttachmentUISiteName, string.Empty,ContextMenuItemType.SubMenuItem, string.Empty));
                        Guid operationId = row.Field<Guid>("ID");
                        listCopyName.ForEach(copyName =>
                        {
                            string fileName = copyName;
                            DocumentInfo temp = this.CurrentPart.ListAddedDocuments.Find(document => document.OperationID == operationId && document.DocumentType == documentType && document.Name == copyName);
                            if (temp != null)
                            {
                                fileName = temp.OriginalPath;
                            }
                            attachmentItems.Add(InnerCreateItemInfo((globalId++).ToString(), copyName, CommonConstants.Command_Delete_Attachment_Name, deleteAttachmentUISiteName, string.Empty, string.Empty,ContextMenuItemType.MenuItem, fileName));

                        });
                    }

                }

            }
            return businessItems.Concat(attachmentItems).ToList();
        }
        public ContextMenuItemInfo InnerCreateItemInfo(string id, string text, string name, string site, string registerSite, string imageName, ContextMenuItemType type, string businessNo)
        {
            ContextMenuItemInfo itemInfo = new ContextMenuItemInfo();
            itemInfo.Id = id;
            itemInfo.Name = name;
            itemInfo.ImageName = imageName;
            itemInfo.RegisterSite = registerSite;
            itemInfo.Site = site;
            itemInfo.Text = text;
            itemInfo.Type =type;
            itemInfo.BusinessNo = businessNo;

            return itemInfo;
        }

        private void UnregisterDeleteAttachmentExtensionSite()
        {
            //移除删除附件UI拓展点
            if (this.RootWorkItem.UIExtensionSites.Contains(deleteAttachmentUISiteName))
            {
                UIExtensionSite site = this.RootWorkItem.UIExtensionSites[deleteAttachmentUISiteName];
                site.Clear();
                this.RootWorkItem.UIExtensionSites.UnregisterSite(deleteAttachmentUISiteName);
            }
        }
        //protected override void CreateItems()
        //{
        //    this.Items.Clear();

        //}
        protected override void OnMenuItemClick(object sender, EventArgs e)
        {
            base.OnMenuItemClick(sender, e);
        }
    }
    /// <summary>
    /// 右键菜单项Tag信息类
    /// </summary>
    public struct MenuItemTag
    {
        public string Name;
        public object Tag;
        public MenuItemTag(string name, object tag)
        {
            this.Name = name;
            this.Tag = tag;
        }
    }
}
