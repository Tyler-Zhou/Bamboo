using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraBars;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FAM.UI.Business
{
    [ToolboxItem(false)]
    public partial class BusinessToolBar : BaseEditPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        public BusinessToolBar()
        {
            InitializeComponent();
            Disposed += delegate {
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            BulidCommond();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (LocalCommonServices.PermissionService.HaveActionPermission(ActionsConstants.FAM_EDITSPECIALBILL) == false)
            {
                barBatchAddBill.Visibility = BarItemVisibility.Never;
            }
            
        }

        private void BulidCommond()
        {
            barViewBusinessInfo.ItemClick += delegate { Workitem.Commands[BusinessCommandConstants.Command_ViewBusinessInfo ].Execute(); };
            barBill.ItemClick += delegate { Workitem.Commands[BusinessCommandConstants.Command_Bill].Execute(); };
            barShowSearch.ItemClick += delegate { Workitem.Commands[BusinessCommandConstants.Command_ShowSearch].Execute(); };
            barSelectAll.ItemClick += delegate { Workitem.Commands[BusinessCommandConstants.Command_SelectAll].Execute(); };
            barRefresh.ItemClick += delegate { Workitem.Commands[BusinessCommandConstants.Command_Refresh].Execute(); };
            barBatchAddBill.ItemClick += delegate { Workitem.Commands[BusinessCommandConstants.Command_BatchAddBill].Execute(); };

            barClose.ItemClick += delegate { FindForm().Close(); };
        }

        #region IEditPart成员

        public override object DataSource
        {
            get
            {
                return null;
            }
            set
            {
                BindingSource(value);
            }
        }

        private void BindingSource(object value)
        {
            BusinessList listData = value as BusinessList;
            if (listData == null || listData.IsNew)
            {
                barViewBusinessInfo.Enabled =
                barBill.Enabled =
                barSelectAll.Enabled =
                barRefresh.Enabled =false;
            }
            else
            {
                barViewBusinessInfo.Enabled =
                barBill.Enabled =
                barSelectAll.Enabled =
                barRefresh.Enabled =true;

            }
        }

        #endregion

        #region IPart 成员
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key == "Selected")
                {
                    List<BusinessList> data = item.Value as List<BusinessList>;
                    if (data == null || data.Count == 0) barBatchAddBill.Enabled = false;
                    else barBatchAddBill.Enabled = true;
                }
            }
        }
        #endregion
    }
}
