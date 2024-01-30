using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Common;
using ICP.FAM.ServiceInterface;
using ICP.Common.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FAM.ServiceInterface.DataObjects.SaveRequests;
using ICP.Framework.ClientComponents.Service;

namespace ICP.FAM.UI.TelexApply
{
    [ToolboxItem(false)]
    public partial class TelexApplyEditPart : BaseEditPart
    {
        #region 服务注入

        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region 属性及变量
        TelexApplyInfo _telexApply = new TelexApplyInfo();
        bool isDirty = false;
        /// <summary>
        /// 是否有数据发生改变
        /// </summary>
        private bool IsDirty
        {
            get
            {
                if (_telexApply == null)
                {
                    return false;
                }
                if (isDirty)
                {
                    return true;
                }
                if (_telexApply.IsDirty)
                {
                    return true;
                }
                return _telexApply.Consignees.Any(item => item.IsDirty);
            }
        }
        #endregion

        #region init

        public TelexApplyEditPart()
        {
            InitializeComponent();
            Disposed += delegate {
                if (customerFinder != null)
                {
                    customerFinder.Dispose();
                    customerFinder = null;
                }
                if (consigneeFinder != null)
                {
                    consigneeFinder.Dispose();
                    consigneeFinder = null;
                }
                _telexApply = null;
                Saved = null;
                lwGridControl1.DataSource = null;
                
                dxErrorProvider1.DataSource = null;
                bsTelexApply.DataSource = null;
                bsTelexApply.Dispose();
                
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };

            SetConsignees();
            if (LocalData.IsEnglish)
            {
                txtCustomer.DataBindings.Add(new Binding("Text", bsTelexApply, "CustomerEName", true));
            }
            else
            {
                txtCustomer.DataBindings.Add(new Binding("Text", bsTelexApply, "CustomerCName", true));
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
                InitMessage();
                Closing += TelexApplyEditPart_Closing;
                rdoTelexType.SelectedIndexChanged += rdoTelexType_SelectedIndexChanged;
            }
        }

        void rdoTelexType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                RadioGroup rg = sender as RadioGroup;
                if (rg != null)
                {
                    var selTelexType=(TelexType) rg.SelectedIndex;
                    _telexApply.TelexType = selTelexType;
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }

        void TelexApplyEditPart_Closing(object sender, FormClosingEventArgs e)
        {
            if (IsDirty)
            {
                DialogResult dr = FAMUtility.EnquireIsSaveCurrentDataByUpdated();

                if (dr == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (dr == DialogResult.Yes)
                {
                    if (!Save())
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void InitControls()
        {
            SetDataLazyLoaders();
            SearchRegister();
            rdoTelexType.SelectedIndex = 1;
        }

        private void InitMessage()
        {
            RegisterMessage("1108190001", LocalData.IsEnglish ? "Input the consignee info" : "请录入收货人信息");
        }

        #region 搜索器注册
        private IDisposable customerFinder, consigneeFinder;
        void SearchRegister()
        {
            //客户搜索器
          customerFinder=  DataFindClientService.Register(txtCustomer, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName,
                SearchFieldConstants.CustomerResultValue,
                      delegate(object inputSource, object[] resultData)
                      {
                          _telexApply.CustomerEName = resultData[2].ToString();
                          _telexApply.CustomerCName = resultData[3].ToString();
                          txtCustomer.Text = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                          txtCustomer.Tag = _telexApply.CustomerId = new Guid(resultData[0].ToString());

                      }, delegate
                      {
                          txtCustomer.Text = _telexApply.CustomerCName = _telexApply.CustomerEName = string.Empty;
                          txtCustomer.Tag = _telexApply.CustomerId = Guid.Empty;

                      },
                      ClientConstants.MainWorkspace);



          consigneeFinder=  DataFindClientService.RegisterGridColumnFinder(colConsignees
                                    , CommonFinderConstants.CustoemrFinder
                                    , "CustomerId"
                                    , "CustomerName"
                                    , "ID"
                                    , LocalData.IsEnglish ? "EName" : "CName");
        }

        #endregion

        #region 下拉列表数据延迟加载

        void SetDataLazyLoaders()
        {
            FAMUtility.BindComboBoxByCompany(cmbCompany);
        }

        #endregion

        #endregion

        #region Save

        private void btnOK_Click(object sender, EventArgs e)
        {
            Save();
        }
        public bool Leaving()
        {
            if (_telexApply != null && _telexApply.IsDirty)
            {
                if (FAMUtility.EnquireIsSaveCurrentDataByUpdated() == DialogResult.Yes)
                {
                    return Save();
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }


        private bool Save()
        {
            txtApplicant.Focus();
            if (ValidateData() == false)
            {
                return false;
            }

            if (!IsDirty)
            {
                return true;
            }

            TelexRequestSaveRequest saveRequest = new TelexRequestSaveRequest();
            saveRequest.Id = _telexApply.ID;
            saveRequest.ApplyTime = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
            saveRequest.TelexType = _telexApply.TelexType;
            saveRequest.CompanyId = _telexApply.CompanyId;
            saveRequest.CreateById = LocalData.UserInfo.LoginID;
            saveRequest.CustomerId = _telexApply.CustomerId;
            saveRequest.CustomerDescription = _telexApply.CustomerDescription;
            saveRequest.IsValid = _telexApply.IsValid;
            saveRequest.Remark = _telexApply.Remark;
            saveRequest.UpdateDate = _telexApply.UpdateDate;

            if (cbxOpenEnd.Checked)
            {
                saveRequest.ValidDate = DateTime.MaxValue;
            }
            else
                saveRequest.ValidDate = _telexApply.ValidDate;

            saveRequest.ConsigneeIds = new List<Guid>();

            if (!chkForAllConsignees.Checked)
            {
                saveRequest.ConsigneeIds = _telexApply.Consignees.Select(o => o.CustomerId).ToList();

                if (saveRequest.ConsigneeIds == null || saveRequest.ConsigneeIds.Count == 0)
                {
                    FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1108190001"));
                    return false;
                }
            }

            try
            {


                SingleResult result = FinanceService.SaveTelexRequest(saveRequest);

                _telexApply.ID = result.GetValue<Guid>("ID");
                _telexApply.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                _telexApply.ConsigneeName = GetConsignee();

                if (Saved != null) Saved(_telexApply);

                _telexApply.CancelEdit();
                isDirty = false;

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");


                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                return false;
            }
        }

        private bool ValidateData()
        {
            EndEdit();

            bsTelexApply.EndEdit();

            foreach (TelexConsignee item in _telexApply.Consignees)
            {
                item.EndEdit();
            }

            return _telexApply.Validate();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FindForm().Close();
        }


        #endregion

        #region IEditPart 成员

        public override object DataSource
        {
            get { return bsTelexApply.DataSource; }
            set { BindingData(value); }
        }
        void BindingData(object data)
        {

            if (data == null)
            {
                bsTelexApply.DataSource = typeof(TelexApplyInfo);

                consigneesBindingSource.DataSource = typeof(TelexConsignee);

                Enabled = false;
            }
            else
            {
                Enabled = true;
                _telexApply = data as TelexApplyInfo;

                if (_telexApply == null && data != null)
                {
                    _telexApply = FinanceService.GetTelexApply(((TelexApplyList)data).ID);
                }

                bsTelexApply.DataSource = _telexApply;
                bsTelexApply.ResetBindings(false);


                consigneesBindingSource.DataSource = _telexApply.Consignees;
                consigneesBindingSource.ResetBindings(false);
                rdoTelexType.SelectedIndex = (int)_telexApply.TelexType;
                cbxOpenEnd.Checked = _telexApply.ValidDate.Year == DateTime.MaxValue.Year;
                _telexApply.IsDirty = false;
                isDirty = false;

                ((BaseDataObject)data).CancelEdit();
                ((BaseDataObject)data).BeginEdit();
            }
        }

        public override bool SaveData()
        {
            return Save();
        }

        public override void EndEdit()
        {
            bsTelexApply.EndEdit();
            consigneesBindingSource.EndEdit();
        }

        public override event SavedHandler Saved;

        #endregion

        private void cbxForAllConsignees_CheckedChanged(object sender, EventArgs e)
        {
            SetConsignees();
        }

        void SetConsignees()
        {
            groupConsignee.Enabled = !chkForAllConsignees.Checked;
        }

        private void bbiAddConsignee_ItemClick(object sender, ItemClickEventArgs e)
        {
            TelexConsignee consignee = new TelexConsignee();

            consigneesBindingSource.Add(consignee);


            bsTelexApply.ResetBindings(false);
            gvCustomer.ClearSorting();

            isDirty = true;

            gvCustomer.MoveLast();
        }

        private void bbiRemoveConsignee_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!FAMUtility.EnquireIsDeleteCurrentData())
            {
                return;
            }

            _telexApply.IsDirty = true;
            isDirty = true;

            consigneesBindingSource.RemoveCurrent();

            gvCustomer.CloseEditor();
            consigneesBindingSource.EndEdit();

            gvCustomer.Focus();

            consigneesBindingSource.ResetBindings(false);



        }

        private void gvCustomer_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            _telexApply.ConsigneeName = _telexApply.Consignees.Select(o => o.CustomerName).ToArray().Join(",");

            isDirty = true;

        }

        private void bbiSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Save();
            }
        }

        /// <summary>
        /// 获得收货人名称
        /// </summary>
        /// <returns></returns>
        private string GetConsignee()
        {
            string str = string.Empty;

            List<TelexConsignee> DataList = consigneesBindingSource.DataSource as List<TelexConsignee>;

            if (DataList == null)
            {
                return string.Empty;
            }

            foreach (TelexConsignee item in DataList)
            {
                if (string.IsNullOrEmpty(str))
                {
                    str = item.CustomerName;
                }
                else
                {
                    str = str + "," + item.CustomerName;
                }
            }

            return str;

        }

        private void cbxOpenEnd_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxOpenEnd.Checked)
            {
                dteValidDate.Enabled = false;
                _telexApply.ValidDate = DateTime.MaxValue;
            }
            else
            {

                dteValidDate.Enabled = true;
            }
        }
    }
}
