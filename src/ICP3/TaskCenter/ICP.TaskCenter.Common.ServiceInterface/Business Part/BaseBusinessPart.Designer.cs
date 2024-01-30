using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.MailCenter.Business.ServiceInterface
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
           ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.gridControlList)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
           ((System.ComponentModel.ISupportInitialize)(this.gridViewList)).BeginInit();
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
           this.barTool.OptionsBar.MultiLine = true;
           this.barTool.OptionsBar.UseWholeRow = true;
           this.barTool.Text = "Main menu";
           // 
           // barDockControlTop
           // 
           this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
           this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
           this.barDockControlTop.Size = new System.Drawing.Size(534, 24);
           // 
           // barDockControlBottom
           // 
           this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
           this.barDockControlBottom.Location = new System.Drawing.Point(0, 314);
           this.barDockControlBottom.Size = new System.Drawing.Size(534, 0);
           // 
           // barDockControlLeft
           // 
           this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
           this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
           this.barDockControlLeft.Size = new System.Drawing.Size(0, 290);
           // 
           // barDockControlRight
           // 
           this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
           this.barDockControlRight.Location = new System.Drawing.Point(534, 24);
           this.barDockControlRight.Size = new System.Drawing.Size(0, 290);
           // 
           // gridControlList
           // 
           this.gridControlList.AllowDrop = true;
           this.gridControlList.ContextMenuStrip = this.contextMenuStrip;
           this.gridControlList.DataSource = this.bindingSource;
           this.gridControlList.Dock = System.Windows.Forms.DockStyle.Fill;
           this.gridControlList.Location = new System.Drawing.Point(0, 24);
           this.gridControlList.MainView = this.gridViewList;
           this.gridControlList.MenuManager = this.barManager;
           this.gridControlList.Name = "gridControlList";
           this.gridControlList.Size = new System.Drawing.Size(534, 290);
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
           this.gridViewList.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
           this.gridViewList.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.Blue;
           this.gridViewList.Appearance.FocusedRow.Options.UseBackColor = true;
           this.gridViewList.GridControl = this.gridControlList;
           this.gridViewList.IndicatorWidth = 40;
           this.gridViewList.Name = "gridViewList";
           this.gridViewList.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
           this.gridViewList.OptionsCustomization.AllowColumnMoving = false;
           this.gridViewList.OptionsCustomization.AllowQuickHideColumns = false;
           this.gridViewList.OptionsCustomization.AllowRowSizing = true;
           this.gridViewList.OptionsNavigation.EnterMoveNextColumn = true;
           this.gridViewList.OptionsSelection.MultiSelect = true;
           this.gridViewList.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
           this.gridViewList.OptionsView.ColumnAutoWidth = false;
           this.gridViewList.OptionsView.RowAutoHeight = true;
           this.gridViewList.OptionsView.ShowGroupPanel = false;
           this.gridViewList.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewList_FocusedRowChanged);
           this.gridViewList.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridViewList_CustomDrawRowIndicator);
           this.gridViewList.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.OnColumnWidthChanged);
           this.gridViewList.ColumnPositionChanged += new System.EventHandler(this.OnColumnPositionChanged);
           this.gridViewList.ShowGridMenu += new DevExpress.XtraGrid.Views.Grid.GridMenuEventHandler(this.gridViewList_ShowGridMenu);
           // 
           // BaseBusinessPart
           // 
           this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
           this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
           this.Controls.Add(this.gridControlList);
           this.Controls.Add(this.barDockControlLeft);
           this.Controls.Add(this.barDockControlRight);
           this.Controls.Add(this.barDockControlBottom);
           this.Controls.Add(this.barDockControlTop);
           this.Name = "BaseBusinessPart";
           this.Size = new System.Drawing.Size(534, 314);
           ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.gridControlList)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
           ((System.ComponentModel.ISupportInitialize)(this.gridViewList)).EndInit();
           this.ResumeLayout(false);

       }

  
    




    



    
       private DevExpress.XtraBars.BarManager barManager;
       private ICP.Framework.ClientComponents.Controls.LWGridControl gridControlList;
       private ICP.Framework.ClientComponents.Controls.LWGridView gridViewList;
       private System.Windows.Forms.BindingSource bindingSource;
       private DevExpress.XtraBars.BarDockControl barDockControlTop;
       private DevExpress.XtraBars.BarDockControl barDockControlBottom;
       private DevExpress.XtraBars.BarDockControl barDockControlLeft;
       private DevExpress.XtraBars.BarDockControl barDockControlRight;
       private DevExpress.XtraBars.Bar barTool;
       private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
   }
}
