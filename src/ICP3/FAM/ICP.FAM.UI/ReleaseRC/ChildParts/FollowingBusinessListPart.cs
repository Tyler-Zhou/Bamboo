using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FAM.UI.ReleaseRC
{
    [ToolboxItem(false)]
    public partial class FollowingBusinessListPart : BaseListPart 
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

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

        public IBusinessInfoProviderFactory BusinessInfoProviderFactory
        {
            get
            {
                return ServiceClient.GetClientService<IBusinessInfoProviderFactory>();
            }
        }

        #endregion

        #region init

        public FollowingBusinessListPart()
        {
            InitializeComponent();
            Disposed += delegate {
                gcMain.DataSource = null;
                bsList.DataSource = null;
                bsList.Dispose();
                _OperationCommonInfo = null;


                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        #endregion

        FollowingBusinessList CurrentRow
        {
            get { return bsList.Current as FollowingBusinessList; }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (cmbCustomer.EditValue == null) return;

            List<FollowingBusinessList> list = FinanceService.GetFollowingBusinessList(_OperationCommonInfo.OperationID, new Guid(cmbCustomer.EditValue.ToString()));
            bsList.DataSource = list;
            bsList.ResetBindings(false);
        }

        #region gridview event

        private void gvMain_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0 || e.Column != colOperationNO || CurrentRow ==null) return;
            
            IBusinessInfoProvider provider = BusinessInfoProviderFactory.GetBusinessInfoProvider(OperationType.OceanExport);
            provider.ShowBusinessInfo(CurrentRow.OperationType, CurrentRow.ID , ClientConstants.MainWorkspace);
        }
        #endregion

        #region IListPart 成员

        OperationCommonInfo _OperationCommonInfo = null;

        /// <summary>
        /// OperationCommonInfo
        /// </summary>
        public override object DataSource
        {
            get {return _OperationCommonInfo; }
            set {BindingData(value);}
        }

        private void BindingData(object value)
        {
            _OperationCommonInfo = null;
            ReleaseBLList parentList = value as ReleaseBLList;
            if (parentList != null)
                _OperationCommonInfo = FCMCommonService.GetOperationCommonInfo(parentList.OperationID, parentList.OperationType);


            cmbCustomer.Properties.Items.Clear();
            if (_OperationCommonInfo == null)
            {
                Enabled = false;
                bsList.DataSource = typeof(FollowingBusinessList);
                bsList.ResetBindings(false);
            }
            else
            {
                Enabled = true;
                cmbCustomer.Properties.Items.Clear();
                foreach (var item in _OperationCommonInfo.TradeCustomers)
                {
                    cmbCustomer.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish?item.EName:item.CName, item.ID));
                }
                cmbCustomer.SelectedIndex = 0;
                bsList.DataSource = typeof(FollowingBusinessList);
            }
        }
        #endregion

    }
}
