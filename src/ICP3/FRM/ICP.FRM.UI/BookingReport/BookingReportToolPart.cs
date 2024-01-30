using System;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;

namespace ICP.FRM.UI.BookingReport
{
    [ToolboxItem(false)]
    public partial class BookingReportToolPart : BaseToolBar
    {
        public BookingReportToolPart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                barSearch.ItemClick += delegate { Workitem.Commands[BookingReportCommonConstants.Command_ShowSearch].Execute(); };
                barExPortToExcel.ItemClick += delegate { Workitem.Commands[BookingReportCommonConstants.Command_ExportToExcel].Execute(); };
                barClose.ItemClick += delegate { FindForm().Close(); };
            }
        }
    }
}
