
//-----------------------------------------------------------------------
// <copyright file="ShellToolBar.cs" company="ICP">
//     Copyright (c) ICP. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.WorkFlowDesigner
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using DevExpress.XtraBars;
    using ICP.Framework.ClientComponents.UIFramework;
    using Microsoft.Practices.CompositeUI;
    using DevExpress.XtraEditors;

    /// <summary>
    /// 工具栏
    /// </summary>
    public partial class WorkFlowMainTool : BaseToolBar
    {
        #region 服务

        [ServiceDependency]
        public WorkItem WorkItem { get; set; }

        #endregion

        #region 初始化

        public WorkFlowMainTool()
        {
            this.InitializeComponent();
            this.Disposed += delegate {
                this.ComBoxValueChanged = null;
                this.CmbZoomLevel.SelectedValueChanged -= this.CmbZoomLevel_SelectedValueChanged;
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
        
            //初始化变焦下拉框的内容
            CmbZoomLevel.Items.Clear();
            
            CmbZoomLevel.Items.Add("50%");
            CmbZoomLevel.Items.Add("75%");
            CmbZoomLevel.Items.Add("100%");
            CmbZoomLevel.Items.Add("150%");
            CmbZoomLevel.Items.Add("200%");
            CmbZoomLevel.Items.Add("300%");
            CmbZoomLevel.Items.Add("400%");
        
        }

        //注册命令
        private void RegisterCommands()
        {
            this.WorkItem.Commands[CommandConstants.Command_Flow_New].AddInvoker(barNew, "ItemClick");
            this.WorkItem.Commands[CommandConstants.Command_Flow_OpenLocal].AddInvoker(barOpenLocal, "ItemClick");
            this.WorkItem.Commands[CommandConstants.Command_Flow_OpenServer].AddInvoker(barOpenServer, "ItemClick");
            this.WorkItem.Commands[CommandConstants.Command_Flow_SaveLocal].AddInvoker(barSaveLocal, "ItemClick");
            this.WorkItem.Commands[CommandConstants.Command_Flow_SaveServer].AddInvoker(barSaveServer, "ItemClick");
            this.WorkItem.Commands[CommandConstants.Command_Flow_SaveAS].AddInvoker(barSaveAs, "ItemClick");

            this.WorkItem.Commands[CommandConstants.Command_Flow_PrintSet].AddInvoker(barPrintSet, "ItemClick");
            this.WorkItem.Commands[CommandConstants.Command_Flow_Print].AddInvoker(barPrint, "ItemClick");
            this.WorkItem.Commands[CommandConstants.Command_Flow_PrintPreview].AddInvoker(barPrintPreview, "ItemClick");


            this.WorkItem.Commands[CommandConstants.Command_Flow_ZoomIn].AddInvoker(barZoomIn,"ItemClick");
            this.WorkItem.Commands[CommandConstants.Command_Flow_ZoomOut].AddInvoker(barZoomOut,"ItemClick");
            this.WorkItem.Commands[CommandConstants.Command_Flow_CancelZaom].AddInvoker(barCancelZaom,"ItemClick");

            this.WorkItem.Commands[CommandConstants.Command_Flow_Expand].AddInvoker(barExpand,"ItemClick");
            this.WorkItem.Commands[CommandConstants.Command_Flow_Collapse].AddInvoker(barCollapse,"ItemClick");

            this.WorkItem.Commands[CommandConstants.Command_Flow_Paster].AddInvoker(barPaste, "ItemClick");
            this.WorkItem.Commands[CommandConstants.Command_Flow_Remove].AddInvoker(barDelete, "ItemClick");
            this.WorkItem.Commands[CommandConstants.Command_Flow_Cut].AddInvoker(barCut, "ItemClick");
            this.WorkItem.Commands[CommandConstants.Command_Flow_Copy].AddInvoker(barCopy, "ItemClick");

           // this.WorkItem.Commands[CommandConstants.Command_Flow_Close].AddInvoker(barClose, "ItemClick");

            this.barClose.ItemClick += new ItemClickEventHandler(barClose_ItemClick);

            //this.WorkItem.Commands[CommandConstants.Command_Flow_ZoomLevelSelectedValueChanged].AddInvoker(CmbZoomLevel, "SelectedValueChanged");


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
            foreach (LinkPersistInfo item in bar2.LinksPersistInfo)
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
            foreach (BarItemLink item in bar2.ItemLinks)
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
            foreach (BarItemLink item in bar2.ItemLinks)
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
            foreach (BarItemLink item in bar2.ItemLinks)
            {
                if (item.Item.Name.Equals(name))
                {
                    item.Item.Visibility = visible ? BarItemVisibility.Always : BarItemVisibility.Never;

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
        /// <param name="result">可操作性逻辑</param>
        public override void SetVisible(string name, ConditionResultHandler result)
        {

        }

        #endregion

        private void CmbZoomLevel_SelectedValueChanged(object sender, EventArgs e)
        {
            if (ComBoxValueChanged != null)
            {
                ValueChangedEventArgs arg = null;
                ComboBoxEdit comboBoxEdit = sender as ComboBoxEdit;
                if (comboBoxEdit.EditValue != null)
                {
                    arg = new ValueChangedEventArgs(comboBoxEdit.EditValue.ToString());
                }
                else 
                {
                    arg = new ValueChangedEventArgs("");
                }   

                ComBoxValueChanged(sender, arg);
            }
        }

        [Microsoft.Practices.CompositeUI.EventBroker.EventPublication("CmbZoomLevel_SelectValuCharge")]
        public event EventHandler<ValueChangedEventArgs> ComBoxValueChanged;
        
    }

    public delegate void  ValueChangedEventHandler(object sender,ValueChangedEventArgs args);

    public class ValueChangedEventArgs : EventArgs
    {
        string value;

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public ValueChangedEventArgs(string val)
        {
            value = val;
        }
    }

}
