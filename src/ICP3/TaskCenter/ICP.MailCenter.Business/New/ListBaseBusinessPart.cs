using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.Utility;
using DevExpress.XtraGrid.Columns;
using System.IO;
using ICP.MailCenter.CommonUI;
using Microsoft.Practices.CompositeUI.Commands;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraBars;
using DevExpress.XtraGrid;
using System.Reflection;
using System.Threading;
using ICP.FCM.Common.ServiceInterface.Interfaces;
namespace ICP.Common.Business.ServiceInterface
{
    /// <summary>
    /// 带数据列表业务面板基类
    /// </summary>
    public partial class ListBaseBusinessPart : BaseBusinessPart_New, IListBaseBusinessPart
    {
        #region 字段
        /// <summary>
        /// 取消按钮需要包括的关键字
        /// </summary>
        public string CancelName = "cancel";
        /// <summary>
        /// 恢复按钮需要包括的关键字
        /// </summary>
        public string ResumeName = "resume";
        /// <summary>
        /// 保存按钮需要包括的关键字
        /// </summary>
        public string SaveName = "submit";
        /// <summary>
        /// 编辑按钮需要包括的关键字
        /// </summary>
        public string EditName = "edit";
        /// <summary>
        /// 复制按钮需要包括的关键字
        /// </summary>
        public string CopyName = "copy";
        /// <summary>
        /// 上下文菜单拓展点名称后缀
        /// </summary>
        private string contextMenuStripUISiteName = "_MenuUISite";
        /// <summary>
        /// 当前列表的用户自定义信息
        /// </summary>
        private UserCustomGridInfo currentUserCustomGridInfo;
        /// <summary>
        /// 用户是否改变列设置，包含列宽度，列位置
        /// </summary>
        private bool isColumnSettingChanged = false;
        private const string associatedColumn = "IsAssociated";
        /// <summary>
        /// 是否有效
        /// </summary>
        private string isValid = "IsValid";
        /// <summary>
        /// 未知业务面板搜索关键字时，如果搜索过就会产生列，之后搜索就不需要在产生列了
        /// </summary>
        private bool IsGeneralColumns = false;
        /// <summary>
        /// 标识是否清空Grid的列和数据源
        /// </summary>
        private bool IsClearColumnsAndDataSource = false;

        /// <summary>
        /// 未知业务面板模版代码
        /// </summary>
        public string MailLink4Unknown
        {
            get { return ICP.Common.ServiceInterface.DataObjects.ListFormType.MailLink4Unknown.ToString(); }
        }
        /// <summary>
        /// 当前点击上下文菜单项所包含的自定义信息
        /// </summary>
        public object CurrentContextMenuItemTag
        {
            get;
            set;
        }
        /// <summary>
        /// 当前选择列是否有效
        /// </summary>
        public bool? IsValid
        {
            get
            {
                return !FocusedDataRow.Table.Columns.Contains(isValid) ?
                    null : (bool?)FocusedDataRow["IsValid"];
            }
            set { FocusedDataRow["IsValid"] = value; }
        }
        /// <summary>
        /// 选中的数据行
        /// </summary>
        public DataRow FocusedDataRow
        {
            get { return this.gridViewList.GetFocusedDataRow(); }
        }

        private int visibleIndex = 0;


        /// <summary>
        /// 支持拖入操作的列列表
        /// </summary>
        private List<BusinessColumnInfo> listDropableColumns = new List<BusinessColumnInfo>();
        /// <summary>
        /// 可编辑列列表
        /// </summary>
        private List<BusinessColumnInfo> listEditableColumns = new List<BusinessColumnInfo>();
        /// <summary>
        /// 维护业务和上传文档的列表字典
        /// </summary>
        private ListDictionary<Guid, string> listDicAddedDocuments = new ListDictionary<Guid, string>();
        /// <summary>
        /// 新上传的文档实体列表
        /// </summary>
        private List<DocumentInfo> listAddedDocuments = new List<DocumentInfo>();
        /// <summary>
        /// 新上传的文档实体列表
        /// </summary>
        public List<DocumentInfo> ListAddedDocuments
        {
            get
            {
                return this.listAddedDocuments;
            }
            set
            {
                this.listAddedDocuments = value;
            }
        }
        /// <summary>
        /// 维护业务和删除文档的列表字典
        /// </summary>
        private ListDictionary<Guid, string> listDicDeletedDocuments = new ListDictionary<Guid, string>();
        /// <summary>
        /// 维护行号和值发生更改列的列表字典
        /// </summary>
        private ListDictionary<int, string> listDicEditedCells = new ListDictionary<int, string>();


        /// <summary>
        /// 当前列表控件中的列信息
        /// </summary>
        private List<BusinessColumnInfo> listColumnInfos;
        /// <summary>
        /// 功能自定义添加列集合信息
        /// </summary>
        private List<BusinessColumnInfo> listCustomColumnInfos;

        private DocumentType DocumentType
        {
            get
            {
                if (TemplateCode.Equals(ICP.Common.ServiceInterface.DataObjects.ListFormType.MailLink4CarrierSO.ToString()))
                    return DocumentType.SO;
                if (TemplateCode.Equals(ICP.Common.ServiceInterface.DataObjects.ListFormType.MailLink4CarrierMBL.ToString()))
                    return DocumentType.MBL;
                if (TemplateCode.Equals(ICP.Common.ServiceInterface.DataObjects.ListFormType.MailLink4CarrierAP.ToString()))
                    return DocumentType.AP;
                return DocumentType.Other;
            }
        }
        #endregion


        /// <summary>
        /// 构造函数
        /// </summary>
        public ListBaseBusinessPart()
        {
            InitializeComponent();
            this.toolTipController1.CreateShowArgs();
            this.Load += (sender, e) =>
            {
                if (!LocalData.IsDesignMode)
                {
                    DocumentNotifyClientService.DocumentUploadFailed += OnDocumentUploadFailed;
                    DocumentNotifyClientService.DocumentUploadSucessed += OnDocumentUploadSucessed;
                    localCommands = new List<string> { CommonConstants.Command_Add_Attachment_Name, CommonConstants.Command_Delete_Attachment_Name, "Command_BusinessPart_DataSubmit" };
                    ServiceClient.GetClientService<IMainForm>().ApplicationExit += OnApplicationExit;
                }

            };
            this.Disposed += delegate
            {
                if (this.DocumentNotifyClientService != null)
                {
                    DocumentNotifyClientService.DocumentUploadFailed -= OnDocumentUploadFailed;
                    DocumentNotifyClientService.DocumentUploadSucessed -= OnDocumentUploadSucessed;
                    ServiceClient.GetClientService<IMainForm>().ApplicationExit -= OnApplicationExit;
                    this.FocusedRowChangedHandler = null;
                    this.gridControlList.DataSource = null;
                    if (this.bindingSource != null)
                    {
                        this.bindingSource.DataSource = null;
                        this.bindingSource = null;
                    }
                    this.localCommands = null;
                    RootWorkItem.Items.Remove(this);


                }

            };
        }


        private List<string> localCommands;
        public List<string> LocalCommands
        {
            get
            {
                return this.localCommands;
            }
            set
            {
                this.localCommands = value;
            }
        }
        #region 公共属性及服务

        private IBusinessOperationHandleService _BusinessOperationHandleService;
        public IBusinessOperationHandleService BusinessOperationHandleService
        {
            get
            {
                if (_BusinessOperationHandleService == null)
                    _BusinessOperationHandleService = ServiceClient.GetClientService<IBusinessOperationHandleService>();
                return _BusinessOperationHandleService;
            }
        }

        private UCSelectedContactListPart _contactListPart;
        public UCSelectedContactListPart contactListPart
        {
            get
            {
                if (_contactListPart == null)
                    _contactListPart = RootWorkItem.Items.AddNew<UCSelectedContactListPart>();
                return _contactListPart;
            }
        }

        private IBusinessQueryService _businessQueryService;
        public IBusinessQueryService BusinessQueryService
        {
            get
            {
                if (_businessQueryService == null)
                    _businessQueryService = ServiceClient.GetService<IBusinessQueryService>();
                return _businessQueryService;
            }
        }

        public IICPCommonOperationService CommonOperationService
        {
            get { return ServiceClient.GetService<IICPCommonOperationService>(); }
        }

        /// <summary>
        /// 列表当前选择行改变事件
        /// </summary>
        public event FocusedRowChangedEventHandler FocusedRowChangedHandler;
        /// <summary>
        /// 当前选中的列表数据行索引
        /// </summary>
        public int CurrentRowHandle
        {
            get
            {
                return this.gridViewList.FocusedRowHandle;
            }
        }
        /// <summary>
        /// 列表当前数据行
        /// </summary>
        public DataRow CurrentRow
        {
            get
            {
                int rowIndex = this.gridViewList.GetFocusedDataSourceRowIndex();
                if (rowIndex < 0)
                    return null;
                return this.CurrentDataSource.Rows[rowIndex];
            }
        }
        /// <summary>
        /// 列表当前绑定的数据源
        /// </summary>
        public DataTable CurrentDataSource
        {
            get
            {
                return this.bindingSource.DataSource as DataTable;
            }
            set { this.bindingSource.DataSource = value; }
        }


        public DocumentNotifyClientService DocumentNotifyClientService
        {
            get
            {
                return ClientHelper.Get<DocumentNotifyClientService, DocumentNotifyClientService>();
            }

        }
        /// <summary>
        /// 列表行字体
        /// </summary>
        public Font RowFont
        {
            get
            {
                return this.gridViewList.Appearance.Row.Font;
            }
        }

        private IClientFileService _ClientFileService;
        public IClientFileService ClientFileService
        {
            get
            {
                if (_ClientFileService == null)
                    _ClientFileService = ServiceClient.GetService<IClientFileService>();
                return _ClientFileService;
            }
        }

        public BarManager barManager
        {
            get { return base.barManager; }
            set { base.barManager = value; }
        }

        /// <summary>
        /// 业务类型
        /// </summary>
        public BusinessType BusinessType
        {
            get { return (BusinessType)Enum.Parse(typeof(OperationType), OperationType.ToString()); }
        }

        /// <summary>
        /// 通过模板代码更改业务面板显示
        /// </summary>
        /// <param name="templateCode"></param>
        public void Switch(string templateCode)
        {
            UnRegisterExtensionSite();
            UpdateSubGridToolBarItem(templateCode);


            this.TemplateCode = templateCode;
            CreateColumns(this.TemplateCode);
            BindData(this.Parameter);
        }
        private void UpdateSubGridToolBarItem(string templateCode)
        {
            RemoveBusinessToobarItems(this.TemplateCode);
            RegisterExtensionSite();
            BuildToobar(templateCode);
        }
        public void RemoveBusinessToobarItems(string templateCode)
        {

            List<BarItem> items = new List<BarItem>();
            BarItems barItems = this.barManager.Items;
            foreach (BarItem item in barItems)
            {
                ProcessBarItem(items, item, templateCode);
            }
            if (items.Count <= 0)
                return;
            foreach (BarItem item in items)
            {

                this.barManager.Items.Remove(item);
            }


        }
        /// <summary>
        /// 根据名称清除样式
        /// </summary>
        /// <param name="styleName"></param>
        public void ClearStyle(string styleName)
        {
            if (this.gridViewList.FormatConditions[styleName] == null)
            {
                return;
            }
            else
            {
                StyleFormatCondition rowCondition = this.gridViewList.FormatConditions[styleName] as StyleFormatCondition;
                rowCondition.Expression = "1 !=1 ";
            }
        }

        /// <summary>
        /// 添加业务样式 如果表达式和先前添加的样式相同，则直接返回
        /// </summary>
        /// <param name="styleCondition"></param>
        public void AddBusinessStyleFormationCondition(StyleFormatCondition styleCondition)
        {

            if (this.gridViewList.FormatConditions[styleCondition.Tag] == null)
            {
                this.gridViewList.FormatConditions.Add(styleCondition);
            }
            else
            {
                StyleFormatCondition rowCondition = this.gridViewList.FormatConditions[styleCondition.Tag] as StyleFormatCondition;
                if (rowCondition.Expression == styleCondition.Expression)
                    return;
                rowCondition.Expression = styleCondition.Expression;
            }
        }

        private void gridViewList_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            DataRow row = gridViewList.GetDataRow(e.RowHandle) as DataRow;
            if (row == null) return;

            if (row.Table.Columns.Contains(isValid))
            {
                if (!row.Field<bool>(isValid))
                {
                    ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance,
                                                                                ICP.Framework.CommonLibrary.Helper.PresenceStyle.Disabled);
                }
            }
        }

        /// <summary>
        /// 通过包含名称查找工具栏并设置是否可用
        /// </summary>
        /// <param name="name">包含的名称</param>
        /// <param name="enable">是否可用</param>
        /// <returns></returns>
        public void SetBarItemEnable(string name, bool enable)
        {
            foreach (BarItem bi in barManager.Items)
            {
                if (bi.Name.ToLower().Contains(name) && enable == false)
                {
                    bi.Visibility = BarItemVisibility.Never;
                }
                else if (bi.Name.ToLower().Contains(name))
                {
                    bi.Visibility = BarItemVisibility.Always;
                }

            }
        }

        /// <summary>
        /// 设置列表获得焦点的行
        /// </summary>
        /// <param name="rowHandle"></param>
        public void SetFocusedRowHandle(int rowHandle)
        {
            this.gridViewList.FocusedRowHandle = rowHandle;
        }
        /// <summary>
        /// 设置数据源
        /// </summary>
        /// <param name="data"></param>
        public void SetDataSource(object data)
        {
            if (this.bindingSource.DataSource != null && this.bindingSource.DataSource is IDisposable)
            {
                (this.bindingSource.DataSource as IDisposable).Dispose();
            }
            this.bindingSource.DataSource = null;
            this.bindingSource.DataSource = data;
            ResetBinding();
        }

        public override void BindData(object parameter)
        {
            this.AdvanceQueryString = AppendAdvanceStringToSQL();
            //需要生成列
            RootWorkItem.State["NeedGererateColumns"] = false;
            this.IsGeneralColumns = false;
            this.IsClearColumnsAndDataSource = false;
            //区分是否要连接本地缓存数据库还是数据库,1表示连接数据库，0表示连接本地缓存数据库
            RootWorkItem.State["IsAdvanceSearch"] = false;

            base.BindData(parameter);
            //标识内部沟通面板是否要限制最小Size
            LimitInternalMailMinimize();
            UIHelper.TemplateCode = this.TemplateCode;
        }

        private void LimitInternalMailMinimize()
        {
            if (IsUnknownTemplateCode())
            {
                RootWorkItem.State["LimitInternalMailSize"] = 2;
                RootWorkItem.Commands["Command_InternalMailBusinessPartFixedSize"].Execute();
            }
            else
                RootWorkItem.State["LimitInternalMailSize"] = 0;
        }

        /// <summary>
        /// 根据查询数据的有无来区分是否要添加BarItems，还是Columns.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private bool HasUnknownBusinessPartColumnInfos = false;
        public void SpeciallyInitMailLink4Unknown(int count)
        {
            if (SetSplitPanelVisibility(count))
            {
                bool needGeneratedColumns = UIHelper.IsNeedGenerateColumns(RootWorkItem);
                if (needGeneratedColumns || count > 0)
                {
                    if (!this.IsGeneralColumns) //标识需要去生成列
                    {
                        //用户切换下一邮件封也是Unknown业务面板的Case
                        if (listColumnInfos == null || !HasUnknownBusinessPartColumnInfos)
                        {
                            this.CreateColumns(
                                ICP.Common.ServiceInterface.DataObjects.ListFormType.MailLink4Customer.ToString());
                            HasUnknownBusinessPartColumnInfos = true;
                        }
                    }
                    this.IsGeneralColumns = true;
                }
                else
                {
                    VisibleBarItemAndClearColumns();
                    HasUnknownBusinessPartColumnInfos = false;
                }
            }
            else
                HasUnknownBusinessPartColumnInfos = false;
        }

        public void InitUnknownBusinessPart(int count)
        {
            SetSplitPanelVisibility(count);
            VisibleBarItemAndClearColumns();
        }

        private void VisibleBarItemAndClearColumns()
        {
            //SetBarItemVisibility(BarItemVisibility.Always);
            ClearGridControl();
            listColumnInfos = null;
        }

        public void InitControl(int count)
        {
            if (count == 0) return;

            splitBusinessPart.Panel2.Controls.Clear();
            contactListPart.Dock = DockStyle.Fill;
            splitBusinessPart.Panel2.Controls.Add(contactListPart);
            contactListPart.BringToFront();

            WaitCallback callback = (obj) => contactListPart.BeginInvoke((System.Action)(SetContactListDataSource));
            ThreadPool.QueueUserWorkItem(callback);
        }

        private void SetContactListDataSource()
        {
            try
            {
                contactListPart.SetDataSource(OperationID, OperationType, true);
            }
            catch (System.Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
            }
        }


        //private void SetBarItemVisibility(BarItemVisibility visibility)
        //{
        //    BarItems items = this.barManager.Items;
        //    ToolbarManager.VisibleBarItem(items, "txtQuery", visibility);
        //    ToolbarManager.VisibleBarItem(items, "btnFind", visibility);
        //    items = null;
        //}

        /// <summary>
        /// 根据业务号和参考号来查询业务数据
        /// </summary>
        /// <returns></returns>
        private string AppendAdvanceStringToSQL()
        {
            StringBuilder strSQL = new StringBuilder();
            var nos = RootWorkItem.State["MatchingSubjectKeyWord"] as List<string>;
            if (nos != null && nos.Count > 0)
            {
                for (int i = 0; i < nos.Count; i++)
                {
                    strSQL.Append(SQLCondition(nos[i]));
                }
            }
            return strSQL.ToString();
        }

        private string SQLCondition(string query)
        {
            return string.Format(" AND No LIKE '%{0}%' OR RefNO LIKE '%{0}%'", query);
        }

        private void ProcessBarItem(List<BarItem> items, BarItem item, string templateCode)
        {
            OperationToolbarCommand command = item.Tag == null ? null : (OperationToolbarCommand)item.Tag;
            if (command != null && command.Tag == templateCode)
            {
                if (!string.IsNullOrEmpty(command.Name))
                {
                    this.RootWorkItem.Commands[command.Name].RemoveInvoker(item, "ItemClick");
                    this.RootWorkItem.Commands[command.Name].Dispose();
                }
                if (!string.IsNullOrEmpty(command.RegisterSite))
                {
                    this.RootWorkItem.UIExtensionSites.UnregisterSite(command.RegisterSite);
                }
                items.Add(item);

            }


        }
        #endregion


        #region 重写基类方法
        private string demoOperationNo;


        public override DateTime? Updatetime
        {
            get
            {
                if (FocusedDataRow == null)
                    return null;
                return FocusedDataRow["UpdateDate"] as DateTime?;
            }
            set
            {
                base.Updatetime = value;
            }
        }


        public override string OperationNo
        {
            get
            {
                if (FocusedDataRow["No"] == null)
                    return string.Empty;
                return FocusedDataRow["NO"] as string;
            }
            set
            {
                base.OperationNo = value;
            }
        }



        public override Guid OperationID
        {
            get
            {
                if (FocusedDataRow == null)
                    return Guid.Empty;
                return new Guid(FocusedDataRow["ID"].ToString());
            }
            set
            {
                base.OperationID = value;
            }
        }
        public override void UnRegisterExtensionSite()
        {
            base.UnRegisterExtensionSite();
            string businessContextMenuStripUISiteName = GetBusinessContextMenuStripUISiteName();
            if (RootWorkItem.UIExtensionSites.Contains(businessContextMenuStripUISiteName))
            {
                RootWorkItem.UIExtensionSites.UnregisterSite(businessContextMenuStripUISiteName);
            }
        }
        public override void RegisterExtensionSite()
        {
            base.RegisterExtensionSite();
            string businessContextMenuStripUISiteName = GetBusinessContextMenuStripUISiteName();
            if (!RootWorkItem.UIExtensionSites.Contains(businessContextMenuStripUISiteName))
            {
                RootWorkItem.UIExtensionSites.RegisterSite(businessContextMenuStripUISiteName, this.contextMenuStrip);
            }
        }
        public string GetBusinessContextMenuStripUISiteName()
        {
            return GetPresentTemplateCode() + this.contextMenuStripUISiteName;
        }

        public override void AfterDataSourceChanged()
        {
            ResetDocumentRecord();
        }
        #endregion

        #region IMessageBaseBusinessPart 成员

        public void SaveCustomColumnInfo()
        {
            List<CustomColumnInfo> columnInfos = GetGridColumnInfos();

            if (columnInfos.Count <= 0)
                return;

            UserCustomGridInfo gridInfo = this.currentUserCustomGridInfo;
            if (gridInfo == null)
            {
                gridInfo = new UserCustomGridInfo();
                gridInfo.TemplateCode = TemplateCode;
                gridInfo.Id = Guid.NewGuid();

            }
            if (gridInfo.UserId == null)
            {
                gridInfo.UserId = LocalData.UserInfo.LoginID;
            }

            gridInfo.Columns = columnInfos;
            try
            {
                isColumnSettingChanged = false;
                ServiceClient.GetService<IClientCustomDataGridService>().Save(gridInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<CustomColumnInfo> GetGridColumnInfos()
        {
            int columnCount = this.gridViewList.Columns.Count;
            CustomColumnInfo[] list = new CustomColumnInfo[columnCount];
            ///如果有只有代码新增的排序列，则直接返回
            if (columnCount == 1)
            {
                return list.ToList();
            }
            for (int i = 0; i < columnCount; i++)
            {
                DevExpress.XtraGrid.Columns.GridColumn column = this.gridViewList.Columns[i];
                list[i] = new CustomColumnInfo();
                list[i].Name = column.Name;
                list[i].Visible = column.Visible;
                list[i].VisibleIndex = column.VisibleIndex + 1;
                list[i].Width = column.Width;
                list[i].Fixed = (ColumnFixedStyle)Enum.Parse(typeof(ColumnFixedStyle), column.Fixed.ToString());
                list[i].AbsoluteIndex = column.AbsoluteIndex;
                list[i].SortOrder = (ICP.DataCache.ServiceInterface.SortOrder)((int)column.SortOrder);
            }
            Array.Sort<CustomColumnInfo>(list, new CustomColumnInfoComparer());
            return list.ToList();
        }

        public UserCustomGridInfo GetUserCustomGridInfo()
        {
            try
            {
                UserCustomGridInfo customInfo = ServiceClient.GetService<IClientCustomDataGridService>().Get(TemplateCode);
                return customInfo;
            }
            catch (Exception)
            {

            }
            return null;
        }

        public List<BusinessColumnInfo> GetColumnInfos(string templateCode)
        {
            return ColumnTemplateLoader.Current[templateCode];
        }


        #endregion

        #region 辅助方法
        /// <summary>
        /// 行宽度改变时设置行显示信息改变标识
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            isColumnSettingChanged = true;
        }
        /// <summary>
        /// 行位置改变时设置行显示信息改变标识
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnColumnPositionChanged(object sender, EventArgs e)
        {
            isColumnSettingChanged = true;
        }
        /// <summary>
        /// 移除列表列显示事件处理
        /// </summary>
        private void RemoveEventHandler()
        {
            this.gridViewList.ColumnPositionChanged -= OnColumnPositionChanged;
            this.gridViewList.ColumnWidthChanged -= OnColumnWidthChanged;
        }
        /// <summary>
        /// 添加列表列显示事件处理
        /// </summary>
        private void AddEventHandler()
        {
            this.gridViewList.ColumnPositionChanged += OnColumnPositionChanged;
            this.gridViewList.ColumnWidthChanged += OnColumnWidthChanged;
        }

        private CustomColumnInfo _DemoColumnInfo;
        public CustomColumnInfo DemoColumnInfo
        {
            get
            {
                if (_DemoColumnInfo == null)
                    _DemoColumnInfo = new CustomColumnInfo { VisibleIndex = visibleIndex++, Fixed = ColumnFixedStyle.None, Visible = true, Width = 100 };
                return _DemoColumnInfo;
            }
        }

        /// <summary>
        /// 根据XML配置和用户自定义列信息 构造列表
        /// </summary>
        /// <param name="columnInfos"></param>
        private List<BusinessColumnInfo> columnInfos;
        private void CreateColumns(string templateCode)
        {
            columnInfos = GetColumnInfos(templateCode);
            listColumnInfos = null;
            listColumnInfos = columnInfos;
            if (columnInfos == null || columnInfos.Count <= 0)
                return;
            GetColumnsByProperties();
            this.SuspendLayout();
            RemoveEventHandler();
            ClearGridControl();
            gridViewList.Tag = templateCode;
            this.currentUserCustomGridInfo = GetUserCustomGridInfo();

            bool isUserCustomGridInfoNull = IsUserCustomGridInfoNull();
            CustomColumnInfo columnCustomInfo = isUserCustomGridInfoNull ? GetDemoCustomColumnInfo() : null;
            foreach (BusinessColumnInfo columnInfo in columnInfos)
            {
                InnerCreateColumn(columnInfo, isUserCustomGridInfoNull ? columnCustomInfo : this.currentUserCustomGridInfo.Columns.Find(c => c.Name == columnInfo.Name));
            }
            listCustomColumnInfos = null;
            //如果集合中包含Isassociated列，就不需要再添加了
            listCustomColumnInfos = listColumnInfos.FindAll(o => o.FieldName.Equals(associatedColumn));
            if (listCustomColumnInfos == null || listCustomColumnInfos.Count == 0)
            {
                listCustomColumnInfos = GetCustomColumns();
                listCustomColumnInfos = listCustomColumnInfos ?? new List<BusinessColumnInfo>();
                listColumnInfos.AddRange(listCustomColumnInfos);
                AddCustomColumns(listCustomColumnInfos);
            }

            AddSortColumnInfos();
            AddEventHandler();
            this.ResumeLayout();
        }

        private CustomColumnInfo GetDemoCustomColumnInfo()
        {
            int absoluteIndex = 0;
            CustomColumnInfo demoColumnInfo = DemoColumnInfo;
            demoColumnInfo.AbsoluteIndex = absoluteIndex++;

            return demoColumnInfo;
        }

        private void GetColumnsByProperties()
        {
            int count = columnInfos.Count;
            for (int i = 0; i < count; i++)
            {
                BusinessColumnInfo item = columnInfos[i];
                if (item.Dropable)
                    listDropableColumns.Add(item);
                if (item.Editable && !item.ReadOnly)
                    listEditableColumns.Add(item);

                item = null;
            }
        }

        private bool IsUserCustomGridInfoNull()
        {
            return this.currentUserCustomGridInfo == null;
        }

        private void ClearGridControl()
        {
            this.gridViewList.Columns.Clear();
            this.gridControlList.RepositoryItems.Clear();
            this.CurrentDataSource = null;
            IsClearColumnsAndDataSource = true;
        }

        public override void Init(object parameter)
        {
            base.Init(parameter);
            if (SetSplitPanelVisibility(0))
            {
                return;
            }
            CreateColumns(TemplateCode);
            HookEvent();
        }
        private bool SetSplitPanelVisibility(int count)
        {
            bool isUnknownTemplateCode = false;
            isUnknownTemplateCode = IsUnknownTemplateCode();
            if (isUnknownTemplateCode && count > 0)
            {
                splitBusinessPart.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
            }
            else
                splitBusinessPart.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
            return isUnknownTemplateCode;
        }

        private bool IsUnknownTemplateCode()
        {
            return this.TemplateCode.Equals(MailLink4Unknown);
        }

        private void HookEvent()
        {
            BusinessOperationHandleService.BusinessOperationCompleted += new EventHandler<CommonEventArgs<BusinessOperationParameter>>(OnBusinessOperationCompleted);
        }
        #region 面板回调数据刷新
        /// <summary>
        /// 邮件中心调用FCM业务服务后，回调刷新业务面板数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnBusinessOperationCompleted(object sender, CommonEventArgs<BusinessOperationParameter> e)
        {
            BusinessOperationParameter businessParameter = e.Data;
            Guid operationId = businessParameter.Context.OperationID;
            string operationNo = businessParameter.Context.OpeationNO;
            OperationType operationType = businessParameter.Context.OperationType;

            WaitCallback callback = (obj) => this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
            {
                try
                {
                    this.AdvanceQueryString = "$@NO@ ='" + operationNo + "'";
                    BusinessQueryCriteria criteria = GetQueryCriteria(true);
                    IBusinessQueryServiceGetter getter = BusinessInfoExtractorFactory.GetQueryService(this.Parameter);
                    object result = getter.Query(criteria, this.Parameter);
                    DataTable dt = GetInnerTable(result);
                    if (dt != null)
                    {
                        AddCustomColumn(dt);
                    }
                    RefreshData(dt);

                    PostHande(result);
                }
                catch (System.Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
                }
            });
            ThreadPool.QueueUserWorkItem(callback);
        }
        /// <summary>
        /// 回调时刷新数据
        /// </summary>
        /// <param name="data"></param>
        public void RefreshData(object data)
        {

            DataTable result = data as DataTable;
            if (result == null || result.Rows.Count <= 0)
                return;
            DataRow row = result.Rows[0];
            Guid operationId = row.Field<Guid>("ID");
            DataTable dt = CurrentDataSource;
            DataRow[] temp = dt.Select("ID='" + operationId + "'");
            RemoveCellValueChangeHandler();
            //如果当前列表数据源中不含有指定业务Id的业务，则代表新增
            if (temp == null || temp.Length <= 0)
            {
                result.Rows.Remove(row);
                CurrentDataSource.ImportRow(row);
            }
            //更新
            else
            {
                DataRow targetRow = temp[0];
                targetRow.ItemArray = row.ItemArray;
            }
            ResetBinding();
            AddCellValueChangeHandler();

        }
        public void RemoveCellValueChangeHandler()
        {
            this.gridViewList.CellValueChanged -= this.gridViewList_CellValueChanged;
        }
        public void AddCellValueChangeHandler()
        {
            this.gridViewList.CellValueChanged += this.gridViewList_CellValueChanged;
        }
        #endregion
        public override void AddCustomColumn(DataTable dt)
        {
            if (listCustomColumnInfos != null && listCustomColumnInfos.Count > 0)
            {
                foreach (BusinessColumnInfo columnInfo in listCustomColumnInfos)
                {
                    DataColumn column = new DataColumn(columnInfo.FieldName);
                    bool isStringColumn = IsStringColumn(columnInfo);
                    column.DataType = isStringColumn ? typeof(string) : typeof(int);
                    if (isStringColumn)
                    {
                        column.DefaultValue = string.Empty;
                    }
                    else
                    {
                        column.DefaultValue = 0;
                    }

                    dt.Columns.Add(column);
                }
            }
        }
        /// <summary>
        /// 判断列对应的Field是否为字符串类型
        /// </summary>
        /// <param name="columnInfo"></param>
        /// <returns></returns>
        private bool IsStringColumn(BusinessColumnInfo columnInfo)
        {
            return (columnInfo.EditType == ColumnEditType.Text || columnInfo.EditType == ColumnEditType.Memo);
        }

        /// <summary>
        /// 按照业务号降序排列
        /// </summary>
        private void AddSortColumnInfos()
        {
            List<BusinessColumnInfo> sortColumns = listCustomColumnInfos.FindAll(column => column.Name.ToLower().EndsWith("_sort"));
            if (sortColumns != null && sortColumns.Count > 0)
            {
                foreach (BusinessColumnInfo columnInfo in sortColumns)
                {
                    GridColumn column = this.gridViewList.Columns.ColumnByFieldName(columnInfo.FieldName);
                    GridColumnSortInfo sortInfo = new GridColumnSortInfo(column, DevExpress.Data.ColumnSortOrder.Descending);
                    this.gridViewList.SortInfo.Add(sortInfo);
                }
            }

            GridColumn columnNo = this.gridViewList.Columns.ColumnByFieldName("NO");
            GridColumnSortInfo sortInfoNo = new GridColumnSortInfo(columnNo, DevExpress.Data.ColumnSortOrder.Descending);
            this.gridViewList.SortInfo.Add(sortInfoNo);

        }

        private void AddCustomColumns(List<BusinessColumnInfo> customColumns)
        {
            if (customColumns == null || customColumns.Count <= 0)
                return;
            foreach (BusinessColumnInfo columnInfo in customColumns)
            {
                DevExpress.XtraGrid.Columns.GridColumn gridColumn = this.gridViewList.Columns.Add();

                gridColumn.Caption = columnInfo.Caption;
                gridColumn.FieldName = columnInfo.FieldName;
                gridColumn.Name = columnInfo.Name;
                DevExpress.XtraEditors.Repository.RepositoryItem columnEdit = ColumnEditFactory.GetColumnEdit(columnInfo);
                columnEdit.ReadOnly = columnInfo.ReadOnly;
                this.gridControlList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { columnEdit });
                gridColumn.Width = 0;
                gridColumn.Visible = false;
                gridColumn.VisibleIndex = -1;
                gridColumn.ColumnEdit = columnEdit;
            }

        }
        private void InnerCreateColumn(BusinessColumnInfo columnInfo, CustomColumnInfo columnCustomInfo)
        {
            var gridColumn = this.gridViewList.Columns.Add();

            gridColumn.OptionsColumn.AllowEdit = columnInfo.Editable;
            if (columnCustomInfo != null)
            {
                //mark:当设置列不可见时，就不需要设置VisibleIndex，否则就会显示出来
                if (columnInfo.FieldName.Equals(associatedColumn))
                    gridColumn.Visible = false;
                else
                {
                    gridColumn.Visible = columnCustomInfo.Visible;
                    gridColumn.VisibleIndex = columnCustomInfo.VisibleIndex;
                }

                gridColumn.SortOrder = (DevExpress.Data.ColumnSortOrder)Enum.Parse(typeof(DevExpress.Data.ColumnSortOrder), columnCustomInfo.SortOrder.ToString());

                gridColumn.Fixed = (DevExpress.XtraGrid.Columns.FixedStyle)Enum.Parse(typeof(DevExpress.XtraGrid.Columns.FixedStyle), columnCustomInfo.Fixed.ToString());
                gridColumn.Width = columnCustomInfo.Width;
            }
            else
            {
                gridColumn.Visible = true;
                gridColumn.Width = 100;

            }

            gridColumn.Caption = columnInfo.Caption;
            gridColumn.FieldName = columnInfo.FieldName;
            gridColumn.Name = columnInfo.Name;
            DevExpress.XtraEditors.Repository.RepositoryItem columnEdit = ColumnEditFactory.GetColumnEdit(columnInfo);
            columnEdit.ReadOnly = columnInfo.ReadOnly;

            this.gridControlList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { columnEdit });
            gridColumn.ColumnEdit = columnEdit;

            if (columnCustomInfo != null)
            {
                if (!columnCustomInfo.Visible)
                {
                    this.gridViewList.Columns.ColumnByFieldName(columnInfo.FieldName).Visible = false;
                }
                gridColumn.AbsoluteIndex = columnCustomInfo.AbsoluteIndex;
            }


        }
        /// <summary>
        /// 添加用户用于特殊目的的自定义列,比如排序等
        /// </summary>
        /// <returns></returns>
        public List<BusinessColumnInfo> GetCustomColumns()
        {
            ICustomColumnGetter getter = BusinessInfoExtractorFactory.GetCustomColumnGetter(this.Parameter);
            return getter.Get();
        }

        /// <summary>
        /// 重新绑定数据源
        /// </summary>
        public void ResetBinding()
        {
            this.bindingSource.ResetBindings(false);

        }
        /// <summary>
        /// 取消编辑
        /// </summary>
        public void CancelEdit()
        {
            (this.bindingSource.DataSource as DataTable).RejectChanges();
            this.bindingSource.CancelEdit();
            this.bindingSource.ResetBindings(false);
        }
        /// <summary>
        /// 接受更改
        /// </summary>
        public void AcceptChanges()
        {
            (this.bindingSource.DataSource as DataTable).AcceptChanges();
            this.bindingSource.EndEdit();
            this.bindingSource.ResetBindings(false);
        }
        #endregion

        #region 处理拖放操作
        /// <summary>
        /// 设置拖放效果为复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gridControlList_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        void gridControlList_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            Point gvLocation = PointToScreen(this.gridControlList.Location);
            int x = e.X - gvLocation.X;
            int y = e.Y - gvLocation.Y;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi = this.gridViewList.CalcHitInfo(new Point(x, y));
            int iMouseRowHandle = hi.RowHandle;
            string fieldName = hi.Column.FieldName;
            //如果拖动到的列不是可拖入列，则直接返回
            if (!ValidateTargetFieldDropable(fieldName))
                return;


            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (files == null || files.Length <= 0)
                return;
            ///只有符合类型的文档才允许拖入业务列表
            string fileExtensions = CommonUIUtility.GetFileExtensions();

            List<string> listTareget = files.Where(file => fileExtensions.Contains(Path.GetExtension(file))).ToList();

            HandleDragDrop(iMouseRowHandle, listTareget, fieldName);
        }
        /// <summary>
        /// 验证拖动到的目标列是否可拖入
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        private bool ValidateTargetFieldDropable(string fieldName)
        {
            return listDropableColumns.Exists(columnInfo => columnInfo.FieldName == fieldName);

        }
        /// <summary>
        /// 通过行索引查找数据行
        /// </summary>
        /// <param name="rowHandle"></param>
        /// <returns></returns>
        public DataRow GetDataRowByRowHandle(int rowHandle)
        {
            if (rowHandle < 0)
                return null;
            DataRow row = this.gridViewList.GetDataRow(rowHandle);
            return row;
        }
        private DataTable GetDataSource()
        {
            return this.bindingSource.DataSource as DataTable;
        }
        /// <summary>
        ///一票业务下不允许存在多个重名文档
        /// </summary>
        /// <param name="temps"></param>
        /// <param name="existsCopyName"></param>
        /// <returns></returns>
        private bool ValidateDuplicateCopyExists(List<string> temps, DataRow row)
        {
            if (temps == null || temps.Count <= 0 || row == null)
                return false;
            List<string> listExists = new List<string>();
            foreach (BusinessColumnInfo column in this.listDropableColumns)
            {

                string fileNames = row.Field<string>(column.FieldName);
                if (string.IsNullOrEmpty(fileNames))
                    continue;
                List<string> temp2 = fileNames.Split(CommonConstants.NormalSeparator.ToCharArray()).Where(name => !string.IsNullOrEmpty(name)).ToList();
                listExists.AddRange(temp2);

            }

            List<string> listCommon = temps.Intersect(listExists).ToList();
            if (listCommon != null && listCommon.Count > 0)
            {
                string tip = string.Format(LocalData.IsEnglish ? "File(s):{0} already exists,Do you want to overrite?" : "文档:{0}已经存在,是否覆盖?", listCommon.Aggregate((a, b) => a + "," + b));
                DialogResult result = MessageBoxService.ShowQuestion(tip);
                if (result == DialogResult.OK)
                {
                    DeleteDocument(listCommon);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;

        }
        public IMessageBoxService MessageBoxService
        {
            get
            {
                return ServiceClient.GetClientService<IMessageBoxService>();
            }
        }
        /// <summary>
        /// 提供给具体业务面板处理拖放操作
        /// </summary>
        /// <param name="rowHandle"></param>
        /// <param name="filePaths"></param>
        public virtual void HandleDragDrop(int rowHandle, List<string> filePaths, string fieldName)
        {
            if (rowHandle < 0)
                return;
            string normalSeparator = CommonConstants.NormalSeparator;
            List<string> listFileName = (from file in filePaths
                                         select Path.GetFileName(file)).ToList();
            string fileNames = listFileName.Aggregate((a, b) => a + normalSeparator + b);
            string copyPaths = filePaths.Aggregate((a, b) => a + normalSeparator + b);
            DataRow row = GetDataRowByRowHandle(rowHandle);

            string existsCopyName = row.Field<string>(fieldName);

            if (ValidateDuplicateCopyExists(listFileName, row))
                return;
            DataTable dt = GetDataSource();
            Guid operationId = row.Field<Guid>("ID");

            List<String> listExistsCopyPath = this.listDicAddedDocuments.ContainsKey(operationId) ? this.listDicAddedDocuments[operationId] : new List<string>();



            row[fieldName] = string.IsNullOrEmpty(existsCopyName) ? fileNames : existsCopyName + CommonConstants.NormalSeparator + fileNames;
            listDicAddedDocuments[operationId].AddRange(filePaths);

            DocumentType documentType = listDropableColumns.Find(columnInfo => columnInfo.FieldName == fieldName).DocumentType;
            int count = filePaths.Count;
            List<DocumentInfo> documents = new List<DocumentInfo>();
            DocumentInfo document = null;
            for (int i = 0; i < count; i++)
            {
                string fileName = Path.GetFileName(filePaths[i]);
                document = new DocumentInfo();
                document.Id = Guid.NewGuid();
                document.Name = fileName;
                document.OriginalPath = filePaths[i];
                document.DocumentType = documentType;
                document.CreateBy = LocalData.UserInfo.LoginID;
                document.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                document.FormType = FormType.Unknown;
                document.OperationID = operationId;
                document.Type = this.OperationType;

                documents.Add(document);

            }
            listAddedDocuments.AddRange(documents);

            if (documentType == DocumentType.SO && gridViewList.Columns.ColumnByFieldName("SONO") != null && SOSetting.Current.AutoFillSO)
            {

                string existsSONOs = row.Field<string>("SONO");
                AutoFillSONOs(operationId, existsSONOs, filePaths);
            }

            ResetBinding();

            // SetBarItemEnable(SaveName, true);


        }
        /// <summary>
        /// 通过业务ID查找所属数据行
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        public DataRow GetDataRowByOperationId(Guid operationId)
        {
            DataRow[] rows = CurrentDataSource.Select(string.Format("ID='{0}'", operationId));
            if (rows != null && rows.Length > 0)
                return rows.First();
            return null;
        }
        private string soNOseparator = "/";
        /// <summary>
        /// 自动根据拖拽文件生成SONO
        /// 规则为:
        /// 1.如果只有一个附件，则SONO为附件名称
        /// 2.多于一个附件，则首先取第一个附件的完整名称，再加上其他附件名称的后两位，中间用/分隔
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private void AutoFillSONOs(Guid operationId, string existsSONOs, List<string> filePaths)
        {
            DataRow row = GetDataRowByOperationId(operationId);

            string soNos = (from file in filePaths
                            select Path.GetFileNameWithoutExtension(file)).ToList().Aggregate((a, b) => a + CommonConstants.NormalSeparator + b);
            string newSONos = soNos;
            if (string.IsNullOrEmpty(existsSONOs))
            {
                if (filePaths.Count < 2)
                {
                    newSONos = soNos;
                }
                else
                {
                    string temp = filePaths[0];
                    filePaths.RemoveAt(0);
                    soNos = (from file in filePaths
                             let item = Path.GetFileNameWithoutExtension(file)
                             select item.Substring(item.Length - 2, 2)).ToList().Aggregate((a, b) => a + CommonConstants.NormalSeparator + b);
                    newSONos = string.Format("{0}{1}{2}", temp, soNOseparator, soNos);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(soNos))
                {
                    if (soNos.Length > 2)
                    {
                        soNos = soNos.Substring(soNos.Length - 2, 2);
                    }
                    newSONos = string.Format("{0}{1}{2}", existsSONOs, soNOseparator, soNos);
                }
            }
            row["SONO"] = newSONos;
            int datasourceIndex = this.CurrentDataSource.Rows.IndexOf(row);
            int rowHandle = this.gridViewList.GetRowHandle(datasourceIndex);
            RecordChange(rowHandle, "SONO");


        }
        void gridViewList_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //右键单击时，显示右键菜单
            if (e.Button == MouseButtons.Right && e.Clicks == 1)
                DoShowMenu(gridViewList.CalcHitInfo(new Point(e.X, e.Y)));

        }
        void DoShowMenu(DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi)
        {
            DataRow row = this.gridViewList.GetFocusedDataRow();
            if (row == null)
            {
                return;
            }
            UnregisterContextMenuUISite();

            BusinessColumnInfo columnInfo = listDropableColumns.Find(column => column.FieldName == hi.Column.FieldName);
            // 如果点击的时列表单元格，则显示菜单
            if (hi.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
            {
                CustomGridMenu gridMenu = new CustomGridMenu(this.gridViewList);
                RegisterContextMenuUISite(gridMenu);
                gridMenu.InitData(this, row, columnInfo, GetPresentTemplateCode());
                gridMenu.Init(hi);
                //显示在光标点击位置
                gridMenu.Show(hi.HitPoint);
            }
        }

        private void RegisterContextMenuUISite(CustomGridMenu gridMenu)
        {
            string businessContextMenuStripUISiteName = GetBusinessContextMenuStripUISiteName();
            if (!RootWorkItem.UIExtensionSites.Contains(businessContextMenuStripUISiteName))
            {
                RootWorkItem.UIExtensionSites.RegisterSite(businessContextMenuStripUISiteName, gridMenu);
            }
        }

        private void UnregisterContextMenuUISite()
        {
            string businessContextMenuStripUISiteName = GetBusinessContextMenuStripUISiteName();
            if (RootWorkItem.UIExtensionSites.Contains(businessContextMenuStripUISiteName))
            {
                RootWorkItem.UIExtensionSites.UnregisterSite(businessContextMenuStripUISiteName);
            }

        }

        void toolStripItem_Click(object sender, EventArgs e)
        {
            ToolStripItem item = sender as ToolStripItem;

            RootWorkItem.State["CurrentBaseBusinessPart"] = this;
            CurrentContextMenuItemTag = item.Tag;
            string itemName = item.Name;
            if (!localCommands.Contains(itemName))
            {
                RootWorkItem.Commands[itemName].Execute();
            }
            else
            {
                MethodInfo method = this.GetType().GetMethod(itemName);
                method.Invoke(this, new object[] { sender, e });
            }
        }


        private List<ToolStripItem> GetBusinessItems()
        {
            List<ToolStripItem> items = new List<ToolStripItem>();
            foreach (ToolStripItem item in this.contextMenuStrip.Items)
            {
                if (item.Tag != null)
                {
                    items.Add(item);
                }
            }
            return items;
        }

        public void EmailAdvanceQuery()
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Dictionary<string, object> initValues = new Dictionary<string, object>();
                initValues.Add(ICP.Common.Business.ServiceInterface.Constants.BusinessTypeKey, this.BusinessType);
                initValues.Add("TemplateCode", TemplateCode);
                frmAdvanceQuery frmQuery = this.RootWorkItem.Items.AddNew<frmAdvanceQuery>();
                frmQuery.Init(initValues);
                frmQuery.MaximizeBox = frmQuery.MinimizeBox = false;
                frmQuery.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                DialogResult result = frmQuery.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    this.AdvanceQueryString = frmQuery.AdvanceQueryString;
                    //区分是否要连接本地缓存数据库还是数据库,1表示连接数据库，0表示连接本地缓存数据库
                    RootWorkItem.State["IsAdvanceSearch"] = true;
                    this.QueryData(true);
                }
                frmQuery = null;
            }
        }
        /// <summary>
        /// 下拉多选业务所属公司
        /// <remarks>加载时默认为所有公司</remarks>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        public void EmailSelectCompany()
        {
            if (this.TemplateCode.Equals(ICP.Common.ServiceInterface.DataObjects.ListFormType.MailLink4Carrier.ToString()))
                return;
            SelectedCompanyIds = RootWorkItem.State["BarEditItemChangedValue"] == null ? null :
            RootWorkItem.State["BarEditItemChangedValue"].ToString();
            this.QueryData(false);
        }
        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonConstants.Command_Add_Attachment_Name)]
        public void Command_Add_Attachment(object sender, EventArgs e)
        {
            DocumentType documentType = (DocumentType)Enum.Parse(typeof(DocumentType), CurrentContextMenuItemTag.ToString());
            string fieldName = this.listDropableColumns.Find(columnInfo => columnInfo.DocumentType == documentType).FieldName;

            string[] filePaths = CommonUIUtility.SelectFilesToUpload();
            if (filePaths == null) return;
            //文档大小超出限制是以异常形式抛出，故在此捕获异常
            try
            {
                if (!CommonUIUtility.ValidateFileInfo(filePaths)) return;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this, ex);
                return;
            }

            HandleDragDrop(this.CurrentRowHandle, filePaths.ToList(), fieldName);

        }
        /// <summary>
        /// 从字符串中移除指定的子字符串
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        private string RemoveSubString(string source, string target, string separator)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(target))
                return source;
            List<string> temp = source.Split(separator.ToCharArray()).Where(name => !string.IsNullOrEmpty(name)).ToList();
            temp.Remove(target);
            temp.RemoveAll(item => item == separator);
            if (temp.Count == 0)
                return null;
            return temp.Aggregate((a, b) => a + separator + b).Trim(separator.ToCharArray());
        }

        /// <summary>
        /// 删除附件
        /// 如果是在服务端已保存的文档，则获取出来的信息为 文件名
        /// 否则获取出来为 本地文件的绝对路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonConstants.Command_Delete_Attachment_Name)]
        public void Command_Delete_Attachment(object sender, EventArgs e)
        {
            string attachmentName = this.CurrentContextMenuItemTag.ToString();
            DeleteDocument(new List<string> { attachmentName });
        }
        private void DeleteDocument(List<string> listFileName)
        {
            if (listFileName == null || listFileName.Count == 0)
                return;
            int rowHandle = this.CurrentRowHandle;
            foreach (string attachmentName in listFileName)
            {
                //测试是否为本地文件绝对路径，如果是，则代表当前要删除的文档为本地刚上传且还没提交到服务端保存的文档
                bool isLocalPath = Path.IsPathRooted(attachmentName);
                DocumentType documentType;
                string fieldName;
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(attachmentName);
                string fileName = Path.GetFileName(attachmentName);
                //如果是本地刚上传还未提交到服务端的文档，则从相关的维护集合中删除上传信息
                if (isLocalPath)
                {
                    this.listDicAddedDocuments[this.OperationID].Remove(attachmentName);
                    DocumentInfo documentInfo = this.listAddedDocuments.Find(document => document.OperationID == this.OperationID && document.OriginalPath == attachmentName);
                    this.listAddedDocuments.Remove(documentInfo);
                    documentType = documentInfo.DocumentType;
                    fieldName = this.listDropableColumns.Find(column => column.DocumentType == documentType).FieldName;

                }
                //如果是删除服务端文档，则添加删除记录
                else
                {
                    this.listDicDeletedDocuments[this.OperationID].Add(attachmentName);
                    List<string> dropableFieldNames = this.listDropableColumns.Select<BusinessColumnInfo, string>(column => column.FieldName).ToList();
                    fieldName = GetFieldName(this.CurrentRow, dropableFieldNames, attachmentName);
                    documentType = this.listDropableColumns.Find(column => column.FieldName == fieldName).DocumentType;

                }
                //更新对应的附件列文件名称
                this.CurrentRow[fieldName] = RemoveSubString(this.CurrentRow[fieldName].ToString(), fileName, CommonConstants.NormalSeparator);
                //如果是自定填充SO号，则从SO号列移除对应的SO号
                if (documentType == DocumentType.SO && gridViewList.Columns.ColumnByFieldName("SONO") != null && SOSetting.Current.AutoFillSO)
                {

                    string removeNO = fileNameWithoutExtension;
                    if (!this.CurrentRow.Field<string>("SONO").StartsWith(fileNameWithoutExtension))
                    {
                        removeNO = fileNameWithoutExtension.Substring(fileNameWithoutExtension.Length - 2, 2);
                    }
                    string newSONO = RemoveSubString(this.CurrentRow.Field<string>("SONO"), removeNO, soNOseparator);
                    this.CurrentRow["SONO"] = newSONO;
                    RecordChange(rowHandle, "SONO");
                }
            }
            ResetBinding();
        }
        private void RecordChange(int rowHandle, string fieldName)
        {

            if (listEditableColumns.Exists(column => column.FieldName == fieldName))
            {
                if (!IsDataEdited)
                {
                    IsDataEdited = true;
                }
                if (!this.listDicEditedCells[rowHandle].Contains(fieldName))
                {
                    this.listDicEditedCells[rowHandle].Add(fieldName);
                }
            }
        }

        private string GetFieldName(DataRow dataRow, List<string> dropableFieldNames, string attachmentName)
        {
            foreach (string fieldName in dropableFieldNames)
            {
                if (dataRow.Field<string>(fieldName).Contains(attachmentName))
                    return fieldName;
            }
            throw new NullReferenceException("无法找到附件所在的列名!");
        }

        /// <summary>
        /// 编辑改变单元格触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewList_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            string fieldName = e.Column.FieldName;
            int rowHandle = e.RowHandle;
            RecordChange(rowHandle, fieldName);

        }

        private void OnGridViewFocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (FocusedRowChangedHandler != null)
            {
                this.FocusedRowChangedHandler(this, e);

                if (CurrentRow == null)
                    return;
                bool isvalid = bool.Parse(CurrentRow["IsValid"].ToString());
                if (!isvalid)
                {
                    SetBarItemEnable(CancelName, false);
                    SetBarItemEnable(ResumeName, true);
                    SetBarItemEnable(CopyName, false);
                    SetBarItemEnable(EditName, false);
                }
                else
                {
                    SetBarItemEnable(CancelName, true);
                    SetBarItemEnable(ResumeName, false);
                    SetBarItemEnable(CopyName, true);
                    SetBarItemEnable(EditName, true);
                }
            }
        }

        void gridViewList_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            this.toolTipController1.ShowHint(this.gridViewList.GetFocusedDisplayText());
        }

        /// <summary>
        /// 保存附件
        /// </summary>
        public void Save()
        {
            InnerSaveData();
        }

        private void InnerSaveData()
        {
            if (this.listDicAddedDocuments.Count == 0 && this.listDicDeletedDocuments.Count == 0 && this.listDicEditedCells.Count == 0)
                return;
            string message = LocalData.IsEnglish ? "Saving is in progress…" : "正在保存。。。";
            BeginShowAnimation(message);
            try
            {
                //先保存业务更改信息
                SaveUpdateOperationData();
                //保存文档更改信息
                if (SaveAttachment())
                    AfterSaveDocument();
            }
            catch (System.Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
            }
        }

        private void SaveUpdateOperationData()
        {
            if (this.listDicEditedCells.Count > 0)
            {
                List<BusinessSaveParameter> filedParameters = GetUpdateFiledNameParameter();
                DataTable dt = BusinessQueryService.Save(filedParameters);
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<BusinessSaveParameter> tempParameters = new List<BusinessSaveParameter>();
                    int count = dt.Rows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        Guid id = dt.Rows[i].Field<Guid>("ID");
                        DateTime? updateDate = dt.Rows[i].Field<DateTime?>("UpdateDate");
                        DataRow row = GetDataRowByOperationId(id);
                        row["UpdateDate"] = updateDate;
                        BusinessSaveParameter parameter = filedParameters.Find(o => o["ID"].Equals(id.ToString()));
                        parameter["UpdateDate"] = updateDate;
                        tempParameters.Add(parameter);
                    }
                    FCMCommonOperationService.UpdateBusinessCopyFileList(tempParameters);
                    tempParameters = null;
                }
                dt = null;
                filedParameters = null;
                this.listDicEditedCells.Clear();
            }
        }

        private List<BusinessSaveParameter> GetUpdateFiledNameParameter()
        {
            List<BusinessSaveParameter> parameters = new List<BusinessSaveParameter>();
            foreach (int rowHandle in this.listDicEditedCells.Keys)
            {
                BusinessSaveParameter parameter = new BusinessSaveParameter();
                DataRow row = GetDataRowByRowHandle(rowHandle);
                parameter["ID"] = row.Field<Guid>("ID");
                // parameter["OperationType"] = operationType;
                parameter["UpdateDate"] = row.Field<DateTime?>("UpdateDate");
                foreach (string fieldName in this.listDicEditedCells[rowHandle])
                {
                    parameter[fieldName] = row[fieldName];
                }
                parameters.Add(parameter);
            }
            return parameters;
        }

        private void AfterSaveDocument()
        {
            EndShowAnimation();
            this.AcceptChanges();
            ResetDocumentRecord();
        }
        protected override void BeginShowAnimation(string tip)
        {
            ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm(this.gridControlList, tip);
        }
        protected override void EndShowAnimation()
        {
            ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm();
        }
        ///// <summary>
        ///// 保存文档 如果有文档需要保存，则返回true,否则返回false
        ///// </summary>
        ///// <returns>如果有文档需要保存，则返回true,否则返回false</returns>
        //private bool SaveAttachment()
        //{
        //    if (this.listDicAddedDocuments.Count > 0 || this.listDicDeletedDocuments.Count > 0)
        //    {
        //        List<string> listDeletedAttachment = new List<string>();
        //        List<Guid> listOperationIds = new List<Guid>();
        //        //foreach (Guid operationId in this.listDicDeletedDocuments.Keys)
        //        //{
        //        //    foreach (string attachmentName in this.listDicDeletedDocuments[operationId])
        //        //    {
        //        //        listDeletedAttachment.Add(attachmentName);
        //        //        listOperationIds.Add(operationId);

        //        //    }
        //        //}

        //        List<DocumentInfo> documents = new List<DocumentInfo>();
        //        if (listDicAddedDocuments.Count > 0)
        //        {
        //            foreach (Guid operationId in this.listDicAddedDocuments.Keys)
        //            {
        //                foreach (string copyFilePath in this.listDicAddedDocuments[operationId])
        //                {
        //                    documents.Add(CreateDocumentInfo(operationId, copyFilePath, DocumentType));
        //                }
        //                listOperationIds.Add(operationId);
        //            }

        //            Upload(listOperationIds, documents, listDeletedAttachment);
        //        }

        //        if (listDicDeletedDocuments.Count > 0)
        //        {
        //            List<Guid> deleteOperationIds = new List<Guid>();
        //            foreach (var item in listDicDeletedDocuments)
        //            {
        //                listDeletedAttachment.AddRange(item.Value);
        //                int count = item.Value.Count;
        //                Guid operationId = item.Key;
        //                for (int i = 0; i < count; i++)
        //                {
        //                    deleteOperationIds.Add(operationId);
        //                }
        //            }
        //            Upload(deleteOperationIds, new List<DocumentInfo>(), listDeletedAttachment);
        //        }
        //        return true;
        //    }
        //    return false;
        //}
        private bool SaveAttachment()
        {
            bool IsSaveAttachment = false;
            if (this.listDicAddedDocuments.Count > 0 || this.listDicDeletedDocuments.Count > 0)
            {
                List<string> listDeletedAttachment = new List<string>();
                List<Guid> listOperationIds = new List<Guid>();
                foreach (Guid operationId in this.listDicDeletedDocuments.Keys)
                {
                    foreach (string attachmentName in this.listDicDeletedDocuments[operationId])
                    {
                        listDeletedAttachment.Add(attachmentName);
                        listOperationIds.Add(operationId);
                    }
                }
                try
                {
                    ClientFileService.Save(listAddedDocuments, listDeletedAttachment, listOperationIds);
                    IsSaveAttachment = true;
                }
                catch (System.Exception ex)
                {
                    IsSaveAttachment = false;
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                }
            }

            return IsSaveAttachment;
        }

        private void Upload(List<Guid> operationIds, List<DocumentInfo> documents, List<string> deleteFileNames)
        {
            List<string> soNos = new List<string>(operationIds.Count);
            List<DateTime?> updateDates = new List<DateTime?>(operationIds.Count);
            bool isContainsSONO = CurrentDataSource.Columns.Contains("SONO");
            foreach (DataRow row in CurrentDataSource.Rows)
            {
                if (operationIds.Contains<Guid>(row.Field<Guid>("ID")) && isContainsSONO)
                {
                    soNos.Add(row.Field<string>("SONO"));
                    updateDates.Add(row.Field<DateTime?>("UpdateDate"));
                }
            }
            //ClientFileService.UploadSOCopyAndSoNos(CreateFileCopyParametersInfo(operationIds.ToArray<Guid>(), documents, deleteFileNames, soNos.ToArray(), updateDates.ToArray(), isContainsSONO));
        }

        private FileCopyParameters CreateFileCopyParametersInfo(Guid[] operationIds, List<DocumentInfo> documents, List<string> deleteFiles, string[] soNos, DateTime?[] updateDates, bool isContainsSONO)
        {
            return new FileCopyParameters() { OperationIDs = operationIds, Documents = documents, DeleteFiles = deleteFiles, SONOs = soNos, UpdateDates = updateDates, HasSONOColumn = isContainsSONO };
        }

        private DocumentInfo CreateDocumentInfo(Guid operationId, string copyFilePath, DocumentType documentType)
        {
            DocumentInfo document = new DocumentInfo();
            document.OriginalPath = copyFilePath;
            document.Id = Guid.NewGuid();
            document.Name = Path.GetFileName(copyFilePath);
            document.FileSources = FileSource.FDocument;
            document.DocumentType = documentType;
            document.OperationID = operationId;
            document.CreateBy = LocalData.UserInfo.LoginID;
            document.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            document.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;

            return document;
        }


        private void ResetDocumentRecord()
        {
            this.listDicAddedDocuments.Clear();
            this.listAddedDocuments.Clear();
            this.listDicDeletedDocuments.Clear();

        }

        private void OnDocumentUploadFailed(UploadFailedMessage message)
        {
            EndShowAnimation();
            TrapException(new ApplicationException(message.ErrorMessage));


        }
        private void OnDocumentUploadSucessed(DocumentInfo[] documents)
        {
            AfterSaveDocument();

        }
        /// <summary>
        /// 设置指定单元格的值
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        public void SetCellValue(Guid operationId, string fieldName, object value)
        {

            DataRow row = GetDataRowByOperationId(operationId);
            if (row != null && row.Table.Columns.Contains(fieldName))
            {
                row[fieldName] = value;
            }
        }
        /// <summary>
        /// 设置过滤结果数据航指定列的值
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        public void SetFilterRowCellValue(string filterExpression, string columnName, object value)
        {
            DataTable dt = this.CurrentDataSource;
            if (dt == null || dt.Rows.Count <= 0 || !dt.Columns.Contains(columnName))
                return;
            DataRow[] rows = dt.Select(filterExpression);
            if (rows == null || rows.Length <= 0)
                return;
            int lenth = rows.Length;
            DataRow[] newRows = new DataRow[lenth];

            for (int i = 0; i < lenth; i++)
            {
                rows[i][columnName] = value;

            }

        }
        /// <summary>
        /// 处理异常捕获
        /// </summary>
        /// <param name="ex"></param>
        public void TrapException(Exception ex)
        {
            LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex);
        }

        #region 根据关键字显示数据，并且高亮度显示关键字
        private BarEditItem barEditItem = null;
        private BarEditItem GetBarEditSearchItem()
        {
            if (barEditItem != null)
                return barEditItem;
            string businessPartToolbarUISiteName = this.TemplateCode;
            BarEditItem item = this.RootWorkItem.UIExtensionSites[businessPartToolbarUISiteName].First(barItem => ((BarItem)barItem).Name == "txtQuery") as BarEditItem;
            barEditItem = item;
            return item;
        }
        public string QueryNo
        {
            get
            {
                BarEditItem item = GetBarEditSearchItem();
                if (item.EditValue == null)
                    return RootWorkItem.State["QueryString"].ToString();
                else
                    return item.EditValue.ToString().Trim();
            }
        }
        private DataRow[] previousMatchRows = null;
        private int previousSelectRowIndex = -1;
        private string previousQueryNo = string.Empty;

        public void EmailQuery()
        {
            //if (CurrentDataSource == null)
            //   return;
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string queryNo = this.QueryNo;
                //如果搜索单号为空，则直接返回
                if (string.IsNullOrEmpty(queryNo))
                    return;
                //如果此次搜索单号和上一次搜索相同，则从旧有匹配记录里查找
                if (queryNo.Equals(this.previousQueryNo, StringComparison.CurrentCultureIgnoreCase))
                {
                    //未知业务面板，用户在有搜索TextBox的BarItem去搜索("OESHB12120212")，这个时候生成了列，
                    //当用户切换到下一封邮件也是有搜索栏的未知业务面板时，同时输入了相同的业务号("OESHB12120212"),这个时候要去重新查找出数据源和赋值
                    if (this.IsClearColumnsAndDataSource)
                    {
                        InnerQuery(queryNo);
                        this.IsClearColumnsAndDataSource = false;
                        return;
                    }
                    //如果上次搜索没有匹配行，则直接返回
                    if (previousMatchRows == null || previousMatchRows.Length <= 0)
                        return;
                    //如果上次搜索匹配已经到了最后一条匹配记录，则直接返回
                    if (previousSelectRowIndex == previousMatchRows.Length - 1)
                        return;
                    if (IsNullDataSource())
                        return;

                    SetSelectedRow();
                }
                //如果此次搜索和上次搜索单号不同
                else
                {
                    InnerQuery(queryNo);
                }
            }
        }

        /// <summary>
        /// 从数据源中执行查找匹配的KeyWord
        /// </summary>
        /// <param name="queryNo"></param>
        private void InnerQuery(string queryNo)
        {
            //重置搜索记录
            ResetQueryRecord();
            EmailQueryForMailLink4Unknown(queryNo);
            while (IsDataBindingComplete())
            {
                //数据绑定完成
                RootWorkItem.State["DataBindingComplete"] = false;
                if (IsNullDataSource())
                    return;
                DataRow[] rows = previousMatchRows = this.CurrentDataSource.Select(GetFilterExpression());
                if (rows == null || rows.Length <= 0)
                    return;
                SetSelectedRow();
                previousQueryNo = queryNo;
            }
        }

        private bool IsNullDataSource()
        {
            return this.CurrentDataSource == null;
        }

        private bool IsDataBindingComplete()
        {
            bool isComplete = false;
            bool.TryParse(RootWorkItem.State["DataBindingComplete"].ToString(), out isComplete);
            return isComplete;
        }

        public void EmailQueryForMailLink4Unknown(string queryNo)
        {
            if (IsUnknownTemplateCode())
            {
                this.AdvanceQueryString = SQLCondition(queryNo);
                //需要绑定列
                RootWorkItem.State["NeedGererateColumns"] = true;
                //数据绑定完成
                RootWorkItem.State["DataBindingComplete"] = false;
                this.QueryData(true);
                previousQueryNo = queryNo;
            }
            else
            {
                //数据绑定完成
                RootWorkItem.State["DataBindingComplete"] = true;
            }
        }

        public string GetPresentTemplateCode()
        {
            return IsUnknownTemplateCode() ? ICP.Common.ServiceInterface.DataObjects.ListFormType.MailLink4Customer.ToString() : this.TemplateCode;
        }


        private void SetSelectedRow()
        {
            DataRow row = previousMatchRows[++previousSelectRowIndex];

            int dataSourceIndex = GetDataSource().Rows.IndexOf(row);
            if (dataSourceIndex == -1)
                return;
            int rowIndex = this.gridViewList.GetRowHandle(dataSourceIndex);
            this.gridViewList.FocusedRowHandle = rowIndex;
        }

        private void ResetQueryRecord()
        {
            this.previousQueryNo = string.Empty;
            this.previousMatchRows = null;
            this.previousSelectRowIndex = -1;
        }
        private string GetFilterExpression()
        {
            List<string> filterColumnNames = AllColumns();
            if (filterColumnNames == null || filterColumnNames.Count <= 0)
                return "1=1";
            return GetExpression(filterColumnNames, this.QueryNo, false);
        }

        /// <summary>
        /// 业务面板查找列集合
        /// </summary>
        /// <returns></returns>
        public List<string> AllColumns()
        {
            List<string> _AllColumns = new List<string>();
            List<BusinessColumnInfo> list = GetColumnInfos(GetPresentTemplateCode());
            if (list == null || list.Count == 0)
                return null;
            foreach (BusinessColumnInfo column in list)
            {
                if (column.EditType == ColumnEditType.Memo || column.EditType == ColumnEditType.Text)
                {
                    _AllColumns.Add(column.FieldName);
                }
            }
            return _AllColumns;
        }

        private string GetExpression(List<string> list, string name, bool isValueList)
        {
            if (list == null || list.Count <= 0)
                return string.Empty;
            List<string> result = new List<string>();
            foreach (string item in list)
            {
                if (isValueList)
                {
                    result.Add(string.Format(" [{0}] like '%{1}%' ", name, item));
                }
                else
                {
                    result.Add(string.Format(" [{0}] like '%{1}%' ", item, name));
                }

            }
            return result.Aggregate((condition1, condition2) => condition1 + " Or " + condition2);
        }
        #endregion

        private void OnApplicationExit(object sender, EventArgs e)
        {
            if (!isColumnSettingChanged)
                return;
            WaitCallback callback = (obj) => SaveCustomColumnInfo();
            ThreadPool.QueueUserWorkItem(callback);
        }

        #endregion
    }
}
