using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICP.Common.CommandHandler.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.DataCache.ServiceInterface;

namespace ICP.Business.Common.UI.Contact
{

    /// <summary>
    /// 单选客户或承运人角色的联系人列表
    /// </summary>
    public partial class UCSelectedContactListPart : DevExpress.XtraEditors.XtraUserControl
    {
        public WorkItem RootWorkItem
        {
            get { return ServiceClient.GetClientService<WorkItem>(); }
        }

        private UCCustomerList _customerList;
        public UCCustomerList CustomerList
        {
            get
            {
                if (_customerList == null)
                {
                    _customerList = RootWorkItem.Items.AddNew<UCCustomerList>();
                    _customerList.IsShowCaption = true;
                    _customerList.IsShowCustomerNameColumn = true;
                }

                return _customerList;
            }
        }

        private List<CustomerCarrierObjects> _DemoCustomerList;
        public List<CustomerCarrierObjects> demoCustomerList
        {
            get { return _DemoCustomerList ?? new List<CustomerCarrierObjects>(); }

        }
        private EmailSourceType emailSourceType = EmailSourceType.Customer;
        public EmailSourceType Type
        {
            get
            {
                return this.emailSourceType;
            }
            set
            {
                this.emailSourceType = value;
            }
        }

        public UCSelectedContactListPart()
        {
            InitializeComponent();
            if (!LocalData.IsDesignMode)
            {
                //this.CreateHandle();
                if (!LocalData.IsEnglish) SetCtnText();

            }
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        private void SetCtnText()
        {
            this.rdoCustomer.Text = "客户";
            this.rdoCarrier.Text = "承运人";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!LocalData.IsDesignMode)
            {
                Init();
            }
        }

        private void Init()
        {
            pnlContactList.SuspendLayout();
            CustomerList.Dock = DockStyle.Fill;
            this.pnlContactList.Controls.Add(CustomerList);
            pnlContactList.ResumeLayout(false);
            CustomerList.SetCustomerGridListToolTip();
        }

        public void SetDataSource(BusinessOperationContext operationContext)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((System.Action)(delegate
                    {
                        InnerSetDataSource(operationContext);
                    }));
            }
            else
                InnerSetDataSource(operationContext);
        }

        private void InnerSetDataSource(BusinessOperationContext operationContext)
        {
            rdoCustomer.Checked = true;
            CustomerList.ShowCaption(false);
            bool isCustomer = IsCustomer();
            CustomerList.IsCustomerPart = isCustomer;
            this.CustomerList.OperationContext = operationContext;
            this.CustomerList.Type = isCustomer ? ContactType.Customer : ContactType.Carrier;
            this.CustomerList.DataSource = demoCustomerList;
            ICP.Message.ServiceInterface.Message currentMessage = MailHelper.GetMailInfo();
            CustomerList.InsertList(ICP.FCM.Common.ServiceInterface.FCMInterfaceUtility.GetOperationContactsAndMailContacts(currentMessage, operationContext, true, false, false));
            bool[] arrList = { false, false, false, false, false, false, false, false, false };
            CustomerList.conditions = arrList;
        }

        /// <summary>
        /// 验证联系人数据行是否可以满足，如满足，就可以保存
        /// </summary>
        /// <returns></returns>
        public bool ValidateData()
        {
            return CustomerList.ValidateData();
        }
        /// <summary>
        /// 保存联系人信息
        /// </summary>
        /// <param name="operationID"></param>
        /// <param name="updateDate"></param>
        /// <returns></returns>
        public bool SaveData(Guid operationID, OperationType operationType, DateTime? updateDate)
        {
            return CustomerList.Save();
        }

        private bool IsCustomer()
        {
            if (this.rdoCustomer.Checked)
            {
                Type = EmailSourceType.Customer;
                CustomerList.IsCustomerPart = true;
                CustomerList.Type = ContactType.Customer;
                this.CustomerList.ChangeDataRecordContactType(ContactType.Customer);
                return true;
            }
            else
            {
                Type = EmailSourceType.Shipper;
                CustomerList.IsCustomerPart = false;
                CustomerList.Type = ContactType.Carrier;
                this.CustomerList.ChangeDataRecordContactType(ContactType.Carrier);
                return false;
            }
        }

        private void rdoCustomer_CheckedChanged(object sender, EventArgs e)
        {
            IsCustomer();
        }

        private void rdoCarrier_CheckedChanged(object sender, EventArgs e)
        {
            IsCustomer();
        }
        public void AddCheckEventHandler()
        {
            this.rdoCarrier.CheckedChanged += this.rdoCarrier_CheckedChanged;
            this.rdoCustomer.CheckedChanged += this.rdoCustomer_CheckedChanged;
        }
        public void RemoveCheckEventHandler()
        {
            this.rdoCarrier.CheckedChanged -= this.rdoCarrier_CheckedChanged;
            this.rdoCustomer.CheckedChanged -= this.rdoCustomer_CheckedChanged;
        }
    }
}
