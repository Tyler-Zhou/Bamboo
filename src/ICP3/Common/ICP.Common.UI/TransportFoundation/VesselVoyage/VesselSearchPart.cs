using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Common.UI.TransportFoundation.VesselVoyage
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class VesselSearchPart : DevExpress.XtraEditors.XtraUserControl, ICP.Framework.ClientComponents.UIManagement.ISearchPart
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }

        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }

        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }

        #endregion

        public VesselSearchPart()
        {
            InitializeComponent();

            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate {
                this.DataReturned = null;
                this.cmbCarrier.OnFirstEnter -= this.OncmbCarrierFirstEnter;
                CommonUtility.RemoveSearchPartKeyEnterToSearch(new List<Control> { this.txtCode, this.txtName },this.KeyEventHandle);
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }

        private void SetCnText()
        {
            labCarrier.Text = "船东";
            labCode.Text = "代码";
            labIsValid.Text = "有效";
            labMax.Text = "最大行数";
            labName.Text = "名称";
            lwchkIsValid.CheckedText = "有效";
            lwchkIsValid.NULLText = "全部";
            lwchkIsValid.UnCheckedText = "无效";
            this.btnClear.Text = "清空(&L)";
            this.btnSearch.Text = "查询(&R)";
            nbarBase.Caption = "基本信息";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
            //if (DataReturned != null)
            //    DataReturned(this, new ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>(GetData()));

            CommonUtility.SearchPartKeyEnterToSearch(new List<Control> { this.txtCode, this.txtName }, this.btnSearch,this.KeyEventHandle);
        }
        private void KeyEventHandle(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnSearch.PerformClick();
        }
        private void OncmbCarrierFirstEnter(object sender, EventArgs e)
        {
            List<CustomerList> customers = CustomerService.GetCustomerListByList(string.Empty, string.Empty, string.Empty, string.Empty,
                                                                           string.Empty, string.Empty, null, null, null,
                                                                           CustomerType.Carrier, null, null, null, null, null, 0);

            CustomerList emptyCustomer = new CustomerList();
            emptyCustomer.CName = emptyCustomer.EName = string.Empty;
            emptyCustomer.ID = Guid.Empty;
            customers.Insert(0, emptyCustomer);
            this.cmbCarrier.Properties.BeginUpdate();
            foreach (CustomerList item in customers)
            {
                cmbCarrier.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
            }
            this.cmbCarrier.Properties.EndUpdate();
        }
        void InitControls()
        {
            this.cmbCarrier.OnFirstEnter += this.OncmbCarrierFirstEnter;
           
          
        }

        #region ISearchPart 成员

        public object GetData()
        {
            List<VesselList> list = TransportFoundationService.GetVesselList(txtCode.Text.Trim().Trim (),
                                                            txtName.Text.Trim(),
                                                            cmbCarrier.Text.Trim(),
                                                            lwchkIsValid.Checked,
                                                            int.Parse(numMax.Value.ToString()));

            return list;
        }

        public event EventHandler<ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>> DataReturned;

        #endregion

        #region btn
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (DataReturned != null)
            {
                using (new CursorHelper())
                {
                    DataReturned(this, new ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>(GetData()));
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                if (item is DevExpress.XtraEditors.TextEdit
                    && (item is DevExpress.XtraEditors.SpinEdit) == false
                    && (item is DevExpress.XtraEditors.ComboBoxEdit) == false
                    
                    && item.Enabled == true
                    && (item as DevExpress.XtraEditors.TextEdit).Properties.ReadOnly == false)
                    item.Text = string.Empty;
            }
            cmbCarrier.SelectedIndex = 0;

            lwchkIsValid.Checked = true;
            txtCode.Focus();
        }

        #endregion

        #region ISearchPart 成员

        public virtual void InitialValues(string searchValue, string property, SearchConditionCollection conditions, FinderTriggerType triggerType)
        {
            btnClear.PerformClick();
        }

        #endregion
    }
}
