using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.UI.CustomerManager;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.ClientComponents.Controls;

namespace ICP.Common.UI.CustomerFinder
{
    public partial class CustomerMainListPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region Init

        public CustomerMainListPart()
        {
            InitializeComponent();

            colEName.Width = 300;
            colCName.Width = 300;

            this.Disposed += delegate {
                this.Selected = null;
                this.CurrentChanged = null;
                this.CurrentChanging = null;
                this.gvMain.BeforeLeaveRow -= this.gvMain_BeforeLeaveRow;
                this.gvMain.RowStyle -= this.gvMain_RowStyle;
                this.gcMain.DataSource = null;
                this.bsList.PositionChanged -= this.bsMainList_PositionChanged;
                this.bsList.DataSource = null;
                this.bsList.Dispose();

                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CommonUtility.ShowGridRowNo(gvMain);
        }

        #endregion

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }
        protected CustomerInfo CurrentRow
        {
            get { return Current as CustomerInfo; }
        }

        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                bsList.DataSource = value;
                bsList.ResetBindings(false);
                if (CurrentChanged != null) CurrentChanged(this, Current);
                if (bsList.List.Count > 0)
                {
                    gvMain.Focus();
                    gvMain.SelectRow(0);
                }
            }
        }

        public override void Refresh(object items)
        {
            List<CustomerInfo> list = this.DataSource as List<CustomerInfo>;
            if (list == null) return;
            List<CustomerInfo> newLists = items as List<CustomerInfo>;

            foreach (var item in newLists)
            {
                CustomerInfo tager = list.Find(delegate(CustomerInfo jItem) { return item.ID == item.ID; });
                if (tager == null) continue;

                CommonUtility.CopyToValue(tager, item, typeof(CustomerInfo));
            }
            bsList.ResetBindings(false);
        }

        public override void RemoveItem(int index)
        {
            bsList.RemoveAt(index);
        }

        public override void RemoveItem(object item)
        {
            bsList.Remove(item);
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SelectedHandler Selected;

        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;
        public override event CancelEventHandler CurrentChanging;

        #endregion

        #region GridView Event

        private void bsMainList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        private void gvMain_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            this.gvMain.BeforeLeaveRow -= new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(gvMain_BeforeLeaveRow);
            if (CurrentChanging != null)
            {
                CancelEventArgs ce = new CancelEventArgs();
                CurrentChanging(this, ce);
                e.Allow = !ce.Cancel;
            }
            this.gvMain.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(gvMain_BeforeLeaveRow);
        }

        private void gvMain_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            CustomerInfo list = gvMain.GetRow(e.RowHandle) as CustomerInfo;
            if (list == null) return;

            if (list.State == CustomerStateType.Invalid)
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Disabled);
        }

        #endregion

        #region Workitem Common

        [CommandHandler(CustomerCommonConstants.Common_FinderAdd)]
        public void Common_FinderAdd(object sender, EventArgs e)
        {
            CustomerManagerCheckForm checkForm = this.Workitem.Items.AddNew<CustomerManagerCheckForm>();

            string title = LocalData.IsEnglish ? "New Customer" : "新增客户";
            DialogResult dlg = PartLoader.ShowDialog(checkForm, title);
            if (dlg != DialogResult.OK)
            {
                return;
            }

            CustomerInfo customer = new CustomerInfo();
            if (LocalData.IsEnglish)
            {
                customer.EName = checkForm.CustomerName;
            }
            else
            {
                customer.CName = checkForm.CustomerName;
            }

            ISmartPartInfo info = new SmartPartInfo();
            info.Title = title;
            ShowEditCustomerForm(customer, info);
        }

        /// <summary>
        /// 编辑客户
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CustomerCommonConstants.Common_FinderEdit)]
        public void Common_FinderEdit(object s, EventArgs e)
        {
            if (bsList.Current == null) return;
            var customer = bsList.Current as CustomerInfo;
            ISmartPartInfo info = new SmartPartInfo();
            info.Title = LocalData.IsEnglish ? "Edit Customer" : "编辑客户";
            ShowEditCustomerForm(customer, info);
        }

        private void ShowEditCustomerForm(CustomerInfo customer, ISmartPartInfo info)
        {
            if (customer == null) return;
            CustomerManagerEditPart editPart = this.Workitem.Items.AddNew<CustomerManagerEditPart>();
            editPart.Saved += new SavedHandler(editPart_Saved);
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("CustomerList", customer);
            editPart.Init(values);
            IWorkspace mainWorkspace = this.Workitem.RootWorkItem.Workspaces[ClientConstants.MainWorkspace];
            mainWorkspace.Show(editPart, info);
        }

        private void editPart_Saved(params object[] prams)
        {
            if (prams == null || prams.Length == 0) return;

            CustomerInfo data = prams[0] as CustomerInfo;
            List<CustomerInfo> source = this.DataSource as List<CustomerInfo>;
            if (source == null || source.Count == 0)
            {
                List<CustomerInfo> orgSource = new List<CustomerInfo>();
                bsList.DataSource = orgSource;
                bsList.Insert(0, data);
                bsList.ResetBindings(false);
            }
            else
            {
                CustomerInfo tager = source.Find(delegate(CustomerInfo item) { return item.ID == data.ID; });
                if (tager == null)
                {
                    bsList.Insert(0, data);
                    bsList.ResetBindings(false);
                }
                else
                {
                    CommonUtility.CopyToValue(data, tager, typeof(CustomerInfo));
                    bsList.ResetItem(bsList.IndexOf(tager));
                }
            }
        }

        #endregion

      
    }
}
