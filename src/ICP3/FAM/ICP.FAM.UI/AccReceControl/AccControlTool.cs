using System.Collections.Generic;
using ICP.Framework.ClientComponents.UIFramework;
using DevExpress.XtraBars;
using Microsoft.Practices.CompositeUI;

namespace ICP.FAM.UI.AccReceControl
{
    public partial class AccControlTool : BaseToolBar
    {
        public AccControlTool()
        {
            InitializeComponent();

            BulidBarItemDictionary();

            BulidCommond();
            Disposed += delegate
            {
                _barItemDic.Clear();
                _barItemDic = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }

            };
        }

        Dictionary<string, BarItem> _barItemDic = new Dictionary<string, BarItem>();

        private void BulidBarItemDictionary()
        {
            foreach (BarItem item in barManager1.Items)
            {
                _barItemDic.Add(item.Name, item);
            }
        }
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        /// <summary>
        /// 注册命令
        /// </summary>
        private void BulidCommond()
        {
            barMail.ItemClick += delegate { Workitem.Commands[AccControlCommandConstants.Command_AccControlMail].Execute(); };
            barMarkStatus.ItemClick += delegate { Workitem.Commands[AccControlCommandConstants.Command_AccControlMarkStatus].Execute(); };
            barCustomerPreference.ItemClick += delegate { Workitem.Commands[AccControlCommandConstants.Command_AccControlCustomerPreference].Execute(); };
        }

    }
}
