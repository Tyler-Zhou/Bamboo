using System;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;

namespace ICP.FAM.UI
{
    [ToolboxItem(false)]
    public partial class InvoiceDetailListPart : BaseListPart
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion


        public InvoiceDetailListPart()
        {
            InitializeComponent();
            Disposed += delegate { 
                if (Workitem != null) 
                {
                     Workitem.Items.Remove(this); 
                      Workitem=null;
                    } 
            };
        }

        #region 属性
        public string FormType
        {
            get;
            set;
        }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
            }
        }

        private void InitControls()
        {
            if (FormType == "List")
            {
                bar2.Visible = false;
                gvMain.OptionsBehavior.Editable = false;
            }

        }

    
    }
}
