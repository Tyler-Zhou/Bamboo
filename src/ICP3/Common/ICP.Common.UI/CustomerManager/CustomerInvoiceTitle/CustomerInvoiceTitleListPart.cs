using System;
using System.Collections.Generic;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.Controls;

namespace ICP.Common.UI.CustomerManager
{
    public partial class CustomerInvoiceTitleListPart : BaseListEditPart
    {
        public CustomerInvoiceTitleListPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
               
                this._currentCustomer = null;
                this.mainGridView.DoubleClick -= this.mainGridView_DoubleClick;
                this.mainGridView.CustomDrawRowIndicator -= this.mainGridView_CustomDrawRowIndicator;
                this.mainGridList.DataSource = null;
                this.bsDataSource.DataSource = null;
                this.bsDataSource.Dispose();
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
        #endregion

        #region 重写
        public override void Init(IDictionary<string, object> values)
        {
            if (values != null)
            {
                if (values.Keys.Contains("CustomerList"))
                {
                    _currentCustomer = (CustomerList)values["CustomerList"];
                    if (_currentCustomer == null)
                    {
                        this.bsDataSource.DataSource = null;
                        this.Enabled = false;
                        return;
                    }
                    else
                    {
                        this.Enabled = true;
                    }

                    List<CustomerInvoiceTitleInfo> invoiceTitleList = new List<CustomerInvoiceTitleInfo>();
                    if (_currentCustomer.ID != null && _currentCustomer.ID != Guid.Empty)
                    {
                        invoiceTitleList = this.Controller.GetCustomerInvoiceTitleList(_currentCustomer.ID, LocalData.UserInfo.DefaultCompanyID);
                    }

                    this.bsDataSource.DataSource = invoiceTitleList;
                }
            }

            RefreshToolBars();
        }

        #endregion

        #region 窗体事件

        public CustomerInvoiceTitleInfo CurrentData
        {
            get
            {
                return this.bsDataSource.Current as CustomerInvoiceTitleInfo;
            }
        }

        private void RefreshToolBars()
        {
            if (CurrentData == null)
            {
                this.barEdit.Enabled = false;
            }
            else
            {
                barEdit.Enabled = true;
            }
            
        }
        private void mainGridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(_currentCustomer==null||CommonUtility.GuidIsNullOrEmpty(_currentCustomer.ID))
            {
                return ;
            }
            CustomerInvoiceTitleInfo itemData = new CustomerInvoiceTitleInfo();
            itemData.CustomerID = _currentCustomer.ID;
            itemData.CreateByID = LocalData.UserInfo.LoginID;
            itemData.CompanyID = LocalData.UserInfo.DefaultCompanyID;
            itemData.UpdateBy = LocalData.UserInfo.LoginID;
            itemData.InvoiceType = CustomerInvoiceType.Dedicated;
            itemData.IsValid = true;

            PartLoader.ShowEditPartInDialog<CustomerInvoiceTitleEditPart>(this.WorkItem, itemData, "新增发票抬头", EditPartSaved);

        }

        /// <summary>
        /// 编辑界面保存数据
        /// </summary>
        /// <param name="prams"></param>
        private void EditPartSaved(object[] prams)
        {
            if (prams == null || prams.Length == 0) return;

            CustomerInvoiceTitleInfo data = prams[0] as CustomerInvoiceTitleInfo;

            List<CustomerInvoiceTitleInfo> source = this.bsDataSource.DataSource as List<CustomerInvoiceTitleInfo>;

            if (source == null || source.Count == 0)
            {
                this.DataSource = new List<CustomerInvoiceTitleEditPart>();
                bsDataSource.Add(data);
                bsDataSource.ResetBindings(false);
            }
            else
            {
                CustomerInvoiceTitleInfo tager = source.Find(delegate(CustomerInvoiceTitleInfo item) { return item.ID == data.ID; });
                if (tager == null)
                {
                    bsDataSource.Insert(0, data);
                    bsDataSource.ResetBindings(false);
                }
                else
                {
                    CommonUtility.CopyToValue(data, tager, typeof(CustomerInvoiceTitleInfo));
                    bsDataSource.ResetItem(bsDataSource.IndexOf(tager));
                }
            }
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditData();
        }
        /// <summary>
        /// 编辑
        /// </summary>
        private void EditData()
        {
            if (CurrentData == null)
            {
                return;
            }

            CustomerInvoiceTitleInfo data = new CustomerInvoiceTitleInfo();
            CommonUtility.CopyToValue(CurrentData, data, typeof(CustomerInvoiceTitleInfo));

            PartLoader.ShowEditPartInDialog<CustomerInvoiceTitleEditPart>(this.WorkItem, data, "编辑发票抬头", EditPartSaved);

        }
        private void mainGridView_DoubleClick(object sender, System.EventArgs e)
        {
              EditData();
        }

        #endregion

    }
}
