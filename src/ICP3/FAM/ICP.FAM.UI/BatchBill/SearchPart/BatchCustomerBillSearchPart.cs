using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Server;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace ICP.FAM.UI.BatchBill
{
    /// <summary>
    /// 批量账单搜索界面
    /// </summary>
    [ToolboxItem(false)]
    [SmartPart]
    public partial class BatchCustomerBillSearchPart : BaseSearchPart
    {
        #region Service
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        /// <summary>
        /// 财务服务
        /// </summary>
        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }
        #endregion

        #region Property
        #region 已选择口岸ID集合
        /// <summary>
        /// 已选择口岸ID集合
        /// </summary>
        List<Guid> _CompanyIDs
        {
            get
            {
                return (from CheckedListBoxItem item in chkcmbCompany.Properties.Items 
                        where item.CheckState == CheckState.Checked 
                        select new Guid(item.Value.ToString())).ToList();
            }
        } 
        #endregion

        #region 已选择账单状态
        /// <summary>
        /// 已选择账单状态
        /// </summary>
        BillState? _BillStateValue
        {
            get
            {
                if (cmbState.EditValue != null) return (BillState)cmbState.EditValue;
                return null;
            }
        } 
        #endregion

        #region 已选择账单类型
        /// <summary>
        /// 已选择账单类型
        /// </summary>
        BillType? _BillTypeValue
        {
            get
            {
                if (cmbType.EditValue != null) return (BillType)cmbType.EditValue;
                return null;
            }
        } 
        #endregion

        #region 开始时间
        /// <summary>
        /// 开始时间
        /// </summary>
        DateTime? _From
        {
            get
            {
                return fromToDateMonthControl1.From;
            }
        } 
        #endregion

        #region 结束时间
        /// <summary>
        /// 结束时间
        /// </summary>
        DateTime? _To
        {
            get
            {
                return fromToDateMonthControl1.To;
            }
        } 
        #endregion
        #endregion

        #region Delegate
        public override event SearchResultHandler OnSearched;
        #endregion

        #region Init
        /// <summary>
        /// 批量账单搜索界面
        /// </summary>
        public BatchCustomerBillSearchPart()
        {
            InitializeComponent();
            RegisterEvent();
            Disposed += (sender, arg) =>
            {
                UnRegisterEvent();
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;

                }
            };
        }
        /// <summary>
        /// Load
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!LocalData.IsDesignMode)
            {
                InitControls();
            }
        }
        #endregion

        #region Controls Event
        /// <summary>
        /// 控件按键事件
        /// </summary>
        private void OnControlKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                case Keys.Enter:
                    btnSearch.PerformClick();
                    break;
                case Keys.F3:
                    btnClear.PerformClick();
                    break;
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        void btnSearch_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (OnSearched != null)
                    OnSearched(this, GetData());
            }
        }
        /// <summary>
        /// 清空查询
        /// </summary>
        void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control item in panelBase.Controls)
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
            }
        }

        #endregion

        #region Custom Method
        /// <summary>
        /// 注册事件
        /// </summary>
        void RegisterEvent()
        {
            foreach (Control item in panelBase.Controls)
            {
                item.KeyDown += OnControlKeyDown;
            }
            btnSearch.Click += btnSearch_Click;
            btnClear.Click += btnClear_Click;
            //foreach (Control item in panelBase.Controls)
            //{
            //    if (item is DevExpress.XtraEditors.TextEdit
            //    && (item is DevExpress.XtraEditors.SpinEdit) == false)
            //    {
            //        item.KeyDown += OnControlKeyDown;

            //    }
            //}
        }
        

        
        /// <summary>
        /// 移除事件
        /// </summary>
        void UnRegisterEvent()
        {
            foreach (Control item in panelBase.Controls)
            {
                item.KeyDown -= OnControlKeyDown;
            }
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            #region Company
            List<LocalOrganizationInfo> userCompanyList = LocalData.UserInfo.UserOrganizationList.FindAll(item => item.Type == LocalOrganizationType.Company);
            chkcmbCompany.Properties.BeginUpdate();
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
            #endregion

            #region State
            cmbState.Properties.BeginUpdate();
            List<EnumHelper.ListItem<BillState>> billStates = EnumHelper.GetEnumValues<BillState>(LocalData.IsEnglish);
            cmbState.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "ALL" : "全部", null));
            foreach (var item in billStates.Where(item => item.Value != BillState.None))
            {
                cmbState.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbState.Properties.EndUpdate();
            cmbState.SelectedIndex = 0;
            #endregion

            #region Type
            cmbType.Properties.BeginUpdate();
            List<EnumHelper.ListItem<BillType>> billTypes = EnumHelper.GetEnumValues<BillType>(LocalData.IsEnglish);
            cmbType.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "ALL" : "全部", null));
            foreach (var item in billTypes.Where(item => item.Value != BillType.None))
            {
                cmbType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value, (short)item.Value));
            }
            cmbType.Properties.EndUpdate();
            cmbType.SelectedIndex = 0;
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public override object GetData()
        {
            if (_CompanyIDs.Count == 0)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ? "At least one company must be selected." : "必须至少选择一个操作口岸.");

                return null;
            }
            try
            {
                List<BillList> list = FinanceService.GetBillList(_CompanyIDs.ToArray()
                    ,txtNo.Text.Trim()
                    , stxtCustomer.Text.Trim()
                    , _BillStateValue
                    , _BillTypeValue
                    ,lwchkIsValid.Checked
                    , _From
                    , _To
                    , int.Parse(numMax.Value.ToString(CultureInfo.InvariantCulture)));
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "total search " + list.Count + " data." : "总共查询到 " + list.Count + " 条数据.");
                return list;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); return null; }
        }
        #endregion
    }
}
