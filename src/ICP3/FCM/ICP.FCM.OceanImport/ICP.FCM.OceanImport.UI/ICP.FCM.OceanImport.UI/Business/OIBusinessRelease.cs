using System;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;
using Microsoft.Practices.CompositeUI;

namespace ICP.FCM.OceanImport.UI
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
            if (!LocalData.IsDesignMode)
            {
                this.Disposed += delegate
                {
                    businessList = null;
                    Saved = null;
                    this.errorList.DataSource = null;
                    this.cmbReleaseType.OnFirstEnter -= this.OncmbReleaseTypeFirstEnter;
                    if (this.WorkItem != null)
                    {
                        this.WorkItem.Items.Remove(this);
                        this.WorkItem = null;
                    }
                };
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
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

        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }



        #endregion

        #region 属性
        public bool IsInternalAgent
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

        /// <summary>
        /// 业务对象
        /// </summary>
        private OceanBusinessList businessList;

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
            set { BindingData(value); }
        }

        private void BindingData(object value)
        {
            if (value == null)
            {
                return;
            }
            businessList = value as OceanBusinessList;

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
        private void OncmbReleaseTypeFirstEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.SetComboxByEnum<FCMReleaseType>(this.cmbReleaseType, true);
        }
        private void InitControls()
        {
            ////放货方式
            //Utility.SetEnterToExecuteOnec(cmbReleaseType, delegate
            //{
            //ICPCommUIHelperService.SetComboxByEnum<ReleaseType>(this.cmbReleaseType, true);
            //});
            //this.cmbReleaseType.Enabled = IsInternalAgent;

            this.cmbReleaseType.ShowSelectedValue(businessList.ReleaseType,
              EnumHelper.GetDescription<FCMReleaseType>(businessList.ReleaseType, LocalData.IsEnglish));
            this.cmbReleaseType.OnFirstEnter += this.OncmbReleaseTypeFirstEnter;


            this.txtConsigneeName.Text = businessList.ConsigneeName;
            this.txtCustomerName.Text = businessList.CustomerName;
            this.txtMBLNo.Text = businessList.MBLNo;
            this.txtSubNo.Text = businessList.SubNo;
            this.txtNo.Text = businessList.No;
            this.txtSales.Text = businessList.SalesName;
        }
        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!ValidateData())
            {
                return;
            }
            try
            {
                SingleResult result = OceanImportService.SetOIReleaseData(businessList.ID, this.dtpReleaseDate.DateTime, businessList.UpdateDate, LocalData.UserInfo.LoginID);
                if (result != null)
                {
                    if (string.IsNullOrEmpty(result.GetValue<string>("ErrorMessage")))
                    {
                    BusinessReleaseInfo releaseinfo = new BusinessReleaseInfo();
                    releaseinfo.UpdateDate = result.GetValue<DateTime>("UpdateDate");
                    releaseinfo.Releasedate = this.dtpReleaseDate.DateTime;
                    releaseinfo.ReleaseType = (FCMReleaseType)this.cmbReleaseType.EditValue;
                    if (Saved != null)
                    {
                        Saved(new object[] { releaseinfo, businessList });
                    }
                }
                else
                {
                    string str = LocalData.IsEnglish ? "No:" : "业务号:";
                        str = str + businessList.No + " " + result.GetValue<string>("ErrorMessage");
                    string endstr = LocalData.IsEnglish ? " No RC Conditions" : " 不满足放货条件.";
                    DevExpress.XtraEditors.XtraMessageBox.Show(endstr);           
                }
                }
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
