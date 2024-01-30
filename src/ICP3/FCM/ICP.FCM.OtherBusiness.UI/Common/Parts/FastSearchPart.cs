using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using ICP.FCM.OtherBusiness.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary;

namespace ICP.FCM.OtherBusiness.UI
{
    /// <summary>
    /// 快速搜索面板
    /// </summary>
    [ToolboxItem(false)]
    [SmartPart]
    public partial class FastSearchPart : BaseSearchPart
    {
        #region Field & Property & Event

        /// <summary>
        /// 单号搜索项
        /// </summary>
        protected NoSearchType PartNoSearchType 
        { get { return (NoSearchType)Enum.Parse(typeof(NoSearchType), cmbNoSearchType.EditValue.ToString()); } }
        /// <summary>
        /// 客户搜索项
        /// </summary>
        protected CustomerSearchType PartCustomerSearchType
        { get { return (CustomerSearchType)Enum.Parse(typeof(CustomerSearchType), cmbCustomerSearchType.EditValue.ToString()); } }
        /// <summary>
        /// 地点查询项
        /// </summary>
        protected PortSearchType PartPortSearchType 
        { get { return (PortSearchType)Enum.Parse(typeof(PortSearchType), cmbPortSearchType.EditValue.ToString()); } }
        /// <summary>
        /// 日期搜索项
        /// </summary>
        protected DateSearchType PartDateSearchType 
        { get { return (DateSearchType)Enum.Parse(typeof(DateSearchType), cmbDateSearchType.EditValue.ToString()); } }
        List<Guid> companyIDs = null;
        /// <summary>
        /// 公司IDs,默认为当前用户所在公司( OrganizationType.Company)
        /// </summary>
        public virtual Guid[] CompanyIDs
        {
            get
            {
                if (companyIDs != null)
                {
                    return companyIDs.ToArray();
                }
                else
                {
                    companyIDs = new List<Guid>();

                    List<OrganizationList> userCompanyList = UserService.GetUserCompanyList(LocalData.UserInfo.LoginID, OrganizationType.Company);
                    if (userCompanyList.Count == 0)
                    {
                        throw new Exception(LocalData.IsEnglish ? "You have no rights to query data of any company. Please contat administrator." : "您没有权限查询任何操作口岸的数据，请联系管理员！");
                    }
                    foreach (var item in userCompanyList)
                    {
                        companyIDs.Add(item.ID);
                    }
                    return companyIDs.ToArray();
                }
            }
        }
        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime? From
        {
            get
            {
                switch (cmbDateType.SelectedIndex)
                {
                    case 0:
                        return null;
                    case 1:
                        return DateTime.Now.Date.AddDays(-7);
                    case 2:
                        return DateTime.Now.Date.AddDays(-30);
                    case 3:
                        return DateTime.Now.Date.AddDays(-365);
                };
                return null;
            }
        }
        /// <summary>
        /// 最终时间
        /// </summary>
        public DateTime? To
        {
            get
            {
                if (cmbDateType.SelectedIndex == 0)
                    return null;
                else
                    return DateTime.Now.DateAttachEndTime();
            }
        }
        /// <summary>
        /// 查询事件处理器
        /// </summary>
        public override event SearchResultHandler OnSearched;
        #endregion

        #region service
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 其他业务
        /// </summary>
        public IOtherBusinessService OtherBusinessService
        {
            get
            {
                return ServiceClient.GetService<IOtherBusinessService>();
            }
        }
        /// <summary>
        /// 用户服务
        /// </summary>
        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        #endregion 

        #region init

        /// <summary>
        /// 快速搜索面板
        /// </summary>
        public FastSearchPart()
        {
            InitializeComponent();

            if (DesignMode == false)
            {
                Load += FastSearchPart_Load;
                if (LocalData.IsEnglish == false)
                {
                    SetCnText();
                }
                InitControls();
            }
            Disposed += delegate
            {
                OnSearched = null;
                companyIDs = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            
            };
        }

        /// <summary>
        /// 初始化控件值
        /// </summary>
        private void InitControls()
        {
            List<EnumHelper.ListItem<NoSearchType>> noSearchTypes = EnumHelper.GetEnumValues<NoSearchType>(LocalData.IsEnglish);
            foreach (var item in noSearchTypes)
            {
                cmbNoSearchType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }

            List<EnumHelper.ListItem<CustomerSearchType>> customerSearchTypes = EnumHelper.GetEnumValues<CustomerSearchType>(LocalData.IsEnglish);
            foreach (var item in customerSearchTypes)
            {
                cmbCustomerSearchType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }

            List<EnumHelper.ListItem<PortSearchType>> portSearchTypes = EnumHelper.GetEnumValues<PortSearchType>(LocalData.IsEnglish);
            foreach (var item in portSearchTypes)
            {
                cmbPortSearchType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }

            List<EnumHelper.ListItem<DateSearchType>> dateSearchTypes = EnumHelper.GetEnumValues<DateSearchType>(LocalData.IsEnglish);
            foreach (var item in dateSearchTypes)
            {
                cmbDateSearchType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }

            cmbNoSearchType.SelectedIndex = cmbCustomerSearchType.SelectedIndex = cmbPortSearchType.SelectedIndex = cmbDateSearchType.SelectedIndex = 0;

            cmbDateType.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "UnKnow" : "不确定", 0));
            cmbDateType.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "Last Week" : "一周内", 1));
            cmbDateType.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "This Month" : "一月内", 2));
            cmbDateType.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "Last Year" : "一年内", 3));
            cmbDateType.SelectedIndex = 0;
        }

        #endregion

        #region Event
        /// <summary>
        /// 面板加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FastSearchPart_Load(object sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }
            SetControlsEnterToSearch();
        }

        /// <summary>
        /// 注册回车、F2按键查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2) btnSearch.PerformClick();
        }

        /// <summary>
        /// 查询事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (OnSearched != null)
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    OnSearched(this, GetData());
                }
            }
        }

        /// <summary>
        /// 清空查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtNo.Text = stxtCustomer.Text = stxtPort.Text = string.Empty;
            cmbCustomerSearchType.SelectedIndex = cmbDateSearchType.SelectedIndex = cmbDateType.SelectedIndex = cmbNoSearchType.SelectedIndex = 0;
        }
        #endregion

        #region ISearchPart 成员
        
        /// <summary>
        ///查询
        /// </summary>
        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        #endregion

        #region Method
        /// <summary>
        /// 更多查找
        /// </summary>
        protected virtual void OnClickMore() { }

        private void SetCnText()
        {
            btnSearch.Text = "查询(&S)";
            llabMore.Text = "更多";
        }

        private void SetControlsEnterToSearch()
        {
            txtNo.KeyDown += TextBox_KeyDown;
            stxtCustomer.KeyDown += TextBox_KeyDown;
            stxtPort.KeyDown += TextBox_KeyDown;
            KeyDown += TextBox_KeyDown;

            llabMore.Click += delegate { OnClickMore(); };
        }
        #endregion

        #region btn
        
        #endregion
    }
}
