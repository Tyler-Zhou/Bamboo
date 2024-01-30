using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FAM.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;

namespace ICP.FAM.UI.CustomerBill.Print
{
    public partial class SelectBillPart : BaseEditPart
    {   
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        Dictionary<string, List<BillList>> _orgList = new Dictionary<string, List<BillList>>();

        public SelectBillPart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                _orgList.Clear();
                _orgList = null;
                gcMain.DataSource = null;
                bsBillList.DataSource = null;
                bsBillList.Dispose();
                if (WorkItem != null)
                {
                    WorkItem.Items.Remove(this);
                    WorkItem = null;
                }
            
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            foreach (var item in _orgList)
            {
                cmbCustomer.Properties.Items.Add(new ImageComboBoxItem(item.Key));
            }
            cmbCustomer.SelectedIndexChanged += new EventHandler(cmbCustomer_SelectedIndexChanged);
            cmbCustomer.SelectedIndex = 0;
        }

        void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string customerName = cmbCustomer.EditValue == null ? string.Empty : cmbCustomer.EditValue.ToString();
            if (customerName.IsNullOrEmpty()) return;

            bsBillList.DataSource = _orgList[customerName];
            bsBillList.ResetBindings(false);
        }

        #region IEditPart 成员

        /// <summary>
        /// List。BillList 传入前请判断是否为空，而且数据中的CustoemrName不能为空
        /// </summary>
        public override object DataSource
        {
            get
            {
                return bsBillList.DataSource;
            }
            set
            {
                BindingData(value);
            }
        }

        private void BindingData(object value)
        {
            List<BillList> list = value as List<BillList>;
            foreach (var item in list)
            {
                if (_orgList.ContainsKey(item.CustomerName) == false) _orgList.Add(item.CustomerName, new List<BillList>());
                _orgList[item.CustomerName].Add(item);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FindForm().Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            FindForm().DialogResult = DialogResult.OK;
            FindForm().Close();
        }

        //private void BindingData(object data)
        //{
        //    if (data == null)
        //    {
        //        this.Enabled = false;
        //        this.bsBillInfo.DataSource = typeof(BillInfo);
        //        this.bsChargeList.DataSource = typeof(ChargeList);
        //    }
        //    else
        //    {
        //        BillInfo source = data as BillInfo;

        //        InitByNewData(source);
        //        this.bsBillInfo.DataSource = source;
        //        this.bsChargeList.DataSource = source.Fees;

        //        InitTrem();
        //        InitBillType();
        //        InitTradeCustomers();
        //        InitCurrencyRate();
        //        InitCustomerPopup();
        //        this.Enabled = true;
        //        ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).CancelEdit();
        //        ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).BeginEdit();

        //    }

        //    RefreshBillTatol();
        //}

        //private void InitByNewData(BillInfo source)
        //{
        //    if (source.IsNew)
        //    {
        //        cmbCustomer.Focus();
        //        source.Fees = new List<ChargeList>();
        //        if (_OperationCommonInfo.CurrentFormID.IsNullOrEmpty() == false)
        //        {
        //            source.FormID = _OperationCommonInfo.CurrentFormID;
        //            source.FormType = _OperationCommonInfo.Forms.Find(delegate(FormData item) { return item.ID == _OperationCommonInfo.CurrentFormID; }).Type;
        //        }
        //        rdoGroupType.Enabled = true;
        //    }
        //    else
        //    {
        //        rdoGroupType.Enabled = false;
        //    }
        //}
        //public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        //public override void EndEdit()
        //{
        //    this.Validate();
        //    bsBillInfo.EndEdit();
        //    bsChargeList.EndEdit();
        //    bsCurrencyRateData.EndEdit();
        //}

        #endregion
    }
}
