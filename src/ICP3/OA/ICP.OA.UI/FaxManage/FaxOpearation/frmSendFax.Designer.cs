using System.Windows.Forms;
namespace ICP.OA.UI.FaxManage
{
    partial class frmSendFax
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSendFax));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCC = new DevExpress.XtraEditors.LabelControl();
            this.lblTo = new DevExpress.XtraEditors.LabelControl();
            this.btnContent = new DevExpress.XtraEditors.SimpleButton();
            this.imageLBAttachment = new DevExpress.XtraEditors.ImageListBoxControl();
            this.imageListToolBar = new System.Windows.Forms.ImageList(this.components);
            this.labSubject = new DevExpress.XtraEditors.LabelControl();
            this.txtSubject = new DevExpress.XtraEditors.TextEdit();
            this.txtMailTo = new DevExpress.XtraEditors.TextEdit();
            this.txtCC = new DevExpress.XtraEditors.TextEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rtxtContent = new DevExpress.XtraRichEdit.RichEditControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barSend = new DevExpress.XtraBars.BarButtonItem();
            this.barPriority = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenuPriority = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barHigh = new DevExpress.XtraBars.BarCheckItem();
            this.barNormal = new DevExpress.XtraBars.BarCheckItem();
            this.barLow = new DevExpress.XtraBars.BarCheckItem();
            this.barAttachment = new DevExpress.XtraBars.BarButtonItem();
            this.barCheck = new DevExpress.XtraBars.BarButtonItem();
            this.barItemSaveDraft = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barFont = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlRcMain = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.fontBar1 = new DevExpress.XtraRichEdit.UI.FontBar();
            this.undoItem1 = new DevExpress.XtraRichEdit.UI.UndoItem();
            this.redoItem1 = new DevExpress.XtraRichEdit.UI.RedoItem();
            this.changeFontNameItem1 = new DevExpress.XtraRichEdit.UI.ChangeFontNameItem();
            this.repositoryItemFontEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemFontEdit();
            this.changeFontSizeItem1 = new DevExpress.XtraRichEdit.UI.ChangeFontSizeItem();
            this.repositoryItemRichEditFontSizeEdit2 = new DevExpress.XtraRichEdit.Design.RepositoryItemRichEditFontSizeEdit();
            this.changeFontColorItem1 = new DevExpress.XtraRichEdit.UI.ChangeFontColorItem();
            this.changeFontBackColorItem1 = new DevExpress.XtraRichEdit.UI.ChangeFontBackColorItem();
            this.toggleFontBoldItem1 = new DevExpress.XtraRichEdit.UI.ToggleFontBoldItem();
            this.toggleFontItalicItem1 = new DevExpress.XtraRichEdit.UI.ToggleFontItalicItem();
            this.toggleFontUnderlineItem1 = new DevExpress.XtraRichEdit.UI.ToggleFontUnderlineItem();
            this.toggleFontStrikeoutItem1 = new DevExpress.XtraRichEdit.UI.ToggleFontStrikeoutItem();
            this.toggleParagraphAlignmentLeftItem1 = new DevExpress.XtraRichEdit.UI.ToggleParagraphAlignmentLeftItem();
            this.toggleParagraphAlignmentCenterItem1 = new DevExpress.XtraRichEdit.UI.ToggleParagraphAlignmentCenterItem();
            this.toggleParagraphAlignmentRightItem1 = new DevExpress.XtraRichEdit.UI.ToggleParagraphAlignmentRightItem();
            this.toggleParagraphAlignmentJustifyItem1 = new DevExpress.XtraRichEdit.UI.ToggleParagraphAlignmentJustifyItem();
            this.barDockControlRichEdit = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barRemoveAchment = new DevExpress.XtraBars.BarButtonItem();
            this.barOpenFile = new DevExpress.XtraBars.BarButtonItem();
            this.barAddAttachment = new DevExpress.XtraBars.BarButtonItem();
            this.fileNewItem1 = new DevExpress.XtraRichEdit.UI.FileNewItem();
            this.setSingleParagraphSpacingItem1 = new DevExpress.XtraRichEdit.UI.SetSingleParagraphSpacingItem();
            this.setSesquialteralParagraphSpacingItem1 = new DevExpress.XtraRichEdit.UI.SetSesquialteralParagraphSpacingItem();
            this.setDoubleParagraphSpacingItem1 = new DevExpress.XtraRichEdit.UI.SetDoubleParagraphSpacingItem();
            this.showLineSpacingFormItem1 = new DevExpress.XtraRichEdit.UI.ShowLineSpacingFormItem();
            this.addSpacingBeforeParagraphItem1 = new DevExpress.XtraRichEdit.UI.AddSpacingBeforeParagraphItem();
            this.removeSpacingBeforeParagraphItem1 = new DevExpress.XtraRichEdit.UI.RemoveSpacingBeforeParagraphItem();
            this.addSpacingAfterParagraphItem1 = new DevExpress.XtraRichEdit.UI.AddSpacingAfterParagraphItem();
            this.removeSpacingAfterParagraphItem1 = new DevExpress.XtraRichEdit.UI.RemoveSpacingAfterParagraphItem();
            this.setSingleParagraphSpacingItem2 = new DevExpress.XtraRichEdit.UI.SetSingleParagraphSpacingItem();
            this.setSesquialteralParagraphSpacingItem2 = new DevExpress.XtraRichEdit.UI.SetSesquialteralParagraphSpacingItem();
            this.setDoubleParagraphSpacingItem2 = new DevExpress.XtraRichEdit.UI.SetDoubleParagraphSpacingItem();
            this.showLineSpacingFormItem2 = new DevExpress.XtraRichEdit.UI.ShowLineSpacingFormItem();
            this.addSpacingBeforeParagraphItem2 = new DevExpress.XtraRichEdit.UI.AddSpacingBeforeParagraphItem();
            this.removeSpacingBeforeParagraphItem2 = new DevExpress.XtraRichEdit.UI.RemoveSpacingBeforeParagraphItem();
            this.addSpacingAfterParagraphItem2 = new DevExpress.XtraRichEdit.UI.AddSpacingAfterParagraphItem();
            this.removeSpacingAfterParagraphItem2 = new DevExpress.XtraRichEdit.UI.RemoveSpacingAfterParagraphItem();
            this.setSingleParagraphSpacingItem3 = new DevExpress.XtraRichEdit.UI.SetSingleParagraphSpacingItem();
            this.setSesquialteralParagraphSpacingItem3 = new DevExpress.XtraRichEdit.UI.SetSesquialteralParagraphSpacingItem();
            this.setDoubleParagraphSpacingItem3 = new DevExpress.XtraRichEdit.UI.SetDoubleParagraphSpacingItem();
            this.showLineSpacingFormItem3 = new DevExpress.XtraRichEdit.UI.ShowLineSpacingFormItem();
            this.addSpacingBeforeParagraphItem3 = new DevExpress.XtraRichEdit.UI.AddSpacingBeforeParagraphItem();
            this.removeSpacingBeforeParagraphItem3 = new DevExpress.XtraRichEdit.UI.RemoveSpacingBeforeParagraphItem();
            this.addSpacingAfterParagraphItem3 = new DevExpress.XtraRichEdit.UI.AddSpacingAfterParagraphItem();
            this.removeSpacingAfterParagraphItem3 = new DevExpress.XtraRichEdit.UI.RemoveSpacingAfterParagraphItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemFontEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemFontEdit();
            this.repositoryItemRichEditFontSizeEdit1 = new DevExpress.XtraRichEdit.Design.RepositoryItemRichEditFontSizeEdit();
            this.repositoryItemRichEditStyleEdit1 = new DevExpress.XtraRichEdit.Design.RepositoryItemRichEditStyleEdit();
            this.repositoryItemRichEditStyleEdit2 = new DevExpress.XtraRichEdit.Design.RepositoryItemRichEditStyleEdit();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.labTip = new System.Windows.Forms.LinkLabel();
            this.popupMenuAttachment = new DevExpress.XtraBars.PopupMenu(this.components);
            this.richEditBarController1 = new DevExpress.XtraRichEdit.UI.RichEditBarController();
            this.toggleFontDoubleUnderlineItem1 = new DevExpress.XtraRichEdit.UI.ToggleFontDoubleUnderlineItem();
            this.dxErrorProvider2 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.imageListAttachment = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageLBAttachment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMailTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCC.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuPriority)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemFontEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichEditFontSizeEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemFontEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichEditFontSizeEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichEditStyleEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichEditStyleEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuAttachment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.richEditBarController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblCC);
            this.groupBox1.Controls.Add(this.lblTo);
            this.groupBox1.Controls.Add(this.btnContent);
            this.groupBox1.Controls.Add(this.imageLBAttachment);
            this.groupBox1.Controls.Add(this.labSubject);
            this.groupBox1.Controls.Add(this.txtSubject);
            this.groupBox1.Controls.Add(this.txtMailTo);
            this.groupBox1.Controls.Add(this.txtCC);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(800, 87);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // lblCC
            // 
            this.lblCC.Location = new System.Drawing.Point(17, 34);
            this.lblCC.Name = "lblCC";
            this.lblCC.Size = new System.Drawing.Size(14, 14);
            this.lblCC.TabIndex = 32;
            this.lblCC.Text = "CC";
            // 
            // lblTo
            // 
            this.lblTo.Location = new System.Drawing.Point(17, 13);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(15, 14);
            this.lblTo.TabIndex = 31;
            this.lblTo.Text = "To";
            // 
            // btnContent
            // 
            this.btnContent.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnContent.Image = global::ICP.OA.UI.Properties.Resources.fax1;
            this.btnContent.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnContent.Location = new System.Drawing.Point(12, 86);
            this.btnContent.Name = "btnContent";
            this.btnContent.Size = new System.Drawing.Size(56, 23);
            this.btnContent.TabIndex = 30;
            this.btnContent.TabStop = false;
            this.btnContent.Text = "Fax";
            this.btnContent.Click += new System.EventHandler(this.btnContent_Click);
            // 
            // imageLBAttachment
            // 
            this.imageLBAttachment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.imageLBAttachment.ImageList = this.imageListAttachment;
            this.imageLBAttachment.SelectionMode = SelectionMode.One;
            this.imageLBAttachment.Location = new System.Drawing.Point(88, 86);
            this.imageLBAttachment.Name = "imageLBAttachment";
           
            this.imageLBAttachment.Size = new System.Drawing.Size(704, 38);
            this.imageLBAttachment.TabIndex = 28;
            this.imageLBAttachment.MouseClick += new System.Windows.Forms.MouseEventHandler(this.imageLBAttachment_MouseClick);
            this.imageLBAttachment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.imageLBAttachment_KeyDown);
            // 
            // imageListToolBar
            // 
            this.imageListToolBar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListToolBar.ImageStream")));
            this.imageListToolBar.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListToolBar.Images.SetKeyName(0, "Low.png");
            this.imageListToolBar.Images.SetKeyName(1, "Nor.png");
            this.imageListToolBar.Images.SetKeyName(2, "20071126201302634.png");
            this.imageListToolBar.Images.SetKeyName(3, "Attachment.png");
            this.imageListToolBar.Images.SetKeyName(4, "Check.png");
            this.imageListToolBar.Images.SetKeyName(5, "High.png");
            this.imageListToolBar.Images.SetKeyName(6, "send.png");
            // 
            // labSubject
            // 
            this.labSubject.Location = new System.Drawing.Point(17, 61);
            this.labSubject.Name = "labSubject";
            this.labSubject.Size = new System.Drawing.Size(42, 14);
            this.labSubject.TabIndex = 25;
            this.labSubject.Text = "Subject";
            // 
            // txtSubject
            // 
            this.txtSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dxErrorProvider2.SetIconAlignment(this.txtSubject, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dxErrorProvider1.SetIconAlignment(this.txtSubject, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtSubject.Location = new System.Drawing.Point(88, 60);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Properties.MaxLength = 500;
            this.txtSubject.Size = new System.Drawing.Size(705, 21);
            this.txtSubject.TabIndex = 2;
            // 
            // txtMailTo
            // 
            this.txtMailTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dxErrorProvider2.SetIconAlignment(this.txtMailTo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dxErrorProvider1.SetIconAlignment(this.txtMailTo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtMailTo.Location = new System.Drawing.Point(88, 10);
            this.txtMailTo.Name = "txtMailTo";
            this.txtMailTo.Size = new System.Drawing.Size(705, 21);
            this.txtMailTo.TabIndex = 0;
            // 
            // txtCC
            // 
            this.txtCC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dxErrorProvider2.SetIconAlignment(this.txtCC, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dxErrorProvider1.SetIconAlignment(this.txtCC, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtCC.Location = new System.Drawing.Point(88, 35);
            this.txtCC.Name = "txtCC";
            this.txtCC.Size = new System.Drawing.Size(705, 21);
            this.txtCC.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rtxtContent);
            this.groupBox2.Controls.Add(this.barDockControlRichEdit);
            this.groupBox2.Controls.Add(this.barDockControlRcMain);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 129);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(800, 448);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // rtxtContent
            // 
            this.rtxtContent.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Simple;
            this.rtxtContent.Appearance.Text.Options.UseFont = true;
            this.rtxtContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtContent.ImeMode = System.Windows.Forms.ImeMode.On;
            this.rtxtContent.Location = new System.Drawing.Point(3, 72);
            this.rtxtContent.MenuManager = this.barManager1;
            this.rtxtContent.Name = "rtxtContent";
            this.rtxtContent.Options.DocumentCapabilities.InlinePictures = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.rtxtContent.Options.DocumentCapabilities.ParagraphFormatting = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.rtxtContent.Options.DocumentCapabilities.Paragraphs = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.rtxtContent.Options.DocumentCapabilities.ParagraphStyle = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            this.rtxtContent.Options.Export.Html.Encoding = ((System.Text.Encoding)(resources.GetObject("rtxtContent.Options.Export.Html.Encoding")));
            this.rtxtContent.Options.Export.Html.ExportRootTag = DevExpress.XtraRichEdit.Export.Html.ExportRootTag.Body;
            this.rtxtContent.Options.FormattingMarkVisibility.ShowHiddenText = false;
            this.rtxtContent.Options.Import.Html.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("rtxtContent.Options.Import.Html.ActualEncoding")));
            this.rtxtContent.Options.Import.Html.AsyncImageLoading = true;
            this.rtxtContent.Options.Import.Html.Encoding = ((System.Text.Encoding)(resources.GetObject("rtxtContent.Options.Import.Html.Encoding")));
            this.rtxtContent.Options.Import.Mht.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("rtxtContent.Options.Import.Mht.ActualEncoding")));
            this.rtxtContent.Options.Import.Mht.AsyncImageLoading = true;
            this.rtxtContent.Options.Import.OpenDocument.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("rtxtContent.Options.Import.OpenDocument.ActualEncoding")));
            this.rtxtContent.Options.Import.OpenXml.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("rtxtContent.Options.Import.OpenXml.ActualEncoding")));
            this.rtxtContent.Options.Import.PlainText.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("rtxtContent.Options.Import.PlainText.ActualEncoding")));
            this.rtxtContent.Options.Import.Rtf.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("rtxtContent.Options.Import.Rtf.ActualEncoding")));
            this.rtxtContent.Options.Import.WordML.ActualEncoding = ((System.Text.Encoding)(resources.GetObject("rtxtContent.Options.Import.WordML.ActualEncoding")));
            this.rtxtContent.Size = new System.Drawing.Size(794, 373);
            this.rtxtContent.TabIndex = 0;
            this.rtxtContent.Views.SimpleView.HidePartiallyVisibleRow = false;
            // 
            // barManager1
            // 
            this.barManager1.AllowCustomization = false;
            this.barManager1.AllowMoveBarOnToolbar = false;
            this.barManager1.AllowQuickCustomization = false;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar3,
            this.fontBar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockControls.Add(this.barDockControlRcMain);
            this.barManager1.DockControls.Add(this.barDockControlRichEdit);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSend,
            this.barPriority,
            this.barClose,
            this.barHigh,
            this.barNormal,
            this.barLow,
            this.barAttachment,
            this.barCheck,
            this.barRemoveAchment,
            this.barOpenFile,
            this.barAddAttachment,
            this.barFont,
            this.fileNewItem1,
            this.undoItem1,
            this.redoItem1,
            this.toggleFontBoldItem1,
            this.toggleFontItalicItem1,
            this.toggleFontUnderlineItem1,
            this.toggleFontStrikeoutItem1,
            this.toggleParagraphAlignmentLeftItem1,
            this.toggleParagraphAlignmentCenterItem1,
            this.toggleParagraphAlignmentRightItem1,
            this.toggleParagraphAlignmentJustifyItem1,
            this.setSingleParagraphSpacingItem1,
            this.setSesquialteralParagraphSpacingItem1,
            this.setDoubleParagraphSpacingItem1,
            this.showLineSpacingFormItem1,
            this.addSpacingBeforeParagraphItem1,
            this.removeSpacingBeforeParagraphItem1,
            this.addSpacingAfterParagraphItem1,
            this.removeSpacingAfterParagraphItem1,
            this.setSingleParagraphSpacingItem2,
            this.setSesquialteralParagraphSpacingItem2,
            this.setDoubleParagraphSpacingItem2,
            this.showLineSpacingFormItem2,
            this.addSpacingBeforeParagraphItem2,
            this.removeSpacingBeforeParagraphItem2,
            this.addSpacingAfterParagraphItem2,
            this.removeSpacingAfterParagraphItem2,
            this.changeFontNameItem1,
            this.changeFontSizeItem1,
            this.changeFontColorItem1,
            this.changeFontBackColorItem1,
            this.setSingleParagraphSpacingItem3,
            this.setSesquialteralParagraphSpacingItem3,
            this.setDoubleParagraphSpacingItem3,
            this.showLineSpacingFormItem3,
            this.addSpacingBeforeParagraphItem3,
            this.removeSpacingBeforeParagraphItem3,
            this.addSpacingAfterParagraphItem3,
            this.removeSpacingAfterParagraphItem3,
            this.barItemSaveDraft});
            this.barManager1.LargeImages = this.imageListToolBar;
            this.barManager1.MaxItemId = 121;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemFontEdit1,
            this.repositoryItemRichEditFontSizeEdit1,
            this.repositoryItemRichEditStyleEdit1,
            this.repositoryItemRichEditStyleEdit2,
            this.repositoryItemFontEdit2,
            this.repositoryItemRichEditFontSizeEdit2});
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 2";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSend, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barPriority),
            new DevExpress.XtraBars.LinkPersistInfo(this.barAttachment),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheck),
            new DevExpress.XtraBars.LinkPersistInfo(this.barItemSaveDraft),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableCustomization = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Custom 2";
            // 
            // barSend
            // 
            this.barSend.Caption = "&Send";
            this.barSend.Glyph = global::ICP.OA.UI.Properties.Resources.send;
            this.barSend.Id = 0;
            this.barSend.ImageIndex = 0;
            this.barSend.LargeGlyph = global::ICP.OA.UI.Properties.Resources.send;
            this.barSend.Name = "barSend";
            this.barSend.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barSend.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSend_ItemClick);
            // 
            // barPriority
            // 
            this.barPriority.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.barPriority.Caption = "Normal";
            this.barPriority.DropDownControl = this.popupMenuPriority;
            this.barPriority.Glyph = ((System.Drawing.Image)(resources.GetObject("barPriority.Glyph")));
            this.barPriority.Id = 3;
            this.barPriority.Name = "barPriority";
            this.barPriority.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barPriority.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPriority_ItemClick);
            // 
            // popupMenuPriority
            // 
            this.popupMenuPriority.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barHigh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barNormal, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barLow, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.popupMenuPriority.Manager = this.barManager1;
            this.popupMenuPriority.Name = "popupMenuPriority";
            // 
            // barHigh
            // 
            this.barHigh.Caption = "High";
            this.barHigh.Glyph = global::ICP.OA.UI.Properties.Resources.High;
            this.barHigh.Id = 4;
            this.barHigh.Name = "barHigh";
            this.barHigh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barHigh_ItemClick);
            // 
            // barNormal
            // 
            this.barNormal.Caption = "Normal";
            this.barNormal.Checked = true;
            this.barNormal.Glyph = ((System.Drawing.Image)(resources.GetObject("barNormal.Glyph")));
            this.barNormal.Id = 5;
            this.barNormal.Name = "barNormal";
            this.barNormal.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barNormal_ItemClick);
            // 
            // barLow
            // 
            this.barLow.Caption = "Low";
            this.barLow.Glyph = global::ICP.OA.UI.Properties.Resources.Low;
            this.barLow.Id = 6;
            this.barLow.Name = "barLow";
            this.barLow.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLow_ItemClick);
            // 
            // barAttachment
            // 
            this.barAttachment.Caption = "Attachment";
            this.barAttachment.Glyph = global::ICP.OA.UI.Properties.Resources.Attachment;
            this.barAttachment.Id = 7;
            this.barAttachment.Name = "barAttachment";
            this.barAttachment.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barAttachment.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAttachment_ItemClick);
            // 
            // barCheck
            // 
            this.barCheck.Caption = "Check";
            this.barCheck.Glyph = global::ICP.OA.UI.Properties.Resources.Check;
            this.barCheck.Id = 8;
            this.barCheck.Name = "barCheck";
            this.barCheck.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barCheck.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheck_ItemClick);
            // 
            // barItemSaveDraft
            // 
            this.barItemSaveDraft.Caption = "Sa&ve";
            this.barItemSaveDraft.Glyph = global::ICP.OA.UI.Properties.Resources.Save;
            this.barItemSaveDraft.Id = 120;
            this.barItemSaveDraft.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S));
            this.barItemSaveDraft.Name = "barItemSaveDraft";
            this.barItemSaveDraft.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barItemSaveDraft.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barItemSaveDraft_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "Close";
            this.barClose.Glyph = global::ICP.OA.UI.Properties.Resources.Transfer;
            this.barClose.Id = 9;
            this.barClose.Name = "barClose";
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // bar3
            // 
            this.bar3.BarName = "Custom 4";
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone;
            this.bar3.FloatLocation = new System.Drawing.Point(56, 307);
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barFont, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DisableCustomization = true;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.StandaloneBarDockControl = this.barDockControlRcMain;
            this.bar3.Text = "Custom 4";
            // 
            // barFont
            // 
            this.barFont.Caption = "Font";
            this.barFont.Glyph = global::ICP.OA.UI.Properties.Resources.Font_S;
            this.barFont.Id = 15;
            this.barFont.Name = "barFont";
            this.barFont.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barFont_ItemClick);
            // 
            // barDockControlRcMain
            // 
            this.barDockControlRcMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlRcMain.Location = new System.Drawing.Point(3, 18);
            this.barDockControlRcMain.Name = "barDockControlRcMain";
            this.barDockControlRcMain.Size = new System.Drawing.Size(794, 27);
            this.barDockControlRcMain.Text = "standaloneBarDockControl1";
            // 
            // fontBar1
            // 
            this.fontBar1.DockCol = 0;
            this.fontBar1.DockRow = 0;
            this.fontBar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone;
            this.fontBar1.FloatLocation = new System.Drawing.Point(161, 456);
            this.fontBar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.undoItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.redoItem1),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.changeFontNameItem1, "", true, true, true, 83),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.changeFontSizeItem1, "", false, true, true, 50),
            new DevExpress.XtraBars.LinkPersistInfo(this.changeFontColorItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.changeFontBackColorItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.toggleFontBoldItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.toggleFontItalicItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.toggleFontUnderlineItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.toggleFontStrikeoutItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.toggleParagraphAlignmentLeftItem1, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.toggleParagraphAlignmentCenterItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.toggleParagraphAlignmentRightItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.toggleParagraphAlignmentJustifyItem1)});
            this.fontBar1.OptionsBar.UseWholeRow = true;
            this.fontBar1.StandaloneBarDockControl = this.barDockControlRichEdit;
            // 
            // undoItem1
            // 
            this.undoItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("undoItem1.Glyph")));
            this.undoItem1.Id = 44;
            this.undoItem1.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z));
            this.undoItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("undoItem1.LargeGlyph")));
            this.undoItem1.Name = "undoItem1";
            // 
            // redoItem1
            // 
            this.redoItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("redoItem1.Glyph")));
            this.redoItem1.Id = 45;
            this.redoItem1.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y));
            this.redoItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("redoItem1.LargeGlyph")));
            this.redoItem1.Name = "redoItem1";
            // 
            // changeFontNameItem1
            // 
            this.changeFontNameItem1.Edit = this.repositoryItemFontEdit2;
            this.changeFontNameItem1.Id = 100;
            this.changeFontNameItem1.Name = "changeFontNameItem1";
            // 
            // repositoryItemFontEdit2
            // 
            this.repositoryItemFontEdit2.AutoHeight = false;
            this.repositoryItemFontEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemFontEdit2.Name = "repositoryItemFontEdit2";
            // 
            // changeFontSizeItem1
            // 
            this.changeFontSizeItem1.Edit = this.repositoryItemRichEditFontSizeEdit2;
            this.changeFontSizeItem1.Id = 101;
            this.changeFontSizeItem1.Name = "changeFontSizeItem1";
            // 
            // repositoryItemRichEditFontSizeEdit2
            // 
            this.repositoryItemRichEditFontSizeEdit2.AutoHeight = false;
            this.repositoryItemRichEditFontSizeEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemRichEditFontSizeEdit2.Control = this.rtxtContent;
            this.repositoryItemRichEditFontSizeEdit2.Name = "repositoryItemRichEditFontSizeEdit2";
            // 
            // changeFontColorItem1
            // 
            this.changeFontColorItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("changeFontColorItem1.Glyph")));
            this.changeFontColorItem1.Id = 102;
            this.changeFontColorItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("changeFontColorItem1.LargeGlyph")));
            this.changeFontColorItem1.Name = "changeFontColorItem1";
            // 
            // changeFontBackColorItem1
            // 
            this.changeFontBackColorItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("changeFontBackColorItem1.Glyph")));
            this.changeFontBackColorItem1.Id = 103;
            this.changeFontBackColorItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("changeFontBackColorItem1.LargeGlyph")));
            this.changeFontBackColorItem1.Name = "changeFontBackColorItem1";
            // 
            // toggleFontBoldItem1
            // 
            this.toggleFontBoldItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("toggleFontBoldItem1.Glyph")));
            this.toggleFontBoldItem1.Id = 50;
            this.toggleFontBoldItem1.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B));
            this.toggleFontBoldItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("toggleFontBoldItem1.LargeGlyph")));
            this.toggleFontBoldItem1.Name = "toggleFontBoldItem1";
            // 
            // toggleFontItalicItem1
            // 
            this.toggleFontItalicItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("toggleFontItalicItem1.Glyph")));
            this.toggleFontItalicItem1.Id = 51;
            this.toggleFontItalicItem1.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I));
            this.toggleFontItalicItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("toggleFontItalicItem1.LargeGlyph")));
            this.toggleFontItalicItem1.Name = "toggleFontItalicItem1";
            // 
            // toggleFontUnderlineItem1
            // 
            this.toggleFontUnderlineItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("toggleFontUnderlineItem1.Glyph")));
            this.toggleFontUnderlineItem1.Id = 52;
            this.toggleFontUnderlineItem1.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U));
            this.toggleFontUnderlineItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("toggleFontUnderlineItem1.LargeGlyph")));
            this.toggleFontUnderlineItem1.Name = "toggleFontUnderlineItem1";
            // 
            // toggleFontStrikeoutItem1
            // 
            this.toggleFontStrikeoutItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("toggleFontStrikeoutItem1.Glyph")));
            this.toggleFontStrikeoutItem1.Id = 53;
            this.toggleFontStrikeoutItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("toggleFontStrikeoutItem1.LargeGlyph")));
            this.toggleFontStrikeoutItem1.Name = "toggleFontStrikeoutItem1";
            // 
            // toggleParagraphAlignmentLeftItem1
            // 
            this.toggleParagraphAlignmentLeftItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("toggleParagraphAlignmentLeftItem1.Glyph")));
            this.toggleParagraphAlignmentLeftItem1.Id = 57;
            this.toggleParagraphAlignmentLeftItem1.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L));
            this.toggleParagraphAlignmentLeftItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("toggleParagraphAlignmentLeftItem1.LargeGlyph")));
            this.toggleParagraphAlignmentLeftItem1.Name = "toggleParagraphAlignmentLeftItem1";
            // 
            // toggleParagraphAlignmentCenterItem1
            // 
            this.toggleParagraphAlignmentCenterItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("toggleParagraphAlignmentCenterItem1.Glyph")));
            this.toggleParagraphAlignmentCenterItem1.Id = 58;
            this.toggleParagraphAlignmentCenterItem1.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E));
            this.toggleParagraphAlignmentCenterItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("toggleParagraphAlignmentCenterItem1.LargeGlyph")));
            this.toggleParagraphAlignmentCenterItem1.Name = "toggleParagraphAlignmentCenterItem1";
            // 
            // toggleParagraphAlignmentRightItem1
            // 
            this.toggleParagraphAlignmentRightItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("toggleParagraphAlignmentRightItem1.Glyph")));
            this.toggleParagraphAlignmentRightItem1.Id = 54;
            this.toggleParagraphAlignmentRightItem1.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R));
            this.toggleParagraphAlignmentRightItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("toggleParagraphAlignmentRightItem1.LargeGlyph")));
            this.toggleParagraphAlignmentRightItem1.Name = "toggleParagraphAlignmentRightItem1";
            // 
            // toggleParagraphAlignmentJustifyItem1
            // 
            this.toggleParagraphAlignmentJustifyItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("toggleParagraphAlignmentJustifyItem1.Glyph")));
            this.toggleParagraphAlignmentJustifyItem1.Id = 55;
            this.toggleParagraphAlignmentJustifyItem1.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.J));
            this.toggleParagraphAlignmentJustifyItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("toggleParagraphAlignmentJustifyItem1.LargeGlyph")));
            this.toggleParagraphAlignmentJustifyItem1.Name = "toggleParagraphAlignmentJustifyItem1";
            // 
            // barDockControlRichEdit
            // 
            this.barDockControlRichEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlRichEdit.Location = new System.Drawing.Point(3, 45);
            this.barDockControlRichEdit.Name = "barDockControlRichEdit";
            this.barDockControlRichEdit.Size = new System.Drawing.Size(794, 27);
            this.barDockControlRichEdit.Text = "standaloneBarDockControl1";
            this.barDockControlRichEdit.Visible = false;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(800, 42);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 600);
            this.barDockControlBottom.Size = new System.Drawing.Size(800, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 42);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 558);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(800, 42);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 558);
            // 
            // barRemoveAchment
            // 
            this.barRemoveAchment.Caption = "&Remove Achment";
            this.barRemoveAchment.Glyph = global::ICP.OA.UI.Properties.Resources.Delete_S;
            this.barRemoveAchment.Id = 9;
            this.barRemoveAchment.Name = "barRemoveAchment";
            this.barRemoveAchment.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRemoveAchment_ItemClick);
            // 
            // barOpenFile
            // 
            this.barOpenFile.Caption = "Open File";
            this.barOpenFile.Glyph = global::ICP.OA.UI.Properties.Resources.OpenFile_S;
            this.barOpenFile.Id = 10;
            this.barOpenFile.Name = "barOpenFile";
            this.barOpenFile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barOpenFile_ItemClick);
            // 
            // barAddAttachment
            // 
            this.barAddAttachment.Caption = "Add Achment";
            this.barAddAttachment.Glyph = global::ICP.OA.UI.Properties.Resources.Attachment_S;
            this.barAddAttachment.Id = 12;
            this.barAddAttachment.Name = "barAddAttachment";
            this.barAddAttachment.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAddAchment_ItemClick);
            // 
            // fileNewItem1
            // 
            this.fileNewItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("fileNewItem1.Glyph")));
            this.fileNewItem1.Id = 56;
            this.fileNewItem1.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N));
            this.fileNewItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("fileNewItem1.LargeGlyph")));
            this.fileNewItem1.Name = "fileNewItem1";
            // 
            // setSingleParagraphSpacingItem1
            // 
            this.setSingleParagraphSpacingItem1.Id = 59;
            this.setSingleParagraphSpacingItem1.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1));
            this.setSingleParagraphSpacingItem1.Name = "setSingleParagraphSpacingItem1";
            // 
            // setSesquialteralParagraphSpacingItem1
            // 
            this.setSesquialteralParagraphSpacingItem1.Id = 60;
            this.setSesquialteralParagraphSpacingItem1.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D5));
            this.setSesquialteralParagraphSpacingItem1.Name = "setSesquialteralParagraphSpacingItem1";
            // 
            // setDoubleParagraphSpacingItem1
            // 
            this.setDoubleParagraphSpacingItem1.Id = 61;
            this.setDoubleParagraphSpacingItem1.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2));
            this.setDoubleParagraphSpacingItem1.Name = "setDoubleParagraphSpacingItem1";
            // 
            // showLineSpacingFormItem1
            // 
            this.showLineSpacingFormItem1.Id = 62;
            this.showLineSpacingFormItem1.Name = "showLineSpacingFormItem1";
            // 
            // addSpacingBeforeParagraphItem1
            // 
            this.addSpacingBeforeParagraphItem1.Id = 63;
            this.addSpacingBeforeParagraphItem1.Name = "addSpacingBeforeParagraphItem1";
            // 
            // removeSpacingBeforeParagraphItem1
            // 
            this.removeSpacingBeforeParagraphItem1.Id = 64;
            this.removeSpacingBeforeParagraphItem1.Name = "removeSpacingBeforeParagraphItem1";
            // 
            // addSpacingAfterParagraphItem1
            // 
            this.addSpacingAfterParagraphItem1.Id = 65;
            this.addSpacingAfterParagraphItem1.Name = "addSpacingAfterParagraphItem1";
            // 
            // removeSpacingAfterParagraphItem1
            // 
            this.removeSpacingAfterParagraphItem1.Id = 66;
            this.removeSpacingAfterParagraphItem1.Name = "removeSpacingAfterParagraphItem1";
            // 
            // setSingleParagraphSpacingItem2
            // 
            this.setSingleParagraphSpacingItem2.Id = 104;
            this.setSingleParagraphSpacingItem2.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1));
            this.setSingleParagraphSpacingItem2.Name = "setSingleParagraphSpacingItem2";
            // 
            // setSesquialteralParagraphSpacingItem2
            // 
            this.setSesquialteralParagraphSpacingItem2.Id = 105;
            this.setSesquialteralParagraphSpacingItem2.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D5));
            this.setSesquialteralParagraphSpacingItem2.Name = "setSesquialteralParagraphSpacingItem2";
            // 
            // setDoubleParagraphSpacingItem2
            // 
            this.setDoubleParagraphSpacingItem2.Id = 106;
            this.setDoubleParagraphSpacingItem2.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2));
            this.setDoubleParagraphSpacingItem2.Name = "setDoubleParagraphSpacingItem2";
            // 
            // showLineSpacingFormItem2
            // 
            this.showLineSpacingFormItem2.Id = 107;
            this.showLineSpacingFormItem2.Name = "showLineSpacingFormItem2";
            // 
            // addSpacingBeforeParagraphItem2
            // 
            this.addSpacingBeforeParagraphItem2.Id = 108;
            this.addSpacingBeforeParagraphItem2.Name = "addSpacingBeforeParagraphItem2";
            // 
            // removeSpacingBeforeParagraphItem2
            // 
            this.removeSpacingBeforeParagraphItem2.Id = 109;
            this.removeSpacingBeforeParagraphItem2.Name = "removeSpacingBeforeParagraphItem2";
            // 
            // addSpacingAfterParagraphItem2
            // 
            this.addSpacingAfterParagraphItem2.Id = 110;
            this.addSpacingAfterParagraphItem2.Name = "addSpacingAfterParagraphItem2";
            // 
            // removeSpacingAfterParagraphItem2
            // 
            this.removeSpacingAfterParagraphItem2.Id = 111;
            this.removeSpacingAfterParagraphItem2.Name = "removeSpacingAfterParagraphItem2";
            // 
            // setSingleParagraphSpacingItem3
            // 
            this.setSingleParagraphSpacingItem3.Id = 112;
            this.setSingleParagraphSpacingItem3.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1));
            this.setSingleParagraphSpacingItem3.Name = "setSingleParagraphSpacingItem3";
            // 
            // setSesquialteralParagraphSpacingItem3
            // 
            this.setSesquialteralParagraphSpacingItem3.Id = 113;
            this.setSesquialteralParagraphSpacingItem3.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D5));
            this.setSesquialteralParagraphSpacingItem3.Name = "setSesquialteralParagraphSpacingItem3";
            // 
            // setDoubleParagraphSpacingItem3
            // 
            this.setDoubleParagraphSpacingItem3.Id = 114;
            this.setDoubleParagraphSpacingItem3.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2));
            this.setDoubleParagraphSpacingItem3.Name = "setDoubleParagraphSpacingItem3";
            // 
            // showLineSpacingFormItem3
            // 
            this.showLineSpacingFormItem3.Id = 115;
            this.showLineSpacingFormItem3.Name = "showLineSpacingFormItem3";
            // 
            // addSpacingBeforeParagraphItem3
            // 
            this.addSpacingBeforeParagraphItem3.Id = 116;
            this.addSpacingBeforeParagraphItem3.Name = "addSpacingBeforeParagraphItem3";
            // 
            // removeSpacingBeforeParagraphItem3
            // 
            this.removeSpacingBeforeParagraphItem3.Id = 117;
            this.removeSpacingBeforeParagraphItem3.Name = "removeSpacingBeforeParagraphItem3";
            // 
            // addSpacingAfterParagraphItem3
            // 
            this.addSpacingAfterParagraphItem3.Id = 118;
            this.addSpacingAfterParagraphItem3.Name = "addSpacingAfterParagraphItem3";
            // 
            // removeSpacingAfterParagraphItem3
            // 
            this.removeSpacingAfterParagraphItem3.Id = 119;
            this.removeSpacingAfterParagraphItem3.Name = "removeSpacingAfterParagraphItem3";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // repositoryItemFontEdit1
            // 
            this.repositoryItemFontEdit1.AutoHeight = false;
            this.repositoryItemFontEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemFontEdit1.Name = "repositoryItemFontEdit1";
            // 
            // repositoryItemRichEditFontSizeEdit1
            // 
            this.repositoryItemRichEditFontSizeEdit1.AutoHeight = false;
            this.repositoryItemRichEditFontSizeEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemRichEditFontSizeEdit1.Control = this.rtxtContent;
            this.repositoryItemRichEditFontSizeEdit1.Name = "repositoryItemRichEditFontSizeEdit1";
            // 
            // repositoryItemRichEditStyleEdit1
            // 
            this.repositoryItemRichEditStyleEdit1.AutoHeight = false;
            this.repositoryItemRichEditStyleEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemRichEditStyleEdit1.Control = this.rtxtContent;
            this.repositoryItemRichEditStyleEdit1.Name = "repositoryItemRichEditStyleEdit1";
            // 
            // repositoryItemRichEditStyleEdit2
            // 
            this.repositoryItemRichEditStyleEdit2.AutoHeight = false;
            this.repositoryItemRichEditStyleEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemRichEditStyleEdit2.Control = this.rtxtContent;
            this.repositoryItemRichEditStyleEdit2.Name = "repositoryItemRichEditStyleEdit2";
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.labTip);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 577);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 23);
            this.panel1.TabIndex = 1;
            // 
            // labTip
            // 
            this.labTip.AutoSize = true;
            this.labTip.Location = new System.Drawing.Point(1, 3);
            this.labTip.Name = "labTip";
            this.labTip.Size = new System.Drawing.Size(0, 14);
            this.labTip.TabIndex = 0;
            this.labTip.Visible = false;
            this.labTip.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.labTip_LinkClicked);
            // 
            // popupMenuAttachment
            // 
            this.popupMenuAttachment.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAddAttachment, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRemoveAchment, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barOpenFile, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.popupMenuAttachment.Manager = this.barManager1;
            this.popupMenuAttachment.Name = "popupMenuAttachment";
            // 
            // richEditBarController1
            // 
            this.richEditBarController1.BarItems.Add(this.fileNewItem1);
            this.richEditBarController1.BarItems.Add(this.undoItem1);
            this.richEditBarController1.BarItems.Add(this.redoItem1);
            this.richEditBarController1.BarItems.Add(this.toggleFontBoldItem1);
            this.richEditBarController1.BarItems.Add(this.toggleFontItalicItem1);
            this.richEditBarController1.BarItems.Add(this.toggleFontUnderlineItem1);
            this.richEditBarController1.BarItems.Add(this.toggleFontStrikeoutItem1);
            this.richEditBarController1.BarItems.Add(this.toggleParagraphAlignmentLeftItem1);
            this.richEditBarController1.BarItems.Add(this.toggleParagraphAlignmentCenterItem1);
            this.richEditBarController1.BarItems.Add(this.toggleParagraphAlignmentRightItem1);
            this.richEditBarController1.BarItems.Add(this.toggleParagraphAlignmentJustifyItem1);
            this.richEditBarController1.BarItems.Add(this.setSingleParagraphSpacingItem1);
            this.richEditBarController1.BarItems.Add(this.setSesquialteralParagraphSpacingItem1);
            this.richEditBarController1.BarItems.Add(this.setDoubleParagraphSpacingItem1);
            this.richEditBarController1.BarItems.Add(this.showLineSpacingFormItem1);
            this.richEditBarController1.BarItems.Add(this.addSpacingBeforeParagraphItem1);
            this.richEditBarController1.BarItems.Add(this.removeSpacingBeforeParagraphItem1);
            this.richEditBarController1.BarItems.Add(this.addSpacingAfterParagraphItem1);
            this.richEditBarController1.BarItems.Add(this.removeSpacingAfterParagraphItem1);
            this.richEditBarController1.BarItems.Add(this.setSingleParagraphSpacingItem2);
            this.richEditBarController1.BarItems.Add(this.setSesquialteralParagraphSpacingItem2);
            this.richEditBarController1.BarItems.Add(this.setDoubleParagraphSpacingItem2);
            this.richEditBarController1.BarItems.Add(this.showLineSpacingFormItem2);
            this.richEditBarController1.BarItems.Add(this.addSpacingBeforeParagraphItem2);
            this.richEditBarController1.BarItems.Add(this.removeSpacingBeforeParagraphItem2);
            this.richEditBarController1.BarItems.Add(this.addSpacingAfterParagraphItem2);
            this.richEditBarController1.BarItems.Add(this.removeSpacingAfterParagraphItem2);
            this.richEditBarController1.BarItems.Add(this.changeFontNameItem1);
            this.richEditBarController1.BarItems.Add(this.changeFontSizeItem1);
            this.richEditBarController1.BarItems.Add(this.changeFontColorItem1);
            this.richEditBarController1.BarItems.Add(this.changeFontBackColorItem1);
            this.richEditBarController1.BarItems.Add(this.setSingleParagraphSpacingItem3);
            this.richEditBarController1.BarItems.Add(this.setSesquialteralParagraphSpacingItem3);
            this.richEditBarController1.BarItems.Add(this.setDoubleParagraphSpacingItem3);
            this.richEditBarController1.BarItems.Add(this.showLineSpacingFormItem3);
            this.richEditBarController1.BarItems.Add(this.addSpacingBeforeParagraphItem3);
            this.richEditBarController1.BarItems.Add(this.removeSpacingBeforeParagraphItem3);
            this.richEditBarController1.BarItems.Add(this.addSpacingAfterParagraphItem3);
            this.richEditBarController1.BarItems.Add(this.removeSpacingAfterParagraphItem3);
            this.richEditBarController1.RichEditControl = this.rtxtContent;
            // 
            // toggleFontDoubleUnderlineItem1
            // 
            this.toggleFontDoubleUnderlineItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("toggleFontDoubleUnderlineItem1.Glyph")));
            this.toggleFontDoubleUnderlineItem1.Id = 29;
            this.toggleFontDoubleUnderlineItem1.ItemShortcut = new DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                            | System.Windows.Forms.Keys.D));
            this.toggleFontDoubleUnderlineItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("toggleFontDoubleUnderlineItem1.LargeGlyph")));
            this.toggleFontDoubleUnderlineItem1.Name = "toggleFontDoubleUnderlineItem1";
            // 
            // dxErrorProvider2
            // 
            this.dxErrorProvider2.ContainerControl = this;
            // 
            // imageListAttachment
            // 
            this.imageListAttachment.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListAttachment.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListAttachment.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // frmSendFax
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmSendFax";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Send Fax";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SendEMailForm_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageLBAttachment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMailTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCC.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuPriority)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemFontEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichEditFontSizeEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemFontEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichEditFontSizeEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichEditStyleEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichEditStyleEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuAttachment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.richEditBarController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider2)).EndInit();
            this.ResumeLayout(false);

        }

        void SendEMailForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraRichEdit.RichEditControl rtxtContent;
        private DevExpress.XtraEditors.LabelControl labSubject;
        private DevExpress.XtraEditors.TextEdit txtSubject;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarButtonItem barSend;
        private DevExpress.XtraBars.BarButtonItem barPriority;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraBars.PopupMenu popupMenuPriority;
        private DevExpress.XtraBars.BarCheckItem barHigh;
        private DevExpress.XtraBars.BarCheckItem barNormal;
     
        private DevExpress.XtraBars.BarCheckItem barLow;
        private DevExpress.XtraBars.BarButtonItem barAttachment;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraBars.BarButtonItem barCheck;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.ImageListBoxControl imageLBAttachment;
        private System.Windows.Forms.ImageList imageListToolBar;
        private DevExpress.XtraBars.BarButtonItem barRemoveAchment;
        private DevExpress.XtraBars.BarButtonItem barOpenFile;
        private DevExpress.XtraBars.PopupMenu popupMenuAttachment;
        private DevExpress.XtraEditors.TextEdit txtMailTo;
        private DevExpress.XtraEditors.TextEdit txtCC;
        private System.Windows.Forms.LinkLabel labTip;
        private DevExpress.XtraBars.BarButtonItem barAddAttachment;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.StandaloneBarDockControl barDockControlRcMain;
        private DevExpress.XtraBars.StandaloneBarDockControl barDockControlRichEdit;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem barFont;
        private DevExpress.XtraRichEdit.UI.UndoItem undoItem1;
        private DevExpress.XtraRichEdit.UI.RedoItem redoItem1;
        private DevExpress.XtraRichEdit.UI.FontBar fontBar1;
        private DevExpress.XtraEditors.Repository.RepositoryItemFontEdit repositoryItemFontEdit1;
        private DevExpress.XtraRichEdit.Design.RepositoryItemRichEditFontSizeEdit repositoryItemRichEditFontSizeEdit1;
        private DevExpress.XtraRichEdit.UI.ToggleFontBoldItem toggleFontBoldItem1;
        private DevExpress.XtraRichEdit.UI.ToggleFontItalicItem toggleFontItalicItem1;
        private DevExpress.XtraRichEdit.UI.ToggleFontUnderlineItem toggleFontUnderlineItem1;
        private DevExpress.XtraRichEdit.UI.ToggleFontStrikeoutItem toggleFontStrikeoutItem1;
        private DevExpress.XtraRichEdit.UI.ToggleParagraphAlignmentRightItem toggleParagraphAlignmentRightItem1;
        private DevExpress.XtraRichEdit.UI.ToggleParagraphAlignmentJustifyItem toggleParagraphAlignmentJustifyItem1;
        private DevExpress.XtraRichEdit.UI.FileNewItem fileNewItem1;
        private DevExpress.XtraRichEdit.UI.ToggleParagraphAlignmentLeftItem toggleParagraphAlignmentLeftItem1;
        private DevExpress.XtraRichEdit.UI.ToggleParagraphAlignmentCenterItem toggleParagraphAlignmentCenterItem1;
        private DevExpress.XtraRichEdit.UI.SetSingleParagraphSpacingItem setSingleParagraphSpacingItem1;
        private DevExpress.XtraRichEdit.UI.SetSesquialteralParagraphSpacingItem setSesquialteralParagraphSpacingItem1;
        private DevExpress.XtraRichEdit.UI.SetDoubleParagraphSpacingItem setDoubleParagraphSpacingItem1;
        private DevExpress.XtraRichEdit.UI.ShowLineSpacingFormItem showLineSpacingFormItem1;
        private DevExpress.XtraRichEdit.UI.AddSpacingBeforeParagraphItem addSpacingBeforeParagraphItem1;
        private DevExpress.XtraRichEdit.UI.RemoveSpacingBeforeParagraphItem removeSpacingBeforeParagraphItem1;
        private DevExpress.XtraRichEdit.UI.AddSpacingAfterParagraphItem addSpacingAfterParagraphItem1;
        private DevExpress.XtraRichEdit.UI.RemoveSpacingAfterParagraphItem removeSpacingAfterParagraphItem1;
        private DevExpress.XtraRichEdit.Design.RepositoryItemRichEditStyleEdit repositoryItemRichEditStyleEdit1;
        private DevExpress.XtraRichEdit.UI.RichEditBarController richEditBarController1;
        private DevExpress.XtraRichEdit.UI.ToggleFontDoubleUnderlineItem toggleFontDoubleUnderlineItem1;
        private DevExpress.XtraRichEdit.UI.ChangeFontNameItem changeFontNameItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemFontEdit repositoryItemFontEdit2;
        private DevExpress.XtraRichEdit.UI.ChangeFontSizeItem changeFontSizeItem1;
        private DevExpress.XtraRichEdit.Design.RepositoryItemRichEditFontSizeEdit repositoryItemRichEditFontSizeEdit2;
        private DevExpress.XtraRichEdit.UI.ChangeFontColorItem changeFontColorItem1;
        private DevExpress.XtraRichEdit.UI.ChangeFontBackColorItem changeFontBackColorItem1;
        private DevExpress.XtraRichEdit.UI.SetSingleParagraphSpacingItem setSingleParagraphSpacingItem2;
        private DevExpress.XtraRichEdit.UI.SetSesquialteralParagraphSpacingItem setSesquialteralParagraphSpacingItem2;
        private DevExpress.XtraRichEdit.UI.SetDoubleParagraphSpacingItem setDoubleParagraphSpacingItem2;
        private DevExpress.XtraRichEdit.UI.ShowLineSpacingFormItem showLineSpacingFormItem2;
        private DevExpress.XtraRichEdit.UI.AddSpacingBeforeParagraphItem addSpacingBeforeParagraphItem2;
        private DevExpress.XtraRichEdit.UI.RemoveSpacingBeforeParagraphItem removeSpacingBeforeParagraphItem2;
        private DevExpress.XtraRichEdit.UI.AddSpacingAfterParagraphItem addSpacingAfterParagraphItem2;
        private DevExpress.XtraRichEdit.UI.RemoveSpacingAfterParagraphItem removeSpacingAfterParagraphItem2;
        private DevExpress.XtraRichEdit.UI.SetSingleParagraphSpacingItem setSingleParagraphSpacingItem3;
        private DevExpress.XtraRichEdit.UI.SetSesquialteralParagraphSpacingItem setSesquialteralParagraphSpacingItem3;
        private DevExpress.XtraRichEdit.UI.SetDoubleParagraphSpacingItem setDoubleParagraphSpacingItem3;
        private DevExpress.XtraRichEdit.UI.ShowLineSpacingFormItem showLineSpacingFormItem3;
        private DevExpress.XtraRichEdit.UI.AddSpacingBeforeParagraphItem addSpacingBeforeParagraphItem3;
        private DevExpress.XtraRichEdit.UI.RemoveSpacingBeforeParagraphItem removeSpacingBeforeParagraphItem3;
        private DevExpress.XtraRichEdit.UI.AddSpacingAfterParagraphItem addSpacingAfterParagraphItem3;
        private DevExpress.XtraRichEdit.UI.RemoveSpacingAfterParagraphItem removeSpacingAfterParagraphItem3;
        private DevExpress.XtraRichEdit.Design.RepositoryItemRichEditStyleEdit repositoryItemRichEditStyleEdit2;
        private DevExpress.XtraEditors.SimpleButton btnContent;
        private DevExpress.XtraEditors.LabelControl lblCC;
        private DevExpress.XtraEditors.LabelControl lblTo;
        private DevExpress.XtraBars.BarButtonItem barItemSaveDraft;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider2;
        private ImageList imageListAttachment;
     
    }
}