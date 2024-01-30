using System;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Common.UI.ShippingLineManager
{
    public partial class ShippingLineMainWorkspace : DevExpress.XtraEditors.XtraUserControl
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public ShippingLineMainWorkspace()
        {
            InitializeComponent();
        }

           private void SetCnText()
        {
            this.dpSearch.Text = "查询";
            this.tabCountryPort.Text = "国家和港口";
            this.tabEdit.Text =  "编辑";
        }

           protected override void OnLoad(EventArgs e)
           {
               base.OnLoad(e);
               if (!LocalData.IsDesignMode)
               {
                   SetCnText();
               }
           }
    }
}
