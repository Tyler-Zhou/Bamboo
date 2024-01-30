#region Comment

/*
 * 
 * FileName:    InquireRatesHistoryToolBar.cs
 * CreatedOn:   
 * CreatedBy:   
 * 
 * 
 * Description：
 *      ->海运询价历史-工具栏
 * History：
 *      ->
 * 
 * 
 * 
 */

#endregion

using System;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraBars;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.Utility;

namespace ICP.FRM.UI.InquireRates
{
    /// <summary>
    /// 海运询价历史-工具栏
    /// </summary>
    [ToolboxItem(false)]
    public partial class InquireRatesHistoryToolBar : BaseEditPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region 构造函数
        public InquireRatesHistoryToolBar()
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
            BulidCommand();
            if (!DesignMode) { InitMessage(); }
        } 
        #endregion

        #region 委托事件
        /// <summary>
        /// 历史数据
        /// </summary>
        object historyData = new object();
        /// <summary>
        /// 询价历史记录--替换
        /// </summary>
        [EventPublication(InquireRatesCommandConstants.Command_HistoryReplace)]
        public event EventHandler<DataEventArgs<object>> ReplaceEvent;
        /// <summary>
        /// 询价历史记录--复制
        /// </summary>
        [EventPublication(InquireRatesCommandConstants.Command_HistoryCopy)]
        public event EventHandler<DataEventArgs<object>> CopyEvent; 
        #endregion

        #region Override
        /// <summary>
        /// 数据源
        /// </summary>
        public override object DataSource
        {
            get
            {
                return null;
            }
            set
            {
                //绑定数据
                BindingData(value);
            }
        } 
        #endregion

        #region 窗体事件
        /// <summary>
        /// 替换
        /// </summary>
        void barReplace_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ReplaceEvent != null)
                ReplaceEvent(this, new DataEventArgs<object>(historyData));
        }
        /// <summary>
        /// 复制
        /// </summary>
        private void barCopy_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (CopyEvent != null)
            {
                CopyEvent(this, new DataEventArgs<object>(historyData));
            }
        } 
        #endregion

        #region 方法定义
        /// <summary>
        /// 注册Message
        /// </summary>
        private void InitMessage()
        {
            RegisterMessage("Invalidate", "&Invalidate");
            RegisterMessage("Resume", "Resume(&I)");
            RegisterMessage("Publish", "&Publish");
            RegisterMessage("Pause", "&Pause");
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="value">数据对象</param>
        private void BindingData(object value)
        {
            historyData = value;
        }

        /// <summary>
        /// 构建按钮Command事件
        /// </summary>
        private void BulidCommand()
        {
            barReplace.ItemClick += barReplace_ItemClick;
            barCopy.ItemClick += barCopy_ItemClick;
        }
        #endregion
    }
}
