using System;
using System.Collections.Generic;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Server;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using DevExpress.XtraEditors.Controls;
namespace ICP.FAM.UI
{
    public partial class TransitionPart : BaseEditPart
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


        public TransitionPart()
        {
            InitializeComponent();
            Disposed += delegate {
                Saved = null;
                bsList.DataSource = null;
                bsList.Dispose();
                if (Workitem != null) 
                { 
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
            }

        }
        public override object DataSource
        {
            get { return bsList.DataSource; }
            set { bsList.DataSource = value; }
        }
        public override event SavedHandler Saved;
        private void InitControls()
        {
            #region Company

            List<LocalOrganizationInfo> userCompanyList = FAMUtility.GetCompanyList();
            cmbCompanyID.Properties.BeginUpdate();
            cmbCompanyID.Properties.Items.Clear();
            foreach (var item in userCompanyList)
            {
                cmbCompanyID.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? item.EShortName : item.CShortName,item.ID));
         
            }
            cmbCompanyID.Properties.EndUpdate();

            #endregion


            ReleaseRCList list = bsList.DataSource as ReleaseRCList;

            txtInvoiceNo.Text = list.BlNo;
            txtExpressNo.Text = list.ConsigneeName;
            textEdit3.Text = list.RcCompanyName;
            textEdit3.Tag = list.RcCompanyID;
        }
        public Guid company
        {
            get
            {
                if (cmbCompanyID.EditValue != null && cmbCompanyID.EditValue != DBNull.Value)
                {
                    return (Guid)cmbCompanyID.EditValue;
                }
                else return Guid.Empty;
            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                ReleaseRCList list = bsList.DataSource as ReleaseRCList;

                SingleResult result = FinanceService.SaveReleaseCompany(list.ID, company
                                                                     , list.UpdateDate, LocalData.UserInfo.LoginID, LocalData.IsEnglish);
                list.ID = result.GetValue<Guid>("ID");
                list.UpdateDate = result.GetValue<DateTime?>("UpdateDate");

                if (Saved != null) Saved(new object[] { bsList.DataSource });
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Update Successfully" : "修改成功");
                FindForm().Close();
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            FindForm().Close();
        }
    }
}
