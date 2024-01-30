using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Operation.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.Controls;
using Microsoft.Practices.ObjectBuilder;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.Common.ServiceInterface;
using ICP.Business.Common.UI.Contact;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Business.Common.UI.Document;
using Microsoft.Practices.CompositeUI.SmartParts;


namespace ICP.FCM.OceanExport.UI.Booking
{
    /// <summary>
    /// 报关委托编辑界面
    /// </summary>
    [ToolboxItem(false)]
    [SmartPart]
    public partial class OceanCustomsEditPart : BaseEditPart, IDisposable
    {
        #region Service

        [ServiceDependency]
        public WorkItem workitem
        {
            get;
            set;
        }
        public IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }
        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        public ICP.FCM.Common.ServiceInterface.IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<ICP.FCM.Common.ServiceInterface.IFCMCommonService>();
            }
        }
        public OceanExportPrintHelper OceanExportPrintHelper
        {
            get
            {
                return ClientHelper.Get<OceanExportPrintHelper, OceanExportPrintHelper>();
            }
        }

        #endregion

        #region 私有变量
        private OceanCustoms _OceanCustoms = null;

        private bool isNewOrder = false;

        private BusinessOperationContext context;
        private ICP.Message.ServiceInterface.Message mail;
        /// <summary>
        /// 邮件中心与ICP业务关联信息
        /// </summary>
        BusinessOperationParameter _businessOperationParameter = null;
        #endregion

        #region 数据源
        public override object DataSource
        {
            get
            {
                return this.bsOceanContainerList.DataSource;
            }
            set
            {
                BindData(value as List<OceanContainerList>);
            }
        }

        private void BindData(List<OceanContainerList> containerList)
        {
            if (containerList != null)
            {
                InnerBindData(containerList);
            }
            else
            {

                Guid operationId = (Guid)this.StateValues["OperationId"];
                List<OceanContainerList> oceanContainerList = OceanExportService.GetOceanContainerList(operationId);
                contactSoure = FCMCommonService.GetContactList(operationId, OperationType.OceanExport);
                InnerBindData(oceanContainerList);
            }
        }

        private delegate void DataBindDelegate(List<OceanContainerList> containerList);
        private void InnerBindData(List<OceanContainerList> containerList)
        {
            if (containerList == null)
            {
                this.bsOceanContainerList.DataSource = typeof(OceanContainerList);
            }

            this.bsOceanContainerList.DataSource = containerList;
            if (contactSoure != null && contactSoure.CustomerCarrier != null && contactSoure.CustomerCarrier.Count > 0)
            {
                //this.ucCustomerList.Type = ICP.Framework.CommonLibrary.Common.ContactType.Customer;                            
                this.ucCustomerList.DataSource = contactSoure.CustomerCarrier.FindAll(o => o.Type == ContactType.Customer && o.CF);
            }
            InitData();
            //设置文档列表数据源
            SetDocumentDataSource();
            Refresh();

        }

        public OceanContainerList CurrentRow
        {
            get
            {
                return (OceanContainerList)bsOceanContainerList.Current;
            }
        }
        #endregion

        #region init

        public OceanCustomsEditPart()
        {
            InitializeComponent();
            SyncLocalData = true;
            if (!LocalData.IsDesignMode)
            {
                if (LocalData.IsEnglish == false)
                    SetCnText();
            }
            this.Disposed += delegate
            {

                gridControl1.DataSource = null;
                dxErrorContainers.DataSource = null;
                dxErrorProvider1.DataSource = null;
                bsOceanCustomsList.PositionChanged -= this.bsOceanCustomsList_PositionChanged;
                gluContainer.EditValueChanged -= this.gluContainer_EditValueChanged;
                gvMain.CustomDrawRowIndicator -= this.gvMain_CustomDrawRowIndicator;
                bsOceanCustomsList.DataSource = null;
                bsOceanCustomsList.Dispose();
                bsOceanContainerList.DataSource = null;
                bsOceanCustomsList.PositionChanged -= this.bsOceanCustomsList_PositionChanged;
                bsOceanContainerList.Dispose();
                
                _businessOperationParameter = null;
                _OceanCustoms = null;
                Saved = null;
                mail = null;
                context = null;
                if (this.workitem != null)
                {
                    if (ucCustomerList != null)
                    {
                        workitem.Items.Remove(this.ucCustomerList);
                        ucCustomerList = null;
                    }
                    if (ucDocumentListDispatch != null)
                    {
                        workitem.Items.Remove(this.ucDocumentListDispatch);
                        ucDocumentListDispatch = null;
                    }
                    workitem.Items.Remove(this);
                    workitem = null;

                }
                DocumentListPresenter = null;
                OperationContext = null;
                contactSoure = null;
            };

        }

        private void SetCnText()
        {
            barAdd.Caption = "新增";
            barRemove.Caption = "删除";
            barSave.Caption = "保存";
            barPrint.Caption = "打印";
            barClose.Caption = "关闭";

            dpOrderCustoms.Text = "报关委托单";
            dpAttachment.Text = "文档列表";
            dpCustomsOfficer.Text = "委托联系人";

            gcCustoms.Text = "报关委托单";
            lblContainerNo.Text = "箱号";
            lblTitle.Text = "抬头";
            lblCustomsPort.Text = "报关地点";
            lblCustoms.Text = "报关行";
            lblMemo.Text = "备注";

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!LocalData.IsDesignMode)
            {
                LoadControl();
                SearchRegister();
            }
        }
        private void InitData()
        {
            OperationContext = new BusinessOperationContext();

            if (CurrentRow != null)
            {
                OperationContext.OperationID = CurrentRow.OceanBookingID;
                OperationContext.OperationID = CurrentRow.OceanBookingID;
                OperationContext.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
                OperationContext.OperationType = OperationType.OceanExport;
            }
            this.ucDocumentListDispatch.ShowControlCheckState = true;
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        IDictionary<string, object> StateValues
        {
            get;
            set;
        }
        public override void Init(IDictionary<string, object> values)
        {
            this.StateValues = values;
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key.ToUpper() == "BusinessOperationParameter".ToUpper())
                {
                    _businessOperationParameter = item.Value as BusinessOperationParameter;
                    return;
                }
            }
        }

        void LoadControl()
        {
            //加载客户列表界面
            ucCustomerList.Dock = DockStyle.Fill;
            gcCustomsOfficer.Controls.Clear();
            ucCustomerList.IsCustomerPart = true;
            gcCustomsOfficer.Controls.Add(ucCustomerList);

            //加载文档列表界面            
            ucDocumentListDispatch.Dock = DockStyle.Fill;
            gcAttachment.Controls.Clear();
            gcAttachment.Controls.Add(ucDocumentListDispatch);
        }
        private void Refresh()
        {
            this.bsOceanCustomsList.ResetBindings(false);
            this.gvMain.RefreshData();
            this.gvMain.BestFitColumns();
            //EditLock(false);
            RegisterCurrentDataCahngedEvent();
        }



        /// <summary>
        ///初始化OceanCustoms对象
        /// </summary>
        private void InitOceanCustoms()
        {
            if (_OceanCustoms == null)
            {
                _OceanCustoms = new OceanCustoms();
                _OceanCustoms.ID = Guid.NewGuid();
                _OceanCustoms.CreateBy = LocalData.UserInfo.LoginID;
                _OceanCustoms.CreateDate = DateTime.Now;
            }
            _OceanCustoms.OceanBookingID = CurrentRow == null ? Guid.Empty : CurrentRow.OceanBookingID;
            _OceanCustoms.OceanContainerID = CurrentRow == null ? Guid.Empty : CurrentRow.ID;
            if (stxtCustoms.Tag != null && _OceanCustoms != null)
                _OceanCustoms.CustomsID = new Guid(stxtCustoms.Tag.ToString());
            if (_OceanCustoms != null)
                _OceanCustoms.OceanContainerNo = gluContainer.EditValue.ToString();
            if (txtCustomsPort.EditValue != null)
                _OceanCustoms.PortToCustoms = txtCustomsPort.EditValue.ToString();
            _OceanCustoms.Way = int.Parse(rgWay.EditValue.ToString());
            _OceanCustoms.Remark = meMemo.Text.Trim();
            _OceanCustoms.UpdateBy = LocalData.UserInfo.LoginID;
            _OceanCustoms.UpdateDate = DateTime.Now;
            _OceanCustoms.Title = txtTitle.Text.Trim();
        }

        private void EditorClear()
        {
            //EditLock(true);

            gluContainer.EditValue = null;
            txtTitle.Text = string.Empty;
            stxtCustoms.EditValue = null;
            txtCustomsPort.Text = string.Empty;
            rgWay.EditValue = 1;
            meMemo.Text = string.Empty;
            gluContainer.Focus();

        }
        //void EditLock(bool value)
        //{
        //    txtTitle.Enabled = value;
        //    txtCustomsPort.Enabled = value;
        //    stxtCustoms.Enabled = value;
        //    rgWay.Enabled = value;
        //    meMemo.Enabled = value;
        //}
        bool ValidateData()
        {
            bool isScrr = true;
            if (_OceanCustoms.Validate
                (
                    delegate(ValidateEventArgs e)
                    {
                        if (gluContainer.EditValue == null)
                            e.SetErrorInfo("OceanContainerNo", LocalData.IsEnglish ? "Container No must input." : "箱号必须填写.");
                        if (string.IsNullOrEmpty(txtTitle.Text))
                            e.SetErrorInfo("Title", LocalData.IsEnglish ? "Title must input." : "报关抬头必须填写.");
                        if (string.IsNullOrEmpty(_OceanCustoms.PortToCustoms))
                            e.SetErrorInfo("PortToCustoms", LocalData.IsEnglish ? "Port To Customs must input." : "报关地点必须填写.");
                        if (string.IsNullOrEmpty(_OceanCustoms.CustomsID.ToString()))
                            e.SetErrorInfo("CustomersName", LocalData.IsEnglish ? "Port To Customer Name must input." : "报关客户必须填写.");
                    }
                ) == false) isScrr = false;
            return isScrr;
        }

        bool TitleUniqueness()
        {
            bool relust = false;
            bool val = false;
            List<OceanCustoms> lst = bsOceanCustomsList.DataSource as List<OceanCustoms>;
            foreach (OceanCustoms item in lst)
            {
                if (item.OceanBookingID == CurrentRow.OceanBookingID && item.OceanContainerID == CurrentRow.ID && item.Title == _OceanCustoms.Title)
                {
                    val = true;
                    break;
                }
            }
            if (val && isNewOrder)
                relust = true;
            return relust;
        }

        #endregion

        #region CRUD

        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //创建新对象
            _OceanCustoms = new OceanCustoms();
            _OceanCustoms.ID = Guid.NewGuid();
            _OceanCustoms.CreateBy = LocalData.UserInfo.LoginID;
            _OceanCustoms.CreateDate = DateTime.Now;
            EditorClear();
            isNewOrder = true;
        }

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Save())
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                BindingView();
                gvMain.FocusedRowHandle = bsOceanCustomsList.IndexOf(bsOceanCustomsList.Current);
                Refresh();
            }
            else
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), LocalData.IsEnglish ? "Save Unsuccessfully" : "保存失败");
        }

        bool Save()
        {
            try
            {
                InitOceanCustoms();
                if (ValidateData() == false)
                    return false;
                //判断Title是否唯一
                if (TitleUniqueness())
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), LocalData.IsEnglish ? "The title must be unique" : "报关抬头必须唯一");
                    return false;
                }
                isNewOrder = false;
                bool isSuccess = true;
                isSuccess = OceanExportService.SaveOceanCustomsInfo(_OceanCustoms);
                if (isSuccess)
                {
                    AfterSave();
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); return false; }
        }


        private void AfterSave()
        {
            if (_businessOperationParameter == null)
            {
                _businessOperationParameter = new BusinessOperationParameter();
            }
            else
            {
                _businessOperationParameter.Context = GetContext(_OceanCustoms);
            }

            Saved(new object[] { null, _businessOperationParameter,_businessOperationParameter.Context });
        }

        private BusinessOperationContext GetContext(OceanCustoms customsInfo)
        {
            BusinessOperationContext context = new BusinessOperationContext();
            context.OperationID = customsInfo.OceanBookingID;
            context.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.OceanExport;
            context.FormId = customsInfo.OceanBookingID;
            context.FormType = ICP.Framework.CommonLibrary.Common.FormType.Customs;
            return context;
        }

        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.FindForm().Close();
        }

        private void barRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (bsOceanCustomsList.Current == null) return;

            if (PartLoader.EnquireIsDeleteCurrentData())
            {
                OceanCustoms currentData = (OceanCustoms)bsOceanCustomsList.Current;

                try
                {
                    OceanExportService.RemoveOceanCustomsInfo(currentData.ID, LocalData.UserInfo.LoginID, currentData.UpdateDate);
                    bsOceanCustomsList.RemoveCurrent();
                    EditorClear();
                    Refresh();
                    isNewOrder = false;
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }

            }
        }

        #endregion

        #region 搜索器注释
        void SearchRegister()
        {
            DataFindClientService.Register(stxtCustoms, CommonFinderConstants.CustomerAgentOfCarrierFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                   delegate(object inputSource, object[] resultData)
                   {
                       stxtCustoms.Text = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                       stxtCustoms.Tag = new Guid(resultData[0].ToString());
                   }, Guid.Empty, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
        }

        #endregion

        #region Event

        private void BindingView()
        {
            // 获取该箱号的所有报关单
            if (CurrentRow != null)
            {
                List<OceanCustoms> aList = OceanExportService.GetOceanCustomsServiceList(CurrentRow.OceanBookingID, CurrentRow.ID);
                bsOceanCustomsList.DataSource = aList;
            }
        }

        private void gluContainer_EditValueChanged(object sender, EventArgs e)
        {
            this.bsOceanCustomsList.DataSource = CurrentRow;
            //EditorClear();
            // 获取该箱号的所有报关单
            BindingView();
            this.gvMain.BestFitColumns();
            bsOceanCustomsList.PositionChanged += new EventHandler(bsOceanCustomsList_PositionChanged);
        }

        void bsOceanCustomsList_PositionChanged(object sender, EventArgs e)
        {
            if (_OceanCustoms != null)
            {
                _OceanCustoms.PropertyChanged -= new PropertyChangedEventHandler(_V_OceanCustoms_PropertyChanged);
            }

            _OceanCustoms = bsOceanCustomsList.Current as OceanCustoms;
            if (_OceanCustoms != null)
            {
                //isNewOrder = false;
                gluContainer.EditValue = _OceanCustoms.OceanContainerNo;
                txtTitle.EditValue = _OceanCustoms.Title;
                txtCustomsPort.EditValue = _OceanCustoms.PortToCustoms;
                stxtCustoms.EditValue = _OceanCustoms.CustomsName;
                rgWay.EditValue = (int)_OceanCustoms.Way;
                meMemo.EditValue = _OceanCustoms.Remark;
                //_OceanCustoms = oeService.GetOceanCustomsInfo(_OceanCustoms.ID);

                //_OceanCustoms.PropertyChanged += new PropertyChangedEventHandler(_V_OceanCustoms_PropertyChanged);
            }
        }

        void _V_OceanCustoms_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SetCurrentDataCahnged();
        }

        private void SetCurrentDataCahnged()
        {
            if (_OceanCustoms == null) return;
            _OceanCustoms.PropertyChanged -= new PropertyChangedEventHandler(_V_OceanCustoms_PropertyChanged);
            _OceanCustoms.IsDirty = true;
        }

        private void RegisterCurrentDataCahngedEvent()
        {
            if (_OceanCustoms == null) return;
            _OceanCustoms.PropertyChanged -= new PropertyChangedEventHandler(_V_OceanCustoms_PropertyChanged);
            _OceanCustoms.PropertyChanged += new PropertyChangedEventHandler(_V_OceanCustoms_PropertyChanged);
        }

        private void barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_OceanCustoms == null)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), LocalData.IsEnglish ? "Select the order you want to print." : "请选择要打印的单据");
                return;
            }
            if (_OceanCustoms.IsNew || _OceanCustoms.IsDirty)
            {
                if (Save() == false) return;
            }
            //国内报关委托单
            if (CurrentRow != null)
            {
                OceanExportPrintHelper.PrintCustomsCN(_OceanCustoms.ID, CurrentRow.OceanBookingID);
            }
        }


        private void gvMain_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        #endregion

        #region Attachment
        /// <summary>
        /// 文档列表呈现类
        /// </summary>
        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public DocumentListPresenter DocumentListPresenter { get; set; }

        /// <summary>
        /// 业务操作上下文类
        /// </summary>
        private BusinessOperationContext OperationContext;
        /// <summary>
        /// 操作ID
        /// </summary>
        public Guid OperationId { get; set; }

        /// <summary>
        /// 设置文档列表数据源
        /// </summary>
        /// <param name="para"></param>
        private void SetDocumentDataSource()
        {
            DocumentListPresenter.ucList = this.ucDocumentListDispatch;
            DocumentListPresenter.BindData(OperationContext);
        }

        #endregion

        #region CustomerList

        private UCCustomerList _CustomerList;
        public UCCustomerList ucCustomerList
        {
            get
            {
                if (_CustomerList == null)
                    _CustomerList = workitem.Items.AddNew<UCCustomerList>();
                return _CustomerList;
            }
            set { _CustomerList = value; }
        }

        private UCDocumentList _DocumentList;
        public UCDocumentList ucDocumentListDispatch
        {
            get
            {
                if (_DocumentList == null)
                    _DocumentList = workitem.Items.AddNew<UCDocumentList>();
                return _DocumentList;
            }
            set { _DocumentList = value; }
        }

        ContactObjects contactSoure = null;

        #endregion

    }
}
