using System;
using System.ComponentModel;
using DevExpress.XtraEditors.Controls;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FAM.UI.ReleaseRC
{
    [ToolboxItem(false)]
    public partial class ReleaseRCDebtListPart : BaseListPart
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }

        #endregion

        #region init

        public ReleaseRCDebtListPart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                _OperationCommonInfo = null;
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(ReportWorkspace);
                    Workitem.Items.Remove(this);
                    ReportWorkspace.PerformLayout();
                    Workitem = null;
                }
            };
        }

        #endregion

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (cmbCustomer.EditValue == null) return;

            //List<FollowingBusinessList> list = finService.GetFollowingBusinessList(_OperationCommonInfo.OperationID, new Guid(cmbCustomer.EditValue.ToString()), _OperationCommonInfo.OperationType);
            //bsList.DataSource = list;
            //bsList.ResetBindings(false);
        }


        #region IListPart 成员

        OperationCommonInfo _OperationCommonInfo = null;

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
            _OperationCommonInfo = null;
            ReleaseBLList parentList = value as ReleaseBLList;
            if(parentList !=null)
                _OperationCommonInfo = FCMCommonService.GetOperationCommonInfo(parentList.OperationID, parentList.OperationType);


            cmbCustomer.Properties.Items.Clear();
            if (_OperationCommonInfo == null)
            {
                Enabled = false;
            }
            else
            {
                Enabled = true;
                cmbCustomer.Properties.Items.Clear();
                foreach (var item in _OperationCommonInfo.TradeCustomers)
                {
                    cmbCustomer.Properties.Items.Add(new ImageComboBoxItem(item.EName, item.ID));
                }
                cmbCustomer.SelectedIndex = 0;
            }
        }
        #endregion
    }
}
