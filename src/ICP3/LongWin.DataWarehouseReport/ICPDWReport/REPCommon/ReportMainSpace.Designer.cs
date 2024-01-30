namespace LongWin.DataWarehouseReport.WinUI
{
    partial class ReportMainSpace
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.UltraWinDock.DockAreaPane dockAreaPane1 = new Infragistics.Win.UltraWinDock.DockAreaPane(Infragistics.Win.UltraWinDock.DockedLocation.DockedLeft, new System.Guid("889135c8-7e78-41a0-8fb6-248dadb872f1"));
            Infragistics.Win.UltraWinDock.DockableControlPane dockableControlPane1 = new Infragistics.Win.UltraWinDock.DockableControlPane(new System.Guid("ea97c1fb-06a5-420e-8818-16aea66857a6"), new System.Guid("00000000-0000-0000-0000-000000000000"), -1, new System.Guid("889135c8-7e78-41a0-8fb6-248dadb872f1"), -1);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.workSpaceSearch = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.ultraDockManager1 = new Infragistics.Win.UltraWinDock.UltraDockManager(this.components);
            this._ReportMainSpaceUnpinnedTabAreaLeft = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._ReportMainSpaceUnpinnedTabAreaRight = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._ReportMainSpaceUnpinnedTabAreaTop = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._ReportMainSpaceUnpinnedTabAreaBottom = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._ReportMainSpaceAutoHideControl = new Infragistics.Win.UltraWinDock.AutoHideControl();
            this.windowDockingArea1 = new Infragistics.Win.UltraWinDock.WindowDockingArea();
            this.dockableWindow1 = new Infragistics.Win.UltraWinDock.DockableWindow();
            this.workSpaceList = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDockManager1)).BeginInit();
            this.windowDockingArea1.SuspendLayout();
            this.dockableWindow1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.workSpaceSearch, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 18);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(245, 375);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // workSpaceSearch
            // 
            this.workSpaceSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workSpaceSearch.Location = new System.Drawing.Point(3, 3);
            this.workSpaceSearch.Name = "workSpaceSearch";
            this.workSpaceSearch.Size = new System.Drawing.Size(239, 369);
            this.workSpaceSearch.TabIndex = 0;
            this.workSpaceSearch.Text = "deckWorkspace1";
            // 
            // ultraDockManager1
            // 
            dockAreaPane1.ChildPaneStyle = Infragistics.Win.UltraWinDock.ChildPaneStyle.VerticalSplit;
            dockableControlPane1.Control = this.tableLayoutPanel1;
            dockableControlPane1.OriginalControlBounds = new System.Drawing.Rectangle(135, 108, 209, 224);
            dockableControlPane1.Settings.AllowClose = Infragistics.Win.DefaultableBoolean.False;
            dockableControlPane1.Settings.AllowDragging = Infragistics.Win.DefaultableBoolean.False;
            dockableControlPane1.Size = new System.Drawing.Size(100, 100);
            dockableControlPane1.Text = "Search";
            dockAreaPane1.Panes.AddRange(new Infragistics.Win.UltraWinDock.DockablePaneBase[] {
            dockableControlPane1});
            dockAreaPane1.Settings.AllowClose = Infragistics.Win.DefaultableBoolean.False;
            dockAreaPane1.Size = new System.Drawing.Size(245, 393);
            this.ultraDockManager1.DockAreas.AddRange(new Infragistics.Win.UltraWinDock.DockAreaPane[] {
            dockAreaPane1});
            this.ultraDockManager1.HostControl = this;
            // 
            // _ReportMainSpaceUnpinnedTabAreaLeft
            // 
            this._ReportMainSpaceUnpinnedTabAreaLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this._ReportMainSpaceUnpinnedTabAreaLeft.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._ReportMainSpaceUnpinnedTabAreaLeft.Location = new System.Drawing.Point(0, 0);
            this._ReportMainSpaceUnpinnedTabAreaLeft.Name = "_ReportMainSpaceUnpinnedTabAreaLeft";
            this._ReportMainSpaceUnpinnedTabAreaLeft.Owner = this.ultraDockManager1;
            this._ReportMainSpaceUnpinnedTabAreaLeft.Size = new System.Drawing.Size(0, 393);
            this._ReportMainSpaceUnpinnedTabAreaLeft.TabIndex = 0;
            // 
            // _ReportMainSpaceUnpinnedTabAreaRight
            // 
            this._ReportMainSpaceUnpinnedTabAreaRight.Dock = System.Windows.Forms.DockStyle.Right;
            this._ReportMainSpaceUnpinnedTabAreaRight.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._ReportMainSpaceUnpinnedTabAreaRight.Location = new System.Drawing.Point(553, 0);
            this._ReportMainSpaceUnpinnedTabAreaRight.Name = "_ReportMainSpaceUnpinnedTabAreaRight";
            this._ReportMainSpaceUnpinnedTabAreaRight.Owner = this.ultraDockManager1;
            this._ReportMainSpaceUnpinnedTabAreaRight.Size = new System.Drawing.Size(0, 393);
            this._ReportMainSpaceUnpinnedTabAreaRight.TabIndex = 1;
            // 
            // _ReportMainSpaceUnpinnedTabAreaTop
            // 
            this._ReportMainSpaceUnpinnedTabAreaTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._ReportMainSpaceUnpinnedTabAreaTop.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._ReportMainSpaceUnpinnedTabAreaTop.Location = new System.Drawing.Point(0, 0);
            this._ReportMainSpaceUnpinnedTabAreaTop.Name = "_ReportMainSpaceUnpinnedTabAreaTop";
            this._ReportMainSpaceUnpinnedTabAreaTop.Owner = this.ultraDockManager1;
            this._ReportMainSpaceUnpinnedTabAreaTop.Size = new System.Drawing.Size(553, 0);
            this._ReportMainSpaceUnpinnedTabAreaTop.TabIndex = 2;
            // 
            // _ReportMainSpaceUnpinnedTabAreaBottom
            // 
            this._ReportMainSpaceUnpinnedTabAreaBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._ReportMainSpaceUnpinnedTabAreaBottom.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._ReportMainSpaceUnpinnedTabAreaBottom.Location = new System.Drawing.Point(0, 393);
            this._ReportMainSpaceUnpinnedTabAreaBottom.Name = "_ReportMainSpaceUnpinnedTabAreaBottom";
            this._ReportMainSpaceUnpinnedTabAreaBottom.Owner = this.ultraDockManager1;
            this._ReportMainSpaceUnpinnedTabAreaBottom.Size = new System.Drawing.Size(553, 0);
            this._ReportMainSpaceUnpinnedTabAreaBottom.TabIndex = 3;
            // 
            // _ReportMainSpaceAutoHideControl
            // 
            this._ReportMainSpaceAutoHideControl.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._ReportMainSpaceAutoHideControl.Location = new System.Drawing.Point(0, 0);
            this._ReportMainSpaceAutoHideControl.Name = "_ReportMainSpaceAutoHideControl";
            this._ReportMainSpaceAutoHideControl.Owner = this.ultraDockManager1;
            this._ReportMainSpaceAutoHideControl.Size = new System.Drawing.Size(0, 0);
            this._ReportMainSpaceAutoHideControl.TabIndex = 4;
            // 
            // windowDockingArea1
            // 
            this.windowDockingArea1.Controls.Add(this.dockableWindow1);
            this.windowDockingArea1.Dock = System.Windows.Forms.DockStyle.Left;
            this.windowDockingArea1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.windowDockingArea1.Location = new System.Drawing.Point(0, 0);
            this.windowDockingArea1.Name = "windowDockingArea1";
            this.windowDockingArea1.Owner = this.ultraDockManager1;
            this.windowDockingArea1.Size = new System.Drawing.Size(250, 393);
            this.windowDockingArea1.TabIndex = 6;
            // 
            // dockableWindow1
            // 
            this.dockableWindow1.Controls.Add(this.tableLayoutPanel1);
            this.dockableWindow1.Location = new System.Drawing.Point(0, 0);
            this.dockableWindow1.Name = "dockableWindow1";
            this.dockableWindow1.Owner = this.ultraDockManager1;
            this.dockableWindow1.Size = new System.Drawing.Size(245, 393);
            this.dockableWindow1.TabIndex = 0;
            // 
            // workSpaceList
            // 
            this.workSpaceList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.workSpaceList.Location = new System.Drawing.Point(250, 0);
            this.workSpaceList.Name = "workSpaceList";
            this.workSpaceList.Size = new System.Drawing.Size(303, 393);
            this.workSpaceList.TabIndex = 7;
            this.workSpaceList.Text = "deckWorkspace1";
            // 
            // ReportMainSpace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._ReportMainSpaceAutoHideControl);
            this.Controls.Add(this.workSpaceList);
            this.Controls.Add(this.windowDockingArea1);
            this.Controls.Add(this._ReportMainSpaceUnpinnedTabAreaTop);
            this.Controls.Add(this._ReportMainSpaceUnpinnedTabAreaBottom);
            this.Controls.Add(this._ReportMainSpaceUnpinnedTabAreaLeft);
            this.Controls.Add(this._ReportMainSpaceUnpinnedTabAreaRight);
            this.Name = "ReportMainSpace";
            this.Size = new System.Drawing.Size(553, 393);
            this.Load += new System.EventHandler(this.ReportMainSpace_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraDockManager1)).EndInit();
            this.windowDockingArea1.ResumeLayout(false);
            this.dockableWindow1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinDock.UltraDockManager ultraDockManager1;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _ReportMainSpaceUnpinnedTabAreaLeft;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _ReportMainSpaceUnpinnedTabAreaRight;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _ReportMainSpaceUnpinnedTabAreaTop;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _ReportMainSpaceUnpinnedTabAreaBottom;
        private Infragistics.Win.UltraWinDock.AutoHideControl _ReportMainSpaceAutoHideControl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Infragistics.Win.UltraWinDock.WindowDockingArea windowDockingArea1;
        private Infragistics.Win.UltraWinDock.DockableWindow dockableWindow1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace workSpaceSearch;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace workSpaceList;
    }
}
