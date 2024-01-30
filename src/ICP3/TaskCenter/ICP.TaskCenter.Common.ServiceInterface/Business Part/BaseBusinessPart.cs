using System;
using System.Collections.Generic;
using System.Linq;
using ICP.MailCenter.Business.ServiceInterface;
using DevExpress.XtraEditors;
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
using ICP.Framework.ClientComponents.Controls;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface.DataObjects;
using DevExpress.XtraEditors.Repository;
using ICP.Framework.CommonLibrary.Server;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
//using ICP.MailCenter.Business.ServiceInterface;
using System.Reflection;
using ICP.FCM.Common.ServiceInterface.Interfaces;
using ICP.MailCenter.ServiceInterface.Common;
using Microsoft.Practices.CompositeUI.Utility;
using ICP.MailCenter.CommonUI;
using ICP.TaskCenter.ServiceInterface;
using DevExpress.XtraGrid.Views.Base;
//using ICP.TaskCenter.ServiceInterface;

namespace ICP.MailCenter.Business.ServiceInterface
{
    /// <summary>
    /// 业务面板基类
    /// </summary>
    [SmartPart]
    public   partial class BaseBusinessPart : XtraUserControl, IBusinessPart
    {
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
        public IOperationViewService OperationViewService
        {
            get
            {
                return ServiceClient.GetService<IOperationViewService>();
            }
        }


        //public IBusinessOperationHandleService BusinessOperationHandleService
        //{
        //    get
        //    {
        //        return ServiceClient.GetClientService<IBusinessOperationHandleService>();
        //    }
        //}


        //public IGetBillInfoFactory GetBillInfoFactory
        //{
        //    get
        //    {
        //        return ServiceClient.GetClientService<IGetBillInfoFactory>();
        //    }
        //}

       /// <summary>
       /// 发生邮件地址
       /// </summary>
        public string SenderEmailAddress { get; set; }
        /// <summary>
        /// 是否存在公共右键菜单项默认为false
        /// </summary>
        private bool isCommonContextMenuItemsExists = false;
        private List<ContextMenuItemInfo> contextMenuCommonItemList;
        public  string contextMenuStripUISiteName = "TaskCenterContextMenu";
        public string toolbarUISiteName = "TaskCenterToolbar";
        private ICP.Message.ServiceInterface.Message currentMessage;
        private bool needBindData = true;
      //  private int visibleIndex = 0;

        private DevExpress.XtraEditors.PictureEdit picTipImage;
        private DevExpress.XtraEditors.LabelControl lblAnimationTip;
        private Dictionary<string, string> dicMessageRelation = new Dictionary<string, string>();
        private UserCustomGridInfo currentUserCustomGridInfo;

        internal string selectedCompanyIds = LocalData.UserInfo.UserOrganizationList.FindAll(o => o.Type == LocalOrganizationType.Company).Select(o => o.ID.ToString()).Aggregate((a, b) => a + "," + b);

        /// <summary>
        /// 弹出菜单注册名称
        /// </summary>
        public string ContextMenuStripUISiteName
        {
            get
            {//this.SourceType.ToString() + 
                return this.contextMenuStripUISiteName;//this.contextMenuStripUISiteName;
            }
        }
        /// <summary>
        /// 业务面板是否需要绑定数据
        /// </summary>
        public virtual bool NeedBindData
        {
            get
            {
                return this.needBindData;
            }
            set
            {
                this.needBindData = value;
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
                return this.CurrentDataSource.Rows[rowIndex];
            }
        }
        /// <summary>
        /// 操作视图模板编码
        /// </summary>
        public  string TemplateCode
        {
            get;
            set;
        }

        public ListDictionary<Guid, string> AddedAttachments
        {
            get;
            set;
        }

        public ListDictionary<Guid, string> DeleteAttachments
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
        /// <summary>
        /// 业务面板中的子面板类型
        /// </summary>
        //public  ListFormType ListType
        //{
        //    get;
        //    set;
        //}

        public  bool IsGridVisible
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
        /// 暂时不用 列表中业务所属的业务类型
        /// </summary>
        public BusinessType BusinessType
        {
            get
            {
               // return BusinessType.OE;
               // int businessTypeValue = Int32.Parse(this.gridViewList.GetFocusedDataRow()["Type"].ToString());
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
        private OperationMessageRelation operationMessageRelation;
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
        /// <summary>
        /// 过滤条件列名默认为"RefNO"
        /// </summary>
        private string conditionColumnName = "RefNO";

        /// <summary>
        /// 过滤条件列名
        /// </summary>
        public  string ConditionColumnName
        {
            get
            {
                return this.conditionColumnName;
            }
            set
            {
                this.conditionColumnName = value;
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
        private delegate void SetBusinessStyleDelegate(OperationMessageRelation relation);
        /// <summary>
        /// 添加当前邮件关联业务行样式
        /// </summary>
        /// <param name="message"></param>
        private void AddBusinessStyleFormatCondition(ICP.Message.ServiceInterface.Message message)
        {
            if (string.IsNullOrEmpty(message.MessageId))
                return;
            WaitCallback callback = (data) =>
            {

                try
                {
                    ICP.Message.ServiceInterface.Message currentMessage = data as ICP.Message.ServiceInterface.Message;
                    IOperationMessageRelationService operationMessageRelationService = ServiceClient.GetService<IOperationMessageRelationService>();
                    this.CurrentMessageRelation = operationMessageRelationService.GetByMessageId(currentMessage.MessageId);
                    if (!this.CurrentMessageRelation.HasData)
                    {
                        this.dicMessageRelation.Clear();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message);
                    return;
                }

                SetBusinessStyleDelegate businessStyleDelegate = new SetBusinessStyleDelegate(InnerAddBusinessStyleFormatCondition);
                this.Invoke(businessStyleDelegate, this.CurrentMessageRelation);


            };
            ThreadPool.QueueUserWorkItem(callback, message);
        }
        public BaseBusinessPart()
        {
            InitializeComponent();
        }
        public bool IsCurrentBusinessRelated()
        {
            return this.dicMessageRelation.ContainsValue(this.OperationNo);
        }
        /// <summary>
        /// 添加当前邮件关联业务行样式
        /// </summary>
        /// <param name="relation"></param>
        private void InnerAddBusinessStyleFormatCondition(OperationMessageRelation relation)
        {
            this.dicMessageRelation.Clear();
            if (!relation.HasData)
                return;
            if (CurrentDataSource == null || CurrentDataSource.Rows.Count <= 0)
                return;
            this.dicMessageRelation.Add(relation.MessageId.ToString(), relation.OperationNo);
            StyleFormatCondition rowCondition;
            if (this.gridViewList.FormatConditions["BusinessStyle"] == null)
            {
                rowCondition = new DevExpress.XtraGrid.StyleFormatCondition();
                rowCondition.Tag = "BusinessStyle";


                rowCondition.Appearance.Options.UseForeColor = true;
                rowCondition.Appearance.ForeColor = Color.Blue;

                rowCondition.Condition = FormatConditionEnum.Expression;
                rowCondition.ApplyToRow = true;
                rowCondition.Expression = GetBusinessExpression(relation);
                this.gridViewList.FormatConditions.Add(rowCondition);
            }
            else
            {
                rowCondition = this.gridViewList.FormatConditions["BusinessStyle"] as StyleFormatCondition;
                rowCondition.Expression = GetBusinessExpression(relation);
            }
            string operationNo = relation.OperationNo;
            string filterExpression = string.Format("NO='{0}'", operationNo);
            ChangeDataRowPosition(filterExpression);


        }
        private void ChangeDataRowPosition(string filterExpression)
        {
            DataTable dt = CurrentDataSource;

            DataRow[] rows = dt.Select(filterExpression);
            if (rows == null || rows.Length <= 0)
                return;
            int lenth = rows.Length;
            DataRow[] newRows = new DataRow[lenth];
            for (int i = 0; i < lenth; i++)
            {
                //if (dt.Rows.IndexOf(rows[i]) <= lenth - 1)
                //    continue;
                DataRow row = rows[i];
                DataRow newRow = dt.NewRow();
                newRow.ItemArray = row.ItemArray;
                dt.Rows.Remove(row);
                dt.Rows.InsertAt(newRow, 0);

            }

            AcceptChanges();
        }
        private string GetBusinessExpression(OperationMessageRelation relation)
        {
            return string.Format("[NO] == '{0}'", relation.OperationNo);
        }
        /// <summary>
        /// 添加标题单号匹配行样式
        /// </summary>
        private void AddNoRowStyleFormatCondition()
        {
            if (this.CurrentDataSource == null || this.CurrentDataSource.Rows.Count <= 0)
                return;
            StyleFormatCondition rowCondition;
            if (this.Nos == null || this.Nos.Count <= 0)
            {
                if (this.gridViewList.FormatConditions["NoStyle"] != null)
                {
                    rowCondition = this.gridViewList.FormatConditions["NoStyle"] as StyleFormatCondition;
                    rowCondition.Expression = "1 != 1";

                }
                return;
            }


            string filterExpression = GetExpression(this.Nos, this.ConditionColumnName, true);
            if (this.gridViewList.FormatConditions["NoStyle"] == null)
            {
                rowCondition = new DevExpress.XtraGrid.StyleFormatCondition();
                rowCondition.Tag = "NoStyle";
                rowCondition.Appearance.Font = new System.Drawing.Font(gridViewList.Appearance.Row.Font, System.Drawing.FontStyle.Bold);
                rowCondition.Appearance.Options.UseFont = true;
                rowCondition.Condition = FormatConditionEnum.Expression;
                rowCondition.ApplyToRow = true;
                rowCondition.Expression = filterExpression;
                this.gridViewList.FormatConditions.Add(rowCondition);
            }
            else
            {
                rowCondition = this.gridViewList.FormatConditions["NoStyle"] as StyleFormatCondition;
                rowCondition.Expression = filterExpression;
            }
            ChangeDataRowPosition(filterExpression);

        }

        private string GetExpression(List<string> list, string name, bool isValueList)
        {
            if (list == null || list.Count <= 0)
                return string.Empty;
            List<string> result = new List<string>();
            foreach (string no in list)
            {
                if (isValueList)
                {
                    result.Add(string.Format(" [{0}] like '%{1}%' ", name, no));
                }
                else
                {
                    result.Add(string.Format(" [{0}] like '%{1}%' ", no, name));
                }

            }
            return result.Aggregate((condition1, condition2) => condition1 + " Or " + condition2);
        }



        public void Init(Message.ServiceInterface.Message mail)
        {

          //  GetMailInfo(mail);
            RegisterExtensionSite();

            List<OperationToolbarCommand> commands = GetToolbarCommands();
            this.barManager.Images = ToobarItemFactory.ImageList;
            BuildToobar(commands);
            List<BusinessColumnInfo> columnInfos = GetColumnInfos();
            CreateColumns(columnInfos);

            BindData(mail);
            HookEvent();
        }
        public void Refersh()
        {
            ICP.Message.ServiceInterface.Message mail = new ICP.Message.ServiceInterface.Message();
            BindData(mail);
           // HookEvent();
        }

        private void HookEvent()
        {
        //    BusinessOperationHandleService.BusinessOperationCompleted += new EventHandler<CommonEventArgs<BusinessOperationParameter>>(OnBusinessOperationCompleted);
            IMainForm form = ServiceClient.GetClientService<IMainForm>();
            form.ApplicationExit += new EventHandler(OnApplicationExit);
        }


        /// <summary>
        ///注册拓展点
        /// </summary>
        private void RegisterExtensionSite()
        {//this.SourceType.ToString() + 
            string businessPartToolbarUISiteName = this.toolbarUISiteName;
            if (!WorkItem.UIExtensionSites.Contains(businessPartToolbarUISiteName))
            {
                WorkItem.UIExtensionSites.RegisterSite(businessPartToolbarUISiteName, this.barTool);
            }
            if (!WorkItem.UIExtensionSites.Contains(this.ContextMenuStripUISiteName))
            {
                WorkItem.UIExtensionSites.RegisterSite(this.ContextMenuStripUISiteName, this.contextMenuStrip);
            }
        }

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

        private void CreateColumns(List<BusinessColumnInfo> columnInfos)
        {
            if (columnInfos == null || columnInfos.Count <= 0)
                return;
            // this.SuspendLayout();
            RemoveEventHandler();
            // this.gridViewList.Columns.Clear();
            //this.gridControlList.RepositoryItems.Clear();

            // visibleIndex = 0;
           this.currentUserCustomGridInfo = GetUserCustomGridInfo();
           if (this.currentUserCustomGridInfo != null)
           {
               foreach (BusinessColumnInfo columnInfo in columnInfos)
               {
                   CustomColumnInfo columnCustomInfo = this.currentUserCustomGridInfo.Columns.Find(c => c.Name == "col"+columnInfo.Name);
                   if (columnCustomInfo!=null)
                    {
                       InnerCreateColumn(columnInfo, columnCustomInfo);
                    }
                  
               }
           }
            AddEventHandler();
            // this.ResumeLayout();

        }


        private void DisplayColumn(CustomColumnInfo columnInfo)
        {
            DevExpress.XtraGrid.Columns.GridColumn column = this.gridViewList.Columns.ColumnByName(columnInfo.Name);

            if (column == null) return;
            column.Visible = columnInfo.Visible;
            column.VisibleIndex = columnInfo.VisibleIndex;
            column.AbsoluteIndex = columnInfo.AbsoluteIndex;
            column.Fixed = (DevExpress.XtraGrid.Columns.FixedStyle)Enum.Parse(typeof(DevExpress.XtraGrid.Columns.FixedStyle), columnInfo.Fixed.ToString());
            column.Width = columnInfo.Width;

            if (!columnInfo.Visible)
            {
                this.gridViewList.Columns.ColumnByName(columnInfo.Name).Visible = columnInfo.Visible;
            }
        }

        private void InnerCreateColumn(BusinessColumnInfo columnInfo, CustomColumnInfo columnCustomInfo)
        {
            DevExpress.XtraGrid.Columns.GridColumn gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();

            gridColumn.OptionsColumn.AllowEdit = columnInfo.Editable;
            gridColumn.Visible = columnCustomInfo.Visible;
            gridColumn.VisibleIndex = columnCustomInfo.VisibleIndex;

            gridColumn.Fixed = (DevExpress.XtraGrid.Columns.FixedStyle)Enum.Parse(typeof(DevExpress.XtraGrid.Columns.FixedStyle), columnCustomInfo.Fixed.ToString());
            gridColumn.Width = columnCustomInfo.Width;

            //if (!columnCustomInfo.Visible)
            //{
            //    this.gridViewList.Columns.ColumnByName(columnInfo.Name).Visible = columnCustomInfo.Visible;
            //}
            gridColumn.Caption = columnInfo.Caption;
            gridColumn.FieldName = columnInfo.FieldName;
            gridColumn.Name = columnInfo.Name;
            DevExpress.XtraEditors.Repository.RepositoryItem columnEdit = ColumnEditFactory.GetColumnEdit(columnInfo);
            this.gridControlList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { columnEdit });

            gridColumn.ColumnEdit = columnEdit;

            this.gridViewList.Columns.Add(gridColumn);

            gridColumn.AbsoluteIndex = columnCustomInfo.AbsoluteIndex;


        }
        public void BindData(ICP.Message.ServiceInterface.Message mail)
        {
            PreBindData(mail);
            BusinessQueryCriteria criteria = null;
            if (this.NeedBindData)
            {
                criteria = GetQueryCriteria();
            }
            WaitCallback callback = (data) =>
            {
                DataTable dt = GetData(data as BusinessQueryCriteria);
                InnerBindData(dt);
            };
            ThreadPool.QueueUserWorkItem(callback, criteria);

            //}
        }
        public void ResetMessageRelationRecord()
        {
            this.dicMessageRelation.Clear();
        }
        public void DynamicBindData(ListFormType? gridType)
        {
            ResetMessageRelationRecord();
            if (gridType == null)
            {
                DataTable dtTemp = new DataTable();
                InnerBindData(dtTemp);
                return;
            }
            BusinessQueryCriteria criteria = GetQueryCriteria();

            // criteria.TemplateCode = gridType.ToString();
            DataTable dt = GetData(criteria);
            AddCopyPathColumn(dt);
            InnerBindData(dt);
            AddNoRowStyleFormatCondition();
            AddBusinessStyleFormatCondition(this.CurrentMessage);
        }
        /// <summary>
        /// 向数据库返回的表结构添加自定义列
        /// </summary>
        /// <param name="dt"></param>
        public virtual void AddCopyPathColumn(DataTable dt)
        {

        }
        public virtual void PreBindData(ICP.Message.ServiceInterface.Message mail)
        {
            this.CurrentMessage = mail;
            GetMailInfo(mail);
            AddNoRowStyleFormatCondition();
            //this.currentUserCustomGridInfo = null;
        }
        public DataTable GetData(BusinessQueryCriteria criteria)
        {
            DataTable dt = new DataTable();
            if (criteria != null)
            {
                //实际用
                dt = ServiceClient.GetService<IOperationViewService>().Get(criteria);

             

            }
            else
            {
                //测试用
                dt = new DataTable();

                dt.Columns.Add("ID");
                dt.Columns.Add("NO");
                dt.Columns.Add("Type");
                dt.Columns.Add("UpDateDate");
                dt.Columns.Add("IsValid");


                for (int i = 1; i < 100; i++)
                {
                    DataRow dr = dt.NewRow();

                    dr["ID"] = Guid.NewGuid().ToString();
                    dr["NO"] = Guid.NewGuid().ToString();
                    dr["Type"] = i;
                    dr["UpDateDate"] = DateTime.Now;
                    dr["IsValid"] = 1;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        delegate void DataBindDelegate(DataTable dt, bool needBindData, ICP.Message.ServiceInterface.Message mail);
        private void InnerBindData(DataTable dt)
        {

            DataBindDelegate bindDelegate = new DataBindDelegate(InnerBindData2);
            this.Invoke(bindDelegate, dt, this.NeedBindData, this.CurrentMessage);

        }
        private void InnerBindData2(DataTable dt, bool needBindData, ICP.Message.ServiceInterface.Message mail)
        {
            this.bindingSource.DataSource = dt;
            (this.bindingSource.DataSource as DataTable).AcceptChanges();
            this.bindingSource.EndEdit();
            ResetQueryRecord();
            if (needBindData)
            {
                AddBusinessStyleFormatCondition(mail);
            }
        }


        private void ResetQueryRecord()
        {
            this.previousQueryNo = string.Empty;
            this.previousMatchRows = null;
            this.previousSelectRowIndex = -1;
        }

        private BusinessQueryCriteria GetQueryCriteria()
        {
            BusinessQueryCriteria criteria = new BusinessQueryCriteria();
           // criteria.EmailAddress = this.SenderEmailAddress;
            criteria.SourceType = this.SourceType;
            criteria.companyIDs = string.IsNullOrEmpty(this.selectedCompanyIds) ? null : this.selectedCompanyIds.Split(',').Select(id => new Guid(id)).ToArray();

            criteria.TemplateCode = TemplateCode;
            return criteria;
        }
        /// <summary>
        /// 从邮件信息抽取信息（包括单号，来源类型，发送人地址）
        /// </summary>
        /// <param name="mail"></param>
        private void GetMailInfo(ICP.Message.ServiceInterface.Message mail)
        {
           // MailInfoGetter.ExtractMailInfo(mail, this.GetType().Name);
            this.SourceType = MailInfoGetter.SourceType;
            this.Nos = MailInfoGetter.Nos;
            this.SenderEmailAddress = MailInfoGetter.EmailAddress;
            this.currentMessage = mail;
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

        }

        private void CreateContextMenuStripItems(List<ContextMenuItemInfo> items)
        {
            if (!isCommonContextMenuItemsExists)
            {
                CreateCommonContextMenuStripItems();
                isCommonContextMenuItemsExists = true;
            }
            CreateBusinessContextMenuStripItems(items);
        }

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
            foreach (ContextMenuItemInfo item in itemInfos)
            {
                ToolStripItem toolStripItem = ToolStripItemFactory.GetToolStripItem(item);
                if (!string.IsNullOrEmpty(item.Name))
                {

                    WorkItem.Commands[item.Name].AddInvoker(toolStripItem, "Click");
                }
                if (!isBusiness)
                {

                    WorkItem.UIExtensionSites[this.ContextMenuStripUISiteName].Add(toolStripItem);
                }
                else
                {
                    toolStripItem.Tag = item.BusinessNo;
                    WorkItem.UIExtensionSites[this.ContextMenuStripUISiteName].Insert(0, toolStripItem);
                }

                if (!string.IsNullOrEmpty(item.RegisterSite) && !WorkItem.UIExtensionSites.Contains(item.RegisterSite))
                {
                    WorkItem.UIExtensionSites.RegisterSite(item.RegisterSite, toolStripItem);
                }
            }
        }
        void gridViewList_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1));
            }

        }
        private bool isColumnSettingChanged = false;
        void OnColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            isColumnSettingChanged = true;
        }

        void OnColumnPositionChanged(object sender, EventArgs e)
        {
            isColumnSettingChanged = true;
        }
        private void RemoveEventHandler()
        {
            this.gridViewList.ColumnPositionChanged -= OnColumnPositionChanged;
            this.gridViewList.ColumnWidthChanged -= OnColumnWidthChanged;
        }
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
        public UserCustomGridInfo GetUserCustomGridInfo()
        {
            UserCustomGridInfo customInfo = ServiceClient.GetService<IClientCustomDataGridService>().Get(GetListFormType());
           
            return customInfo;
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

        public EmailSourceType SourceType
        {
            get;
            set;
        }
        //public abstract List<OperationToolbarCommand> GetToolbarCommands();
        //public abstract List<BusinessColumnInfo> GetColumnInfos();
        //public abstract List<ContextMenuItemInfo> GetContextMenuItems(DataRow row);
        //public abstract List<ContextMenuItemInfo> GetBusinessRelatedContextMenuItems(DataRow row);

        //public abstract List<string> GetQueryColumnNames();

        protected  Dictionary<string, object> InsertInitValues(Dictionary<string, object> dic)
        {
            return dic;
        }

        //public OperationType GetOperationType()
        //{
        //    return OperationType.OceanExport;
        //}
        private DialogResult PromptChangeRelation()
        {
            string messageId = this.CurrentMessage.MessageId;
            string tip = IsEnglish ? "" : "";
            if (dicMessageRelation.ContainsKey(messageId))
            {
                string oldOperationNo = this.dicMessageRelation[messageId];
                tip = IsEnglish ? string.Format("The current mail is connected to {0},", oldOperationNo) + Environment.NewLine + string.Format("Do you want to re-connect it to {0}?", this.OperationNo) : string.Format("当前邮件已关联到业务:{0},", oldOperationNo) + Environment.NewLine + string.Format("你想重新关联到业务:{0}吗?", this.OperationNo);
            }
            else
            {
                tip = IsEnglish ? string.Format("Do you want to connect the current mail to {0}?", this.OperationNo) : string.Format("你想关联当前邮件到当前业务:{0}", this.OperationNo);
            }

            return XtraMessageBox.Show(tip, "Tips", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
        }
        #region 命令处理
        /// <summary>
        /// 关联当前业务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        public void Command_EmailOperationMessageRelation(object sender, EventArgs e)
        {
            if (CurrentDataSource == null || CurrentRow == null)
                return;
            if (dicMessageRelation.ContainsValue(this.OperationNo))
                return;
            string messageId = this.CurrentMessage.MessageId;

            DialogResult result = PromptChangeRelation();
            if (result == DialogResult.OK)
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    OperationMessageRelation relation;
                    if (!this.CurrentMessageRelation.HasData)
                    {
                        this.CurrentMessage.UserProperties = new MessageUserPropertiesObject();
                        this.CurrentMessage.UserProperties.OperationId = this.ID;
                        this.CurrentMessage.UserProperties.OperationType = OperationType;
                        this.CurrentMessage.Type = MessageType.Email;
                        ServiceClient.GetService<IClientMessageService>().Save(this.CurrentMessage);

                        relation = new OperationMessageRelation { HasData = true, OperationID = this.ID, IMessageId = this.CurrentMessage.Id, ID = Guid.NewGuid(), OperationNo = this.OperationNo, MessageId = messageId };

                    }
                    else
                    {
                        relation = this.CurrentMessageRelation;
                        relation.OperationNo = this.OperationNo;
                        relation.OperationID = this.ID;
                        relation.OperationType = OperationType;
                        relation.HasData = true;
                        SaveRelation(relation);
                    }

                    this.CurrentMessageRelation = relation;
                    dicMessageRelation.Clear();
                    dicMessageRelation.Add(messageId, this.OperationNo);
                    InnerAddBusinessStyleFormatCondition(relation);
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
                   // SingleResult result = ServiceClient.GetService<IOperationMessageRelationService>().SaveOperationMailMessage(relation);
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
            XtraMessageBox.Show(ex.Message);
        }
        bool isFirstTimeEnter = true;
        /// <summary>
        /// 下拉多选业务所属公司
        /// <remarks>加载时默认为所有公司</remarks>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Command_EmailSelectCompany(object sender, EventArgs e)
        {
            if (isFirstTimeEnter)
            {
                isFirstTimeEnter = false;
                return;
            }

            using (new CursorHelper(Cursors.WaitCursor))
            {
                ListFormType formType = GetListFormType();
                if (formType == ListFormType.MailLink4Carrier)
                    return;
                Command command = sender as Command;
                if (command == null)
                    return;
                List<RepositoryItem> items = command.FindAdapters<RepositoryItemCommandAdapter>().Last().Invokers.Keys.ToList();
                if (items == null || items.Count <= 0)
                    return;
                RepositoryItemCheckedComboBoxEdit item = items.Last() as RepositoryItemCheckedComboBoxEdit;
                if (item == null)
                {
                    return;
                }
                string checkedItems = item.GetCheckedItems().ToString();
                if (selectedCompanyIds == checkedItems)
                {
                    return;
                }

                selectedCompanyIds = checkedItems;
                BusinessQueryCriteria criteria = GetQueryCriteria();
                DataTable dt = GetData(criteria);
                InnerBindData(dt);
            }

        }

        public void Command_EmailAddOE(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                //ClientHelper.HideForm(LocalData.MainFormHandle);
                Dictionary<string, object> dic = CreateBusinessParameter(ICP.Common.ServiceInterface.DataObjects.ActionType.Create);
                FCMCommonOperationService.CreateSO(dic);
            }
        }

        public Dictionary<string, object> CreateBusinessParameter(ICP.Common.ServiceInterface.DataObjects.ActionType actionType)
        {
            BusinessOperationParameter businessOperation = new BusinessOperationParameter();
            businessOperation.BusinessPartType = ListType;
            businessOperation.Message = this.currentMessage;
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
        public void OnBusinessOperationCompleted(object sender, CommonEventArgs<BusinessOperationParameter> e)
        {

            //BusinessOperationParameter parameter = e.Data;
            //if (parameter == null) return;
            //if (parameter.BusinessPartType != MailUtility.CurrentListFormType || this.CurrentMessage.MessageId != parameter.Message.MessageId)
            //    return;
            //OperationType operationType = parameter.Context.OperationType;

            //BusinessOperationContext context = parameter.Context;
            //FormType bltype = parameter.Context.FormType;
            //Guid operationId = parameter.Context.OperationID;
            //string operationNo = parameter.Context.OpeationNO;
            //List<string> FilesName = parameter.FilesName;

            //object[] data = parameter.Data;
            //object formInfo = data[0];
            //DataRow targetRow = null;
            //if (parameter.ActionType == ICP.Common.ServiceInterface.DataObjects.ActionType.Create)
            //{
            //    targetRow = CurrentDataSource.NewRow();
            //}
            //else if (parameter.ActionType == ICP.Common.ServiceInterface.DataObjects.ActionType.Edit)
            //{
            //    targetRow = CurrentDataSource.Select(string.Format("ID='{0}'", context.OperationID)).First();
            //}
            ////IGetDescriptionCommand getDescriptionCommand = GetBillInfoFactory.GetDescriptionCommand(data[0]);
            ////IGetRefNoCommand getRefNoCommand = GetBillInfoFactory.GetRefNoCommand(data[0]);

            //#region 客户面板数据刷新
            //if (this.ListType == ListFormType.MailLink4Customer)
            //{
            //    if (context.FormType == FormType.ShippingOrder)
            //    {
            //        OceanBookingInfo billInfo = parameter.Data[0] as OceanBookingInfo;
            //        targetRow["ID"] = operationId;
            //        targetRow["NO"] = operationNo;
            //        targetRow["IsValid"] = billInfo.IsValid;
            //        targetRow["State"] = billInfo.State;
            //        if (billInfo.UpdateDate == null) targetRow["UpdateDate"] = DBNull.Value;
            //        else targetRow["UpdateDate"] = billInfo.UpdateDate;

            //        targetRow["RefNO"] = getRefNoCommand.Get(billInfo);
            //        targetRow["Description"] = getDescriptionCommand.Get(billInfo);
            //    }
            //    else if (context.FormType == FormType.MBL)
            //    {
            //        OceanMBLInfo mblInfo = parameter.Data[0] as OceanMBLInfo;
            //        targetRow["ID"] = operationId;
            //        targetRow["NO"] = operationNo;
            //        targetRow["IsValid"] = mblInfo.IsValid;
            //        targetRow["State"] = mblInfo.State;
            //        if (mblInfo.UpdateDate == null) targetRow["UpdateDate"] = DBNull.Value;
            //        else targetRow["UpdateDate"] = mblInfo.UpdateDate;
            //        targetRow["RefNO"] = getRefNoCommand.Get(mblInfo);
            //        targetRow["Description"] = getDescriptionCommand.Get(mblInfo);
            //        //ListDictionary<string, string> dicProperties = new ListDictionary<string, string>();

            //        //dicProperties.Add("ID", "ID");
            //        //dicProperties.Add("NO", "NO");
            //        //dicProperties.Add("IsValid", "IsValid");
            //        //dicProperties.Add("State", "State");
            //        //dicProperties["RefNO"] = new List<string>{"SONO","ContainerNos",};
            //        //dicProperties["Description"] = new List<string> { "POLName", "PODName", "ETD", "ETA" };
            //        //dicProperties.Add("UpdateDate", "UpdateDate");
            //    }
            //    else if (context.FormType == FormType.HBL)
            //    {
            //        OceanHBLInfo hblInfo = parameter.Data[0] as OceanHBLInfo;
            //        targetRow["ID"] = operationId;
            //        targetRow["NO"] = operationNo;
            //        targetRow["IsValid"] = hblInfo.IsValid;
            //        targetRow["State"] = hblInfo.State;
            //        if (hblInfo.UpdateDate == null) targetRow["UpdateDate"] = DBNull.Value;
            //        else targetRow["UpdateDate"] = hblInfo.UpdateDate;
            //        targetRow["RefNO"] = getRefNoCommand.Get(hblInfo);
            //        targetRow["Description"] = getDescriptionCommand.Get(hblInfo);
            //    }
            //    else if (context.FormType == FormType.Customs)
            //    {
            //        OceanCustoms customes = parameter.Data[0] as OceanCustoms;
            //        targetRow["ID"] = operationId;
            //        targetRow["NO"] = operationNo;
            //        targetRow["IsValid"] = true;
            //        targetRow["State"] = true;
            //        if (customes.UpdateDate == null) targetRow["UpdateDate"] = DBNull.Value;
            //        else targetRow["UpdateDate"] = customes.UpdateDate;
            //        targetRow["RefNO"] = string.Format("SO:{0}", operationNo);
            //        targetRow["Description"] = string.Empty;
            //    }
            //    else if (context.FormType == FormType.Truck)
            //    {
            //        OceanTruckInfo truckInfo = parameter.Data[0] as OceanTruckInfo;
            //        targetRow["ID"] = operationId;
            //        targetRow["NO"] = operationNo;
            //        targetRow["IsValid"] = true;
            //        targetRow["State"] = true;
            //        if (truckInfo.UpdateDate == null) targetRow["UpdateDate"] = DBNull.Value;
            //        else targetRow["UpdateDate"] = truckInfo.UpdateDate;
            //        targetRow["RefNO"] = string.Format("SO:{0}", truckInfo.ShippingOrderNo);
            //        targetRow["Description"] = string.Empty;
            //    }
            //}
            //#endregion

            //#region   承运人SO面板数据刷新
            //else if (this.ListType == ListFormType.MailLink4CarrierSO)
            //{
            //    // SOCopy,SONO,Vessel,Voyage,ContainerDesc,BLNO,Carrier,POL,POD,ID,NO,UpdateDate,IsValid,RefNO
            //    string strSOCopy = GetDocumentsName(FilesName);

            //    if (context.FormType == FormType.ShippingOrder)
            //    {
            //        OceanBookingInfo billInfo = parameter.Data[0] as OceanBookingInfo;

            //        targetRow["SOCopy"] = strSOCopy;
            //        targetRow["SONO"] = billInfo.No;
            //        targetRow["Vessel"] = billInfo.VesselVoyage;
            //        targetRow["Voyage"] = billInfo.VoyageName;
            //        targetRow["ContainerDesc"] = billInfo.ContainerDescription;

            //        targetRow["BLNO"] = string.Empty;
            //        targetRow["Carrier"] = billInfo.CarrierName;
            //        targetRow["POL"] = billInfo.POLName;
            //        targetRow["POD"] = billInfo.PODName;
            //        targetRow["NO"] = operationNo;
            //        targetRow["ID"] = operationId;
            //        targetRow["UpdateDate"] = billInfo.UpdateDate;

            //        targetRow["IsValid"] = true;
            //        targetRow["RefNO"] = getRefNoCommand.Get(billInfo);
            //    }
            //    else if (context.FormType == FormType.MBL)
            //    {
            //        OceanMBLInfo mblInfo = parameter.Data[0] as OceanMBLInfo;
            //        targetRow["SOCopy"] = strSOCopy;
            //        targetRow["SONO"] = mblInfo.SONO;
            //        targetRow["Vessel"] = mblInfo.VesselVoyage;
            //        targetRow["Voyage"] = mblInfo.VesselVoyage;
            //        targetRow["ContainerDesc"] = mblInfo.ContainerQtyDescription;

            //        targetRow["BLNO"] = string.Empty;
            //        targetRow["Carrier"] = mblInfo.CarrierName;

            //        targetRow["POL"] = mblInfo.POLName;
            //        targetRow["POD"] = mblInfo.PODName;
            //        targetRow["NO"] = operationNo;
            //        targetRow["ID"] = operationId;
            //        targetRow["UpdateDate"] = mblInfo.UpdateDate;

            //        targetRow["IsValid"] = true;
            //        targetRow["RefNO"] = getRefNoCommand.Get(mblInfo);
            //    }
            //    else if (context.FormType == FormType.HBL)
            //    {
            //        OceanHBLInfo hblInfo = parameter.Data[0] as OceanHBLInfo;
            //        targetRow["SOCopy"] = strSOCopy;
            //        targetRow["SONO"] = hblInfo.SONO;
            //        targetRow["Vessel"] = hblInfo.VesselVoyage;
            //        targetRow["Voyage"] = hblInfo.VesselVoyage;
            //        targetRow["ContainerDesc"] = hblInfo.ContainerQtyDescription;

            //        targetRow["BLNO"] = string.Empty;
            //        targetRow["Carrier"] = hblInfo.CarrierName;

            //        targetRow["POL"] = hblInfo.POLName;
            //        targetRow["POD"] = hblInfo.PODName;
            //        targetRow["NO"] = operationNo;
            //        targetRow["ID"] = operationId;
            //        targetRow["UpdateDate"] = hblInfo.UpdateDate;

            //        targetRow["IsValid"] = true;
            //        targetRow["RefNO"] = getRefNoCommand.Get(hblInfo);
            //    }
            //    else if (context.FormType == FormType.Customs)
            //    {
            //        OceanCustoms customes = parameter.Data[0] as OceanCustoms;
            //        targetRow["SOCopy"] = strSOCopy;
            //        targetRow["SONO"] = string.Empty;
            //        targetRow["Vessel"] = string.Empty;
            //        targetRow["Voyage"] = string.Empty;
            //        targetRow["ContainerDesc"] = string.Empty;

            //        targetRow["BLNO"] = string.Empty;
            //        targetRow["Carrier"] = string.Empty;

            //        targetRow["POL"] = string.Empty;
            //        targetRow["POD"] = string.Empty;
            //        targetRow["NO"] = operationNo;
            //        targetRow["ID"] = operationId;
            //        targetRow["UpdateDate"] = string.Empty;

            //        targetRow["IsValid"] = true;
            //        targetRow["RefNO"] = string.Format("SO:{0}", operationNo);

            //    }
            //    else if (context.FormType == FormType.Truck)
            //    {
            //        OceanTruckInfo truckInfo = parameter.Data[0] as OceanTruckInfo;
            //        targetRow["SOCopy"] = strSOCopy;
            //        targetRow["SONO"] = string.Empty;
            //        targetRow["Vessel"] = truckInfo.VesselVoyage;
            //        targetRow["Voyage"] = truckInfo.VesselVoyage;
            //        targetRow["ContainerDesc"] = string.Empty;

            //        targetRow["BLNO"] = string.Empty;
            //        targetRow["Carrier"] = truckInfo.CarrierName;

            //        targetRow["POL"] = string.Empty;
            //        targetRow["POD"] = string.Empty;
            //        targetRow["NO"] = operationNo;
            //        targetRow["ID"] = operationId;
            //        targetRow["UpdateDate"] = truckInfo.UpdateDate;

            //        targetRow["IsValid"] = true;
            //        targetRow["RefNO"] = string.Format("SO:{0}", truckInfo.ShippingOrderNo);

            //    }
            //}
            //#endregion

            //#region 承运人MBL面板数据刷新
            //else if (this.ListType == ListFormType.MailLink4CarrierMBL)
            //{
            //    //MBLCopy,APCopy,Vessel,Voyage,BLNO,ContainerDesc,Carrier,POL,POD,ID,NO,UpdateDate,IsValid,RefNO
            //    string strMBLCopy = GetDocumentsName(FilesName);

            //    if (context.FormType == FormType.ShippingOrder)
            //    {

            //        OceanBookingInfo billInfo = parameter.Data[0] as OceanBookingInfo;
            //        targetRow["MBLCopy"] = strMBLCopy;
            //        targetRow["APCopy"] = string.Empty;
            //        targetRow["Vessel"] = billInfo.VesselVoyage;
            //        targetRow["Voyage"] = billInfo.VoyageName;
            //        targetRow["ContainerDesc"] = billInfo.ContainerDescription;

            //        targetRow["BLNO"] = string.Empty;
            //        targetRow["Carrier"] = billInfo.CarrierName;

            //        targetRow["POL"] = billInfo.POLName;
            //        targetRow["POD"] = billInfo.PODName;
            //        targetRow["NO"] = billInfo.No;
            //        targetRow["ID"] = billInfo.ID;
            //        targetRow["UpdateDate"] = billInfo.UpdateDate;

            //        targetRow["IsValid"] = true;
            //        targetRow["RefNO"] = getRefNoCommand.Get(billInfo);
            //    }
            //    else if (context.FormType == FormType.MBL)
            //    {
            //        OceanMBLInfo mblInfo = parameter.Data[0] as OceanMBLInfo;
            //        targetRow["MBLCopy"] = strMBLCopy;
            //        targetRow["APCopy"] = string.Empty;
            //        targetRow["Vessel"] = mblInfo.VesselVoyage;
            //        targetRow["Voyage"] = mblInfo.VesselVoyage;
            //        targetRow["ContainerDesc"] = mblInfo.ContainerQtyDescription;

            //        targetRow["BLNO"] = string.Empty;
            //        targetRow["Carrier"] = mblInfo.CarrierName;

            //        targetRow["POL"] = mblInfo.POLName;
            //        targetRow["POD"] = mblInfo.PODName;
            //        targetRow["NO"] = mblInfo.No;
            //        targetRow["ID"] = mblInfo.ID;
            //        targetRow["UpdateDate"] = mblInfo.UpdateDate;

            //        targetRow["IsValid"] = true;
            //        targetRow["RefNO"] = getRefNoCommand.Get(mblInfo);
            //    }
            //    else if (context.FormType == FormType.HBL)
            //    {
            //        OceanHBLInfo hblInfo = parameter.Data[0] as OceanHBLInfo;
            //        targetRow["MBLCopy"] = strMBLCopy;
            //        targetRow["APCopy"] = string.Empty;
            //        targetRow["Vessel"] = hblInfo.VesselVoyage;
            //        targetRow["Voyage"] = hblInfo.VesselVoyage;
            //        targetRow["ContainerDesc"] = hblInfo.ContainerQtyDescription;

            //        targetRow["BLNO"] = string.Empty;
            //        targetRow["Carrier"] = hblInfo.CarrierName;

            //        targetRow["POL"] = hblInfo.POLName;
            //        targetRow["POD"] = hblInfo.PODName;
            //        targetRow["NO"] = hblInfo.No;
            //        targetRow["ID"] = hblInfo.ID;
            //        targetRow["UpdateDate"] = hblInfo.UpdateDate;

            //        targetRow["IsValid"] = true;
            //        targetRow["RefNO"] = getRefNoCommand.Get(hblInfo);
            //    }
            //    else if (context.FormType == FormType.Customs)
            //    {
            //        OceanCustoms customes = parameter.Data[0] as OceanCustoms;
            //        targetRow["MBLCopy"] = strMBLCopy;
            //        targetRow["APCopy"] = string.Empty;
            //        targetRow["Vessel"] = string.Empty;
            //        targetRow["Voyage"] = string.Empty;
            //        targetRow["ContainerDesc"] = string.Empty;

            //        targetRow["BLNO"] = string.Empty;
            //        targetRow["Carrier"] = string.Empty;

            //        targetRow["POL"] = string.Empty;
            //        targetRow["POD"] = string.Empty;
            //        targetRow["NO"] = operationNo;
            //        targetRow["ID"] = operationId;
            //        targetRow["UpdateDate"] = customes.UpdateDate;

            //        targetRow["IsValid"] = true;
            //        targetRow["RefNO"] = string.Empty;
            //    }
            //    else if (context.FormType == FormType.Truck)
            //    {
            //        OceanTruckInfo truckInfo = parameter.Data[0] as OceanTruckInfo;
            //        targetRow["MBLCopy"] = strMBLCopy;
            //        targetRow["APCopy"] = string.Empty;
            //        targetRow["Vessel"] = truckInfo.VesselVoyage;
            //        targetRow["Voyage"] = truckInfo.VesselVoyage;
            //        targetRow["ContainerDesc"] = string.Empty;

            //        targetRow["BLNO"] = string.Empty;
            //        targetRow["Carrier"] = truckInfo.CarrierName;

            //        targetRow["POL"] = string.Empty;
            //        targetRow["POD"] = string.Empty;
            //        targetRow["NO"] = operationNo;
            //        targetRow["ID"] = operationId;
            //        targetRow["UpdateDate"] = truckInfo.UpdateDate;

            //        targetRow["IsValid"] = true;
            //        targetRow["RefNO"] = string.Format("SO:{0}", truckInfo.ShippingOrderNo);
            //    }
            //}
            //#endregion

            //#region 承运人AP面板数据刷新
            //else if (this.ListType == ListFormType.MailLink4CarrierAP)
            //{
            //    //APCopy,BLNO,Vessel,Voyage,ContainerDesc,Carrier,POL,POD,ID,NO,UpdateDate,IsValid,RefNO

            //    string strAPCopy = GetDocumentsName(FilesName);

            //    if (context.FormType == FormType.ShippingOrder)
            //    {
            //        OceanBookingInfo billInfo = parameter.Data[0] as OceanBookingInfo;
            //        targetRow["APCopy"] = strAPCopy;
            //        targetRow["BLNO"] = billInfo.No;
            //        targetRow["Vessel"] = billInfo.VesselVoyage;
            //        targetRow["Voyage"] = billInfo.VoyageName;
            //        targetRow["ContainerDesc"] = billInfo.ContainerDescription;

            //        targetRow["BLNO"] = string.Empty;
            //        targetRow["Carrier"] = billInfo.CarrierName;

            //        targetRow["POL"] = billInfo.POLName;
            //        targetRow["POD"] = billInfo.PODName;
            //        targetRow["NO"] = operationNo;
            //        targetRow["ID"] = operationId;
            //        targetRow["UpdateDate"] = billInfo.UpdateDate;

            //        targetRow["IsValid"] = true;
            //        targetRow["RefNO"] = getRefNoCommand.Get(billInfo);
            //    }
            //    else if (context.FormType == FormType.MBL)
            //    {
            //        OceanMBLInfo mblInfo = parameter.Data[0] as OceanMBLInfo;
            //        targetRow["APCopy"] = strAPCopy;
            //        targetRow["BLNO"] = mblInfo.No;
            //        targetRow["Vessel"] = mblInfo.VesselVoyage;
            //        targetRow["Voyage"] = mblInfo.VesselVoyage;
            //        targetRow["ContainerDesc"] = mblInfo.ContainerQtyDescription;

            //        targetRow["BLNO"] = string.Empty;
            //        targetRow["Carrier"] = mblInfo.CarrierName;

            //        targetRow["POL"] = mblInfo.POLName;
            //        targetRow["POD"] = mblInfo.PODName;
            //        targetRow["NO"] = operationNo;
            //        targetRow["ID"] = operationId;
            //        targetRow["UpdateDate"] = mblInfo.UpdateDate;

            //        targetRow["IsValid"] = true;
            //        targetRow["RefNO"] = getRefNoCommand.Get(mblInfo);
            //    }
            //    else if (context.FormType == FormType.HBL)
            //    {
            //        OceanHBLInfo hblInfo = parameter.Data[0] as OceanHBLInfo;
            //        targetRow["APCopy"] = strAPCopy;
            //        targetRow["BLNO"] = hblInfo.No;
            //        targetRow["Vessel"] = hblInfo.VesselVoyage;
            //        targetRow["Voyage"] = hblInfo.VesselVoyage;
            //        targetRow["ContainerDesc"] = hblInfo.ContainerQtyDescription;

            //        targetRow["BLNO"] = string.Empty;
            //        targetRow["Carrier"] = hblInfo.CarrierName;

            //        targetRow["POL"] = hblInfo.POLName;
            //        targetRow["POD"] = hblInfo.PODName;
            //        targetRow["NO"] = operationNo;
            //        targetRow["ID"] = operationId;
            //        targetRow["UpdateDate"] = hblInfo.UpdateDate;

            //        targetRow["IsValid"] = true;
            //        targetRow["RefNO"] = getRefNoCommand.Get(hblInfo);
            //    }
            //    else if (context.FormType == FormType.Customs)
            //    {
            //        OceanCustoms customes = parameter.Data[0] as OceanCustoms;
            //        targetRow["APCopy"] = strAPCopy;
            //        targetRow["BLNO"] = string.Empty;
            //        targetRow["Vessel"] = string.Empty;
            //        targetRow["Voyage"] = string.Empty;
            //        targetRow["ContainerDesc"] = string.Empty;

            //        targetRow["BLNO"] = string.Empty;
            //        targetRow["Carrier"] = string.Empty;

            //        targetRow["POL"] = string.Empty;
            //        targetRow["POD"] = string.Empty;
            //        targetRow["NO"] = operationNo;
            //        targetRow["ID"] = operationId;
            //        targetRow["UpdateDate"] = customes.UpdateDate;

            //        targetRow["IsValid"] = true;
            //        targetRow["RefNO"] = string.Empty;
            //    }
            //    else if (context.FormType == FormType.Truck)
            //    {
            //        OceanTruckInfo truckInfo = parameter.Data[0] as OceanTruckInfo;
            //        targetRow["APCopy"] = strAPCopy;
            //        targetRow["BLNO"] = string.Empty;
            //        targetRow["Vessel"] = truckInfo.VesselVoyage;
            //        targetRow["Voyage"] = truckInfo.VesselVoyage;
            //        targetRow["ContainerDesc"] = truckInfo.ContainerDescription;

            //        targetRow["BLNO"] = string.Empty;
            //        targetRow["Carrier"] = truckInfo.CarrierName;

            //        targetRow["POL"] = string.Empty;
            //        targetRow["POD"] = string.Empty;
            //        targetRow["NO"] = operationNo;
            //        targetRow["ID"] = operationId;
            //        targetRow["UpdateDate"] = truckInfo.UpdateDate;

            //        targetRow["IsValid"] = true;
            //        targetRow["RefNO"] = string.Format("SO:{0}", truckInfo.ShippingOrderNo);
            //    }

            //}
            //#endregion

            //if (parameter.ActionType == ICP.Common.ServiceInterface.DataObjects.ActionType.Create)
            //{
            //    CurrentDataSource.Rows.Add(targetRow);
            //}

            //ResetBinding();
            ////SaveMessage(parameter.Context, parameter.Message);



            ////switch (operationType)
            ////{
            ////    case OperationType.AirExport:
            ////        //AirBookingInfo airExportInfo = formInfo as AirBookingInfo;
            ////       // EditPartSaved(airExportInfo.ID, operationType);
            ////        HandleBusinessOperation<AirBookingInfo>(formInfo, "ID", operationType);
            ////        break;
            ////    case OperationType.AirImport:
            ////        AirBookingInfo airImport = formInfo as AirBookingInfo;
            ////        EditPartSaved(airImport.ID, operationType);
            ////        break;
            ////    case OperationType.OceanExport:
            ////        if (bltype == FormType.HBL)
            ////        {
            ////            OceanHBLInfo hblinfo = formInfo as OceanHBLInfo;
            ////            EditPartSaved(hblinfo.OceanBookingID, operationType);
            ////            break;
            ////        }
            ////        else if (bltype == FormType.MBL)
            ////        {
            ////            OceanMBLInfo mblinfo = formInfo as OceanMBLInfo;
            ////            EditPartSaved(mblinfo.OceanBookingID, operationType);
            ////            break;
            ////        }
            ////        OceanBookingInfo oceanExportInfo = formInfo as OceanBookingInfo;
            ////        EditPartSaved(oceanExportInfo.ID, operationType);
            ////        break;
            ////    case OperationType.OceanImport:
            ////        OceanBookingInfo oceanImportInfo = formInfo as OceanBookingInfo;
            ////        EditPartSaved(oceanImportInfo.ID, operationType);
            ////        break;
            ////    case OperationType.Other:
            ////        OtherBusinessInfo otherInfo = formInfo as OtherBusinessInfo;

            ////        EditPartSaved(otherInfo.ID, operationType);
            ////        break;
            ////}

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

            relation = ServiceClient.GetService<IOperationMessageRelationService>().GetByMessageId(message.MessageId);

            if (relation != null)
            {
                DialogResult dlg = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Are you sure?" : string.Format("当前邮件之前已关联业务号{0}，是否确认重新关联业务?", relation.OperationNo), LocalData.IsEnglish ? "Confirm" : "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
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



        public void Command_EmailAdvanceQuery(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                //Dictionary<string, object> initValues = new Dictionary<string, object>();
                //initValues.Add(ICP.MailCenter.Business.ServiceInterface.Constants.BusinessTypeKey, this.BusinessType);
                //initValues.Add(Constants.OperationViewCodeKey, this.TemplateCode);
                //InsertInitValues(initValues);
                //frmAdvanceQuery frmQuery = this.WorkItem.Items.AddNew<frmAdvanceQuery>();
                //frmQuery.Init(initValues);
                //frmQuery.MaximizeBox = frmQuery.MinimizeBox = false;
                //frmQuery.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                //DialogResult result = frmQuery.ShowDialog(this);
                //if (result == DialogResult.OK)
                //{
                //    InnerBindData(frmQuery.Results);
                //}
                //frmQuery = null;
            }

        }
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
           // this.picTipImage.EditValue = Properties.Resources.process2;


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

        public DataRow GetDataRowByOperationId(Guid operationId)
        {
            DataRow row = (this.bindingSource.DataSource as DataTable).Select(string.Format("ID='{0}'", operationId)).First();

            return row;
        }
        public DataRow GetDataRowByRowHandle(int rowHandle)
        {
            if (rowHandle < 0)
                return null;
            DataRow row = this.gridViewList.GetDataRow(rowHandle);
            return row;
        }
        void gridControlList_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            Point gvLocation = PointToScreen(this.gridControlList.Location);
            int x = e.X - gvLocation.X;
            int y = e.Y - gvLocation.Y;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi = this.gridViewList.CalcHitInfo(new Point(x, y));
            int iMouseRowHandle = hi.RowHandle;
            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (files == null || files.Length <= 0)
                return;
            string fileExtensions = CommonUIUtility.GetFileExtensions();

            List<string> listTareget = files.Where(file => fileExtensions.Contains(Path.GetExtension(file))).ToList();

            HandleDragDrop(iMouseRowHandle, listTareget);
        }
        public virtual void HandleDragDrop(int rowHandle, List<string> filePaths)
        {

        }
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
        /// <summary>
        /// 搜索命令处理
        /// 从列表中搜索匹配的记录行
        /// 如果存在匹配，则匹配第一个满足条件的行；如果同一个搜索条件多次搜索，则按顺序依次匹配，匹配行为选中状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        public void Command_EmailQuery(object sender, EventArgs e)
        {
            if (CurrentDataSource == null)
                return;
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string queryNo = this.QueryNo;
                //如果搜索单号为空，则直接返回
                if (string.IsNullOrEmpty(queryNo))
                    return;
                //如果此次搜索单号和上一次搜索相同，则从旧有匹配记录里查找
                if (queryNo.Equals(this.previousQueryNo, StringComparison.CurrentCultureIgnoreCase))
                {
                    //如果上次搜索没有匹配行，则直接返回
                    if (previousMatchRows == null || previousMatchRows.Length <= 0)
                        return;
                    //如果上次搜索匹配已经到了最后一条匹配记录，则直接返回
                    if (previousSelectRowIndex == previousMatchRows.Length - 1)
                        return;
                    SetSelectedRow();
                }
                //如果此次搜索和上次搜索单号不同
                else
                {
                    //重置搜索记录
                    ResetQueryRecord();
                    DataRow[] rows = previousMatchRows = this.CurrentDataSource.Select(GetFilterExpression());
                    if (rows == null || rows.Length <= 0)
                        return;
                    SetSelectedRow();
                    previousQueryNo = queryNo;
                }
            }
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
        private DataTable GetDataSource()
        {
            return this.bindingSource.DataSource as DataTable;
        }

        private string GetFilterExpression()
        {
            List<string> filterColumnNames = GetQueryColumnNames();
            if (filterColumnNames == null || filterColumnNames.Count <= 0)
                return "1=1";
            return GetExpression(filterColumnNames, this.QueryNo, false);
        }
        private DataRow[] previousMatchRows = null;
        private int previousSelectRowIndex = -1;
        private string previousQueryNo = string.Empty;

        #endregion

        #region IBusinessPart 成员


        public  ListFormType GetListFormType()
        {
            return this.ListType;
        }

        #endregion

        private List<CustomColumnInfo> GetGridColumnInfos()
        {
            List<CustomColumnInfo> list = new List<CustomColumnInfo>();
            int columnCount = this.gridViewList.Columns.Count;
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
                list.Add(columnInfo);
            }
            return list;
        }
        private void OnApplicationExit(object sender, EventArgs e)
        {
            SaveCustomColumnInfo();
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
                    gridInfo.ListType = GetListFormType();
                    gridInfo.Id = Guid.NewGuid();

                }
                if (gridInfo.UserId == null)
                {
                    gridInfo.UserId = LocalData.UserInfo.LoginID;
                    gridInfo.Id = Guid.NewGuid();
                }

                gridInfo.Columns = columnInfos;
                ServiceClient.GetService<IClientCustomDataGridService>().Save(gridInfo);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex);
            }

        }

        /// <summary>
        /// 选择行改变时触发事件
        /// </summary>
        public FocusedRowChangedEventHandler FocusedRowChanged;

        private void gridViewList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (FocusedRowChanged!=null)
            {
                FocusedRowChanged(sender, e);
            }
        }



        public int globalId = 1;

        public  List<OperationToolbarCommand> GetToolbarCommands()
        {
            return ToolbarTemplateLoader.Current[this.TemplateCode];
        }

        public  List<BusinessColumnInfo> GetColumnInfos()
        {

            return ColumnTemplateLoader.Current[this.TemplateCode];
        }

        public  List<ContextMenuItemInfo> GetContextMenuItems(DataRow row)
        {
            SectionKey key = new SectionKey { SectionCode = this.TemplateCode, Type = this.BusinessType };

            return ContextMenuFileLoader.Current[key];
        }

        public  List<string> GetQueryColumnNames()
        {
            return QueryColumnLoader.GetQueryColumns(TemplateCode) ;
        }


        public  ListFormType ListType
        {
            get;
            set;
        }
        public List<ContextMenuItemInfo> GetBusinessRelatedContextMenuItems(DataRow row)
        {
            if (row == null || string.IsNullOrEmpty(row["RefNO"].ToString()))
                return new List<ContextMenuItemInfo>();
            string refNo = row["RefNO"].ToString();
            // BusinessType businessType = (BusinessType)int.Parse(row["Type"].ToString());
            //BusinessType businessType = BusinessType.OE;
            Dictionary<string, string> dicNoPair = UIHelper.ExtractNoPairs(refNo, Environment.NewLine.ToCharArray(), ':');
            if (dicNoPair.Count <= 0)
                return new List<ContextMenuItemInfo>();
            List<ContextMenuItemInfo> items = new List<ContextMenuItemInfo>();
            switch (OperationType)
            {
                case OperationType.OceanExport:
                    items = GetOEBusinessContextMenuItems(dicNoPair);
                    break;

            }
            return items;
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

        //ICP.Common.UI.ICPCommUIHelper uiHelper;
        //// [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        //public ICP.Common.UI.ICPCommUIHelper ICPCommUIHelper
        //{
        //    get
        //    {
        //        if (uiHelper != null)
        //            return uiHelper;
        //        else
        //        {

        //            uiHelper = WorkItem.Items.AddNew<ICP.Common.UI.ICPCommUIHelper>();
        //            return uiHelper;
        //        }
        //    }
        //}

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

        [CommandHandler("Command_TaskCenter_Refresh")]
        public void OnHelpAbout(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Bank Teller QuickStart Version 1.0", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        [CommandHandler("Command_TaskCenter_Add")]
        public void OnHelpAbout1(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Bank Teller QuickStart Version 1.0", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }




}
