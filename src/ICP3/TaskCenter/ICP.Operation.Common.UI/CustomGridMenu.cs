using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Views.Grid;
using ICP.DataCache.ServiceInterface;
using ICP.FileSystem.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Operation.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace ICP.Operation.Common.UI
{
    /// <summary>
    /// 列表自定义右键菜单
    /// </summary>
    public class CustomGridMenu : GridViewMenu
    {
        #region Fields & Property & Services
        /// <summary>
        /// Lock Object
        /// </summary>
        private static object synObj = new object();

        /// <summary>
        /// 业务公共上下文菜单项
        /// </summary>
        private static ListDictionary<string, ContextMenuItemInfo> contextMenuCommonItemList = new ListDictionary<string, ContextMenuItemInfo>();

        /// <summary>
        /// 删除菜单拓展点名称。
        /// 动态生成上下文菜单时，如果单击的时可拖入单元格，则需要动态生成删除文档菜单，删除菜单为二级结构，
        /// 即：删除+文档类型名称->具体文档名称
        /// 故需要动态添加删除具体文档菜单项的拓展点
        /// </summary>
        private const string deleteAttachmentUISiteName = "DeleteAttachment_UISite";

        /// <summary>
        /// WorkItem
        /// </summary>
        public WorkItem RootWorkItem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }

        /// <summary>
        /// 全局ID
        /// </summary>
        private int globalId = 1;

        /// <summary>
        /// 菜单索引
        /// </summary>
        public int MenuItemIndex = 1;
        /// <summary>
        /// 子菜单项
        /// </summary>
        public int SubitemmenuItem = 1;

        /// <summary>
        /// 当前业务列表面板
        /// </summary>
        public ListBaseBusinessPart CurrentPart
        {
            get;
            set;
        }
        /// <summary>
        /// 模板代码
        /// </summary>
        public string TemplateCode
        {
            get;
            set;
        }
        /// <summary>
        /// 业务类型
        /// </summary>
        public OperationType OperationType
        {
            get;
            set;
        }
        /// <summary>
        /// 业务上下文菜单项产生器工厂
        /// </summary>
        public BusinessContextMenuItemGeneratorFactory BusinessContextMenuItemGeneratorFactory
        {
            get
            {
                return ClientHelper.Get<BusinessContextMenuItemGeneratorFactory, BusinessContextMenuItemGeneratorFactory>();
            }
        } 
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="view"></param>
        public CustomGridMenu(GridView view)
            : base(view)
        {

        }
        /// <summary>
        /// 重写释放
        /// </summary>
        public override void Dispose()
        {
            if (CurrentPart != null)
            {
                CurrentPart = null;
            }
            base.Dispose();
        }
        
        /// <summary>
        /// 文本菜单是否存在
        /// </summary>
        /// <param name="templateCode"></param>
        /// <returns></returns>
        private static bool IsCommonContextMenuItemExists(string templateCode)
        {
            lock (synObj.GetType())
            {
                return contextMenuCommonItemList.ContainsKey(templateCode);

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="templateCode"></param>
        /// <returns></returns>
        private List<ContextMenuItemInfo> GetCommonContextMenuItems(string templateCode)
        {
            return IsCommonContextMenuItemExists(templateCode) ? contextMenuCommonItemList[templateCode] : AddCommonContextMenuItems(templateCode);
        }

        private List<ContextMenuItemInfo> AddCommonContextMenuItems(string templateCode)
        {
            SectionKey key = new SectionKey { SectionCode = templateCode };

            List<ContextMenuItemInfo> items = ContextMenuFileLoader.Current[key];
            //显示右键菜单快捷键
            if (LocalData.ApplicationType == ApplicationType.EmailCenter)
            {
                foreach (ContextMenuItemInfo item in items)
                {
                    Keys hotkey = CurrentPart.SetContextMenuItemKeys(item.Id);
                    if (hotkey != Keys.None)
                    {
                        int w = 50 - Encoding.Default.GetByteCount(item.Text);
                        item.Text = item.Text.PadRight(w) + "Ctrl+" + hotkey.ToString().Substring(hotkey.ToString().Length - 1);
                    }
                }
            }
            lock (synObj)
            {
                contextMenuCommonItemList[templateCode].AddRange(items);
            }
            return items;
        }
        
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="listPart">业务列表面板</param>
        /// <param name="row">数据行</param>
        /// <param name="columnInfo">业务面板列信息类</param>
        /// <param name="templateCode">模板代码</param>
        public void InitData(ListBaseBusinessPart listPart, DataRow row, BusinessColumnInfo columnInfo, string templateCode)
        {
            CurrentPart = listPart;
            TemplateCode = templateCode;
            if (row.Table.Columns.Contains("OperationType"))
            {
                OperationType = (OperationType)int.Parse(row["OperationType"].ToString());
            }
            else
            {
                OperationType = listPart.OperationType;
            }
            UnregisterDeleteAttachmentExtensionSite();
            //针对模板代码的公共右键菜单项
            List<ContextMenuItemInfo> commonItems = null;
            //if (this.OperationType == OperationType.OceanImport && templateCode == "MailLink4Customer")
            //    commonItems = GetCommonContextMenuItems("OceanImport" + this.TemplateCode);
            //else
            //    commonItems = GetCommonContextMenuItems(this.TemplateCode);
            if (LocalData.ApplicationType == ApplicationType.ICP)
                commonItems = GetCommonContextMenuItems(TemplateCode);
            else
                commonItems = GetCommonContextMenuItems(string.Format("{0}_{1}", TemplateCode, OperationType.ToString()));

            List<ContextMenuItemInfo> businessRelatedItems = GetBusinessRelatedContextMenuItems(row, columnInfo);
            IEnumerable<ContextMenuItemInfo> contextMenuItemInfos = GetContextMenuItems(commonItems,row);
            InnerAddToolStripItems(contextMenuItemInfos, true);
            InnerAddToolStripItems(businessRelatedItems, true);

        }
        /// <summary>
        /// 添加右键菜单
        /// </summary>
        /// <param name="itemInfos">上下文菜单项信息列表</param>
        /// <param name="isBusiness">是否业务</param>
        private void InnerAddToolStripItems(IEnumerable<ContextMenuItemInfo> itemInfos, bool isBusiness)
        {
            foreach (ContextMenuItemInfo item in itemInfos)
            {
                MenuItemTag tag = new MenuItemTag(item.Name, string.IsNullOrEmpty(item.BusinessNo) ? item.Tag : item.BusinessNo);
                DXMenuItem menuItem = MenuItemFactory.GetMenuItem(item, tag);
                menuItem.Enabled = item.Enabled;

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
                //当前服务是否已经存在,存在制空
                else if (RootWorkItem.UIExtensionSites.Contains(item.RegisterSite))
                {
                    RootWorkItem.UIExtensionSites.UnregisterSite(item.RegisterSite);
                    RootWorkItem.UIExtensionSites.RegisterSite(item.RegisterSite, menuItem);
                }

            }
        }
        /// <summary>
        /// 菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void toolStripItem_Click(object sender, EventArgs e)
        {
            if (RaiseClickEvent(sender, null)) return;
            DXMenuItem item = sender as DXMenuItem;
            if (item == null) return;
            RootWorkItem.State["CurrentBaseBusinessPart"] = CurrentPart;
            MenuItemTag tag = (MenuItemTag)item.Tag;
            CurrentPart.CurrentContextMenuItemTag = tag.Tag;
            string itemName = tag.Name;
            if (!CurrentPart.LocalCommands.Contains(itemName))
            {
                RootWorkItem.Commands[itemName].Execute();
            }
            else
            {
                MethodInfo method = CurrentPart.GetType().GetMethod(itemName);
                method.Invoke(CurrentPart, new object[] { sender, e });
            }
        }

        /// <summary>
        /// 1.根据点击所在的列，判断是否列为可拖入列，动态添加上传和删除附件菜单项
        ///2.根据业务类型和当前数据行添加业务相关的菜单项
        /// </summary>
        /// <param name="row">数据行</param>
        /// <param name="columnInfo">业务面板列信息</param>
        /// <returns></returns>
        public List<ContextMenuItemInfo> GetBusinessRelatedContextMenuItems(DataRow row, BusinessColumnInfo columnInfo)
        {
            //业务菜单项
            IBusinessContextMenuItemGenerator generator = BusinessContextMenuItemGeneratorFactory.Get(OperationType);
            string contextMenuStripUISiteName = CurrentPart.GetBusinessContextMenuStripUISiteName();
            List<ContextMenuItemInfo> businessItems = generator.Get(row, contextMenuStripUISiteName);

            #region 业务菜单项快捷键（禁用——已注释）

            //////显示右键菜单快捷键
            ////if (LocalData.ApplicationType == ApplicationType.EmailCenter)
            ////{
            ////    int n = businessItems.Count();
            ////    foreach (ContextMenuItemInfo item in businessItems)
            ////    {
            ////        int w = 50 - Encoding.Default.GetByteCount(item.Text);
            ////        item.Text = item.Text.PadRight(w) + "Ctrl+" + n--;
            ////    }
            ////}

            #endregion

            //附件相关菜单项
            List<ContextMenuItemInfo> attachmentItems = new List<ContextMenuItemInfo>();

            //添加上传附件菜单项
            if (columnInfo != null)
            {
                string normalSeparator = CommonConstants.NormalSeparator;
                DocumentType documentType = columnInfo.DocumentType;
                string text = string.Format(LocalData.IsEnglish ? "Upload {0} Copy" : "上传{0} Copy", documentType);
                attachmentItems.Add(InnerCreateItemInfo((globalId++).ToString(), text, CommonConstants.Command_Add_Attachment_Name, contextMenuStripUISiteName, string.Empty, string.Empty, ContextMenuItemType.MenuItem, documentType.ToString()));

                //添加删除附件菜单项，二级结构：删除附件->附件名称列表
                string copyNames = row.Field<string>(columnInfo.FieldName);
                RootWorkItem.State["FieldName"] = columnInfo.FieldName;
                if (!string.IsNullOrEmpty(copyNames))
                {
                    List<string> listCopyName = copyNames.Split(normalSeparator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
                    //反转列表，以文档的显示顺序构造菜单
                    listCopyName.Reverse();
                    if (listCopyName.Count > 0)
                    {
                        string deleteAttachmentText = string.Format(LocalData.IsEnglish ? "Delete {0} Copy" : "删除 {0} Copy", documentType);
                        attachmentItems.Add(InnerCreateItemInfo((globalId++).ToString(), deleteAttachmentText, string.Empty, contextMenuStripUISiteName, deleteAttachmentUISiteName, string.Empty, ContextMenuItemType.SubMenuItem, string.Empty));
                        Guid operationId = row.Field<Guid>(Constants.OperationIDFieldName);
                        listCopyName.ForEach(copyName =>
                        {
                            string fileName = copyName;
                            DocumentInfo temp = CurrentPart.ListAddedDocuments.Find(document => document.OperationID == operationId && document.DocumentType == documentType && document.Name == copyName);
                            if (temp != null)
                            {
                                fileName = temp.OriginalPath;
                            }
                            attachmentItems.Add(InnerCreateItemInfo((globalId++).ToString(), copyName, CommonConstants.Command_Delete_Attachment_Name, deleteAttachmentUISiteName, string.Empty, string.Empty, ContextMenuItemType.MenuItem, fileName));

                        });
                    }

                }

            }
            //返回结果集合的判断
            return businessItems == null ? attachmentItems : businessItems.Concat(attachmentItems).ToList();
        }

        /// <summary>
        /// 构造带有子菜单项的右键菜单
        /// </summary>
        /// <param name="contextMenuItem">右键菜单集合</param>
        /// <param name="row">数据行</param>
        /// <returns></returns>
        private IEnumerable<ContextMenuItemInfo> GetContextMenuItems(IEnumerable<ContextMenuItemInfo> contextMenuItem,DataRow row)
        {
            //附件相关菜单项
            List<ContextMenuItemInfo> attachmentItems = new List<ContextMenuItemInfo>();

            foreach (var info in contextMenuItem)
            {
                if (row.Table.Columns.Contains("IsValid"))
                {
                    string oldname = LocalData.IsEnglish ? "Cancel" : "取消";
                    string newname=LocalData.IsEnglish ? "Restore" : "恢复";
                    if (bool.Parse(row["IsValid"].ToString()) == false)
                    {
                        if (info.Text == oldname)
                        {
                            info.Text = newname;
                        }
                    }
                    else if (bool.Parse(row["IsValid"].ToString()))
                    {
                        if (info.Text == newname)
                        {
                            info.Text = oldname;
                        }
                    }
                }
               
                if (info.Type == ContextMenuItemType.MenuItem && info.Subitem == false)
                {
                    var menuItem = InnerCreateItemInfo((globalId++).ToString(), info.Text,
                                              info.Name, info.Site, string.Empty, string.Empty,
                                              info.Type, string.Empty);
                    menuItem.Index = MenuItemIndex++;
                    menuItem.BeginGroup = info.BeginGroup;
                    attachmentItems.Add(menuItem);
                }
                
                else if (info.Type == ContextMenuItemType.MenuItem && info.Subitem)
                {
                    var menuItem = InnerCreateItemInfo((globalId++).ToString(), info.Text,
                        info.Name, info.Site, string.Empty, string.Empty,
                        info.Type, string.Empty);
                    menuItem.Index = SubitemmenuItem++;
                    menuItem.BeginGroup = info.BeginGroup;
                    attachmentItems.Add(menuItem);
                }
                else if (info.Type == ContextMenuItemType.SubMenuItem)
                {
                    var subMenuItem = InnerCreateItemInfo((globalId++).ToString(), info.Text,
                        info.Name, info.Site, info.RegisterSite, info.ImageName,
                        info.Type, string.Empty);
                    subMenuItem.Index = MenuItemIndex++;
                    subMenuItem.BeginGroup = info.BeginGroup;
                    attachmentItems.Add(subMenuItem);
                }
            }
            return attachmentItems.OrderByDescending(n => n.Index).ToList();
        }
        /// <summary>
        /// 构造菜单项
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text">显示信息</param>
        /// <param name="name">控件的Name</param>
        /// <param name="site">注册点</param>
        /// <param name="registerSite">父级节点名称</param>
        /// <param name="imageName">图片名称</param>
        /// <param name="type">控件类型</param>
        /// <param name="businessNo">关联的业务号(可以是SO,MBL NO,HBL No等)</param>
        /// <returns></returns>
        public ContextMenuItemInfo InnerCreateItemInfo(string id, string text, string name, string site, string registerSite, string imageName, ContextMenuItemType type, string businessNo)
        {
            ContextMenuItemInfo itemInfo = new ContextMenuItemInfo();
            itemInfo.Id = id;
            itemInfo.Name = name;
            itemInfo.ImageName = imageName;
            itemInfo.RegisterSite = registerSite;
            itemInfo.Site = site;
            itemInfo.Text = text;
            itemInfo.Type = type;
            itemInfo.BusinessNo = businessNo;
            return itemInfo;
        }

        private void UnregisterDeleteAttachmentExtensionSite()
        {
            //移除删除附件UI拓展点
            if (RootWorkItem.UIExtensionSites.Contains(deleteAttachmentUISiteName))
            {
                UIExtensionSite site = RootWorkItem.UIExtensionSites[deleteAttachmentUISiteName];
                site.Clear();
                RootWorkItem.UIExtensionSites.UnregisterSite(deleteAttachmentUISiteName);
            }
        }

        #region Comment Code
        //protected override void CreateItems()
        //{
        //    this.Items.Clear();

        //}
        protected override void OnMenuItemClick(object sender, EventArgs e)
        {
            base.OnMenuItemClick(sender, e);
        } 
        #endregion
    }

}
