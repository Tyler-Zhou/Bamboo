using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.WF.ServiceInterface;

namespace ICP.WF.Controls.Form.CustomerExpense
{
    /// <summary>
    /// CRM客户列表
    /// </summary>
    [ToolboxItem(false)]
    public partial class WFCRMCustomerListPart : BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        #endregion

        #region 初始化

        public WFCRMCustomerListPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.CurrentChanged = null;
                this.gcMain.DataSource = null;
                this.bsList.PositionChanged -= this.bsList_PositionChanged;
                this.bsList.DataSource = null;
                this.bsList.Dispose();
                this.gvMain.CustomDrawRowIndicator -= this.gvMain_CustomDrawRowIndicator;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        #endregion


        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }
        public WFCECRMCustomerList CurrentRow
        {
            get { return Current as WFCECRMCustomerList; }
        }

        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                BindingData(value);
            }
        }

        private void BindingData(object value)
        {
           List<WFCECRMCustomerList> list=value as  List<WFCECRMCustomerList>;
            if(list==null)
            {
                list=new List<WFCECRMCustomerList>();
            }
            bsList.DataSource = list;
            bsList.ResetBindings(false);

            if (CurrentChanged != null)
            {
                CurrentChanged(this, CurrentRow);
            }
        }
        private void gvMain_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) );
            }
        }
        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;

        #endregion

        #region gridview event

        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
        }
        #endregion


    }
}
