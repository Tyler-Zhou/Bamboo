using System;
using Microsoft.Practices.CompositeUI;

namespace ICP.FCM.OceanImport.UI
{
    public partial class OIBusinessDownloadTool : ICP.Framework.ClientComponents.UIFramework.BaseToolBar
    {
        public OIBusinessDownloadTool()
        {
            InitializeComponent();

            BulidCommond();
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
        #region 窗体加载
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if(!DesignMode)
            {
                InitControls();
            }
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        { 

        }

        #endregion

    }
}
