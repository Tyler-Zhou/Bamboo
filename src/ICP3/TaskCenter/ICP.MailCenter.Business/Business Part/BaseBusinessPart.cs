using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using Microsoft.Practices.CompositeUI.SmartParts;
using DevExpress.XtraGrid;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;
using System.Data;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;
using DevExpress.XtraBars;
using Microsoft.Practices.CompositeUI.WinForms;
using ICP.DataCache.ServiceInterface;
using System.Drawing;
using System.IO;
using ICP.Message.ServiceInterface;
using System.Threading;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Server;
using System.Reflection;
using ICP.FCM.Common.ServiceInterface.Interfaces;
using Microsoft.Practices.CompositeUI.Utility;
using ICP.MailCenter.CommonUI;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using Microsoft.Practices.CompositeUI.EventBroker;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.TaskCenter.ServiceInterface;
using ICP.MailCenter.Business.ServiceInterface;


namespace ICP.Common.Business.ServiceInterface
{
    /// <summary>
    /// 业务面板基类
    /// </summary>
    [SmartPart]
    public partial class BaseBusinessPart : XtraUserControl, IBusinessPart
    {
        #region 属性
        [ServiceDependency]
        public WorkItem WorkItem { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public MailInfoGetter MailInfoGetter { get; set; }

        public IICPCommonOperationService FCMCommonOperationService
        {

            get
            {
                return ServiceClient.GetService<IICPCommonOperationService>();
            }
        }


        public IBusinessOperationHandleService BusinessOperationHandleService
        {
            get
            {
                return ServiceClient.GetClientService<IBusinessOperationHandleService>();
            }
        }
        public IClientBusinessContactService ClientBusinessContactService
        {
            get
            {
                return ServiceClient.GetService<IClientBusinessContactService>();
            }
        }

        /// <summary>
        /// 文件服务类
        /// </summary>
        public IClientFileService ClientFileService
        {
            get
            {
                return ServiceClient.GetService<IClientFileService>();
            }
        }



        /// <summary>
        /// 业务信息获取工厂，比如获取描述和RefNo
        /// </summary>
        public IGetBillInfoFactory GetBillInfoFactory
        {
            get
            {
                return ServiceClient.GetClientService<IGetBillInfoFactory>();
            }
        }
        public IOperationMessageRelationService OperationMessageRelationService
        {
            get
            {
                return ServiceClient.GetService<IOperationMessageRelationService>();
            }
        }

        public string SenderEmailAddress { get; set; }
        #endregion
        private bool isCommonContextMenuItemsExists = false;
        private List<ContextMenuItemInfo> contextMenuCommonItemList;
        public string contextMenuStripUISiteName = "EmailCenterContextMenu";
        public string toolbarUISiteName = "EmailCenterToolbar";
        private ICP.Message.ServiceInterface.Message currentMessage;
        private bool needBindData = true;
        private int visibleIndex = 0;

        private DevExpress.XtraEditors.PictureEdit picTipImage;
        private DevExpress.XtraEditors.LabelControl lblAnimationTip;
        public Dictionary<string, string> dicMessageRelation = new Dictionary<string, string>();
        private UserCustomGridInfo currentUserCustomGridInfo;
        public object syncObj = new object();
        public string selectedCompanyIds = LocalData.UserInfo.UserOrganizationList.FindAll(o => o.Type == LocalOrganizationType.Company).Select(o => o.ID.ToString()).Aggregate((a, b) => a + "," + b);
        private string isAssociatedColumnName = "IsAssociated";

        ///<summary>
        ///可以修改列和对应是否已经修改字典
        ///</summary>
        private Dictionary<string, bool> dictEditable = new Dictionary<string, bool>();
        /// <summary>
        /// 当前弹出菜单附加数据
        /// </summary>
        public object CurrentTag = null;

        /// <summary>
        /// 当前选中行行号
        /// </summary>
        int FocusedRowHandle = 0;


        public AbstractBussinessPartOperation AbstractBussinessPartOperation = null;
        /// <summary>
        /// 任务中心初始化BaseBusinessPart的参数对象
        /// </summary>
        public TaskCenterOpreateParams CurrentTaskCenterOpreateParams;

        private string copyPathColumnName = "CopyPath";
        private string normalSeparator = Environment.NewLine;



        private List<string> allCopyColumns = new List<string>();
        /// <summary>
        /// 所有可以上传的列名列表
        /// </summary>
        private List<string> AllCopyColumns
        {
            get
            {
                if (allCopyColumns.Count < 1)
                {
                    allCopyColumns.AddRange(DictUploadColumnType.Keys.ToArray());
                }
                return allCopyColumns;
            }
        }

        /// <summary>
        /// 本业务中可以上传文件的列集合
        /// </summary>
        private Dictionary<string, string> dictCopyColumns = new Dictionary<string, string>();

        /// <summary>
        /// 维护业务号和删除文档之间的关系
        /// </summary>
        private ListDictionary<Guid, string> dicDeleteAttachments = new ListDictionary<Guid, string>();

        private Dictionary<string, UploadColumnType> dictUploadColumnType;
        /// <summary>
        /// 上传文件时文件类型词典
        /// </summary>
        private Dictionary<string, UploadColumnType> DictUploadColumnType
        {
            get
            {
                if (dictUploadColumnType == null)
                {
                    dictUploadColumnType = ClientFileService.GetUploadColumnType((int)OperationType);
                }
                return dictUploadColumnType;
            }
        }

        public string ContextMenuStripUISiteName
        {
            get
            {
                return contextMenuStripUISiteName;   // this.ListType.ToString();
            }
        }
        /// <summary>
        /// 业务面板是否需要绑定数据
        /// </summary>
        public virtual bool NeedBindData
        {
            get
            {
                // ListFormType formType = this.GetListFormType();
                if (TemplateCode == "MailLink4Carrier" || TemplateCode == "MailLink4Unknown")
                    return false;
                else
                    return true;
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
        /// 模板代码
        /// </summary>
        public virtual string TemplateCode
        {
            get;
            set;
        }

        public virtual ListDictionary<Guid, string> AddedAttachments
        {
            get;
            set;
        }

        public virtual ListDictionary<Guid, string> DeleteAttachments
        {
            get;
            set;
        }

        public bool IsEnglish
        {
            get
            {
                return LocalData.IsEnglish;
            }
        }
        ///// <summary>
        ///// 业务面板中的子面板类型
        ///// </summary>
        //public virtual ListFormType ListType
        //{
        //    get;
        //    set;
        //}

        public virtual bool IsGridVisible
        {
            get
            {
                return this.gridControlList.Visible;
            }
            set
            {
                this.gridControlList.Visible = value;
            }
        }
        /// <summary>
        /// 当前行业务号
        /// </summary>
        public string OperationNo
        {
            get
            {
                return this.CurrentRow["NO"] as string;
            }
        }
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid ID
        {
            get
            {
                if (CurrentRow == null)
                    return Guid.Empty;
                return new Guid(this.CurrentRow["ID"].ToString());
            }
        }
        /// <summary>
        /// 业务更新时间
        /// </summary>
        public DateTime? UpdateDate
        {
            get
            {
                string updateDate = this.CurrentRow["UpDateDate"].ToString();
                if (string.IsNullOrEmpty(updateDate))
                    return null;
                else
                    return DateTime.Parse(updateDate);
            }
        }
        /// <summary>
        /// 业务是否有效
        /// </summary>
        public bool IsValid
        {
            get
            {
                return (bool)this.CurrentRow["IsValid"];
            }
        }
        /// <summary>
        /// 列表中业务所属的业务类型
        /// </summary>
        public BusinessType BusinessType
        {
            get
            {
                // return BusinessType.OE;
                //int businessTypeValue = Int32.Parse(this.gridViewList.GetFocusedDataRow()["Type"].ToString());
                //return (BusinessType)businessTypeValue;
                return (BusinessType)((int)OperationType);
            }
        }
        /// <summary>
        /// 列表中业务所属的业务类型,操作视图编码的第二部分代表业务类型
        /// </summary>
        public OperationType OperationType
        {
            get
            {
                string strOperationType = TemplateCode.Split(new char[] { '_' })[1];
                return (ICP.Framework.CommonLibrary.Common.OperationType)Enum.Parse(typeof(ICP.Framework.CommonLibrary.Common.OperationType), strOperationType, true);
            }
        }
        /// <summary>
        /// 面板当前关联的邮件信息
        /// </summary>
        public ICP.Message.ServiceInterface.Message CurrentMessage
        {
            get
            {
                return this.currentMessage;
            }
            set
            {
                this.currentMessage = value;
            }
        }
        /// <summary>
        /// Tab选项卡集合信息
        /// </summary>
        public List<TabControl> TabControls { get; set; }

        private OperationMessageRelation operationMessageRelation;
        /// <summary>
        /// 当前邮件与业务关联信息
        /// </summary>
        public OperationMessageRelation CurrentMessageRelation
        {
            get
            {

                return this.operationMessageRelation;
            }
            set
            {
                this.operationMessageRelation = value;
            }

        }
        private List<string> conditionColumnNames = new List<string>() { "NO", "RefNO" };
        /// <summary>
        /// 邮件标题与列表数据需匹配的行名列表
        /// </summary>
        public virtual List<string> ConditionColumnNames
        {
            get
            {
                return this.conditionColumnNames;
            }
            set
            {
                this.conditionColumnNames = value;
            }
        }
        /// <summary>
        /// 邮件标题中抽取出来的可能单号
        /// </summary>
        public List<string> Nos
        {
            get;
            set;
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
        }
        private delegate void ChangeBusinessStyleDelegate(OperationMessageRelation relation, ICP.Message.ServiceInterface.Message message);
        private delegate void ClearBusinessStyleDelegate();

        /// <summary>
        /// 业务操作面板来自哪里类型
        /// TaskCenterOperation为来自任务中心
        /// EmailCenterOperation为来自邮件中心
        /// </summary>
        public OperationSourceType OperationSourceType
        {
            get;
            set;
        }


        /// <summary>
        /// 当前显示页号
        /// </summary>
        private int currentPageIndex = 0;

        /// <summary>
        /// 不同业务的不同处理的的接口实现对象
        /// </summary>
        private IBusiness BusinessSave
        {
            get
            {
                return BusinessFactory.CreateBusiness(TemplateCode);
            }
        }

        /// <summary>
        /// 高级查询面板传递过来的高级查询条件字符串
        /// </summary>
        public string AdvanceQueryString
        {
            get;
            set;
        }



        public EmailSourceType SourceType
        {
            get;
            set;
        }

        /// <summary>
        /// 添加附件命令命令名称
        /// </summary>
        public const string Command_Add_Attachment = "Command_Add_Attachment";
        public const string Command_Delete_Attachment = "Command_Delete_Attachment";

        /// <summary>
        /// 附件上传隐藏列后缀名称如APCopy列对应名称为APCopyPath
        /// </summary>
        const string CopyPath = "Path";


        /// <summary>
        ///刷新事件等细节面板
        /// </summary>
        public void RefershDetail()
        {
            SelectedPageChanged(CurrentRow, null);
        }

        private void gridViewList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                FocusedRowHandle = e.FocusedRowHandle;
                DataRow dr = gridViewList.GetDataRow(e.FocusedRowHandle);
                int state = int.Parse(dr["State"].ToString());
                bool isvalid = bool.Parse(dr["IsValid"].ToString());
                if (!isvalid)
                {
                    SetBarItemEnable(CancelName, false);
                    SetBarItemEnable(ResumeName, true);
                    SetBarItemEnable(CopyName, false);
                    SetBarItemEnable(EditName, false);
                    //控制行的可编制性
                    ControlRowEditable(false);
                }
                else
                {
                    SetBarItemEnable(CancelName, true);
                    SetBarItemEnable(ResumeName, false);
                    SetBarItemEnable(CopyName, true);
                    SetBarItemEnable(EditName, true);
                    ControlRowEditable(true);
                }
                SelectedPageChanged(CurrentRow, null);
            }
            catch (System.Exception ex)
            {

            }
        }

        /// <summary>
        /// 控制行的可编辑列的可以编辑性
        /// </summary>
        /// <param name="isEdit">是否可编辑</param>
        private void ControlRowEditable(bool isEdit)
        {
            foreach (string key in dictEditable.Keys)
            {
                gridViewList.Columns[key].OptionsColumn.AllowEdit = isEdit;
            }
        }

        public BaseBusinessPart()
        {

            InitializeComponent();
            //gridViewList.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewList_FocusedRowChanged);
            // UserPrivateInfo.(TemplateCode + "SplitPositionKey", this.splitContainerControl1.SplitterPosition.ToString());
            xtraTabControl.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(SelectedPageChanged);
            UserPrivateInfo.SetSplitterPosition(this.splitContainerControl1, TemplateCode + "SplitPositionKey", 360);
        }
        /// <summary>
        /// 当前行有没有关联业务
        /// </summary>
        /// <returns></returns>
        public bool IsCurrentBusinessRelated()
        {
            return this.dicMessageRelation.ContainsValue(this.OperationNo);
        }
        private void InnerClearBusinessStyleFormatCondition()
        {
            if (this.gridViewList.FormatConditions["BusinessStyle"] == null)
            {
                return;
            }
            else
            {
                StyleFormatCondition rowCondition = this.gridViewList.FormatConditions["BusinessStyle"] as StyleFormatCondition;
                rowCondition.Expression = "1 !=1 ";
            }
        }
        public void ClearBusinessStyleFormatCondition()
        {
            ClearBusinessStyleDelegate clearDelegate = new ClearBusinessStyleDelegate(InnerClearBusinessStyleFormatCondition);
            this.Invoke(clearDelegate);
        }

        /// <summary>
        /// 设置与当前邮件已关联业务行的行样式
        /// 当前样式为：字体加粗显示
        /// </summary>
        /// <param name="operationNo"></param>
        private void SetBusinessStyleFormationCondition(string operationNo)
        {
            StyleFormatCondition rowCondition;
            if (this.gridViewList.FormatConditions["BusinessStyle"] == null)
            {
                rowCondition = new DevExpress.XtraGrid.StyleFormatCondition();
                rowCondition.Tag = "BusinessStyle";
                rowCondition.Appearance.Font = new System.Drawing.Font(gridViewList.Appearance.Row.Font, System.Drawing.FontStyle.Bold);
                rowCondition.Appearance.Options.UseFont = true;

                rowCondition.Condition = FormatConditionEnum.Expression;
                rowCondition.ApplyToRow = true;
                rowCondition.Expression = GetBusinessExpression(operationNo);
                this.gridViewList.FormatConditions.Add(rowCondition);
            }
            else
            {
                rowCondition = this.gridViewList.FormatConditions["BusinessStyle"] as StyleFormatCondition;
                rowCondition.Expression = GetBusinessExpression(operationNo);
            }
        }

        private void ChangeDataRowPosition(string filterExpression)
        {
            DataTable dt = CurrentDataSource;
            if (dt == null || dt.Rows.Count <= 0)
                return;
            DataRow[] rows = dt.Select(filterExpression);
            if (rows == null || rows.Length <= 0)
                return;
            int lenth = rows.Length;
            DataRow[] newRows = new DataRow[lenth];

            for (int i = 0; i < lenth; i++)
            {
                rows[i][isAssociatedColumnName] = 1;

            }
            this.gridViewList.FocusedRowHandle = 0;

            AcceptChanges();
        }
        private string GetBusinessExpression(string operationNo)
        {
            return string.Format("[NO] == '{0}'", operationNo);
        }
        /// <summary>
        /// 业务面板初始化入口
        /// 动作:1.解析邮件信息
        ///      2.注册工具栏，右键菜单栏拓展点
        ///      3.获取工具栏命令实体，构造工具栏
        ///      4.获取列实体列表，构造列表列
        ///      5.挂接命令处理
        /// </summary>
        /// <param name="mail"></param>
        public void Init()
        {
            // RegisterExtensionSite();
            List<OperationToolbarCommand> commands = GetToolbarCommands();
            this.barManager.Images = ToobarItemFactory.ImageList;
            BuildToobar(commands);
            List<BusinessColumnInfo> columnInfos = GetColumnInfos();
            CreateColumns(columnInfos);
            HookEvent();
            SetBarItemEnable(SaveName, false);
            UserPrivateInfo.SetSplitterPosition(this.splitContainerControl1, TemplateCode + "SplitPositionKey", 360);
            StructuretabWork(TemplateCode);
        }


        /// <summary>
        /// 挂接应用程序退出事件和FCM回调处理程序
        /// </summary>
        private void HookEvent()
        {
            // BusinessOperationHandleService.BusinessOperationCompleted += new EventHandler<CommonEventArgs<BusinessOperationParameter>>(OnBusinessOperationCompleted);
            //IMainForm form = ServiceClient.GetClientService<IMainForm>();
            //form.ApplicationExit += new EventHandler(OnApplicationExit);
        }
        /// <summary>
        /// 刷新数据
        /// </summary>
        public void Refersh()
        {
            // BindData();
            // ICP.Message.ServiceInterface.Message mail = new ICP.Message.ServiceInterface.Message();
            if (AbstractBussinessPartOperation != null)
            {
                AbstractBussinessPartOperation.BindData();
            }

            // HookEvent();
        }

        /// <summary>
        ///注册拓展点
        /// </summary>
        public void RegisterExtensionSite()
        {
            string businessPartToolbarUISiteName = this.SourceType.ToString() + toolbarUISiteName;
            if (!WorkItem.UIExtensionSites.Contains(businessPartToolbarUISiteName))
            {
                WorkItem.UIExtensionSites.RegisterSite(businessPartToolbarUISiteName, this.barTool);
            }
            if (!WorkItem.UIExtensionSites.Contains(this.ContextMenuStripUISiteName))
            {
                WorkItem.UIExtensionSites.RegisterSite(this.ContextMenuStripUISiteName, this.contextMenuStrip);
            }
        }
        /// <summary>
        ///注册拓展点
        /// </summary>
        public void RegisterExtensionSite(string businessPartToolbarUISiteName, string contextMenuStripUISiteName)
        {
            if (!WorkItem.UIExtensionSites.Contains(businessPartToolbarUISiteName))
            {
                WorkItem.UIExtensionSites.RegisterSite(businessPartToolbarUISiteName, this.barTool);
            }
            if (!WorkItem.UIExtensionSites.Contains(contextMenuStripUISiteName))
            {
                WorkItem.UIExtensionSites.RegisterSite(contextMenuStripUISiteName, this.contextMenuStrip);
            }
        }

        /// <summary>
        /// 建立工具栏信息
        /// </summary>
        /// <param name="commands"></param>
        private void BuildToobar(List<OperationToolbarCommand> commands)
        {
            if (commands == null || commands.Count <= 0)
                return;
            foreach (OperationToolbarCommand command in commands)
            {
                ToolbarBuilder builder = new ToolbarBuilder(command);
                builder.CurrentBaseBusinessPart = this;

                DevExpress.XtraBars.BarItem barItem = builder.BuildIn(this.WorkItem, this.barManager);
                if (!string.IsNullOrEmpty(command.ImageName))
                {
                    ImageList imageList = this.barManager.Images as ImageList;
                    if (imageList == null)
                        return;
                    int index = imageList.Images.IndexOfKey(command.ImageName);
                    barItem.ImageIndex = index;
                }
            }

        }

        /// <summary>
        /// 承运人子面板动态构造
        /// </summary>
        /// <param name="gridType"></param>
        public void DynamicCreateColumns(ListFormType? gridType)
        {
            if (gridType == null)
            {
                this.SuspendLayout();
                this.gridViewList.Columns.Clear();
                this.gridControlList.RepositoryItems.Clear();

                this.ResumeLayout();
                return;
            }
            List<BusinessColumnInfo> columns = ColumnTemplateLoader.Current[gridType.ToString()];
            CreateColumns(columns);
        }
        /// <summary>
        /// 根据XML配置和用户自定义列信息 构造列表
        /// </summary>
        /// <param name="columnInfos"></param>
        private void CreateColumns(List<BusinessColumnInfo> columnInfos)
        {
            if (columnInfos == null || columnInfos.Count <= 0)
                return;
            this.SuspendLayout();
            RemoveEventHandler();
            this.gridViewList.Columns.Clear();
            this.gridControlList.RepositoryItems.Clear();

            // visibleIndex = 0;
            this.currentUserCustomGridInfo = GetUserCustomGridInfo();
            int absoluteIndex = 0;
            ///
            CustomColumnInfo demoColumnInfo = new CustomColumnInfo { AbsoluteIndex = absoluteIndex++, VisibleIndex = visibleIndex++, Fixed = ColumnFixedStyle.None, Visible = true, Width = 100 };
            foreach (BusinessColumnInfo columnInfo in columnInfos)
            {

                //添加可以上传列名
                if (AllCopyColumns.Contains(columnInfo.FieldName))
                {
                    dictCopyColumns.Add(columnInfo.FieldName, columnInfo.Caption);
                }

                //添加可编辑列名
                if (columnInfo.Editable)
                {
                    dictEditable.Add(columnInfo.FieldName, false);
                }
                CustomColumnInfo columnCustomInfo = this.currentUserCustomGridInfo == null ? demoColumnInfo : this.currentUserCustomGridInfo.Columns.Find(c => c.Name == columnInfo.Name);

                InnerCreateColumn(columnInfo, columnCustomInfo);

            }
            AddUploadHideColumns(columnInfos);
            AddGridColumnForSort();
            AddSortColumnInfos();
            AddEventHandler();
            this.ResumeLayout();

        }

        private void AddGridColumnForSort()
        {
            DevExpress.XtraGrid.Columns.GridColumn gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();

            gridColumn.OptionsColumn.AllowEdit = false;
            gridColumn.Visible = false;
            gridColumn.VisibleIndex = -1;
            gridColumn.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
            gridColumn.Width = 0;

            gridColumn.Caption = "isAssociated";
            gridColumn.FieldName = isAssociatedColumnName;
            gridColumn.Name = isAssociatedColumnName;
            this.gridViewList.Columns.Add(gridColumn);

        }
        private void AddUploadHideColumns(List<BusinessColumnInfo> columnInfos)
        {
            try
            {
                foreach (BusinessColumnInfo bci in columnInfos)
                {
                    if (!DictUploadColumnType.ContainsKey(bci.FieldName)) continue;

                    UploadColumnType uct = DictUploadColumnType[bci.FieldName];
                    if (uct != null)
                    {
                        DevExpress.XtraGrid.Columns.GridColumn gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();

                        gridColumn.OptionsColumn.AllowEdit = false;
                        gridColumn.Visible = false;
                        gridColumn.VisibleIndex = -1;
                        gridColumn.SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                        gridColumn.Width = 0;

                        gridColumn.Caption = bci.FieldName + CopyPath;
                        gridColumn.FieldName = gridColumn.Caption;
                        gridColumn.Name = gridColumn.Caption;
                        this.gridViewList.Columns.Add(gridColumn);
                    }

                }
            }
            catch (System.Exception ex)
            {

            }

        }
        /// <summary>
        /// 按照业务号降序排列
        /// </summary>
        private void AddSortColumnInfos()
        {

            GridColumn column = this.gridViewList.Columns.ColumnByFieldName(isAssociatedColumnName);
            GridColumnSortInfo sortInfo = new GridColumnSortInfo(column, DevExpress.Data.ColumnSortOrder.Descending);
            this.gridViewList.SortInfo.Add(sortInfo);
            column = this.gridViewList.Columns.ColumnByFieldName("NO");
            sortInfo = new GridColumnSortInfo(column, DevExpress.Data.ColumnSortOrder.Descending);
            this.gridViewList.SortInfo.Add(sortInfo);

        }

        private void InnerCreateColumn(BusinessColumnInfo columnInfo, CustomColumnInfo columnCustomInfo)
        {
            var gridColumn = new GridColumn();

            gridColumn.OptionsColumn.AllowEdit = columnInfo.Editable;
            // gridColumn.OptionsColumn.ReadOnly = !columnInfo.Editable;
            if (columnCustomInfo != null)
            {
                gridColumn.Visible = columnCustomInfo.Visible;
                gridColumn.VisibleIndex = columnCustomInfo.VisibleIndex;
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
            columnEdit.ReadOnly = !gridColumn.OptionsColumn.AllowEdit;
            this.gridControlList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { columnEdit });

            gridColumn.ColumnEdit = columnEdit;

            this.gridViewList.Columns.Add(gridColumn);

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
        /// 向数据库返回的表结构添加自定义列
        /// </summary>
        /// <param name="dt"></param>
        public virtual void AddCustomColumn(DataTable dt)
        {
            if (this.NeedBindData)
            {
                DataColumn column = new DataColumn("IsAssociated", typeof(int));
                column.DefaultValue = 0;
                dt.Columns.Add(column);
            }
            foreach (string key in dictCopyColumns.Keys)
            {
                DataColumn column = new DataColumn(key + CopyPath, typeof(string));
                column.DefaultValue = "";
                dt.Columns.Add(column);
            }
        }

        /// <summary>
        /// 重置邮件与业务关联信息缓存
        /// </summary>
        public void ResetMessageRelationRecord()
        {
            this.dicMessageRelation.Clear();
        }
        public void DynamicBindData(ListFormType? gridType)
        {
            if (!gridType.HasValue)
            {
                return;
            }
            if (AbstractBussinessPartOperation != null)
            {
                AbstractBussinessPartOperation.QueryAndBindData(false);
            }
            // QueryAndBindData(this.CurrentMessage, false);
        }

        /// <summary>
        /// 改变相关业务的样式
        /// </summary>
        /// <param name="relation"></param>
        /// <param name="message"></param>
        public void ChangeBusinessStyle(OperationMessageRelation relation, ICP.Message.ServiceInterface.Message message)
        {
            ChangeBusinessStyleDelegate changeDelegate = new ChangeBusinessStyleDelegate(InnerChangeBusinessStyle);
            this.Invoke(changeDelegate, relation, message);
        }
        private void InnerChangeBusinessStyle(OperationMessageRelation relation, ICP.Message.ServiceInterface.Message message)
        {
            string opNO = relation.OperationNo;
            this.dicMessageRelation.Clear();
            this.dicMessageRelation.Add(message.MessageId, opNO);
            SetBusinessStyleFormationCondition(opNO);
            string expression = string.Format("NO='{0}'", opNO);
            this.CurrentMessageRelation = relation;
            this.CurrentMessage = message;
            ChangeDataRowPosition(expression);
        }



        public void ResetQueryRecord()
        {
            this.previousQueryNo = string.Empty;
            this.previousMatchRows = null;
            this.previousSelectRowIndex = -1;
        }


        void gridViewList_ShowGridMenu(object sender, DevExpress.XtraGrid.Views.Grid.GridMenuEventArgs e)
        {
            DataRow row = this.gridViewList.GetFocusedDataRow();
            if (row == null)
            {
                e.Allow = false;
                return;
            }
            if (contextMenuCommonItemList == null || contextMenuCommonItemList.Count <= 0)
            {
                contextMenuCommonItemList = GetContextMenuItems(row);

            }
            else
            {
                List<ToolStripItem> existsBusinessItems = GetBusinessItems();
                foreach (ToolStripItem item in existsBusinessItems)
                {
                    this.contextMenuStrip.Items.Remove(item);
                }

            }

            List<ContextMenuItemInfo> businessRelatedItems = GetBusinessRelatedContextMenuItems(row);

            CreateContextMenuStripItems(businessRelatedItems);

            ControlContextMenuStripEnable(row);

        }

        /// <summary>
        /// 控制行右键菜单的可用性
        /// </summary>
        /// <param name="dr"></param>
        public void ControlContextMenuStripEnable(DataRow dr)
        {
            try
            {
                int state = int.Parse(dr["State"].ToString());
                bool isvalid = bool.Parse(dr["IsValid"].ToString());
                //是否有效
                if (!isvalid)
                {
                    SetToolStripItemEnable(CancelName, false);
                    SetToolStripItemEnable(ResumeName, true);
                    SetToolStripItemEnable(CopyName, false);
                    SetToolStripItemEnable(EditName, false);
                }
                else
                {
                    SetToolStripItemEnable(CancelName, true);
                    SetToolStripItemEnable(ResumeName, false);
                    SetToolStripItemEnable(CopyName, true);
                    SetToolStripItemEnable(EditName, true);
                }
            }
            catch (System.Exception ex)
            {

            }
        }
        /// <summary>
        /// 构建右键菜单列表，包括业务通用菜单和根据当前行数据构造的业务菜单
        /// </summary>
        /// <param name="items"></param>
        private void CreateContextMenuStripItems(List<ContextMenuItemInfo> items)
        {
            if (!isCommonContextMenuItemsExists)
            {
                CreateCommonContextMenuStripItems();
                isCommonContextMenuItemsExists = true;
            }
            CreateBusinessContextMenuStripItems(items);
        }
        /// <summary>
        /// 构造当前行业务菜单
        /// </summary>
        /// <param name="items"></param>
        private void CreateBusinessContextMenuStripItems(List<ContextMenuItemInfo> items)
        {
            InnerAddToolStripItems(items, true);
        }

        private void CreateCommonContextMenuStripItems()
        {
            if (contextMenuCommonItemList == null || contextMenuCommonItemList.Count <= 0)
                return;
            InnerAddToolStripItems(contextMenuCommonItemList, false);

        }
        private void InnerAddToolStripItems(List<ContextMenuItemInfo> itemInfos, bool isBusiness)
        {
            foreach (string key in dictCopyColumns.Keys)
            {
                WorkItem.UIExtensionSites.UnregisterSite(key);
            }

            foreach (ContextMenuItemInfo item in itemInfos)
            {
                ToolStripItem toolStripItem = ToolStripItemFactory.GetToolStripItem(item);


                if (!string.IsNullOrEmpty(item.Name))
                {

                    // WorkItem.Commands[item.Name].AddInvoker(toolStripItem, "Click");

                    toolStripItem.Click += (sender, e) =>
                    {
                        WorkItem.RootWorkItem.State["CurrentBaseBusinessPart"] = this;
                        CurrentTag = ((ToolStripItem)sender).Tag;
                        WorkItem.Commands[((ToolStripItem)sender).Name].Execute();

                    };
                }
                if (!isBusiness)
                {

                    WorkItem.UIExtensionSites[item.Site].Add(toolStripItem);
                }
                else
                {
                    toolStripItem.Tag = item.BusinessNo;
                    //int index = WorkItem.UIExtensionSites[item.Site].Count;
                    WorkItem.UIExtensionSites[item.Site].Insert(0, toolStripItem);
                }

                if (!string.IsNullOrEmpty(item.RegisterSite) && !WorkItem.UIExtensionSites.Contains(item.RegisterSite))
                {
                    WorkItem.UIExtensionSites.RegisterSite(item.RegisterSite, toolStripItem);
                }
            }
        }

        private bool isColumnSettingChanged = false;
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
        /// <summary>
        /// 返回业务上下文子菜单项所关联的业务号（比如SO,MBL No.,HBL NO.等)
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        protected string GetContextMenuItemBusinessRelatedNo(object sender, bool isThrowException)
        {
            Command command = sender as Command;
            if (command == null)
                return string.Empty;

            List<ToolStripItem> items = command.FindAdapters<ToolStripItemCommandAdapter>().Last().Invokers.Keys.ToList();
            if (items == null || items.Count <= 0)
                return string.Empty;
            ToolStripItem item = items.Last();
            if (item == null || item.Tag == null)
            {
                if (!isThrowException)
                    return string.Empty;
                else
                {
                    throw new NullReferenceException();
                }
            }
            return item.Tag as string;
        }
        /// <summary>
        /// 获取针对具体业务子面板的用户自定义列信息
        /// </summary>
        /// <returns></returns>
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
        private void ProcessBarItem(List<BarItem> items, BarItem item, string subGridType)
        {
            OperationToolbarCommand command = item.Tag == null ? null : (OperationToolbarCommand)item.Tag;
            if (command != null && command.Tag == subGridType)
            {
                if (!string.IsNullOrEmpty(command.Name))
                {
                    WorkItem.Commands[command.Name].RemoveInvoker(item, "ItemClick");
                    WorkItem.Commands[command.Name].Dispose();
                }
                if (!string.IsNullOrEmpty(command.RegisterSite))
                {
                    WorkItem.UIExtensionSites.UnregisterSite(command.RegisterSite);
                }
                items.Add(item);

            }


        }
        public void RemoveBusinessToobarItems(ListFormType? gridType)
        {
            if (gridType == null)
                return;
            string subGridType = gridType.ToString();

            List<BarItem> items = new List<BarItem>();
            BarItems barItems = this.barManager.Items;
            foreach (BarItem item in barItems)
            {
                ProcessBarItem(items, item, subGridType);
            }
            if (items.Count <= 0)
                return;
            foreach (BarItem item in items)
            {

                this.barManager.Items.Remove(item);
            }


        }
        public void AddBusinessToobarItems(ListFormType? gridType)
        {
            if (gridType == null)
                return;
            List<OperationToolbarCommand> businessCommands = ToolbarTemplateLoader.Current[gridType.ToString()];

            if (businessCommands == null || businessCommands.Count <= 0)
                return;
            BuildToobar(businessCommands);


        }
        private BarEditItem barEditItem = null;
        private BarEditItem GetBarEditSearchItem()
        {
            if (barEditItem != null)
                return barEditItem;
            string businessPartToolbarUISiteName = this.SourceType.ToString() + toolbarUISiteName;
            BarEditItem item = this.WorkItem.UIExtensionSites[businessPartToolbarUISiteName].First(barItem => ((BarItem)barItem).Name == "txtQuery") as BarEditItem;
            barEditItem = item;
            return item;
        }
        public string QueryNo
        {
            get
            {
                BarEditItem item = GetBarEditSearchItem();
                if (item.EditValue == null)
                    return string.Empty;
                else
                    return item.EditValue.ToString().Trim();
            }

        }
        public DataTable GetChangedRow()
        {
            DataTable dt = (this.bindingSource.DataSource as DataTable).GetChanges(DataRowState.Modified);
            return dt;
        }

        /// <summary>
        /// 重新绑定数据源
        /// </summary>
        protected void ResetBinding()
        {
            this.bindingSource.ResetBindings(false);
        }
        public void CancelEdit()
        {
            (this.bindingSource.DataSource as DataTable).RejectChanges();
            this.bindingSource.CancelEdit();
            this.bindingSource.ResetBindings(false);
        }
        public void AcceptChanges()
        {
            (this.bindingSource.DataSource as DataTable).AcceptChanges();
            this.bindingSource.EndEdit();
            this.bindingSource.ResetBindings(false);
        }



        protected virtual Dictionary<string, object> InsertInitValues(Dictionary<string, object> dic)
        {
            return dic;
        }


        public int globalId = 1;

        public List<OperationToolbarCommand> GetToolbarCommands()
        {
            return ToolbarTemplateLoader.Current[this.TemplateCode];
        }

        public List<BusinessColumnInfo> GetColumnInfos()
        {

            return ColumnTemplateLoader.Current[this.TemplateCode];
        }

        public List<ContextMenuItemInfo> GetContextMenuItems(DataRow row)
        {
            SectionKey key = new SectionKey { SectionCode = this.TemplateCode, Type = this.BusinessType };

            return ContextMenuFileLoader.Current[key];
        }

        //public List<string> GetQueryColumnNames()
        //{
        //    return QueryColumnLoader.GetQueryColumns(TemplateCode);
        //}



        /// <summary>
        /// 重写基类方法，添加动态生成删除附件上下文菜单项数据
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public List<ContextMenuItemInfo> GetBusinessRelatedContextMenuItems(DataRow row)
        {
            List<ContextMenuItemInfo> menuItems = new List<ContextMenuItemInfo>();

            List<ContextMenuItemInfo> items = new List<ContextMenuItemInfo>();

            foreach (string key in dictCopyColumns.Keys)
            {
                //加添上传附件列菜单
                items.Add(InnerCreateItemInfo((globalId++).ToString(), dictCopyColumns[key], string.Empty, "TaskCenterContextMenu", key, string.Empty, string.Empty));


                //加添删除附件菜单

                string copyNames = row.Field<string>(key);
                if (string.IsNullOrEmpty(copyNames))
                {
                    //加添上传附件菜单
                    items.Add(InnerCreateItemInfo((globalId++).ToString(), IsEnglish ? "Add Attachment" : "添加附件", Command_Add_Attachment, key, string.Empty, string.Empty, key));
                    continue;
                }
                List<string> listCopyNames = copyNames.Split(normalSeparator.ToCharArray()).Where(copyName => !string.IsNullOrEmpty(copyName)).ToList();

                string formatString = IsEnglish ? "Delete Copy:{0}" : "删除附件:{0}";

                List<string> captions = (from name in listCopyNames
                                         select string.Format(formatString, name)).ToList();

                captions.Reverse();
                foreach (string caption in captions)
                {
                    int index = caption.IndexOf(":") + 1;
                    string attachmentRelatedId = GetAttachmentNameOrLocalFullPath(row, key, caption.Substring(index));
                    //string attachmentRelatedId = GetCopyFilePath(row, key, caption.Substring(index));

                    items.Add(InnerCreateItemInfo((globalId++).ToString(), caption, Command_Delete_Attachment, key, string.Empty, string.Empty, attachmentRelatedId + "@" + key));
                }

                //加添上传附件菜单
                items.Add(InnerCreateItemInfo((globalId++).ToString(), IsEnglish ? "Add Attachment" : "添加附件", Command_Add_Attachment, key, string.Empty, string.Empty, key));

            }
            return menuItems.Concat(items).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="attachmentName"></param>
        /// <returns></returns>
        private string GetAttachmentNameOrLocalFullPath(DataRow row, string attachmentName)
        {
            DataRow[] copyRows = dtClone.Select(string.Format("ID='{0}' and {1} like '%{2}%'", row.Field<Guid>("ID"), GetCopyNameColumnName(), attachmentName));
            if (copyRows != null && copyRows.Length > 0)
            {
                return attachmentName;
            }


            //string attachmentIds = row.Field<string>(copyInfoColumnName);
            //if (string.IsNullOrEmpty(attachmentIds))
            //{
            //    return GetCopyFilePath(row, attachmentName);

            //}
            //List<string> ids = attachmentIds.Split('|').ToList();
            //string id = (from item in ids
            //             where item.Contains(attachmentName)
            //             select item).FirstOrDefault();
            //if (!string.IsNullOrEmpty(id))
            //    return id;
            return GetCopyFilePath(row, attachmentName);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="attachmentName"></param>
        /// <returns></returns>
        private string GetAttachmentNameOrLocalFullPath(DataRow row, string fieldName, string attachmentName)
        {
            string result = GetCopyFilePath(row, fieldName, attachmentName);
            if (result != null)
            {
                return result;
            }

            DataRow[] copyRows = CurrentDataSource.Select(string.Format("ID='{0}' and {1} like '%{2}%'", row.Field<Guid>("ID"), fieldName, attachmentName));
            if (copyRows != null && copyRows.Length > 0)
            {
                return attachmentName;
            }
            return "";


            //string attachmentIds = row.Field<string>(copyInfoColumnName);
            //if (string.IsNullOrEmpty(attachmentIds))
            //{
            //    return GetCopyFilePath(row, attachmentName);

            //}
            //List<string> ids = attachmentIds.Split('|').ToList();
            //string id = (from item in ids
            //             where item.Contains(attachmentName)
            //             select item).FirstOrDefault();
            //if (!string.IsNullOrEmpty(id))
            //    return id;


        }

        private string GetCopyFilePath(DataRow row, string attachmentName)
        {
            string existsCopyPaths = row.Field<string>(copyPathColumnName);
            List<string> temp = existsCopyPaths.Split(normalSeparator.ToCharArray()).ToList();
            if (temp != null && temp.Count > 0)
            {
                string copyPath = (from item in temp
                                   where item.Contains(attachmentName)
                                   select item).First();
                return copyPath;
            }
            return null;
        }

        private string GetCopyFilePath(DataRow row, string fieldName, string attachmentName)
        {
            try
            {
                string existsCopyPaths = gridViewList.GetRowCellValue(FocusedRowHandle, fieldName + CopyPath).ToString();
                List<string> temp = existsCopyPaths.Split(normalSeparator.ToCharArray()).ToList();
                string copyPath = (from item in temp
                                   where item.Contains(attachmentName)
                                   select item).First();
                return copyPath;
            }
            catch (System.Exception ex)
            {
            }
            return null;
        }

        #region 业务上下文菜单构造
        public List<ContextMenuItemInfo> GetOEBusinessContextMenuItems(Dictionary<string, string> dicNoPair)
        {
            string mblTypeName = "MBL";
            string hblTypeName = "HBL";
            //string soTypeName = "SO";
            List<string> mblNos = GetConcreateNos(mblTypeName, dicNoPair);
            List<string> hblNos = GetConcreateNos(hblTypeName, dicNoPair);
            // List<string> soNos = GetConcreateNos(soTypeName, dicNoPair);
            List<ContextMenuItemInfo> mblItems = GetItemInfos(mblNos, mblTypeName);
            List<ContextMenuItemInfo> hblItems = GetItemInfos(hblNos, hblTypeName);
            // List<ContextMenuItemInfo> soItems = GetItemInfos(soNos, soTypeName);
            List<ContextMenuItemInfo> list = hblItems.Concat(mblItems).ToList();

            if (!IsCurrentBusinessRelated())
            {
                string text = IsEnglish ? "Associate Current Shipment" : "关联当前业务";
                ContextMenuItemInfo item = InnerCreateItemInfo("MessageRelation", text, string.Format("Command_{0}EmailOperationMessageRelation", this.SourceType.ToString()), "EmailCenterContextMenu", "", "", string.Empty);
                list.Add(item);
            }
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
            string text = (IsEnglish ? string.Format("Open {0}", typeName) : string.Format("打开{0}", typeName)) + ":" + no;
            string name = string.Format("Command_EmailCenter_{0}_Open{1}", this.ContextMenuStripUISiteName, typeName);
            string site = this.ContextMenuStripUISiteName;
            string id = (globalId++).ToString();
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
            itemInfo.Type = ContexuMenuItemType.MenuItem;
            itemInfo.BusinessNo = businessNo;

            return itemInfo;
        }

        public ContextMenuItemInfo InnerCreateItemInfo(string id, string text, string name, string site, string registerSite, string imageName, string businessNo, string tag)
        {
            ContextMenuItemInfo itemInfo = new ContextMenuItemInfo();
            itemInfo.Id = id;
            itemInfo.Name = name;
            itemInfo.ImageName = imageName;
            itemInfo.RegisterSite = registerSite;
            itemInfo.Site = site;
            itemInfo.Text = text;
            itemInfo.Type = ContexuMenuItemType.MenuItem;
            itemInfo.BusinessNo = businessNo;
            itemInfo.Tag = tag;

            return itemInfo;
        }

        private List<string> GetConcreateNos(string key, Dictionary<string, string> pairs)
        {
            List<KeyValuePair<string, string>> list = pairs.Where(pair => pair.Key.Equals(key, StringComparison.CurrentCultureIgnoreCase)).ToList();
            return (from pair in list select pair.Value).ToList();
        }
        #endregion
        public EditPartShowCriteria ShowCriteria
        {
            get
            {
                return new EditPartShowCriteria { OperationNo = this.OperationNo, BillNo = this.ID };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public OperationType GetOperationType()
        {
            return OperationType.OceanExport;
        }
        private DialogResult PromptChangeRelation()
        {
            string messageId = this.CurrentMessage.MessageId;
            string tip;
            if (dicMessageRelation.ContainsKey(messageId))
            {
                string oldOperationNo = this.dicMessageRelation[messageId];
                tip = IsEnglish ? string.Format("The current mail is associated to {0},", oldOperationNo) + Environment.NewLine + string.Format("Do you want to re-associate it to {0}?", this.OperationNo) : string.Format("当前邮件已关联到业务:{0},", oldOperationNo) + Environment.NewLine + string.Format("你想重新关联到业务:{0}吗?", this.OperationNo);
            }
            else
            {
                tip = IsEnglish ? string.Format("Do you want to associate the current mail to {0}?", this.OperationNo) : string.Format("你想关联当前邮件到当前业务:{0}", this.OperationNo);
            }

            return XtraMessageBox.Show(tip, "Tips", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
        }


        #region 命令处理

        private void ResetIsAssociatedColumnValue(string previousOperationNo)
        {
            if (!string.IsNullOrEmpty(previousOperationNo))
            {
                DataRow row = GetDataRowByOperationNo(previousOperationNo);
                if (row != null)
                {
                    row[isAssociatedColumnName] = 0;
                }
            }
        }
        private void SaveRelation(OperationMessageRelation relation)
        {

            WaitCallback callback = (data) =>
            {
                try
                {
                    OperationMessageRelation parameter = data as OperationMessageRelation;
                    //SingleResult result = OperationMessageRelationService.SaveOperationMailMessage(relation);
                    //this.CurrentMessageRelation.ID = result.GetValue<Guid>("ID");
                    //this.CurrentMessageRelation.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                }
                catch (Exception ex)
                {
                    this.dicMessageRelation.Clear();
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
                }
            };
            ThreadPool.QueueUserWorkItem(callback, relation);

        }
        /// <summary>
        /// 处理异常捕获
        /// </summary>
        /// <param name="ex"></param>
        public void TrapException(Exception ex)
        {
            LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex);
        }


        public Dictionary<string, object> CreateBusinessParameter(ICP.Common.ServiceInterface.DataObjects.ActionType actionType)
        {
            BusinessOperationParameter businessOperation = new BusinessOperationParameter();
            businessOperation.TemplateCode = TemplateCode;
            businessOperation.Message = this.currentMessage;
            //businessOperation.SenderEmailAddress = 
            if (actionType == ICP.Common.ServiceInterface.DataObjects.ActionType.Edit)
            {
                businessOperation.Context = new BusinessOperationContext();
                businessOperation.Context.OpeationNO = this.OperationNo;
                businessOperation.Context.OperationID = this.ID;
            }
            else
            {
                businessOperation.Context = new BusinessOperationContext();
            }
            businessOperation.ActionType = actionType;
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("businessOperationParameter", businessOperation);
            return dic;
        }

        private void ExtractBillProperties<T>(DataRow row, object billInfo, ListDictionary<string, string> dicPropertyNames)
        {
            List<PropertyInfo> sourceProperties = typeof(T).GetProperties().Where(p => dicPropertyNames.Values.Contains(p.Name)).ToList();
            foreach (string columnName in dicPropertyNames.Keys)
            {
                List<PropertyInfo> properties = sourceProperties.Where(p => dicPropertyNames[columnName].Contains(p.Name)).ToList();
                row[columnName] = FormatPropertyValues(properties, billInfo);
            }

        }

        private object FormatPropertyValues(List<PropertyInfo> properties, object billInfo)
        {
            if (properties.Count <= 0)
                return string.Empty;
            if (properties.Count < 2)
            {
                return properties[0].GetValue(billInfo, null);
            }
            else
            {
                List<string> result = (from item in properties
                                       select (item.Name + ":" + item.GetValue(billInfo, null).ToString())).ToList();
                return result.Aggregate((a, b) => a + Environment.NewLine + b);
            }
        }
        private delegate void EditPartSavedDelegate(OceanBookingInfo relation);

        #region 面板回调数据刷新
        /// <summary>
        /// 邮件中心调用FCM业务服务后，回调刷新业务面板数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnBusinessOperationCompleted(object sender, CommonEventArgs<BusinessOperationParameter> e)
        {

            BusinessOperationParameter parameter = e.Data;
            if (parameter == null) return;
            if (parameter.TemplateCode == TemplateCode || this.CurrentMessage.MessageId != parameter.Message.MessageId)
                return;
            //未知面板进行新增操作后，当前邮件的发件人标识为客户，并显示客户面板
            if (parameter.TemplateCode == "MailLink4Unknown")
            {
                ClientBusinessContactService.Save(parameter.SenderEmailAddress, EmailSourceType.Customer);
                this.WorkItem.Commands[Constants.ShowBusinessPartCommand].Execute();
                return;
            }
            OperationType operationType = parameter.Context.OperationType;

            BusinessOperationContext context = parameter.Context;
            FormType bltype = parameter.Context.FormType;
            Guid operationId = parameter.Context.OperationID;
            string operationNo = parameter.Context.OpeationNO;
            List<string> FilesName = parameter.FilesName;

            object[] data = parameter.Data;
            object formInfo = data[0];
            DataRow targetRow = null;
            if (parameter.ActionType == ICP.Common.ServiceInterface.DataObjects.ActionType.Create)
            {
                targetRow = CurrentDataSource.NewRow();
            }
            else if (parameter.ActionType == ICP.Common.ServiceInterface.DataObjects.ActionType.Edit)
            {
                targetRow = CurrentDataSource.Select(string.Format("ID='{0}'", context.OperationID)).First();
            }
            IGetDescriptionCommand getDescriptionCommand = GetBillInfoFactory.GetDescriptionCommand(data[0]);
            IGetRefNoCommand getRefNoCommand = GetBillInfoFactory.GetRefNoCommand(data[0]);

            #region 客户面板数据刷新
            if (this.TemplateCode == "MailLink4Customer")
            {
                if (context.FormType == FormType.ShippingOrder)
                {
                    OceanBookingInfo billInfo = parameter.Data[0] as OceanBookingInfo;
                    targetRow["ID"] = operationId;
                    targetRow["NO"] = operationNo;
                    targetRow["IsValid"] = billInfo.IsValid;
                    targetRow["State"] = billInfo.State;
                    if (billInfo.UpdateDate == null) targetRow["UpdateDate"] = DBNull.Value;
                    else targetRow["UpdateDate"] = billInfo.UpdateDate;

                    targetRow["RefNO"] = getRefNoCommand.Get(billInfo);
                    targetRow["Description"] = getDescriptionCommand.Get(billInfo);
                }
                else if (context.FormType == FormType.MBL)
                {
                    OceanMBLInfo mblInfo = parameter.Data[0] as OceanMBLInfo;
                    targetRow["ID"] = operationId;
                    targetRow["NO"] = operationNo;
                    targetRow["IsValid"] = mblInfo.IsValid;
                    targetRow["State"] = mblInfo.State;
                    if (mblInfo.UpdateDate == null) targetRow["UpdateDate"] = DBNull.Value;
                    else targetRow["UpdateDate"] = mblInfo.UpdateDate;
                    targetRow["RefNO"] = getRefNoCommand.Get(mblInfo);
                    targetRow["Description"] = getDescriptionCommand.Get(mblInfo);
                    //ListDictionary<string, string> dicProperties = new ListDictionary<string, string>();

                    //dicProperties.Add("ID", "ID");
                    //dicProperties.Add("NO", "NO");
                    //dicProperties.Add("IsValid", "IsValid");
                    //dicProperties.Add("State", "State");
                    //dicProperties["RefNO"] = new List<string>{"SONO","ContainerNos",};
                    //dicProperties["Description"] = new List<string> { "POLName", "PODName", "ETD", "ETA" };
                    //dicProperties.Add("UpdateDate", "UpdateDate");
                }
                else if (context.FormType == FormType.HBL)
                {
                    OceanHBLInfo hblInfo = parameter.Data[0] as OceanHBLInfo;
                    targetRow["ID"] = operationId;
                    targetRow["NO"] = operationNo;
                    targetRow["IsValid"] = hblInfo.IsValid;
                    targetRow["State"] = hblInfo.State;
                    if (hblInfo.UpdateDate == null) targetRow["UpdateDate"] = DBNull.Value;
                    else targetRow["UpdateDate"] = hblInfo.UpdateDate;
                    targetRow["RefNO"] = getRefNoCommand.Get(hblInfo);
                    targetRow["Description"] = getDescriptionCommand.Get(hblInfo);
                }
                else if (context.FormType == FormType.Customs)
                {
                    OceanCustoms customes = parameter.Data[0] as OceanCustoms;
                    targetRow["ID"] = operationId;
                    targetRow["NO"] = operationNo;
                    targetRow["IsValid"] = true;
                    targetRow["State"] = true;
                    if (customes.UpdateDate == null) targetRow["UpdateDate"] = DBNull.Value;
                    else targetRow["UpdateDate"] = customes.UpdateDate;
                    targetRow["RefNO"] = string.Format("SO:{0}", operationNo);
                    targetRow["Description"] = string.Empty;
                }
                else if (context.FormType == FormType.Truck)
                {
                    OceanTruckInfo truckInfo = parameter.Data[0] as OceanTruckInfo;
                    targetRow["ID"] = operationId;
                    targetRow["NO"] = operationNo;
                    targetRow["IsValid"] = true;
                    targetRow["State"] = true;
                    if (truckInfo.UpdateDate == null) targetRow["UpdateDate"] = DBNull.Value;
                    else targetRow["UpdateDate"] = truckInfo.UpdateDate;
                    targetRow["RefNO"] = string.Format("SO:{0}", truckInfo.ShippingOrderNo);
                    targetRow["Description"] = string.Empty;
                }
            }
            #endregion

            #region   承运人SO面板数据刷新
            else if (this.TemplateCode == "MailLink4CarrierSO")
            {
                // SOCopy,SONO,Vessel,Voyage,ContainerDesc,BLNO,Carrier,POL,POD,ID,NO,UpdateDate,IsValid,RefNO
                string strSOCopy = GetDocumentsName(FilesName);

                if (context.FormType == FormType.ShippingOrder)
                {
                    OceanBookingInfo billInfo = parameter.Data[0] as OceanBookingInfo;

                    targetRow["SOCopy"] = strSOCopy;
                    targetRow["SONO"] = billInfo.No;
                    targetRow["Vessel"] = billInfo.VesselVoyage;
                    targetRow["Voyage"] = billInfo.VoyageName;
                    targetRow["ContainerDesc"] = billInfo.ContainerDescription;

                    targetRow["BLNO"] = string.Empty;
                    targetRow["Carrier"] = billInfo.CarrierName;
                    targetRow["POL"] = billInfo.POLName;
                    targetRow["POD"] = billInfo.PODName;
                    targetRow["NO"] = operationNo;
                    targetRow["ID"] = operationId;
                    if (billInfo.UpdateDate == null) targetRow["UpdateDate"] = DBNull.Value;
                    else targetRow["UpdateDate"] = billInfo.UpdateDate;

                    targetRow["IsValid"] = true;
                    targetRow["RefNO"] = getRefNoCommand.Get(billInfo);
                }
                else if (context.FormType == FormType.MBL)
                {
                    OceanMBLInfo mblInfo = parameter.Data[0] as OceanMBLInfo;
                    targetRow["SOCopy"] = strSOCopy;
                    targetRow["SONO"] = mblInfo.SONO;
                    targetRow["Vessel"] = mblInfo.VesselVoyage;
                    targetRow["Voyage"] = mblInfo.VesselVoyage;
                    targetRow["ContainerDesc"] = mblInfo.ContainerQtyDescription;

                    targetRow["BLNO"] = string.Empty;
                    targetRow["Carrier"] = mblInfo.CarrierName;

                    targetRow["POL"] = mblInfo.POLName;
                    targetRow["POD"] = mblInfo.PODName;
                    targetRow["NO"] = operationNo;
                    targetRow["ID"] = operationId;
                    if (mblInfo.UpdateDate == null) targetRow["UpdateDate"] = DBNull.Value;
                    else targetRow["UpdateDate"] = mblInfo.UpdateDate;

                    targetRow["IsValid"] = true;
                    targetRow["RefNO"] = getRefNoCommand.Get(mblInfo);
                }
                else if (context.FormType == FormType.HBL)
                {
                    OceanHBLInfo hblInfo = parameter.Data[0] as OceanHBLInfo;
                    targetRow["SOCopy"] = strSOCopy;
                    targetRow["SONO"] = hblInfo.SONO;
                    targetRow["Vessel"] = hblInfo.VesselVoyage;
                    targetRow["Voyage"] = hblInfo.VesselVoyage;
                    targetRow["ContainerDesc"] = hblInfo.ContainerQtyDescription;

                    targetRow["BLNO"] = string.Empty;
                    targetRow["Carrier"] = hblInfo.CarrierName;

                    targetRow["POL"] = hblInfo.POLName;
                    targetRow["POD"] = hblInfo.PODName;
                    targetRow["NO"] = operationNo;
                    targetRow["ID"] = operationId;
                    if (hblInfo.UpdateDate == null) targetRow["UpdateDate"] = DBNull.Value;
                    else targetRow["UpdateDate"] = hblInfo.UpdateDate;
                    targetRow["IsValid"] = true;
                    targetRow["RefNO"] = getRefNoCommand.Get(hblInfo);
                }
                else if (context.FormType == FormType.Customs)
                {
                    OceanCustoms customes = parameter.Data[0] as OceanCustoms;
                    targetRow["SOCopy"] = strSOCopy;
                    targetRow["SONO"] = string.Empty;
                    targetRow["Vessel"] = string.Empty;
                    targetRow["Voyage"] = string.Empty;
                    targetRow["ContainerDesc"] = string.Empty;

                    targetRow["BLNO"] = string.Empty;
                    targetRow["Carrier"] = string.Empty;

                    targetRow["POL"] = string.Empty;
                    targetRow["POD"] = string.Empty;
                    targetRow["NO"] = operationNo;
                    targetRow["ID"] = operationId;
                    if (customes.UpdateDate == null) targetRow["UpdateDate"] = DBNull.Value;
                    else targetRow["UpdateDate"] = customes.UpdateDate;

                    targetRow["IsValid"] = true;
                    targetRow["RefNO"] = string.Format("SO:{0}", operationNo);

                }
                else if (context.FormType == FormType.Truck)
                {
                    OceanTruckInfo truckInfo = parameter.Data[0] as OceanTruckInfo;
                    targetRow["SOCopy"] = strSOCopy;
                    targetRow["SONO"] = string.Empty;
                    targetRow["Vessel"] = truckInfo.VesselVoyage;
                    targetRow["Voyage"] = truckInfo.VesselVoyage;
                    targetRow["ContainerDesc"] = string.Empty;

                    targetRow["BLNO"] = string.Empty;
                    targetRow["Carrier"] = truckInfo.CarrierName;

                    targetRow["POL"] = string.Empty;
                    targetRow["POD"] = string.Empty;
                    targetRow["NO"] = operationNo;
                    targetRow["ID"] = operationId;
                    if (truckInfo.UpdateDate == null) targetRow["UpdateDate"] = DBNull.Value;
                    else targetRow["UpdateDate"] = truckInfo.UpdateDate;

                    targetRow["IsValid"] = true;
                    targetRow["RefNO"] = string.Format("SO:{0}", truckInfo.ShippingOrderNo);

                }
            }
            #endregion

            #region 承运人MBL面板数据刷新
            else if (this.TemplateCode == "MailLink4CarrierMBL")
            {
                //MBLCopy,APCopy,Vessel,Voyage,BLNO,ContainerDesc,Carrier,POL,POD,ID,NO,UpdateDate,IsValid,RefNO
                string strMBLCopy = GetDocumentsName(FilesName);

                if (context.FormType == FormType.ShippingOrder)
                {

                    OceanBookingInfo billInfo = parameter.Data[0] as OceanBookingInfo;
                    targetRow["MBLCopy"] = strMBLCopy;
                    targetRow["APCopy"] = string.Empty;
                    targetRow["Vessel"] = billInfo.VesselVoyage;
                    targetRow["Voyage"] = billInfo.VoyageName;
                    targetRow["ContainerDesc"] = billInfo.ContainerDescription;

                    targetRow["BLNO"] = string.Empty;
                    targetRow["Carrier"] = billInfo.CarrierName;

                    targetRow["POL"] = billInfo.POLName;
                    targetRow["POD"] = billInfo.PODName;
                    targetRow["NO"] = billInfo.No;
                    targetRow["ID"] = billInfo.ID;
                    if (billInfo.UpdateDate == null) targetRow["UpdateDate"] = DBNull.Value;
                    else targetRow["UpdateDate"] = billInfo.UpdateDate;

                    targetRow["IsValid"] = true;
                    targetRow["RefNO"] = getRefNoCommand.Get(billInfo);
                }
                else if (context.FormType == FormType.MBL)
                {
                    OceanMBLInfo mblInfo = parameter.Data[0] as OceanMBLInfo;
                    targetRow["MBLCopy"] = strMBLCopy;
                    targetRow["APCopy"] = string.Empty;
                    targetRow["Vessel"] = mblInfo.VesselVoyage;
                    targetRow["Voyage"] = mblInfo.VesselVoyage;
                    targetRow["ContainerDesc"] = mblInfo.ContainerQtyDescription;

                    targetRow["BLNO"] = string.Empty;
                    targetRow["Carrier"] = mblInfo.CarrierName;

                    targetRow["POL"] = mblInfo.POLName;
                    targetRow["POD"] = mblInfo.PODName;
                    targetRow["NO"] = mblInfo.No;
                    targetRow["ID"] = mblInfo.ID;
                    if (mblInfo.UpdateDate == null) targetRow["UpdateDate"] = DBNull.Value;
                    else targetRow["UpdateDate"] = mblInfo.UpdateDate;

                    targetRow["IsValid"] = true;
                    targetRow["RefNO"] = getRefNoCommand.Get(mblInfo);
                }
                else if (context.FormType == FormType.HBL)
                {
                    OceanHBLInfo hblInfo = parameter.Data[0] as OceanHBLInfo;
                    targetRow["MBLCopy"] = strMBLCopy;
                    targetRow["APCopy"] = string.Empty;
                    targetRow["Vessel"] = hblInfo.VesselVoyage;
                    targetRow["Voyage"] = hblInfo.VesselVoyage;
                    targetRow["ContainerDesc"] = hblInfo.ContainerQtyDescription;

                    targetRow["BLNO"] = string.Empty;
                    targetRow["Carrier"] = hblInfo.CarrierName;

                    targetRow["POL"] = hblInfo.POLName;
                    targetRow["POD"] = hblInfo.PODName;
                    targetRow["NO"] = hblInfo.No;
                    targetRow["ID"] = hblInfo.ID;
                    if (hblInfo.UpdateDate == null) targetRow["UpdateDate"] = DBNull.Value;
                    else targetRow["UpdateDate"] = hblInfo.UpdateDate;

                    targetRow["IsValid"] = true;
                    targetRow["RefNO"] = getRefNoCommand.Get(hblInfo);
                }
                else if (context.FormType == FormType.Customs)
                {
                    OceanCustoms customes = parameter.Data[0] as OceanCustoms;
                    targetRow["MBLCopy"] = strMBLCopy;
                    targetRow["APCopy"] = string.Empty;
                    targetRow["Vessel"] = string.Empty;
                    targetRow["Voyage"] = string.Empty;
                    targetRow["ContainerDesc"] = string.Empty;

                    targetRow["BLNO"] = string.Empty;
                    targetRow["Carrier"] = string.Empty;

                    targetRow["POL"] = string.Empty;
                    targetRow["POD"] = string.Empty;
                    targetRow["NO"] = operationNo;
                    targetRow["ID"] = operationId;
                    if (customes.UpdateDate == null) targetRow["UpdateDate"] = DBNull.Value;
                    else targetRow["UpdateDate"] = customes.UpdateDate;

                    targetRow["IsValid"] = true;
                    targetRow["RefNO"] = string.Empty;
                }
                else if (context.FormType == FormType.Truck)
                {
                    OceanTruckInfo truckInfo = parameter.Data[0] as OceanTruckInfo;
                    targetRow["MBLCopy"] = strMBLCopy;
                    targetRow["APCopy"] = string.Empty;
                    targetRow["Vessel"] = truckInfo.VesselVoyage;
                    targetRow["Voyage"] = truckInfo.VesselVoyage;
                    targetRow["ContainerDesc"] = string.Empty;

                    targetRow["BLNO"] = string.Empty;
                    targetRow["Carrier"] = truckInfo.CarrierName;

                    targetRow["POL"] = string.Empty;
                    targetRow["POD"] = string.Empty;
                    targetRow["NO"] = operationNo;
                    targetRow["ID"] = operationId;
                    if (truckInfo.UpdateDate == null) targetRow["UpdateDate"] = DBNull.Value;
                    else targetRow["UpdateDate"] = truckInfo.UpdateDate;

                    targetRow["IsValid"] = true;
                    targetRow["RefNO"] = string.Format("SO:{0}", truckInfo.ShippingOrderNo);
                }
            }
            #endregion

            #region 承运人AP面板数据刷新
            else if (this.TemplateCode == "MailLink4CarrierAP")
            {
                //APCopy,BLNO,Vessel,Voyage,ContainerDesc,Carrier,POL,POD,ID,NO,UpdateDate,IsValid,RefNO

                string strAPCopy = GetDocumentsName(FilesName);

                if (context.FormType == FormType.ShippingOrder)
                {
                    OceanBookingInfo billInfo = parameter.Data[0] as OceanBookingInfo;
                    targetRow["APCopy"] = strAPCopy;
                    targetRow["BLNO"] = billInfo.No;
                    targetRow["Vessel"] = billInfo.VesselVoyage;
                    targetRow["Voyage"] = billInfo.VoyageName;
                    targetRow["ContainerDesc"] = billInfo.ContainerDescription;

                    targetRow["BLNO"] = string.Empty;
                    targetRow["Carrier"] = billInfo.CarrierName;

                    targetRow["POL"] = billInfo.POLName;
                    targetRow["POD"] = billInfo.PODName;
                    targetRow["NO"] = operationNo;
                    targetRow["ID"] = operationId;
                    if (billInfo.UpdateDate == null) targetRow["UpdateDate"] = DBNull.Value;
                    else targetRow["UpdateDate"] = billInfo.UpdateDate;

                    targetRow["IsValid"] = true;
                    targetRow["RefNO"] = getRefNoCommand.Get(billInfo);
                }
                else if (context.FormType == FormType.MBL)
                {
                    OceanMBLInfo mblInfo = parameter.Data[0] as OceanMBLInfo;
                    targetRow["APCopy"] = strAPCopy;
                    targetRow["BLNO"] = mblInfo.No;
                    targetRow["Vessel"] = mblInfo.VesselVoyage;
                    targetRow["Voyage"] = mblInfo.VesselVoyage;
                    targetRow["ContainerDesc"] = mblInfo.ContainerQtyDescription;

                    targetRow["BLNO"] = string.Empty;
                    targetRow["Carrier"] = mblInfo.CarrierName;

                    targetRow["POL"] = mblInfo.POLName;
                    targetRow["POD"] = mblInfo.PODName;
                    targetRow["NO"] = operationNo;
                    targetRow["ID"] = operationId;
                    if (mblInfo.UpdateDate == null) targetRow["UpdateDate"] = DBNull.Value;
                    else targetRow["UpdateDate"] = mblInfo.UpdateDate;

                    targetRow["IsValid"] = true;
                    targetRow["RefNO"] = getRefNoCommand.Get(mblInfo);
                }
                else if (context.FormType == FormType.HBL)
                {
                    OceanHBLInfo hblInfo = parameter.Data[0] as OceanHBLInfo;
                    targetRow["APCopy"] = strAPCopy;
                    targetRow["BLNO"] = hblInfo.No;
                    targetRow["Vessel"] = hblInfo.VesselVoyage;
                    targetRow["Voyage"] = hblInfo.VesselVoyage;
                    targetRow["ContainerDesc"] = hblInfo.ContainerQtyDescription;

                    targetRow["BLNO"] = string.Empty;
                    targetRow["Carrier"] = hblInfo.CarrierName;

                    targetRow["POL"] = hblInfo.POLName;
                    targetRow["POD"] = hblInfo.PODName;
                    targetRow["NO"] = operationNo;
                    targetRow["ID"] = operationId;
                    if (hblInfo.UpdateDate == null) targetRow["UpdateDate"] = DBNull.Value;
                    else targetRow["UpdateDate"] = hblInfo.UpdateDate;

                    targetRow["IsValid"] = true;
                    targetRow["RefNO"] = getRefNoCommand.Get(hblInfo);
                }
                else if (context.FormType == FormType.Customs)
                {
                    OceanCustoms customes = parameter.Data[0] as OceanCustoms;
                    targetRow["APCopy"] = strAPCopy;
                    targetRow["BLNO"] = string.Empty;
                    targetRow["Vessel"] = string.Empty;
                    targetRow["Voyage"] = string.Empty;
                    targetRow["ContainerDesc"] = string.Empty;

                    targetRow["BLNO"] = string.Empty;
                    targetRow["Carrier"] = string.Empty;

                    targetRow["POL"] = string.Empty;
                    targetRow["POD"] = string.Empty;
                    targetRow["NO"] = operationNo;
                    targetRow["ID"] = operationId;
                    if (customes.UpdateDate == null) targetRow["UpdateDate"] = DBNull.Value;
                    else targetRow["UpdateDate"] = customes.UpdateDate;


                    targetRow["IsValid"] = true;
                    targetRow["RefNO"] = string.Empty;
                }
                else if (context.FormType == FormType.Truck)
                {
                    OceanTruckInfo truckInfo = parameter.Data[0] as OceanTruckInfo;
                    targetRow["APCopy"] = strAPCopy;
                    targetRow["BLNO"] = string.Empty;
                    targetRow["Vessel"] = truckInfo.VesselVoyage;
                    targetRow["Voyage"] = truckInfo.VesselVoyage;
                    targetRow["ContainerDesc"] = truckInfo.ContainerDescription;

                    targetRow["BLNO"] = string.Empty;
                    targetRow["Carrier"] = truckInfo.CarrierName;

                    targetRow["POL"] = string.Empty;
                    targetRow["POD"] = string.Empty;
                    targetRow["NO"] = operationNo;
                    targetRow["ID"] = operationId;
                    if (truckInfo.UpdateDate == null) targetRow["UpdateDate"] = DBNull.Value;
                    else targetRow["UpdateDate"] = truckInfo.UpdateDate;


                    targetRow["IsValid"] = true;
                    targetRow["RefNO"] = string.Format("SO:{0}", truckInfo.ShippingOrderNo);
                }

            }
            #endregion

            if (parameter.ActionType == ICP.Common.ServiceInterface.DataObjects.ActionType.Create)
            {
                CurrentDataSource.Rows.Add(targetRow);
            }

            ResetBinding();
            SetBusinessStyleFormationCondition(operationNo);
            //SaveMessage(parameter.Context, parameter.Message);



            //switch (operationType)
            //{
            //    case OperationType.AirExport:
            //        //AirBookingInfo airExportInfo = formInfo as AirBookingInfo;
            //       // EditPartSaved(airExportInfo.ID, operationType);
            //        HandleBusinessOperation<AirBookingInfo>(formInfo, "ID", operationType);
            //        break;
            //    case OperationType.AirImport:
            //        AirBookingInfo airImport = formInfo as AirBookingInfo;
            //        EditPartSaved(airImport.ID, operationType);
            //        break;
            //    case OperationType.OceanExport:
            //        if (bltype == FormType.HBL)
            //        {
            //            OceanHBLInfo hblinfo = formInfo as OceanHBLInfo;
            //            EditPartSaved(hblinfo.OceanBookingID, operationType);
            //            break;
            //        }
            //        else if (bltype == FormType.MBL)
            //        {
            //            OceanMBLInfo mblinfo = formInfo as OceanMBLInfo;
            //            EditPartSaved(mblinfo.OceanBookingID, operationType);
            //            break;
            //        }
            //        OceanBookingInfo oceanExportInfo = formInfo as OceanBookingInfo;
            //        EditPartSaved(oceanExportInfo.ID, operationType);
            //        break;
            //    case OperationType.OceanImport:
            //        OceanBookingInfo oceanImportInfo = formInfo as OceanBookingInfo;
            //        EditPartSaved(oceanImportInfo.ID, operationType);
            //        break;
            //    case OperationType.Other:
            //        OtherBusinessInfo otherInfo = formInfo as OtherBusinessInfo;

            //        EditPartSaved(otherInfo.ID, operationType);
            //        break;
            //}

        }

        /// <summary>
        /// 获取文档列表文件名集合
        /// </summary>
        /// <param name="FilesName"></param>
        /// <returns></returns>
        private string GetDocumentsName(List<string> FilesName)
        {
            if (AddedAttachments.Count != 0)
            {
                foreach (var item in AddedAttachments.Values)
                {
                    FilesName.Add(item);
                }
            }

            if (DeleteAttachments.Count != 0)
            {
                foreach (var item in DeleteAttachments.Values)
                {
                    if (FilesName.Contains(item))
                    {
                        FilesName.Remove(item);
                    }
                }
            }

            return FilesName.ToArray().Aggregate((a, n) => a + " " + n);
        }
        #endregion

        private void RefreshGridData()
        {

        }

        /// <summary>
        /// 保存消息
        /// </summary>
        /// <param name="context"></param>
        /// <param name="message"></param>
        public void SaveMessage(BusinessOperationContext context, Message.ServiceInterface.Message message)
        {
            OperationMessageRelation relation = new OperationMessageRelation();

            relation = OperationMessageRelationService.GetByMessageId(message.MessageId);

            if (relation != null)
            {
                DialogResult dlg = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Are you sure?" : string.Format("当前邮件已关联业务号{0}，是否确认重新关联业务?", relation.OperationNo), LocalData.IsEnglish ? "Confirm" : "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dlg != DialogResult.OK)
                {
                    return;
                }
            }

            CurrentMessage.UserProperties = new MessageUserPropertiesObject();
            CurrentMessage.UserProperties.OperationType = context.OperationType;
            CurrentMessage.UserProperties.OperationId = context.OperationID;
            currentMessage.UserProperties.FormType = context.FormType;
            currentMessage.UserProperties.FormId = context.FormId;

            WaitCallback callback = (data) =>
            {
                try
                {
                    Message.ServiceInterface.Message messageInfo = data as Message.ServiceInterface.Message;
                    ServiceClient.GetService<IMessageService>().Save(messageInfo);
                }
                catch (Exception ex)
                {
                    TrapException(ex);
                }

            };
            ThreadPool.QueueUserWorkItem(callback, message);


        }

        #region 工具栏提交动作的动画显示控制
        public void BeginShowAnimation()
        {
            this.picTipImage = new DevExpress.XtraEditors.PictureEdit();

            this.lblAnimationTip = new LabelControl();
            this.lblAnimationTip.Text = IsEnglish ? "Saving is in progress…" : "正在保存。。。";
            ((System.ComponentModel.ISupportInitialize)(this.picTipImage.Properties)).BeginInit();
            this.gridControlList.SuspendLayout();
            // 
            // picTipImage
            // 
            this.picTipImage.Dock = System.Windows.Forms.DockStyle.None;
            //this.picTipImage.EditValue = Properties.Resources.process2;


            this.picTipImage.Name = "picTipImage";
            this.picTipImage.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.picTipImage.Properties.Appearance.ForeColor = System.Drawing.Color.White;
            this.picTipImage.Properties.Appearance.Options.UseBackColor = true;
            this.picTipImage.Properties.Appearance.Options.UseForeColor = true;
            this.picTipImage.Properties.PictureAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.picTipImage.ShowToolTips = false;
            this.picTipImage.Size = new Size(300, 60);

            Point panelMiddle = new Point(this.gridControlList.Width / 2, this.gridControlList.Height / 2);
            this.picTipImage.Location = new Point(panelMiddle.X - this.picTipImage.Size.Width / 2, panelMiddle.Y - this.picTipImage.Size.Height / 2);



            this.picTipImage.TabIndex = 0;
            // 
            // lblAnimationTip
            // 

            this.lblAnimationTip.Name = "lblAnimationTip";
            this.lblAnimationTip.Size = new System.Drawing.Size(200, 31);
            this.lblAnimationTip.Location = new Point(this.picTipImage.Location.X + this.picTipImage.Width / 2 - this.lblAnimationTip.Width / 2, this.picTipImage.Location.Y + this.picTipImage.Height);
            this.lblAnimationTip.TabIndex = 1;

            this.lblAnimationTip.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            this.gridControlList.Controls.Add(this.picTipImage);
            this.gridControlList.Controls.Add(this.lblAnimationTip);
            this.gridControlList.ResumeLayout();

        }
        public void EndShowAnimation()
        {
            this.gridControlList.SuspendLayout();
            this.gridControlList.Controls.Remove(this.picTipImage);
            this.gridControlList.Controls.Remove(this.lblAnimationTip);
            this.gridControlList.ResumeLayout();
        }
        #endregion
        /// <summary>
        /// 通过业务ID查找所属数据行
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        public DataRow GetDataRowByOperationId(Guid operationId)
        {
            DataRow row = (this.bindingSource.DataSource as DataTable).Select(string.Format("ID='{0}'", operationId)).First();

            return row;
        }
        /// <summary>
        /// 通过业务号查找数据行
        /// </summary>
        /// <param name="operationNo"></param>
        /// <returns></returns>
        public DataRow GetDataRowByOperationNo(string operationNo)
        {
            DataRow[] rows = (this.bindingSource.DataSource as DataTable).Select(string.Format("NO='{0}'", operationNo));
            if (rows == null || rows.Length <= 0)
                return null;
            else
                return rows[0];
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

        /// <summary>
        /// 查找时选择行
        /// </summary>
        private void SetSelectedRow()
        {
            if (previousMatchRows.Length == 0)
            {
                return;
            }
            if (previousSelectRowIndex != -1 && previousSelectRowIndex == previousMatchRows.Length - 1)
            {
                previousSelectRowIndex = -1;
            }
            DataRow row = previousMatchRows[++previousSelectRowIndex];

            int dataSourceIndex = GetDataSource().Rows.IndexOf(row);
            if (dataSourceIndex == -1)
            {
                return;
            }
            int rowIndex = this.gridViewList.GetRowHandle(dataSourceIndex);
            ///清除已有的选择行
            if (this.gridViewList.SelectedRowsCount > 0)
            {
                int[] selectedRowHandles = this.gridViewList.GetSelectedRows();
                for (int i = 0; i < selectedRowHandles.Length; i++)
                {
                    this.gridViewList.UnselectRow(selectedRowHandles[i]);
                }
            }
            this.gridViewList.FocusedRowHandle = rowIndex;

            this.gridViewList.SelectRow(rowIndex);

        }

        private void SetFocus()
        {
            BarEditItem item = GetBarEditSearchItem();
            this.barManager.Items[item.Name].Links[0].Focus();
        }



        private DataTable GetDataSource()
        {
            return this.bindingSource.DataSource as DataTable;
        }

        private string GetFilterExpression()
        {
            List<string> filterColumnNames = null;
            if (filterColumnNames == null || filterColumnNames.Count <= 0)
                return "1=1";
            string expression = DataCacheUtility.GetExpression(filterColumnNames, new List<string> { this.QueryNo }, false);
            return expression;
        }
        private DataRow[] previousMatchRows = null;
        private int previousSelectRowIndex = -1;
        private string previousQueryNo = string.Empty;

        #endregion



        private List<CustomColumnInfo> GetGridColumnInfos()
        {
            List<CustomColumnInfo> list = new List<CustomColumnInfo>();
            int columnCount = this.gridViewList.Columns.Count;
            ///如果有只有代码新增的排序列，则直接返回
            if (columnCount == 1)
            {
                return list;
            }
            for (int i = 0; i < columnCount; i++)
            {
                DevExpress.XtraGrid.Columns.GridColumn column = this.gridViewList.Columns[i];
                CustomColumnInfo columnInfo = new CustomColumnInfo();
                columnInfo.Name = column.Name;
                columnInfo.Visible = column.Visible;
                columnInfo.VisibleIndex = column.VisibleIndex + 1;
                columnInfo.Width = column.Width;
                columnInfo.Fixed = (ColumnFixedStyle)Enum.Parse(typeof(ColumnFixedStyle), column.Fixed.ToString());
                columnInfo.AbsoluteIndex = column.AbsoluteIndex;
                columnInfo.SortOrder = (ICP.DataCache.ServiceInterface.SortOrder)((int)column.SortOrder);
                list.Add(columnInfo);
            }
            return list;
        }

        /// <summary>
        /// 保存列表自定义信息
        /// </summary>
        public void SaveCustomColumnInfo()
        {
            if (!isColumnSettingChanged)
                return;
            List<CustomColumnInfo> columnInfos = GetGridColumnInfos();

            if (columnInfos.Count <= 0)
                return;
            try
            {
                UserCustomGridInfo gridInfo = this.currentUserCustomGridInfo;
                if (gridInfo == null)
                {
                    gridInfo = new UserCustomGridInfo();
                    // gridInfo.ListType = GetListFormType();
                    gridInfo.TemplateCode = TemplateCode;
                    gridInfo.Id = Guid.NewGuid();

                }
                if (gridInfo.UserId == null)
                {
                    gridInfo.UserId = LocalData.UserInfo.LoginID;
                    //gridInfo.TemplateCode = TemplateCode;
                    //gridInfo.Id = Guid.NewGuid();
                }

                gridInfo.Columns = columnInfos;
                ServiceClient.GetService<IClientCustomDataGridService>().Save(gridInfo);
                isColumnSettingChanged = false;
            }
            catch (Exception ex)
            {
                // throw ex;
            }

        }

        /// <summary>
        /// 得到业务操作上下文对象
        /// </summary>
        public BusinessOperationContext Context
        {
            get
            {
                BusinessOperationContext context = new BusinessOperationContext();
                context.OperationID = this.ID;
                context.OperationType = OperationType;
                context.FormType = ICP.Framework.CommonLibrary.Common.FormType.Unknown;
                context.FormId = this.ID;
                return context;
            }
        }

    

        private void gridViewList_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            object dss = gridViewList.GetRow(e.RowHandle);
            DataRow dr = gridViewList.GetDataRow(e.RowHandle);
            if (dr == null)
            {
                return;
            }
            int strState = int.Parse(dr["State"].ToString());
            bool isValid = (bool)dr["IsValid"];



            if (isValid == false)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Disabled);
            }
            else if (strState == 1)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.NewLine);
            }
            else if (strState == 2)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Confirmed);
            }
            else if (strState == 3)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Error);
            }
            else if (strState == 4)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Warning);
            }
        }

        /// <summary>
        /// 通过包含名称查找工具栏并设置是否可用
        /// </summary>
        /// <param name="name">包含的名称</param>
        /// <param name="enable">是否可用</param>
        /// <returns></returns>
        private void SetBarItemEnable(string name, bool enable)
        {
            //jibg
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
        /// 通过包含名称查找右键菜单并设置是否可用
        /// </summary>
        /// <param name="name">包含的名称</param>
        /// <param name="enable">是否可用</param>
        /// <returns></returns>
        private void SetToolStripItemEnable(string name, bool enable)
        {
            foreach (ToolStripItem tsi in contextMenuStrip.Items)
            {
                if (tsi.Name.ToLower().Contains(name))
                {
                    tsi.Enabled = enable;
                }
            }
        }

        /// <summary>
        /// 取消按钮需要包括的关键字
        /// </summary>
        const string CancelName = "cancel";
        /// <summary>
        /// 恢复按钮需要包括的关键字
        /// </summary>
        const string ResumeName = "resume";
        /// <summary>
        /// 恢复按钮需要包括的关键字
        /// </summary>
        const string SaveName = "submit";
        const string EditName = "edit";
        const string CopyName = "copy";

        /// <summary>
        /// 编辑改变单元格触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewList_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            IsEdited = true;
            SetBarItemEnable(SaveName, true);

            if (dictEditable.ContainsKey(e.Column.FieldName))
            {
                dictEditable[e.Column.FieldName] = true;
            }
        }

        /// <summary>
        /// 列表数据是否被修改
        /// </summary>
        private bool IsEdited = false;



        /// <summary>
        /// 保存修改数据
        /// </summary>
        /// <returns></returns>
        public bool SaveEditData()
        {
            try
            {

                UpdateBuisinessParam editParam = new UpdateBuisinessParam();
                editParam.UserID = LocalData.UserInfo.LoginID;
                editParam.OperationType = OperationType;


                //得到修改数据
                foreach (DataRow dr in CurrentDataSource.Rows)
                {
                    if (dr.RowState == DataRowState.Modified)
                    {
                        editParam.AddData("ID", dr["ID"]);

                        foreach (string key in dictEditable.Keys)
                        {
                            if (dictEditable[key])
                            {
                                editParam.AddData(key, dr[key]);
                            }
                        }
                    }
                }

                //保存修改数据

                if (editParam.IDList.Count > 0)
                {
                    bool result = BusinessSave.SaveEditData(editParam);
                    //保存成功，修改dictEditable字典为false；
                    if (result)
                    {
                        foreach (string key in dictEditable.Keys)
                        {
                            dictEditable[key] = false;
                        }
                    }
                    return result;
                }
                else
                {
                    return true;
                }



            }
            catch (System.Exception ex)
            {

            }
            return false;

        }

        private bool isInSubmit = false;
        private DataTable dtClone = new DataTable();


        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(Command_Add_Attachment)]
        public void CommandAddAttachment(object sender, EventArgs e)
        {
            if (CurrentTag == null) return;

            string fieldName = CurrentTag.ToString();

            string[] filePaths = CommonUIUtility.SelectFilesToUpload();
            if (filePaths == null) return;
            if (!CommonUIUtility.ValidateFileInfo(filePaths)) return;


            ///只有符合类型的文档才允许拖入业务列表
            string fileExtensions = CommonUIUtility.GetFileExtensions();

            List<string> listTareget = filePaths.Where(file => fileExtensions.Contains(Path.GetExtension(file))).ToList();



            HandleDragDrop(FocusedRowHandle, listTareget, fieldName);



        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("Command_TaskCenter_Submit")]
        public void CommandTaskCenterSubmit(object sender, EventArgs e)
        {
            //if ( IsEdited)
            //{
            SaveEditData();
            IsEdited = false;
            //}

            if (isInSubmit)
                return;
            if (dicAddedAttachments.Count <= 0 && this.dicDeleteAttachments.Count <= 0)
                return;

            int fileNum = this.dicAddedAttachments.Values.Count;
            List<DocumentInfo> documents = new List<DocumentInfo>();
            //  DocumentType documentType = GetDocumentType();
            try
            {
                isInSubmit = true;
                BeginShowAnimation();
                string[] paths = this.dicAddedAttachments.Values.ToArray();
                for (int i = 0; i < paths.Length; i++)
                {
                    paths[i] = paths[i].Substring(0, paths[i].IndexOf("@"));
                }
                ICP.MailCenter.CommonUI.CommonUIUtility.ValidateFileInUse(paths);

                foreach (Guid operationId in this.dicAddedAttachments.Keys)
                {
                    foreach (string pathAndType in this.dicAddedAttachments[operationId])
                    {
                        DocumentInfo document = new DocumentInfo();
                        string[] pathAndTypes = pathAndType.Split(new string[] { "@" }, StringSplitOptions.RemoveEmptyEntries);



                        document.OriginalPath = pathAndTypes[0];
                        document.Id = Guid.NewGuid();
                        document.Name = Path.GetFileName(pathAndTypes[0]);
                        document.FileSources = FileSource.FDocument;
                        document.DocumentType = ICP.DataCache.ServiceInterface.DocumentType.AP;
                        document.DocumentTypeValue = int.Parse(pathAndTypes[1]);
                        document.OperationID = operationId;
                        document.CreateBy = LocalData.UserInfo.LoginID;
                        document.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                        document.FormType = ICP.Framework.CommonLibrary.Common.FormType.Unknown;
                        documents.Add(document);
                    }
                }
                List<string> deleteFileNames = new List<string>();
                List<Guid> operationIds = new List<Guid>();
                foreach (var item in dicDeleteAttachments)
                {
                    deleteFileNames.AddRange(item.Value);
                    int count = item.Value.Count;
                    Guid operationId = item.Key;
                    for (int i = 0; i < count; i++)
                    {
                        operationIds.Add(operationId);
                    }
                }
                ClientFileService.Save(documents, deleteFileNames, operationIds);

            }
            catch (Exception ex)
            {
                isInSubmit = false;

                TrapException(ex);

            }
            finally
            {
                isInSubmit = false;
                EndShowAnimation();
            }
            ResetCopyOperationRecord();

        }

        private void ResetCopyOperationRecord()
        {
            this.dicAddedAttachments.Clear();
            this.dicDeleteAttachments.Clear();
        }

        /// <summary>
        /// 删除附件
        /// 如果是在服务端已保存的文档，则获取出来的信息为 文件名:文档Id：文档更新时间
        /// 否则获取出来为 文件名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(Command_Delete_Attachment)]
        public void CommandDeleteAttachment(object sender, EventArgs e)
        {
            if (CurrentTag == null) return;
            string[] tags = CurrentTag.ToString().Split(new char[] { '@' });
            string attachmentNameOrLocalFullPath = tags[0]; //GetContextMenuItemBusinessRelatedNo(sender, false);


            string existsCopyPaths = CurrentRow.Field<string>(tags[1] + CopyPath);

            string existsSONOs = CurrentRow.Field<string>("SONO");
            string copyNameColumnName = tags[1];

            string existsCopyName = CurrentRow.Field<string>(copyNameColumnName);
            string copyFileName = Path.GetFileName(attachmentNameOrLocalFullPath);
            //文件名列移除
            CurrentRow[copyNameColumnName] = RemoveSubString(existsCopyName, copyFileName, normalSeparator);


            CurrentRow["SONO"] = RemoveSubString(existsSONOs, Path.GetFileNameWithoutExtension(copyFileName), normalSeparator);
            //如果包含绝对路径信息，则代表是本地上传还未保存到数据库
            if (Path.IsPathRooted(attachmentNameOrLocalFullPath))
            {
                this.dicAddedAttachments[this.ID].Remove(attachmentNameOrLocalFullPath + "@" + DictUploadColumnType[tags[1]].OperateType);
                //从文件路径字段移除
                CurrentRow[tags[1] + CopyPath] = RemoveSubString(existsCopyPaths, attachmentNameOrLocalFullPath, normalSeparator);
            }
            //如果是服务端文档
            else
            {
                dicDeleteAttachments.Add(this.ID, attachmentNameOrLocalFullPath);

            }
            ResetBinding();


        }
        private string RemoveSubString(string source, string target, string separator)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(target))
                return source;
            List<string> temp = source.Split(separator.ToCharArray()).Where(name => !string.IsNullOrEmpty(name)).ToList();
            temp.Remove(target);
            if (temp.Count == 0)
                return null;
            return temp.Aggregate((a, b) => a + separator + b).Trim(separator.ToCharArray());
        }



        private void ShowSubGrid(ListFormType? gridType)
        {
            //if (currentGridType == gridType)
            //    return;
            UpdateSubGridToolBarItem(gridType);
            // currentGridType = gridType;
            DynamicCreateColumns(gridType);
            DynamicBindData(gridType);


        }

        private void UpdateSubGridToolBarItem(ListFormType? gridType)
        {
            //  RemoveBusinessToobarItems(currentGridType);
            AddBusinessToobarItems(gridType);
        }


        private void OnDocumentUploadFailed(UploadFailedMessage message)
        {
            EndShowAnimation();
            TrapException(new ApplicationException(message.ErrorMessage));
            // XtraMessageBox.Show(message.ErrorMessage, "Tip", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.CancelEdit();
            ResetCopyOperationRecord();
            isInSubmit = false;

        }
        private void OnDocumentUploadSucessed(DocumentInfo[] documents)
        {
            EndShowAnimation();
            foreach (Guid operationId in this.dicAddedAttachments.Keys)
            {
                DataRow row = GetDataRowByOperationId(operationId);
                row["CopyPath"] = null;
            }

            this.AcceptChanges();
            this.ResetCopyOperationRecord();
            isInSubmit = false;


        }

        private void splitContainerControl1_SplitterPositionChanged(object sender, EventArgs e)
        {
            UserPrivateInfo.SaveSplitterPosition(TemplateCode + "SplitPositionKey", this.splitContainerControl1.SplitterPosition.ToString());

        }

        #region 文件拖放处理
        void gridControlList_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            Point gvLocation = PointToScreen(this.gridControlList.Location);
            int x = e.X - gvLocation.X;
            int y = e.Y - gvLocation.Y;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi = this.gridViewList.CalcHitInfo(new Point(x, y));
            int iMouseRowHandle = hi.RowHandle;
            string fieldName = hi.Column.FieldName;


            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (files == null || files.Length <= 0)
                return;
            ///只有符合类型的文档才允许拖入业务列表
            string fileExtensions = CommonUIUtility.GetFileExtensions();

            List<string> listTareget = files.Where(file => fileExtensions.Contains(Path.GetExtension(file))).ToList();

            HandleDragDrop(iMouseRowHandle, listTareget, fieldName);
        }
        /// <summary>
        /// 提供给具体业务面板处理拖放操作
        /// </summary>
        /// <param name="rowHandle"></param>
        /// <param name="filePaths"></param>
        public virtual void HandleDragDrop(int rowHandle, List<string> filePaths, string fieldName)
        {
            try
            {
                List<string> temps = (from file in filePaths
                                      select Path.GetFileName(file)).ToList();


                string fileNames = temps.Aggregate((a, b) => a + normalSeparator + b);
                string copyPaths = filePaths.Aggregate((a, b) => a + normalSeparator + b);
                if (rowHandle < 0)
                    return;

                //  string copyNameColumnName = GetCopyNameColumnName();
                DataRow row = GetDataRowByRowHandle(rowHandle);

                DataTable dt = GetDataSource();

                //   GridColumn gc = gridViewList.Columns.ColumnByFieldName(fieldName);


                string existsCopyName = row.Field<string>(fieldName);

                //object ss= gridViewList.GetRowCellValue(rowHandle, gc);
                //existsCopyName = ss == null ? null : ss.ToString();

                if (ValidateDuplicateCopyExists(temps, existsCopyName))
                    return;

                string existsCopyPaths = row.Field<string>(fieldName + CopyPath);

                Guid operationNo = row.Field<Guid>("ID");

                // gridViewList.SetRowCellValue(rowHandle, fieldName, existsCopyName == null ? fileNames : existsCopyName + normalSeparator + fileNames);

                row[fieldName] = existsCopyName == null ? fileNames : existsCopyName + normalSeparator + fileNames;


                // gridViewList.SetRowCellValue(rowHandle, fieldName + CopyPath, existsCopyPaths == null ? copyPaths : existsCopyPaths + normalSeparator + copyPaths);

                row[fieldName + CopyPath] = existsCopyPaths == null ? copyPaths : existsCopyPaths + normalSeparator + copyPaths;
                //if (currentGridType == ListFormType.MailLink4CarrierSO && SOSetting.Current.AutoFillSO)
                //{
                if (gridViewList.Columns.ColumnByFieldName("SONO") != null)
                {

                    string existsSONOs = row.Field<string>("SONO");
                    string soNos = (from file in filePaths
                                    select Path.GetFileNameWithoutExtension(file)).ToList().Aggregate((a, b) => a + normalSeparator + b);
                    row["SONO"] = existsSONOs == null ? soNos : existsSONOs + normalSeparator + soNos;
                    IsEdited = true;
                    //  gridViewList.SetRowCellValue(rowHandle, "SONO", existsSONOs == null ? soNos : existsSONOs + normalSeparator + soNos);
                }
                //}
                foreach (string copyPath in filePaths)
                {
                    dicAddedAttachments.Add(operationNo, copyPath + "@" + DictUploadColumnType[fieldName].DocumentType.ToString());
                }
                ResetBinding();

                SetBarItemEnable(SaveName, true);
            }
            catch (System.Exception ex)
            {

            }

        }

        private ListDictionary<Guid, string> dicAddedAttachments = new ListDictionary<Guid, string>();


        /// <summary>
        ///一票业务下不允许存在多个重名文档
        /// </summary>
        /// <param name="temps"></param>
        /// <param name="existsCopyName"></param>
        /// <returns></returns>
        private bool ValidateDuplicateCopyExists(List<string> temps, string existsCopyName)
        {
            if (temps == null || temps.Count <= 0 || string.IsNullOrEmpty(existsCopyName))
                return false;
            List<string> listExists = existsCopyName.Split(normalSeparator.ToCharArray()).ToList();
            List<string> listCommon = temps.Intersect(listExists).ToList();
            if (listCommon != null && listCommon.Count > 0)
            {
                string tip = string.Format(IsEnglish ? "Copy already exists:{0}." : "副本:{0}已经存在。", listCommon.Aggregate((a, b) => a + "," + b));
                XtraMessageBox.Show(tip, "Tip", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            return false;

        }

        private string GetCopyNameColumnName()
        {
            string columnName = string.Empty;
            //if (currentGridType.Value == ListFormType.MailLink4CarrierAP)
            //{
            //    columnName = "APCopy";
            //}
            //else if (currentGridType.Value == ListFormType.MailLink4CarrierMBL)
            //{
            //    columnName = "MBLCopy";
            //}
            //else
            //{
            //    columnName = "SOCopy";
            //}
            return columnName;
        }
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
        #endregion

        #region tab操作
        /// <summary>
        /// 构造Tab选项卡
        /// </summary>
        /// <param name="code">当前的节点code</param>
        public void StructuretabWork(string code)
        {
            var xtra = new List<XtraTabPage>();
            var xtraTab = new XtraTabPage();
            var tabControlReadXml = new TabControlReadXml();
            TabControls = tabControlReadXml.GetTabControlList(code);
            if (TabControls.Any() == false)
            {
                return;
            }
            foreach (var control in TabControls)
            {
                var xtraTabPage = new XtraTabPage
                    {
                        Text = LocalData.IsEnglish ? control.Ename : control.Cname,
                        Name = control.Ename,
                        TabIndex = control.Order,
                        Dock = DockStyle.Fill
                    };
                xtra.Add(xtraTabPage);
                if (control.Selected)
                {
                    xtraTab = xtraTabPage;
                }

            }
            xtraTabControl.TabPages.AddRange(xtra.ToArray());
            xtraTabControl.SelectedTabPage = xtraTab;
        }

        /// <summary>
        /// 选项卡发生改变时触发 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            var name = xtraTabControl.SelectedTabPage.Name;
            var indextabControl = (from t in TabControls where t.Ename == name select t).FirstOrDefault();
            if (indextabControl != null)
            {
                Databind(indextabControl);
            }
        }

        /// <summary>
        /// 控件数据绑定的方法
        /// </summary>
        /// <param name="tab">对象集合</param>
        public void Databind(TabControl tab)
        {
            #region 构造面板加载控件
            if (tab.Dictionary.Values.Any(item => item == false))
            {
                //添加面板控件
                var deck = new DeckWorkspace
                {
                    Dock = DockStyle.Fill,
                    Name = "DeckWorkspace" + tab.Ename,
                    TabIndex = 1,
                    Text = "DeckWorkspace" + tab.Ename,
                    BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241))))),
                    Location = new System.Drawing.Point(0, 0)
                };
                //加入当前自定义控件
                tab.Control.Dock = DockStyle.Fill;
                deck.Controls.Add(tab.Control);
                //当前选项卡加载面板
                xtraTabControl.SelectedTabPage.Controls.Add(deck);
                tab.Dictionary.Remove(tab.Order);
                var dictionary = new Dictionary<int, bool> { { tab.Order, true } };
                tab.Dictionary = dictionary;
            }
            #endregion

            #region  执行查询

            if (CurrentRow == null)
            {
                return;
            }
            var business = new BusinessOperationContext()
            {
                FormType = FormType.Booking,
                OperationType = OperationType,
                State = DocumentState.Pending,
                OperationID = new Guid(CurrentRow["ID"].ToString()),
                FormId = new Guid(CurrentRow["ID"].ToString())
            };
            var taskCenterDataBind = tab.Control as Common.ServiceInterface.IDataBind;
            if (taskCenterDataBind != null)
            {
                taskCenterDataBind.DataBind(business);
            }

            #endregion

        }
        #endregion

        #region    Delete Void

        /// <summary>
        /// 关联当前业务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        //public void Command_EmailOperationMessageRelation(object sender, EventArgs e)
        //{
        //    if (CurrentDataSource == null || CurrentRow == null)
        //        return;
        //    if (dicMessageRelation.ContainsValue(this.OperationNo))
        //        return;
        //    if (GetListFormType() == ListFormType.MailLink4Carrier)
        //        return;
        //    string messageId = this.CurrentMessage.MessageId;

        //    DialogResult result = PromptChangeRelation();
        //    if (result == DialogResult.OK)
        //    {
        //        string previousOperationNo = string.Empty;
        //        if (this.dicMessageRelation.Values.Count > 0)
        //        {
        //            previousOperationNo = this.dicMessageRelation.Values.First();
        //        }
        //        using (new CursorHelper(Cursors.WaitCursor))
        //        {
        //            OperationMessageRelation relation;
        //            if (this.CurrentMessageRelation == null || !this.CurrentMessageRelation.HasData)
        //            {
        //                this.CurrentMessage.UserProperties = new MessageUserPropertiesObject();
        //                this.CurrentMessage.UserProperties.OperationId = this.ID;
        //                this.CurrentMessage.UserProperties.OperationType = GetOperationType();
        //                this.CurrentMessage.Type = MessageType.Email;
        //                this.CurrentMessage.Id = Guid.NewGuid();
        //                try
        //                {
        //                    ServiceClient.GetService<IClientMessageService>().Save(this.CurrentMessage);
        //                }
        //                catch (Exception ex)
        //                {
        //                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
        //                }
        //                relation = new OperationMessageRelation { HasData = true, OperationID = this.ID, IMessageId = this.CurrentMessage.Id, ID = Guid.NewGuid(), OperationNo = this.OperationNo, MessageId = messageId };

        //            }
        //            else
        //            {
        //                relation = this.CurrentMessageRelation;
        //                relation.OperationNo = this.OperationNo;
        //                relation.OperationID = this.ID;
        //                relation.OperationType = GetOperationType();
        //                relation.HasData = true;
        //                SaveRelation(relation);
        //            }

        //            this.CurrentMessageRelation = relation;
        //            dicMessageRelation.Clear();
        //            dicMessageRelation.Add(messageId, this.OperationNo);
        //            SetBusinessStyleFormationCondition(this.OperationNo);
        //            string expression = string.Format("NO='{0}'", relation.OperationNo);
        //            ResetIsAssociatedColumnValue(previousOperationNo);
        //            ChangeDataRowPosition(expression);
        //        }

        //    }
        //}

        #region IBusinessPart 成员


        //public virtual ListFormType GetListFormType()
        //{
        //    return this.ListType;
        //}

        #endregion

        ///// <summary>
        ///// 从邮件信息抽取信息（包括单号，来源类型，发送人地址）
        ///// </summary>
        ///// <param name="mail"></param>
        //private void GetMailInfo(ICP.Message.ServiceInterface.Message mail)
        //{
        //    MailInfoGetter.ExtractMailInfo(mail, this.GetType().Name);
        //    this.SourceType = MailInfoGetter.SourceType;
        //    this.Nos = MailInfoGetter.Nos;
        //    this.SenderEmailAddress = MailInfoGetter.EmailAddress;
        //    this.currentMessage = mail;
        //}


        /// <summary>
        /// 高级查找
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //public void Command_EmailAdvanceQuery(object sender, EventArgs e)
        //{
        //    using (new CursorHelper(Cursors.WaitCursor))
        //    {
        //        Dictionary<string, object> initValues = new Dictionary<string, object>();
        //        initValues.Add(ICP.Common.Business.ServiceInterface.Constants.BusinessTypeKey, this.BusinessType);
        //        initValues.Add(Constants.OperationViewCodeKey, this.ListType.ToString());
        //        initValues.Add(Constants.SelectedCompnayKey, this.selectedCompanyIds);
        //        InsertInitValues(initValues);
        //        frmAdvanceQuery frmQuery = this.WorkItem.Items.AddNew<frmAdvanceQuery>();
        //        frmQuery.Init(initValues);
        //        frmQuery.MaximizeBox = frmQuery.MinimizeBox = false;
        //        frmQuery.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        //        DialogResult result = frmQuery.ShowDialog(this);
        //        if (result == DialogResult.OK)
        //        {
        //            this.AdvanceQueryString = frmQuery.AdvanceQueryString;
        //            QueryAndBindData(this.CurrentMessage, true);

        //        }
        //        frmQuery = null;
        //    }

        //}
        //private void ProcessMessageRelation(BusinessQueryResult result, BindParameter parameter)
        //{
        //    if (!IsSameAsCurrentMessage(parameter.Message))
        //        return;
        //    this.dicMessageRelation.Clear();
        //    this.CurrentMessageRelation = null;
        //    //如果当前邮件已关联业务，则更改关联业务行的显示样式，将业务行放到第一行位置
        //    if (result.Relation.HasData)
        //    {
        //        ChangeBusinessStyle(result.Relation, parameter.Message);
        //        return;
        //    }
        //    DataTable dt = parameter.Dt;
        //    List<string> nos = parameter.Nos;
        //    if (dt == null || dt.Rows.Count <= 0)
        //        return;
        //    if (nos == null || nos.Count <= 0)
        //    {
        //        ClearBusinessStyleFormatCondition();
        //        return;
        //    }
        //    string filterExpression = DataCacheUtility.GetExpression(nos, this.ConditionColumnNames, true);
        //    DataRow[] rows = dt.Select(filterExpression);
        //    if (rows == null || rows.Length <= 0)
        //    {
        //        ClearBusinessStyleFormatCondition();
        //        return;
        //    }
        //    string operationNo = rows[0].Field<string>("NO");
        //    Guid operationId = rows[0].Field<Guid>("ID");
        //    ICP.Message.ServiceInterface.Message message = parameter.Message;
        //    message.UserProperties = new MessageUserPropertiesObject();
        //    message.UserProperties.OperationId = operationId;
        //    message.UserProperties.OperationType = GetOperationType();
        //    if (!IsSameAsCurrentMessage(parameter.Message))
        //        return;
        //    message.UserProperties["OperationNo"] = operationNo;
        //    WaitCallback callback = (data) =>
        //    {

        //        ICP.Message.ServiceInterface.Message temp = data as ICP.Message.ServiceInterface.Message;
        //        if (!IsSameAsCurrentMessage(parameter.Message))
        //            return;

        //        ServiceClient.GetService<IClientMessageService>().Save(message);
        //        if (!IsSameAsCurrentMessage(message))
        //            return;

        //        OperationMessageRelation relation = new OperationMessageRelation { HasData = true, OperationID = operationId, IMessageId = message.Id, ID = Guid.NewGuid(), OperationNo = operationNo, MessageId = message.MessageId };
        //        lock (syncObj)
        //        {
        //            if (IsSameAsCurrentMessage(message))
        //            {
        //              ChangeBusinessStyle(relation,message);
        //            }
        //        }
        //    };
        //    ThreadPool.QueueUserWorkItem(callback, message);




        //}

        //private void OnApplicationExit(object sender, EventArgs e)
        //{
        //    SaveCustomColumnInfo();
        //}

        //private void InitDictEditable()
        //{
        //    dictEditable.Add("SoNo", false);
        //    //dictEditable.Add("df", false);
        //}


        //protected override void AfterDataBind()
        //{
        //    isInSubmit = false;


        //    dtClone.Dispose();
        //    dtClone = null;

        //    dtClone = CurrentDataSource.Copy();
        //}


        //public void Command_EmailQuery(object sender, EventArgs e)
        //{
        //    if (GetListFormType() == ListFormType.MailLink4Carrier)
        //        return;
        //    if (CurrentDataSource == null)
        //        return;
        //    using (new CursorHelper(Cursors.WaitCursor))
        //    {
        //        string queryNo = this.QueryNo;
        //        //如果搜索单号为空，则直接返回
        //        if (string.IsNullOrEmpty(queryNo))
        //        {

        //            return;
        //        }
        //        //如果此次搜索单号和上一次搜索相同，则从旧有匹配记录里查找
        //        if (queryNo.Equals(this.previousQueryNo, StringComparison.CurrentCultureIgnoreCase))
        //        {
        //            //如果上次搜索没有匹配行，则直接返回
        //            if (previousMatchRows == null || previousMatchRows.Length <= 0)
        //            {

        //                return;
        //            }
        //            //如果上次搜索匹配已经到了最后一条匹配记录，则直接返回
        //            //if (previousSelectRowIndex == previousMatchRows.Length - 1)
        //            //    return;
        //            SetSelectedRow();

        //        }
        //        //如果此次搜索和上次搜索单号不同
        //        else
        //        {
        //            //重置搜索记录
        //            ResetQueryRecord();
        //            DataRow[] rows = previousMatchRows = this.CurrentDataSource.Select(GetFilterExpression());
        //            if (rows == null || rows.Length <= 0)
        //            {
        //                BarEditItem item2 = GetBarEditSearchItem();
        //                item2.Links[0].Focus();
        //                return;
        //            }
        //            previousQueryNo = queryNo;
        //            SetSelectedRow();



        //        }




        //    }
        //}


        bool isFirstTimeEnter = true;
        /// <summary>
        /// 下拉多选业务所属公司
        /// <remarks>加载时默认为所有公司</remarks>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //public void Command_EmailSelectCompany(object sender, EventArgs e)
        //{
        //    if (isFirstTimeEnter)
        //    {
        //        isFirstTimeEnter = false;
        //        return;
        //    }

        //    using (new CursorHelper(Cursors.WaitCursor))
        //    {
        //        ListFormType formType = GetListFormType();
        //        if (formType == ListFormType.MailLink4Carrier)
        //            return;
        //        Command command = sender as Command;
        //        if (command == null)
        //            return;
        //        List<RepositoryItem> items = command.FindAdapters<RepositoryItemCommandAdapter>().Last().Invokers.Keys.ToList();
        //        if (items == null || items.Count <= 0)
        //            return;
        //        RepositoryItemCheckedComboBoxEdit item = items.Last() as RepositoryItemCheckedComboBoxEdit;
        //        if (item == null)
        //        {
        //            return;
        //        }
        //        string checkedItems = item.GetCheckedItems().ToString();
        //        if (selectedCompanyIds == checkedItems)
        //        {
        //            return;
        //        }

        //        selectedCompanyIds = checkedItems;
        //        QueryAndBindData(this.CurrentMessage,false);
        //    }

        //}

        //public void Command_EmailAddOE(object sender, EventArgs e)
        //{
        //    using (new CursorHelper(Cursors.WaitCursor))
        //    {
        //        //ClientHelper.HideForm(LocalData.MainFormHandle);
        //        Dictionary<string, object> dic = CreateBusinessParameter(ICP.Common.ServiceInterface.DataObjects.ActionType.Create);
        //        FCMCommonOperationService.CreateSO(dic);
        //    }
        //}
        #endregion

    }
    /// <summary>
    /// 数据绑定参数类
    /// </summary>
    public class BindParameter
    {
        public ICP.Message.ServiceInterface.Message Message { get; set; }
        public object Parameter { get; set; }
        public List<string> Nos { get; set; }
        public DataTable Dt { get; set; }
        public bool NeedBindData { get; set; }
        public BusinessQueryCriteria QueryCriteria { get; set; }
        public IBusinessQueryServiceGetter QueryServiceGetter { get; set; }
    }
}





