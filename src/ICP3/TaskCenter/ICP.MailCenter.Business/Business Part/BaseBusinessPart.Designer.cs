using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ICP.Common.Business.ServiceInterface
{
   partial class BaseBusinessPart
   {
       /// <summary> 
       /// 必需的设计器变量。
       /// </summary>
       private System.ComponentModel.IContainer components = null;

       /// <summary> 
       /// 清理所有正在使用的资源。
       /// </summary>
       /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
       protected override void Dispose(bool disposing)
       {
           if (disposing && (components != null))
           {
               components.Dispose();
           }
           base.Dispose(disposing);
       }
       private void InitializeComponent()
       {
           this.components = new System.ComponentModel.Container();
           this.barManager = new DevExpress.XtraBars.BarManager(this.components);
           this.barTool = new DevExpress.XtraBars.Bar();
           this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
           this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
           this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
           this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
           this.gridControlList = new ICP.Framework.ClientComponents.Controls.LWGridControl();
           this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
           this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
           this.gridViewList = new ICP.Framework.ClientComponents.Controls.LWGridView();
           this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
           this.tabPage1 = new System.Windows.Forms.TabPage();
           this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
           ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.gridControlList)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.gridViewList)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
           this.splitContainerControl1.SuspendLayout();
           ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
           this.SuspendLayout();
           // 
           // barManager
           // 
           this.barManager.AllowCustomization = false;
           this.barManager.AllowMoveBarOnToolbar = false;
           this.barManager.AllowQuickCustomization = false;
           this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barTool});
           this.barManager.DockControls.Add(this.barDockControlTop);
           this.barManager.DockControls.Add(this.barDockControlBottom);
           this.barManager.DockControls.Add(this.barDockControlLeft);
           this.barManager.DockControls.Add(this.barDockControlRight);
           this.barManager.Form = this;
           this.barManager.MainMenu = this.barTool;
           this.barManager.MaxItemId = 0;
           // 
           // barTool
           // 
           this.barTool.BarName = "Main menu";
           this.barTool.DockCol = 0;
           this.barTool.DockRow = 0;
           this.barTool.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
           this.barTool.OptionsBar.AllowQuickCustomization = false;
           this.barTool.OptionsBar.DisableCustomization = true;
           this.barTool.OptionsBar.DrawDragBorder = false;
           this.barTool.OptionsBar.MultiLine = true;
           this.barTool.OptionsBar.UseWholeRow = true;
           this.barTool.Text = "Main menu";
           // 
           // barDockControlTop
           // 
           this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
           this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
           this.barDockControlTop.Size = new System.Drawing.Size(923, 24);
           // 
           // barDockControlBottom
           // 
           this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
           this.barDockControlBottom.Location = new System.Drawing.Point(0, 575);
           this.barDockControlBottom.Size = new System.Drawing.Size(923, 0);
           // 
           // barDockControlLeft
           // 
           this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
           this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
           this.barDockControlLeft.Size = new System.Drawing.Size(0, 551);
           // 
           // barDockControlRight
           // 
           this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
           this.barDockControlRight.Location = new System.Drawing.Point(923, 24);
           this.barDockControlRight.Size = new System.Drawing.Size(0, 551);
           // 
           // gridControlList
           // 
           this.gridControlList.AllowDrop = true;
           this.gridControlList.ContextMenuStrip = this.contextMenuStrip;
           this.gridControlList.DataSource = this.bindingSource;
           this.gridControlList.Dock = System.Windows.Forms.DockStyle.Fill;
           this.gridControlList.Location = new System.Drawing.Point(0, 0);
           this.gridControlList.MainView = this.gridViewList;
           this.gridControlList.MenuManager = this.barManager;
           this.gridControlList.Name = "gridControlList";
           this.gridControlList.Size = new System.Drawing.Size(923, 277);
           this.gridControlList.TabIndex = 4;
           this.gridControlList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewList});
           this.gridControlList.DragDrop += new System.Windows.Forms.DragEventHandler(this.gridControlList_DragDrop);
           this.gridControlList.DragEnter += new System.Windows.Forms.DragEventHandler(this.gridControlList_DragEnter);
           // 
           // contextMenuStrip
           // 
           this.contextMenuStrip.Name = "contextMenuStrip";
           this.contextMenuStrip.Size = new System.Drawing.Size(61, 4);
           // 
           // gridViewList
           // 
           this.gridViewList.GridControl = this.gridControlList;
           this.gridViewList.Name = "gridViewList";
           this.gridViewList.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
           this.gridViewList.OptionsCustomization.AllowRowSizing = true;
           this.gridViewList.OptionsNavigation.EnterMoveNextColumn = true;
           this.gridViewList.OptionsSelection.MultiSelect = true;
           this.gridViewList.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
           this.gridViewList.OptionsView.ColumnAutoWidth = false;
           this.gridViewList.OptionsView.RowAutoHeight = true;
           this.gridViewList.OptionsView.ShowGroupPanel = false;
           this.gridViewList.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewList_FocusedRowChanged);
           this.gridViewList.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewList_CellValueChanged);
           this.gridViewList.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.OnColumnWidthChanged);
           this.gridViewList.ColumnPositionChanged += new System.EventHandler(this.OnColumnPositionChanged);
           this.gridViewList.ShowGridMenu += new DevExpress.XtraGrid.Views.Grid.GridMenuEventHandler(this.gridViewList_ShowGridMenu);
           this.gridViewList.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridViewList_RowStyle);
           // 
           // splitContainerControl1
           // 
           this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
           this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
           this.splitContainerControl1.Horizontal = false;
           this.splitContainerControl1.Location = new System.Drawing.Point(0, 24);
           this.splitContainerControl1.Name = "splitContainerControl1";
           this.splitContainerControl1.Panel1.Controls.Add(this.gridControlList);
           this.splitContainerControl1.Panel1.Text = "Panel1";
           this.splitContainerControl1.Panel2.Controls.Add(this.xtraTabControl);
           this.splitContainerControl1.Panel2.Text = "Panel2";
           this.splitContainerControl1.Size = new System.Drawing.Size(923, 551);
           this.splitContainerControl1.SplitterPosition = 277;
           this.splitContainerControl1.TabIndex = 9;
           this.splitContainerControl1.Text = "splitContainerControl1";
           this.splitContainerControl1.SplitterPositionChanged += new System.EventHandler(this.splitContainerControl1_SplitterPositionChanged);
           // 
           // tabPage1
           // 
           this.tabPage1.Location = new System.Drawing.Point(4, 23);
           this.tabPage1.Name = "tabPage1";
           this.tabPage1.Size = new System.Drawing.Size(915, 242);
           this.tabPage1.TabIndex = 0;
           this.tabPage1.Text = "tabPage1";
           this.tabPage1.UseVisualStyleBackColor = true;
           // 
           // xtraTabControl
           // 
           this.xtraTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
           this.xtraTabControl.Location = new System.Drawing.Point(0, 0);
           this.xtraTabControl.Name = "xtraTabControl";
           this.xtraTabControl.Size = new System.Drawing.Size(923, 268);
           this.xtraTabControl.TabIndex = 0;
           // 
           // BaseBusinessPart
           // 
           this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
           this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
           this.Controls.Add(this.splitContainerControl1);
           this.Controls.Add(this.barDockControlLeft);
           this.Controls.Add(this.barDockControlRight);
           this.Controls.Add(this.barDockControlBottom);
           this.Controls.Add(this.barDockControlTop);
           this.Name = "BaseBusinessPart";
           this.Size = new System.Drawing.Size(923, 575);
           ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.gridControlList)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.gridViewList)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
           this.splitContainerControl1.ResumeLayout(false);
           ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
           this.ResumeLayout(false);

       }

     


  
    




    



    
       internal DevExpress.XtraBars.BarManager barManager;
       private ICP.Framework.ClientComponents.Controls.LWGridControl gridControlList;
       private ICP.Framework.ClientComponents.Controls.LWGridView gridViewList;
       public  System.Windows.Forms.BindingSource bindingSource;
       private DevExpress.XtraBars.BarDockControl barDockControlTop;
       private DevExpress.XtraBars.BarDockControl barDockControlBottom;
       private DevExpress.XtraBars.BarDockControl barDockControlLeft;
       private DevExpress.XtraBars.BarDockControl barDockControlRight;
       private DevExpress.XtraBars.Bar barTool;
       private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
       private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
       private System.Windows.Forms.TabPage tabPage1;
       private DevExpress.XtraTab.XtraTabControl xtraTabControl;
   }
}
