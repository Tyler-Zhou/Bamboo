using System.ComponentModel;
using DevExpress.XtraBars;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;

namespace ICP.FAM.UI.GLCode.Finder
{
    [ToolboxItem(false)]
    public partial class GLCodeFinderToolBar : BaseEditPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region Init

        public GLCodeFinderToolBar()
        {
            InitializeComponent();
            Disposed += delegate {
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        #endregion

        #region Button

        private void barConfirm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[GLCodeCommandConstants.Command_FinderConfirm].Execute();
            FindForm().Close();
        }

        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindForm().Close();
        }

        #endregion

        #region BaseEdit成员

        public override object DataSource
        {
            get
            {
                return null;
            }
            set
            {
                SolutionGLCodeList data = value as SolutionGLCodeList;
                if (data == null)
                    barConfirm.Enabled = false;
                else
                    barConfirm.Enabled = true;
            }
        }

        #endregion
    }
}
