
//-----------------------------------------------------------------------
// <copyright file="ReportConfigureListPart.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.Common.UI.Configure.ReportConfigure
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using DevExpress.XtraGrid.Views.Grid.ViewInfo;
    using ICP.Common.ServiceInterface;
    using ICP.Common.ServiceInterface.DataObjects;
    using ICP.Framework.ClientComponents.UIFramework;
    using ICP.Framework.CommonLibrary.Client;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Framework.CommonLibrary.Helper;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.Commands;
    using Microsoft.Practices.CompositeUI.SmartParts;

    /// <summary>
    /// Report配置-主列表
    /// </summary>
    [System.ComponentModel.ToolboxItem(false)]
    [SmartPart]
    public class ReportConfigureListPart:BaseListPart
    {
        #region 服务

        /// <summary>
        /// 公司配置服务
        /// </summary>
        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        #endregion

        #region 初始化

        public ReportConfigureListPart()
        {
            this.InitializeComponent();
            this.Disposed += delegate {
                this.Selected = null;
                this.Selecting = null;
                this.CurrentChanged = null;
                this.CurrentChanging = null;
                this.mainGridList.MouseDown -= this.mainGridList_MouseDown;
                this.mainGridList.MouseLeave -= this.mainGridList_MouseLeave;
                this.mainGridList.MouseMove -= this.mainGridList_MouseMove;
                this.mainGridList.MouseUp -= this.mainGridList_MouseUp;
                this.mainGridView.BeforeLeaveRow -= this.mainGridView_BeforeLeaveRow;
                this.mainGridView.CustomDrawRowIndicator -= this.mainGridView_CustomDrawRowIndicator;
                this.mainGridView.DoubleClick -= this.mainGridView_DoubleClick;
                this.mainGridView.RowStyle -= this.mainGridView_RowStyle;
                this.mainGridList.DataSource = null;
                this.bsDataSource.CurrentChanged -= this.bsDataSource_CurrentChanged;
                this.bsDataSource.DataSource = null;
                this.bsDataSource.Dispose();
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            this.mainGridView.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(mainGridView_BeforeLeaveRow);
            //this.bsDataSource.PositionChanged += new EventHandler(bsDataSource_PositionChanged);
            this.bsDataSource.CurrentChanged += new EventHandler(bsDataSource_CurrentChanged);
            this.mainGridView.DoubleClick += new EventHandler(mainGridView_DoubleClick);
        }
     
        #endregion

        #region 事件处理

        /*行将要改变*/
        void mainGridView_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            if (this.CurrentChanging != null)
            {
                CancelEventArgs ce = new CancelEventArgs();
                this.CurrentChanging(this, ce);

                e.Allow = !ce.Cancel;
            }
        }      

        void bsDataSource_CurrentChanged(object sender, EventArgs e)
        {
            this.RaiseCurrentChanged();
        }

        void mainGridView_DoubleClick(object sender, EventArgs e)
        {
            this.RaiseSelected();
        }     

        #endregion

        #region IListPart接口

        /// <summary>
        /// 返回当前选择行数据
        /// </summary>
        public override object Current
        {
            get
            {
                return bsDataSource.Current;
            }
        }

        /// <summary>
        /// 数据源
        /// </summary>
        public override object DataSource
        {
            get
            {
                return bsDataSource.DataSource;
            }
            set
            {
                bsDataSource.DataSource = value;

                string message = string.Empty;
                List<ReportConfigureList> list = value as List<ReportConfigureList>;
                if (list.Count > 0)
                {
                    if (LocalData.IsEnglish)
                    {
                        message = string.Format("{0} record found", list.Count);
                    }
                    else
                    {
                        message = string.Format("查询到 {0} 条记录", list.Count);
                    }
                }
                else
                {
                    message = LocalData.IsEnglish ? "Nothing found!" : "没有查询到任何结果。";
                }

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), message);
            }
        }


        /// <summary>
        /// 当前面版是否只读
        /// </summary>
        //public override bool ReadOnly { get; set; }


        public override event CurrentChangedHandler CurrentChanged;
        public override event CancelEventHandler CurrentChanging;
        public override event SelectedHandler Selected;
        public override event SelectingHandler Selecting;

        /// <summary>
        /// 添加行
        /// </summary>
        /// <param name="item">数据</param>
        public override void AddItem(object item)
        {
            bsDataSource.Add(item);
        }

        public override void Clear()
        {
            bsDataSource.Clear();
        }


        public override void Init(IDictionary<string, object> values)
        {
        }

        public override void InsertItem(int index, object item)
        {
            bsDataSource.Insert(index, item);
            mainGridView.FocusedRowHandle = index;
        }

        public override void RaiseCurrentChanged()
        {
            if (this.CurrentChanged != null)
            {
                this.CurrentChanged(this, this.Current);
            }
        }

        public override void RaiseCurrentChanging()
        {
            //if(this.CurrentChanging != null)
            //{
            //    this.CurrentChanging(this, this.Current);
            //}
        }


        public override void RaiseSelected()
        {
            if(this.Selected != null)
            {
                this.Selected(this, this.Current);
            }
        }

        public override void RaiseSelecting()
        {
        }

        public override void Refresh(object items)
        {
            BindingSourceHelper.UpdateItem(
                this.bsDataSource, 
                items);
            
            this.mainGridView.RefreshData();
        }

        public override void RemoveItem(int index)
        {
            this.bsDataSource.RemoveAt(index);
        }

        public override void RemoveItem(object item)
        {
            this.bsDataSource.Remove(item);
        }

        #endregion

        #region 设计器生成代码

        private ICP.Framework.ClientComponents.Controls.LWGridControl mainGridList;
        private DevExpress.XtraGrid.Views.Grid.GridView mainGridView;
        private System.Windows.Forms.BindingSource bsDataSource;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;
        private DevExpress.XtraGrid.Columns.GridColumn colCDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colEDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateByName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private System.ComponentModel.IContainer components;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainGridList = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsDataSource = new System.Windows.Forms.BindingSource(this.components);
            this.mainGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateByName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.mainGridList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDataSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // mainGridList
            // 
            this.mainGridList.AllowDrop = true;
            this.mainGridList.DataSource = this.bsDataSource;
            this.mainGridList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainGridList.Location = new System.Drawing.Point(0, 0);
            this.mainGridList.MainView = this.mainGridView;
            this.mainGridList.Name = "mainGridList";
            this.mainGridList.Size = new System.Drawing.Size(549, 273);
            this.mainGridList.TabIndex = 0;
            this.mainGridList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.mainGridView});
            this.mainGridList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mainGridList_MouseUp);
            this.mainGridList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mainGridList_MouseMove);
            this.mainGridList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mainGridList_MouseDown);
            this.mainGridList.MouseLeave += new System.EventHandler(this.mainGridList_MouseLeave);
            // 
            // bsDataSource
            // 
            this.bsDataSource.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.ReportConfigureList);
            // 
            // mainGridView
            // 
            this.mainGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCode,
            this.colCDescription,
            this.colEDescription,
            this.colType,
            this.colCreateByName,
            this.colCreateDate});
            this.mainGridView.GridControl = this.mainGridList;
            this.mainGridView.IndicatorWidth = 35;
            this.mainGridView.Name = "mainGridView";
            this.mainGridView.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.mainGridView.OptionsBehavior.Editable = false;
            this.mainGridView.OptionsSelection.MultiSelect = true;
            this.mainGridView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.mainGridView.OptionsView.ShowDetailButtons = false;
            this.mainGridView.OptionsView.ShowGroupPanel = false;
            this.mainGridView.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.mainGridView_CustomDrawRowIndicator);
            this.mainGridView.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.mainGridView_RowStyle);
            // 
            // colCode
            // 
            this.colCode.Caption = "代码";
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 0;
            // 
            // colCDescription
            // 
            this.colCDescription.Caption = "中文描述";
            this.colCDescription.FieldName = "CDescription";
            this.colCDescription.Name = "colCDescription";
            this.colCDescription.Visible = true;
            this.colCDescription.VisibleIndex = 1;
            // 
            // colEDescription
            // 
            this.colEDescription.Caption = "英文描述";
            this.colEDescription.FieldName = "EDescription";
            this.colEDescription.Name = "colEDescription";
            this.colEDescription.Visible = true;
            this.colEDescription.VisibleIndex = 2;
            // 
            // colType
            // 
            this.colType.Caption = "类型";
            this.colType.FieldName = "ReportTypeName";
            this.colType.Name = "colType";
            this.colType.Visible = true;
            this.colType.VisibleIndex = 3;
            // 
            // colCreateByName
            // 
            this.colCreateByName.Caption = "创建人";
            this.colCreateByName.FieldName = "CreateByName";
            this.colCreateByName.Name = "colCreateByName";
            this.colCreateByName.Visible = true;
            this.colCreateByName.VisibleIndex = 4;
            // 
            // colCreateDate
            // 
            this.colCreateDate.Caption = "创建时间";
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 5;
            // 
            // ReportConfigureListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.Controls.Add(this.mainGridList);
            this.Name = "ReportConfigureListPart";
            this.Size = new System.Drawing.Size(549, 273);
            ((System.ComponentModel.ISupportInitialize)(this.mainGridList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDataSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        #region 处理拖放事件

        GridHitInfo hitInfo = null;
        private void mainGridList_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            hitInfo = mainGridView.CalcHitInfo(e.X, e.Y);
        }

        private void mainGridList_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (hitInfo == null) return;
            if (e.Button != MouseButtons.Left) return;

            Rectangle dragRect = new Rectangle(new Point(
                hitInfo.HitPoint.X - SystemInformation.DragSize.Width / 2,
                hitInfo.HitPoint.Y - SystemInformation.DragSize.Height / 2), SystemInformation.DragSize);
            if (!dragRect.Contains(new Point(e.X, e.Y)))
            {
                if (hitInfo.InRowCell
                    || hitInfo.HitTest == GridHitTest.RowIndicator)
                {
                    object rowdata = mainGridView.GetRow(hitInfo.RowHandle);

                    mainGridList.DoDragDrop(rowdata, DragDropEffects.Copy);
                }
            }
        }

        private void mainGridList_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }

        private void mainGridList_MouseUp(object sender, MouseEventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }

        private void mainGridView_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            ReportConfigureList list = mainGridView.GetRow(e.RowHandle) as ReportConfigureList;
            if (list == null) return;

            if (list.IsValid == false)
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Disabled);
        }

        private void mainGridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }

        #endregion

        [CommandHandler(ReportConfigureConstants.CMD_Disable)]
        public void Common_DisuseData(object sender, EventArgs e)
        {
            ReportConfigureList currentRow = Current as ReportConfigureList;
            if (currentRow == null) return;

            try
            {
                SingleResultData result = ConfigureService.ChangeReportConfigureState(currentRow.ID, !currentRow.IsValid, LocalData.UserInfo.LoginID, currentRow.UpdateDate);
                currentRow.IsValid = !currentRow.IsValid;
                currentRow.UpdateDate = result.UpdateDate;
                bsDataSource.ResetCurrentItem();
                bsDataSource_CurrentChanged(this, null);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Disuse Successfully" : "作废/激活成功");
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }
    }
}
