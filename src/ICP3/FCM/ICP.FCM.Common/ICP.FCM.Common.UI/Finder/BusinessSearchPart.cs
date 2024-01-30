using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ICP.Common.UI;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.Common.UI.Finder
{
    /// <summary>
    /// 
    /// </summary>
    [ToolboxItem(false)]
    [SmartPart]
    public partial class BusinessSearchPart : BaseSearchPart
    {

        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IFCMCommonService fcmCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }


        public IUserService userService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
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

        /// <summary>
        /// 外部传入，为null时代表全部，否则业务类型下拉限制为传入类型组
        /// </summary>
        private List<OperationType> _operationTypes = null;
        /// <summary>
        /// 所选业务类型
        /// </summary>
        private List<OperationType> OperationTypes
        {
            get
            {
                List<OperationType> types = null;
                if (cmbOperationType.EditValue == null)   //当选了全部
                {
                    if (_operationTypes != null)
                    {
                        types = _operationTypes;
                    }
                }
                else
                {
                    types = new List<OperationType> { (OperationType)cmbOperationType.EditValue };
                }
                return types;
            }
        }
        #endregion

        #region init

        public BusinessSearchPart()
        {
            InitializeComponent();
            if (DesignMode) return;
            //if (LocalData.IsEnglish == false) SetCnText();

            Disposed += delegate {
                _operationTypes = null;
                OnSearched = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };

            Load += BookingSearchPart_Load;
        }

        void BookingSearchPart_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                SetKeyDownToSearch();
            }
        }
        private void SetKeyDownToSearch()
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                item.KeyDown += item_KeyDown;
            }
        }

        void item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2) btnSearch.PerformClick();
            else if (e.KeyCode == Keys.F3) btnClear.PerformClick();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
            }
        }

        private void InitControls()
        {
            #region enum

            SetState();

            List<EnumHelper.ListItem<DateSearchTypeForDataFinder>> dateSearchTypes = EnumHelper.GetEnumValues<DateSearchTypeForDataFinder>(LocalData.IsEnglish);
            cmbDateSearchType.Properties.BeginUpdate();

            foreach (var item in dateSearchTypes)
            {
                if (item.Value == DateSearchTypeForDataFinder.All) continue;
                cmbDateSearchType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }

            cmbDateSearchType.Properties.EndUpdate();
            cmbDateSearchType.SelectedIndex = 0;

            #endregion

          

            #region 用户和部门

            List<OrganizationList> userCompanyList = SetCompany();

            //	当前用户所在的操作口岸的海运订舱单中所有揽货人	数据从用户表[sm..Users]检索
            //Utility.SetEnterToExecuteOnec(mcmbSales, delegate
            //{
               
                List<UserList> userList = userService.GetUnderlingUserList(null, new string[] { "海外拓展", "销售代表", "电商顾问", "拓展员", "总裁", "副总裁", "总经理助理", "分公司总经理" }, null, true);
                UserList insertItem = new UserList();
                insertItem.ID = Guid.Empty;
                insertItem.EName = insertItem.CName = string.Empty;
                userList.Insert(0, insertItem);

                Dictionary<string, string> col = new Dictionary<string, string>();
                col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
                col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");

                mcmbSales.InitSource<UserList>(userList, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
            //});

            #endregion

            SetControlsEnterToSearch();
        }

        private void SetOperationTypes()
        {
            cmbOperationType.Properties.BeginUpdate();
            if (_operationTypes == null || _operationTypes.Count == 0)
            {
                List<EnumHelper.ListItem<OperationType>> types = EnumHelper.GetEnumValues<OperationType>(LocalData.IsEnglish);
                foreach (var item in types)
                {
                    if (item.Value == OperationType.Unknown)
                    {
                        continue;
                    }

                    cmbOperationType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item));
                }
            }
            else
            {
                if (_operationTypes.Count > 1)
                {
                    cmbOperationType.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "All" : "全部", null));  //不是所有全部
                }

                foreach (var item in _operationTypes)
                {
                    string description = EnumHelper.GetDescription(item, LocalData.IsEnglish);
                    cmbOperationType.Properties.Items.Add(new ImageComboBoxItem(description, item));
                }
            }

            cmbOperationType.Properties.EndUpdate();
            cmbOperationType.SelectedIndex = 0;
        }

        /// <summary>
        /// 设置状态查询栏位的可查状态值
        /// </summary>
        protected virtual void SetState()
        {
            cmbState.Properties.BeginUpdate();

            cmbState.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "ALL" : "全部", null));
            List<EnumHelper.ListItem<OrderState>> orderStates = EnumHelper.GetEnumValues<OrderState>(LocalData.IsEnglish);

            foreach (var item in orderStates.Where(item => item.Value != OrderState.Unknown && item.Value != OrderState.Rejected))
            {
                cmbState.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbState.Properties.EndUpdate();
            cmbState.SelectedIndex = 0;
        }

        /// <summary>
        /// 设置一些TextBox输入Enter键时,触发查询事件
        /// </summary>
        private void SetControlsEnterToSearch()
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                if (item is TextEdit
                && (item is SpinEdit) == false)
                {
                    item.KeyDown += delegate(object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.Enter) btnSearch.PerformClick();
                    };
                }
            }
        }

        private List<OrganizationList> SetCompany()
        {
            List<OrganizationList> userCompanyList = userService.GetUserCompanyList(LocalData.UserInfo.LoginID, OrganizationType.Company);
            chkcmbCompany.Properties.BeginUpdate();
            chkcmbCompany.Properties.Items.Clear();
            foreach (var item in userCompanyList)
            {
                if (item.ID == LocalData.UserInfo.DefaultCompanyID)
                {
                    chkcmbCompany.Properties.Items.Add(item.ID, LocalData.IsEnglish ? item.EShortName : item.CShortName,
                                                   CheckState.Checked, true);
                }
                else
                {
                    chkcmbCompany.Properties.Items.Add(item.ID, LocalData.IsEnglish ? item.EShortName : item.CShortName,
                                                       CheckState.Unchecked, true);
                }
            }

            chkcmbCompany.Properties.EndUpdate();
            return userCompanyList;
        }

        #endregion

        #region ISearchPart 成员
        /// <summary>
        /// 
        /// </summary>
        public override event SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        string GetFirst(string value)
        {
            string[] nos = value.Split(',');
            if (nos.Length > 0)
            {
                return nos[0];
            }
            else
            {
                return value;
            }
        }

        public override void Init(IDictionary<string, object> values)
        {
            if (values == null)
            {
                return;
            }

            txtOperationNo.Text = string.Empty;
            txtBLNO.Text = string.Empty;
            txtOrderNO.Text = string.Empty;
            stxtCustomer.Text = string.Empty;
            stxtPOL.Text = string.Empty;
            stxtPOD.Text = string.Empty;
           
            foreach (var item in values)
            {
                switch (item.Key)
                {
                    case "BusinessNo":
                        txtOperationNo.Text =item.Value == null ? string.Empty : item.Value.ToString();
                        break;
                    case "OperationTypes":
                        _operationTypes = item.Value as List<OperationType>;
                        SetOperationTypes();
                        break;
                }
            }
        }

        public override object GetData()
        {
            if (CompanyIDs.Count == 0)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ? "At least one company must be selected." : "必须至少选择一个操作口岸.");

                return null;
            }

            
            
            try
            {
                List<BusinessData> list = fcmCommonService.GetBusinessListForDataFinder(OperationTypes == null ? null : OperationTypes.ToArray(),
                                                                                        CompanyIDs.ToArray(),
                                                                                        txtOperationNo.Text.Trim(),
                                                                                        txtBLNO.Text.Trim(),
                                                                                        stxtCustomer.Text.Trim(),
                                                                                        stxtPOL.Text.Trim(),
                                                                                        stxtPOD.Text.Trim(),
                                                                                        SalesID,
                                                                                        lwchkIsValid.Checked,
                                                                                        OrderStateValue,
                                                                                        DateSearchTypeValue,
                                                                                        From,
                                                                                        To,
                                                                                        int.Parse(numMax.Value.ToString(CultureInfo.InvariantCulture)));

                return list;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); return null; }
        }

        #endregion

        #region btn

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (OnSearched != null)
                    OnSearched(this, GetData());
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        private void ClearControls()
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                if (item is LWImageComboBoxEdit)
                {
                    (item as LWImageComboBoxEdit).SelectedIndex = 0;
                }
                else if (item is MultiSearchCommonBox)
                {
                    MultiSearchCommonBox msc = (MultiSearchCommonBox)item;
                    msc.ShowSelectedValue(null, string.Empty);
                }
                else if (item is TextEdit
                         && (item is SpinEdit) == false
                         && item.Enabled == true
                         && (item as TextEdit).Properties.ReadOnly == false)
                {
                    item.Text = string.Empty;
                }

                cmbDateSearchType.SelectedIndex = 0;
            }
        }

        #endregion

        #region 公共成员
        /// <summary>
        /// 
        /// </summary>
        public OrderState? OrderStateValue
        {
            get
            {
                if (cmbState.EditValue != null)
                    return (OrderState)cmbState.EditValue;
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateSearchTypeForDataFinder DateSearchTypeValue
        {
            get
            {
                if (nbarDate.Expanded && cmbDateSearchType.EditValue.ToString() != DateSearchTypeForDataFinder.All.ToString())
                {
                    return (DateSearchTypeForDataFinder)cmbDateSearchType.EditValue;
                }
                else
                    return DateSearchTypeForDataFinder.All;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? From
        {
            get
            {
                if (DateSearchTypeValue == DateSearchTypeForDataFinder.All) return null;
                else return fromToDateMonthControl1.From;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? To
        {
            get
            {
                if (DateSearchTypeValue == DateSearchTypeForDataFinder.All) return null;
                else return fromToDateMonthControl1.To;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public List<Guid> CompanyIDs
        {
            get
            {
                List<Guid> companyIDs = new List<Guid>();
                foreach (CheckedListBoxItem item in chkcmbCompany.Properties.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                        companyIDs.Add(new Guid(item.Value.ToString()));
                }
                return companyIDs;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid? SalesID
        {
            get
            {
                if (mcmbSales.EditValue == null) return null;
                return new Guid(mcmbSales.EditValue.ToString());
            }
        }

        #endregion
    }
}
