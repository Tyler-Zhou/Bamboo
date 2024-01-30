using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.FAM.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.Common.UI.UCBillList
{
    [ToolboxItem(false)]
    public partial class UCBillListPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart, IDataBind
    {
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        #region 服务注入

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        public IClientOceanExportService ClientOceanExportService
        {
            get
            {
                return ServiceClient.GetClientService<IClientOceanExportService>();
            }
        }

        #endregion

        public UCBillListPart()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this.gcMain.DataSource = null;
                this.bsList.DataSource = null;
                this.bsList.Dispose();
                this.context = null;
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }

            };
        }
        private bool isBindEnum = false;

        public override object Current
        {
            get { return bsList.Current; }
        }
        public BillList CurrentRow
        {
            get { return Current as BillList; }
        }

        // [CommandHandler(ReleaseBLCommondConstants.Command_ChangedTab)]
        public void Command_ChangedTab(object o, EventArgs e)
        {
            if (context.OperationID == null) return;

            if (this.Visible && showedOperationID != context.OperationID)
            {
                List<BillList> data = FinanceService.GetBillListByOperactioID(context.OperationID);

                showedOperationID = context.OperationID;
                this.bsList.DataSource = data;
                bsList.ResetBindings(false);
            }
        }

        #region IListPart 成员

        Guid showedOperationID = Guid.Empty;
        BusinessOperationContext context = null;
        /// <summary>
        /// OperationCommonInfo
        /// </summary>
        public override object DataSource
        {
            get { return bsList.DataSource; }
            set { BindingData(value); }
        }

        private void BindingData(object value)
        {
            context = value as BusinessOperationContext;

            if (context == null)
            {
                this.bsList.DataSource = new List<BillList>();
                this.Enabled = false;
            }
            else
            {
                this.Enabled = true;
                //if (this.Visible)
                //{

                if (context.OperationID != null && context.OperationID != Guid.Empty)
                {
                    List<BillList> data = FinanceService.GetBillListByOperactioID(context.OperationID);

                    showedOperationID = context.OperationID;
                    BindEnums();
                    this.bsList.DataSource = data;
                    bsList.ResetBindings(false);
                }

                //}
            }
        }

        private void BindEnums()
        {
            if (isBindEnum)
                return;
            //帐单状态
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<BillState>> currencyBillStates
                = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<BillState>(LocalData.IsEnglish);
            foreach (var item in currencyBillStates)
            {
                rcmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }

            //到账状态
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<BillType>> currencyBillType
                = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<BillType>(LocalData.IsEnglish);
            foreach (var item in currencyBillType)
            {
                cmbBillType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            isBindEnum = true;
        }
        #endregion

        #region IDataBind 成员

        public void ControlsReadOnly(bool flg)
        {
            //throw new NotImplementedException();
        }

        public void DataBind(BusinessOperationContext business)
        {
            BindingData(business);
        }

        #endregion

        private void gcMain_DoubleClick(object sender, EventArgs e)
        {
            if (Current != null)
            {
                ClientOceanExportService.OpenBill(this.context.OperationID, OperationType.OceanImport);
            }
        }
    }
}
