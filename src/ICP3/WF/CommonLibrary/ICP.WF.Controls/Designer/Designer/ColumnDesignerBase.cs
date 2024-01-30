#region Copyright (c) 2000-2010 Developer Express Inc.
/*
{*******************************************************************}
{                                                                   }
{       Developer Express .NET Component Library                    }
{       XtraEditors                                                 }
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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.ComponentModel.Design;
using DevExpress.Utils;
using DevExpress.Utils.Frames;
using DevExpress.Utils.Design;
using DevExpress.Data.Helpers;
using DevExpress.XtraTab;
using DevExpress.XtraEditors;
namespace ICP.WF.Controls.Design
{
	[ToolboxItem(false)]
    [Serializable()]
	public abstract class LWColumnDesignerBase : LWXtraPGFrame {
		public DevExpress.XtraEditors.SimpleButton btnDown;
		public DevExpress.XtraEditors.SimpleButton btnUp;
        DevExpress.XtraEditors.ListBoxControl ColumnList;
		DevExpress.XtraEditors.SimpleButton btRemove;
		DevExpress.XtraEditors.SimpleButton btAdd;
		public DevExpress.XtraEditors.PanelControl pnlProperty;
        DevExpress.XtraEditors.SimpleButton btInsert;
		//DevExpress.XtraEditors.SplitterControl splLeft;
        //DevExpress.XtraEditors.ListBoxControl FieldList;
       // public DevExpress.XtraEditors.Frames.SortGroupPanel groupControlFields;
		DevExpress.XtraEditors.GroupControl groupControlColumns;
		DevExpress.XtraEditors.PanelControl pnlArrows;
		System.ComponentModel.IContainer components = null;
		XtraTabControl tabControl = null;
		protected override bool AllowGlobalStore { get { return false; } }
		public override void StoreLocalProperties(PropertyStore localStore) {
			localStore.AddProperty("ShowFields", VisibleList);
			base.StoreLocalProperties(localStore);
		}
		public override void RestoreLocalProperties(PropertyStore localStore) {
			base.RestoreLocalProperties(localStore);
			visibleList = localStore.RestoreBoolProperty("ShowFields", false);
		}
		#region Component Designer generated code
		void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LWColumnDesignerBase));
            this.ColumnList = new DevExpress.XtraEditors.ListBoxControl();
            this.pnlProperty = new DevExpress.XtraEditors.PanelControl();
            this.pnlArrows = new DevExpress.XtraEditors.PanelControl();
            this.btnUp = new DevExpress.XtraEditors.SimpleButton();
            this.btnDown = new DevExpress.XtraEditors.SimpleButton();
            this.btRemove = new DevExpress.XtraEditors.SimpleButton();
            this.btInsert = new DevExpress.XtraEditors.SimpleButton();
            this.btAdd = new DevExpress.XtraEditors.SimpleButton();
            this.groupControlColumns = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlControl)).BeginInit();
            this.pnlControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.horzSplitter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColumnList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlProperty)).BeginInit();
            this.pnlProperty.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlArrows)).BeginInit();
            this.pnlArrows.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlColumns)).BeginInit();
            this.groupControlColumns.SuspendLayout();
            this.SuspendLayout();
            // 
            // splMain
            // 
            resources.ApplyResources(this.splMain, "splMain");
            // 
            // pgMain
            // 
            resources.ApplyResources(this.pgMain, "pgMain");
            // 
            // pnlControl
            // 
            this.pnlControl.Controls.Add(this.btInsert);
            this.pnlControl.Controls.Add(this.btAdd);
            this.pnlControl.Controls.Add(this.btRemove);
            resources.ApplyResources(this.pnlControl, "pnlControl");
            // 
            // lbCaption
            // 
            resources.ApplyResources(this.lbCaption, "lbCaption");
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.groupControlColumns);
            this.pnlMain.Controls.Add(this.pnlProperty);
            resources.ApplyResources(this.pnlMain, "pnlMain");
            // 
            // horzSplitter
            // 
            resources.ApplyResources(this.horzSplitter, "horzSplitter");
            // 
            // ColumnList
            // 
            this.ColumnList.AllowDrop = true;
            resources.ApplyResources(this.ColumnList, "ColumnList");
            this.ColumnList.ItemHeight = 16;
            this.ColumnList.Name = "ColumnList";
            this.ColumnList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ColumnList.DragOver += new System.Windows.Forms.DragEventHandler(this.ColumnList_DragOver);
            this.ColumnList.DragDrop += new System.Windows.Forms.DragEventHandler(this.ColumnList_DragDrop);
            this.ColumnList.SelectedIndexChanged += new System.EventHandler(this.ColumnList_SelectedIndexChanged);
            this.ColumnList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ColumnList_KeyDown);
            // 
            // pnlProperty
            // 
            this.pnlProperty.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlProperty.Controls.Add(this.pnlArrows);
            resources.ApplyResources(this.pnlProperty, "pnlProperty");
            this.pnlProperty.Name = "pnlProperty";
            this.pnlProperty.SizeChanged += new System.EventHandler(this.pnlProperty_SizeChanged);
            // 
            // pnlArrows
            // 
            this.pnlArrows.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlArrows.Controls.Add(this.btnUp);
            this.pnlArrows.Controls.Add(this.btnDown);
            resources.ApplyResources(this.pnlArrows, "pnlArrows");
            this.pnlArrows.Name = "pnlArrows";
            // 
            // btnUp
            // 
            resources.ApplyResources(this.btnUp, "btnUp");
            this.btnUp.Name = "btnUp";
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            resources.ApplyResources(this.btnDown, "btnDown");
            this.btnDown.Name = "btnDown";
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btRemove
            // 
            resources.ApplyResources(this.btRemove, "btRemove");
            this.btRemove.Name = "btRemove";
            this.btRemove.Click += new System.EventHandler(this.btRemove_Click);
            // 
            // btInsert
            // 
            resources.ApplyResources(this.btInsert, "btInsert");
            this.btInsert.Name = "btInsert";
            this.btInsert.Click += new System.EventHandler(this.btInsert_Click);
            // 
            // btAdd
            // 
            resources.ApplyResources(this.btAdd, "btAdd");
            this.btAdd.Name = "btAdd";
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // groupControlColumns
            // 
            this.groupControlColumns.Controls.Add(this.ColumnList);
            resources.ApplyResources(this.groupControlColumns, "groupControlColumns");
            this.groupControlColumns.Name = "groupControlColumns";
            // 
            // LWColumnDesignerBase
            // 
            this.Name = "LWColumnDesignerBase";
            resources.ApplyResources(this, "$this");
            ((System.ComponentModel.ISupportInitialize)(this.pnlControl)).EndInit();
            this.pnlControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.horzSplitter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColumnList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlProperty)).EndInit();
            this.pnlProperty.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlArrows)).EndInit();
            this.pnlArrows.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlColumns)).EndInit();
            this.groupControlColumns.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		protected bool IsDesignMode { get { return ColumnsOwner.Site != null && ColumnsOwner.Site.DesignMode; } }
		protected virtual bool AllowModify {
			get {
				return !IsDesignMode || InheritanceHelper.AllowCollectionModify(ColumnsOwner, ColumnsDescriptor, Columns);
			}
		}
		protected virtual bool AllowElementsEdit {
			get {
				return !IsDesignMode || InheritanceHelper.AllowCollectionItemEdit(ColumnsOwner, ColumnsDescriptor, Columns);
			}
		}
		protected virtual bool AllowRemoveItem(object item) {
			return !IsDesignMode || InheritanceHelper.AllowCollectionItemRemove(ColumnsOwner, ColumnsDescriptor, Columns, item);
		}
		protected virtual bool AllowEditItem(object item) {
			return !IsDesignMode || InheritanceHelper.AllowCollectionItemEdit(ColumnsOwner, ColumnsDescriptor, Columns, item);
		}
		protected abstract PropertyDescriptor ColumnsDescriptor { get; }
		protected abstract Component ColumnsOwner { get; }
		protected abstract CollectionBase Columns { get; }
		protected abstract ColumnObject CreateColumnObject(object column);
		protected abstract void RetrieveColumnsCore();
		protected abstract object CreateNewColumn(string fieldName, int index);
		protected abstract string[] GetDataFieldList();
		protected virtual object InternalGetService(Type type) { return null;  }
		protected virtual string[] GetTabsCaption() { return null;  } 
		protected virtual bool CanRetrieveFields { get { return true; } }
		protected virtual string GroupControlColumnsText { get { return "Columns"; } }
		protected virtual void OnColumnAdded(object column, int visibleIndex) {}
		protected virtual void OnColumnRemoved(object column, int index) {}
		protected virtual void OnColumnRecreated() {}
		#region Init & Ctor
		public LWColumnDesignerBase() : base(2) {
			InitializeComponent();
			//FieldList.DoubleClick += new System.EventHandler(this.FieldList_DoubleClick);
			pgMain.BringToFront();
			SetPanelBkColor(pnlControl);
			SetTransparentBitmap(pnlProperty);
			//groupControlFields.SortOrderChanged += new EventHandler(groupControlFields_SortOrderChanged);
		}
		void groupControlFields_SortOrderChanged(object sender, EventArgs e) {
			UpdateFieldList(VisibleList);
			UpdateDescriptionPanel();
		}
		protected override void InitImages() {
			base.InitImages();
			btAdd.Image = DesignerImages16.Images[DesignerImages16AddIndex];
			btInsert.Image = DesignerImages16.Images[DesignerImages16InsertIndex];
			btRemove.Image = DesignerImages16.Images[DesignerImages16RemoveIndex];
			btnUp.Image = DesignerImages16.Images[DesignerImages16UpIndex];
			btnDown.Image = DesignerImages16.Images[DesignerImages16DownIndex];
		}
		public override void InitComponent() {
			IComponentChangeService cs = InternalGetService(typeof(IComponentChangeService)) as IComponentChangeService;
			if(cs != null)
				cs.ComponentRename += new ComponentRenameEventHandler(ComponentChangeService_Rename);
			FillData();
			this.tabControl = CreateTabControl(GetTabsCaption());
			if(this.tabControl != null) 
				this.tabControl.SelectedPageChanged += new TabPageChangedEventHandler(changeTabPage);
			string[] fields = GetDataFieldList();
	
			ShowVisibleList(VisibleList);
			pgMain.Enabled = AllowElementsEdit;
			btAdd.Enabled = AllowModify;
		}
		XtraTabControl CreateTabControl(string[] tabs) {
			if(tabs == null || tabs.Length == 0) return null;
			XtraTabControl tabControl = new XtraTabControl();
			for(int i = 0; i < tabs.Length; i++) {
				XtraTabPage page = tabControl.TabPages.Add(tabs[i]);
				if(i == 0) {
					page.Controls.Add(pgMain);
					pgMain.Dock = DockStyle.Fill;
				}
			}
			this.Controls.Add(tabControl);
			tabControl.Dock = DockStyle.Fill;
			tabControl.BringToFront();
			return tabControl;
		}
		protected virtual void  DeInitComponent() {
			if(EditingObject == null) return;
			IComponentChangeService cs = InternalGetService(typeof(IComponentChangeService)) as IComponentChangeService;
			if(cs != null)
				cs.ComponentRename -= new ComponentRenameEventHandler(ComponentChangeService_Rename);
		}
		void FillData() {
			if(Columns == null) return;
			ColumnList.BeginUpdate();
			ColumnList.Items.BeginUpdate();
			try {
				ColumnList.Items.Clear();
				foreach(object column in Columns) {
					ColumnList.Items.Add(CreateColumnObject(column));
				}
				if(ColumnList.Items.Count > 0)
					ColumnList.SelectedIndex = 0;
			}
			finally {
				ColumnList.EndUpdate();
				ColumnList.Items.EndUpdate();
			}
			UpdatePropertyGridAndButtons();
		}
		protected override void Dispose(bool disposing) {	
			DeInitComponent();
			if(components != null) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}
		#endregion
		#region Column options
		void changeTabPage(object sender, TabPageChangedEventArgs e) {
			RefreshPropertyGrid();
			e.Page.Controls.Add(pgMain);
			pgMain.ShowEvents(SelectedTabPage == 0);
			pgMain.Refresh();
		}
		protected int SelectedTabPage { get { return tabControl != null ? tabControl.SelectedTabPageIndex : 0; } }
		#endregion
		#region Editing		
		protected void ComponentChangeService_Rename(object sender, ComponentRenameEventArgs e) {
			UpdateColumnData();
		}
		protected void UpdateColumnData() {
			ColumnList.Refresh();
			//FieldList.Refresh();
		}
		ColumnObject FocusedColumnObject { get { return ColumnList.SelectedItem as ColumnObject; } }
		object FocusedColumn { get { return FocusedColumnObject != null ? FocusedColumnObject.Column : null; } }
		protected override object[] SelectedObjects {
			get {
				ColumnObject[] columns = GetSelectedColumns();
				if(columns == null) return null;
				object[] selectedObjects = new object[columns.Length];
				for(int i = 0; i < columns.Length; i ++) 
					selectedObjects[i] = columns[i].Column;
				return selectedObjects;
			}
		}
		ColumnObject[] GetSelectedColumns() {
			if(ColumnList.SelectedIndex < 0) return null;
			ColumnObject[] selectedColumns = new ColumnObject[ColumnList.SelectedIndices.Count];
			for(int i = 0; i < ColumnList.SelectedIndices.Count; i++) 
				selectedColumns[i] = ColumnList.Items[ColumnList.SelectedIndices[i]] as ColumnObject;
			return selectedColumns;
		}
		void AddColumn() { AddColumn("", -1); }
		void AddColumn(int index) { AddColumn("", index); }
		void AddColumn(string fieldName, int index) {
			if(!AllowModify) return;
			if(ColumnsOwner == null) return;
			if(index < 0) index = Columns.Count;
			object col = CreateNewColumn(fieldName, index);
			ColumnObject columnObject = CreateColumnObject(col);
			if(index  < ColumnList.Items.Count) 
				ColumnList.Items.Insert(index, columnObject);
				else ColumnList.Items.Add(columnObject);
			OnColumnAdded(col, index);
			ColumnList.SelectedIndex = index;
		}
		void RemoveColumn() {
			if(!AllowModify) return;
			if(ColumnsOwner == null || SelectedObjects == null) return;
			ColumnList.BeginUpdate();
			try {
				int selectedIndex = ColumnList.SelectedIndex;
				ColumnList.Items.BeginUpdate();
				try {
					for(int i = ColumnList.SelectedIndices.Count - 1; i >= 0 ; i--) {
						int removedIndex = ColumnList.SelectedIndices[i];
						ColumnObject columnObject = ColumnList.Items[removedIndex] as ColumnObject;
						if(!AllowRemoveItem(columnObject)) continue;
						OnColumnRemoved(columnObject.Column, removedIndex);
						IDisposable disposableColumn = columnObject.Column as IDisposable;
						if(disposableColumn != null)
							disposableColumn.Dispose();
						ColumnList.Items.RemoveAt(removedIndex);	
					}
				}
				finally {
					ColumnList.Items.EndUpdate();
				}
				if(selectedIndex >= ColumnList.Items.Count)
					selectedIndex = ColumnList.Items.Count - 1;
				ColumnList.SelectedIndex = selectedIndex;
			}
			finally {
				ColumnList.EndUpdate();
			}
			UpdatePropertyGridAndButtons();
		}
		void btAdd_Click(object sender, System.EventArgs e) {
			AddColumn();
		}
		void btRemove_Click(object sender, System.EventArgs e) {
			RemoveColumn();
		}
		void RetrieveColumns() {
			if(ColumnsOwner == null) return;
			if(Columns.Count > 0) {
				if(XtraMessageBox.Show(this, "The collection will be cleared and then populated with entries for each fields in the bound data source.\r\nDo you want to continue?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes) 
					return;
			}
			RetrieveColumnsCore();
			OnColumnRecreated();
			FillData();
		}
		void btRetrieve_Click(object sender, System.EventArgs e) {
			RetrieveColumns();
		}
		void MoveFocusedColumn(int delta) {
			if(FocusedColumnObject == null) return;
			ColumnList.BeginUpdate();
			try {
				int absoluteIndex = FocusedColumnObject.AbsoluteIndex;
				absoluteIndex += delta;
				if(absoluteIndex < 0)
					absoluteIndex = 0;
				if(absoluteIndex >= Columns.Count)
					absoluteIndex = Columns.Count - 1;
				FocusedColumnObject.AbsoluteIndex = absoluteIndex;
				FillData();
				ColumnList.SelectedIndex = absoluteIndex;
			} finally {
				ColumnList.EndUpdate();
			}
			UpdateButtonsAndColumnCount();
		}
		void btnDown_Click(object sender, System.EventArgs e) {
			MoveFocusedColumn(1);
		}
		void btnUp_Click(object sender, System.EventArgs e) {
			MoveFocusedColumn(-1);
		}
		void UpdateButtonsAndColumnCount() {
			int focusedColumnCount = SelectedObjects != null ? SelectedObjects.Length : 0; 
			btnUp.Enabled = focusedColumnCount == 1 && ColumnList.SelectedIndex != 0;
			btnDown.Enabled = focusedColumnCount == 1 && ColumnList.SelectedIndex != ColumnList.Items.Count - 1;
			btRemove.Enabled = focusedColumnCount > 0 && AllowRemoveItem(SelectedObjects[0]);
			btInsert.Enabled = focusedColumnCount > 0 && AllowModify;
			groupControlColumns.Text = string.Format(GroupControlColumnsText + " ({0})", ColumnList.Items.Count);
			pgMain.Enabled = focusedColumnCount > 0 && AllowEditItem(SelectedObjects[0]);
		}
		void ColumnList_SelectedIndexChanged(object sender, System.EventArgs e) {
			UpdatePropertyGridAndButtons();
			pgMain.ShowEvents(SelectedTabPage == 0);
		}
		void UpdatePropertyGridAndButtons() {
			RefreshPropertyGrid();
			UpdateButtonsAndColumnCount();
			UpdateColumnData();
		}
		void btInsert_Click(object sender, System.EventArgs e) {
			AddColumn(ColumnList.SelectedIndex);
		}
		void FireChanged() {
			IComponentChangeService srv = InternalGetService(typeof(IComponentChangeService)) as IComponentChangeService;
			if(srv != null) srv.OnComponentChanged(ColumnsOwner, null, null, null);
		}
		void ColumnList_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			switch(e.KeyCode) {
				case Keys.Delete:
					RemoveColumn();
					e.Handled = true;
					break;
				case Keys.Insert:
					if(btInsert.Enabled)
						AddColumn(ColumnList.SelectedIndex);
					e.Handled = true;
					break;
			}
		}
		#endregion
		#region FieldList
		bool visibleList = false;
		protected bool VisibleList { get { return visibleList; } }
		void chbFieldList_Click(object sender, System.EventArgs e) {
			visibleList = !VisibleList;
			ShowVisibleList(VisibleList);
		}
		void ShowVisibleList(bool visible) {
			//pnlList.Width = ColumnList.Width;
			//pnlList.Visible = splLeft.Visible = visible;
			//chbFieldList.ImageIndex = visible ? 10 : 11;
			//chbFieldList.ToolTip = (visible ? "Hide" : "Show") + " Field List";
			UpdateFieldList(visible);
			UpdateDescriptionPanel();
		}
		void UpdateFieldList(bool visible) {
			if(!visible) return;
            //string[] list = GetDataFieldList();
            //FieldList.DataSource = list;
            //groupControlFields.Text = string.Format("Field List ({0})", FieldList.ItemCount);
		}
		protected override string DescriptionText { 
			get { return "You can add and delete columns, modify their settings, assign in-place editors and enable total summaries." + (VisibleList ? " Select and drag field to the columns panel to create column." : ""); }
		}
		private void FieldList_DoubleClick(object sender, System.EventArgs e) {
            DevExpress.XtraEditors.ListBoxControl lbControl = sender as DevExpress.XtraEditors.ListBoxControl;
			Point p = lbControl.PointToClient(Control.MousePosition);
			int i = lbControl.IndexFromPoint(p);
			if(i > -1) AddColumn(lbControl.GetItemText(i), -1);
		}
		#endregion
		#region DragDrop
		Point mDown = Point.Empty;
		void FieldList_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			mDown = new Point(e.X, e.Y);
		}
		void FieldList_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {
			if(mDown == Point.Empty) return;
			Rectangle dragRect = new Rectangle(new Point(
				mDown.X - SystemInformation.DragSize.Width / 2,
				mDown.Y - SystemInformation.DragSize.Height / 2), SystemInformation.DragSize);
            //if(!dragRect.Contains(new Point(e.X, e.Y))) { 
            //    int index = FieldList.IndexFromPoint(mDown);
            //    if(index < 0 || index >= FieldList.ItemCount || e.Button != MouseButtons.Left) return;
            //    FieldList.DoDragDrop(FieldList.GetItemText(index).ToString(), DragDropEffects.Copy);
            //    mDown = Point.Empty;
            //}
		}
		string GetDragObject(IDataObject data) {
			return data.GetData(typeof(string)) as string;
		}
		void ColumnList_DragOver(object sender, System.Windows.Forms.DragEventArgs e) {
			string field = GetDragObject(e.Data);
			e.Effect = field == null ? DragDropEffects.None : DragDropEffects.Copy;
		}
		void ColumnList_DragDrop(object sender, System.Windows.Forms.DragEventArgs e) {
			DevExpress.XtraEditors.ListBoxControl lBox = sender as DevExpress.XtraEditors.ListBoxControl;
			string field = GetDragObject(e.Data);
			if(field == null) return;
			int index = lBox.IndexFromPoint(lBox.PointToClient(new Point(e.X, e.Y)));
			AddColumn(field, index);
		}
		#endregion
		#region DrawItem
		bool IsColumnExisting(string fieldName) {
			for(int i = 0; i < ColumnList.Items.Count; i ++) {
				ColumnObject columnObject = ColumnList.Items[i] as ColumnObject;
				if(columnObject.FieldName == fieldName) return true;
			}
			return false;
		}
		Font CurrentFont(int index) {
			 return null;
            //string fieldName = FieldList.GetItemText(index);
            //return CurrentFont(fieldName); 
		}
		Font CurrentFont(string fieldName) {
			return IsColumnExisting(fieldName) ?  DevExpress.Utils.AppearanceObject.DefaultFont : new Font(DevExpress.Utils.AppearanceObject.DefaultFont, FontStyle.Bold); 
		}
		void FieldList_DrawItem(object sender, DevExpress.XtraEditors.ListBoxDrawItemEventArgs e) {
            //if(e.Index < 0) return;
            //string fieldName = FieldList.GetItemText(e.Index);
            //Font font = CurrentFont(fieldName);
            //e.Appearance.Font = font;
		}
		void FieldList_MeasureItem(object sender, System.Windows.Forms.MeasureItemEventArgs e) {
			e.ItemHeight = ColumnList.ItemHeight;
		}
		#endregion
		void pnlProperty_SizeChanged(object sender, System.EventArgs e) {
			pnlArrows.Top = (pnlProperty.Height - pnlArrows.Height) / 2;
		}
	}

    [Serializable()]
    public abstract class ColumnObject {
		object column;
		public ColumnObject(object column) {
			this.column = column;
		}
		public object Column { get { return column; } }
		public abstract string Caption { get; }
		public abstract string FieldName { get; }
		public abstract int AbsoluteIndex { get; set; }
		public override string ToString() {
			ISite site = Column as Component != null ? (Column as Component).Site : null;
			if(IsFullName(site, Caption))
				return string.Format("{0} <{1}>", site.Name, Caption);
			if(site != null)
				return site.Name;
			if(Caption != "")
				return Caption;
			if(FieldName != "")
				return FieldName;
			return Column.ToString();
		}
		bool IsNumber(string caption) {
			int dump;
			return Int32.TryParse(caption, out dump);
		}
		protected virtual bool IsFullName(ISite site, string caption) {
			if(site == null || caption == string.Empty) return false;
			int start = site.Name.IndexOf(caption);
			if(start > -1 && start + caption.Length == site.Name.Length) return false;
			if(start > -1 && IsNumber(site.Name.Substring(start + caption.Length, site.Name.Length - (start + caption.Length)))) return false;
			return true;
		}
	}



}
