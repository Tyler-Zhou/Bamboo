using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using System;
using System.ComponentModel;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.Business.Common.UI.QuotedPrice.Recent
{
    /// <summary>
    /// 最近使用报价单
    /// </summary>
    public partial class QuotedPriceOrderControl : XtraUserControl
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
        /// 最近报价信息
        /// </summary>
        private RecentQuotedPricePopupContainerControl recentQuotedPricePopupContainerControl; 
        #endregion

        #region Property

        #region 报价单ID
        private Guid _QuotedPriceID;
        /// <summary>
        /// 报价ID
        /// </summary>
        public Guid QuotedPriceID
        {
            get { return _QuotedPriceID; }
            set
            {
                if (!ArgumentHelper.GuidIsNullOrEmpty(value))
                    btnView.Enabled = true;
                if (_QuotedPriceID != value)
                {
                    _QuotedPriceID = value;
                    recentQuotedPricePopupContainerControl.QuotedPriceID = value;
                }
            }
        } 
        #endregion

        #region 报价单号

        private string _QuotedPriceNo;

        /// <summary>
        /// 报价单号
        /// </summary>
        public string QuotedPriceNo
        {
            get { return _QuotedPriceNo; }
            set
            {
                _QuotedPriceNo = value;
                popupContainerEdit.Text = _QuotedPriceNo;
            }
        }
        #endregion

        #region 揽货人ID

        /// <summary>
        /// 揽货人ID
        /// </summary>
        public Guid SalesID { get; set; }

        #endregion

        #region 客户ID

        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid CustomerID { get; set; }

        #endregion

        #region 客户名称
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }
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
        public event EventHandler<CommonEventArgs<QuotedPricePartInfo>> SelectChanged; 
        #endregion
        #endregion

        #region Init
        /// <summary>
        /// 最近使用报价单
        /// </summary>
        public QuotedPriceOrderControl()
        {
            InitializeComponent();
            RegisterEvent();
            InitControls();
            InitPopupControl();
            Disposed += (sender,arg)=>
            {
                UnRegisterEvent();
                if (recentQuotedPricePopupContainerControl != null)
                {
                    recentQuotedPricePopupContainerControl.SelectChanged -= recentQuotedPricePopupContainerControl_SelectChanged;
                    recentQuotedPricePopupContainerControl.Dispose();
                }
            };
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
        /// 预览报价
        /// </summary>
        void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                EditPartShowCriteria ShowCriteria = new EditPartShowCriteria
                {

                    OperationID = QuotedPriceID,
                    OperationNo = QuotedPriceNo,
                    BillNo = QuotedPriceID

                };
                string title = ((LocalData.IsEnglish) ? "View QP Order: " : "查看报价单: ") + CommonUIUtility.GetSerialNumber(ShowCriteria.OperationNo);
                PartLoader.ShowEditPart<QuotedPriceEditPart>(RootWorkItem, ShowCriteria, EditMode.Edit, null, title, null, ShowCriteria.BillNo.ToString());
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }
        }
        /// <summary>
        /// 选择报价
        /// </summary>
        void recentQuotedPricePopupContainerControl_SelectChanged(object sender, CommonEventArgs<QuotedPricePartInfo> e)
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
            if (CustomerID == Guid.Empty || SalesID == Guid.Empty)
            {
                MessageBoxService.ShowWarning(LocalData.IsEnglish ? "Customer Or Sales Must input." : "客户或揽货人未选择"
                    , LocalData.IsEnglish ? "Tip" : "提示");
                return;
            }
            recentQuotedPricePopupContainerControl.SalesID = SalesID;
            recentQuotedPricePopupContainerControl.CustomerID = CustomerID;

            if (e.Button.Kind != ButtonPredefines.Combo)
            {
                string title = LocalData.IsEnglish ? "Select Quoted Price" : "选择报价";
                SelectQuotedPrice selectDataForm = RootWorkItem.Items.AddNew<SelectQuotedPrice>();
                selectDataForm.CustomerID = CustomerID;
                selectDataForm.QuoteBy = SalesID;
                if (DialogResult.OK !=
                        PartLoader.ShowDialog(selectDataForm, title, FormBorderStyle.FixedSingle, FormWindowState.Normal, true,
                            false)) return;
                QuotedPricePartInfo selectData = selectDataForm.SelectedPrice;
                if (selectData != null)
                {
                    QuotedPriceNo = selectData.QuotedPriceNo;
                    QuotedPriceID = selectData.QuotedPriceID.Value;
                }
            }
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
            btnView.Text = "查看";
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitPopupControl()
        {
            if (recentQuotedPricePopupContainerControl == null)
            {
                recentQuotedPricePopupContainerControl = new RecentQuotedPricePopupContainerControl();
                recentQuotedPricePopupContainerControl.SelectChanged += recentQuotedPricePopupContainerControl_SelectChanged;
                popupContainerEdit.Properties.PopupControl = recentQuotedPricePopupContainerControl;

            }
        }
        /// <summary>
        /// 注册事件
        /// </summary>
        private void RegisterEvent()
        {
            btnView.Click += btnView_Click;
            popupContainerEdit.ButtonClick += popupContainerEdit_ButtonClick;
        }
        /// <summary>
        /// 移除事件
        /// </summary>
        private void UnRegisterEvent()
        {
            btnView.Click -= btnView_Click;
            popupContainerEdit.ButtonClick -= popupContainerEdit_ButtonClick;
        }
        #endregion
    }
}
