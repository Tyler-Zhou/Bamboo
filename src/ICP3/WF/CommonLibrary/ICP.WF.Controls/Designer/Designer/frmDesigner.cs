#region Copyright (c) 2000-2010 Developer Express Inc.
/*
{*******************************************************************}
{                                                                   }
{       Developer Express .NET Component Library                    }
{       XtraGrid                                                    }
{                                                                   }
{       Copyright (c) 2000-2010 Developer Express Inc.              }
{       ALL RIGHTS RESERVED                                         }
{                                                                   }
{   The entire contents of this file is protected by U.S. and       }
{   International Copyright Laws. Unauthorized reproduction,        }
{   reverse-engineering, and distribution of all or any portion of  }
{   the code contained in this file is strictly prohibited and may  }
{   result in severe civil and criminal penalties and will be       }
{   prosecuted to the maximum extent possible under the law.        }
{                                                                   }
{   RESTRICTIONS                                                    }
{                                                                   }
{   THIS SOURCE CODE AND ALL RESULTING INTERMEDIATE FILES           }
{   ARE CONFIDENTIAL AND PROPRIETARY TRADE                          }
{   SECRETS OF DEVELOPER EXPRESS INC. THE REGISTERED DEVELOPER IS   }
{   LICENSED TO DISTRIBUTE THE PRODUCT AND ALL ACCOMPANYING .NET    }
{   CONTROLS AS PART OF AN EXECUTABLE PROGRAM ONLY.                 }
{                                                                   }
{   THE SOURCE CODE CONTAINED WITHIN THIS FILE AND ALL RELATED      }
{   FILES OR ANY PORTION OF ITS CONTENTS SHALL AT NO TIME BE        }
{   COPIED, TRANSFERRED, SOLD, DISTRIBUTED, OR OTHERWISE MADE       }
{   AVAILABLE TO OTHER INDIVIDUALS WITHOUT EXPRESS WRITTEN CONSENT  }
{   AND PERMISSION FROM DEVELOPER EXPRESS INC.                      }
{                                                                   }
{   CONSULT THE END USER LICENSE AGREEMENT FOR INFORMATION ON       }
{   ADDITIONAL RESTRICTIONS.                                        }
{                                                                   }
{*******************************************************************}
*/
#endregion Copyright (c) 2000-2010 Developer Express Inc.

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.Utils.Frames;
using DevExpress.Utils.Design;
using DevExpress.XtraNavBar;
using System.ComponentModel.Design;
using DevExpress.XtraGrid.Design;
namespace ICP.WF.Controls.Design
{
	public class frmDesigner : BaseDesignerForm {
		EditingGridInfo gridInfo;
		private PanelControl panel1;
		private System.Windows.Forms.Label label1;
		private PopupLevelEdit leActiveView;
		#region Designer generated code
		private void InitializeComponent() {
            this.leActiveView = new ICP.WF.Controls.Design.PopupLevelEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.leActiveView.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // leActiveView
            // 
            this.leActiveView.Location = new System.Drawing.Point(120, 5);
            this.leActiveView.Name = "leActiveView";
            this.leActiveView.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.leActiveView.Properties.PopupSizeable = false;
            this.leActiveView.Size = new System.Drawing.Size(256, 21);
            this.leActiveView.TabIndex = 0;
            this.leActiveView.EditValueChanged += new System.EventHandler(this.leActiveView_EditValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label1.Location = new System.Drawing.Point(8, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Selected View : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panel1.Controls.Add(this.leActiveView);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(789, 32);
            this.panel1.TabIndex = 10;
            this.panel1.Visible = false;
            // 
            // frmDesigner
            // 
            this.ClientSize = new System.Drawing.Size(789, 446);
            this.Controls.Add(this.panel1);
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MinimumSize = new System.Drawing.Size(700, 452);
            this.Name = "frmDesigner";
            this.Controls.SetChildIndex(this.panel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.leActiveView.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

            this.NavBar.Visible = false;

		}
		#endregion
		protected DevExpress.XtraGrid.GridControl EditingGrid { get { return GridInfo == null ? null : GridInfo.EditingGrid; } }
		private DevExpress.XtraGrid.Views.Base.BaseView CurrentView {
			get {
				return GridInfo == null ? null : GridInfo.SelectedView;
			}
		}
		public LWGridControlDesigner EditingGridDesigner {
			get {
				if(EditingGrid == null) return null;
				IDesignerHost host = EditingGrid.InternalGetService(typeof(IDesignerHost)) as IDesignerHost;
				if(host == null) return null;
				return host.GetDesigner(EditingGrid) as LWGridControlDesigner;
			}
		}
		public virtual EditingGridInfo GridInfo { get { return gridInfo; } }
		protected string CurrentViewName {
			get {
				if(CurrentView == null) return "";
				else return CurrentView.BaseInfo.ViewName;
			}
		}
		private BaseDesigner emptyDesigner = null;
		BaseDesigner gridDesigner;
		public frmDesigner() {
			InitializeComponent();
			panel1.SendToBack();
			this.gridInfo = null;
            this.emptyDesigner = new EmptyViewDesigner();
            this.emptyDesigner.Init();
			this.gridDesigner = new ControlGridDesigner();
			this.gridDesigner.Init();
		}
		IComponentChangeService changeService = null;
		public virtual void InitGrid(DevExpress.XtraGrid.GridControl grid) {
			InitGrid(grid, "", null);
		}
		public virtual void InitGrid(DevExpress.XtraGrid.GridControl grid, BaseView view) {
			InitGrid(grid, "", view);
		}
		public virtual void InitGrid(DevExpress.XtraGrid.GridControl grid, string itemLinkCaption) {
			InitGrid(grid, itemLinkCaption, null);
		}
		public virtual void InitGrid(DevExpress.XtraGrid.GridControl grid, string itemLinkCaption, BaseView view) {
			Initialize();
			this.gridInfo = new EditingGridInfo(grid);
			this.gridInfo.SelectionChanged += new EventHandler(OnGridInfo_SelectionChanged);
			OnGridInfo_SelectionChanged(this, EventArgs.Empty);
			if(view != null) gridInfo.SelectedObject = view;
			leActiveView.Grid = grid;
			leActiveView.SetViewCore(gridInfo.SelectedView);
			InitEditingObject(grid, itemLinkCaption);
			this.changeService = EditingGrid.InternalGetService(typeof(IComponentChangeService)) as IComponentChangeService;
			if(this.changeService != null)
				this.changeService.ComponentRename += new ComponentRenameEventHandler(OnComponentRename);
		}
		protected void OnComponentRename(object sender, ComponentRenameEventArgs e) {
			OnGridInfo_SelectionChanged(this, EventArgs.Empty);
		}
		protected void OnGridInfo_SelectionChanged(object sender, EventArgs e) {
			leActiveView.SetViewCore(GridInfo.SelectedView);
		}
		public const string GridSettings = "Software\\Developer Express\\Designer\\XtraGrid\\";
		protected override string RegistryStorePath { get { return GridSettings; } }
		protected override void InitFrame(string caption, Bitmap bitmap) {
			XF.InitFrame(CurrentView, GridInfo, caption, bitmap);
		}
		protected override BaseDesigner ActiveDesigner {
			get { 
				if(GridInfo != null && GridInfo.SelectedObject is GridControl) {
					return gridDesigner;
				}
                if (CurrentView == null)
                    return emptyDesigner;
                else
					return CurrentView.BaseInfo.Designer; 
			}
		}
		protected override void OnXF_RefreshWizard(object sender, RefreshWizardEventArgs e) {
			if(e.Condition == "ChangedView") {
				RefreshDesigner(false);
			}
		}
		protected void RefreshDesigner(bool updateActiveFrame) {
			InitNavBar(updateActiveFrame);
			EnabledMainItems();
		}
		protected override Type ResolveType(string type) {
			Type t = typeof(frmDesigner).Assembly.GetType(type);
			if(t != null) return t;
			return base.ResolveType(type);
		}
		protected void EnabledMainItems() {
			for(int i = 0; i < NavBar.Items.Count; i++) {
				DesignerItem item = NavBar.Items[i].Tag as DesignerItem;
				if(item != null && item.Tag != null && item.Tag.ToString() == "main")
					NavBar.Items[i].Enabled = CurrentView == EditingGrid.MainView;
			}
		}
		private static int ViewType(object view, ImageList iml) {
			string[] viewNames = new string[] {"CardView", "GridView", "BandedGridView", "AdvBandedGridView"};
			if(view == null) return 0;
			int index = Array.IndexOf(viewNames, ((BaseView)view).BaseInfo.ViewName);
			return index < 0 ? iml.Images.Count - 1 : index + 1;
		}
		protected void FireGridChanged() {
			if(this.changeService != null) 
				this.changeService.OnComponentChanged(this, null, null, null);
		}
		protected override void OnClosed(EventArgs e) {	
			if(this.changeService != null) {
				this.changeService.ComponentRename -= new ComponentRenameEventHandler(OnComponentRename);
				this.changeService = null;
			}
			base.OnClosed(e);
		}
		private void leActiveView_EditValueChanged(object sender, System.EventArgs e) {
			if(GridInfo == null) return;
			GridInfo.SelectedObject = leActiveView.View;
			RefreshDesigner(true);
		}
	}
	public class EditingGridInfo : ISelectionService {
		object selectedObject;
		GridControl editingGrid;
		public EditingGridInfo(GridControl editingGrid) {
			this.editingGrid = editingGrid;
			this.selectedObject = EditingGrid == null ? null : EditingGrid.MainView;
		}
		public virtual GridControl EditingGrid {
			get { return editingGrid; }
		}
		public virtual BaseView SelectedView { 
			get { return SelectedObject as BaseView; }
		}
		public virtual object SelectedObject { 
			get { return selectedObject; } 
			set {
				if(SelectedObject == value) return;
				if(SelectionChanging != null) SelectionChanging(this, EventArgs.Empty);
				this.selectedObject = value;
				if(SelectionChanged != null) SelectionChanged(this, EventArgs.Empty);
			}
		}
		bool ISelectionService.GetComponentSelected(object component) { return component == SelectedObject; }
		ICollection ISelectionService.GetSelectedComponents() {
			if(SelectedObject == null) return null;
			return new object[] { SelectedObject };
		}
		object ISelectionService.PrimarySelection {
			get { return SelectedObject; }
		}
		int ISelectionService.SelectionCount { get { return SelectedObject == null ? 0 : 1; } }
		void ISelectionService.SetSelectedComponents(ICollection components, SelectionTypes types) {
			((ISelectionService)this).SetSelectedComponents(components);
		}
		void ISelectionService.SetSelectedComponents(ICollection components) {
			if(components == null || components.Count == 0) SelectedObject = null;
			else {
				foreach(object sel in components) {
					SelectedObject = sel;
					break;
				}
			}
		}
		public event EventHandler SelectionChanged, SelectionChanging;
	}
}
