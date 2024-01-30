using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface;

namespace ICP.FCM.Common.UI.DispatchCompare
{
    [ToolboxItem(false)]
    public partial class HistoryOceanRecordPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart, IDataBind
    {
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        #region 服务注入

        public IOperationAgentService OperationAgentService
        {
            get
            {
                return ServiceClient.GetService<IOperationAgentService>();
            }
        }

        #endregion

        public HistoryOceanRecordPart()
        {
            InitializeComponent();

            if (!LocalData.IsEnglish)
            {
                this.barRefresh.Caption = "刷新";
            }
            this.Disposed += delegate
            {
                this.gcMain.DataSource = null;
                this.bsList.DataSource = null;
                this.bsList.Dispose();
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }

            };
        }


        #region IListPart 成员

        Guid showedOperationID = Guid.Empty;


        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OperationID
        {
            get;
            set;
        }
        /// <summary>
        /// 操作类型（1为海出2为海进）
        /// </summary>
        public int Type
        {
            get;
            set;
        }

        /// <summary>
        /// OperationCommonInfo
        /// </summary>
        public override object DataSource
        {
            get { return bsList.DataSource; }
            set { BindingData(); }
        }

        BusinessOperationContext operationContext = new BusinessOperationContext();
        List<HistoryOceanRecord> Records = new List<HistoryOceanRecord>();
        public void BindingData()
        {
            // OperationID =  value as Guid;
            //  Type = type;

            if (!ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(operationContext.OperationID))
            {
                Records = OperationAgentService.GetHistoryOceanRecord(operationContext.OperationID, (int)operationContext.OperationType);
            }
            else
            {
                Records = OperationAgentService.GetHistoryOceanRecord(OperationID, Type);
            }
            if (Records != null && Records.Count > 0)
            {
                //int count = data.Count;

                //Records.Clear();
                //Records.AddRange(data);

                //data.RemoveAt(count - 1);

                this.bsList.DataSource = Records;
                bsList.ResetBindings(false);

                this.gcMain.Enabled = true;
            }
            else
            {
                this.bsList.DataSource = new List<HistoryOceanRecord>();
                this.gcMain.Enabled = false;
            }
        }


        #endregion

        private void gvMain_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks > 1)
            {
                int rowIndex = e.RowHandle;
                HistoryOceanRecord record = Records[rowIndex];

                if (record.OperationType == 2)
                {
                    FCMUIUtility.ShowHistoryDocumentCompare(WorkItem, record.OEBookingID, record.ID, record.RefID.Value);
                }
                else if (record.OperationType == 1)
                {
                    ICP.FCM.Common.UI.FCMUIUtility.ShowHistoryReviseBillCompare(WorkItem, record.OEBookingID, record.ID, record.RefID.Value);

                }


            }
        }

        private void gvMain_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.Name == "colType")
            {
                if (e.Value.ToString() == "1")
                {
                    e.DisplayText = LocalData.IsEnglish ? "Revise fees" : "分发文件";
                }
                else
                {
                    e.DisplayText = LocalData.IsEnglish ? "Dispatch Docs" : "修订费用";
                }
            }
        }

        private void barRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BindingData();
        }

        #region IDataBind 成员

        public void ControlsReadOnly(bool flg)
        {
            this.Enabled = true;
        }

        public void DataBind(BusinessOperationContext business)
        {
            operationContext = business;
            BindingData();
        }

        #endregion
    }
}
