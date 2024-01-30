using System;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FAM.ServiceInterface.DataObjects;
using System.Collections.Generic;

namespace ICP.FAM.UI
{
    public partial class SetInvoiceNoPart : BaseEditPart
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        #endregion

        #region init

        public SetInvoiceNoPart()
        {
            InitializeComponent();
            Disposed += delegate {
                Saved = null;
                dxErrorProvider1.DataSource = null;
                bsList.DataSource = null;
                bsList.Dispose();
                _currentInvoiceList = null;
                
                if (Workitem != null)
                { 
                    Workitem.Items.Remove(this);
                    Workitem = null;
                } 
            };
        }
        #endregion

        #region IEditPart 成员

        public override object DataSource
        {
            //get { return bsList.DataSource; }
            //set { bsList.DataSource = value; }
            set
            {
                BindingData(value);
            }
        }

        private void BindingData(object value)
        {
            _currentInvoiceList = value as List<InvoiceList>;
            if (_currentInvoiceList == null || _currentInvoiceList.Count == 0)
            {
                txtInvoiceNo.Text = string.Empty;
                txtExpressNo.Text = string.Empty;
                dteInvoiceDate.EditValue = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            }
            else
            {
                string invoiceNoString = string.Empty;
                string expressNoString = string.Empty;
                foreach (var item in _currentInvoiceList)
                {
                    if (!string.IsNullOrEmpty(invoiceNoString))
                    {
                        invoiceNoString += ", ";
                    }

                    if (!string.IsNullOrEmpty(expressNoString))
                    {
                        expressNoString += ", ";
                    }

                    invoiceNoString += item.InvoiceNo;
                    expressNoString += item.ExpressNo;
                }

                txtInvoiceNo.Text = invoiceNoString;
                txtExpressNo.Text = expressNoString;
                dteInvoiceDate.EditValue = _currentInvoiceList[0].InvoiceDate;
            }

            txtBillNo.Text = CurrentBill.BillNO;
        }

        public override event SavedHandler Saved;

        List<InvoiceList> _currentInvoiceList = null;
        public CurrencyBillList CurrentBill{ get;set; }       
     
        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidateData() == false) return;
            try
            {
                List<Guid> invoiceIds = new List<Guid>();
                List<DateTime?> invoiceUpdateDates = new List<DateTime?>();
                foreach (var item in _currentInvoiceList)
                {
                    invoiceIds.Add(item.ID);
                    invoiceUpdateDates.Add(item.UpdateDate);
                }

                FinanceService.SaveInvoiceNoAndExpressNoForBills(new Guid[] { CurrentBill.ID },
                    new Guid[] { CurrentBill.CurrencyID },
                    new FeeWay[] { CurrentBill.Way },
                    new bool[] { CurrentBill.IsCommission },
                    new string[] { invoiceIds.ToArray().Join() },
                     new string[] { txtInvoiceNo.Text.Trim()},
                      new string[] { txtExpressNo.Text.Trim() },
                      new string[] { dteInvoiceDate.EditValue.ToString() },
                       LocalData.UserInfo.LoginID,
                      new string[] { invoiceUpdateDates.ToArray().Join() },
                    LocalData.IsEnglish);

                if (Saved != null) Saved(new object[] { txtInvoiceNo.Text.Trim(), txtExpressNo.Text.Trim() });
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Set Successfully" : "设置成功");
                FindForm().Close();
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); }
        }

        #region 验证
        bool ValidateData()
        {
            bool isSrcc = true;
            if (txtInvoiceNo.Text.Trim().IsNullOrEmpty())
            {
                dxErrorProvider1.SetError(txtInvoiceNo, "Must Input.");
                isSrcc = false;
            }

            if (dteInvoiceDate.EditValue == null)
            {
                dxErrorProvider1.SetError(dteInvoiceDate, "Must Input.");
                isSrcc = false;
            }

            return isSrcc;
        }
        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            FindForm().Close();
        }
    }
}