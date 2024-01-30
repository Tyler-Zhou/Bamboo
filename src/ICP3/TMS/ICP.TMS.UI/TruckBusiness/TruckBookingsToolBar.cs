using System.Collections.Generic;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraBars;

namespace ICP.TMS.UI
{
    [ToolboxItem(false)]
    public partial class TruckBookingsToolBar : BaseToolBar
    {
        public TruckBookingsToolBar()
        {
            InitializeComponent();
            BulidBarItemDictionary();
            this.Disposed += delegate
            {
                this._barItemDic.Clear();
                this._barItemDic = null;
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

        #region 注册按钮
        Dictionary<string, BarItem> _barItemDic = new Dictionary<string, BarItem>();


        private void BulidBarItemDictionary()
        {
            foreach (BarItem item in barManager1.Items)
            {
                _barItemDic.Add(item.Name, item);
            }
        }
        #endregion


        #region IToolBar成员

        public override void SetEnable(string name, bool enable)
        {
            if (_barItemDic.ContainsKey(name) && _barItemDic[name] != null)
                _barItemDic[name].Enabled = enable;
        }

        public override void SetVisible(string name, bool visible)
        {
            if (_barItemDic.ContainsKey(name) && _barItemDic[name] != null)
                _barItemDic[name].Visibility = visible ? BarItemVisibility.Always : BarItemVisibility.Never;
        }

        public override void SetText(string name, string text)
        {
            if (_barItemDic.ContainsKey(name) && _barItemDic[name] != null)
                _barItemDic[name].Caption = text;
        }

        #endregion

        #region 按钮点击
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Workitem.Commands[TruckBookingsCommandConstants.Command_Add].Execute(); 
        }
        /// <summary>
        /// 派车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barDispatch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Workitem.Commands[TruckBookingsCommandConstants.Command_Truck].Execute(); 
        }
        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barDownLoad_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Workitem.Commands[TruckBookingsCommandConstants.Command_DownLoadBusiness].Execute();
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Workitem.Commands[TruckBookingsCommandConstants.Command_Edit].Execute();
        }
        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barVoid_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Workitem.Commands[TruckBookingsCommandConstants.Command_Cancel].Execute();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Workitem.Commands[TruckBookingsCommandConstants.Command_Delete].Execute();
        }
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Workitem.Commands[TruckBookingsCommandConstants.Command_Print].Execute();
        }
        /// <summary>
        /// 账单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBill_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Workitem.Commands[TruckBookingsCommandConstants.Command_Bill].Execute();
        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Workitem.Commands[TruckBookingsCommandConstants.Command_Refresh].Execute();
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.FindForm().Close();
        }
        /// <summary>
        /// 显示查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barShowSearch_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[TruckBookingsCommandConstants.Command_ShowSearch].Execute();
        }
        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barCopy_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[TruckBookingsCommandConstants.Command_Copy].Execute();
        }
        #endregion

  

    


    }
}
