        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
namespace ICP.FCM.OtherBusiness.UI.Common.Controls
{
    partial class TreeSelectBox
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
            this.popControl1 = new DevExpress.XtraEditors.PopupContainerControl();
            this.treeMain = new DevExpress.XtraTreeList.TreeList();
            this.colName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.rPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.popEdit1 = new DevExpress.XtraEditors.PopupContainerEdit();
            ((System.ComponentModel.ISupportInitialize)(this.popControl1)).BeginInit();
            this.popControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rPictureEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // popControl1
            // 
            this.popControl1.Controls.Add(this.treeMain);
            this.popControl1.Location = new System.Drawing.Point(3, 27);
            this.popControl1.Name = "popControl1";
            this.popControl1.Size = new System.Drawing.Size(237, 195);
            this.popControl1.TabIndex = 6;
            // 
            // treeMain
            // 
            this.treeMain.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colName});
            this.treeMain.DataSource = this.bindingSource1;
            this.treeMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeMain.Location = new System.Drawing.Point(0, 0);
            this.treeMain.Name = "treeMain";
            this.treeMain.OptionsBehavior.AllowExpandOnDblClick = false;
            this.treeMain.OptionsBehavior.Editable = false;
            this.treeMain.OptionsView.ShowColumns = false;
            this.treeMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rPictureEdit1});
            this.treeMain.RootValue = null;
            this.treeMain.Size = new System.Drawing.Size(237, 195);
            this.treeMain.TabIndex = 6;
            this.treeMain.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.treeMain_NodeCellStyle);
            this.treeMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeMain_MouseDoubleClick);
            // 
            // colName
            // 
            this.colName.Caption = "Name";
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            this.colName.Width = 92;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.FCM.OtherBusiness.UI.Common.Controls.TreeSelectSource);
            // 
            // rPictureEdit1
            // 
            this.rPictureEdit1.Name = "rPictureEdit1";
            // 
            // popEdit1
            // 
            this.popEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.popEdit1.Location = new System.Drawing.Point(0, 0);
            this.popEdit1.Name = "popEdit1";
            this.popEdit1.Properties.Appearance.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.popEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.popEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.popEdit1.Properties.PopupControl = this.popControl1;
            this.popEdit1.Size = new System.Drawing.Size(243, 21);
            this.popEdit1.TabIndex = 5;
            this.popEdit1.TabStop = false;
            this.popEdit1.QueryPopUp += new System.ComponentModel.CancelEventHandler(this.popEdit1_QueryPopUp);
            // 
            // TreeSelectBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.popControl1);
            this.Controls.Add(this.popEdit1);
            this.Name = "TreeSelectBox";
            this.Size = new System.Drawing.Size(243, 21);
            ((System.ComponentModel.ISupportInitialize)(this.popControl1)).EndInit();
            this.popControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rPictureEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PopupContainerControl popControl1;
        private DevExpress.XtraEditors.PopupContainerEdit popEdit1;
        private DevExpress.XtraTreeList.TreeList treeMain;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit rPictureEdit1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colName;
    }
}
