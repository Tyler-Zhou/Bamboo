using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Common.UI.TransportFoundation.Flight
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class FlightSearchPart : DevExpress.XtraEditors.XtraUserControl, ICP.Framework.ClientComponents.UIManagement.ISearchPart
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

        #region  init

        public FlightSearchPart()
        {
            InitializeComponent();

            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate
            {
                this.DataReturned = null;
                this.cmbAirLine.OnFirstEnter -= this.OnCmbAirLineEnter;
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }

        private void SetCnText()
        {
            labIsValid.Text = "有效";
            labMax.Text = "最大行数";
            labNo.Text = "航班号";
            labAirline.Text = "航空公司";
            lwchkIsValid.CheckedText = "有效";
            lwchkIsValid.UnCheckedText = "无效";
            lwchkIsValid.NULLText = "全部";
            this.btnClear.Text = "清空(&L)";
            this.btnSearch.Text = "查询(&R)";
            nbarBase.Caption = "基本信息";        
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //List<CustomerList> airlineCustomerlist = customerService.GetCustomerList(string.Empty, string.Empty,
            //                         string.Empty, string.Empty, string.Empty, string.Empty,
            //                         null, null, CustomerStateType.Valid, CustomerType.Airline, null, null, null, null, null,
            //                         100);

            //cmbAirLine.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(string.Empty, Guid.Empty));
            //foreach (var item in airlineCustomerlist)
            //{
            //    cmbAirLine.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
            //}
            //cmbAirLine.SelectedIndex = 0;
            this.cmbAirLine.OnFirstEnter += OnCmbAirLineEnter;
     

            //if (DataReturned != null)
            //    DataReturned(this, new ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>(GetData()));

            //Utility.SearchPartKeyEnterToSearch(new List<Control> { this.txtNo, this.stxtPod,stxtPol }, this.btnSearch);
        }

        #endregion
        private void OnCmbAirLineEnter(object sender, EventArgs e)
        {
            List<CustomerList> airlineCustomerlist = CustomerService.GetCustomerListByList(string.Empty, string.Empty,
                                       string.Empty, string.Empty, string.Empty, string.Empty,
                                       null, null, CustomerStateType.Valid, CustomerType.Airline, null, null, null, null, null,
                                       100);
            CustomerList nullCustomer = new CustomerList();
            nullCustomer.ID = Guid.Empty;
            nullCustomer.EName = nullCustomer.CName = string.Empty;
            airlineCustomerlist.Insert(0, nullCustomer);

            Dictionary<string, string> col = new Dictionary<string, string>();
            col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
            //col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");
            cmbAirLine.InitSource<CustomerList>(airlineCustomerlist, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
        }
        #region ISearchPart 成员

        public object GetData()
        {
            Guid? airLineId = null;
            //if (cmbAirLine.SelectedIndex !=0)
            //{
            //    airLineId = new Guid(cmbAirLine.EditValue.ToString());
            //}

            if (this.cmbAirLine.EditValue != null && new Guid(this.cmbAirLine.EditValue.ToString()) != Guid.Empty)
            {
                airLineId = new Guid(this.cmbAirLine.EditValue.ToString());
            }

            List<FlightList> list = TransportFoundationService.GetFlightList(airLineId,
                                                            txtNo.Text.Trim(),
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

            cmbAirLine.EditValue = null;
            lwchkIsValid.Checked = true;
            txtNo.Focus();
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
