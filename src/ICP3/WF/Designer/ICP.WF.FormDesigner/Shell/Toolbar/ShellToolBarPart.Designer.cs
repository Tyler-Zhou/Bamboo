namespace ICP.WF.FormDesigner
{
    partial class ShellToolBarPart
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShellToolBarPart));
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barFormDesignToolBarManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barNew = new DevExpress.XtraBars.BarButtonItem();
            this.barOpen = new DevExpress.XtraBars.BarSubItem();
            this.barOpenLocal = new DevExpress.XtraBars.BarButtonItem();
            this.barOpenServer = new DevExpress.XtraBars.BarButtonItem();
            this.barSave = new DevExpress.XtraBars.BarSubItem();
            this.barSaveLocal = new DevExpress.XtraBars.BarButtonItem();
            this.barSaveServer = new DevExpress.XtraBars.BarButtonItem();
            this.barCopy = new DevExpress.XtraBars.BarButtonItem();
            this.barCut = new DevExpress.XtraBars.BarButtonItem();
            this.barPaste = new DevExpress.XtraBars.BarButtonItem();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barUnDo = new DevExpress.XtraBars.BarButtonItem();
            this.barReDo = new DevExpress.XtraBars.BarButtonItem();
            this.barTabOrder = new DevExpress.XtraBars.BarButtonItem();
            this.barLeft = new DevExpress.XtraBars.BarButtonItem();
            this.barRight = new DevExpress.XtraBars.BarButtonItem();
            this.barCenter = new DevExpress.XtraBars.BarButtonItem();
            this.barTop = new DevExpress.XtraBars.BarButtonItem();
            this.barBottom = new DevExpress.XtraBars.BarButtonItem();
            this.barMiddle = new DevExpress.XtraBars.BarButtonItem();
            this.barViewCode = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barLinkContainerItem3 = new DevExpress.XtraBars.BarLinkContainerItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.barFormDesignToolBarManager)).BeginInit();
            this.SuspendLayout();
            // 
            // bar2
            // 
            this.bar2.BarName = "Tools";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.bar2, "bar2");
            // 
            // barFormDesignToolBarManager
            // 
            this.barFormDesignToolBarManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3});
            this.barFormDesignToolBarManager.DockControls.Add(this.barDockControlTop);
            this.barFormDesignToolBarManager.DockControls.Add(this.barDockControlBottom);
            this.barFormDesignToolBarManager.DockControls.Add(this.barDockControlLeft);
            this.barFormDesignToolBarManager.DockControls.Add(this.barDockControlRight);
            this.barFormDesignToolBarManager.Form = this;
            this.barFormDesignToolBarManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barNew,
            this.barLinkContainerItem3,
            this.barButtonItem1,
            this.barButtonItem2,
            this.barSubItem1,
            this.barOpen,
            this.barOpenLocal,
            this.barOpenServer,
            this.barSave,
            this.barSaveLocal,
            this.barSaveServer,
            this.barCopy,
            this.barCut,
            this.barPaste,
            this.barDelete,
            this.barLeft,
            this.barButtonItem4,
            this.barTop,
            this.barBottom,
            this.barCenter,
            this.barRight,
            this.barMiddle,
            this.barViewCode,
            this.barClose,
            this.barUnDo,
            this.barReDo,
            this.barTabOrder});
            this.barFormDesignToolBarManager.MainMenu = this.bar3;
            this.barFormDesignToolBarManager.MaxItemId = 32;
            // 
            // bar3
            // 
            this.bar3.BarName = "Main menu";
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barNew, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barOpen),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCopy, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCut),
            new DevExpress.XtraBars.LinkPersistInfo(this.barPaste),
            new DevExpress.XtraBars.LinkPersistInfo(this.barDelete),
            new DevExpress.XtraBars.LinkPersistInfo(this.barUnDo, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barReDo),
            new DevExpress.XtraBars.LinkPersistInfo(this.barTabOrder),
            new DevExpress.XtraBars.LinkPersistInfo(this.barLeft, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barRight),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCenter),
            new DevExpress.XtraBars.LinkPersistInfo(this.barTop),
            new DevExpress.XtraBars.LinkPersistInfo(this.barBottom),
            new DevExpress.XtraBars.LinkPersistInfo(this.barMiddle),
            new DevExpress.XtraBars.LinkPersistInfo(this.barViewCode, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barClose)});
            this.bar3.OptionsBar.MultiLine = true;
            this.bar3.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.bar3, "bar3");
            // 
            // barNew
            // 
            this.barNew.AccessibleDescription = null;
            this.barNew.AccessibleName = null;
            resources.ApplyResources(this.barNew, "barNew");
            this.barNew.Glyph = global::ICP.WF.FormDesigner.Properties.Resources.新建;
            this.barNew.Id = 0;
            this.barNew.Name = "barNew";
            // 
            // barOpen
            // 
            this.barOpen.AccessibleDescription = null;
            this.barOpen.AccessibleName = null;
            resources.ApplyResources(this.barOpen, "barOpen");
            this.barOpen.Glyph = global::ICP.WF.FormDesigner.Properties.Resources.文件夹打开状态;
            this.barOpen.Id = 10;
            this.barOpen.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barOpenLocal),
            new DevExpress.XtraBars.LinkPersistInfo(this.barOpenServer, true)});
            this.barOpen.Name = "barOpen";
            this.barOpen.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barOpenLocal
            // 
            this.barOpenLocal.AccessibleDescription = null;
            this.barOpenLocal.AccessibleName = null;
            resources.ApplyResources(this.barOpenLocal, "barOpenLocal");
            this.barOpenLocal.Glyph = global::ICP.WF.FormDesigner.Properties.Resources.文件夹打开状态;
            this.barOpenLocal.Id = 11;
            this.barOpenLocal.Name = "barOpenLocal";
            // 
            // barOpenServer
            // 
            this.barOpenServer.AccessibleDescription = null;
            this.barOpenServer.AccessibleName = null;
            resources.ApplyResources(this.barOpenServer, "barOpenServer");
            this.barOpenServer.Glyph = global::ICP.WF.FormDesigner.Properties.Resources.文件夹打开状态;
            this.barOpenServer.Id = 12;
            this.barOpenServer.Name = "barOpenServer";
            // 
            // barSave
            // 
            this.barSave.AccessibleDescription = null;
            this.barSave.AccessibleName = null;
            resources.ApplyResources(this.barSave, "barSave");
            this.barSave.Glyph = global::ICP.WF.FormDesigner.Properties.Resources.保存;
            this.barSave.Id = 13;
            this.barSave.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSaveLocal),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSaveServer, true)});
            this.barSave.Name = "barSave";
            // 
            // barSaveLocal
            // 
            this.barSaveLocal.AccessibleDescription = null;
            this.barSaveLocal.AccessibleName = null;
            resources.ApplyResources(this.barSaveLocal, "barSaveLocal");
            this.barSaveLocal.Glyph = global::ICP.WF.FormDesigner.Properties.Resources.保存;
            this.barSaveLocal.Id = 14;
            this.barSaveLocal.Name = "barSaveLocal";
            // 
            // barSaveServer
            // 
            this.barSaveServer.AccessibleDescription = null;
            this.barSaveServer.AccessibleName = null;
            resources.ApplyResources(this.barSaveServer, "barSaveServer");
            this.barSaveServer.Glyph = global::ICP.WF.FormDesigner.Properties.Resources.保存;
            this.barSaveServer.GlyphDisabled = global::ICP.WF.FormDesigner.Properties.Resources.保存;
            this.barSaveServer.Id = 15;
            this.barSaveServer.Name = "barSaveServer";
            // 
            // barCopy
            // 
            this.barCopy.AccessibleDescription = null;
            this.barCopy.AccessibleName = null;
            resources.ApplyResources(this.barCopy, "barCopy");
            this.barCopy.Glyph = global::ICP.WF.FormDesigner.Properties.Resources.copy;
            this.barCopy.Id = 16;
            this.barCopy.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C));
            this.barCopy.Name = "barCopy";
            // 
            // barCut
            // 
            this.barCut.AccessibleDescription = null;
            this.barCut.AccessibleName = null;
            resources.ApplyResources(this.barCut, "barCut");
            this.barCut.Glyph = global::ICP.WF.FormDesigner.Properties.Resources.CutHS;
            this.barCut.Id = 17;
            this.barCut.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X));
            this.barCut.Name = "barCut";
            // 
            // barPaste
            // 
            this.barPaste.AccessibleDescription = null;
            this.barPaste.AccessibleName = null;
            resources.ApplyResources(this.barPaste, "barPaste");
            this.barPaste.Glyph = global::ICP.WF.FormDesigner.Properties.Resources.paster;
            this.barPaste.Id = 18;
            this.barPaste.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V));
            this.barPaste.Name = "barPaste";
            // 
            // barDelete
            // 
            this.barDelete.AccessibleDescription = null;
            this.barDelete.AccessibleName = null;
            resources.ApplyResources(this.barDelete, "barDelete");
            this.barDelete.Glyph = global::ICP.WF.FormDesigner.Properties.Resources.删除;
            this.barDelete.Id = 19;
            this.barDelete.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Delete);
            this.barDelete.Name = "barDelete";
            // 
            // barUnDo
            // 
            this.barUnDo.AccessibleDescription = null;
            this.barUnDo.AccessibleName = null;
            resources.ApplyResources(this.barUnDo, "barUnDo");
            this.barUnDo.Glyph = global::ICP.WF.FormDesigner.Properties.Resources.undo;
            this.barUnDo.Id = 29;
            this.barUnDo.Name = "barUnDo";
            this.barUnDo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // barReDo
            // 
            this.barReDo.AccessibleDescription = null;
            this.barReDo.AccessibleName = null;
            resources.ApplyResources(this.barReDo, "barReDo");
            this.barReDo.Glyph = global::ICP.WF.FormDesigner.Properties.Resources.redo;
            this.barReDo.Id = 30;
            this.barReDo.ItemShortcut = new DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                            | System.Windows.Forms.Keys.Z));
            this.barReDo.Name = "barReDo";
            this.barReDo.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // barTabOrder
            // 
            this.barTabOrder.AccessibleDescription = null;
            this.barTabOrder.AccessibleName = null;
            resources.ApplyResources(this.barTabOrder, "barTabOrder");
            this.barTabOrder.Glyph = global::ICP.WF.FormDesigner.Properties.Resources.统计信息;
            this.barTabOrder.Id = 31;
            this.barTabOrder.Name = "barTabOrder";
            // 
            // barLeft
            // 
            this.barLeft.AccessibleDescription = null;
            this.barLeft.AccessibleName = null;
            resources.ApplyResources(this.barLeft, "barLeft");
            this.barLeft.Glyph = global::ICP.WF.FormDesigner.Properties.Resources.AlignObjectsLeftHS;
            this.barLeft.Id = 20;
            this.barLeft.Name = "barLeft";
            // 
            // barRight
            // 
            this.barRight.AccessibleDescription = null;
            this.barRight.AccessibleName = null;
            resources.ApplyResources(this.barRight, "barRight");
            this.barRight.Glyph = global::ICP.WF.FormDesigner.Properties.Resources.AlignObjectsRightHS;
            this.barRight.Id = 25;
            this.barRight.Name = "barRight";
            // 
            // barCenter
            // 
            this.barCenter.AccessibleDescription = null;
            this.barCenter.AccessibleName = null;
            resources.ApplyResources(this.barCenter, "barCenter");
            this.barCenter.Glyph = global::ICP.WF.FormDesigner.Properties.Resources.AlignObjectsCenteredVerticalHS;
            this.barCenter.Id = 24;
            this.barCenter.Name = "barCenter";
            // 
            // barTop
            // 
            this.barTop.AccessibleDescription = null;
            this.barTop.AccessibleName = null;
            resources.ApplyResources(this.barTop, "barTop");
            this.barTop.Glyph = global::ICP.WF.FormDesigner.Properties.Resources.AlignObjectsTopHS;
            this.barTop.Id = 22;
            this.barTop.Name = "barTop";
            // 
            // barBottom
            // 
            this.barBottom.AccessibleDescription = null;
            this.barBottom.AccessibleName = null;
            resources.ApplyResources(this.barBottom, "barBottom");
            this.barBottom.Glyph = global::ICP.WF.FormDesigner.Properties.Resources.AlignObjectsBottomHS;
            this.barBottom.Id = 23;
            this.barBottom.Name = "barBottom";
            // 
            // barMiddle
            // 
            this.barMiddle.AccessibleDescription = null;
            this.barMiddle.AccessibleName = null;
            resources.ApplyResources(this.barMiddle, "barMiddle");
            this.barMiddle.Glyph = global::ICP.WF.FormDesigner.Properties.Resources.AlignObjectsCenteredHorizontalHS;
            this.barMiddle.Id = 26;
            this.barMiddle.Name = "barMiddle";
            // 
            // barViewCode
            // 
            this.barViewCode.AccessibleDescription = null;
            this.barViewCode.AccessibleName = null;
            resources.ApplyResources(this.barViewCode, "barViewCode");
            this.barViewCode.Glyph = global::ICP.WF.FormDesigner.Properties.Resources.查看;
            this.barViewCode.Id = 27;
            this.barViewCode.Name = "barViewCode";
            // 
            // barClose
            // 
            this.barClose.AccessibleDescription = null;
            this.barClose.AccessibleName = null;
            resources.ApplyResources(this.barClose, "barClose");
            this.barClose.Glyph = global::ICP.WF.FormDesigner.Properties.Resources.退出;
            this.barClose.Id = 28;
            this.barClose.Name = "barClose";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.AccessibleDescription = null;
            this.barDockControlTop.AccessibleName = null;
            resources.ApplyResources(this.barDockControlTop, "barDockControlTop");
            this.barDockControlTop.Font = null;
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.AccessibleDescription = null;
            this.barDockControlBottom.AccessibleName = null;
            resources.ApplyResources(this.barDockControlBottom, "barDockControlBottom");
            this.barDockControlBottom.Font = null;
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.AccessibleDescription = null;
            this.barDockControlLeft.AccessibleName = null;
            resources.ApplyResources(this.barDockControlLeft, "barDockControlLeft");
            this.barDockControlLeft.Font = null;
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.AccessibleDescription = null;
            this.barDockControlRight.AccessibleName = null;
            resources.ApplyResources(this.barDockControlRight, "barDockControlRight");
            this.barDockControlRight.Font = null;
            // 
            // barLinkContainerItem3
            // 
            this.barLinkContainerItem3.AccessibleDescription = null;
            this.barLinkContainerItem3.AccessibleName = null;
            resources.ApplyResources(this.barLinkContainerItem3, "barLinkContainerItem3");
            this.barLinkContainerItem3.Id = 6;
            this.barLinkContainerItem3.Name = "barLinkContainerItem3";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.AccessibleDescription = null;
            this.barButtonItem1.AccessibleName = null;
            this.barButtonItem1.ActAsDropDown = true;
            this.barButtonItem1.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            resources.ApplyResources(this.barButtonItem1, "barButtonItem1");
            this.barButtonItem1.Id = 7;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.AccessibleDescription = null;
            this.barButtonItem2.AccessibleName = null;
            resources.ApplyResources(this.barButtonItem2, "barButtonItem2");
            this.barButtonItem2.Id = 8;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // barSubItem1
            // 
            this.barSubItem1.AccessibleDescription = null;
            this.barSubItem1.AccessibleName = null;
            resources.ApplyResources(this.barSubItem1, "barSubItem1");
            this.barSubItem1.Id = 9;
            this.barSubItem1.Name = "barSubItem1";
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.AccessibleDescription = null;
            this.barButtonItem4.AccessibleName = null;
            resources.ApplyResources(this.barButtonItem4, "barButtonItem4");
            this.barButtonItem4.Id = 21;
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // ShellToolBarPart
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ShellToolBarPart";
            ((System.ComponentModel.ISupportInitialize)(this.barFormDesignToolBarManager)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarManager barFormDesignToolBarManager;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem barNew;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarLinkContainerItem barLinkContainerItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarSubItem barOpen;
        private DevExpress.XtraBars.BarButtonItem barOpenLocal;
        private DevExpress.XtraBars.BarButtonItem barOpenServer;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarSubItem barSave;
        private DevExpress.XtraBars.BarButtonItem barSaveLocal;
        private DevExpress.XtraBars.BarButtonItem barSaveServer;
        private DevExpress.XtraBars.BarButtonItem barCopy;
        private DevExpress.XtraBars.BarButtonItem barCut;
        private DevExpress.XtraBars.BarButtonItem barPaste;
        private DevExpress.XtraBars.BarButtonItem barDelete;
        private DevExpress.XtraBars.BarButtonItem barLeft;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem barTop;
        private DevExpress.XtraBars.BarButtonItem barBottom;
        private DevExpress.XtraBars.BarButtonItem barCenter;
        private DevExpress.XtraBars.BarButtonItem barRight;
        private DevExpress.XtraBars.BarButtonItem barMiddle;
        private DevExpress.XtraBars.BarButtonItem barViewCode;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraBars.BarButtonItem barUnDo;
        private DevExpress.XtraBars.BarButtonItem barReDo;
        private DevExpress.XtraBars.BarButtonItem barTabOrder;

    }
}
