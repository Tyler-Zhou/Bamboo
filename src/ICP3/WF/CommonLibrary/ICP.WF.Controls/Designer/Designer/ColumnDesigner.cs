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
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils.Frames;
using DevExpress.XtraEditors.Frames;
using DevExpress.Data.Helpers;
using System.ComponentModel.Design;
namespace ICP.WF.Controls.Design
{
	[ToolboxItem(false)]
    [Serializable()]
	public class ColumnDesigner : LWColumnDesignerBase	{
		private System.ComponentModel.IContainer components = null;
		public ColumnDesigner()	{
			InitializeComponent();
            //groupControlFields.SortOrder = 1;
            //groupControlFields.AllowSort = true;
		}
		public ColumnView ColumnView { get { return EditingObject as ColumnView; } }
		PropertyDescriptor columnDescriptor = null;
		protected override PropertyDescriptor ColumnsDescriptor {
			get {
				if(columnDescriptor == null) {
					columnDescriptor  = TypeDescriptor.GetProperties(ColumnsOwner)["Columns"];
				}
				return columnDescriptor;
			}
		}
		protected override Component ColumnsOwner { get {return ColumnView; } }
		protected override CollectionBase Columns { get { return ColumnView != null ? ColumnView.Columns : null; } }
		protected override ColumnObject CreateColumnObject(object column) { return new GridColumnObject(column); }
		protected override void RetrieveColumnsCore() {
			if(ColumnView == null) return;
			if(ColumnView.GridControl.MainView == ColumnView)
				ColumnView.PopulateColumns();
			else {
				DetailNodeInfo detailInfo = FieldNameTypeConverter.FindDetailNodeInfo(ColumnView);
				if(detailInfo != null) {
					if(detailInfo.Columns != null) 
						ColumnView.PopulateColumns(detailInfo.Columns);
					else
						ColumnView.PopulateColumns(detailInfo.List);
				}
			}
			FireChanged(ColumnView);			
		}
		void FireChanged(Component component) {
			if(component == null || component.Site == null) return;
			IComponentChangeService srv = component.Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
			if(srv != null) srv.OnComponentChanged(component, null, null, null);
		}
		protected override object CreateNewColumn(string fieldName, int index) {
			if(ColumnView == null) return null;
            GridColumn column = new LWGridColumn();
            column.FieldName = fieldName;
            ColumnView.Columns.Add(column);
			if(fieldName == "") 
				column.Caption = column.Site != null ? column.Site.Name : "NewColumn";
			column.AbsoluteIndex = index; 
			column.VisibleIndex = index;
			return column;
		}
		protected override string[] GetDataFieldList(){
            //string[] ret = FieldNameTypeConverter.GetFieldList(ColumnView);
            //if(groupControlFields.SortOrder == -1 || ret == null) return ret;
            //ArrayList array = new ArrayList(ret);
            //array.Sort();
            //for(int i = 0; i < array.Count; i++) {
            //    int j = i;
            //    if(groupControlFields.SortOrder == 0) j = array.Count - i - 1;
            //    ret[i] = array[j].ToString();
            //}
            //return ret;
            return new string[] { };
		}
		protected override object[] GetPropertyGridSampleObjects(object obj) {
			GridColumn column = obj as GridColumn;
			if(SampleObjects == null || column == null) return null;
			object[] samples = new object[SampleObjects.Length];
			for(int i = 0; i < samples.Length; i++) {
				ColumnView view = SampleObjects[i] as ColumnView;
				try {
					samples[i] = view.Columns[column.AbsoluteIndex];
				}
				catch { }
			}
			return samples;
		}
		protected override object GetNestedPropertyGridObject(object obj) {
			GridColumn column = obj as GridColumn;
			if(column != null && SelectedTabPage > 0) {
				return SelectedTabPage == 1 ? (object)column.OptionsColumn : (object)column.OptionsFilter;
			}
			return obj; 
		}
		protected override string[] GetTabsCaption() { 
			if(EmbeddedFrameInit == null)
				return new string[] {"Column properties", "Column options", "Filter options"}; 
			else return null;
		}
		protected override void OnColumnAdded(object column, int visibleIndex) {
			GridColumn gridColumn = column as GridColumn;
			if(SampleView == null || gridColumn == null) return;
			GridColumn col	= SampleView.Columns.Add();
			col.AbsoluteIndex = gridColumn.AbsoluteIndex;
			col.FieldName = gridColumn.FieldName;
			col.Caption = gridColumn.Caption;
			col.VisibleIndex = visibleIndex;
		}
		protected override void OnColumnRemoved(object column, int index) {
			if(SampleView != null && index < SampleView.Columns.Count) 
				SampleView.Columns.RemoveAt(index);
		}
		protected override void OnColumnRecreated() {
			if(SampleView != null)
				SampleView.Assign(this.ColumnView, false);
		}
		protected ColumnView SampleView { get { return SampleObjects != null && SampleObjects.Length > 0 ? SampleObjects[0] as ColumnView : null; } }
		protected override object InternalGetService(Type type) { 
			if(ColumnView == null || ColumnView.GridControl == null) return null;
			return ColumnView.GridControl.InternalGetService(type);  
		}
		public override void InitComponent() {
			base.InitComponent();
			if(ColumnView != null)
				ColumnView.ColumnChanged += new EventHandler(EditingViewColumnChanged);
		}
		protected override void  DeInitComponent() {
			base.DeInitComponent();
			if(ColumnView != null)
				ColumnView.ColumnChanged -= new EventHandler(EditingViewColumnChanged);
		}
		void EditingViewColumnChanged(object sender, EventArgs e) {
			UpdateColumnData();
		}
		protected override void Dispose( bool disposing ) {
			if( disposing )	{
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#region Designer generated code
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion
	}
   
    [Serializable()]
    public class GridColumnObject : ColumnObject {
		public GridColumnObject(object column) : base(column) {}
		public override string FieldName { get { return GridColumn.FieldName; } }
		public override string Caption { get { return GridColumn.GetTextCaption(); } }
		public override int AbsoluteIndex { get { return GridColumn.AbsoluteIndex; } set { GridColumn.AbsoluteIndex = value; } }
		protected GridColumn GridColumn { get { return Column as GridColumn; } }
	}
}
