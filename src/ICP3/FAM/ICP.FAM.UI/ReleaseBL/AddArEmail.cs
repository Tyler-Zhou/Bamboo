using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using ICP.Framework.ClientComponents;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FAM.UI.ReleaseBL
{
    public partial class AddArEmail : BaseEditPart
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }

        }


        #endregion

        public Guid OperationID = Guid.Empty;
        public Guid CustomerID = Guid.Empty;

        List<ReleaseAndArContact> releaseAndArList = null;
        List<CustomerCarrierObjects> cusList = new List<CustomerCarrierObjects>();

        public AddArEmail()
        {
            InitializeComponent();
        }

        private void AddArEmail_Load(object sender, EventArgs e)
        {
            if (!LocalData.IsEnglish)
            {
                colName.Caption = "客户";
                colEMail.Caption = "邮箱";
            }

            ContactObjects dataList = FCMCommonService.GetContactList(OperationID, OperationType.OceanExport);
            if (dataList != null && dataList.CustomerCarrier != null)
            {
                cusList = dataList.CustomerCarrier;
            }

            releaseAndArList = FCMCommonService.GetReleaseAndArContactList(OperationID);

            if (releaseAndArList != null && releaseAndArList.Count > 0)
            {
                cmbAREmail.Properties.Items.Clear();
                foreach (var item in releaseAndArList)
                {
                    cmbAREmail.Properties.Items.Add(item.Mail);
                }

                if (releaseAndArList.Count(r => r.IsAR) > 0)
                {
                    cmbAREmail.Text = releaseAndArList.Find(r => r.IsAR).Mail;
                }

                if (string.IsNullOrEmpty(cmbAREmail.Text))
                {
                    cmbAREmail.SelectedIndex = 0;
                }
            }
            else
            {
                List<CustomerCarrierObjects> list = FCMCommonService.GetLatesterContactList(OperationType.OceanExport,null,CustomerID, ContactType.Customer, ContactStage.AR);
                if (list != null && list.Count > 0)
                {
                    cmbAREmail.Text = list[0].Mail;
                }
            }

            gcCustomer.DataSource = cusList.FindAll(r => r.AR);
            gvCustomer.RefreshData();

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            List<CustomerCarrierObjects> DataSourceList = new List<CustomerCarrierObjects>();
            CustomerCarrierObjects newCustomer = null;
            if (!string.IsNullOrEmpty(cmbAREmail.Text))
            {
                if (cusList != null)
                {
                    newCustomer = cusList.Find(r => r.Mail.ToUpper().Trim() == cmbAREmail.Text.ToUpper().Trim());
                }

                if (newCustomer == null)
                {
                    newCustomer = new CustomerCarrierObjects();
                    newCustomer.Release = false;
                    newCustomer.AR = true;
                    newCustomer.CreateByID = LocalData.UserInfo.LoginID;
                    newCustomer.Name = cmbAREmail.Text.Substring(0, cmbAREmail.Text.IndexOf('@'));
                    newCustomer.Mail = cmbAREmail.Text;
                    newCustomer.OceanBookingID = OperationID;
                    newCustomer.OperationType = OperationType.OceanExport;
                    newCustomer.Type = ContactType.Customer;
                    newCustomer.CustomerID = CustomerID;
                    DataSourceList.Add(newCustomer);
                }
                else
                {
                    newCustomer.Release = false;
                    newCustomer.AR = true;
                    newCustomer.CustomerID = CustomerID;
                    DataSourceList.Add(newCustomer);
                }
            }

            if (DataSourceList.Count > 0)
            {
                if (DialogResult.Yes == MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "Do you want to save AR and ReleaseBL contact mail"
                                              : "是否保存催款及催放单联系人邮箱？", "Tips", MessageBoxButtons.YesNo))
                {
                    FCMCommonService.SaveContactList(DataSourceList);
                }

                //this.FindForm().Close();
            }
            DataSourceList.Clear();
            ContactObjects dataList = FCMCommonService.GetContactList(OperationID, OperationType.OceanExport);
            if (dataList != null && dataList.CustomerCarrier != null)
            {
                cusList = dataList.CustomerCarrier;
            }

            gvCustomer.FocusedRowChanged -= gvCustomer_FocusedRowChanged;
            gcCustomer.DataSource = cusList.FindAll(r => r.AR);
            gvCustomer.RefreshData();
            gvCustomer.FocusedRowChanged += gvCustomer_FocusedRowChanged;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            FindForm().Close();
        }

        private void gvCustomer_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
            {
                return;
            }

            cmbAREmail.Text = cusList[e.FocusedRowHandle].Mail;
        }

        private void btDel_Click(object sender, EventArgs e)
        {
            if (gvCustomer.FocusedRowHandle < 0)
            {
                return;
            }

            if (DialogResult.Yes == MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "Do you want to Delelt AR contact mail"
                                             : "是否删除催款及催放单联系人邮箱？", "Tips", MessageBoxButtons.YesNo))
            {
                FCMCommonService.RemoveContactInfo(cusList[gvCustomer.FocusedRowHandle].Id, LocalData.UserInfo.LoginID, cusList[gvCustomer.FocusedRowHandle].UpdateDate);
                ContactObjects dataList = FCMCommonService.GetContactList(OperationID, OperationType.OceanExport);
                if (dataList != null && dataList.CustomerCarrier != null)
                {
                    cusList = dataList.CustomerCarrier;
                }

                gvCustomer.FocusedRowChanged -= gvCustomer_FocusedRowChanged;
                gcCustomer.DataSource = cusList.FindAll(r => r.AR);
                gvCustomer.RefreshData();
                gvCustomer.FocusedRowChanged += gvCustomer_FocusedRowChanged;
            }
        }
    }
}
