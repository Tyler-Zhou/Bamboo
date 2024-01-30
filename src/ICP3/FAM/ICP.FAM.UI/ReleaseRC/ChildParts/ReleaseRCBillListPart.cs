using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraEditors.Controls;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FAM.UI.ReleaseRC
{
    [ToolboxItem(false)]
    public partial class ReleaseRCBillListPart : BaseListPart 
    {

        #region 服务注入

        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }

        #endregion

        public ReleaseRCBillListPart()
        {
            InitializeComponent();
            Disposed += delegate {
                gcMain.DataSource = null;
                bsList.DataSource = null;
                bsList.Dispose();
                _OperationCommonInfo = null;
                if (WorkItem != null)
                {
                    WorkItem.Items.Remove(this);
                    WorkItem = null;
                }
            
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //帐单状态
            List<EnumHelper.ListItem<BillState>> currencyBillStates
                = EnumHelper.GetEnumValues<BillState>(LocalData.IsEnglish);
            foreach (var item in currencyBillStates)
            {
                rcmbState.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }

            //到账状态
            List<EnumHelper.ListItem<PaidStatus>> currencyPaidStatue
                = EnumHelper.GetEnumValues<PaidStatus>(LocalData.IsEnglish);
            foreach (var item in currencyPaidStatue)
            {
                cmbPaidStatue.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
        }

        [CommandHandler(ReleaseRCCommondConstants.Command_ChangedTab)]
        public void Command_ChangedTab(object o, EventArgs e)
        {
            if (_OperationCommonInfo == null) return;

            if (Visible && showedOperationID!= _OperationCommonInfo.OperationID)
            {
                BillListAllData data = FinanceService.GetBillListByList(BillSearchAuditorStatue.All, BillSearchWriteOffStatue.All, BillSearchFeeWay.AR, BillSearchInvoiceStatue.All
                                                                       , null, string.Empty, _OperationCommonInfo.OperationNo
                                                                       , string.Empty, string.Empty, string.Empty
                                                                       , null, null, null, null, null, null
                                                                       , new DataPageInfo
                                                                       {
                                                                           CurrentPage = 0
                                                                           ,
                                                                           PageSize = 1000
                                                                           ,
                                                                           SortByName = "BillNo"
                                                                           ,
                                                                           SortOrderType = SortOrderType.Asc
                                                                       }
                                                                       , _OperationCommonInfo.OperationType, null, LocalData.IsEnglish);

                showedOperationID = _OperationCommonInfo.OperationID;
                bsList.DataSource = data.PageList;
                bsList.ResetBindings(false);
            }
        }

        #region IListPart 成员

        OperationCommonInfo _OperationCommonInfo = null;
        Guid showedOperationID = Guid.Empty;

        /// <summary>
        /// OperationCommonInfo
        /// </summary>
        public override object DataSource
        {
            get { return _OperationCommonInfo; }
            set { BindingData(value); }
        }

        private void BindingData(object value)
        {
            showedOperationID = Guid.Empty;
            _OperationCommonInfo = null;
            ReleaseBLList parentList = value as ReleaseBLList;
            if (parentList != null)
                _OperationCommonInfo = FCMCommonService.GetOperationCommonInfo(parentList.OperationID, parentList.OperationType);

            if (_OperationCommonInfo == null)
            {
                bsList.DataSource = typeof(CurrencyBillList);
                Enabled = false;
            }
            else
            {
                Enabled = true;
                if (Visible)
                {

                    BillListAllData data = FinanceService.GetBillListByList(BillSearchAuditorStatue.All, BillSearchWriteOffStatue.All, BillSearchFeeWay.AR, BillSearchInvoiceStatue.All
                                                                     , null, string.Empty, _OperationCommonInfo.OperationNo
                                                                     , string.Empty ,string.Empty,string.Empty
                                                                     ,null,null,null,null,null,null
                                                                     ,new DataPageInfo
                                                                     { CurrentPage =0
                                                                         ,PageSize = 1000
                                                                         ,  SortByName ="BillNo"
                                                                         , SortOrderType = SortOrderType.Asc
                                                                     }
                                                                     ,_OperationCommonInfo.OperationType,null,LocalData.IsEnglish);
                    
                
                    showedOperationID = _OperationCommonInfo.OperationID;
                    bsList.DataSource = data.PageList;
                    bsList.ResetBindings(false);
                }
            }
        }
        #endregion
    }
}
