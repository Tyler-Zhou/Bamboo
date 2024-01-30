using System;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.AirImport.ServiceInterface;
using Microsoft.Practices.ObjectBuilder;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;

namespace ICP.FCM.AirImport.UI
{
    /// <summary>
    /// 放货
    /// </summary>
    [ToolboxItem(false)]
    public partial class OIBusinessRelease : BaseEditPart
    {
        public OIBusinessRelease()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        #region 服务


        [ServiceDependency]
        public IAirImportService oiService { get; set; }


        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ICPCommUIHelper ICPCommUIHelperService { get; set; }

        #endregion

        #region 属性
        public AirBusinessList list
        {
            get;
            set;
        }
        public DateTime UpdateTime
        {
            get;
            set;
        }

        public DateTime ReleaseDate
        {
            get;
            set;
        }

        public FCMReleaseType releaseType
        {
            set;
            get;
        }
        #endregion


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
            }
        }

        private bool ValidateData()
        {
            if (this.cmbReleaseType.EditValue == null || (FCMReleaseType)this.cmbReleaseType.EditValue == FCMReleaseType.Unknown)
            {
                string message = LocalData.IsEnglish ? "Please Select ReleaseType" : "请选择放货类型";
                errorList.SetError(cmbReleaseType, message);
                cmbReleaseType.Focus();
                return false;
            }
            if (this.dtpReleaseDate.EditValue == null)
            {
                string message = LocalData.IsEnglish ? "Please Select ReleaseDate" : "请选择放货日期";
                errorList.SetError(dtpReleaseDate, message);
                dtpReleaseDate.Focus();
                return false;
            }

            return true;
        }

        private void InitControls()
        {
            ////放货方式
            //Utility.SetEnterToExecuteOnec(cmbReleaseType, delegate
            //{
           //ICPCommUIHelper.SetComboxByEnum<ReleaseType>(this.cmbReleaseType, true);
            //});
            this.cmbReleaseType.ShowSelectedValue(list.ReleaseType,
              EnumHelper.GetDescription<FCMReleaseType>(list.ReleaseType, LocalData.IsEnglish));
            ICPCommUIHelperService.SetComboxByEnum<FCMReleaseType>(this.cmbReleaseType,true);

            this.txtConsigneeName.Text = list.ConsigneeName;
            this.txtCustomerName.Text = list.CustomerName;
            this.txtMBLNo.Text = list.MBLNo;
            this.txtSubNo.Text = list.SubNo;
            this.txtNo.Text = list.No;
            this.txtSales.Text = list.SalesName;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!ValidateData())
            {
                return;
            }
            try
            {

                SingleResult result = oiService.SetAIReleaseData(list.ID, (FCMReleaseType)this.cmbReleaseType.EditValue, this.dtpReleaseDate.DateTime, list.UpdateDate, LocalData.UserInfo.LoginID);

                UpdateTime = result.GetValue<DateTime>("UpdateDate");
                ReleaseDate = this.dtpReleaseDate.DateTime;
                releaseType = (FCMReleaseType)this.cmbReleaseType.EditValue;

                this.FindForm().DialogResult = DialogResult.OK;
                this.FindForm().Close();
            }
            catch (Exception ex)
            {
                string message = (LocalData.IsEnglish ? "Release Faily" : "放货失败.") + ex.Message;
                DevExpress.XtraEditors.XtraMessageBox.Show(message);
            }
        }
    }
}
