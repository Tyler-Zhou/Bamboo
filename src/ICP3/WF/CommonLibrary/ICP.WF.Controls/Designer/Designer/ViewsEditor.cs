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
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.Utils.Design;
using DevExpress.Utils.Frames;
using DevExpress.XtraGrid.Design;
using DevExpress.XtraEditors.Design;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Registrator;
namespace ICP.WF.Controls.Design
{
	[ToolboxItem(false)]
	public class ViewsEditor : BaseRepositoryEditor {
		private System.ComponentModel.IContainer components;
		System.Windows.Forms.ImageList imageList1;
		protected override bool AllowGlobalStore { get { return false; } }
		protected override string DescriptionText { get { return "Manage the views in the grid's repository (add, remove, modify settings)."; } }
		public ViewsEditor() : base(-1) {
			InitializeComponent();
			ImageList viewRepositoryIcons = DevExpress.Utils.ResourceImageHelper.CreateImageListFromResources("DevExpress.XtraGrid.Design.Designer.ViewRepositoryIcons.bmp", typeof(LevelSelector).Assembly, new Size(16, 16), Color.Magenta);
			foreach(Image img in viewRepositoryIcons.Images){
				imageList1.Images.Add(img);
			}
			firstViewRepositoryImage  = imageList1.Images.Count - viewRepositoryIcons.Images.Count;
			this.listView1.SmallImageList = this.imageList1;
		}
		int firstViewRepositoryImage = 0;
		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if(components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#region Component Designer generated code
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ViewsEditor));
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			((System.ComponentModel.ISupportInitialize)(this.pnlControl)).BeginInit();
			this.pnlControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			this.pnlMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.horzSplitter)).BeginInit();
			this.SuspendLayout();
			this.btAdd.Size = new System.Drawing.Size(180, 24);
			this.btAddM.Location = new System.Drawing.Point(184, 4);
			this.btRemove.Location = new System.Drawing.Point(208, 4);
			this.listView1.Location = new System.Drawing.Point(6, 20);
			this.listView1.Size = new System.Drawing.Size(160, 250);
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.pgMain,
																		  this.splMain,
																		  this.pnlMain,
																		  this.horzSplitter,
																		  this.pnlControl,
																		  this.lbCaption});
			this.Name = "ViewsEditor";
			((System.ComponentModel.ISupportInitialize)(this.pnlControl)).EndInit();
			this.pnlControl.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			this.pnlMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.horzSplitter)).EndInit();
			this.ResumeLayout(false);
		}
		#endregion
		BaseView EditingView { get { return EditingObject as BaseView;} }
		protected override int FindElement(object component) {
			BaseView item = component as BaseView;
			if(item.BaseInfo == null) return -1;
			if(item != null) return EditingView.GridControl.AvailableViews.IndexOf(item.BaseInfo);
			return -1;
		}
		protected override object GetDefaultElement() {
			if(EditingView == null) return null;
			if(lastElement == null) lastElement = EditingView.GridControl.AvailableViews["GridView"];
			return lastElement;
		}
		protected override bool GetElementVisible(object element) { return true; }
		protected override object GetElement(int index) {
			if(index < 0 || index >= GetElementCount()) return null;
			return EditingView.GridControl.AvailableViews[index];
		}
		protected override object GetElementByName(string name) {
			return EditingView.GridControl.AvailableViews[name];
		}
		protected override string GetElementText(object element) {
			BaseInfoRegistrator info = element as BaseInfoRegistrator;
			if(info == null) return "GridView";
			return info.ViewName;
		}
		protected override int GetElementCount() {
			return EditingView.GridControl.AvailableViews.Count;
		}
		protected override int GetElementImageIndex(string name) { 
			switch(name) {
				case "CardView": return firstViewRepositoryImage + 0;
				case "BandedGridView": return firstViewRepositoryImage + 1;
				case "GridView": return firstViewRepositoryImage + 2;
				case "AdvBandedGridView": return firstViewRepositoryImage + 3;
				case "LayoutView": return firstViewRepositoryImage + 5;
			}
			return -1; 
		}
		protected override ArrayList GetSortElementList() {
			ArrayList list = new ArrayList();
			foreach(BaseInfoRegistrator info in EditingView.GridControl.AvailableViews)
				list.Add(info.ViewName);
			list.Sort();
			return list;
		}
		protected override Image GetElementImage(object element) {
			BaseInfoRegistrator info = element as BaseInfoRegistrator;
			if(info == null) return null;
			int image = GetElementImageIndex(info.ViewName);
			if(image >= 0 && image < this.imageList1.Images.Count) return this.imageList1.Images[image];
			return null; 
		}
		protected override void AddNewItem(object item) {
			BaseInfoRegistrator info = item as BaseInfoRegistrator;
			if(info != null) {
				EditingView.GridControl.ViewCollection.Add(EditingView.GridControl.CreateView(info.ViewName));
				this.lastElement = info;
				UpdateButtonText();
				RefreshListBox();
				listView1.Items[listView1.Items.Count - 1].Selected = true;
			}
		}
		protected override IList GetComponentCollection() { return EditingView.GridControl.ViewCollection; } 
		protected override bool CanRemoveComponent(object component) {
			BaseView item = component as BaseView;
			return item != EditingView && item != null && item.LinkCount == 0;
		}
		protected override void OnSelectedItemChanged(object item) {
			BaseView view = item as BaseView;
			if(view == null) return;
			RefreshPropertyGrid();
		}
		protected override string WarningText {
			get {
				return "This will remove all unassigned views. Are you sure?";
			}
		}
	}
}
