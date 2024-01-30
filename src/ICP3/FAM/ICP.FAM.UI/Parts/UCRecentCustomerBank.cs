using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.UI.Controls;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Common;
using ICP.FAM.ServiceInterface.DataObjects;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.ClientComponents;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FAM.UI.Parts
{
    /// <summary>
    /// 最近客户银行
    /// </summary>
    public partial class UCRecentCustomerBank : UserControl
    {
        #region Services & Fields & Property
        #region Services
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem RootWorkItem { get; set; }

        #endregion

        #region Feilds
        /// <summary>
        /// 是否首次获得焦点
        /// </summary>
        private bool isFirstTimeEnter = true;

        /// <summary>
        /// 客户银行显示控件
        /// </summary>
        private CustomerBankPopupContainerControl popupContainerControl;
        #endregion

        #region Property

        #region 客户ID
        private Guid _customerid;
        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid CustomerID
        {
            get { return _customerid; }
            set
            {
                if (_customerid != value)
                {
                    _customerid = value;
                    popupContainerControl.CustomerID = value;
                }
            }
        }
        #endregion

        #region 客户名称

        private string _customername;

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName
        {
            get {
                _customername = popupContainerEdit.Text;
                return _customername; 
            }
            set
            {
                if (_customername!=value)
                {
                    _customername = value;
                    popupContainerEdit.Text = _customername;
                }
            }
        }
        #endregion

        #endregion

        #endregion

        #region Delegate
        /// <summary>
        /// 首次获得焦点事件
        /// </summary>
        public EventHandler FirstTimeEnter;
        /// <summary>
        /// 选择行
        /// </summary>
        public event EventHandler<CommonEventArgs<CustomerBankInfo>> SelectChanged;
        #endregion

        #region Constructor
        /// <summary>
        /// 最近客户银行
        /// </summary>
        public UCRecentCustomerBank()
        {
            InitializeComponent();
            InitPopupControl();
            Disposed += (sender, arg) =>
            {
                UnRegisterEvent();
                if (popupContainerControl != null)
                {
                    popupContainerControl.SelectChanged -= popupContainerControl_SelectChanged;
                    popupContainerControl.Dispose();
                }
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            RegisterEvent();
            InitControls();
        }
        #endregion

        #region Control Event
        /// <summary>
        /// 禁止修改高度
        /// </summary>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Height = 21;
        }
        /// <summary>
        /// 获得焦点事件
        /// </summary>
        protected override void OnEnter(EventArgs e)
        {
            if (FirstTimeEnter != null && isFirstTimeEnter)
            {
                FirstTimeEnter(this, e);
                isFirstTimeEnter = false;
            }
            base.OnEnter(e);
        }
        
        /// <summary>
        /// 选择报价
        /// </summary>
        void popupContainerControl_SelectChanged(object sender, CommonEventArgs<CustomerBankInfo> e)
        {
            if (SelectChanged != null)
            {
                SelectChanged(sender, e);
            }
        }

        /// <summary>
        ///  弹出最近十条使用报价单
        /// </summary>
        void popupContainerEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (CustomerID == Guid.Empty)
            {
                MessageBoxService.ShowWarning(LocalData.IsEnglish ? "Customer Must input." : "客户未选择"
                    , LocalData.IsEnglish ? "Tip" : "提示");
                return;
            }
            popupContainerControl.CustomerID = CustomerID;
            popupContainerControl.AccountName = popupContainerEdit.Text;
            if (popupContainerEdit.Properties.PopupControl.Visible == false)
            {
                popupContainerEdit.ShowPopup();
            }
            else
            {
                popupContainerEdit.ClosePopup();
            }

        }

        void popupContainerEdit_QueryCloseUp(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }
        #endregion

        #region Custom Method

        private void InitControls()
        {
            SetLanguage();
        }

        /// <summary>
        /// 设置语言
        /// </summary>
        private void SetLanguage()
        {
            if (LocalData.IsEnglish) return;
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitPopupControl()
        {
            if (popupContainerControl == null)
            {
                popupContainerControl = new CustomerBankPopupContainerControl();
                popupContainerControl.IsEnglish = LocalData.IsEnglish;
                popupContainerControl.SelectChanged += popupContainerControl_SelectChanged;
                popupContainerEdit.Properties.PopupControl = popupContainerControl;

            }
        }
        /// <summary>
        /// 注册事件
        /// </summary>
        private void RegisterEvent()
        {
            popupContainerEdit.ButtonClick += popupContainerEdit_ButtonClick;
        }
        /// <summary>
        /// 移除事件
        /// </summary>
        private void UnRegisterEvent()
        {
            popupContainerEdit.ButtonClick -= popupContainerEdit_ButtonClick;
        }
        #endregion
    }
}
