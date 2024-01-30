
//-----------------------------------------------------------------------
// <copyright file="ShellToolBar.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.FormDesigner
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using DevExpress.XtraBars;
    using ICP.Framework.ClientComponents.UIFramework;
    using ICP.WF.FormDesigner.Common;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// 工具栏
    /// </summary>
    public partial class ShellToolBarPart : BaseToolBar
    {
        #region 服务

        [ServiceDependency]
        public WorkItem WorkItem { get; set; }

        #endregion

        #region 初始化

        public ShellToolBarPart()
        {
            this.InitializeComponent();
            this.Disposed += delegate {
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            this.RegisterCommands();

            this.InitControls();
        }

        //初始化控件
        private void InitControls()
        {
            //从全局资源中取工具栏图标
            barClose.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Close_16;
        }

        //注册命令
        private void RegisterCommands()
        {
            this.WorkItem.Commands[CommandConstants.Command_Form_New].AddInvoker(barNew, "ItemClick");
            this.WorkItem.Commands[CommandConstants.Command_Form_OpenLocal].AddInvoker(barOpenLocal, "ItemClick");
            this.WorkItem.Commands[CommandConstants.Command_Form_OpenServer].AddInvoker(barOpenServer, "ItemClick");
            this.WorkItem.Commands[CommandConstants.Command_Form_SaveLocal].AddInvoker(barSaveLocal, "ItemClick");
            this.WorkItem.Commands[CommandConstants.Command_Form_SaveServer].AddInvoker(barSaveServer, "ItemClick");

            this.WorkItem.Commands[CommandConstants.Command_Form_UnDo].AddInvoker(barUnDo, "ItemClick");
            this.WorkItem.Commands[CommandConstants.Command_Form_ReDo].AddInvoker(barReDo, "ItemClick");
            this.WorkItem.Commands[CommandConstants.Command_Form_TabOrder].AddInvoker(barTabOrder, "ItemClick");

            this.WorkItem.Commands[CommandConstants.Command_Form_Paster].AddInvoker(barPaste, "ItemClick");
            this.WorkItem.Commands[CommandConstants.Command_Form_Remove].AddInvoker(barDelete, "ItemClick");
            this.WorkItem.Commands[CommandConstants.Command_Form_Cut].AddInvoker(barCut, "ItemClick");
            this.WorkItem.Commands[CommandConstants.Command_Form_Copy].AddInvoker(barCopy, "ItemClick");

            this.WorkItem.Commands[CommandConstants.Command_Form_Left].AddInvoker(barLeft, "ItemClick");
            this.WorkItem.Commands[CommandConstants.Command_Form_Right].AddInvoker(barRight, "ItemClick");
            this.WorkItem.Commands[CommandConstants.Command_Form_Center].AddInvoker(barCenter, "ItemClick");
            this.WorkItem.Commands[CommandConstants.Command_Form_Bottom].AddInvoker(barBottom, "ItemClick");
            this.WorkItem.Commands[CommandConstants.Command_Form_Top].AddInvoker(barTop, "ItemClick");
            this.WorkItem.Commands[CommandConstants.Command_Form_Middle].AddInvoker(barMiddle, "ItemClick");

            this.WorkItem.Commands[CommandConstants.Command_Form_ViewCode].AddInvoker(barViewCode, "ItemClick");
           // this.WorkItem.Commands[CommandConstants.Command_Form_Close].AddInvoker(barClose, "ItemClick");
            this.barClose.ItemClick += new ItemClickEventHandler(barClose_ItemClick);
        }

        void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.FindForm().Close();
        }
  
        #endregion

        #region IToolBar 接口

        /// <summary>
        /// 获取工具栏上的值列表(列入下拉工具栏按钮,文本框按钮)
        /// <remarks>
        /// 只有场景中需要该接口方法取值时,才需要实现该接口方法.
        /// 如果没用到该接口方法,为了代码的清洁可以清除.
        /// </remarks>
        /// </summary>
        /// <returns></returns>
        public override IDictionary<string, object> GetValues()
        {
            Dictionary<string, object> results = new Dictionary<string, object>();
            return results;
        }

        /// <summary>
        /// 从外界向工具栏初始化值
        /// <remarks>
        /// 只有场景中需要该接口方法取值时,才需要实现该接口方法.
        /// 如果没用到该接口方法,为了代码的清洁可以清除.
        /// </remarks>
        /// </summary>
        /// <param name="values">初始化值字典</param>
        public override void Init(IDictionary<string, object> values)
        {
        }

        /// <summary>
        /// 在指定位置添加工具栏按钮
        /// <remarks>
        /// 只有场景中需要该接口方法取值时,才需要实现该接口方法.
        /// 如果没用到该接口方法,为了代码的清洁可以清除.
        /// </remarks>
        /// </summary>
        /// <param name="index">位置</param>
        /// <param name="name">名称</param>
        /// <param name="text">标题</param>
        /// <param name="image">图标</param>
        /// <param name="handler">工具栏的处理逻辑</param>
        public override void InsertItem(int index, string name, string text, Image image, EventHandler handler)
        {
        }

        /// <summary>
        /// 设置工具栏按钮的可操作性
        /// <remarks>
        /// 只有场景中需要该接口方法取值时,才需要实现该接口方法.
        /// 如果没用到该接口方法,为了代码的清洁可以清除.
        /// </remarks>
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="enable">是否可操作</param>
        public override void SetEnable(string name, bool enable)
        {
            barFormDesignToolBarManager.Items[name].Enabled = enable;
        }

        /// <summary>
        /// 设置工具栏按钮的可操作性
        /// <remarks>
        /// 只有场景中需要该接口方法取值时,才需要实现该接口方法.
        /// 如果没用到该接口方法,为了代码的清洁可以清除.
        /// </remarks>
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="result">是否可操作逻辑</param>
        public override void SetEnable(string name, ConditionResultHandler result)
        {
        }

        /// <summary>
        /// 设置工具栏按钮的图标
        /// <remarks>
        /// 只有场景中需要该接口方法取值时,才需要实现该接口方法.
        /// 如果没用到该接口方法,为了代码的清洁可以清除.
        /// </remarks>
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="image">图标</param>
        public override void SetImage(string name, Image image)
        {
            barFormDesignToolBarManager.Items[name].Glyph = image;
        }

        /// <summary>
        /// 设置工具栏按钮的标题
        /// <remarks>
        /// 只有场景中需要该接口方法取值时,才需要实现该接口方法.
        /// 如果没用到该接口方法,为了代码的清洁可以清除.
        /// </remarks>
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="text">标题</param>
        public override void SetText(string name, string text)
        {
            barFormDesignToolBarManager.Items[name].Caption = text;
        }

        /// <summary>
        /// 设置工具栏按钮的可操作性
        /// <remarks>
        /// 只有场景中需要该接口方法取值时,才需要实现该接口方法.
        /// 如果没用到该接口方法,为了代码的清洁可以清除.
        /// </remarks>
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="visible">可操作性</param>
        public override void SetVisible(string name, bool visible)
        {
            barFormDesignToolBarManager.Items[name].Visibility = visible? BarItemVisibility.Always: BarItemVisibility.Never;
        }

        /// <summary>
        /// 设置工具栏按钮的可操作性
        /// <remarks>
        /// 只有场景中需要该接口方法取值时,才需要实现该接口方法.
        /// 如果没用到该接口方法,为了代码的清洁可以清除.
        /// </remarks>
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="result">可操作性逻辑</param>
        public override void SetVisible(string name, ConditionResultHandler result)
        {
        }


       
        #endregion



    }
}
