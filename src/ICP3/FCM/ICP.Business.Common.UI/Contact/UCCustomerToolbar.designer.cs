using ICP.Framework.CommonLibrary.Client;
namespace ICP.Business.Common.UI.Contact
{
    partial class UCCustomerToolbar
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
            this.barItemAddNew = new DevExpress.XtraBars.BarButtonItem();
            this.barItemRemove = new DevExpress.XtraBars.BarButtonItem();
            this.barItemSave = new DevExpress.XtraBars.BarButtonItem();
            this.barItemType = new DevExpress.XtraBars.BarEditItem();
            this.radioRole = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.barItemType2 = new DevExpress.XtraBars.BarEditItem();
            this.checkComb = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioRole)).BeginInit();
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
            this.barItemAddNew,
            this.barItemRemove,
            this.barItemSave,
            this.barItemType,
            this.barItemType2
            });
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 5;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.radioRole});
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barItemAddNew, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barItemRemove, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemSave),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.barItemType, "", true, true, true, 179),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.barItemType2, "", true, true, true, 80)
            });
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barItemAddNew
            // 
            this.barItemAddNew.Caption = "Add";
            this.barItemAddNew.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Add;
            this.barItemAddNew.Id = 0;
            this.barItemAddNew.Name = "barItemAddNew";
            // 
            // barItemRemove
            // 
            this.barItemRemove.Caption = "Remove";
            this.barItemRemove.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Delete_16;
            this.barItemRemove.Id = 1;
            this.barItemRemove.Name = "barItemRemove";
            // 
            // barItemSave
            // 
            this.barItemSave.Caption = "OK";
            this.barItemSave.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Save;
            this.barItemSave.Id = 3;
            this.barItemSave.Name = "barItemSave";
            this.barItemSave.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barItemType
            // 
            this.barItemType.Caption = "Type";
            this.barItemType.Edit = this.radioRole;
            this.barItemType.Id = 4;
            this.barItemType.Name = "barItemType";
            this.barItemType.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
           
            // 
            // repositoryItemRadioGroup1
            // 
            this.radioRole.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Customer"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "Carrier")});
            this.radioRole.Name = "repositoryItemRadioGroup1";
            this.radioRole.EditValueChanged += new System.EventHandler(this.repositoryItemRadioGroup1_EditValueChanged);

            this.barItemType2.Caption = "Stage";
            this.barItemType2.Edit = this.checkComb;
            this.barItemType2.Id = 5;
            this.barItemType2.Name = "barStage";
            this.barItemType2.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;

            this.barItemType2.EditValueChanged += new System.EventHandler(checkComb_EditValueChanged);

            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(679, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 28);
            this.barDockControlBottom.Size = new System.Drawing.Size(679, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 2);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(679, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 2);
            // 
            // UCCustomerToolbar
            // 
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "UCCustomerToolbar";
            this.Size = new System.Drawing.Size(679, 28);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioRole)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barItemAddNew;
        private DevExpress.XtraBars.BarButtonItem barItemRemove;
        private DevExpress.XtraBars.BarButtonItem barItemSave;
        private DevExpress.XtraBars.BarEditItem barItemType;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup radioRole;

        private DevExpress.XtraBars.BarEditItem barItemType2;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit checkComb;
    }
}
