using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;

namespace ICP.Common.UI.Authcodes
{
    public partial class AuthcodeTool : BaseToolBar
    {

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public AuthcodeTool()
        {
            InitializeComponent();

            BulidCommond();
        }


        /// <summary>
        /// 注册命令
        /// </summary>
        private void BulidCommond()
        {

            barNew.ItemClick += delegate { Workitem.Commands[AuthcodeCommandConstants.Command_AuthcodeAdd].Execute(); };
            barSearch.ItemClick += delegate { Workitem.Commands[AuthcodeCommandConstants.Command_AuthcodeShowSearch].Execute(); };
            barDelete.ItemClick += delegate {Workitem.Commands[AuthcodeCommandConstants.Command_AuthcodeDelete].Execute(); };
            barClose.ItemClick += delegate
            {
                var findForm = this.FindForm();
                if (findForm != null) findForm.Close();
            };
        }
    }
}
