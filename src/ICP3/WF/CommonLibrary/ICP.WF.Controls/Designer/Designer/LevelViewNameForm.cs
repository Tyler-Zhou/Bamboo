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
namespace ICP.WF.Controls.Design
{
	public class LevelViewNameForm : System.Windows.Forms.Form {
		private DevExpress.XtraEditors.SimpleButton btOk;
		private DevExpress.XtraEditors.SimpleButton btCancel;
		public DevExpress.XtraEditors.TextEdit teEdit;
		private System.ComponentModel.Container components = null;
		public LevelViewNameForm() {
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
		#region Windows Form Designer generated code
		private void InitializeComponent() {
			this.teEdit = new DevExpress.XtraEditors.TextEdit();
			this.btOk = new DevExpress.XtraEditors.SimpleButton();
			this.btCancel = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.teEdit.Properties)).BeginInit();
			this.SuspendLayout();
			this.teEdit.EditValue = "teEdit";
			this.teEdit.Location = new System.Drawing.Point(12, 15);
			this.teEdit.Name = "teEdit";
			this.teEdit.Size = new System.Drawing.Size(248, 20);
			this.teEdit.TabIndex = 0;
			this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btOk.Location = new System.Drawing.Point(100, 48);
			this.btOk.Name = "btOk";
			this.btOk.Size = new System.Drawing.Size(73, 24);
			this.btOk.TabIndex = 1;
			this.btOk.Text = "&Ok";
			this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btCancel.Location = new System.Drawing.Point(188, 48);
			this.btCancel.Name = "btCancel";
			this.btCancel.Size = new System.Drawing.Size(73, 24);
			this.btCancel.TabIndex = 2;
			this.btCancel.Text = "&Cancel";
			this.AcceptButton = this.btOk;
			this.CancelButton = this.btCancel;
			this.ClientSize = new System.Drawing.Size(272, 82);
			this.Controls.Add(this.btCancel);
			this.Controls.Add(this.btOk);
			this.Controls.Add(this.teEdit);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "LevelViewNameForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Enter new level name";
			((System.ComponentModel.ISupportInitialize)(this.teEdit.Properties)).EndInit();
			this.ResumeLayout(false);
		}
		#endregion
	}
}
