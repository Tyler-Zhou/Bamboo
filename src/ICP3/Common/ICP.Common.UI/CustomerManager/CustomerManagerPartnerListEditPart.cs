//-----------------------------------------------------------------------
// <copyright file="CustomerManagerPartnerListEditPart.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace ICP.Common.UI.CustomerManager
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using ICP.Framework.ClientComponents.UIFramework;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI;
    using ICP.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Client;
    using ICP.Framework.CommonLibrary.Common;
    using System.Windows.Forms;

    /// <summary>
    /// 客户管理-合作伙伴维护列表
    /// <remarks>
    /// 在该面板只实现本界面的控制逻辑。
    /// 如果要与服务交互。需通过Controller交互.(最少知识原则)
    /// </remarks>
    /// </summary>
    [System.ComponentModel.ToolboxItem(false)]
    [SmartPart]
    public class CustomerManagerPartnerListEditPart : BaseListEditPart
    {
        #region Service
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        #endregion

        #region 初始化
        
        public CustomerManagerPartnerListEditPart()
        {
            this.InitializeComponent();
            this.Enabled = false;
          
            
            this.Disposed += delegate {
            
                this.Selected = null;
                this.Selecting = null;
                this.Saved = null;
                this.gridView1.CustomDrawRowIndicator -= this.mainGridView_CustomDrawRowIndicator;
                this.gridView1.BeforeLeaveRow -= this.gridView1_BeforeLeaveRow;
                this.gridView1.InitNewRow -= this.gridView1_InitNewRow;
                this.gridView1.RowStyle -= this.gridView1_RowStyle;
                this.lwGrid.DataSource = null;
                this.CurrentChanged = null;
                this.CurrentChanging = null;
                this.bsDataSource.PositionChanged -= this.bsDataSource_PositionChanged;
                this.bsDataSource.DataSource = null;
                this.bsDataSource.Dispose();

                this.partnerCodeFinder.Dispose();
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.InitControls();
        }
        IDisposable partnerCodeFinder;
        private void InitControls()
        {
            barDelect.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Delete_16;
            barSave.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Save_16;

          partnerCodeFinder= DataFindClientService.RegisterGridColumnFinder(colPartnerCode
                                               , ICP.Common.ServiceInterface.CommonFinderConstants.CustoemrFinder
                                               , new string[] { "PartnerID", "PartnerCode", "PartnerName", "PartnerKeyword", "PartnerAddress", "PartnerFax" }
                                               , new string[] { "ID", "Code", LocalData.IsEnglish ? "EName" : "CName", "KeyWord", LocalData.IsEnglish ? "EAddress" : "CAddress", "Fax" });
          
            //gridView1.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(gridView1_InitNewRow);
        }

        #endregion

        #region 控制器

        /// <summary>
        /// 客户管理控制器
        /// </summary>
        public CustomerManagerController Controller
        {
            get
            {
                return ClientHelper.Get<CustomerManagerController, CustomerManagerController>();
            }
        }

        #endregion

        #region IListEditPart接口

        /// <summary>
        /// 返回当前选择行数据
        /// </summary>
        public override object Current
        {
            get
            {
              return  bsDataSource.Current;
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
                BindingData(value);
            }
        }

        void BindingData(object data)
        {
            if (data == null) { this.bsDataSource.DataSource = new List<CustomerPartnerList>(); }
            else
            {
                List<CustomerPartnerList> datas = data as List<CustomerPartnerList>;
                if (datas != null && datas.Count > 0)
                {
                    foreach (var item in datas) { item.BeginEdit(); }
                }

                this.bsDataSource.DataSource = datas;
                this.bsDataSource.ResetBindings(false);
            }
        }

        /// <summary>
        /// 当前面版是否只读
        /// </summary>
        public override bool ReadOnly { get; set; }


        public override event CurrentChangedHandler CurrentChanged;
        public override event CancelEventHandler CurrentChanging;
        public override event SavedHandler Saved;
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

        public override void EndEdit()
        {
            this.Validate();
            bsDataSource.EndEdit();
        }
     
        private CustomerList _currentCustomer = null;
        public override void Init(IDictionary<string, object> values)
        {
            if (values != null)
            {
                _currentCustomer = (CustomerList)values["CustomerList"];
                if (_currentCustomer == null)
                {
                    this.bsDataSource.DataSource = null;
                    this.Enabled = false;
                    return;
                }
                else
                {
                    this.Enabled = true;
                }

                List<CustomerPartnerList> partnerList = new List<CustomerPartnerList>();
                if (_currentCustomer.ID != null && _currentCustomer.ID != Guid.Empty)
                {
                    partnerList = this.Controller.GetCustomerPartnerList(_currentCustomer.ID);
                }

                this.bsDataSource.DataSource = partnerList;
            }

            RefreshToolBars();
        }

        /*刷新工具栏*/
        private void RefreshToolBars()
        {
            if (Current == null)
            {
                barDelect.Enabled = false;
            }
            else
            {
                barDelect.Enabled = true;
            }

            if (bsDataSource.Count > 0)
            {
                barSave.Enabled = true;
            }
            else
            {
                barSave.Enabled = false;
            }
        }

        public override void InsertItem(int index, object item)
        {
            bsDataSource.Insert(index, item);
        }

        public override void RaiseCurrentChanged()
        {
        }

        public override void RaiseCurrentChanging()
        {
        }

        public override void RaiseSaved()
        {
        }

        public override void RaiseSelected()
        {
        }

        public override void RaiseSelecting()
        {
        }

        public override void Refresh(object items)
        {
        }

        public override void RemoveItem(int index)
        {
        }

        public override void RemoveItem(object item)
        {
        }

        public override bool SaveData()
        {
            return Save();
        }

        #endregion

        List<CustomerPartnerList> SelectedItem
        {
            get
            {
                int[] rowIndexs = gridView1.GetSelectedRows();

                if (rowIndexs.Length == 0) return null;

                List<CustomerPartnerList> tagers = new List<CustomerPartnerList>();
                foreach (var item in rowIndexs)
                {
                    CustomerPartnerList ma = gridView1.GetRow(item) as CustomerPartnerList;
                    if (ma != null) tagers.Add(ma);
                }

                return tagers;
            }
        }

        #region 方法

        /*保存*/
        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save();
        }

        private bool Save()
        {
            if (_currentCustomer == null) return false;
            
            EndEdit();
            List<CustomerPartnerList> currentList = bsDataSource.DataSource as List<CustomerPartnerList>;
            if (currentList == null || currentList.Count == 0) return false;

            foreach (var item in currentList)
            {
                if (item.Validate() == false) return false;
            }          

            try
            {
                List<Guid?> ids = new List<Guid?>();
                List<Guid> partnerIDs = new List<Guid>();         
                List<DateTime?> updateDates = new List<DateTime?>(); 

                for (int i = 0; i < currentList.Count; i++)
                {
                    ids.Add(currentList[i].ID);
                    partnerIDs.Add(currentList[i].PartnerID);             
                    updateDates.Add(currentList[i].UpdateDate);
                }           

                ManyResultData result = this.Controller.SaveCustomerPartnerInfo(_currentCustomer.ID
                                                                     , ids.ToArray()
                                                                     , partnerIDs.ToArray()
                                                                     , LocalData.UserInfo.LoginID
                                                                     , updateDates.ToArray());

                for (int i = 0; i < currentList.Count; i++)
                {
                    currentList[i].ID = result.ChildResults[i].ID;
                    currentList[i].UpdateDate = result.ChildResults[i].UpdateDate;
                    currentList[i].BeginEdit();
                }

                //bsDataSource.DataSource = currentList;
                DataSource = currentList;
                if (Saved != null) Saved(currentList);

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
                return false;
            }
        }

        private void barDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<CustomerPartnerList> selectedItem = SelectedItem;
            if (selectedItem == null || selectedItem.Count == 0) return;

            DialogResult dlg = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Are you sure to delete?" : "确认删除当前数据?", LocalData.IsEnglish ? "Confirm" : "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dlg != DialogResult.OK)
            {
                return;
            }

            try
            {
                List<Guid> ids = new List<Guid>();
                List<DateTime?> updateDatas = new List<DateTime?>();
                foreach (var item in selectedItem)
                {
                    if (item.IsNew) continue;
                    ids.Add(item.ID);
                    updateDatas.Add(item.UpdateDate);
                }

                if (ids.Count > 0)
                {
                    this.Controller.RemoveCustomerPartnerInfo(ids.ToArray(), LocalData.UserInfo.LoginID, updateDatas.ToArray());
                }

                List<CustomerPartnerList> currentData = bsDataSource.DataSource as List<CustomerPartnerList>;
                foreach (var item in selectedItem)
                {
                    currentData.Remove(item);
                }

                bsDataSource.DataSource = currentData;
                bsDataSource.ResetBindings(false);
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }          
        }

        private void bsDataSource_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, Current);
            //RefreshEnabled();
            RefreshToolBars();
        }

        private void RefreshEnabled()
        {
            if (Current == null)
                barDelect.Enabled = false;
            else
                barDelect.Enabled = true;
        }

        private void gridView1_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            if (CurrentChanging != null)
            {
                CancelEventArgs ce = new CancelEventArgs();
                CurrentChanging(this, ce);
                e.Allow = !ce.Cancel;
            }
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            CustomerPartnerList newData = gridView1.GetRow(e.RowHandle) as CustomerPartnerList;
            if (newData == null) return;

            if (_currentCustomer != null)
            {
                newData.PartnerID = _currentCustomer.ID;
            }

            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
            newData.IsDirty = false;
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            CustomerPartnerList list = gridView1.GetRow(e.RowHandle) as CustomerPartnerList;
            if (list == null) return;

            if (list.IsNew || list.IsDirty)
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.NewLine);
        }

        private void mainGridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }

        #endregion

        #region 设计器自动生成代码

        private System.Windows.Forms.BindingSource bsDataSource;
        private ICP.Framework.ClientComponents.Controls.LWGridControl lwGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar barDelete;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barDelect;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraGrid.Columns.GridColumn colPartnerCode;
        private DevExpress.XtraGrid.Columns.GridColumn colPartnerName;
        private DevExpress.XtraGrid.Columns.GridColumn colPartnerKeyword;
        private DevExpress.XtraGrid.Columns.GridColumn colPartnerAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colPartnerFax;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateByName;
        private IContainer components;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.bsDataSource = new System.Windows.Forms.BindingSource(this.components);
            this.lwGrid = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colPartnerCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPartnerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPartnerKeyword = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPartnerAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPartnerFax = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateByName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDelete = new DevExpress.XtraBars.Bar();
            this.barDelect = new DevExpress.XtraBars.BarButtonItem();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.bsDataSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lwGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // bsDataSource
            // 
            this.bsDataSource.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.CustomerPartnerList);
            this.bsDataSource.PositionChanged += new System.EventHandler(this.bsDataSource_PositionChanged);
            // 
            // lwGrid
            // 
            this.lwGrid.DataSource = this.bsDataSource;
            this.lwGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lwGrid.Location = new System.Drawing.Point(0, 24);
            this.lwGrid.MainView = this.gridView1;
            this.lwGrid.Name = "lwGrid";
            this.lwGrid.Size = new System.Drawing.Size(551, 310);
            this.lwGrid.TabIndex = 0;
            this.lwGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colPartnerCode,
            this.colPartnerName,
            this.colPartnerKeyword,
            this.colPartnerAddress,
            this.colPartnerFax,
            this.colCreateByName});
            this.gridView1.GridControl = this.lwGrid;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridView1.OptionsSelection.UseIndicatorForSelection = true;       
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gridView1_InitNewRow);
            this.gridView1.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(this.gridView1_BeforeLeaveRow);
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            this.gridView1.IndicatorWidth = 35;
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.mainGridView_CustomDrawRowIndicator);
            // 
            // colPartnerCode
            // 
            this.colPartnerCode.Caption = "代码";
            this.colPartnerCode.FieldName = "PartnerCode";
            this.colPartnerCode.Name = "colPartnerCode";
            this.colPartnerCode.Visible = true;
            this.colPartnerCode.VisibleIndex = 0;
            // 
            // colPartnerName
            // 
            this.colPartnerName.Caption = "名称";
            this.colPartnerName.FieldName = "PartnerName";
            this.colPartnerName.Name = "colPartnerName";
            this.colPartnerName.OptionsColumn.AllowEdit = false;
            this.colPartnerName.Visible = true;
            this.colPartnerName.VisibleIndex = 1;
            // 
            // colPartnerKeyword
            // 
            this.colPartnerKeyword.Caption = "关键字";
            this.colPartnerKeyword.FieldName = "PartnerKeyword";
            this.colPartnerKeyword.Name = "colPartnerKeyword";
            this.colPartnerKeyword.OptionsColumn.AllowEdit = false;
            this.colPartnerKeyword.Visible = true;
            this.colPartnerKeyword.VisibleIndex = 2;
            // 
            // colPartnerAddress
            // 
            this.colPartnerAddress.Caption = "地址";
            this.colPartnerAddress.FieldName = "PartnerAddress";
            this.colPartnerAddress.Name = "colPartnerAddress";
            this.colPartnerAddress.OptionsColumn.AllowEdit = false;
            this.colPartnerAddress.Visible = true;
            this.colPartnerAddress.VisibleIndex = 3;
            // 
            // colPartnerFax
            // 
            this.colPartnerFax.Caption = "传真";
            this.colPartnerFax.FieldName = "PartnerFax";
            this.colPartnerFax.Name = "colPartnerFax";
            this.colPartnerFax.OptionsColumn.AllowEdit = false;
            this.colPartnerFax.Visible = true;
            this.colPartnerFax.VisibleIndex = 4;
            // 
            // colCreateByName
            // 
            this.colCreateByName.Caption = "创建人";
            this.colCreateByName.FieldName = "CreateByName";
            this.colCreateByName.Name = "colCreateByName";
            this.colCreateByName.OptionsColumn.AllowEdit = false;
            this.colCreateByName.Visible = true;
            this.colCreateByName.VisibleIndex = 5;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barDelete});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barDelect,
            this.barSave});
            this.barManager1.MainMenu = this.barDelete;
            this.barManager1.MaxItemId = 3;
            // 
            // barDelete
            // 
            this.barDelete.BarName = "Main menu";
            this.barDelete.DockCol = 0;
            this.barDelete.DockRow = 0;
            this.barDelete.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barDelete.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barDelect),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSave)});
            this.barDelete.OptionsBar.MultiLine = true;
            this.barDelete.OptionsBar.UseWholeRow = true;
            this.barDelete.Text = "Main menu";
            // 
            // barDelect
            // 
            this.barDelect.Caption = "删除(&D)";
            this.barDelect.Id = 1;
            this.barDelect.Name = "barDelect";
            this.barDelect.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barDelect.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelete_ItemClick);
            // 
            // barSave
            // 
            this.barSave.Caption = "保存(&S)";
            this.barSave.Id = 2;
            this.barSave.Name = "barSave";
            this.barSave.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(551, 24);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 334);
            this.barDockControlBottom.Size = new System.Drawing.Size(551, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 310);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(551, 24);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 310);
            // 
            // CustomerManagerPartnerListEditPart
            // 
            this.Controls.Add(this.lwGrid);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "CustomerManagerPartnerListEditPart";
            this.Size = new System.Drawing.Size(551, 334);
            ((System.ComponentModel.ISupportInitialize)(this.bsDataSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lwGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
