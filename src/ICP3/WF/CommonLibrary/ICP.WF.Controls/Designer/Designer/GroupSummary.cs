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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.ComponentModel.Design;
using DevExpress.Utils;
using DevExpress.Utils.Frames;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;
namespace ICP.WF.Controls.Design
{
	[ToolboxItem(false)]
	public class GroupSummary : LWXtraPGFrame {
		private DevExpress.XtraEditors.ListBoxControl SumItemList;
		private DevExpress.XtraEditors.SimpleButton btRemove;
		private DevExpress.XtraEditors.SimpleButton btAdd;
		private System.Windows.Forms.Panel pnlProperty;
		private DevExpress.XtraEditors.GroupControl groupControl1;
		private DevExpress.XtraEditors.PanelControl pnlArrows;
		public DevExpress.XtraEditors.SimpleButton btnUp;
		public DevExpress.XtraEditors.SimpleButton btnDown;
		private System.ComponentModel.Container components = null;
		#region Component Designer generated code
		private void InitializeComponent() {
			this.btRemove = new DevExpress.XtraEditors.SimpleButton();
			this.btAdd = new DevExpress.XtraEditors.SimpleButton();
			this.SumItemList = new DevExpress.XtraEditors.ListBoxControl();
			this.pnlProperty = new System.Windows.Forms.Panel();
			this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
			this.pnlArrows = new DevExpress.XtraEditors.PanelControl();
			this.btnUp = new DevExpress.XtraEditors.SimpleButton();
			this.btnDown = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.pnlControl)).BeginInit();
			this.pnlControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			this.pnlMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.horzSplitter)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SumItemList)).BeginInit();
			this.pnlProperty.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
			this.groupControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlArrows)).BeginInit();
			this.pnlArrows.SuspendLayout();
			this.SuspendLayout();
			this.splMain.Location = new System.Drawing.Point(201, 56);
			this.splMain.Size = new System.Drawing.Size(6, 280);
			this.pgMain.Location = new System.Drawing.Point(207, 56);
			this.pgMain.Size = new System.Drawing.Size(445, 280);
			this.pnlControl.Controls.Add(this.btRemove);
			this.pnlControl.Controls.Add(this.btAdd);
			this.pnlControl.Location = new System.Drawing.Point(0, 24);
			this.pnlControl.Size = new System.Drawing.Size(652, 32);
			this.lbCaption.Size = new System.Drawing.Size(652, 24);
			this.pnlMain.Controls.Add(this.groupControl1);
			this.pnlMain.Controls.Add(this.pnlProperty);
			this.pnlMain.Location = new System.Drawing.Point(0, 56);
			this.pnlMain.Size = new System.Drawing.Size(201, 280);
			this.horzSplitter.Location = new System.Drawing.Point(207, 56);
			this.horzSplitter.Size = new System.Drawing.Size(445, 4);
			this.horzSplitter.Visible = false;
			this.btRemove.Location = new System.Drawing.Point(84, 4);
			this.btRemove.Name = "btRemove";
			this.btRemove.Size = new System.Drawing.Size(92, 24);
			this.btRemove.TabIndex = 1;
			this.btRemove.Text = "&Remove";
			this.btRemove.Click += new System.EventHandler(this.btRemove_Click);
			this.btAdd.Location = new System.Drawing.Point(0, 4);
			this.btAdd.Name = "btAdd";
			this.btAdd.Size = new System.Drawing.Size(80, 24);
			this.btAdd.TabIndex = 0;
			this.btAdd.Text = "&Add";
			this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
			this.SumItemList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SumItemList.ItemHeight = 16;
			this.SumItemList.Location = new System.Drawing.Point(4, 24);
			this.SumItemList.Name = "SumItemList";
			this.SumItemList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.SumItemList.Size = new System.Drawing.Size(161, 252);
			this.SumItemList.TabIndex = 3;
			this.SumItemList.SelectedIndexChanged += new System.EventHandler(this.SumItemList_SelectedIndexChanged);
			this.pnlProperty.Controls.Add(this.pnlArrows);
			this.pnlProperty.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnlProperty.Location = new System.Drawing.Point(169, 0);
			this.pnlProperty.Name = "pnlProperty";
			this.pnlProperty.Size = new System.Drawing.Size(32, 280);
			this.pnlProperty.TabIndex = 0;
			this.pnlProperty.SizeChanged += new System.EventHandler(this.pnlProperty_SizeChanged);
			this.groupControl1.Controls.Add(this.SumItemList);
			this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupControl1.Location = new System.Drawing.Point(0, 0);
			this.groupControl1.Name = "groupControl1";
			this.groupControl1.Padding = new System.Windows.Forms.Padding(2);
			this.groupControl1.Size = new System.Drawing.Size(169, 280);
			this.groupControl1.TabIndex = 4;
			this.groupControl1.Text = "Summary Items";
			this.pnlArrows.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.pnlArrows.Controls.Add(this.btnUp);
			this.pnlArrows.Controls.Add(this.btnDown);
			this.pnlArrows.Location = new System.Drawing.Point(0, 108);
			this.pnlArrows.Name = "pnlArrows";
			this.pnlArrows.Size = new System.Drawing.Size(32, 64);
			this.pnlArrows.TabIndex = 9;
			this.btnUp.Enabled = false;
			this.btnUp.Location = new System.Drawing.Point(4, 4);
			this.btnUp.Name = "btnUp";
			this.btnUp.Size = new System.Drawing.Size(24, 24);
			this.btnUp.TabIndex = 5;
			this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
			this.btnDown.Enabled = false;
			this.btnDown.Location = new System.Drawing.Point(4, 36);
			this.btnDown.Name = "btnDown";
			this.btnDown.Size = new System.Drawing.Size(24, 24);
			this.btnDown.TabIndex = 6;
			this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
			this.Name = "GroupSummary";
			this.Size = new System.Drawing.Size(652, 336);
			((System.ComponentModel.ISupportInitialize)(this.pnlControl)).EndInit();
			this.pnlControl.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			this.pnlMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.horzSplitter)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SumItemList)).EndInit();
			this.pnlProperty.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
			this.groupControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlArrows)).EndInit();
			this.pnlArrows.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		#endregion
		#region Init & Ctor
		protected override void InitImages() {
			base.InitImages();
			btAdd.Image = DesignerImages16.Images[DesignerImages16PlusIndex];
			btRemove.Image = DesignerImages16.Images[DesignerImages16MinusIndex];
			btnUp.Image = DesignerImages16.Images[DesignerImages16UpIndex];
			btnDown.Image = DesignerImages16.Images[DesignerImages16DownIndex];
		}
		protected override string DescriptionText { get { return "Add, remove or modify the summary items responsible for calculating summary values when grouping is performed."; } }
		public GroupSummary() : base(5) {
			InitializeComponent();
			pgMain.BringToFront();
		}
		protected override void Dispose(bool disposing) {
			if(components != null) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}
		public override void InitComponent() {
			FillData();
		}
		protected GridView EditingView { get { return EditingObject as GridView; } }
		private void FillData() {
			FillData(0);
		}
		bool lockUpdate = false;
		private void FillData(int indx) {
			SumItemList.BeginUpdate();
			lockUpdate = true;
			try {
				SumItemList.Items.Clear();
				if(EditingView == null) return;
				for(int i = 0; i < EditingView.GroupSummary.Count; i++) {
					SumItemList.Items.Add(new SummaryListBoxItem(EditingView.GroupSummary[i], i)); 
				}
				if(!DevExpress.XtraEditors.Design.FramesUtils.SelectItem(SumItemList, indx))
					SelectedIndexChanged();
			} finally {
				SumItemList.EndUpdate();
				lockUpdate = false;
				EnableArrowButtons();
			}
		}
		#endregion
		#region Editing
		protected override object[] SelectedObjects {
			get {
				if(SumItemList.SelectedIndex < 0 || EditingView == null) return null;
				object[] selectedObjects = new object[SumItemList.SelectedIndices.Count];
				ArrayList al = new ArrayList();
				for(int i = 0; i < SumItemList.SelectedIndices.Count; i++) {
					selectedObjects[i] = EditingView.GroupSummary[SumItemList.SelectedIndices[i]];
				}
				return selectedObjects;
			}
		}
		protected override object[] GetPropertyGridSampleObjects(object obj) {
			GridGroupSummaryItem item = obj as GridGroupSummaryItem;
			if(SampleObjects == null || item == null) return null;
			object[] samples = new object[SampleObjects.Length];
			for(int i = 0; i < samples.Length; i ++)
				samples[i] = (SampleObjects[i] as GridView).GroupSummary[item.Index];
			return samples;
		}
		void SelectedIndexChanged() {
			RefreshPropertyGrid();
			EnableArrowButtons();
		}
		private void SumItemList_SelectedIndexChanged(object sender, System.EventArgs e) {
			SelectedIndexChanged();
		}
		private void btAdd_Click(object sender, System.EventArgs e) {
			if(EditingView == null) return;
			SumItemList.BeginUpdate();
			try {
				GridSummaryItem item = AddGroupSummary();
				if(item != null) {
					int i = SumItemList.Items.Add(new SummaryListBoxItem(item, SumItemList.Items.Count));
					SumItemList.SelectedIndex = i;
				}
			} finally {
				SumItemList.EndUpdate();
			}
			FireChanged();
		}
		private void btRemove_Click(object sender, System.EventArgs e) {
			if(EditingView == null) return;
			int j = SumItemList.SelectedIndex;
			for(int i = SumItemList.SelectedIndices.Count - 1; i >= 0 ; i--) {
				RemoveGroupSummary(SumItemList.SelectedIndices[i]);
			}
			FillData();
			DevExpress.XtraEditors.Design.FramesUtils.SelectItem(SumItemList, j);
			FireChanged();
		}
		private void FireChanged() {
			if(EditingView == null) return;
			IComponentChangeService srv = EditingView.GridControl.InternalGetService(typeof(IComponentChangeService)) as IComponentChangeService;
			if(srv != null) srv.OnComponentChanged(EditingView, null, null, null);
		}
		protected virtual GridSummaryItem AddGroupSummary() {
			GridSummaryItem item = EditingView.GroupSummary.Add();
			if(SampleView != null)
				SampleView.GroupSummary.Add();
			return item;
		}
		protected virtual void RemoveGroupSummary(int index) {
			EditingView.GroupSummary.RemoveAt(index);
			if(SampleView != null)
				SampleView.GroupSummary.RemoveAt(index);
		}
		protected GridView SampleView { get { return SampleObjects != null && SampleObjects.Length > 0 ? SampleObjects[0] as GridView : null; } }
		protected override void OnPropertyGridPropertyValueChanged(object sender, PropertyValueChangedEventArgs e) {
			base.OnPropertyGridPropertyValueChanged(sender, e);
			SumItemList.Refresh();
			FireChanged();
		}
		#endregion
		class SummaryListBoxItem : object {
			GridSummaryItem item = null;
			int index = -1;
			public SummaryListBoxItem(GridSummaryItem item, int index) {
				this.item = item;
				this.index = index;
			}
			public override string ToString() {
				if(item.FieldName != null && item.FieldName != string.Empty)
					return string.Format("Summary Item - {0} ({1})", item.FieldName, index);
				return string.Format("Summary Item - Index {0}", index);
			}
		}
		bool AllowMoveCollectionItems {
			get {
				if(SelectedObjects == null) return false;
				return SelectedObjects.Length == 1;
			}
		}
		private void pnlProperty_SizeChanged(object sender, EventArgs e) {
			pnlArrows.Top = (pnlProperty.Height - pnlArrows.Height) / 2;
		}
		protected virtual void EnableArrowButtons() {
			if(lockUpdate) return;
			btRemove.Enabled = (SelectedObjects != null && SelectedObjects.Length > 0);
			btnDown.Enabled = AllowMoveCollectionItems && !SumItemList.SelectedIndices.Contains(SumItemList.ItemCount - 1);
			btnUp.Enabled = AllowMoveCollectionItems && !SumItemList.SelectedIndices.Contains(0);
			groupControl1.Text = string.Format("Summary Items ({0})", SumItemList.Items.Count);
		}
		GridGroupSummaryItemCollection GroupSummaryItems { get { return EditingView != null ? EditingView.GroupSummary : null; } }
		void MoveRecord(int j) {
			if(SumItemList.SelectedIndices.Count != 1) return;
			int indx = SumItemList.SelectedIndex;
			ArrayList collection = new ArrayList();
			for(int i = 0; i < GroupSummaryItems.Count; i++) {
				int pos = i;
				if(i == indx + j) pos = i - j;
				if(i == indx) pos = i + j;
				collection.Add(GroupSummaryItems[pos]);
			}
			GroupSummaryItems.Clear();
			for(int i = 0; i < collection.Count; i++)
				GroupSummaryItems.Add((GridSummaryItem)collection[i]);
			FillData(indx + j);
			FireChanged();
		}
		private void btnUp_Click(object sender, EventArgs e) {
			MoveRecord(-1);
		}
		private void btnDown_Click(object sender, EventArgs e) {
			MoveRecord(1);
		}
	}
}
