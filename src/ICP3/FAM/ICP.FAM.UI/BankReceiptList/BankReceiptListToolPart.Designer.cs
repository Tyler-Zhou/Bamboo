﻿namespace ICP.FAM.UI.BankReceiptList
{
    partial class BankReceiptListToolPart
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
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.bbiAudit = new DevExpress.XtraBars.BarButtonItem();
            this.bbiCancelAudit = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barSearch = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barEdit,
            this.barDelete,
            this.barClose,
            this.barAdd,
            this.barSearch,
            this.bbiAudit,
            this.bbiCancelAudit});
            this.barManager1.MaxItemId = 35;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAdd, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barEdit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barDelete, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiAudit, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiCancelAudit),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barAdd
            // 
            this.barAdd.Caption = "新增(&A)";
            this.barAdd.Glyph = global::ICP.FAM.UI.Properties.Resources.Add_File_16;
            this.barAdd.Id = 5;
            this.barAdd.Name = "barAdd";
            // 
            // barEdit
            // 
            this.barEdit.Caption = "编辑(&E)";
            this.barEdit.Glyph = global::ICP.FAM.UI.Properties.Resources.Edit_16;
            this.barEdit.Id = 0;
            this.barEdit.Name = "barEdit";
            // 
            // barDelete
            // 
            this.barDelete.Caption = "作废(&D)";
            this.barDelete.Glyph = global::ICP.FAM.UI.Properties.Resources.Empty;
            this.barDelete.Id = 1;
            this.barDelete.Name = "barDelete";
            // 
            // barClose
            // 
            this.barClose.Caption = "关闭(&C)";
            this.barClose.Glyph = global::ICP.FAM.UI.Properties.Resources.Close_16;
            this.barClose.Id = 3;
            this.barClose.Name = "barClose";
            // 
            // bbiAudit
            // 
            this.bbiAudit.Caption = "审核(&A)";
            this.bbiAudit.Glyph = global::ICP.FAM.UI.Properties.Resources.Review;
            this.bbiAudit.Id = 33;
            this.bbiAudit.Name = "bbiAudit";
            this.bbiAudit.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // bbiCancelAudit
            // 
            this.bbiCancelAudit.Caption = "取消审核(&C)";
            this.bbiCancelAudit.Glyph = global::ICP.FAM.UI.Properties.Resources.CancelBullion;
            this.bbiCancelAudit.Id = 34;
            this.bbiCancelAudit.Name = "bbiCancelAudit";
            this.bbiCancelAudit.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1090, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 29);
            this.barDockControlBottom.Size = new System.Drawing.Size(1090, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 3);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1090, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 3);
            // 
            // barSearch
            // 
            this.barSearch.Caption = "查询(&Q)";
            this.barSearch.Glyph = global::ICP.FAM.UI.Properties.Resources.Preview;
            this.barSearch.Id = 6;
            this.barSearch.Name = "barSearch";
            // 
            // BankReceiptListToolPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "BankReceiptListToolPart";
            this.Size = new System.Drawing.Size(1090, 29);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barEdit;
        private DevExpress.XtraBars.BarButtonItem barDelete;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraBars.BarButtonItem barAdd;
        private DevExpress.XtraBars.BarButtonItem barSearch;
        private DevExpress.XtraBars.BarButtonItem bbiAudit;
        private DevExpress.XtraBars.BarButtonItem bbiCancelAudit;
    }
}
