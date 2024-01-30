using System.Linq;
using DevExpress.XtraBars;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Framework.ClientComponents;

namespace ICP.Business.Common.UI.QuotedPrice
{
    /// <summary>
    /// Quoted Price Edit Part
    /// </summary>
    [ToolboxItem(false)]
    [SmartPart]
    public partial class QuotedPriceEditPart : BaseEditPart
    {
        #region Services & Fields & Property & Delegate

        #region Services
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        /// <summary>
        /// 报价服务
        /// </summary>
        IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }

        /// <summary>
        /// 报价UI数据服务
        /// </summary>
        QuotedPriceUIDataHelper QuotedPriceUIDataServices
        {
            get
            {
                return ClientHelper.Get<QuotedPriceUIDataHelper, QuotedPriceUIDataHelper>();
            }
        }

        /// <summary>
        /// 查询数据服务(客户端)
        /// </summary>
        IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        /// <summary>
        /// 国家，省份，地点信息维护
        /// </summary>
        IGeographyService GeographyService
        {
            get
            {
                return ServiceClient.GetService<IGeographyService>();
            }
        }
        /// <summary>
        /// 公共客户管理服务
        /// </summary>
        ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }
        /// <summary>
        /// ICP UI Common Helper
        /// </summary>
        ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }
        #endregion

        #region Fields
        /// <summary>
        /// 运输条款是否首次加载
        /// </summary>
        bool _TransportClauseIsFristTime = true;
        /// <summary>
        /// 发货人首次记载
        /// </summary>
        bool _CustomerFristTime = true;
        /// <summary>
        /// 
        /// </summary>
        List<CountryList> _countryList = null;
        /// <summary>
        /// 发货人搜索引擎
        /// </summary>
        CustomerFinderBridge customerBridge;
        #endregion

        #region Property
        bool _shown
        {
            get
            {
                return CurrentData.IsDirty;
            }
        }
        /// <summary>
        /// 当前数据对象
        /// </summary>
        private QuotedPriceOrderInfo CurrentData { get; set; }
        /// <summary>
        /// 当前数据源
        /// </summary>
        public override object DataSource
        {
            get { return bsQuotedPrice.DataSource; }
            set { BindingData(value); }
        }
        #endregion

        #region Delegate
        /// <summary>
        /// 保存后触发事件
        /// </summary>
        public override event SavedHandler Saved;
        #endregion
        #endregion

        #region Init
        /// <summary>
        /// Quoted Price Edit Part
        /// </summary>
        public QuotedPriceEditPart()
        {
            InitializeComponent();
            InitControl();
            RegisterEvent();
            Disposed += (sender, arg) =>
            {
                bsQuotedPrice.Clear();
                UnRegisterEvent();
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        } 
        #endregion

        #region Control Event
        /// <summary>
        /// 窗体加载
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            #region Init State
            BaseDataObject data = bsQuotedPrice.DataSource as BaseDataObject;
            if (data != null) data.BeginEdit();
            #endregion

            base.OnLoad(e);
        }

        /// <summary>
        /// 防闪烁
        /// </summary>
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == 0x0014) return;// 禁掉清除背景消息

            base.WndProc(ref m);
        }

        /// <summary>
        /// 保存
        /// </summary>
        void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                SaveData();
            }
        }


        #region 发货人 Customer
        /// <summary>
        /// 首次获得焦点
        /// </summary>
        private void OnstxtCustomerFirstEnter(object sender, EventArgs e)
        {
            if (_CustomerFristTime)
            {
                if (_countryList == null) _countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);

                customerBridge = new CustomerFinderBridge(
               stxtCustomer,
               _countryList,
               DataFindClientService,
               CustomerService,
               GetCustomerStateCondition,
               CurrentData.CustomerDescription,
               ICPCommUIHelper,
               true);
                customerBridge.Init();
                _CustomerFristTime = false;
            }
        }
        SearchConditionCollection GetCustomerStateCondition()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("CodeApplyState", CustomerCodeApplyState.Passed, false);
            return conditions;
        }
        /// <summary>
        /// 确认发货人
        /// </summary>
        void stxtCustomer_OnOk(object sender, EventArgs e)
        {
            if (CurrentData != null && stxtCustomer.CustomerDescription == null)
            {
                CurrentData.CustomerDescription = stxtCustomer.CustomerDescription;
            }
        } 
        #endregion

        /// <summary>
        /// 报价框改变
        /// </summary>
        void txtRates_TextChanged(object sender, EventArgs e)
        {
            if (CurrentData != null)
            {
                CurrentData.Remark = txtRates.Rtf;
                CurrentData.IsDirty = true;
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            var findForm = FindForm();
            if (findForm != null) findForm.Close();
        }

        #endregion

        #region Custom Method
        /// <summary>
        /// 初始化控件数据
        /// </summary>
        private void InitControl()
        {
            #region Init Data
            //查询国家，省份，地点信息维护
            if (_countryList == null) _countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);
            #region TransportClause
            cmbTransportClause.SetEnterToExecuteOnec(delegate
            {
                List<TransportClauseList> transportClauseList = ICPCommUIHelper.SetCmbTransportClause(cmbTransportClause);
            });
            #endregion

            #region TargetType
            
            cmbTargetType.SetEnterToExecuteOnec(delegate
            {
                ICPCommUIHelper.SetComboxByEnum<QPTargetType>(cmbTargetType, false);
            });

            cmbPaymentType.SetEnterToExecuteOnec(delegate {
                ICPCommUIHelper.SetComboxByEnum<QPPaymentType>(cmbPaymentType, false);
            });
            #endregion
            #endregion

            #region Init Message
            InitMessage(); 
            #endregion

            if (CurrentData != null)
            {
                #region TransportClause
                cmbTransportClause.ShowSelectedValue(CurrentData.TransportClauseID, CurrentData.TransportClauseName);
                #endregion
            }
        }
        /// <summary>
        /// 从服务端重新获取报价单信息
        /// </summary>
        /// <param name="qpOrderId">报价单ID</param>
        void GetData(Guid qpOrderId)
        {
            CurrentData = FCMCommonService.GetQuotedPriceOrderInfo(qpOrderId);
        }
        /// <summary>
        /// 注册延迟加载的数据源
        /// </summary>
        void SetLazyLoaders()
        {
            //船公司
            if (CurrentData.CustomerDescription == null)
            {
                CurrentData.CustomerDescription = new CustomerDescription();
            }
        }
        /// <summary>
        /// 初始化提示信息
        /// </summary>
        private void InitMessage()
        {
            RegisterMessage("SaveSuccessfully",LocalData.IsEnglish? "Save Successfully":"保存成功");
        }
        /// <summary>
        /// 注册事件
        /// </summary>
        private void RegisterEvent()
        {
            barSave.ItemClick += barSave_ItemClick;
            barClose.ItemClick += barClose_ItemClick;
            //Customer
            stxtCustomer.OnFirstEnter += OnstxtCustomerFirstEnter;
            stxtCustomer.OnOk += stxtCustomer_OnOk;
            txtRates.LostFocus += txtRates_TextChanged;
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        private void UnRegisterEvent()
        {
            barSave.ItemClick -= barSave_ItemClick;
            barClose.ItemClick -= barClose_ItemClick;
            txtRates.LostFocus -= txtRates_TextChanged;
        }

        /// <summary>
        /// 数据源绑定
        /// </summary>
        /// <param name="data">数据</param>
        public void DataSourceBind(object data)
        {
            DataSource = data;
        }

        /// <summary>
        /// 显示报价单
        /// </summary>
        /// <param name="id"></param>
        public void ViewQuotedPrice(Guid id)
        {
            bar1.Visible = false;
            foreach (Control item in panelMain.Controls)
            {
                item.Enabled = false;
            }
            BindingData(new EditPartShowCriteria() { BillNo = id });
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="data">数据</param>
        void BindingData(object data)
        {
            EditPartShowCriteria criteria = data as EditPartShowCriteria;
            
            if (EditMode == EditMode.New)
            {
                CurrentData = new QuotedPriceOrderInfo
                {
                    TransportClauseID = Guid.Empty,
                    TransportClauseName = "",
                    IsValid = true,
                    CreateByID = LocalData.UserInfo.LoginID,
                    CreateByName = LocalData.UserInfo.LoginName,
                    CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified),
                    FromDate = null,
                    ToDate=null,
                    UpdateDate = null
                };
            }
            else
            {
                ClientHelper.SetApplicationContext();
                GetData(criteria.OperationID);
                txtRates.Rtf = CurrentData.Remark;
                if (EditMode == EditMode.Edit)
                {
                    
                }
                else if (EditMode == EditMode.Copy)
                {
                    CurrentData.ID = Guid.Empty;
                    CurrentData.No = "";
                    CurrentData.ConfirmedBy = Guid.Empty;
                    CurrentData.CreateByID = LocalData.UserInfo.LoginID;
                    CurrentData.CreateByName = LocalData.UserInfo.LoginName;
                    CurrentData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                    CurrentData.FromDate = null;
                    CurrentData.ToDate = null;
                    CurrentData.UpdateDate = null;
                    foreach (var item in CurrentData.RatesList)
                    {
                        item.ID = Guid.Empty;
                    }
                }
                quotedPriceRatesPart.SetSource(CurrentData.RatesList);
            }
            cmbTransportClause.Focus();
            cmbTargetType.Focus();
            cmbPaymentType.Focus();
            Enabled = CurrentData.IsValid;
            SetLazyLoaders();
            bsQuotedPrice.DataSource = CurrentData;
            bsQuotedPrice.ResetBindings(false);
            CurrentData.CancelEdit();
            CurrentData.IsDirty = false;
        }

        #region Save
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        public override bool SaveData()
        {
            return Save();
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        private bool Save()
        {
            panelMain.Focus();
            EndEdit();
            QuotedPriceOrderInfo currentData = bsQuotedPrice.DataSource as QuotedPriceOrderInfo;
            if (currentData == null) return true;
            if (!currentData.IsDirty  && !currentData.IsNew && !quotedPriceRatesPart.IsChanged) return true;
            try
            {
                if (ValidateData() && quotedPriceRatesPart.ValidateData())
                {
                    QPOrderSaveRequest saveRequest = null;
                    if (currentData.ID == Guid.Empty || currentData.IsDirty)
                    {
                        saveRequest = BuildQuotePrice(currentData);
                    }
                    List<QPRatesSaveRequest> ratesList = quotedPriceRatesPart.BuildRatesList(currentData.ID);

                    Dictionary<Guid, SaveResponse> saved = FCMCommonService.SaveQuotedPriceWithTrans(saveRequest, ratesList);
                    if (saveRequest != null)
                    {
                        SaveResponse.Analyze(new List<SaveRequest> { saveRequest }, saved, true);
                        RefreshUI(saveRequest);
                    }
                    if (ratesList != null)
                    {
                        SaveResponse.Analyze(ratesList.Cast<SaveRequest>().ToList(), saved, true);
                        quotedPriceRatesPart.RefreshUI(ratesList);
                    }
                    currentData.CancelEdit();
                    currentData.BeginEdit();
                    AfterSave();
                }
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
                return false;
            }
        }

        void RefreshUI(QPOrderSaveRequest saveRequest)
        {
            SingleResult result = saveRequest.SingleResult;

            CurrentData.ID = result.GetValue<Guid>("ID");
            CurrentData.No = result.GetValue<string>("No");
            CurrentData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
        }

        private void AfterSave()
        {
            if (Saved != null) 
                Saved(CurrentData);
            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), NativeLanguageService.GetText(this, "SaveSuccessfully"));
            SetTitle();
        }
        void SetTitle()
        {
            string titleNo = string.Empty;

            if (CurrentData.No.Length > 4)
            {
                titleNo = CurrentData.No.Substring(CurrentData.No.Length - 4, 4);
            }
            else
            {
                titleNo = CurrentData.No;
            }

            Title = LocalData.IsEnglish ? "QP Order " + titleNo : "报价单：" + titleNo;
        }
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns>验证结果</returns>
        private bool ValidateData()
        {
            QuotedPriceOrderInfo currentData = CurrentData;
            Validate();
            bsQuotedPrice.EndEdit();

            dxErrorProvider1.ClearErrors();
            //TODO:验证控件数据
            bool isSrcc = true;
            
            isSrcc = currentData.Validate(
                (e) =>
                {
                    //Transport Clause
                    if (ArgumentHelper.GuidIsNullOrEmpty(currentData.TransportClauseID))
                    {
                        e.SetErrorInfo("TransportClauseID", LocalData.IsEnglish ? "Please selected Transport Clause." : "请选择贸易条款.");
                        isSrcc = false;
                    }
                    //Customer
                    if (ArgumentHelper.GuidIsNullOrEmpty(currentData.CustomerID))
                    {
                        e.SetErrorInfo("CustomerID", LocalData.IsEnglish ? "Please selected Customer." : "请选择客户.");
                        isSrcc = false;
                    }
                    //From Date
                    if (currentData.FromDate == null || currentData.FromDate.Value == DateTime.MinValue)
                    {
                        e.SetErrorInfo("FromDate", LocalData.IsEnglish ? "Please selected From Date." : "请选择报价开始时间.");
                        isSrcc = false;
                    }
                    //To Date
                    if (currentData.ToDate == null || currentData.ToDate.Value == DateTime.MinValue)
                    {
                        e.SetErrorInfo("ToDate", LocalData.IsEnglish ? "Please selected To Date." : "请选择报价结束时间.");
                        isSrcc = false;
                    }
                }
                );

            return isSrcc;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentData"></param>
        /// <returns></returns>
        private QPOrderSaveRequest BuildQuotePrice(QuotedPriceOrderInfo currentData)
        {
            EndEdit();

            if (currentData.IsDirty == true || currentData.IsNew)
            {
                QPOrderSaveRequest saveRequest = new QPOrderSaveRequest
                {
                    id = currentData.ID,
                    no = currentData.No,
                    TargetType = currentData.TargetType,
                    transportClauseID = currentData.TransportClauseID,
                    customerid = currentData.CustomerID,
                    customerDescription = currentData.CustomerDescription,
                    commodity = currentData.Commodity,
                    remark = currentData.Remark,
                    PaymentType = currentData.PaymentType,
                    fromDate = currentData.FromDate.Value,
                    toDate = currentData.ToDate.Value,
                    quoteBy=LocalData.UserInfo.LoginID,
                    saveByID = LocalData.UserInfo.LoginID,
                    updateDate = currentData.UpdateDate
                };
                return saveRequest;
            }
            return null;
        } 
        #endregion
        #endregion
    }
}
