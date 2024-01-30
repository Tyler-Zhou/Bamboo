using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.ClientComponents.Controls;

namespace ICP.FCM.OceanImport.UI
{
    public partial class OIBusinessDownLoadSearch : ICP.Framework.ClientComponents.UIFramework.BaseSearchPart
    {
        public OIBusinessDownLoadSearch()
        {
            InitializeComponent();
        }

        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IOceanImportService oiService { get; set; }

        [ServiceDependency]
        public IUserService userService { get; set; }

        [ServiceDependency]
        public IOrganizationService orgService { get; set; }
        
        #endregion


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                InitControl();
            }
        }
        List<Guid> companyIDList = new List<Guid>();

        /// <summary>
        /// 初始化窗体控件
        /// </summary>
        private void InitControl()
        {
           
            //绑定操作公司 
            List<OrganizationList> orglist = Utility.GetCompanyList(userService);
            this.cmbCompanyID.Properties.Items.Add(new ImageComboBoxItem(null, null));
            foreach (OrganizationList org in orglist)
            {
                string orgName = LocalData.IsEnglish ? org.EShortName : org.CShortName;

                this.cmbCompanyID.Properties.Items.Add(new ImageComboBoxItem(orgName, org.ID));

                companyIDList.Add(org.ID);
            }
            this.cmbCompanyID.EditValue = LocalData.UserInfo.DefaultCompanyID;

            //绑定代理公司
            List<OrganizationList> alllist = Utility.GetAllCompanyList(orgService);
            this.cmbAgentID.Properties.Items.Add(new ImageComboBoxItem(string.Empty, null));         
            foreach (OrganizationList org in alllist)
            {
                //代理公司不包含当前用户所在的公司
                if (companyIDList.Contains(org.ID))
                {
                    continue;
                }
                string orgName = LocalData.IsEnglish ? org.EShortName : org.CShortName;

                this.cmbAgentID.Properties.Items.Add(new ImageComboBoxItem(orgName, org.ID));
            }

            ///加载状态
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<DownLoadState>> stateType = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<DownLoadState>(LocalData.IsEnglish);
            this.cmbState.Properties.Items.Add(new ImageComboBoxItem(string.Empty, null));
            foreach (var item in stateType)
            {
                if (item.Value.ToString() == "Unknown")
                {
                    continue;
                }

                cmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }

            ///加载时间类型
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<DateType>> dteType = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<DateType>(LocalData.IsEnglish);
            this.cmbDateType.Properties.Items.Add(new ImageComboBoxItem(string.Empty, null));
            foreach (var item in dteType)
            {             
                cmbDateType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Workitem.Commands[OIBusinessDownLoadCommandConstants.Command_SearchDate].Execute();
           
        }

        [CommandHandler(OIBusinessDownLoadCommandConstants.Command_SearchDate)]
        public void Command_SearchDate(object sender, EventArgs e)
        {
            if (OnSearched != null)
            {
                OnSearched(this, GetData());
            }
        }    

        #region 清空

        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, System.EventArgs e)
        {
            foreach (Control item in bgcBase.Controls)
            {
                if (item is LWImageComboBoxEdit)
                {
                    (item as LWImageComboBoxEdit).SelectedIndex = 0;
                }
                else if (item is DevExpress.XtraEditors.TextEdit
                    && (item is DevExpress.XtraEditors.SpinEdit) == false
                    && item.Enabled == true
                    && (item as DevExpress.XtraEditors.TextEdit).Properties.ReadOnly == false)
                    item.Text = string.Empty;

            }

            cmbDateType.SelectedIndex = 0;
        }

        #endregion

        public override object GetData()
        {
            Guid? agentID = null;
            DateTime? beginDate = null;
            DateTime? endDate = null;
            string companyID=null;
            OEBLState? state=null;
            DateType? dateType = null;

            if (this.cmbAgentID.EditValue != null)
            {
                agentID = new Guid(this.cmbAgentID.EditValue.ToString());
            }

            if (this.cmbDateType.EditValue != null)
            {
                dateType = (DateType)this.cmbDateType.EditValue;
            }
            if (txtBeginDate.EditValue != null)
            {
                beginDate = txtBeginDate.DateTime;
            }
            if (txtEndDate.EditValue != null)
            {
                endDate = txtEndDate.DateTime;
            }
            if (cmbCompanyID.EditValue != null)
            {
                companyID = this.cmbCompanyID.EditValue.ToString();
            }
            else
            {
                companyID = companyIDList.ToArray().Join();
            }

            if(this.cmbState.EditValue!=null)
            {
                state=(OEBLState)this.cmbState.EditValue;
            }

            List<OceanBusinessDownLoadList> list = oiService.GetOceanExportList(
                                            this.txtBLNo.Text.Trim(),
                                            this.txtBoxNo.Text.Trim(),
                                            this.txtVesselName.Text.Trim(),
                                            this.txtVoyage.Text.Trim(),
                                            dateType,
                                            beginDate,
                                            endDate,
                                            this.txtPOL.Text.Trim(),
                                            this.txtPOD.Text.Trim(),
                                            this.txtPlaceOfDelivery.Text.Trim(),
                                            this.txtConsignee.Text.Trim(),
                                            agentID,
                                            companyID,
                                            state,
                                            true,
                                            int.Parse(this.numMax.Value.ToString()));

            return list;
        }

        private void cmbDateType_EditValueChanged(object sender, System.EventArgs e)
        {
            if (cmbDateType.EditValue != null)
            {
                txtBeginDate.Enabled = true;
                txtEndDate.Enabled = true;
                txtBeginDate.DateTime = DateTime.Now;
                txtEndDate.DateTime = DateTime.Now;
            }
            else
            {
                txtBeginDate.Enabled = false;
                txtEndDate.Enabled = false;
            }
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;      

    }


}
