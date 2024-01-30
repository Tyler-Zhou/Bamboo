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
using System.Data;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.Utils.Frames;
using DevExpress.XtraEditors.Frames;
using DevExpress.Utils;
using DevExpress.XtraTab;
namespace ICP.WF.Controls.Design
{
	[ToolboxItem(false)]
	public class Layouts : LayoutsBase{
		public Layouts() : base(6) {
		}
		protected override string DescriptionText { get { return "Modify the view's layout (sorting and grouping settings, column and band arrangement) and click the Apply button to apply the modifications to the current view. You can also save the layout to an XML file (this can be loaded and applied to other views at design time and runtime)."; } }
		protected BaseView EditingView { get { return EditingObject as BaseView; } }
		DevExpress.XtraGrid.GridControl grid = null;
		BaseView baseView;
		protected override Control CreatePreviewControl() {
			grid = DevExpress.XtraGrid.Design.GridAssign.CreateGridControlAssign(EditingView.GridControl, EditingView);
			grid.Tag = "Design";
			baseView = grid.MainView;
			if(dataSet != null) 
				grid.DataSource = dataSet.Tables[tableName];
			else
				grid.DataSource = EditingView.DataSource;
			InitView();
			grid.MainView.Layout += new EventHandler(View_Layout);
			return grid;
		}
		protected override void ShowColumnsCustomization() {
			View.ColumnsCustomization();
		}
		protected override void HideColumnsCustomization() {
			View.DestroyCustomization();
		}
		protected override void ApplyLayouts() {
			System.IO.MemoryStream str = new System.IO.MemoryStream();
			baseView.SaveLayoutToStream(str, OptionsLayoutBase.FullLayout);
			str.Seek(0, System.IO.SeekOrigin.Begin);
			EditingView.RestoreLayoutFromStream(str, OptionsLayoutBase.FullLayout);
			str.Close();
		}
		protected override void RestoreLayoutFromXml(string fileName) {
			baseView.RestoreLayoutFromXml(fileName, OptionsLayoutBase.FullLayout);
		}
		protected override void SaveLayoutToXml(string fileName) {
			try {
			baseView.SaveLayoutToXml(fileName, OptionsLayoutBase.FullLayout);
		}
			catch(Exception ex) {
				DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, ex.Source);
			}
		}
		protected override void SetControlDataSource(DataView dataView) {
			grid.DataSource = dataView;
			if(((ColumnView)EditingView).Columns.Count == 0)
				baseView.PopulateColumns();
			else SetLayoutChanged(false);
		}
		ComponentCollection GetCurrentComponents() {
			if(EditingView.GridControl.Container != null) return EditingView.GridControl.Container.Components;
			if(EditingView.Container != null) return EditingView.Container.Components;
			return null;
		}
		protected override DBAdapter CreateDBAdapter() {
			ArrayList adapters = new ArrayList();
			ComponentCollection components = GetCurrentComponents();
			if(components == null) return null;
			foreach(object comp in components)
				adapters.Add(comp);
			return new DBAdapter(adapters, EditingView.GridControl.DataSource, EditingView.GridControl.DataMember);
		}
		protected override DataTable CreateDataTableAdapter() {
			if(EditingView.GridControl.DataSource == null)  return null;
			try {
				CurrencyManager manager = EditingView.GridControl.BindingContext[EditingView.GridControl.DataSource, EditingView.GridControl.DataMember] as CurrencyManager;
				if(manager == null) return null;
				DataView dv = manager.List as DataView;
				if(dv != null) return dv.Table;
			}
			catch {
			}
			return null;
		}
		protected override void OnFillGrid() {
			if(baseView is GridView)
				((GridView)baseView).ExpandAllGroups();
		}
		private GridView View {
			get {
				if(grid.MainView is GridView) return grid.MainView as GridView;
				return null;
			}
		}
		void InitView() {
			ChangeColumnSelectorButtonVisibility(View != null);
			if(View != null) {
				View.ShowCustomizationForm += new System.EventHandler(this.view_ShowCustomizationForm);
				View.HideCustomizationForm += new System.EventHandler(this.view_HideCustomizationForm);
			}
		}
		void view_ShowCustomizationForm(object sender, System.EventArgs e) {
			OnShowCustomizationForm();
		}
		void view_HideCustomizationForm(object sender, System.EventArgs e) {
			OnHideCustomizationForm();
		}
		void View_Layout(object sender, EventArgs e) {
			SetLayoutChanged(true);
		}
	}
}
