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
using System.Reflection;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraTab;
using DevExpress.XtraEditors.Design;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Views.Printing;
namespace ICP.WF.Controls.Design
{
	[ToolboxItem(false)]
	public class PrintAppearancesDesigner : DevExpress.XtraEditors.Frames.AppearancesDesignerSimple {
		private System.ComponentModel.Container components = null;
		public PrintAppearancesDesigner() {
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
			((System.ComponentModel.ISupportInitialize)(this.gcAppearances)).BeginInit();
			this.gcAppearances.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gcPreview)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.beAppearances.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlButtons)).BeginInit();
			this.pnlButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.lbcAppearances)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlControl)).BeginInit();
			this.pnlControl.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
			this.pnlMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.horzSplitter)).BeginInit();
			this.SuspendLayout();
			this.beAppearances.Size = new System.Drawing.Size(148, 22);
			this.pnlButtons.Location = new System.Drawing.Point(6, 20);
			this.pnlButtons.Size = new System.Drawing.Size(148, 25);
			this.lbcAppearances.Location = new System.Drawing.Point(6, 45);
			this.lbcAppearances.Size = new System.Drawing.Size(148, 285);
			this.pgMain.Location = new System.Drawing.Point(164, 60);
			this.pgMain.Size = new System.Drawing.Size(424, 336);
			this.pnlControl.Size = new System.Drawing.Size(588, 32);
			this.lbCaption.Size = new System.Drawing.Size(588, 24);
			this.horzSplitter.Size = new System.Drawing.Size(588, 4);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.pgMain,
																		  this.splMain,
																		  this.pnlMain,
																		  this.horzSplitter,
																		  this.pnlControl,
																		  this.lbCaption});
			this.Name = "PrintAppearancesDesigner";
			this.Size = new System.Drawing.Size(588, 396);
			((System.ComponentModel.ISupportInitialize)(this.gcAppearances)).EndInit();
			this.gcAppearances.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gcPreview)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.beAppearances.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlButtons)).EndInit();
			this.pnlButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.lbcAppearances)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlControl)).EndInit();
			this.pnlControl.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
			this.pnlMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.horzSplitter)).EndInit();
			this.ResumeLayout(false);
		}
		#endregion
		private DevExpress.XtraGrid.Views.Base.BaseView EditingView { get { return EditingObject as DevExpress.XtraGrid.Views.Base.BaseView; } }
		protected override BaseAppearanceCollection Appearances { get { return EditingView.AppearancePrint; } }
		protected override Image AppearanceImage { get { return null; } }
		protected override string DescriptionText { get { return "Select one or more of the Appearance objects to customize the printing appearances of the corresponding visual elements."; } }
		protected override XtraTabControl CreateTab() {
			return DevExpress.XtraEditors.Design.FramesUtils.CreateTabProperty(this, new Control[] {pgMain}, new string[] {"Properties"});
		}
		BaseAppearanceCollection GetPrintAppearance(BaseView view) {
			MethodInfo method = view.GetType().GetMethod("CreatePrintInfoCore", BindingFlags.NonPublic | BindingFlags.Instance);
			BaseViewPrintInfo pi = method.Invoke(view, new object[] { new PrintInfoArgs(view)}) as BaseViewPrintInfo;
			return pi.AppearancePrint ;
		}
		protected override void SetSelectedObject() {
			ArrayList arr = new ArrayList();
			BaseAppearanceCollection bc = GetPrintAppearance(EditingView);
			foreach(AppearanceObject obj in this.SelectedObjects) {
				arr.Add(bc.GetAppearance(obj.Name));
			}
			Preview.SetAppearance(arr.ToArray());
		}
		protected override void SetDefault() {
			EditingView.BeginUpdate();
			base.SetDefault();
			SetEvenOddStyle();
			SetSelectedObject();
			EditingView.EndUpdate();
		}
		protected override void pgMain_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e) {
			if(EditingView is GridView)
				((GridView)EditingView).OptionsPrint.UsePrintStyles = true;
			SetEvenOddStyle();
			SetSelectedObject();
			base.pgMain_PropertyValueChanged(s, e);
		}
		int IsAppearanceNeedEnabled(string name) {
			if(lbcAppearances.SelectedIndices.IndexOf(lbcAppearances.FindString(name)) > -1) {
				return EditingView.AppearancePrint.GetAppearance(name).ShouldSerialize() ? 1 : 0;
			}
			return -1;
		}
		void SetEvenOddStyle() {
			int isEnabled = IsAppearanceNeedEnabled("EvenRow");
			if(isEnabled > -1) 
				if(EditingView is GridView)
					((GridView)EditingView).OptionsPrint.EnableAppearanceEvenRow = isEnabled == 1;
			isEnabled = IsAppearanceNeedEnabled("OddRow");
			if(isEnabled > -1) 
				if(EditingView is GridView)
					((GridView)EditingView).OptionsPrint.EnableAppearanceOddRow = isEnabled == 1;
		}
	}
}
