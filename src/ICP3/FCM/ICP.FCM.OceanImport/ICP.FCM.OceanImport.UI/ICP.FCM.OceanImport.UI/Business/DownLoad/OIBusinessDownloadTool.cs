using System;
using Microsoft.Practices.CompositeUI;
using ICP.DataCache.ServiceInterface;
using System.Collections.Generic;
using DevExpress.XtraBars;
using ICP.Framework.CommonLibrary.Client;
using DevExpress.Utils;

namespace ICP.FCM.OceanImport.UI
{
    public partial class OIBusinessDownloadTool : ICP.Framework.ClientComponents.UIFramework.BaseToolBar
    {
        public OIBusinessDownloadTool()
        {
            InitializeComponent();

            BulidCommond();
            BulidBarItemDictionary();
            this.Disposed += delegate
           {
               this._barItemDic.Clear();
               this._barItemDic = null;

               if (Workitem != null)
               {
                   Workitem.Items.Remove(this);
                   Workitem = null;
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

            this.toolAccepted.ItemClick += delegate { Workitem.Commands[OIBusinessDownLoadCommandConstants.Command_AcceptDocument].Execute(); };
            this.toolUnAccepted.ItemClick += delegate { Workitem.Commands[OIBusinessDownLoadCommandConstants.Command_UnAcceptDocument].Execute(); };
            this.toolTransition.ItemClick += delegate { Workitem.Commands[OIBusinessDownLoadCommandConstants.Command_Transition].Execute(); };
            this.toolAssignTo.ItemClick += delegate { Workitem.Commands[OIBusinessDownLoadCommandConstants.Command_AssignTo].Execute(); };
            this.toolPrint.ItemClick += delegate { Workitem.Commands[OIBusinessDownLoadCommandConstants.Command_PrintAll].Execute(); };
            this.barDownLoadNew.ItemClick += delegate { Workitem.Commands[OIBusinessDownLoadCommandConstants.Command_DownLoadNew].Execute(); };
            this.barAcceptedNew.ItemClick += delegate { Workitem.Commands[OIBusinessDownLoadCommandConstants.Command_AcceptedNew].Execute(); };

            //test accepted document. joe
            this.toolTestAccepted.ItemClick += delegate { Workitem.Commands[OIBusinessDownLoadCommandConstants.Command_AcceptDocument].Execute(); };

        }
        #region 窗体加载
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();

            }
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            if (LocalData.IsEnglish)
            {
                toolAccepted.Caption = "Accept";
                toolAssignTo.Caption = "Assign To";
                toolPrint.Caption = "Print";
                barClose.Caption = "Close";
                toolUnAccepted.Caption = "UnAccepted";

                SuperToolTip superToolTip = new SuperToolTip();
                ToolTipTitleItem toolTipTitleItem = new ToolTipTitleItem();
                toolTipTitleItem.Text = "Only print documents have been received";
                superToolTip.Items.Add(toolTipTitleItem);
                toolPrint.SuperTip = superToolTip;
            }
            else
            {
                SuperToolTip superToolTip = new SuperToolTip();
                ToolTipTitleItem toolTipTitleItem = new ToolTipTitleItem();
                toolTipTitleItem.Text = "只能打印已签收的文件";
                superToolTip.Items.Add(toolTipTitleItem);
                toolPrint.SuperTip = superToolTip;
            }
        }

        private void BulidBarItemDictionary()
        {
            foreach (BarItem item in barManager1.Items)
            {
                _barItemDic.Add(item.Name, item);
            }
        }
        #endregion
        Dictionary<string, BarItem> _barItemDic = new Dictionary<string, BarItem>();
        public override void SetEnable(string name, bool enable)
        {
            if (_barItemDic.ContainsKey(name) && _barItemDic[name] != null)
                _barItemDic[name].Enabled = enable;
        }

    }
}
