using System;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FAM.UI
{
    public partial class SetExpressPart : BaseEditPart
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

        public SetExpressPart()
        {
            InitializeComponent();
            Disposed += delegate {
                Saved = null;
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
            get { return bsList.DataSource; }
            set { bsList.DataSource = value; }
        }

        public override event SavedHandler Saved;

        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidateData() == false) return;
            try
            {
                InvoiceList list = bsList.DataSource as InvoiceList;

                SingleResult result = FinanceService.SetInvoiceExpressInfo(list.ID, list.ExpressNo, list.ExpressDate
                                                                     , LocalData.UserInfo.LoginID, list.UpdateDate, LocalData.IsEnglish);

                list.UpdateDate = result.GetValue<DateTime?>("UpdateDate");

                if (Saved != null) Saved(new object[] { bsList.DataSource });
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Set Successfully" : "设置成功");
                FindForm().Close();
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); }
        }
        #region 验证
        bool ValidateData()
        {
            bool isSrcc = true;
            if (txtExpressNo.Text.Trim().IsNullOrEmpty())
            {
                dxErrorProvider1.SetError(txtExpressNo, "Must Input.");
                isSrcc = false;
            }
            if (dteExpressDate.EditValue == null)
            {
                dxErrorProvider1.SetError(dteExpressDate, "Must Input.");
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