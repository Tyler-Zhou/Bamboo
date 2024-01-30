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
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Design;

namespace ICP.WF.Controls.Design
{
	[ToolboxItem(false)]
	public class LevelSelector : System.Windows.Forms.UserControl {
		ViewTree tree;
		private System.ComponentModel.IContainer components;
		public System.Windows.Forms.Panel panel;
		public DevExpress.XtraEditors.SimpleButton btDesigner;
		bool allowDesignerButton = true;
		GridControl editingGrid = null;
		private System.Windows.Forms.Panel pnlClient;
        private System.Windows.Forms.Label lblLine;
		private PanelControl pnlButton;
		public LevelSelector() {
			InitializeComponent();
			SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			//CreateIcons();
		}
		void CreateIcons() {
			ImageList iml = DevExpress.Utils.ResourceImageHelper.CreateImageListFromResources("ICP.WF.Controls.Designer.Designer.ViewIcons.bmp", typeof(LevelSelector).Assembly, new Size(16, 16), Color.Magenta);
			tree.Images = iml;
		}
		protected override void OnCreateControl() {
			base.OnCreateControl();
			InitScrPanel();
			tree.BorderStyle = BorderStyles.NoBorder;
			UpdateControls();
		}
		public virtual ViewTree Tree { get { return tree; } }
		public virtual bool AllowDesignerButton {
			get { return allowDesignerButton; }
			set {
				if(AllowDesignerButton == value) return;
				this.allowDesignerButton = value;
				UpdateControls();
			}
		}
		public virtual void UpdateLevels() {
			tree.PopulateTree();
		}
		private PanelControl scrPanel;
		private void InitScrPanel() {
			if(scrPanel == null) {
				scrPanel = new PanelControl();
				this.scrPanel.BorderStyle = BorderStyles.NoBorder;
				pnlClient.Controls.Add(scrPanel);
				scrPanel.Location = new Point(0, 0);
				scrPanel.Width = 1;
			}
		}
		protected virtual void UpdateControls() {
			btDesigner.Visible = AllowDesignerButton;
		}
		public GridControl EditingGrid {
			get { return editingGrid; }
			set { 
				if(EditingGrid == value) return;
				editingGrid = value; 
				tree.Grid = value;
			}
		}
		public LWGridControlDesigner Designer {
			get {
				if(EditingGrid == null) return null;
				IDesignerHost host = EditingGrid.InternalGetService(typeof(IDesignerHost)) as IDesignerHost;
				if(host == null) return null;
				return host.GetDesigner(EditingGrid) as LWGridControlDesigner;
			}
		}
		protected override void Dispose(bool disposing) {
			if(disposing) {
				if(components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#region Component Designer generated code
		private void InitializeComponent()
		{
            this.tree = new ICP.WF.Controls.Design.ViewTree();
            this.panel = new System.Windows.Forms.Panel();
            this.pnlClient = new System.Windows.Forms.Panel();
            this.pnlButton = new DevExpress.XtraEditors.PanelControl();
            this.lblLine = new System.Windows.Forms.Label();
            this.btDesigner = new DevExpress.XtraEditors.SimpleButton();
            this.panel.SuspendLayout();
            this.pnlClient.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlButton)).BeginInit();
            this.pnlButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // tree
            // 
            this.tree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tree.Grid = null;
            this.tree.Images = null;
            this.tree.Location = new System.Drawing.Point(0, 0);
            this.tree.Name = "tree";
            this.tree.Size = new System.Drawing.Size(278, 34);
            this.tree.TabIndex = 0;
            this.tree.UseInternalEditor = true;
            this.tree.TreeLayoutChanged += new System.EventHandler(this.levels_TreeLayoutChanged);
            // 
            // panel
            // 
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel.Controls.Add(this.pnlClient);
            this.panel.Controls.Add(this.pnlButton);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(280, 68);
            this.panel.TabIndex = 1;
            // 
            // pnlClient
            // 
            this.pnlClient.AutoScroll = true;
            this.pnlClient.Controls.Add(this.tree);
            this.pnlClient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlClient.Location = new System.Drawing.Point(0, 0);
            this.pnlClient.Name = "pnlClient";
            this.pnlClient.Size = new System.Drawing.Size(278, 34);
            this.pnlClient.TabIndex = 2;
            this.pnlClient.Resize += new System.EventHandler(this.pnlClient_Resize);
            // 
            // pnlButton
            // 
            this.pnlButton.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlButton.Controls.Add(this.lblLine);
            this.pnlButton.Controls.Add(this.btDesigner);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButton.Location = new System.Drawing.Point(0, 34);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(278, 32);
            this.pnlButton.TabIndex = 4;
            // 
            // lblLine
            // 
            this.lblLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLine.Location = new System.Drawing.Point(0, 0);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(278, 1);
            this.lblLine.TabIndex = 3;
            // 
            // btDesigner
            // 
            this.btDesigner.Location = new System.Drawing.Point(144, 4);
            this.btDesigner.Name = "btDesigner";
            this.btDesigner.Size = new System.Drawing.Size(128, 24);
            this.btDesigner.TabIndex = 2;
            this.btDesigner.Text = "Run Designer";
            this.btDesigner.Click += new System.EventHandler(this.btDesigner_Click);
            // 
            // LevelSelector
            // 
            this.Controls.Add(this.panel);
            this.Name = "LevelSelector";
            this.Size = new System.Drawing.Size(280, 68);
            this.panel.ResumeLayout(false);
            this.pnlClient.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlButton)).EndInit();
            this.pnlButton.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		private void btDesigner_Click(object sender, System.EventArgs e) {
			tree.CancelEdit();
		}
		public virtual void UpdateSize() {
			Size levelsSize = tree.CalcBestSize();
			levelsSize.Width = Math.Max(levelsSize.Width, 100);
			levelsSize.Height = Math.Max(levelsSize.Height, 100);
			if(AllowDesignerButton) {
				Size size = levelsSize;
				size.Height = Math.Min(levelsSize.Height + pnlButton.Height + 2, EditingGrid.Height - 50);
				size.Width +=2;
				this.ClientSize = size;
				if(scrPanel != null) 
					scrPanel.Height = levelsSize.Height;
			} else {
				if(scrPanel != null && scrPanel.Size != levelsSize)
					scrPanel.Size = levelsSize;
			}
		}
		private void levels_TreeLayoutChanged(object sender, System.EventArgs e) {
			UpdateSize();
		}
		private void pnlClient_Resize(object sender, System.EventArgs e) {
			UpdateSize();
		}
		private void btPopulateDetail_Click(object sender, System.EventArgs e) {
			tree.PopulateDetailTree();
		}
	}
}
