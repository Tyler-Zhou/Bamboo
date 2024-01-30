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
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils.Frames;
namespace ICP.WF.Controls.Design
{
	[ToolboxItem(false)]
	public class PersistentRepositoryGridEditor : DevExpress.XtraEditors.Design.PersistentRepositoryEditor {
		private DevExpress.XtraEditors.SimpleButton btnClear;
		private System.ComponentModel.Container components = null;
		protected override bool AllowGlobalStore { get { return false; } }
		#region Component Designer generated code
		private void InitializeComponent() {
			this.btnClear = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.pnlControl)).BeginInit();
			this.pnlControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			this.pnlMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.horzSplitter)).BeginInit();
			this.SuspendLayout();
			this.btAdd.Location = new System.Drawing.Point(0, 4);
			this.btAdd.Size = new System.Drawing.Size(176, 24);
			this.btRemove.Location = new System.Drawing.Point(196, 4);
			this.btRemove.Size = new System.Drawing.Size(92, 24);
			this.pnlControl.Controls.AddRange(new System.Windows.Forms.Control[] {
																					 this.btnClear});
			this.btnClear.Location = new System.Drawing.Point(292, 4);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(150, 24);
			this.btnClear.TabIndex = 2;
			this.btnClear.Text = "Remove &Unused";
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.pgMain,
																		  this.splMain,
																		  this.horzSplitter,
																		  this.pnlMain,
																		  this.pnlControl,
																		  this.lbCaption});
			this.Name = "PersistentRepositoryGridEditor";
			((System.ComponentModel.ISupportInitialize)(this.pnlControl)).EndInit();
			this.pnlControl.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			this.pnlMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.horzSplitter)).EndInit();
			this.ResumeLayout(false);
		}
		#endregion
		#region Init & Ctor
		protected override void InitImages() {
			base.InitImages();
			btnClear.Image = DesignerImages16.Images[DesignerImages16ResetIndex];
		}
		protected override RepositoryItemCollection ItemCollection {
			get {
				return ((DevExpress.XtraGrid.Views.Base.BaseView)base.EditingObject).GridControl.RepositoryItems;
			}
		}
		protected override string DescriptionText { get { return "Manage the editors used for in-place editing within the cells (add, remove, modify their settings). Use the Columns page to assign editors to the selected view's columns. The editor can be removed from the collection if it isn't assigned to a column."; } }
		public PersistentRepositoryGridEditor() : base() {
			InitializeComponent();
			pgMain.BringToFront();
		}
		public override void InitComponent() {
			base.InitComponent();
		}
		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if(components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion
		#region Editing
		private void btnClear_Click(object sender, System.EventArgs e) {
			RemoveAllItems();
		}
		#endregion
	}
}
