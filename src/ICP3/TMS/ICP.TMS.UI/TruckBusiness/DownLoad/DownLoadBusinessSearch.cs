using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using DevExpress.XtraEditors.Controls;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.ClientComponents.Controls;
using ICP.TMS.ServiceInterface;

namespace ICP.TMS.UI
{
    public partial class DownLoadBusinessSearch : ICP.Framework.ClientComponents.UIFramework.BaseSearchPart
    {
        public DownLoadBusinessSearch()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this.cmbState.OnFirstEnter -= this.OncmbStateFirstEnter;
                this.cmbCompany.OnFirstEnter -= this.OncmbCompanyFirstEnter;
                this.cmbType.OnFirstEnter -= this.OncmbTypeFirstEnter;
                this.OnSearched = null;
                if (this.Workitem != null)
                {
                    this.Workitem.Items.Remove(this);
                    this.Workitem = null;
                }
            
            };
        }

        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public ITruckBookingService TruckBookingService
        {
            get
            {
                return ServiceClient.GetService<ITruckBookingService>();
            }
        }
        
        #endregion


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                InitControl();
            }
        }
        private void OncmbStateFirstEnter(object sender, EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<DownLoadState>> truckState = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<DownLoadState>(LocalData.IsEnglish);
            cmbState.Properties.BeginUpdate();
            foreach (var item in truckState)
            {
                cmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }

            cmbState.Properties.EndUpdate();
        }
        private void OncmbCompanyFirstEnter(object sender, EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Server.LocalOrganizationInfo> orglist = Utility.GetCompanyList();
            this.cmbCompany.Properties.BeginUpdate();
            this.cmbCompany.Properties.Items.Clear();
            foreach (var org in orglist)
            {
                string orgName = LocalData.IsEnglish ? org.EShortName : org.CShortName;

                this.cmbCompany.Properties.Items.Add(new ImageComboBoxItem(orgName, org.ID));

                companyIDList.Add(org.ID);
            }
            this.cmbCompany.Properties.EndUpdate();
        }
        /// <summary>
        /// 初始化窗体控件
        /// </summary>
        private void InitControl()
        {
            this.dateMonthControl1.IsEngish = LocalData.IsEnglish;
            this.bgDate.Caption ="ETD/ETA";

            //初始化状态下拉框
            cmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "All" : "全部", 0));
            cmbState.SelectedIndex = 0;
            this.cmbState.OnFirstEnter += this.OncmbStateFirstEnter;
            
            //绑定当前用户的公司列表
            this.cmbCompany.ShowSelectedValue(LocalData.UserInfo.DefaultCompanyID, LocalData.UserInfo.DefaultCompanyName);
            this.cmbCompany.OnFirstEnter += this.OncmbCompanyFirstEnter;
            
            //类型
            cmbType.ShowSelectedValue(0, LocalData.IsEnglish ? "ALL" : "全部");
            this.cmbType.SelectedIndex = 0;
            this.cmbType.OnFirstEnter += this.OncmbTypeFirstEnter;

        }
        private void OncmbTypeFirstEnter(object sender, EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<TruckBookingType>> businessType = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<TruckBookingType>(LocalData.IsEnglish);
            cmbType.Properties.BeginUpdate();
            cmbType.Properties.Items.Clear();
            cmbType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "ALL" : "全部", System.DBNull.Value));
            foreach (var item in businessType)
            {
                cmbType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            this.cmbType.SelectedIndex = 0;
            cmbType.Properties.EndUpdate();
        }
 
        List<Guid> companyIDList = new List<Guid>();

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Workitem.Commands[TMSDownLoadCommandConstants.Command_SearchDate].Execute();
           
        }

        [CommandHandler(TMSDownLoadCommandConstants.Command_SearchDate)]
        public void Command_SearchDate(object sender, EventArgs e)
        {
            if (OnSearched != null)
            {
                using (new CursorHelper())
                {
                    OnSearched(this, GetData());
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
                else if (item is MultiSearchCommonBox)
                {
                    (item as MultiSearchCommonBox).EditText = null;
                    (item as MultiSearchCommonBox).EditValue = null;
                    (item as MultiSearchCommonBox).Tag = null;
                }
                else if (item is DevExpress.XtraEditors.TextEdit
                    && (item is DevExpress.XtraEditors.SpinEdit) == false
                    && item.Enabled == true
                    && (item as DevExpress.XtraEditors.TextEdit).Properties.ReadOnly == false)
                    item.Text = string.Empty;

            }

        }

        #endregion

        public override object GetData()
        {
            TruckBookingType? type;
            Guid companyID;

            if(this.cmbType.SelectedIndex>0)
            {
                type=(TruckBookingType)this.cmbType.SelectedIndex;
            }
            else
            {
               type=null; 
            }
            if (this.cmbCompany.EditValue != null)
            {
                companyID = (Guid)this.cmbCompany.EditValue;
            }
            else
            {
                companyID = Guid.Empty;
            }

            List<DownLoadOceanBusinessList> list = TruckBookingService.GetOceandBusinessList(
                                        type,
                                        companyID,
                                        this.txtCustomerRefNo.Text,
                                        this.txtBoxNo.Text,
                                        this.txtVesselName.Text,
                                        this.txtVoyage.Text,
                                        this.cmbState.SelectedIndex,
                                        this.dateMonthControl1.From,
                                        this.dateMonthControl1.To,
                                        (int)this.numMax.Value,
                                        LocalData.UserInfo.LoginID,
                                        LocalData.IsEnglish);

            return list;
        }



        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbType.SelectedIndex == 0)
            {
                this.bgDate.Caption = "ETD/ETA";
            }
            else if (this.cmbType.SelectedIndex == 1)
            {
                this.bgDate.Caption = "ETA";
            }
            else if (this.cmbType.SelectedIndex == 2)
            {
                this.bgDate.Caption = "ETD";
            }
        }
 

    }


}
