using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Common.ServiceInterface.DataObjects;

namespace ICP.FAM.UI.ReleaseRC.Dialogs
{
    [ToolboxItem(false)]
    public partial class ReleaseRCEditPart : BaseEditPart
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }

        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        #endregion

        #region  本地变量

        ReleaseBLList _CurrentData = null;

        /// <summary>
        /// 当前业务的客户列表(收发通)包括帐单已选的新客户
        /// </summary>
        List<OperationCustomer> _TradeCustoemrs = new List<OperationCustomer>();
        Dictionary<Guid, List<CustomerContactList>> _CustomerContactList = new Dictionary<Guid, List<CustomerContactList>>();

        #endregion

        #region init

        public ReleaseRCEditPart()
        {
            InitializeComponent();
            Disposed += delegate {
                if (customerFinder != null)
                {
                    customerFinder.Dispose();
                    customerFinder = null;
                }
                _CurrentData = null;
                _CustomerContactList = null;
                _TradeCustoemrs = null;
                bindingSource1.DataSource = null;
                bindingSource1.Dispose();
                Saved = null;
                cmbCustomer.EditValueChanged -= cmbCustomer_EditValueChanged;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }

        private void InitControls()
        {
            string tip = LocalData.IsEnglish ? "Please Input Code or EName or CName." : "请输入代码、中文名称或英文名称.";
            cmbCustomer.Properties.NullValuePrompt = tip;
            cmbCustomer.Properties.NullValuePromptShowForEmptyValue = true;


            InitComboboxSource();
            InitReleaseType();
            InitState();
            chkIsApply.CheckedChanged += delegate
            {
                if (chkIsApply.Checked)
                    _CurrentData.ApplyDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
                else
                    _CurrentData.ApplyDate =null;
            };
        }

        private void InitReleaseType()
        {
            if (_CurrentData.ReleaseType == ReleaseType.Original)
            {
                rdoReleaseType.SelectedIndex = 0;
                RefreshEnabledByReleaseType();
            }
            else
            {
                rdoReleaseType.SelectedIndex = 1;
                RefreshEnabledByReleaseType();
            }

            rdoReleaseType.SelectedIndexChanged += delegate
            {
                RefreshEnabledByReleaseType();
            };
        }

        private void InitState()
        {
            if (_CurrentData.State == ReleaseBLState.Released || _CurrentData.State == ReleaseBLState.Received)
            {
                if (_CurrentData.ReleaseType == ReleaseType.Original)
                    chkSendOriginal.Checked = true;
                else
                    chkNoticedTelex.Checked = true;


            }

            chkNoticedTelex.CheckedChanged += delegate
            {
                if (chkNoticedTelex.Checked)
                {
                    _CurrentData.State = ReleaseBLState.Released;
                    _CurrentData.ReleaseBy = LocalData.UserInfo.LoginName;
                    _CurrentData.ReleaseDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
                    if (_CurrentData.TelexNo.IsNullOrEmpty() && _CurrentData.ReleaseType == ReleaseType.Telex)
                    {
                        //提单号+当前时间（年月日时分：201107081430）
                        DateTime dtNow = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                        _CurrentData.TelexNo = _CurrentData.BlNo + dtNow.ToString("yyyyMMddHHmm");
                        //_CurrentData.TelexNo = _CurrentData.BlNo + dtNow.Year.ToString()
                        //    + (dtNow.Month >= 10 ? dtNow.Month.ToString() : "0" + dtNow.Month.ToString())
                        //    + (dtNow.Day >= 10 ? dtNow.Day.ToString() : "0" + dtNow.Day.ToString())
                        //    + (dtNow.Minute >= 10 ? dtNow.Minute.ToString() : "0" + dtNow.Minute.ToString())
                        //    + (dtNow.Second >= 10 ? dtNow.Second.ToString() : "0" + dtNow.Second.ToString());
                    }
                }
                else
                {
                    //_CurrentData.State = ReleaseBLState.Issue;
                    _CurrentData.ExpressOrderNo = string.Empty;
                    _CurrentData.TelexNo = string.Empty;
                    _CurrentData.ReleaseBy = string.Empty;
                    _CurrentData.ReleaseDate = null;
                }
            };


            chkSendOriginal.CheckedChanged += delegate
            {

                if (chkSendOriginal.Checked)
                {
                    _CurrentData.State = ReleaseBLState.Released;
                    _CurrentData.ReleaseBy = LocalData.UserInfo.LoginName;
                    _CurrentData.ReleaseDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
                }
                else
                {
                    //_CurrentData.State = ReleaseBLState.Issue;
                    _CurrentData.ExpressOrderNo = string.Empty;
                    _CurrentData.TelexNo = string.Empty;
                    _CurrentData.ReleaseBy = string.Empty;
                    _CurrentData.ReleaseDate = null;
                }
            };
        }

        private void RefreshEnabledByReleaseType()
        {
            if (rdoReleaseType.SelectedIndex == 0)
            {
                _CurrentData.ReleaseType = ReleaseType.Original;
                txtTelexNo.Text = _CurrentData.TelexNo = string.Empty;
                txtTelexNo.Enabled = false;

                txtExpressOrderNo.Enabled = chkSendOriginal.Enabled = true;

                chkNoticedTelex.Checked = false;
                txtTelexNo.Enabled = chkNoticedTelex.Enabled = false ;
                txtTelexNo.Text = _CurrentData.TelexNo = string.Empty;
            }
            else
            {
                _CurrentData.ReleaseType = ReleaseType.Telex;
                if (_CurrentData.TelexNo.IsNullOrEmpty()&& (short)_CurrentData.State >= (short)ReleaseBLState.Released)
                {
                    //提单号+当前时间（年月日时分：201107081430）
                    //_CurrentData.TelexNo = _CurrentData.BlNo + DateTime.Now.Year.ToString()
                    //       + (DateTime.Now.Month >= 10 ? DateTime.Now.Month.ToString() : "0" + DateTime.Now.Month.ToString())
                    //       + (DateTime.Now.Day >= 10 ? DateTime.Now.Day.ToString() : "0" + DateTime.Now.Day.ToString())
                    //       + (DateTime.Now.Minute >= 10 ? DateTime.Now.Minute.ToString() : "0" + DateTime.Now.Minute.ToString())
                    //       + (DateTime.Now.Second >= 10 ? DateTime.Now.Second.ToString() : "0" + DateTime.Now.Second.ToString());
                    //Modified by sunny
                    DateTime dtNow = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                    _CurrentData.TelexNo = _CurrentData.BlNo + dtNow.ToString("yyyyMMddHHmm");
                }


                chkNoticedTelex.Enabled = true;

                chkSendOriginal.Checked = false;
                txtExpressOrderNo.Enabled = chkSendOriginal.Enabled = false;
                txtExpressOrderNo.Text = _CurrentData.ExpressOrderNo = string.Empty;
            }
        }
        private IDisposable customerFinder;
        private void InitComboboxSource()
        {
            #region 放单状态

            List<EnumHelper.ListItem<ReleaseBLState>> releaseStates = EnumHelper.GetEnumValues<ReleaseBLState>(LocalData.IsEnglish);
            foreach (var item in releaseStates)
            {
                if (item.Value == 0) continue;

                if (item.Value == ReleaseBLState.Created)
                    cmbState.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value, 0));
                else
                    cmbState.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value, -1));
            }
            #endregion

            #region 客户

            OperationCommonInfo operationCommonInfo = FCMCommonService.GetOperationCommonInfo(_CurrentData.OperationID, _CurrentData.OperationType);
            _TradeCustoemrs = operationCommonInfo.TradeCustomers;
            if (_CurrentData.CustomerID.IsNullOrEmpty() == false)
            {
                OperationCustomer tager = _TradeCustoemrs.Find(delegate(OperationCustomer item) { return item.ID == _CurrentData.CustomerID; });
                if (tager == null)
                {
                    tager = new OperationCustomer();
                    tager.CName = tager.EName = _CurrentData.Customer;
                    tager.ID = _CurrentData.CustomerID;
                    _TradeCustoemrs.Add(tager);
                }

            }
            cmbCustomer.Properties.DataSource = _TradeCustoemrs;
            #region Customer
            //注册客户搜索器
            customerFinder= DataFindClientService.Register(cmbCustomer, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.CustomerResultValue,
                delegate(object inputSource, object[] resultData)
                {
                    Guid id = new Guid(resultData[0].ToString());
                    cmbCustomer.ClosePopup();
                    OperationCustomer tager = _TradeCustoemrs.Find(delegate(OperationCustomer item) { return item.ID == id; });
                    if (tager != null)
                    {
                        cmbCustomer.EditValue = _CurrentData.ID = id;
                        _CurrentData.Customer = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                    }
                    else
                    {
                        tager = new OperationCustomer();
                        tager.ID = id;
                        tager.Code = resultData[1].ToString();
                        tager.EName = resultData[2].ToString();
                        tager.CName = resultData[3].ToString();
                        tager.Term = int.Parse(resultData[9].ToString());
                        _TradeCustoemrs.Insert(0, tager);
                        cmbCustomer.Properties.DataSource = _TradeCustoemrs;
                        cmbCustomer.EditValue = _CurrentData.ID = id;
                        _CurrentData.Customer = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                    }
                },
                delegate()
                {
                    cmbCustomer.EditValue = null;
                    _CurrentData.Customer = string.Empty;
                    _CurrentData.CustomerID = Guid.Empty;
                },
                ClientConstants.MainWorkspace);

            #endregion
            cmbCustomer.EditValue = _CurrentData.CustomerID;
            cmbCustomer.EditValueChanged += new EventHandler(cmbCustomer_EditValueChanged);
            #endregion          
        }

        void cmbCustomer_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbCustomer.EditValue == null) return;
            Guid customerId = new Guid(cmbCustomer.EditValue.ToString());

            CustomerChanged(customerId);
        }

        private void CustomerChanged(Guid customerId)
        {
           string customerContact = FinanceService.GetReleaseBLRecentlyCustomerContact(customerId);
           if (customerContact.IsNullOrEmpty() == false)
           {
               if (XtraMessageBox.Show(LocalData.IsEnglish ? "TO DO" : ("该客户在上一个放单的联系人是:"+customerContact+",是否采用这个联系人?")
                   , LocalData.IsEnglish ? "Tip" : "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
               {
                   txtContact.Text = _CurrentData.CustomerContact = customerContact;
               }
           }
         
        }


        #region Save

        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Save();
            }
        }

        private bool Save()
        {
            ReleaseBLList currentData = bindingSource1.DataSource as ReleaseBLList;
            if (currentData == null) return false;
            if (currentData.Validate
                (
                delegate(ValidateEventArgs e)
                {
                    if ((short)currentData.State >= (short)ReleaseBLState.Released)
                    {
                        if (currentData.ReleaseType == ReleaseType.Telex && currentData.TelexNo.IsNullOrEmpty())
                        {
                            e.SetErrorInfo("TelexNo", LocalData.IsEnglish ? "TelexNo Must Input." : "电放号必需输入");
                        }
                    }
                }

                ) == false)
            {
                return false;
            }

            try
            {
                SingleResult result = FinanceService.SaveReleaseBLInfo(currentData.ID
                                                                , currentData.CustomerID
                                                                , currentData.CustomerContact 
                                                                , currentData.ReleaseType
                                                                , currentData.State
                                                                , currentData.TelexNo.Encrypt(currentData.ID.ToString(), EncryptType.DES_ID )
                                                                , currentData.ExpressOrderNo
                                                                , currentData.Remark
                                                                , LocalData.UserInfo.LoginID
                                                                , currentData.UpdateDate
                                                                , currentData.BLUpdateDate);

                currentData.CancelEdit();
                
                //if (currentData.State == ReleaseBLState.Created) currentData.State = ReleaseBLState.Issue;

                currentData.ID = result.GetValue<Guid>("ID");
                currentData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                currentData.BLUpdateDate = result.GetValue<DateTime?>("BLUpdateDate");
                currentData.BeginEdit();
                if (Saved != null) Saved(currentData, new object[] { result });

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                return false;
            }
        }

        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindForm().Close();
        }

        #endregion

        #region IEditPart 成员

        public override object DataSource
        {
            get { return bindingSource1.DataSource; }
            set { BindingData(value); }
        }
        void BindingData(object data)
        {
            _CurrentData = data as ReleaseBLList;
            if (_CurrentData == null)
            {
                Enabled = false;
                bindingSource1.DataSource = typeof(ReleaseBLList);
                return; 
            }

            InitDataByNew(_CurrentData);


            bindingSource1.DataSource = _CurrentData;
            ((BaseDataObject)data).CancelEdit();
            ((BaseDataObject)data).BeginEdit();
        }

        private void InitDataByNew(ReleaseBLList data)
        {
            if (_CurrentData.State == ReleaseBLState.Created)
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "保存将自动签收该放单记录" : "保存将自动签收该放单记录");
        }

        public override bool SaveData()
        {
            return Save();
        }

        public override void EndEdit()
        {
            Validate();
            bindingSource1.EndEdit();
        }

        public override event SavedHandler Saved;

        #endregion
       
    }
}
