using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.CompositeObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Helper;
using DevExpress.XtraEditors.Controls;

namespace ICP.Business.Common.UI.CSPBooking
{    
    /// <summary>
    /// 快速搜索界面基类
    /// </summary>
    [ToolboxItem(false)]
    [SmartPart]
    public partial class PartFastSearch : BaseSearchPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }
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

        #region 属性
        #endregion

        #region init

        public PartFastSearch()
        {
            InitializeComponent();
            this.Disposed += (sender, e) => {

                this.OnSearched = null;
                this.KeyDown -=TextBox_KeyDown;
                if (this.Workitem != null)
                {
                    this.Workitem.Items.Remove(this);
                    this.Workitem = null;
                }
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!LocalData.IsDesignMode)
            {
                InitControls();
                SetControlsEnterToSearch();
            }
        }

        private void SetControlsEnterToSearch()
        {
            llabMore.Click += delegate { Workitem.Commands[CSPBKConstants.COMMAND_SHOWSEARCH].Execute(); };
        }

        void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2 || e.KeyCode == Keys.F5) this.btnSearch.PerformClick();
        }

        private void InitControls()
        {
            cmbFreightMethodType.SetEnterToExecuteOnec(delegate
            {
                List<EnumHelper.ListItem<CSP_FREIGHTMETHODTYPE>> freightMethodTypes = EnumHelper.GetEnumValues<CSP_FREIGHTMETHODTYPE>(LocalData.IsEnglish);
                cmbFreightMethodType.Properties.BeginUpdate();
                cmbFreightMethodType.Properties.Items.Clear();
                foreach (var item in freightMethodTypes)
                {
                    cmbFreightMethodType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
                }
                cmbFreightMethodType.Properties.EndUpdate();
            });

            cmbShipmentType.SetEnterToExecuteOnec(delegate
            {
                List<EnumHelper.ListItem<CSP_SHIPMENTTYPE>> freightMethodTypes = EnumHelper.GetEnumValues<CSP_SHIPMENTTYPE>(LocalData.IsEnglish);
                cmbShipmentType.Properties.BeginUpdate();
                cmbShipmentType.Properties.Items.Clear();
                foreach (var item in freightMethodTypes)
                {
                    cmbShipmentType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
                }
                cmbShipmentType.Properties.EndUpdate();
            });
        }

        #endregion

        #region ISearchPart 成员

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

        public override event SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        #endregion

        #region btn

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (OnSearched != null)
            {
                using (new CursorHelper())
                {
                    try
                    {
                        OnSearched(this, GetData());
                    }
                    catch (Exception ex) 
                    { 
                        LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
                    }
                }
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
        }
        #endregion
    }
}
