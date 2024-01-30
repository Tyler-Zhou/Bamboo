using System;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Common.UI.CustomerManager
{
    public partial class TaxCustomerInvoiceTitleListPart : BaseListEditPart
    {
        public TaxCustomerInvoiceTitleListPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.mainGridView.CustomDrawRowIndicator -= this.mainGridView_CustomDrawRowIndicator;
                this.mainGridView.DoubleClick -= this.mainGridView_DoubleClick;
                this.mainGridList.DataSource = null;
                this.bsDataSource.DataSource = null;
                this.bsDataSource.Dispose();
                this.Selected = null;
                this._currentCustomer = null;

                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }

        #region 服务

        [ServiceDependency]
        public WorkItem WorkItem { get; set; }
        /// <summary>
        /// 客户管理控制器
        /// </summary>
        public CustomerManagerController Controller
        {
            get
            {
                return ClientHelper.Get<CustomerManagerController, CustomerManagerController>();
            }
        }
        #endregion

        #region 属性
        private CustomerList _currentCustomer = null;
        public CustomerInvoiceTitleInfo CurrentData
        {
            get
            {
                return bsDataSource.Current as CustomerInvoiceTitleInfo;
            }
        }
        #endregion

        #region 重写

        public override object DataSource
        {
            get
            {
                return this.bsDataSource.DataSource;
            }
            set
            {
                this.bsDataSource.DataSource = value;
            }
        }
        public object CurrentRow
        {
            get
            {
                return this.bsDataSource.Current;
            }
        }

        #endregion

        #region 窗体事件
        private void mainGridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }
       
       
        private void mainGridView_DoubleClick(object sender, System.EventArgs e)
        {
                SelecteData();

        }
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barOK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SelecteData();
        }
        void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var findForm = this.FindForm();
            if (findForm != null) findForm.Close();
        }

        #endregion

        #region 搜索器

        public void SelecteData()
        {
            if (CurrentData == null)
            {
                return;
            }

            WorkItem.Commands[CustomerInvoiceTitleFinderConstants.Command_CustomerInvoiceTitle_FinderConfirm].Execute();

        }

        /// <summary>
        /// 选择数据
        /// </summary>
        public override event SelectedHandler Selected;
        #endregion

    }
}
