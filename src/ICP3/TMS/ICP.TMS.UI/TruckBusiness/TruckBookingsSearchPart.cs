using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.TMS.ServiceInterface;

namespace ICP.TMS.UI
{
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    [ToolboxItem(false)]
    public partial class TruckBookingsSearchPart : BaseSearchPart
    {
        public TruckBookingsSearchPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.OnSearched = null;
                this.cmbType.OnFirstEnter -= this.OnTypeEnter;
                this.cmbState.OnFirstEnter -= this.OnStateEnter;
                this.cmbDateType.OnFirstEnter -= this.OnDateTypeEnter;
                foreach (Control item in bgcBase.Controls)
                {
                    item.KeyDown -=item_KeyDown;
                }
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
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

        #region 私有变量
        /// <summary>
        /// 公司ID集合
        /// </summary>
        public List<Guid> CompanyIDs
        {
            get
            {
                List<Guid> companyIDs = new List<Guid>();
                foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in chkcmbCompany.Properties.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                        companyIDs.Add(new Guid(item.Value.ToString()));
                }
                return companyIDs;
            }
        }
        #endregion

        #region 属性&事件
        /// <summary>
        /// 查询
        /// </summary>
        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;


        #endregion

        #region 初始化
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
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            dateMonthControl1.IsEngish = LocalData.IsEnglish;

            //绑定公司
            Utility.BindCheckComboBoxByCompany(this.chkcmbCompany);

            //类型
            cmbType.ShowSelectedValue(0, LocalData.IsEnglish ? "ALL" : "全部");
            this.cmbType.OnFirstEnter += OnTypeEnter;
            


            //绑定状态
            cmbState.ShowSelectedValue(0, LocalData.IsEnglish ? "ALL" : "全部");

            this.cmbState.OnFirstEnter += this.OnStateEnter;


            //绑定时间查询类型
            cmbDateType.ShowSelectedValue(0,LocalData.IsEnglish ? "ALL" : "全部");
            this.cmbDateType.OnFirstEnter += this.OnDateTypeEnter;

        }

        private void OnDateTypeEnter(object sender,EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<TruckBusinessDateSeachType>> dateSeachType = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<TruckBusinessDateSeachType>(LocalData.IsEnglish);
            cmbDateType.Properties.BeginUpdate();
            cmbDateType.Properties.Items.Clear();
            foreach (var item in dateSeachType)
            {
                cmbDateType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbDateType.Properties.EndUpdate();
        }

        private void OnStateEnter(object sender, EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<SearchTruckBusinessState>> truckState = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<SearchTruckBusinessState>(LocalData.IsEnglish);
            cmbState.Properties.BeginUpdate();
            cmbState.Properties.Items.Clear();
            foreach (var item in truckState)
            {
                cmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            this.cmbState.SelectedIndex = 0;
            cmbState.Properties.EndUpdate();
        }
        private void OnTypeEnter(object sender, EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<SearchTruckBookingType>> businessType = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<SearchTruckBookingType>(LocalData.IsEnglish);
            cmbType.Properties.BeginUpdate();
            cmbType.Properties.Items.Clear();
            foreach (var item in businessType)
            {
                cmbType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            this.cmbType.SelectedIndex = 0;
            cmbType.Properties.EndUpdate();
        }
        /// <summary>
        /// 注册控件事件
        /// </summary>
        private void SetKeyDownToSearch()
        {
            foreach (Control item in bgcBase.Controls)
            {
                item.KeyDown += item_KeyDown;
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
        #endregion

        #region 重写
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="values"></param>
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null)
            {
                return;
            }

            this.txtNo.Text = string.Empty;
            this.txtContainerNo.Text = string.Empty;
            this.txtCustomerRefNo.Text = string.Empty;
            this.txtCustomerName.Text = string.Empty;



            foreach (var item in values)
            {
                if (item.Key == null)
                {
                    continue;
                }
                string value = item.Value == null ? string.Empty : item.Value.ToString();
                switch (item.Key.ToUpper())
                {
                    case "NO":
                        this.txtNo.Text = value;
                        break;
                    case "CONTAINERNO":
                        this.txtContainerNo.Text = value;
                        break;
                    case "CUSTOMERREFNO":
                        this.txtCustomerRefNo.Text = value;
                        break;
                    case "CUSTOMERNAME":
                        this.txtCustomerName.Text = value;
                        break;
                }
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <returns></returns>
        public override object GetData()
        {
            try
            {
                List<TruckBookingsList> list = TruckBookingService.GetTruckBookingsList(
                                   CompanyIDs.ToArray(),
                                   this.txtNo.Text,
                                   this.txtContainerNo.Text,
                                   this.txtMBLNo.Text,
                                   this.txtCustomerRefNo.Text,
                                   this.txtCustomerName.Text,
                                   (SearchTruckBookingType)this.cmbType.SelectedIndex,
                                   this.cmbState.SelectedIndex ,
                                   this.cbValid.Checked,
                                   Int32.Parse(this.numMaxCount.Value.ToString()),
                                   (TruckBusinessDateSeachType)this.cmbDateType.SelectedIndex,
                                   this.dateMonthControl1.From,
                                   this.dateMonthControl1.To,
                                   LocalData.IsEnglish);


                return list;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                return null;
            }

        }
        #endregion

        #region 清空与查询
        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.cmbDateType.SelectedIndex = 0;
            this.cmbState.SelectedIndex = 0;
            this.cmbType.SelectedIndex = 0;

            this.txtContainerNo.Text = string.Empty;
            this.txtCustomerName.Text = string.Empty;
            this.txtCustomerRefNo.Text = string.Empty;
            this.txtMBLNo.Text = string.Empty;
            this.txtNo.Text = string.Empty;

            this.numMaxCount.Value = 100;

            this.chkcmbCompany.SelectAll();
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (OnSearched != null)
            {
                using (new CursorHelper())
                {
                    OnSearched(this, GetData());
                }
            }
        }

   
        #endregion

       

    }
}
