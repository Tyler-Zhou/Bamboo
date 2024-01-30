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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
namespace ICP.WF.Controls.Design
{
	[ToolboxItem(false)]
	public class SchemeDesigner : DevExpress.XtraEditors.Frames.SchemeDesignerBase {
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
		private System.Windows.Forms.Label label1;
		private System.ComponentModel.Container components = null;
		public SchemeDesigner() {
			InitializeComponent();
		}
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
			this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pnlControls)).BeginInit();
			this.pnlControls.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			this.pnlMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
			this.SuspendLayout();
			this.pnlControls.Appearance.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(239)), ((System.Byte)(236)), ((System.Byte)(222)));
			this.pnlControls.Appearance.Options.UseBackColor = true;
			this.pnlControls.Controls.AddRange(new System.Windows.Forms.Control[] {
																					  this.comboBoxEdit1,
																					  this.label1});
			this.btApply.TabIndex = 1;
			this.comboBoxEdit1.EditValue = "";
			this.comboBoxEdit1.Location = new System.Drawing.Point(76, 4);
			this.comboBoxEdit1.Name = "comboBoxEdit1";
			this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
																												  new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEdit1.Properties.Items.AddRange(new object[] {
																		  "Default"});
			this.comboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.comboBoxEdit1.Size = new System.Drawing.Size(148, 21);
			this.comboBoxEdit1.TabIndex = 0;
			this.comboBoxEdit1.SelectedIndexChanged += new System.EventHandler(this.comboBoxEdit1_SelectedIndexChanged);
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Location = new System.Drawing.Point(4, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(84, 20);
			this.label1.TabIndex = 3;
			this.label1.Text = "Paint Style:";
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.pnlMain,
																		  this.horzSplitter,
																		  this.lbCaption});
			this.Name = "SchemeDesigner";
			((System.ComponentModel.ISupportInitialize)(this.pnlControls)).EndInit();
			this.pnlControls.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			this.pnlMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
			this.ResumeLayout(false);
		}
		#endregion
		#region Init & Ctor
		protected override string DescriptionText { 
			get { return "Select the painting scheme and click the Apply button. The appearance settings of the current view will be changed as a result."; }
		}
		private DevExpress.XtraGrid.Views.Base.BaseView EditingView { get { return EditingObject as DevExpress.XtraGrid.Views.Base.BaseView; } }
		private DevExpress.XtraGrid.Design.XAppearances xs;
		private DevExpress.XtraGrid.Design.XViewsPrinting XV;
		DevExpress.XtraGrid.Design.XAppearances XS {
			get {
				if(xs == null) xs = new DevExpress.XtraGrid.Design.XAppearances("System");
				return xs;
			}
		}
		private DevExpress.XtraGrid.GridControl GridPattern;
		void CreateGrid() {
			try {
				GridPattern = Activator.CreateInstance(EditingView.GridControl.GetType()) as GridControl;
			} catch {
				GridPattern = new DevExpress.XtraGrid.GridControl();
			}
			GridPattern.Dock = System.Windows.Forms.DockStyle.Fill;
			AddPreviewControl(GridPattern);
		}
		public override void InitComponent() {
			CreateGrid();
			try {
				DevExpress.XtraGrid.Views.Base.BaseView oldView = GridPattern.MainView;
				GridPattern.MainView = GridPattern.CreateView(EditingView.BaseInfo.ViewName);
				if(oldView != null) oldView.Dispose();
			} catch {}
			XV = new DevExpress.XtraGrid.Design.XViewsPrinting(GridPattern, true);
			StyleList.Items.Clear();
			StyleList.Items.AddRange(XS.FormatNames);
			DevExpress.XtraGrid.Design.GridAssign.AppearanceAssign(EditingView, GridPattern.MainView);
			for(int i = 0; i < EditingView.BaseInfo.PaintStyles.Count; i++) 
				comboBoxEdit1.Properties.Items.Add(EditingView.BaseInfo.PaintStyles[i].Name);
			comboBoxEdit1.EditValue = GridPattern.MainView.PaintStyleName;
			btApply.Enabled = false;
		}
		#endregion
		#region Editing
		protected override void LoadSchemePreview() {
			XS.LoadScheme(StyleList.SelectedItem.ToString(), GridPattern.MainView);
		}
		protected override void SetFormatNames(bool isEnabled) {
			XS.ShowNewStylesOnly = isEnabled;
			StyleList.Items.AddRange(XS.FormatNames);
		}
		protected override void btApply_Click(object sender, System.EventArgs e) {
			bool needFireChanged = false;
			if(EditingView.PaintStyleName != GridPattern.MainView.PaintStyleName) { 
			EditingView.PaintStyleName = GridPattern.MainView.PaintStyleName;
				needFireChanged = true;
			}
			if(StyleList.SelectedItem != null) {
				if(StyleList.SelectedIndex == 0) needFireChanged = true;
				XS.LoadScheme(StyleList.SelectedItem.ToString(), EditingView);
			}
			if(needFireChanged) FireChanged();
		}
		private void FireChanged() {
			IComponentChangeService srv = EditingView.GridControl.InternalGetService(typeof(IComponentChangeService)) as IComponentChangeService;
			if(srv != null) srv.OnComponentChanged(EditingView, null, null, null);
		}
		private void comboBoxEdit1_SelectedIndexChanged(object sender, System.EventArgs e) {
			GridPattern.MainView.PaintStyleName = comboBoxEdit1.Text;
			btApply.Enabled = true;
		}
		#endregion
	}
}
