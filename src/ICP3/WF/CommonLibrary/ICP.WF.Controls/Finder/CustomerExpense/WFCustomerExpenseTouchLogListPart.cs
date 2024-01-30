using System.Collections.Generic;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.WF.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;

namespace ICP.WF.Controls.Form.CustomerExpense
{
    public partial class WFCustomerExpenseTouchLogListPart : BaseListPart
    {
        public WFCustomerExpenseTouchLogListPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.gcMain.DataSource = null;
                this.bsList.PositionChanged -= this.bsList_PositionChanged;
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
            get
            {
                return base.DataSource;
            }
            set
            {
                SetDataSource(value);
            }
        }
        /// <summary>
        /// 数据列表
        /// </summary>
        public List<WFCECRMCustomerTouchLogList> DataList
        {
            get
            {
                List<WFCECRMCustomerTouchLogList> list = bsList.DataSource as List<WFCECRMCustomerTouchLogList>;
                if(list==null)
                {
                    list = new List<WFCECRMCustomerTouchLogList>();
                }
                return list;
            }
        }

        public WFCECRMCustomerTouchLogList CurrentRow
        {
            get
            {
                return bsList.Current as WFCECRMCustomerTouchLogList;
            }
        }

        private void SetDataSource(object value)
        {
           WFCECRMCustomerList item = value as WFCECRMCustomerList;
             List<WFCECRMCustomerTouchLogList> list;
           if (item == null)
           {
               list = new List<WFCECRMCustomerTouchLogList>();
           }
           else
           {
               list = WorkFlowExtendService.GetCRMCustomerTouchLogList(item.ID, LocalData.IsEnglish);
           }

           bsList.DataSource = list;
           bsList.ResetBindings(false);     
           
        }



        public override event CurrentChangedHandler CurrentChanged;

        void bsList_PositionChanged(object sender, System.EventArgs e)
        {
            if (CurrentChanged != null)
            {
                CurrentChanged(this, CurrentRow);
            }
        }

        void barOK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (CurrentRow == null)
            //{
            //    string message = LocalData.IsEnglish ? "Please select a touch log data" : "请选择一条跟进纪录";
            //    DevExpress.XtraEditors.XtraMessageBox.Show(message);
            //    return;
            //}

            Workitem.Commands[CustomerExpenseFinderConstants.Command_CustomerExpense_FinderConfirm].Execute();
        }

    }
}
