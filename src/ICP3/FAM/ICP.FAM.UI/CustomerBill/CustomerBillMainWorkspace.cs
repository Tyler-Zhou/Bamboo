using System;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.FAM.UI.CustomerBill
{
    /// <summary>
    /// 编辑账单界面WorkSpace
    /// </summary>
    public partial class CustomerBillMainWorkspace : BasePart
    {
        #region Services
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; } 
        #endregion

        #region Init
        /// <summary>
        /// 
        /// </summary>
        public CustomerBillMainWorkspace()
        {
            InitializeComponent();
            Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(EditWorkspace);
                    Workitem.Workspaces.Remove(ListWorkspace);
                    Workitem.Items.Remove(this);


                    EditWorkspace.PerformLayout();
                    ListWorkspace.PerformLayout();
                    PerformLayout();
                    Workitem.Dispose();
                    Workitem = null;
                }
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (LocalData.IsEnglish == false) SetCnText();
        } 
        #endregion

        #region Custom Method
        /// <summary>
        /// 设置中文
        /// </summary>
        private void SetCnText()
        {
            groupControl1.Text = "编辑帐单";
        } 
        #endregion
    }
}
