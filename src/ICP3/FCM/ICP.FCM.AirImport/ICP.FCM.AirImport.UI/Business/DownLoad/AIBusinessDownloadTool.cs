using Microsoft.Practices.CompositeUI;

namespace ICP.FCM.AirImport.UI
{
    public partial class OIBusinessDownloadTool : ICP.Framework.ClientComponents.UIFramework.BaseToolBar
    {
        public OIBusinessDownloadTool()
        {
            InitializeComponent();

            BulidCommond();
            this.Disposed += delegate {
                if (this.Workitem != null)
                {
                    this.Workitem.Items.Remove(this);
                    this.Workitem = null;
                }
            
            };
        }

        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion
        
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Workitem.Commands[OIBusinessDownLoadCommandConstants.Command_ShowSearch].Execute();
        }


        /// <summary>
        /// 注册命令
        /// </summary>
        private void BulidCommond()
        {
            barSearch.ItemClick += delegate { Workitem.Commands[OIBusinessDownLoadCommandConstants.Command_ShowSearch].Execute(); };


            barRefresh.ItemClick += delegate { Workitem.Commands[OIBusinessDownLoadCommandConstants.Command_SearchDate].Execute(); };


            barDownLoad.ItemClick += delegate { Workitem.Commands[OIBusinessDownLoadCommandConstants.Command_DownLoad].Execute(); };

            barClose.ItemClick += delegate { this.FindForm().Close(); };
        }
    

    }
}
