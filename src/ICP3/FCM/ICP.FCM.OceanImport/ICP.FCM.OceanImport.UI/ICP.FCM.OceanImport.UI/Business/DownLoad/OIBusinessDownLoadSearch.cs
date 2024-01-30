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
using ICP.DataCache.ServiceInterface;
using System.Diagnostics;
using System.Reflection;
using ICP.FileSystem.ServiceInterface;

namespace ICP.FCM.OceanImport.UI
{
    public partial class OIBusinessDownLoadSearch : ICP.Framework.ClientComponents.UIFramework.BaseSearchPart
    {
        public OIBusinessDownLoadSearch()
        {
            InitializeComponent();
            if (!LocalData.IsDesignMode)
            {
                this.Disposed += delegate 
                {
                    this.companyIDList = null;
                    this.OnSearched = null;
                    this.cmbDateType.EditValueChanged -= this.cmbDateType_EditValueChanged;
                    this.cmbDateType.OnFirstEnter -= this.OncmbDateTypeFirstEnter;
                    this.cmbDocState.OnFirstEnter -= this.OncmbDocStateFirstEnter;
                    this.cmbReleaseCarogo.OnFirstEnter -= this.OncmbReleaseCarogoFirstEnter;
                    this.cmbState.OnFirstEnter -= this.OncmbStateFirstEnter;
                    this.cmbAgentID.OnFirstEnter -= this.OncmbAgentIDFirstEnter;
                    this.cmbCompanyID.OnFirstEnter -= this.OncmbCompanyIDFirstEnter;
                    if (Workitem !=null)
                    {
                        Workitem.Items.Remove(this);
                        Workitem = null;
                    }
                };
            }
        }

        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

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


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!LocalData.IsDesignMode)
            {
                InitControl();
            }
        }
        List<Guid> companyIDList = new List<Guid>();
        private void OncmbCompanyIDFirstEnter(object sender, EventArgs e)
        {
            //绑定操作公司 
            List<ICP.Framework.CommonLibrary.Server.LocalOrganizationInfo> orglist = LocalData.UserInfo.UserOrganizationList.FindAll(item => item.Type == ICP.Framework.CommonLibrary.Server.LocalOrganizationType.Company);
            this.cmbCompanyID.Properties.BeginUpdate();
            this.cmbCompanyID.Properties.Items.Clear();
            this.cmbCompanyID.Properties.Items.Add(new ImageComboBoxItem(null, null));
            foreach (ICP.Framework.CommonLibrary.Server.LocalOrganizationInfo org in orglist)
            {
                string orgName = LocalData.IsEnglish ? org.EShortName : org.CShortName;

                this.cmbCompanyID.Properties.Items.Add(new ImageComboBoxItem(orgName, org.ID));

                companyIDList.Add(org.ID);
            }
            this.cmbCompanyID.Properties.EndUpdate();
            this.cmbCompanyID.EditValue = LocalData.UserInfo.DefaultCompanyID;
        }
        private void OncmbAgentIDFirstEnter(object sender, EventArgs e)
        {
            //绑定代理公司
            List<OrganizationList> alllist = Utility.GetAllCompanyList(OrganizationService);
            //代理公司不包含当前用户所在的公司
            alllist.RemoveAll(item => companyIDList.Contains(item.ID));
            this.cmbAgentID.Properties.BeginUpdate();
            this.cmbAgentID.Properties.Items.Add(new ImageComboBoxItem(string.Empty, null));
            foreach (OrganizationList org in alllist)
            {
                string orgName = LocalData.IsEnglish ? org.EShortName : org.CShortName;

                this.cmbAgentID.Properties.Items.Add(new ImageComboBoxItem(orgName, org.ID));
            }
            this.cmbAgentID.Properties.EndUpdate();
        }
        private void OncmbStateFirstEnter(object sender, EventArgs e)
        {
            ///加载状态
            List<EnumHelper.ListItem<DownLoadState>> stateType = EnumHelper.GetEnumValues<DownLoadState>(LocalData.IsEnglish);
            stateType.RemoveAll(item => item.Value == DownLoadState.Unknown);
            this.cmbState.Properties.BeginUpdate();
            this.cmbState.Properties.Items.Add(new ImageComboBoxItem(string.Empty, null));
            foreach (var item in stateType)
            {
                cmbState.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            this.cmbState.Properties.EndUpdate();
        }
        private void OncmbDateTypeFirstEnter(object sender, EventArgs e)
        {
            ///加载时间类型
            List<EnumHelper.ListItem<DateType>> dteType = EnumHelper.GetEnumValues<DateType>(LocalData.IsEnglish);
            this.cmbDateType.Properties.BeginUpdate();
            this.cmbDateType.Properties.Items.Add(new ImageComboBoxItem(string.Empty, null));
            foreach (var item in dteType)
            {
                cmbDateType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            this.cmbDateType.Properties.EndUpdate();
        }
        private void OncmbDocStateFirstEnter(object sender, EventArgs e)
        {
            //分文件状态
            List<EnumHelper.ListItem<DocumentState>> docState = EnumHelper.GetEnumValues<DocumentState>(LocalData.IsEnglish);
            this.cmbDocState.Properties.BeginUpdate();
            foreach (var item in docState)
            {
                if (((int)item.Value)<3)
                {
                    cmbDocState.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
                }

            }
            this.cmbDocState.Properties.EndUpdate();
        }
        private void OncmbReleaseCarogoFirstEnter(object sender, EventArgs e)
        {
            //放单状态
            List<EnumHelper.ListItem<ReleaseCarogoState>> releasestate = EnumHelper.GetEnumValues<ReleaseCarogoState>(LocalData.IsEnglish);
            this.cmbReleaseCarogo.Properties.BeginUpdate();
            foreach (var item in releasestate)
            {
                cmbReleaseCarogo.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            this.cmbReleaseCarogo.Properties.EndUpdate();
        }
        /// <summary>
        /// 初始化窗体控件
        /// </summary>
        private void InitControl()
        {
            this.cmbCompanyID.ShowSelectedValue(LocalData.UserInfo.DefaultCompanyID, LocalData.UserInfo.DefaultCompanyName);
            this.cmbCompanyID.OnFirstEnter += this.OncmbCompanyIDFirstEnter;
            this.cmbAgentID.OnFirstEnter += this.OncmbAgentIDFirstEnter;

            this.cmbState.OnFirstEnter += this.OncmbStateFirstEnter;

            this.cmbDateType.OnFirstEnter += this.OncmbDateTypeFirstEnter;
            this.cmbDocState.OnFirstEnter += this.OncmbDocStateFirstEnter;
            this.cmbReleaseCarogo.OnFirstEnter += this.OncmbReleaseCarogoFirstEnter;

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
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    OnSearched(this, GetData());
                    MethodBase method = MethodBase.GetCurrentMethod();
                    StopwatchHelper.EndStopwatch(stopwatch, DateTime.Now, method.DeclaringType.FullName, "GET-OILIST", "获得下载列表");
                }
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
            string companyID = null;
            OEBLState? state = null;
            DateType? dateType = null;
            DocumentState? docState = null;
            ReleaseCarogoState? releaseState = null;

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

            if (this.cmbState.EditValue != null)
            {
                state = (OEBLState)this.cmbState.EditValue;
            }

            if (this.cmbDocState.EditValue != null)
            {
                docState = (DocumentState)this.cmbDocState.EditValue;
            }

            if (this.cmbReleaseCarogo.EditValue != null)
            {
                releaseState = (ReleaseCarogoState)this.cmbReleaseCarogo.EditValue;
            }

            List<OceanBusinessDownLoadList> list = OceanImportService.GetOceanExportList(
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
                                            docState,
                                            state,
                                            true,
                                            releaseState,
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
