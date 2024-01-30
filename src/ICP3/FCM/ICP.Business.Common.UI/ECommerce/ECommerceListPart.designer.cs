using DevExpress.XtraBars;
using System.ComponentModel;
using ICP.Business.Common.UI.Properties;
using System.Windows.Forms;
using System.Drawing;
namespace ICP.Business.Common.UI.ECommerce
{
    public partial class ECommerceListPart
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
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barRefersh = new DevExpress.XtraBars.BarButtonItem();
            this.barAssociation = new DevExpress.XtraBars.BarButtonItem();
            this.barAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.pnlToolPart = new DevExpress.XtraEditors.PanelControl();
            this.pnlGridList = new DevExpress.XtraEditors.PanelControl();
            this.gcList = new DevExpress.XtraGrid.GridControl();
            this.bindingSource = new System.Windows.Forms.BindingSource();
            this.gvList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperationDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPOLName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPODName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWeight = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMeasurement = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSalesName = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlToolPart)).BeginInit();
            this.pnlToolPart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGridList)).BeginInit();
            this.pnlGridList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).BeginInit();
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
            this.barManager1.Form = this.pnlToolPart;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barRefersh,
            this.barAssociation,
            this.barEdit,
            this.barAdd});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 35;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRefersh, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAssociation, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barAdd, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barEdit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barRefersh
            // 
            this.barRefersh.Caption = "Refersh";
            this.barRefersh.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Refresh_161;
            this.barRefersh.Id = 7;
            this.barRefersh.Name = "barRefersh";
            // 
            // barAssociation
            // 
            this.barAssociation.Caption = "Association";
            this.barAssociation.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Add;
            this.barAssociation.Id = 32;
            this.barAssociation.Name = "barAssociation";
            // 
            // barAdd
            // 
            this.barAdd.Caption = "Add";
            this.barAdd.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Add;
            this.barAdd.Id = 34;
            this.barAdd.Name = "barAdd";
            this.barAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAdd_ItemClick);
            // 
            // barEdit
            // 
            this.barEdit.Caption = "Edit";
            this.barEdit.Glyph = global::ICP.Business.Common.UI.Properties.Resources.Transfer_16;
            this.barEdit.Id = 33;
            this.barEdit.Name = "barEdit";
            this.barEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barEdit_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(2, 2);
            this.barDockControlTop.Size = new System.Drawing.Size(895, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(2, 29);
            this.barDockControlBottom.Size = new System.Drawing.Size(895, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(2, 28);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 1);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(897, 28);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 1);
            // 
            // pnlToolPart
            // 
            this.pnlToolPart.Controls.Add(this.barDockControlLeft);
            this.pnlToolPart.Controls.Add(this.barDockControlRight);
            this.pnlToolPart.Controls.Add(this.barDockControlBottom);
            this.pnlToolPart.Controls.Add(this.barDockControlTop);
            this.pnlToolPart.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolPart.Location = new System.Drawing.Point(0, 0);
            this.pnlToolPart.Name = "pnlToolPart";
            this.pnlToolPart.Size = new System.Drawing.Size(899, 31);
            this.pnlToolPart.TabIndex = 0;
            // 
            // pnlGridList
            // 
            this.pnlGridList.Controls.Add(this.gcList);
            this.pnlGridList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridList.Location = new System.Drawing.Point(0, 31);
            this.pnlGridList.Name = "pnlGridList";
            this.pnlGridList.Size = new System.Drawing.Size(899, 389);
            this.pnlGridList.TabIndex = 1;
            // 
            // gcList
            // 
            this.gcList.DataSource = this.bindingSource;
            this.gcList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcList.Location = new System.Drawing.Point(2, 2);
            this.gcList.MainView = this.gvList;
            this.gcList.Name = "gcList";
            this.gcList.Size = new System.Drawing.Size(895, 385);
            this.gcList.TabIndex = 0;
            this.gcList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvList});
            // 
            // bindingSource
            // 
            this.bindingSource.DataSource = typeof(ICP.FCM.Common.ServiceInterface.ECommerceList);
            // 
            // gvList
            // 
            this.gvList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSalesName,
            this.colNo,
            this.colOperationDate,
            this.colPOLName,
            this.colPODName,
            this.colQuantity,
            this.colWeight,
            this.colMeasurement});
            this.gvList.GridControl = this.gcList;
            this.gvList.Name = "gvList";
            this.gvList.OptionsBehavior.Editable = false;
            this.gvList.OptionsCustomization.AllowColumnResizing = false;
            this.gvList.OptionsView.ShowFooter = true;
            this.gvList.OptionsView.ShowGroupPanel = false;
            // 
            // colNo
            // 
            this.colNo.Caption = "Operation No";
            this.colNo.FieldName = "No";
            this.colNo.Name = "colNo";
            this.colNo.Visible = true;
            this.colNo.VisibleIndex = 1;
            this.colNo.Width = 121;
            // 
            // colOperationDate
            // 
            this.colOperationDate.Caption = "Operation Date";
            this.colOperationDate.FieldName = "OperationDate";
            this.colOperationDate.Name = "colOperationDate";
            this.colOperationDate.Visible = true;
            this.colOperationDate.VisibleIndex = 2;
            this.colOperationDate.Width = 121;
            // 
            // colPOLName
            // 
            this.colPOLName.Caption = "POL Name";
            this.colPOLName.FieldName = "POLName";
            this.colPOLName.Name = "colPOLName";
            this.colPOLName.Visible = true;
            this.colPOLName.VisibleIndex = 3;
            this.colPOLName.Width = 121;
            // 
            // colPODName
            // 
            this.colPODName.Caption = "POD Name";
            this.colPODName.FieldName = "PODName";
            this.colPODName.Name = "colPODName";
            this.colPODName.SummaryItem.FieldName = "ProfitAmount";
            this.colPODName.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colPODName.Visible = true;
            this.colPODName.VisibleIndex = 4;
            this.colPODName.Width = 121;
            // 
            // colQuantity
            // 
            this.colQuantity.Caption = "Quantity";
            this.colQuantity.FieldName = "Quantity";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colQuantity.Visible = true;
            this.colQuantity.VisibleIndex = 5;
            this.colQuantity.Width = 121;
            // 
            // colWeight
            // 
            this.colWeight.Caption = "Weight";
            this.colWeight.FieldName = "Weight";
            this.colWeight.Name = "colWeight";
            this.colWeight.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colWeight.Visible = true;
            this.colWeight.VisibleIndex = 6;
            this.colWeight.Width = 121;
            // 
            // colMeasurement
            // 
            this.colMeasurement.Caption = "Measurement";
            this.colMeasurement.FieldName = "Measurement";
            this.colMeasurement.Name = "colMeasurement";
            this.colMeasurement.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colMeasurement.Visible = true;
            this.colMeasurement.VisibleIndex = 7;
            this.colMeasurement.Width = 121;
            // 
            // colSalesName
            // 
            this.colSalesName.Caption = "Sales Name";
            this.colSalesName.FieldName = "SalesName";
            this.colSalesName.Name = "colSalesName";
            this.colSalesName.Visible = true;
            this.colSalesName.VisibleIndex = 0;
            this.colSalesName.Width = 80;
            // 
            // ECommerceListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlGridList);
            this.Controls.Add(this.pnlToolPart);
            this.Name = "ECommerceListPart";
            this.Size = new System.Drawing.Size(899, 420);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlToolPart)).EndInit();
            this.pnlToolPart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlGridList)).EndInit();
            this.pnlGridList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barRefersh;
        private DevExpress.XtraEditors.PanelControl pnlToolPart;
        private DevExpress.XtraEditors.PanelControl pnlGridList;
        protected DevExpress.XtraGrid.GridControl gcList;
        protected DevExpress.XtraGrid.Views.Grid.GridView gvList;
        private System.Windows.Forms.BindingSource bindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colNo;
        private DevExpress.XtraGrid.Columns.GridColumn colOperationDate;
        private DevExpress.XtraGrid.Columns.GridColumn colPOLName;
        private DevExpress.XtraGrid.Columns.GridColumn colPODName;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colWeight;
        private DevExpress.XtraGrid.Columns.GridColumn colMeasurement;
        private BarButtonItem barAssociation;
        private BarButtonItem barEdit;
        private BarButtonItem barAdd;
        private DevExpress.XtraGrid.Columns.GridColumn colSalesName;
    }
}
