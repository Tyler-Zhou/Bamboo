using System;
using System.Collections.Generic;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.WF.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.WF.Controls.Form.CustomerExpense
{
    public partial class WFCustomerExpenseLogListPart : BaseListPart
    {
        public WFCustomerExpenseLogListPart()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this.gcMain.DataSource = null;
                this.bsList.DataSource = null;
                this.bsList.Dispose();

                if (this.Workitem != null)
                {
                    this.Workitem.Items.Remove(this);
                    this.Workitem = null;
                }
            };
        }

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IWorkFlowExtendService WorkFlowExtendService 
        {
            get 
            {
                return ServiceClient.GetService<IWorkFlowExtendService>();
            }
        }

        public override object DataSource
        {
            set
            {
                SetDataSource(value);
            }
            get
            {
                return bsList.DataSource; 
            }
        }
        private void SetDataSource(object value)
        {
            WFCECRMCustomerTouchLogList item = value as WFCECRMCustomerTouchLogList;
            if (value != null)
            {
                List<WFCustomerExpenseLogList> list = WorkFlowExtendService.GetWFCustomerExpenseLogList(new Guid[1] { item.ID }, LocalData.IsEnglish);
                bsList.DataSource = list;
            }
            else
            {
                bsList.DataSource = new List<WFCommissionLogList>();
            }
            bsList.ResetBindings(false);

        }



    }
}
