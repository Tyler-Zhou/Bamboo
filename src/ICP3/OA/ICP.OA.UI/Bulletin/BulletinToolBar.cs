using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using ICP.OA.ServiceInterface.DataObjects;

namespace ICP.OA.UI.Bulletin
{
    [ToolboxItem(false)]
    public partial class BulletinToolBar : ICP.Framework.ClientComponents.UIFramework.BaseEditPart
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public BulletinToolBar()
        {
            InitializeComponent();
            this.Disposed += delegate {
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };

            BulidCommand();
        }

        private void BulidCommand()
        {
            barAdd.ItemClick += delegate { Workitem.Commands[BulletinCommonConstants.Command_AddData].Execute(); };
            barEdit.ItemClick += delegate { Workitem.Commands[BulletinCommonConstants.Command_EditData].Execute(); };
            barDelete.ItemClick += delegate { Workitem.Commands[BulletinCommonConstants.Command_DeleteData].Execute(); };

            barShowSearch.ItemClick += delegate { Workitem.Commands[BulletinCommonConstants.Command_ShowSearch].Execute(); };

            barClose.ItemClick += delegate { this.FindForm().Close(); };
        }

        public override object DataSource
        {
            get
            {
                return null;
            }
            set
            {
                BindingData(value);
            }
        }

        private void BindingData(object value)
        {
            BulletinData listData = value as BulletinData;

            if (listData == null)
                barDelete.Enabled = barEdit.Enabled = false;
            else
                barDelete.Enabled = barEdit.Enabled = true;
        }
    }
}
