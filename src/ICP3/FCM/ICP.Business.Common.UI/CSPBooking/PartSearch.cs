using DevExpress.XtraEditors;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.CompositeObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ICP.Business.Common.UI.CSPBooking
{
    /// <summary>
    /// 查询面板
    /// </summary>
    [ToolboxItem(false)]
    [SmartPart]
    public partial class PartSearch : BaseSearchPart
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem
        {
            get;
            set;
        }
        /// <summary>
        /// FCM公用服务
        /// </summary>
        IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }
        #endregion

        #region init
        /// <summary>
        /// 构造函数
        /// </summary>
        public PartSearch()
        {
            InitializeComponent();
            if (LocalData.IsDesignMode) return;

            Disposed += delegate {
                OnSearched = null;
                RemoveKeyDownHandle();
                RemoveControlEnterHandle();
                btnClear.Click -= btnClear_Click;
                btnSearch.Click -= btnSearch_Click;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;

                }
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!LocalData.IsDesignMode)
            {
                SetKeyDownToSearch();
                InitControls();
            }
        }
        
        private void SetKeyDownToSearch()
        {
            foreach (Control item in panel4.Controls)
            {
                item.KeyDown += item_KeyDown;
            }
        }
        private void RemoveKeyDownHandle()
        {
            foreach (Control item in panel4.Controls)
            {
                item.KeyDown -= item_KeyDown;
            }
        }

        void item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2) btnSearch.PerformClick();
            else if (e.KeyCode == Keys.F3) btnClear.PerformClick();
        }
        
        private void InitControls()
        {
            SetControlsEnterToSearch();
        }
        private void RemoveControlEnterHandle()
        {
            foreach (Control item in panel4.Controls)
            {
                if (item is TextEdit
                && (item is SpinEdit) == false)
                {
                    item.KeyDown -= OnControlKeyDown;

                }
            }
        }
       
        /// <summary>
        /// 设置一些TextBox输入Enter键时,触发查询事件
        /// </summary>
        private void SetControlsEnterToSearch()
        {
            foreach (Control item in panel4.Controls)
            {
                if (item is TextEdit
                && (item is SpinEdit) == false)
                {
                    item.KeyDown += OnControlKeyDown;
                }
            }
        }
        private void OnControlKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnSearch.PerformClick();
        }
        #endregion

        #region ISearchPart 成员

        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        public override void Init(IDictionary<string, object> values)
        {
            if (values == null)
            {
                return;
            }


            foreach (var item in values)
            {
                string value = item.Value == null ? string.Empty : item.Value.ToString();
                switch (item.Key)
                {
                    case "No":
                        break;
                    case "MBLNo":
                        break;
                }
            }
        }

        public override object GetData()
        {
            SearchParameterBookingDelegate searchParameter = new SearchParameterBookingDelegate()
            {
                BookingStatus = CSP_BOOKING_STATUS.Unknown,
                FreightMethodType = CSP_FREIGHTMETHODTYPE.Unknown,
                ShipmentType = CSP_SHIPMENTTYPE.Unknown,
                MaxResultCount = 1000,
            };
            List<BookingDelegateList> list = FCMCommonService.GetBookingDelegateListBySearch(searchParameter);
            return list;
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
            foreach (Control item in panel4.Controls)
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
    }
}
