#region Copyright (c) 2000-2010 Developer Express Inc.
/*
{*******************************************************************}
{                                                                   }
{       Developer Express .NET Component Library                    }
{                                                                   }
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
using DevExpress.Utils.Design;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.Data;
using DevExpress.Utils.Frames;

namespace ICP.WF.Controls.Design
{
	[ToolboxItem(false)]
    public class LWXtraPGFrame : DevExpress.Utils.Frames.XtraFrame, DevExpress.Utils.Frames.IEmbeddedFrame
    {
		protected DevExpress.XtraEditors.SplitterControl splMain;
		public DevExpress.Utils.Frames.PropertyGridEx pgMain;
		public PanelControl pnlControl;
        DevExpress.Utils.Frames.EmbeddedFrameInit embeddedFrameInit;
		public LWXtraPGFrame(): this(0){}
		public LWXtraPGFrame(int i): base(i) {
			InitializeComponent();
			pgMain.SelectedObjectsChanged += new EventHandler(OnPropertyGridSelectedObjectChanged);
			pgMain.PropertyValueChanged += new PropertyValueChangedEventHandler(OnPropertyGridPropertyValueChanged);
			pgMain.EventHandlerAdded += new EventHandler(OnPropertyGridEventHandlerAdded);
			pgMain.CommandsVisibleIfAvailable = false;
			pgMain.HelpVisible = true;
			pgMain.DrawFlat = true;
			HavePG = true;
			this.embeddedFrameInit = null;
		}
		static readonly object propertyValueChanged = new object();
		public event PropertyValueChangedEventHandler PropertyValueChanged {
			add { Events.AddHandler(propertyValueChanged, value); }
			remove { Events.RemoveHandler(propertyValueChanged, value); }
		}
		protected virtual void OnPropertyValueChanged(PropertyValueChangedEventArgs e) {
			if(FrameOwner != null) 
				FrameOwner.SourceObjectChanged(this, e);
			PropertyValueChangedEventHandler handler = (PropertyValueChangedEventHandler)this.Events[propertyValueChanged];
			if(handler != null) handler(this, e);
		}
		public override void StoreGlobalProperties(PropertyStore globalStore) {
			if(AllowGlobalStore) 
				globalStore.AddProperty("MainPanel", pnlMain.Width);
			globalStore.AddProperty("PropertyGridSort", pgMain.PropertySort);
			globalStore.AddProperty("PropertyGridHelpVisible", pgMain.HelpVisible);
		}
		public override void RestoreGlobalProperties(PropertyStore globalStore) {
			if(AllowGlobalStore) 
				pnlMain.Width = globalStore.RestoreIntProperty("MainPanel", pnlMain.Width);
			pgMain.PropertySort = (PropertySort)globalStore.RestoreProperty("PropertyGridSort", typeof(PropertySort), pgMain.PropertySort);
			pgMain.HelpVisible = globalStore.RestoreBoolProperty("PropertyGridHelpVisible", pgMain.HelpVisible);
		}
		public override void StoreLocalProperties(PropertyStore localStore) {
			if(!AllowGlobalStore) 
				localStore.AddProperty("MainPanel", pnlMain.Width);
		}
		public override void RestoreLocalProperties(PropertyStore localStore) {
			if(!AllowGlobalStore) 
				pnlMain.Width = localStore.RestoreIntProperty("MainPanel", pnlMain.Width);
		}
		protected override DockStyle DescriptionPanelDock { 
			get { return EmbeddedFrameInit != null ? EmbeddedFrameInit.DescriptionPanelDock : base.DescriptionPanelDock; } 
		}
		protected virtual void UpdatePropertyGridSite() {
			if(DesignMode) return;
			pgMain.Site = null;
			IServiceProvider provider = GetPropertyGridServiceProvider();
			if(provider != null) {
				pgMain.Site = new MySite(provider, pgMain as IComponent);
				pgMain.PropertyTabs.AddTabType(typeof(System.Windows.Forms.Design.EventsTab));
			}
		}
		protected virtual IServiceProvider GetPropertyGridServiceProvider() {
			object selObject = null;
			if(pgMain.SelectedObjects != null && pgMain.SelectedObjects.Length > 0) 
				selObject = pgMain.SelectedObjects[0];
			else selObject = pgMain.SelectedObject;
			IDXObjectWrapper wrapper = selObject as IDXObjectWrapper;
			if(wrapper != null) selObject = wrapper.SourceObject;
			Component selComponent = selObject as Component;
			if(selComponent != null && selComponent.Site != null) {
				return selComponent.Site;
			}
			if(selObject != null && EditingComponent.Site != null) {
				return EditingComponent.Site;
			}
			return null;
		}
		protected virtual void OnPropertyGridSelectedObjectChanged(object sender, EventArgs e) {
			UpdatePropertyGridSite();
			pgMain.ShowEvents(true);
		}
		protected virtual void OnPropertyGridEventHandlerAdded(object sender, EventArgs e) {
			if(!StackTraceHelper.CheckStackFrame("DoubleClickPropertyValue", null))
				return;
			if(ParentForm != null)
				ParentForm.Close();
		}
		protected virtual void OnPropertyGridPropertyValueChanged(object sender, PropertyValueChangedEventArgs e) {
			OnPropertyValueChanged(e);
		}
		protected virtual object[] SelectedObjects {
			get {
				if(SelectedObject == null) return new object[0];
				return new object[] { SelectedObject };
			}
		}
		protected virtual object SelectedObject { get { return EditingObject; } }
		protected virtual object GetPropertyGridObject(object obj) {
			if(!HasFilteredProperties) return GetNestedPropertyGridObject(obj);
			object[] sampleObjects = GetPropertyGridSampleObjects(obj);
			if(sampleObjects != null)
				for(int i = 0; i < sampleObjects.Length; i ++)
					sampleObjects[i] = GetNestedPropertyGridObject(sampleObjects[i]);
			return new FilterObject(GetNestedPropertyGridObject(obj), sampleObjects, EmbeddedFrameInit.Properties);
		}
		protected virtual object GetNestedPropertyGridObject(object obj) {
			return obj;
		}
		protected virtual object[] GetPropertyGridSampleObjects(object obj) {
			return SampleObjects;
		}
		protected override string GetDescriptionTextCore() {
			if(EmbeddedFrameInit != null)
				return EmbeddedFrameInit.Description;
			else return base.GetDescriptionTextCore();
		}
		protected virtual void RefreshPropertyGrid() {
			RefreshPropertyGridCore();
		}
		protected virtual void RefreshPropertyGridCore() {
			object[] selObjects = SelectedObjects;
			if(selObjects == null || selObjects.Length == 0) 
				pgMain.SelectedObject = null;
			else {
				for(int i = 0; i < selObjects.Length; i ++)
					selObjects[i] = GetPropertyGridObject(selObjects[i]);
				if(selObjects.Length == 1)
					pgMain.SelectedObject = selObjects[0];
				else {
					try {
						pgMain.SelectedObjects = selObjects; 
					} catch {}
				}
				SetupPropertyGridAfterRefresh();
			}
		}
		protected void SetupPropertyGridAfterRefresh() {
			if(EmbeddedFrameInit == null) return;
			if(EmbeddedFrameInit.ExpandAllProperties)
				pgMain.ExpandAllGridItems();
			else {
				for(int i = 0; i < EmbeddedFrameInit.ExpandedPropertiesOnStart.Length; i ++)
					pgMain.ExpandProperty(EmbeddedFrameInit.ExpandedPropertiesOnStart[i]);
			}
			if(EmbeddedFrameInit.SelectedPropertyOnStart != string.Empty)
				pgMain.SelectItem(EmbeddedFrameInit.SelectedPropertyOnStart);
		}
		protected EmbeddedFrameInit EmbeddedFrameInit { get { return embeddedFrameInit; } }
		protected IEmbeddedFrameOwner FrameOwner { get { return EmbeddedFrameInit != null ? EmbeddedFrameInit.FrameOwner : null; } }
		protected bool IsEmbedded { get { return FrameOwner != null; } }
		protected object[] SampleObjects { get { return EmbeddedFrameInit != null ? EmbeddedFrameInit.SampleObjects : null; } }
		protected bool HasFilteredProperties {
			get {
				return IsEmbedded && EmbeddedFrameInit.Properties != null && 
					EmbeddedFrameInit.Properties.Length > 0;
			}
		}
        Control DevExpress.Utils.Frames.IEmbeddedFrame.Control { get { return this; } }
        void DevExpress.Utils.Frames.IEmbeddedFrame.InitEmbeddedFrame(DevExpress.Utils.Frames.EmbeddedFrameInit frameInit)
        {
			this.embeddedFrameInit = frameInit;
			InitFrame(frameInit.EditingObject, string.Empty, null);
		}
        void DevExpress.Utils.Frames.IEmbeddedFrame.RefreshPropertyGrid()
        {
			RefreshPropertyGrid();
		}
        void DevExpress.Utils.Frames.IEmbeddedFrame.SelectProperty(string propertyName)
        {
			pgMain.SelectItem(propertyName);
		}
        void DevExpress.Utils.Frames.IEmbeddedFrame.ShowPropertyGridToolBar(bool show)
        {
			pgMain.ToolbarVisible = show;
		}
        void DevExpress.Utils.Frames.IEmbeddedFrame.SetPropertyGridSortMode(PropertySort propertySort)
        {
			pgMain.PropertySort = propertySort;
		}
		protected override void Dispose(bool disposing) {
			if(disposing) {
				if(!pgMain.IsDisposed)
					pgMain.SelectedObject = null;
			}
			base.Dispose(disposing);
		}
		public class MySite : ISite {
			IServiceProvider sp;
			IComponent comp;
			public MySite(IServiceProvider sp, IComponent comp) {
				this.sp = sp;
				this.comp = comp;
			}
			IComponent ISite.Component {
				get { return comp;}
			}
			IContainer ISite.Container {
				get { return null; }
			}
			bool ISite.DesignMode {
				get { return false; }
			}
			string ISite.Name {
				get { return null; }
				set { }
			}
			object IServiceProvider.GetService(Type t) {
				if(t.Equals(typeof(System.ComponentModel.Design.IDesignerHost)))
					return sp.GetService(t);
				return null;
			}
		}
		#region Component Designer generated code
		private void InitializeComponent() {
			this.pgMain = new DevExpress.Utils.Frames.PropertyGridEx();
			this.pnlControl = new PanelControl();
            this.pgMain.BrowsableAttributes = new AttributeCollection(new Attribute[] { ICP.Framework.CommonLibrary.Attributes.ICPBrowsableAttribute.Yes });
			this.splMain = new DevExpress.XtraEditors.SplitterControl();
			this.SuspendLayout();
			this.lbCaption.Size = new System.Drawing.Size(456, 24);
			this.pnlMain.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlMain.Location = new System.Drawing.Point(0, 60);
			this.pnlMain.Size = new System.Drawing.Size(160, 252);
			this.horzSplitter.Size = new System.Drawing.Size(456, 4);
			this.pgMain.CommandsVisibleIfAvailable = true;
			this.pgMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pgMain.DrawFlat = false;
			this.pgMain.HelpVisible = false;
			this.pgMain.LargeButtons = false;
			this.pgMain.BackColor = SystemColors.Control;
			this.pgMain.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.pgMain.Location = new System.Drawing.Point(164, 60);
			this.pgMain.Name = "pgMain";
			this.pgMain.Size = new System.Drawing.Size(292, 252);
			this.pgMain.TabIndex = 2;
			this.pgMain.Text = "PropertyGrid";
			this.pgMain.ViewBackColor = System.Drawing.SystemColors.Window;
			this.pgMain.ViewForeColor = System.Drawing.SystemColors.WindowText;
			this.pgMain.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgMain_PropertyValueChanged);
			this.pnlControl.BorderStyle = BorderStyles.NoBorder;
			this.pnlControl.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlControl.DockPadding.All = 4;
			this.pnlControl.Location = new System.Drawing.Point(0, 28);
			this.pnlControl.Name = "pnlControl";
			this.pnlControl.Size = new System.Drawing.Size(456, 32);
			this.pnlControl.TabIndex = 0;
			this.splMain.Location = new System.Drawing.Point(160, 60);
			this.splMain.MinExtra = 170;
			this.splMain.MinSize = 150;
			this.splMain.Name = "splMain";
			this.splMain.Size = new System.Drawing.Size(4, 252);
			this.splMain.TabIndex = 3;
			this.splMain.TabStop = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.pgMain,
																		  this.splMain,
																		  this.pnlMain,
																		  this.pnlControl,
																		  this.horzSplitter,
																		  this.lbCaption});
			this.Name = "XtraPGFrame";
			this.Size = new System.Drawing.Size(456, 312);
			this.ResumeLayout(false);
		}
		#endregion
		protected virtual void pgMain_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e) {
		}
	}
}
