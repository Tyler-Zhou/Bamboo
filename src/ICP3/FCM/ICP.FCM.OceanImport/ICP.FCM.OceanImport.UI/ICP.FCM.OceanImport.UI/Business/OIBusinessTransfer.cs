using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using ICP.Operation.Common.ServiceInterface;

namespace ICP.FCM.OceanImport.UI
{
    [ToolboxItem(false)]
    public partial class OIBusinessTransfer : BaseEditPart
    {
        public OIBusinessTransfer()
        {
            InitializeComponent();
            SyncLocalData = true;
            if (!LocalData.IsDesignMode)
            {
                this.Disposed += delegate 
                {  

                    bsInfo = null;
                    mblInfo = null;
                    this.Saved = null;
                    this.errorList.DataSource = null;
                    if (this.WorkItem != null)
                    {
                        this.WorkItem.Items.Remove(this);
                        this.WorkItem = null;
                    }
                };
            }
        }

        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key.ToUpper() == "BusinessOperationParameter".ToUpper())
                {
                    _businessOperationParameter = item.Value as BusinessOperationParameter;
                    break;

                }
            }
        }

        #region 服务
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public IOceanImportService OceanImportService
        {
            get
            {
                return ServiceClient.GetService<IOceanImportService>();
            }
        }

        public IOrganizationService OrganizationService
        {
            get
            {
                return ServiceClient.GetService<IOrganizationService>();
            }
        }

        #endregion

        #region 属性
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate
        {
            get;
            set;
        }

        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid BusinessID
        {
            get;
            set;
        }
        OceanBusinessInfo bsInfo = null;
        OceanBusinessMBLList mblInfo = null;

        /// <summary>
        /// 邮件中心与ICP业务关联信息
        /// </summary>
        BusinessOperationParameter _businessOperationParameter = null;
        #endregion

        #region 初始化控件

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            bsInfo = OceanImportService.GetBusinessInfo(BusinessID);

            this.txtConsigneeName.Text = bsInfo.ConsigneeName;
            this.txtCustomerName.Text = bsInfo.CustomerName;
            this.txtNo.Text = bsInfo.No;
            this.txtoldCompanyName.Text = bsInfo.CompanyName;
            this.txtPlaceOfReceiptName.Text = bsInfo.PlaceOfDeliveryName;
            this.txtPOD.Text = bsInfo.PODName;
            this.txtPOL.Text = bsInfo.POLName;
            this.txtSalesName.Text = bsInfo.SalesName;

            if (bsInfo.MBLID != null && bsInfo.MBLID.Value != Guid.Empty)
            {
                mblInfo = OceanImportService.GetOIMBLInfo(bsInfo.MBLID.Value);
                this.txtMBLNo.Text = mblInfo.MBLNo;
                this.txtSubNo.Text = mblInfo.SubNo;
                this.txtITNo.Text = mblInfo.ITNO;
            }

            List<OrganizationList> orgList = OrganizationService.GetOfficeList();

            foreach (OrganizationList org in orgList)
            {
                if (org.ID == bsInfo.CompanyID)
                {
                    //当前操作公司不显示在下拉框中
                    continue;
                }
                string orgName = LocalData.IsEnglish ? org.EShortName : org.CShortName;

                this.cmbNewCompanyID.Properties.Items.Add(new ImageComboBoxItem(orgName, org.ID));
            }

        }


        #endregion

        public override object DataSource
        {
            set { BindingData(value); }
        }

        private void BindingData(object value)
        {
            BusinessID = (Guid)value;
            InitControls();
        }

        #region 取消
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }
        #endregion

        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;
        #region 确认
        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cmbNewCompanyID.EditValue == null)
            {
                string message = LocalData.IsEnglish ? "Please Select New Company" : "请选择新公司";
                errorList.SetError(cmbNewCompanyID, message);
                cmbNewCompanyID.Focus();
                return;
            }
            Guid newCompanyID = new Guid(cmbNewCompanyID.EditValue.ToString());

            try
            {
                SingleResult singleResult = OceanImportService.TransferBusiness(BusinessID, newCompanyID, LocalData.UserInfo.LoginID, bsInfo.UpdateDate);

                UpdateDate = singleResult.GetValue<DateTime?>("UpdateDate");

                if (_businessOperationParameter == null)
                {
                    _businessOperationParameter = new BusinessOperationParameter();
                }

                if (Saved != null) Saved(new object[] { UpdateDate, _businessOperationParameter, _businessOperationParameter.Context });

                this.FindForm().Close();
            }
            catch (Exception ex)
            {
                string message = (LocalData.IsEnglish ? "Transfer Faily" : "转移失败.") + ex.Message;
                DevExpress.XtraEditors.XtraMessageBox.Show(message);
            }
        }
        #endregion
    }
}
