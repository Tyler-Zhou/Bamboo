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
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Windows.Forms.ComponentModel;
using System.Collections;
using System.Reflection;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Design;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils.Design;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Blending;
using DevExpress.XtraGrid.Views.Layout;
using DevExpress.XtraGrid.Views.Layout.ViewInfo;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Registrator;
using DevExpress.Utils;
using DevExpress.XtraGrid.Design;
using DevExpress.XtraGrid.Views.Base.Handler;

namespace ICP.WF.Controls.Design
{
	public class BandedGridViewDesignTimeDesigner : ColumnViewDesigner {
		public override ICollection AssociatedComponents {
			get {
				BandedGridView view = Component as BandedGridView;
				if(view == null) return base.AssociatedComponents;
				ArrayList controls = new ArrayList();
				foreach(GridBand band in view.Bands) {
					AddBand(controls, band);
				}
				AddBase(controls);
				return controls;
			}
		}
		void AddBand(ArrayList controls, GridBand band) {
			if(band == null) return;
			controls.Add(band);
			foreach(GridBand b in band.Children) {
				AddBand(controls, b);
			}
		}
		new void AddBase(ArrayList controls) {
			ICollection coll = base.AssociatedComponents;
			foreach(object obj in coll) {
				if(controls.Contains(obj)) continue;
				controls.Add(obj);
			}
		}
	}
	public class LWGridControlDesigner : BaseControlDesigner {
		DesignerVerbCollection defaultVerbs;
		DesignerVerbCollection layoutViewVerbs;
		LevelSelector levelSelector;
		bool levelDesignerVisible;
		frmDesigner editor;
#if DXWhidbey
		protected override bool AllowHookDebugMode { get { return true; } }
#endif
		protected override bool AllowInheritanceWrapper { get { return true; } }
		public LWGridControlDesigner() {
			this.levelDesignerVisible = true;
			PropertyStore ps = new PropertyStore(frmDesigner.GridSettings);
			ps.Restore();
			this.levelDesignerVisible = ps.RestoreBoolProperty ("ShowLevelDesigner", this.levelDesignerVisible);
			this.levelSelector = new LevelSelector();
			this.levelSelector.Visible = this.levelDesignerVisible;
			this.levelSelector.Name = "LevelSelector";
			this.levelSelector.Tree.UseInternalEditor = false;
			this.levelSelector.btDesigner.Click += new EventHandler(OnDesignerClick);
			this.levelSelector.SizeChanged += new EventHandler(OnSelector_SizeChanged);
			this.levelSelector.Tree.ShownEditor += new EventHandler(OnSelector_ShownEditor);
			this.levelSelector.Tree.HiddenEditor += new EventHandler(OnSelector_HiddenEditor);
			this.levelSelector.Tree.SelectedNodeChanged += new ViewNodeEventHandler(OnSelected_SelectedChanged);
			defaultVerbs =	new DesignerVerbCollection(
								new DesignerVerb[] {
									 //  new DesignerVerb("About", new EventHandler(OnAboutClick)),
									   new DesignerVerb("Run Designer", new EventHandler(OnDesignerClick))
#if !DXWhidbey
									 //  ,new DesignerVerb("Show Level Designer", new EventHandler(OnSwitchLevelDesigner))
#endif
								   }
			);
			layoutViewVerbs =	new DesignerVerbCollection(
								new DesignerVerb[] {
									  // new DesignerVerb("About", new EventHandler(OnAboutClick)),
									   new DesignerVerb("Run Designer", new EventHandler(OnDesignerClick))
									   //new DesignerVerb("Customize Layout", new EventHandler(OnLayoutDesignerClick))
#if !DXWhidbey
									 //  ,new DesignerVerb("Show Level Designer", new EventHandler(OnSwitchLevelDesigner))
#endif
								   }
			);
			editor = null;
			UpdateLevelDesignerVerb();
		}
		protected override bool AllowEditInherited { get { return false; } }
		public virtual bool LevelDesignerVisible {
			get { return levelDesignerVisible; }
			set {
				if(LevelDesignerVisible == value) return;
				levelDesignerVisible = value;
				UpdateLevelDesignerVisibility();
				UpdateLevelDesignerVerb();
			}
		}
		protected internal void UpdateLevelDesignerVisibility() {
			if(levelSelector != null) 
				levelSelector.Visible = LevelDesignerVisible && AllowDesigner;
		}
#if !DXWhidbey
		protected void OnSwitchLevelDesigner(object sender, EventArgs e) {
			LevelDesignerVisible = !LevelDesignerVisible;
			UpdateLevelDesignerVerb();
		}
		protected void UpdateLevelDesignerVerb() {
			//this.verbs[2].Checked = LevelDesignerVisible;
		}
#else
		protected override void OnDebuggingStateChanged() {
			if(levelSelector != null) levelSelector.Enabled = !DebuggingState;
			base.OnDebuggingStateChanged();
		}
		protected void UpdateLevelDesignerVerb() { }
#endif
		protected void OnSelected_SelectedChanged(object sender, ViewNodeEventArgs e) {
		}
		protected virtual void SwitchVisibleView(BaseView view) {
			if(DebuggingStateCheckHelper.PreventDebuggingCrashWhileDebugging(this.Grid,false)) return;
			if(view == null) return;
			string methodName = "ZoomView";
			if(view == Grid.MainView) {
				view = null;
				methodName = "NormalView";
			}
			MethodInfo mi = typeof(GridControl).GetMethod(methodName, BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.NonPublic);
			mi.Invoke(Grid, new Object[] { view });
		}
		protected void OnSelector_ShownEditor(object sender, EventArgs e) {
			UnhookChildControls(Selector);
		}
		protected void OnSelector_HiddenEditor(object sender, EventArgs e) {
		}
		protected void OnAboutClick(object sender, EventArgs e) {
			GridControl.About();
		}
		public virtual void ShowDesigner(BaseView view, string activeItem) {
			if(DebuggingStateCheckHelper.PreventDebuggingCrashWhileDebugging(this.Grid,false)) return;
			if(Grid == null) return;
			if(Editor == null) {
				editor = new frmDesigner();
			}
			try {
				Editor.ShowInTaskbar = false;
				if(view == null && activeItem == null) {
					Editor.InitGrid(Grid);
				} else {
					Editor.InitGrid(Grid, activeItem, view);
				}
				if(Grid.FindForm() != null) 
					Grid.FindForm().AddOwnedForm(editor);
				Editor.ShowDialog();
			} finally {
				Editor = null;
				UpdateLevelSelector(true);
			}
		}
		public virtual void ShowLayoutDesigner(BaseView view, string activeItem) {
			if(DebuggingStateCheckHelper.PreventDebuggingCrashWhileDebugging(this.Grid,false)) return;
			if(Grid == null) return;
			if(Editor == null) editor = new frmDesigner();
			try {
				Editor.ShowInTaskbar = false;
				Editor.InitGrid(Grid,"Layout");
				if(Grid.FindForm() != null) Grid.FindForm().AddOwnedForm(editor);
				Editor.ShowDialog();
			}
			finally {
				Editor = null;
				UpdateLevelSelector(true);
			}
		}
		protected void OnLayoutDesignerClick(object sender, EventArgs e) {
			ShowLayoutDesigner(null, null);
		}
		protected void OnDesignerClick(object sender, EventArgs e) {
			ShowDesigner(null, null);
		}
		protected frmDesigner Editor {  
			get { return editor; }
			set {
				if(Editor == value) return;
				if(Editor != null) Editor.Dispose();
				editor = value;
			}
		}
		public LevelSelector Selector { get { return levelSelector; } }
		protected virtual void UpdateLevelSelector(bool updateLevels) {
			if(Grid == null) return;
			if(updateLevels)
				Selector.UpdateLevels();
			Selector.UpdateSize();
			Selector.Location = new Point(Grid.ClientSize.Width - Selector.Width - 10, Grid.ClientSize.Height - Selector.Height - 22);
			this.Grid.Controls.Add(Selector);
			Selector.BringToFront();
		}
		protected override void OnCreateHandle() {
			base.OnCreateHandle();
			UnhookChildControls(Selector);
		}
		protected void OnGrid_DataSourceChanged(object sender, EventArgs e) {
			if(Grid.IsHandleCreated) 
				UpdateLevelSelector(true);
		}
		protected void OnGrid_HandleCreated(object sender, EventArgs e) {
			UpdateLevelSelector(true);
			UnhookChildControls(Selector);
		}
		protected void OnGrid_Load(object sender, EventArgs e) {
			UpdateLevelSelector(true);
		}
		protected void OnGrid_SizeChanged(object sender, EventArgs e) {
			UpdateLevelSelector(false);
		}
		protected void OnSelector_SizeChanged(object sender, EventArgs e) {
			UpdateLevelSelector(false);
		}
		protected void DisableUndoEngine() {
			if(UndoEngine == null) return;
			UndoEngine.Enabled = false;
		}
		protected void EnableUndoEngine() {
			if(UndoEngine == null) return;
			UndoEngine.Enabled = true;
		}
		protected UndoEngine UndoEngine {
			get { return (UndoEngine)((IDesignerHost)this.Component.Site.Container).GetService(typeof(UndoEngine)); }
		}
		IDesignerHost host;
		public override void Initialize(IComponent component) {
			base.Initialize(component); 
			IInheritanceService srv = GetService(typeof(IInheritanceService)) as IInheritanceService;;
			UpdateLevelDesignerVisibility();
			this.host = GetService(typeof(IDesignerHost)) as IDesignerHost;
			LoaderPatcherService.InstallService(host);
			ISelectionService ss = (ISelectionService)GetService(typeof(ISelectionService));
			if(ss != null) {
				ss.SelectionChanged += new EventHandler(OnSelectionChanged);
				Selector.Tree.SelectionService = ss;
			}
			Grid.SizeChanged += new EventHandler(OnGrid_SizeChanged);
			Grid.DataSourceChanged += new EventHandler(OnGrid_DataSourceChanged);
			Grid.HandleCreated += new EventHandler(OnGrid_HandleCreated);
			Grid.Load += new EventHandler(OnGrid_Load);
			Selector.EditingGrid = Grid;
			if(Grid.IsHandleCreated) 
				UpdateLevelSelector(true);
			UnhookChildControls(Selector);
		}
		protected virtual void ClearDesigner() {
			if(Grid != null) {
				Grid.SizeChanged -= new EventHandler(OnGrid_SizeChanged);
				Grid.DataSourceChanged -= new EventHandler(OnGrid_DataSourceChanged);
				Grid.HandleCreated -= new EventHandler(OnGrid_HandleCreated);
				Grid.HandleCreated -= new EventHandler(OnGrid_Load);
			}
			Editor = null;
			ISelectionService ss = (ISelectionService)GetService(typeof(ISelectionService));
			if (ss != null) {
				ss.SelectionChanged -= new EventHandler(OnSelectionChanged);
			}
			if(Selector != null) {
				this.levelSelector.Tree.SelectionService = null;
				this.levelSelector.Tree.ShownEditor -= new EventHandler(OnSelector_ShownEditor);
				this.levelSelector.Tree.HiddenEditor -= new EventHandler(OnSelector_HiddenEditor);
				Selector.Dispose();
				this.levelSelector = null;
			}
		}
		protected override void Dispose(bool disposing) {
			LoaderPatcherService.UnInstallService(host);
			this.host = null;
			if(disposing) {
				SaveSettings();
				ClearDesigner();
			}
			base.Dispose(disposing);
		}
		protected virtual void SaveSettings() {
			PropertyStore ps = new PropertyStore(frmDesigner.GridSettings);
			ps.AddProperty("ShowLevelDesigner", LevelDesignerVisible);
			ps.Store();
		}
		protected GridControl Grid { get { return Control as GridControl; } }
		public override ICollection AssociatedComponents {
			get {
				if(Grid == null) return base.AssociatedComponents;
				ArrayList controls = new ArrayList();
				foreach(RepositoryItem repository in Grid.RepositoryItems) {
					controls.Add(repository);
				}
				foreach(BaseView view in Grid.ViewCollection) {
					AddView(controls, view);
				}
				AddBase(controls);
				return controls;
			}
		}
		void AddView(ArrayList controls, BaseView view) {
			if(view == null) return;
			controls.Add(view);
		}
		void AddBase(ArrayList controls) {
			foreach(object obj in base.AssociatedComponents) {
				if(controls.Contains(obj)) continue;
				controls.Add(obj);
			}
		}
		protected virtual bool SelectorContains(Point point) {
			if(Selector != null && Selector.Visible && Selector.Bounds.Contains(point)) return true;
			return false;
		}
		public override void InitializeNewComponent(IDictionary defaultValues) {
			base.InitializeNewComponent(defaultValues);
			UpdateLevelSelector(true);
			if(Grid == null || Grid.Container == null) return;
			if(Grid.MenuManager == null)
				Grid.MenuManager = ControlDesignerHelper.GetBarManager(Grid.Container);
		}
		private void OnSelectionChanged(object sender, EventArgs e) {
			Grid.Invalidate();
			ISelectionService ss = (ISelectionService)GetService(typeof(ISelectionService));
			if(ss != null) {
				BaseView view = ss.PrimarySelection as BaseView;
				if(view == null) return;
				if(view.GridControl != Grid) return;
				SwitchVisibleView(view);
			}
		}
		public override DesignerVerbCollection DXVerbs { 
			get {
				if(!AllowDesigner) return null;
				if(Grid!=null && Grid.MainView is LayoutView) return layoutViewVerbs;
				return defaultVerbs;
			} 
		}
		protected override void PostFilterProperties(IDictionary properties) {
			base.PostFilterProperties(properties);
			DXPropertyDescriptor.ConvertDescriptors(properties, null);
		}
		protected virtual bool GetHitTestCore(Point clientPoint) {
			if(SelectorContains(clientPoint)) return true;
			if(Grid == null) return false;
			GridView view = Grid.DefaultView as GridView;
			CardView cardView = Grid.DefaultView as CardView;
			LayoutView layoutView = Grid.DefaultView as LayoutView;
			if(view != null) return GetGridHitTest(view, clientPoint);
			if(cardView != null) return GetCardHitTest(cardView, clientPoint);
			if(layoutView != null) return GetLayoutHitTest(layoutView, clientPoint);
			return false;
		}
		bool GetGridHitTest(GridView view, Point clientPoint) {
			GridHitInfo hitInfo = view.CalcHitInfo(clientPoint);
			if(hitInfo.InColumnPanel && view.Columns.Count > 0) return true;
			if(hitInfo.InGroupPanel && hitInfo.Column != null) return true;
			DevExpress.XtraGrid.Views.BandedGrid.ViewInfo.BandedGridHitInfo bHit = hitInfo as DevExpress.XtraGrid.Views.BandedGrid.ViewInfo.BandedGridHitInfo;
			if(bHit != null && bHit.InBandPanel) return true;
			return false;
		}
		bool GetCardHitTest(CardView view, Point clientPoint) {
			DevExpress.XtraGrid.Views.Card.ViewInfo.CardHitInfo hitInfo = view.CalcHitInfo(clientPoint);
			if(hitInfo.InCard) return true;
			return false;
		}
		bool GetLayoutHitTest(LayoutView view, Point clientPoint) {
			LayoutViewHitInfo hitInfo = view.CalcHitInfo(clientPoint) as LayoutViewHitInfo;
			if(hitInfo.InCard) return true;
			return false;
		}
		protected override bool GetHitTest(Point point) {
			if(Grid == null || !AllowDesigner || DebuggingState) return false;
			Point pt = Grid.PointToClient(point);
			if(GetHitTestCore(pt)) return true;
			return base.GetHitTest(point);
		}
		protected override void RegisterActionLists(DesignerActionListCollection list) {
			if(AllowDesigner)
				list.Add(new GridActionList(this));
			base.RegisterActionLists(list);
		}
		public class GridActionList : DesignerActionList {
			LWGridControlDesigner designer;
			public GridActionList(LWGridControlDesigner designer)
				: base(designer.Component) {
				this.designer = designer;
			}
			public GridControl Grid { get { return designer.Grid; } }
			public override DesignerActionItemCollection GetSortedActionItems() {
				DesignerActionItemCollection res = new DesignerActionItemCollection();
				res.Add(new DesignerActionPropertyItem("DataSource", "Choose Data Source"));
				res.Add(new DesignerActionMethodItem(this, "RunDesigner", "Run Designer"));
				if(Grid!=null && Grid.MainView is LayoutView) {
					res.Add(new DesignerActionMethodItem(this, "RunLayoutDesigner", "Customize Layout"));				
				}
				res.Add(new DesignerActionHeaderItem("Options", "Add"));
				res.Add(new DesignerActionPropertyItem("ShowLevelDesigner", "Show level designer", "Add"));
				return res;
			}
			public void RunDesigner() {
				designer.ShowDesigner(null, null);
			}
			public void RunLayoutDesigner() {
				designer.ShowLayoutDesigner(null, null);
			}
			public bool ShowLevelDesigner {
				get { return designer.LevelDesignerVisible; }
				set { designer.LevelDesignerVisible = value; }
			}
#if DXWhidbey
			[AttributeProvider(typeof(IListSource))]
#endif
			public object DataSource {
				get { return Grid.DataSource; }
				set {
					EditorContextHelper.SetPropertyValue(designer, Grid, "DataSource", value);
				}
			}
		}
	}
	public class XtraGridBlendingDesigner : ComponentDesigner {
		DesignerVerbCollection verbs;
		public XtraGridBlendingDesigner() {
			verbs =	new DesignerVerbCollection(
				new DesignerVerb[] {
									   new DesignerVerb("Preview", new EventHandler(OnPreviewClick))});
		}
		public override DesignerVerbCollection Verbs { get { return verbs; } }
		private XtraGridBlending Blending { get { return Component as XtraGridBlending; } }
		private void OnPreviewClick(object sender, EventArgs e) {
			if(Blending.GridControl != null) 
            {
				System.Windows.Forms.Form dlg = new Preview(Blending);
				dlg.ShowDialog();
			} else 
				XtraMessageBox.Show("The GridControl property is not initialized."+
					"\r\nPlease select a grid control from its dropdown list.",
					Blending.Site.Name + ".GridControl is null...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}
		public override void InitializeNewComponent(IDictionary defaultValues) {
			base.InitializeNewComponent(defaultValues);
			if(Blending == null || Blending.Container == null) return;
			if(Blending.GridControl == null)
				Blending.GridControl = GetGrid(Blending.Container);
		}
		private static GridControl GetGrid(IContainer container) {
			return GetTypeFromContainer(container, typeof(GridControl)) as GridControl;
		}
		protected static object GetTypeFromContainer(IContainer container, Type type) {
			if(container == null || type == null) return null;
			foreach(object obj in container.Components) {
				if(type.IsInstanceOfType(obj)) return obj;
			}
			return null;
		}
	}
	public class BaseViewDesigner : BaseComponentDesigner {
		DesignerVerbCollection verbs = null;
		public BaseViewDesigner() {
		}
		protected override bool AllowInheritanceWrapper { get { return true; } }
		protected override bool UseVerbsAsActionList { get { return true; } }
		public override DesignerVerbCollection DXVerbs { 
			get { 
				if(verbs == null) UpdateVerbs();
				if(!AllowDesigner) return null;
				return verbs; 
			} 
		}
		protected override bool AllowEditInherited { get { return false; } }
		protected override void PostFilterProperties(IDictionary properties) {
			base.PostFilterProperties(properties);
			DXPropertyDescriptor.ConvertDescriptors(properties, null);
		}
		protected BaseView View { get { return Component as BaseView; } }
		protected virtual void UpdateVerbs() {
			this.verbs = new DesignerVerbCollection();
			if(View == null || View.BaseInfo == null || View.BaseInfo.Designer == null) return;
			if(View.GridControl == null || !View.GridControl.IsDesignMode) return;
			foreach(DesignerGroup group in View.BaseInfo.Designer.Groups) {
				foreach(DesignerItem item in group) {
					if(!item.ShowInVerbs) continue;
					verbs.Add(new DesignerVerb(item.Caption, new EventHandler(OnVerbClick)));
				}
			}
		}
		protected virtual void OnVerbClick(object sender, EventArgs e) {
			DesignerVerb verb = sender as DesignerVerb;
			if(verb == null) return;
			if(View == null || View.BaseInfo == null || View.BaseInfo.Designer == null) return;
			DesignerItem item = null;
			foreach(DesignerGroup group in View.BaseInfo.Designer.Groups) {
				item = group.GetItemByCaption(verb.Text);
				if(item != null) break;
			}
			if(item == null) return;
			if(View.GridControl == null) return;
			IDesignerHost host = GetService(typeof(IDesignerHost)) as IDesignerHost;
			if(host == null) return;
			LWGridControlDesigner designer = host.GetDesigner(View.GridControl) as LWGridControlDesigner;
			if(designer == null) return;
			designer.ShowDesigner(View, item.Caption);
		}
	}
	public class LayoutViewFieldDesigner : ComponentDesigner {
		protected string Name {
			get { return Component.Site.Name; }
			set {
				IDesignerHost host = GetService(typeof(IDesignerHost)) as IDesignerHost;
				if(host == null || (host != null && !host.Loading))
					Component.Site.Name = value;
			}
		}
		protected override void PreFilterProperties(IDictionary properties) {
			base.PreFilterProperties(properties);
			ShadowTheProperty(properties, "Name");
		}
		static void ShadowTheProperty(IDictionary properties, string property) {
			PropertyDescriptor prop = (PropertyDescriptor)properties[property];
			if(prop != null)
				properties[property] = TypeDescriptor.CreateProperty(typeof(LayoutViewFieldDesigner), prop, new Attribute[0]);
		}
	}
	public class LayoutViewDesigner : ColumnViewDesigner {
		public override ICollection AssociatedComponents {
			get {
				LayoutView view = Component as LayoutView;
				if (view == null) return base.AssociatedComponents;
				ArrayList controls = new ArrayList();
				foreach (LayoutViewColumn col in view.Columns) {
					AddColumn(controls, col);
				}
				controls.Add(view.TemplateCard);
				AddBase(controls);
				return controls;
			}
		}
		void AddColumn(ArrayList controls, LayoutViewColumn col) {
			if (col == null) return;
			controls.Add(col);
			if (col.LayoutViewField != null) controls.Add(col.LayoutViewField);
		}
	}
	public class ColumnViewDesigner : BaseViewDesigner {
		public override ICollection AssociatedComponents {
			get {
				ColumnView view = Component as ColumnView;
				if(view == null) return base.AssociatedComponents;
				ArrayList controls = new ArrayList();
				foreach(GridColumn col in view.Columns) {
					AddColumn(controls, col);
				}
				AddBase(controls);
				return controls;
			}
		}
		void AddColumn(ArrayList controls, GridColumn col) {
			if(col == null) return;
			controls.Add(col);
		}
		protected void AddBase(ArrayList controls) {
			foreach(object obj in base.AssociatedComponents) {
				if(controls.Contains(obj)) continue;
				controls.Add(obj);
			}
		}
	}
	public class GridColumnDesigner : BaseComponentDesigner {
		protected override bool AllowEditInherited {
			get {
				return false;
			}
		}
	}
	public class GridBandDesigner : BaseComponentDesigner {
		protected override bool AllowEditInherited {
			get {
				return false;
			}
		}
	}
	[GuidAttribute("7494682b-37a0-11d2-a273-00c04f8ef4ff"),ComVisible(true),InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IVSMDPropertyBrowser {
	}
	public class GridLookUpEditDesigner : ButtonEditDesigner {
		public override ICollection AssociatedComponents {
			get {
				ArrayList controls = new ArrayList(base.AssociatedComponents);
				if(Properties != null) controls.Add(Properties.View);
				return controls;
			}
		}
		protected override void RegisterActionLists(DesignerActionListCollection list) {
			list.Add(new LookUpEditBaseDataBindingActionList(this));
			list.Add(new SingleMethodActionList(this, new MethodInvoker(DesignView), "Design View", true));
			base.RegisterActionLists(list);
		}
		public virtual void DesignView() {
			EditorContextHelper.EditValue(this, Properties, "View");
		}
		protected override void OnInitializeNew(IDictionary defaultValues) {
			base.OnInitializeNew(defaultValues);
			IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
			if(host != null) AddToContainer(Editor.Name, Properties, host.Container);
		}
		public RepositoryItemGridLookUpEdit Properties { get { return (Editor == null ? null : Editor.Properties); } }
		public new GridLookUpEdit Editor { get { return base.Editor as GridLookUpEdit; } }
		internal static void AddToContainer(string name, RepositoryItemGridLookUpEdit properties, IContainer container) {
			if(container == null) return;
			try {
				container.Add(properties.View, name + "View");
			} catch {
				try {
					container.Add(properties.View);
				} catch {
				}
			}
		}
	}
	public class GridLookUpEditRepositoryItemDesigner : BaseRepositoryItemDesigner {
		public override ICollection AssociatedComponents {
			get {
				ArrayList controls = new ArrayList(base.AssociatedComponents);
				if(Item != null) controls.Add(Item.View);
				return controls;
			}
		}
		public override void Initialize(IComponent component) {
			base.Initialize(component);
			if(Item != null && !Item.IsLoading && Item.View.Name == "") {
				if(Item.View.Site == null) {
					UpdateContainer();
				}
			}
		}
		protected override void OnInitializeNew(IDictionary defaultValues) {
			base.OnInitializeNew(defaultValues);
			UpdateContainer();
		}
		void UpdateContainer() {
			IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
			if(host != null && !host.Loading) GridLookUpEditDesigner.AddToContainer(Item.Name, Item, host.Container);
		}
		public new RepositoryItemGridLookUpEdit Item{ get { return base.Item as RepositoryItemGridLookUpEdit; } }
		internal static void AddToContainer(string name, RepositoryItemGridLookUpEdit properties, IContainer container) {
			if(container == null) return;
			try {
				container.Add(properties.View, name + "View");
			} catch {
				try {
					container.Add(properties.View);
				} catch {
				}
			}
		}
	}
	public class GridLookUpViewEditor : UITypeEditor {
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) {
			if(context != null && context.Instance != null) return UITypeEditorEditStyle.Modal;
			return base.GetEditStyle(context);
		}
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object objValue) {
			BaseView view = objValue as BaseView;
			if(view == null || context == null || context.Instance == null || provider == null) return objValue;
			IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));	
			if(edSvc == null) return objValue;
			frmDesigner designer = new frmDesigner();
			try {
				view.GridControl.ForceInitialize();
				designer.ShowInTaskbar = true;
				designer.InitGrid(view.GridControl, view);
				edSvc.ShowDialog(designer);
			} catch {}
			designer.Dispose();
			return objValue;
		}
	}
	public class DebuggingStateCheckHelper {
		static string errorMessage  = @"Component cannot be modified in designer while building or debugging.";
		protected static IServiceProvider GetServiceProvider(object obj) {
			IServiceProvider provider = obj as IServiceProvider;
			if(provider != null) {
				return provider;
			}
			Component component = obj as Component;
			if(component != null) {
				return component.Site;
			}
			ComponentDesigner designer = obj as ComponentDesigner;
			if((designer != null) && (designer.Component != null)) {
				return designer.Component.Site;
			}
			return null;
		}
		public static bool IsDebugging(object obj) {
			IServiceProvider provider = GetServiceProvider(obj);
			if(provider == null) return false;
			object loader = provider.GetService(typeof(IResourceService));
			if(loader == null) {
				return false;
			}
			Type loaderType = loader.GetType();
			if(loaderType.Name == "VSCodeDomDesignerLoader") {
				PropertyInfo pi = loaderType.GetProperty("IsDebugging", BindingFlags.NonPublic | BindingFlags.Instance);
				if(pi != null) return (bool)pi.GetValue(loader, null);
			}
			return false;
		}
		public static bool PreventDebuggingCrashWhileDebugging(object obj, bool showMessage) {
			bool result = false;
			if(IsDebugging(obj)) {
				if (showMessage) XtraMessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				result = true;
			}
			return result;
		}
	}

    public class LWGridViewDesigner : BaseGridDesigner
    {
        protected override void CreateGroups()
        {
            base.CreateGroups();
            DesignerGroup group = CreateDefaultMainGroup();
            group.Clear();
            group.Add("ап", "Adjust the Column collection of the current view, assign in-place editors to columns and specify total summaries.", "ICP.WF.Controls.Design.ColumnDesigner", GetDefaultLargeImage(1), GetDefaultSmallImage(1), null);
            
           // group.Add("Group Summary Items", "Manage group summaries.", "DevExpress.XtraGrid.Frames.GroupSummary", GetDefaultLargeImage(5), GetDefaultSmallImage(5), null, false);
          //  CreateDefaultStyleGroup();
           // CreateDefaultRepositoryGroup();
           // group = CreateDefaultPrintingGroup();
           // group.Add("Printing Settings", "Adjust the printing settings of the current view.", "DevExpress.XtraGrid.Frames.GridViewPrinting", GetDefaultLargeImage(11), GetDefaultSmallImage(11), null, false);
        }
    }


    public class LWGridInfoRegistrator : BaseInfoRegistrator
    {
        public LWGridInfoRegistrator()
        {
        }
        protected override BaseGridDesigner CreateDesigner() { return new LWGridViewDesigner(); }
        protected override void RegisterViewPaintStyles()
        {
            PaintStyles.Add(new GridPaintStyle());
            PaintStyles.Add(new GridWindowsXPPaintStyle());
            PaintStyles.Add(new Grid3DPaintStyle());
            PaintStyles.Add(new GridUltraFlatPaintStyle());
            PaintStyles.Add(new GridOffice2003PaintStyle());
            PaintStyles.Add(new GridMixedXPPaintStyle());
            PaintStyles.Add(new GridWebPaintStyle());
            PaintStyles.Add(new GridSkinPaintStyle());
        }
        public override bool IsInternalView { get { return false; } }
        public override string StyleOwnerName { get { return "Grid"; } }
        public override string ViewName { get { return "GridView"; } }
        public override BaseView CreateView(GridControl grid)
        {
            BaseView view = new DevExpress.XtraGrid.Views.Grid.GridView();
            view.GridControl = grid;
            return view;
        }
        public override BaseViewHandler CreateHandler(BaseView view) { return new DevExpress.XtraGrid.Views.Grid.Handler.GridHandler(view as DevExpress.XtraGrid.Views.Grid.GridView); }
        static AppearanceDefaultInfo[] gridPrintAppearances = new AppearanceDefaultInfo[] {
			new AppearanceDefaultInfo("HeaderPanel", new AppearanceDefault(Color.Black, Color.Silver, HorzAlignment.Center, VertAlignment.Center)),
			new AppearanceDefaultInfo("Row", new AppearanceDefault(Color.Black, Color.White, HorzAlignment.Default, VertAlignment.Default)),
			new AppearanceDefaultInfo("EvenRow", new AppearanceDefault(Color.Empty, Color.LightSkyBlue, HorzAlignment.Default, VertAlignment.Default)),
			new AppearanceDefaultInfo("OddRow", new AppearanceDefault(Color.Empty, Color.NavajoWhite, HorzAlignment.Default, VertAlignment.Default)),
			new AppearanceDefaultInfo("GroupRow", new AppearanceDefault(Color.Black, Color.Gainsboro, HorzAlignment.Near, VertAlignment.Default)),
			new AppearanceDefaultInfo("Lines", new AppearanceDefault(Color.DarkGray, Color.DarkGray, HorzAlignment.Default)),
			new AppearanceDefaultInfo("Preview", new AppearanceDefault(Color.DimGray, Color.White, HorzAlignment.Near, VertAlignment.Top)),
			new AppearanceDefaultInfo("FilterPanel", new AppearanceDefault(Color.White, Color.Gray, HorzAlignment.Near, VertAlignment.Top)),
			new AppearanceDefaultInfo("FooterPanel", new AppearanceDefault(Color.Black, Color.DarkGray, HorzAlignment.Far, VertAlignment.Center)),
			new AppearanceDefaultInfo("GroupFooter", new AppearanceDefault(Color.Black, Color.LightGray, HorzAlignment.Far, VertAlignment.Center))
		};
        public override AppearanceDefaultInfo[] GetDefaultPrintAppearance() { return gridPrintAppearances; }
    }
}
