using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraBars;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;

namespace ICP.FRM.UI.SearchRate
{
    /// <summary>
    /// 查询运价工具栏
    /// </summary>
    [ToolboxItem(false)]
    public partial class SearchOceanToolPart : BaseToolBar
    {
        /// <summary>
        /// 查询运价工具栏
        /// </summary>
        public SearchOceanToolPart()
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
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        #endregion

        /// <summary>
        /// 加载初始化
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                barSearch.ItemClick += delegate { Workitem.Commands[SearchOceanCommandConstants.Command_ShowSearch].Execute(); };
                barRefresh.ItemClick += delegate { Workitem.Commands[SearchOceanCommandConstants.Command_RefreshData].Execute(); };
                barExPortToExcel.ItemClick += delegate { Workitem.Commands[SearchOceanCommandConstants.Command_ExportToExcel].Execute(); };
                barUpgradeCloud.ItemClick += delegate { Workitem.Commands[SearchOceanCommandConstants.Command_UpgradeCloud].Execute(); };
                barClose.ItemClick += delegate { Closed(); };

                if (!LocalCommonServices.PermissionService.HaveActionPermission(PermissionCommandConstants.SEARCHOCEAN_EXPORTTOEXCEL))
                {
                    barExPortToExcel.Visibility = BarItemVisibility.Never;
                }
            }
        }
        /// <summary>
        /// 关闭
        /// </summary>
        private void Closed()
        {
            Form frm = FindForm();
            if (frm != null) frm.Close();
        }
    }
}
