using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.AirImport.ServiceInterface;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.AirImport.UI
{
    [ToolboxItem(false)]
    public partial class OIBusinessTransfer : BaseEditPart
    {
        public OIBusinessTransfer()
        {
            InitializeComponent();
        }

        #region 服务

        [ServiceDependency]
        public IAirImportService oiService { get; set; }

        [ServiceDependency]
        public IOrganizationService orgService { get; set; }

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
        AirBusinessInfo bsInfo = null;
        AirBusinessMBLList mblInfo = null; 
        #endregion

        #region 初始化控件

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                InitControls();
            }
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {

            bsInfo = oiService.GetBusinessInfo(BusinessID);

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
                mblInfo = oiService.GetAIMBLInfo(bsInfo.MBLID.Value);
                this.txtMBLNo.Text = mblInfo.MBLNo;
                this.txtSubNo.Text = mblInfo.SubNo;
                this.txtITNo.Text = mblInfo.ITNO;
            }

            List<OrganizationList> orgList= orgService.GetOfficeList();

            foreach (OrganizationList org in orgList)
            {
                if (org.ID == bsInfo.CompanyID)
                {
                    //当前操作公司不显示在下拉框中
                    continue;
                }
                string orgName = LocalData.IsEnglish ? org.EShortName : org.CShortName;

                this.cmbNewCompanyID.Properties.Items.Add(new ImageComboBoxItem(orgName,org.ID));
            }

        }


        #endregion

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

            Guid newCompanyID=new Guid(cmbNewCompanyID.EditValue.ToString());

            try
            {
                SingleResult singleResult=oiService.TransferBusiness(BusinessID, newCompanyID, LocalData.UserInfo.LoginID, bsInfo.UpdateDate);

                UpdateDate = singleResult.GetValue<DateTime?>("UpdateDate");
                this.FindForm().DialogResult = DialogResult.OK;
                //this.FindForm().Close();
            }
            catch(Exception ex)
            {
                string message=(LocalData.IsEnglish ? "Transfer Faily" : "转移失败.")+ ex.Message;
                DevExpress.XtraEditors.XtraMessageBox.Show(message);              
            }

            this.FindForm().Close();
        }

        #endregion
    }
}
