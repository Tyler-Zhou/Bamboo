using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Common.UI.TransportFoundation.VesselVoyage
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class VesselAddPart : DevExpress.XtraEditors.XtraForm 
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }

        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }

        #endregion

        #region init

        VesselInfo CurrentData
        {
            get { return bindingSource1.DataSource as VesselInfo; }
            set { bindingSource1.DataSource = value; }
        }
        public VesselInfo VesselInfoData = null;
        List<CustomerList> _carrierList = null;

        public VesselAddPart()
        {
            InitializeComponent();
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate {
                this.VesselInfoData = null;
                this._carrierList = null;
                this.dxErrorProvider1.DataSource = null;
                this.bindingSource1.DataSource = null;
                this.bindingSource1.Dispose();
                this.cmbCarrier.SelectedIndexChanged -= this.cmbCarrier_SelectedIndexChanged;

                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
                
            };
        }

        private void SetCnText()
        {
            barSave.Caption = "保存(&S)";
            barClose.Caption = "关闭(&C)";
            this.labCarrier.Text = "船东";
            labCode.Text = "代码";
            labName.Text = "名称";
        }

        protected override void OnLoad(EventArgs e)
        {
            SearchRegister();
            base.OnLoad(e);
        }
        private bool isFirstTimeEnter = true;
        private void cmbCarrierEnter(object sender, EventArgs e)
        {
            if (!isFirstTimeEnter)
            {
                return;
            }
            isFirstTimeEnter = false;
            _carrierList = CustomerService.GetCustomerListByList(string.Empty, string.Empty, string.Empty, string.Empty,
                                                                          string.Empty, string.Empty, null, null, null,
                                                                          CustomerType.Carrier, null, null, null, null, null, 0);

            if (_carrierList != null && _carrierList.Count > 0)
            {
                this.cmbCarrier.Properties.BeginUpdate();
                cmbCarrier.Properties.Items.Clear();
                cmbCarrier.Properties.Items.Add(string.Empty);

                foreach (CustomerList item in _carrierList)
                {
                    cmbCarrier.Properties.Items.Add(LocalData.IsEnglish ? item.EName : item.CName);
                }
                this.cmbCarrier.Properties.EndUpdate();
                cmbCarrier.SelectedIndex = 0;
            }
        }
        void SearchRegister()
        {
            this.cmbCarrier.Enter += this.cmbCarrierEnter;
          
        }

        #endregion

        public void SetSource(VesselInfo data) 
        {
            this.bindingSource1.DataSource = data;
            data.BeginEdit();
        }

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (SaveData() == true)
            {
                var findForm = this.FindForm();
                if (findForm != null) findForm.Close();
            }
        }

        private void cmbCarrier_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.cmbCarrier.SelectedItem != null && !string.IsNullOrEmpty(this.cmbCarrier.SelectedItem.ToString()))
            {
                string carrierName = this.cmbCarrier.SelectedItem.ToString();
                foreach (var item in _carrierList)
                {
                    if ((LocalData.IsEnglish ? item.EName : item.CName).ToUpper() == carrierName.ToUpper())
                    {
                        CurrentData.CarrierID = item.ID;
                        CurrentData.CarrierName = carrierName;
                        break;
                    }
                }
            }
        }

        bool SaveData()
        {
            this.Validate();
            bindingSource1.EndEdit();
            if (CurrentData.Validate() == false) return false;

            try
            {
                SingleResultData result = TransportFoundationService.SaveVesselInfo(CurrentData.ID,
                                                                    CurrentData.Code,
                                                                    CurrentData.Name,
                                                                    CurrentData.CarrierID,
                                                                    LocalData.UserInfo.LoginID,
                                                                    CurrentData.UpdateDate,
                                                                    CurrentData.IMO,
                                                                    CurrentData.UNCode,
                                                                    CurrentData.Registration);

                CurrentData.CancelEdit();
                CurrentData.ID = result.ID;
                CurrentData.CarrierName = cmbCarrier.Text.Trim();
                CurrentData.UpdateDate = result.UpdateDate;
                VesselInfoData = CurrentData;

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
                return false;
            }
        }

        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (CurrentData.IsDirty)
            {
                DialogResult dlg = CommonUtility.EnquireIsSaveCurrentDataByUpdated();
                if (dlg == DialogResult.Yes)
                {
                    if (SaveData() == false) return;
                }
                else if (dlg == DialogResult.Cancel) return;
                else if (dlg == DialogResult.No)
                {
                    var findForm = this.FindForm();
                    if (findForm != null) findForm.Close();
                }
            }

            var form = this.FindForm();
            if (form != null) form.Close();
        }
    }
}
