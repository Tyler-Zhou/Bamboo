using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Common.UI;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;

namespace ICP.FCM.Common.UI.Finder
{
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class BusinessSearchPart : ICP.Framework.ClientComponents.UIFramework.BaseSearchPart
    {

        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public ICP.FCM.Common.ServiceInterface.IFCMCommonService fcmCommonService { get; set; }

        [ServiceDependency]
        public ICP.Sys.ServiceInterface.IUserService userService { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ICPCommUIHelper ICPCommUIHelper { get; set; }

        #endregion

        #region 属性

        /// <summary>
        /// 外部传入，为null时代表全部，否则业务类型下拉限制为传入类型组
        /// </summary>
        private List<OperationType> _operationTypes = null;

        #endregion
        ///// <summary>
        ///// 如果是为搜索器准备的界面，订舱不要默认设置成自己
        ///// 添加时间：2011-07-20 10:10
        ///// </summary>
        //public bool IsLoadFromFinder { get; set; }

        #region init

        public BusinessSearchPart()
        {
            InitializeComponent();
            if (DesignMode) return;
            //if (LocalData.IsEnglish == false) SetCnText();

            this.Disposed += delegate { if (Workitem != null) Workitem.Items.Remove(this); };

            this.Load += new EventHandler(BookingSearchPart_Load);
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
                item.KeyDown += new KeyEventHandler(item_KeyDown);
            }
        }

        void item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2) this.btnSearch.PerformClick();
            else if (e.KeyCode == Keys.F3) this.btnClear.PerformClick();
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

            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<DateSearchTypeForDataFinder>> dateSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<DateSearchTypeForDataFinder>(LocalData.IsEnglish);
            cmbDateSearchType.Properties.BeginUpdate();

            foreach (var item in dateSearchTypes)
            {
                if (item.Value == DateSearchTypeForDataFinder.All) continue;
                cmbDateSearchType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }

            cmbDateSearchType.Properties.EndUpdate();
            cmbDateSearchType.SelectedIndex = 0;

            #endregion

          

            #region 用户和部门

            List<OrganizationList> userCompanyList = SetCompany();

            //	当前用户所在的操作口岸的海运订舱单中所有揽货人	数据从用户表[sm..Users]检索
            //Utility.SetEnterToExecuteOnec(mcmbSales, delegate
            //{
               
                List<UserList> userList = userService.GetUnderlingUserList(null, new string[] { "海外拓展", "销售代表", "拓展员", "总裁", "副总裁", "总经理助理", "分公司总经理" }, null, true);
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
                List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OperationType>> types = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OperationType>(LocalData.IsEnglish);
                foreach (var item in types)
                {
                    if (item.Value == OperationType.Unknown)
                    {
                        continue;
                    }

                    cmbOperationType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item));
                }
            }
            else
            {
                if (_operationTypes.Count > 1)
                {
                    cmbOperationType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "All" : "全部", null));  //不是所有全部
                }

                foreach (var item in _operationTypes)
                {
                    string description = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<OperationType>(item, LocalData.IsEnglish);
                    cmbOperationType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(description, item));
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
            //if (SearchOrderStates != null)
            //{
            //    foreach (var item in SearchOrderStates)
            //    {
            //        string description = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<OEOrderState>(item.Value, LocalData.IsEnglish);
            //        cmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(description, item.Value));
            //    }
            //}
            //else
            //{
                cmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "ALL" : "全部", null));
                List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OEOrderState>> orderStates = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OEOrderState>(LocalData.IsEnglish);

                foreach (var item in orderStates)
                {
                    if (item.Value == OEOrderState.Unknown || item.Value == OEOrderState.Rejected) continue;
                    cmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
                }
            //}

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

        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

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

            //this.ClearControls();
            this.txtOperationNo.Text = string.Empty;
            this.txtBLNO.Text = string.Empty;
            this.txtOrderNO.Text = string.Empty;
            this.stxtCustomer.Text = string.Empty;
            this.stxtPOL.Text = string.Empty;
            this.stxtPOD.Text = string.Empty;
           
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
                    //case "HBLNo":
                    //    this.txtBLNO.Text = GetFirst(value);
                    //    break;             
                }
            }
        }

        //public override void InitialValues(string searchValue, string property
        //                              , ICP.Framework.CommonLibrary.Client.SearchConditionCollection conditions
        //                              , ICP.Framework.CommonLibrary.Client.FinderTriggerType triggerType)
        //{
        //    this.ClearControls();
        //    if (triggerType == FinderTriggerType.KeyEnter)
        //    {
        //        if (property.Contains("BusinessNo"))
        //            txtOperationNo.Text = searchValue;
        //        //else if (property.Contains(SearchFieldConstants.BLNo))
        //        //    txtBLNo.Text = searchValue;

        //    }

        //    if (conditions != null)
        //    {
        //        //cmbProgram.Enabled = false;

        //        //if (conditions.Contain("BLNO"))
        //        //{
        //        //    this.txtBLNo.Text = conditions.GetValue("BLNO").Value.ToString();
        //        //}
        //        //if (conditions.Contain("CustomerName"))
        //        //{
        //        //    this.txtCustomer.Text = conditions.GetValue("CustomerName").Value.ToString();
        //        //    if (!string.IsNullOrEmpty(this.txtCustomer.Text))
        //        //    {
        //        //        if (!conditions.GetValue("CustomerName").CanChange)
        //        //        {
        //        //            this.txtCustomer.Properties.ReadOnly = true;
        //        //        }
        //        //    }
        //        //}            
        //    }
        //}

        public override object GetData()
        {
            if (CompanyIDs.Count == 0)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), LocalData.IsEnglish ? "At least one company must be selected." : "必须至少选择一个操作口岸.");

                return null;
            }

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
                types = new List<OperationType>();
                types.Add((OperationType)cmbOperationType.EditValue);
            }
            
            try
            {
                List<BusinessData> list = fcmCommonService.GetBusinessListForDataFinder(types == null ? null : types.ToArray(),
                                                                                        CompanyIDs.ToArray(),
                                                                                        txtOperationNo.Text.Trim(),
                                                                                        this.txtBLNO.Text.Trim(),
                                                                                      //txtOrderNO.Text.Trim(),
                                                                                        stxtCustomer.Text.Trim(),
                                                                                        stxtPOL.Text.Trim(),
                                                                                        stxtPOD.Text.Trim(),
                                                                                        SalesID,
                                                                                        lwchkIsValid.Checked,
                                                                                        OrderStateValue,
                                                                                        DateSearchTypeValue,
                                                                                        From,
                                                                                        To,
                                                                                        int.Parse(numMax.Value.ToString()));

                return list;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); return null; }
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
                else if (item is DevExpress.XtraEditors.TextEdit
                         && (item is DevExpress.XtraEditors.SpinEdit) == false
                         && item.Enabled == true
                         && (item as DevExpress.XtraEditors.TextEdit).Properties.ReadOnly == false)
                {
                    item.Text = string.Empty;
                }

                cmbDateSearchType.SelectedIndex = 0;
            }
        }

        #endregion

        #region 公共成员

        public OEOrderState? OrderStateValue
        {
            get
            {
                if (cmbState.EditValue != null) return (OEOrderState)cmbState.EditValue;
                else return null;
            }
        }

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

        public DateTime? From
        {
            get
            {
                if (DateSearchTypeValue == DateSearchTypeForDataFinder.All) return null;
                else return fromToDateMonthControl1.From;
            }
        }

        public DateTime? To
        {
            get
            {
                if (DateSearchTypeValue == DateSearchTypeForDataFinder.All) return null;
                else return fromToDateMonthControl1.To;
            }
        }

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

    //public class BookingFinderSearchPart : BookingSearchPart
    //{
    //    protected override List<OEOrderState?> SearchOrderStates
    //    {
    //        get
    //        {
    //            List<OEOrderState?> states = new List<OEOrderState?>();
    //            states.Add(OEOrderState.BookingConfirmed);
    //            states.Add(OEOrderState.Checked);
    //            return states;
    //        }
    //    }

    //    public override void Init(IDictionary<string, object> values)
    //    {
    //        if (values == null) return;
    //        foreach (var item in values)
    //        {
    //            if (item.Key == SearchFieldConstants.BookingNO)
    //            {
    //                txtOperationNo.Text = item.Value == null ? string.Empty : item.Value.ToString();
    //            }
    //        }
    //    }
    //}
}
