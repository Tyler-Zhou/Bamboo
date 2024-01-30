using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Microsoft.Practices.CompositeUI;

using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.ObjectBuilder;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.ClientComponents.Controls;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;

namespace ICP.FCM.OceanImport.UI
{
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class OIBusinessSearch : ICP.Framework.ClientComponents.UIFramework.BaseSearchPart
    {

        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        [ServiceDependency]
        public ICP.Sys.ServiceInterface.IUserService userService { get; set; }

        
        [ServiceDependency]
        public IOceanImportService oIService { get; set; }


        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ICPCommUIHelper ICPCommUIHelperServices { get; set; }


        #endregion

        #region 初始化
        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;
        public OIBusinessSearch()
        {
            InitializeComponent();
            if (DesignMode) return;

            this.Disposed += delegate { if (Workitem != null) Workitem.Items.Remove(this); };
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                this.fromToDateMonthControl1.IsEngish = LocalData.IsEnglish;
                InitControls();
                SetKeyDownToSearch();
            }
        }
        private void SetKeyDownToSearch()
        {
            foreach (Control item in bgcBase.Controls)
            {
                item.KeyDown += new KeyEventHandler(item_KeyDown);
            }
        }

        void item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2)
            {
                this.btnSearch.PerformClick();
            }
            else if (e.KeyCode == Keys.F3)
            {
                this.btnClear.PerformClick();
            }
        }

        public Guid companyID
        {
            get
            {
                if (this.cmbCompany.EditValue == null || (Guid)this.cmbCompany.EditValue == Guid.Empty)
                {
                    return LocalData.UserInfo.DefaultCompanyID;
                }
                else
                {
                    return (Guid)this.cmbCompany.EditValue;
                }
            }
        }

        /// <summary>
        /// 加载控件
        /// </summary>
        private void InitControls()
        {
            //注册延迟加载
            SetLazyLoaders();
            SetLazyDataLodersWithDynamicCondition();


            List<UserList> userList = new List<UserList>();


            //客服
            Utility.SetEnterToExecuteOnec(this.cmbCustomerService, BindCustomerService);

            //文件
            Utility.SetEnterToExecuteOnec(this.cmbFiler, BindFiler);

            //状态
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OIOrderState>> orderStates = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OIOrderState>(LocalData.IsEnglish);
            cmbState.Properties.BeginUpdate();
            cmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "ALL" : "全部", System.DBNull.Value));
            foreach (var item in orderStates)
            {
                if (item.Value == OIOrderState.Unknown) continue;
                cmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbState.Properties.EndUpdate();
            cmbState.SelectedIndex = 0;

            //时间
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OIBusinessDateSearchType>> dateSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OIBusinessDateSearchType>(LocalData.IsEnglish);
            cmbDate.Properties.BeginUpdate();
            foreach (var item in dateSearchTypes)
            {
                if (item.Value.ToString() == "All")
                {
                    continue;
                }
                cmbDate.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbDate.Properties.EndUpdate();
            this.cmbDate.SelectedIndex = 0;

            SetControlsEnterToSearch();
        }

        /// <summary>
        /// 绑定客服
        /// </summary>
        private void BindCustomerService()
        {
            ICPCommUIHelperServices.SetMcmbUsers(this.cmbCustomerService, companyID, "客服", string.Empty, true);  
         }
        private void BindFiler()
        {
            ICPCommUIHelperServices.SetMcmbUsers(this.cmbFiler, companyID, "文件", string.Empty, true);  
        }
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null)
            {
                return;
            }

            this.txtAgent.Text = string.Empty;
            this.txtCustomer.Text = string.Empty;
            this.txtNo.Text = string.Empty;
            this.txtCtnNO.Text = string.Empty;
            this.txtBLNO.Text = string.Empty;
            this.txtShipper.Text = string.Empty;
            this.txtConsignee.Text = string.Empty;
            this.txtLoadingPort.Text = string.Empty;
            this.txtDischargePort.Text = string.Empty;
            this.txtPlaceOfDelivery.Text = string.Empty;



            foreach (var item in values)
            {
                string value = item.Value == null ? string.Empty : item.Value.ToString();
                switch (item.Key)
                {
                    case "No":
                        this.txtNo.Text = value;
                        break;
                    case "MBLNo":
                        this.txtBLNO.Text = value;
                        break;
                    case "SubNo":
                        this.txtBLNO.Text = value;
                        break;
                    case "ContainerNo":
                        this.txtCtnNO.Text = value;
                        break;
                    case "CustomerName":
                        this.txtCustomer.Text = value;
                        break;
                    case "AgentName":
                        this.txtAgent.Text = value;
                        break;
                    case "ConsigneeName":
                        this.txtConsignee.Text = value;
                        break;
                    case "ShipperName":
                        this.txtShipper.Text = value;
                        break;
                    case "POLName":
                        this.txtLoadingPort.Text = value;
                        break;
                    case "PODName":
                        this.txtDischargePort.Text = value;
                        break;
                    case "PlaceOfDeliveryName":
                        this.txtPlaceOfDelivery.Text = value;
                        break;
                }
            }
        }

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

          /// <summary>
        /// 注册延迟加载的数据源
        /// </summary>
        void SetLazyLoaders()
        {
            ////操作公司列表   
            //Utility.SetEnterToExecuteOnec(cmbCompany, delegate
            //{
                List<OrganizationList> userCompanyList = userService.GetUserCompanyList(LocalData.UserInfo.LoginID, OrganizationType.Company);
                cmbCompany.Properties.BeginUpdate();
                cmbCompany.Properties.Items.Clear();
                cmbCompany.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(string.Empty, null));        
             
                foreach (var item in userCompanyList)
                {
                    cmbCompany.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? item.EShortName : item.CShortName, item.ID));        
                }

                cmbCompany.Properties.EndUpdate();
                cmbCompany.EditValue = LocalData.UserInfo.DefaultCompanyID;
            //});
        }

        /// <summary>
        /// 延迟加载，而且条件是动态的
        /// </summary>
        void SetLazyDataLodersWithDynamicCondition()
        {
            this.cmbSales.Enter += new EventHandler(cmbSales_Enter);
        }
        /// <summary>
        /// 加载揽货人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbSales_Enter(object sender, EventArgs e)
        {
            Guid depID = Guid.Empty;
            if (cmbCompany.EditValue != null && string.IsNullOrEmpty(cmbCompany.EditValue.ToString()))
            {
                depID = (Guid)cmbCompany.EditValue;
            }
            ICPCommUIHelperServices.SetComboxUsers(cmbSales, depID, string.Empty, string.Empty, true);
        }

        /// <summary>
        /// 设置一些TextBox输入Enter键时,触发查询事件
        /// </summary>
        private void SetControlsEnterToSearch()
        {
            foreach (Control item in bgcBase.Controls)
            {
                if (item is DevExpress.XtraEditors.TextEdit
                && (item is DevExpress.XtraEditors.SpinEdit) == false)
                {
                    item.KeyDown += delegate(object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.Enter) this.btnSearch.PerformClick();
                    };
                }
            }
        }

        #endregion

        #region 清空
        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
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

            lwchkIsValid.Checked = null;
        }

        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (OnSearched != null)
                    OnSearched(this, GetData());
            }
        }

        /// <summary>
        /// 获得数据
        /// </summary>
        /// <returns></returns>
        public override object GetData()
        {
            DateTime? dtFrom = null, dtTo = null;
            OIBusinessDateSearchType dateSearchType = OIBusinessDateSearchType.All;
            OIOrderState? orderState = null;
            Guid? customerServiceID = null;
            Guid? filerID = null;
            Guid? salesID = null;
            Guid[] companyIDs=null;
 
            if (this.cmbCompany.EditValue != null)
            {
                companyIDs=new Guid[1];
                companyIDs[0] = new Guid(this.cmbCompany.EditValue.ToString());
            }
            else
            {
                companyIDs=Utility.GetCompanyIDs(userService);
            }

            if (this.cmbSales.EditValue != null && new Guid(this.cmbSales.EditValue.ToString())!=Guid.Empty)
            {
                salesID = new Guid(this.cmbSales.EditValue.ToString());
            }
            if (this.cmbCustomerService.EditValue != null && new Guid(this.cmbCustomerService.EditValue.ToString()) != Guid.Empty)
            {
                customerServiceID = new Guid(this.cmbCustomerService.EditValue.ToString());
            }

            if (this.cmbFiler.EditValue != null && new Guid(this.cmbFiler.EditValue.ToString()) != Guid.Empty)
            {
                filerID = new Guid(this.cmbFiler.EditValue.ToString());
            }

            if (bgDate.Expanded && cmbDate.EditValue != null && cmbDate.EditValue.ToString() != OIBusinessDateSearchType.All.ToString())
            {
                dateSearchType = (OIBusinessDateSearchType)cmbDate.EditValue;
                dtFrom = fromToDateMonthControl1.From;
                dtTo = fromToDateMonthControl1.To;
            }
            if (cmbState.EditValue != null && !string.IsNullOrEmpty(cmbState.EditValue.ToString()))
            {
                orderState = (OIOrderState)cmbState.EditValue;
            }

            try
            {
            List<OceanBusinessList> list=oIService.GetBusinessList(
                                    companyIDs,
                                    this.txtNo.Text.Trim(),
                                    this.txtBLNO.Text.Trim(),
                                    this.txtCtnNO.Text.Trim(),
                                    this.txtCustomer.Text.Trim(),
                                    this.txtAgent.Text.Trim(),
                                    this.txtConsignee.Text.Trim(),
                                    this.txtShipper.Text.Trim(),
                                    this.txtNotifier.Text.Trim(),
                                    this.txtLoadingPort.Text.Trim(),
                                    this.txtDischargePort.Text.Trim(),
                                    this.txtPlaceOfDelivery.Text.Trim(),
                                    this.txtVesselName.Text.Trim(),
                                    this.txtVoyageNo.Text.Trim(),
                                    customerServiceID,
                                    filerID,
                                    salesID,
                                    orderState,
                                    this.lwchkIsValid.Checked,
                                    int.Parse(this.numMax.Value.ToString()),
                                    dateSearchType,
                                    dtFrom,
                                    dtTo);

            return list;

            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                return null;
            }


        }
        #endregion

        private void pnlMain_SizeChanged(object sender, EventArgs e)
        {
            this.bcMain.Width = this.pnlMain.Width - 17;
        }

        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindFiler();
            this.BindCustomerService();
        }
    }

}