using System;
using System.Collections.Generic;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Common.UI.CustomerManager
{
    /// <summary>
    /// 客户发票抬头信息
    /// </summary>
    public partial class CustomerInvoiceTitleEditPart : BaseEditPart
    {
        public CustomerInvoiceTitleEditPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.bsList.DataSource = null;
                this.bsList.Dispose();
                this.Saved = null;
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
        /// <summary>
        /// BsList
        /// </summary>
        public override object DataSource
        {
            get
            {
                return this.bsList.DataSource;
            }
            set
            {
                this.bsList.DataSource = value;
            }
        }
        /// <summary>
        /// 当前数据源
        /// </summary>
        private CustomerInvoiceTitleInfo InvoiceTitle
        {
            get
            {
                return this.bsList.DataSource as CustomerInvoiceTitleInfo;
            }
        }
        #endregion

        #region 窗体事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerInvoiceTitle_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                InitControls();
            }
        }
        private void InitControls()
        {
            List<EnumHelper.ListItem<CustomerInvoiceType>> customerTypes = EnumHelper.GetEnumValues<CustomerInvoiceType>(LocalData.IsEnglish);
            customerTypes.RemoveAll(item => item.Value == CustomerInvoiceType.Unknown);
            this.cmbType.Properties.BeginUpdate();
            foreach (var item in customerTypes)
            {
                cmbType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            this.cmbType.Properties.EndUpdate();
            this.txtAddressTel.ToolTip = "多个地址请用回车换行隔开";
            this.txtBankAccountNo.ToolTip = "多个银行帐号请用回车换行隔开";
            this.txtTaxNo.ToolTip = "多个税号请用回车换行隔开";
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            var findForm = this.FindForm();
            if (findForm != null) findForm.Close();
        }

        #endregion

        #region 保存
        public override event SavedHandler Saved;
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.btnSave.Focus();
            this.Focus();
            InvoiceTitle.EndEdit();

            if (!InvoiceTitle.Validate())
            {
                return ;
            }

           SingleResultData data=Controller.SaveCustomerInvoiceTitleInfo(InvoiceTitle);

           InvoiceTitle.ID = data.ID;
           InvoiceTitle.UpdateDate = data.UpdateDate;

           if (Saved != null)
           {
               Saved(InvoiceTitle);
           }

           LocalCommonServices.ErrorTrace.SetSuccessfullyInfo( this.FindForm(), "保存成功!");

            var findForm = this.FindForm();
            if (findForm != null) findForm.Close();
        }
        #endregion



    }
}
