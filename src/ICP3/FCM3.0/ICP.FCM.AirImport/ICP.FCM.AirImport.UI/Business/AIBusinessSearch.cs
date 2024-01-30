using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Microsoft.Practices.CompositeUI;

using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.ObjectBuilder;
using ICP.FCM.AirImport.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;
using ICP.Framework.ClientComponents.Controls;

namespace ICP.FCM.AirImport.UI
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
        public IAirImportService oIService { get; set; }


        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ICPCommUIHelper ICPCommUIHelperService { get; set; }

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

        /// <summary>
        /// 加载控件
        /// </summary>
        private void InitControls()
        {
            //注册延迟加载
            SetLazyLoaders();
            SetLazyDataLodersWithDynamicCondition();

            labIsValid.Text = "有效性";
            lwchkIsValid.CheckedText = "有效";
            lwchkIsValid.NULLText = "全部";
            lwchkIsValid.UnCheckedText = "无效";

       

            //客服
            Utility.SetEnterToExecuteOnec(this.cmbCustomerService, delegate
            {
                List<UserList> userList = Utility.GetUserList(userService);
                UserList user = new UserList();
                user.ID = Guid.Empty;
                user.EName = user.CName = string.Empty;

                userList.Insert(0, user);

                Dictionary<string, string> col = new Dictionary<string, string>();
                col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
                col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");
                cmbCustomerService.InitSource<UserList>(userList, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
            });

            //文件
            Utility.SetEnterToExecuteOnec(this.cmbFiler, delegate
            {
                List<UserList> userList = Utility.GetUserList(userService);
                UserList user = new UserList();
                user.ID = Guid.Empty;
                user.EName = user.CName = string.Empty;

                userList.Insert(0, user);

                Dictionary<string, string> col = new Dictionary<string, string>();
                col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
                col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");
                cmbFiler.InitSource<UserList>(userList, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
            });


            //状态
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<AIOrderState>> orderStates = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<AIOrderState>(LocalData.IsEnglish);
            cmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "ALL" : "全部", System.DBNull.Value));
            foreach (var item in orderStates)
            {
                if (item.Value == AIOrderState.Unknown) continue;
                cmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbState.SelectedIndex = 0;

            //时间
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OIBusinessDateSearchType>> dateSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OIBusinessDateSearchType>(LocalData.IsEnglish);
            foreach (var item in dateSearchTypes)
            {
                if (item.Value.ToString() == "All")
                {
                    continue;
                }
                cmbDate.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }

            this.cmbDate.SelectedIndex = 0;

            SetControlsEnterToSearch();
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
            this.txtBLNO.Text = string.Empty;
            this.txtShipper.Text = string.Empty;
            this.txtConsignee.Text = string.Empty;
            this.txtDeparture.Text = string.Empty;
            this.txtDetination.Text = string.Empty;
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
                        this.txtDeparture.Text = value;
                        break;
                    case "PODName":
                        this.txtDetination.Text = value;
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
                ICPCommUIHelperService.BindCompanyByUser(cmbCompany,true);
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
            if (cmbCompany.EditValue != null && !string.IsNullOrEmpty(cmbCompany.EditValue.ToString()))
            {
                depID = new Guid(cmbCompany.EditValue.ToString());
            }

            ICPCommUIHelperService.SetComboxUsers(cmbSales,depID,string.Empty,string.Empty,false);
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
            AIOrderState? orderState = null;
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
                orderState = (AIOrderState)cmbState.EditValue;
            }

            try
            {
            List<AirBusinessList> list=oIService.GetBusinessList(
                                    companyIDs,
                                    this.txtNo.Text.Trim(),
                                    this.txtBLNO.Text.Trim(),
                                    this.txtCustomer.Text.Trim(),
                                    this.txtAgent.Text.Trim(),
                                    this.txtConsignee.Text.Trim(),
                                    this.txtShipper.Text.Trim(),
                                    this.txtNotifier.Text.Trim(),
                                    txtDeparture.Text.Trim(),
                                    txtDetination.Text.Trim(),
                                    this.txtPlaceOfDelivery.Text.Trim(),
                                    this.txtFlightNo.Text.Trim(),
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
    }

}