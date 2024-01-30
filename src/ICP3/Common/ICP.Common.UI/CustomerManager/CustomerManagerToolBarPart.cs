

//-----------------------------------------------------------------------
// <copyright file="CustomerManagerToolBarPart.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.Common.UI.CustomerManager
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using ICP.Framework.ClientComponents.UIFramework;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI;
    using ICP.Framework.CommonLibrary.Attributes;
    using DevExpress.XtraBars;
    using Microsoft.Practices.CompositeUI.Commands;

    /// <summary>
    /// 客户管理-工具栏面版
    /// </summary>
    [System.ComponentModel.ToolboxItem(false)]
    [SmartPart]
    internal class CustomerManagerToolBarPart : BaseToolBar
    {
        #region 服务

        [ServiceDependency]
        public WorkItem WorkItem { get; set; }
        
        #endregion

        #region 初始化

        public CustomerManagerToolBarPart()
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
            base.OnLoad(e);

            this.RegisterCommands();
            
            this.InitControls();
        }

        //初始化控件
        private void InitControls()
        {
            //从全局资源中取工具栏图标
            barAdd.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Add_16;
            barEdit.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Edit_16;
            barDisuse.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Disuse_16;
            barApplyCode.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.UP_16;
            barSetDangerous.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.UP_16;
            barClose.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Close_16;           
        }

        //注册命令
        private void RegisterCommands()
        {
            this.WorkItem.Commands[CustomerManagerConstants.CMD_AddCustomer].AddInvoker(barAdd, "ItemClick");
            this.WorkItem.Commands[CustomerManagerConstants.CMD_EditCustomer].AddInvoker(barEdit, "ItemClick");
            this.WorkItem.Commands[CustomerManagerConstants.CMD_ApplyCode].AddInvoker(barApplyCode, "ItemClick");
            this.WorkItem.Commands[CustomerManagerConstants.CMD_SetDangerous].AddInvoker(barSetDangerous, "ItemClick");
            this.WorkItem.Commands[CustomerManagerConstants.CMD_Disable].AddInvoker(barDisuse, "ItemClick");
           // this.WorkItem.Commands[CustomerManagerConstants.CMD_Close].AddInvoker(barClose, "ItemClick");
            this.barClose.ItemClick += new ItemClickEventHandler(barClose_ItemClick);
        }

        void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            var findForm = this.FindForm();
            if (findForm != null) findForm.Close();
        }

        #endregion
    
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CustomerManagerConstants.CMD_Close)]
        public void CMD_Close(object s, EventArgs e)
        {
            this.FindForm().Close();
        }

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
            foreach (BarItemLink item in bar1.ItemLinks)
            {
                if (item.Item.Name.Equals(name))
                {
                    item.Item.Enabled = enable;

                    break;
                }
            }
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
            foreach (BarItemLink item in bar1.ItemLinks)
            {
                if (item.Item.Name.Equals(name))
                {
                    item.Item.Glyph = image;

                    break;
                }
            }
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
            foreach (BarItemLink item in bar1.ItemLinks)
            {
                if (item.Item.Name.Equals(name))
                {
                    item.Item.Caption = text;

                    break;
                }
            }
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
            foreach (BarItemLink item in bar1.ItemLinks)
            {
                if (item.Item.Name.Equals(name))
                {
                    item.Item.Visibility = visible ? BarItemVisibility.Always : BarItemVisibility.Never;
                    
                    break;
                }
            }
        }

        private DevExpress.XtraBars.BarManager barToolBar;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barAdd;
        private DevExpress.XtraBars.BarButtonItem barDisuse;
        private DevExpress.XtraBars.BarButtonItem barApplyCode;
        private DevExpress.XtraBars.BarButtonItem barSetDangerous;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private BarButtonItem barEdit;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;

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

        #region 设计器自动生成代码

        private System.ComponentModel.IContainer components;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.barToolBar = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barDisuse = new DevExpress.XtraBars.BarButtonItem();
            this.barApplyCode = new DevExpress.XtraBars.BarButtonItem();
            this.barSetDangerous = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.barToolBar)).BeginInit();
            this.SuspendLayout();
            // 
            // barToolBar
            // 
            this.barToolBar.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barToolBar.DockControls.Add(this.barDockControlTop);
            this.barToolBar.DockControls.Add(this.barDockControlBottom);
            this.barToolBar.DockControls.Add(this.barDockControlLeft);
            this.barToolBar.DockControls.Add(this.barDockControlRight);
            this.barToolBar.Form = this;
            this.barToolBar.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barAdd,
            this.barDisuse,
            this.barApplyCode,
            this.barSetDangerous,
            this.barClose,
            this.barEdit});
            this.barToolBar.MaxItemId = 6;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barAdd),
            new DevExpress.XtraBars.LinkPersistInfo(this.barEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.barDisuse),
            new DevExpress.XtraBars.LinkPersistInfo(this.barApplyCode, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSetDangerous),
            new DevExpress.XtraBars.LinkPersistInfo(this.barClose, true)});
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // barAdd
            // 
            this.barAdd.Caption = "新增(&A)";
            this.barAdd.Id = 0;
            this.barAdd.Name = "barAdd";
            this.barAdd.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barEdit
            // 
            this.barEdit.Caption = "编辑(&E)";
            this.barEdit.Id = 5;
            this.barEdit.Name = "barEdit";
            this.barEdit.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barDisuse
            // 
            this.barDisuse.Caption = "作废(&D)";
            this.barDisuse.Id = 1;
            this.barDisuse.Name = "barDisuse";
            this.barDisuse.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barApplyCode
            // 
            this.barApplyCode.Caption = "申请代码(&S)";
            this.barApplyCode.Id = 2;
            this.barApplyCode.Name = "barApplyCode";
            this.barApplyCode.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barSetDangerous
            // 
            this.barSetDangerous.Caption = "设为危险客户(&S)";
            this.barSetDangerous.Id = 3;
            this.barSetDangerous.Name = "barSetDangerous";
            this.barSetDangerous.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barClose
            // 
            this.barClose.Caption = "关闭(&C)";
            this.barClose.Id = 4;
            this.barClose.Name = "barClose";
            this.barClose.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(413, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 26);
            this.barDockControlBottom.Size = new System.Drawing.Size(413, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 0);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(413, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 0);
            // 
            // CustomerManagerToolBarPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "CustomerManagerToolBarPart";
            this.Size = new System.Drawing.Size(413, 26);
            ((System.ComponentModel.ISupportInitialize)(this.barToolBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }


    /// <summary>
    /// 客户管理-操作模式
    /// </summary>
    internal enum CustomerManagerMode
    {
        /// <summary>
        /// 一般
        /// </summary>
        [MemberDescription("一般")]
        Normal,

        /// <summary>
        /// 合并
        /// </summary>
        [MemberDescription("合并")]
        Merged
    }
}
