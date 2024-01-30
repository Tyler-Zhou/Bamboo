
//-----------------------------------------------------------------------
// <copyright file="CustomerManagerContactListEditPart.cs" company="LongWin">
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
    using System.Windows.Forms;
    using ICP.Framework.CommonLibrary.Common;

    /// <summary>
    /// 客户管理-客户备注维护列表
    /// </summary>
    [System.ComponentModel.ToolboxItem(false)]
    [SmartPart]
    public class CustomerManagerMemoListEditPart: BaseListEditPart
    {
        #region 初始化
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public CustomerManagerMemoListEditPart()
        {
            this.InitializeComponent();
            this.Enabled = false;
            this.Disposed += delegate {
                this.CurrentChanged = null;
                this.CurrentChanging = null;
                this.Saved = null;
                this.Selected = null;
                this.Selecting = null;
                this.gridView1.CustomDrawRowIndicator -= this.mainGridView_CustomDrawRowIndicator;
                this.lwGrid.DataSource = null;
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
            base.OnLoad(e);
            this.InitControls();
        }

        private void InitControls()
        {
            barAdd.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Plus_16;
            barDelete.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Delete_16;
            barSave.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Save_16;
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
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox2;
        private DevExpress.XtraGrid.Columns.GridColumn colSubject;
        private DevExpress.XtraGrid.Columns.GridColumn colContent;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colPriority;
        private DevExpress.XtraGrid.Columns.GridColumn colIsShowUser;
        private DevExpress.XtraGrid.Columns.GridColumn colIsShowCustomer;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateByName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private ImageList imageList1;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox3;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox4;
        private ImageList imageList2;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
     
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

                List<CustomerMemoList> memoList = new List<CustomerMemoList>();
                if (_currentCustomer.ID != null && _currentCustomer.ID != Guid.Empty)
                {
                    memoList = this.Controller.GetCustomerMemoList(_currentCustomer.ID);
                }

                if (memoList != null && memoList.Count > 0)
                {
                    foreach (var item in memoList)
                    {
                        if (item.Priority == MemoPriority.Low)
                        {
                            item.BizPriority = 0;
                        }
                        else if (item.Priority == MemoPriority.Normal)
                        {
                            item.BizPriority = 1;
                        }
                        else if (item.Priority == MemoPriority.High)
                        {
                            item.BizPriority = 2;
                        }

                        if (item.Type == MemoType.Memo)
                        {
                            item.BizMemoType = 0;
                        }
                        else if (item.Type == MemoType.System)
                        {
                            item.BizMemoType = 1;
                        }
                    }
                }

                this.bsDataSource.DataSource = memoList;
            }

            RefreshToolBars();
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
            this.SaveData();

            if (this.Saved != null)
            {
                this.Saved(this.DataSource);
            }
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
            Guid?[] ids = new Guid?[bsDataSource.Count];
            string[] subjects = new string[bsDataSource.Count];
            string[] contents = new string[bsDataSource.Count];
            MemoPriority[] prioritys = new MemoPriority[bsDataSource.Count];
            MemoType[] types = new MemoType[bsDataSource.Count];
            bool[] isShowCustomers = new bool[bsDataSource.Count];
            bool[] isShowUsers = new bool[bsDataSource.Count];
            DateTime?[] updateDates = new DateTime?[bsDataSource.Count];

            int i = 0;
            foreach (CustomerMemoList cid in bsDataSource.List)
            {
                ids[i] = cid.ID;
                subjects[i] = cid.Subject;
                contents[i] = cid.Content;
                isShowCustomers[i] = cid.IsShowCustomer;
                isShowUsers[i] = cid.IsShowUser;
                updateDates[i] = cid.UpdateDate;
                if (cid.BizPriority == 0)
                {
                    prioritys[i] = MemoPriority.Low;
                }
                else if (cid.BizPriority == 1)
                {
                    prioritys[i] = MemoPriority.Normal;
                }
                else if (cid.BizPriority == 2)
                {
                    prioritys[i] = MemoPriority.High;
                }

                if(cid.BizMemoType == 0)
                {
                    types[i] = MemoType.Memo;
                }
                else if (cid.BizMemoType == 1)
                {
                    types[i] = MemoType.System;
                }

                i++;
            }

            ManyResultData mans = this.Controller.SaveCustomerMemoInfo(
            _currentCustomer.ID
            , ids
            , subjects
            , contents       
            , types
            , prioritys
            , isShowCustomers
            , isShowUsers
            , LocalData.UserInfo.LoginID
            , updateDates);


            if (mans == null || mans.ChildResults == null || mans.ChildResults.Count == 0)
            {
                return false;
            }
            else
            {
                i = 0;
                foreach (CustomerMemoList cid in bsDataSource.List)
                {
                    cid.ID = mans.ChildResults[i].ID;
                    cid.UpdateDate = mans.ChildResults[i].UpdateDate;
                    i++;
                }

                return true;
            }         
        }

        #endregion

        #region 本地控制逻辑

        /*添加备注*/
        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EndEdit();
            CustomerMemoList newContact = new CustomerMemoList();
            newContact.CreateByID = LocalData.UserInfo.LoginID;
            newContact.ID = Guid.Empty;
            newContact.CreateByName = LocalData.UserInfo.LoginName;
            newContact.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
            newContact.BizMemoType = 0;
            newContact.IsDirty = false;
            bsDataSource.Insert(0, newContact);
            bsDataSource.MoveFirst();
            RefreshToolBars();
        }

        /*删除联系人*/
        private void barDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult dlg = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Are you sure to delete?" : "确认删除当前数据?", LocalData.IsEnglish ? "Confirm" : "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);         
            if (dlg != DialogResult.OK)
            {
                return;
            }

            try
            {
                DeleteRow();
                RefreshToolBars();
            }         
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }
        }

        /*保存联系人*/
        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EndEdit();
            if (!this.ValidateData())
            {
                return;
            }           
            
            if (bsDataSource.Count < 1) return;
            if (Current == null) return;
           
            try
            {
                //保存数据
                this.SaveData();
                RefreshToolBars();

                //触发保存成功事件
                if (this.Saved != null)
                {
                    this.Saved(this.bsDataSource.DataSource);
                }

                //提示保存成功
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(
                    this.FindForm(),
                    "保存成功!");
            }
            catch (Exception ex)
            {
                //设置错误信息
                LocalCommonServices.ErrorTrace.SetErrorInfo(
                    this.FindForm(),
                    ex);
            }
        }

        /*验证表单数据*/
        private bool ValidateData()
        {
            foreach (CustomerMemoList c in bsDataSource.List)
            {
                if (c.Validate() == false)
                {
                    return false;
                }
            }

            return true;
        }

        /*删除当前选择的备注*/
        private void DeleteRow()
        {
            if (Current != null)
            {
                var memo = Current as CustomerMemoList;
                if (memo != null)
                {
                    if (memo.ID != null && memo.ID != Guid.Empty)
                    {
                        if (memo.Type == MemoType.System)
                        {
                            return;
                        }

                        this.Controller.RemoveCustomerMemoInfo(memo.ID, LocalData.UserInfo.LoginID, memo.UpdateDate);
                    }

                    bsDataSource.RemoveCurrent();
                    bsDataSource.MoveFirst();
                }
            }
        }      

        /*刷新工具栏*/
        private void RefreshToolBars()
        {
            if (Current == null)
            {
                barDelete.Enabled = false;
            }
            else
            {
                barDelete.Enabled = true;
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

        private void mainGridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }

        #endregion

        #region 设计器生成代码

        private ICP.Framework.ClientComponents.Controls.LWGridControl lwGrid;
        private System.Windows.Forms.BindingSource bsDataSource;
        private IContainer components;
        private DevExpress.XtraBars.BarManager barManagerContactList;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barAdd;
        private DevExpress.XtraBars.BarButtonItem barDelete;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerManagerMemoListEditPart));
            this.lwGrid = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsDataSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSubject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.colPriority = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox3 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.colIsShowUser = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colIsShowCustomer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colCreateByName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemImageComboBox4 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.barManagerContactList = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.lwGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDataSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManagerContactList)).BeginInit();
            this.SuspendLayout();
            // 
            // lwGrid
            // 
            this.lwGrid.DataSource = this.bsDataSource;
            this.lwGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lwGrid.Location = new System.Drawing.Point(0, 26);
            this.lwGrid.MainView = this.gridView1;
            this.lwGrid.Name = "lwGrid";
            this.lwGrid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox1,
            this.repositoryItemImageComboBox2,
            this.repositoryItemImageComboBox3,
            this.repositoryItemImageComboBox4,
            this.repositoryItemMemoEdit1,
            this.repositoryItemMemoExEdit1,
            this.repositoryItemPictureEdit1,
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit2});
            this.lwGrid.Size = new System.Drawing.Size(387, 284);
            this.lwGrid.TabIndex = 1;
            this.lwGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // bsDataSource
            // 
            this.bsDataSource.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.CustomerMemoList);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.GroupRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(104)))), ((int)(((byte)(185)))));
            this.gridView1.Appearance.GroupRow.Options.UseForeColor = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSubject,
            this.colContent,
            this.colType,
            this.colPriority,
            this.colIsShowUser,
            this.colIsShowCustomer,
            this.colCreateByName,
            this.colCreateDate});
            this.gridView1.GridControl = this.lwGrid;
            this.gridView1.GroupFormat = "[#image]{1} {2}";
            this.gridView1.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Count, "Subject", null, "({0} items)")});
            this.gridView1.IndicatorWidth = 35;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.AllowHtmlDrawHeaders = true;
            this.gridView1.OptionsView.GroupDrawMode = DevExpress.XtraGrid.Views.Grid.GroupDrawMode.Office2003;
            this.gridView1.OptionsView.ShowGroupedColumns = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowVertLines = false;
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.mainGridView_CustomDrawRowIndicator);
            // 
            // colSubject
            // 
            this.colSubject.Caption = "主题";
            this.colSubject.FieldName = "Subject";
            this.colSubject.Name = "colSubject";
            this.colSubject.Visible = true;
            this.colSubject.VisibleIndex = 2;
            // 
            // colContent
            // 
            this.colContent.Caption = "内容";
            this.colContent.ColumnEdit = this.repositoryItemMemoExEdit1;
            this.colContent.FieldName = "Content";
            this.colContent.MinWidth = 150;
            this.colContent.Name = "colContent";
            this.colContent.Visible = true;
            this.colContent.VisibleIndex = 3;
            this.colContent.Width = 150;
            // 
            // repositoryItemMemoExEdit1
            // 
            this.repositoryItemMemoExEdit1.AutoHeight = false;
            this.repositoryItemMemoExEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit1.Name = "repositoryItemMemoExEdit1";
            // 
            // colType
            // 
            this.colType.Caption = "类型";
            this.colType.ColumnEdit = this.repositoryItemImageComboBox2;
            this.colType.FieldName = "BizMemoType";
            this.colType.Name = "colType";
            this.colType.OptionsColumn.AllowEdit = false;
            this.colType.Visible = true;
            this.colType.VisibleIndex = 1;
            // 
            // repositoryItemImageComboBox2
            // 
            this.repositoryItemImageComboBox2.AutoHeight = false;
            this.repositoryItemImageComboBox2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox2.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem(global::ICP.Common.UI.Resources.Resource_EN.RERH, 0, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem(global::ICP.Common.UI.Resources.Resource_EN.RERH, 1, 1)});
            this.repositoryItemImageComboBox2.Name = "repositoryItemImageComboBox2";
            this.repositoryItemImageComboBox2.SmallImages = this.imageList2;
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "new.bmp");
            this.imageList2.Images.SetKeyName(1, "systemMemo.png");
            // 
            // colPriority
            // 
            this.colPriority.Caption = "优先级";
            this.colPriority.ColumnEdit = this.repositoryItemImageComboBox3;
            this.colPriority.FieldName = "BizPriority";
            this.colPriority.Name = "colPriority";
            this.colPriority.Visible = true;
            this.colPriority.VisibleIndex = 0;
            // 
            // repositoryItemImageComboBox3
            // 
            this.repositoryItemImageComboBox3.AutoHeight = false;
            this.repositoryItemImageComboBox3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox3.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Low", 0, 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Normal", 1, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("High", 2, 0)});
            this.repositoryItemImageComboBox3.Name = "repositoryItemImageComboBox3";
            this.repositoryItemImageComboBox3.SmallImages = this.imageList1;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "priority_high.bmp");
            this.imageList1.Images.SetKeyName(1, "Priority_Low.png");
            // 
            // colIsShowUser
            // 
            this.colIsShowUser.Caption = "显示给用户";
            this.colIsShowUser.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colIsShowUser.FieldName = "IsShowUser";
            this.colIsShowUser.MinWidth = 90;
            this.colIsShowUser.Name = "colIsShowUser";
            this.colIsShowUser.Visible = true;
            this.colIsShowUser.VisibleIndex = 4;
            this.colIsShowUser.Width = 90;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.FullFocusRect = true;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // colIsShowCustomer
            // 
            this.colIsShowCustomer.Caption = "显示给客户";
            this.colIsShowCustomer.ColumnEdit = this.repositoryItemCheckEdit2;
            this.colIsShowCustomer.FieldName = "IsShowCustomer";
            this.colIsShowCustomer.MinWidth = 90;
            this.colIsShowCustomer.Name = "colIsShowCustomer";
            this.colIsShowCustomer.Visible = true;
            this.colIsShowCustomer.VisibleIndex = 5;
            this.colIsShowCustomer.Width = 90;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // colCreateByName
            // 
            this.colCreateByName.Caption = "创建人";
            this.colCreateByName.FieldName = "CreateByName";
            this.colCreateByName.Name = "colCreateByName";
            this.colCreateByName.OptionsColumn.AllowEdit = false;
            this.colCreateByName.Visible = true;
            this.colCreateByName.VisibleIndex = 6;
            // 
            // colCreateDate
            // 
            this.colCreateDate.Caption = "创建时间";
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.OptionsColumn.AllowEdit = false;
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 7;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Low", 0, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Medium", 1, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("High", 2, 1)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            // 
            // repositoryItemImageComboBox4
            // 
            this.repositoryItemImageComboBox4.AutoHeight = false;
            this.repositoryItemImageComboBox4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox4.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("新增", 0, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("系统", 1, 1)});
            this.repositoryItemImageComboBox4.Name = "repositoryItemImageComboBox4";
            this.repositoryItemImageComboBox4.SmallImages = this.imageList2;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // repositoryItemPictureEdit1
            // 
            this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
            // 
            // barManagerContactList
            // 
            this.barManagerContactList.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManagerContactList.DockControls.Add(this.barDockControlTop);
            this.barManagerContactList.DockControls.Add(this.barDockControlBottom);
            this.barManagerContactList.DockControls.Add(this.barDockControlLeft);
            this.barManagerContactList.DockControls.Add(this.barDockControlRight);
            this.barManagerContactList.Form = this;
            this.barManagerContactList.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barAdd,
            this.barDelete,
            this.barSave});
            this.barManagerContactList.MaxItemId = 6;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barAdd),
            new DevExpress.XtraBars.LinkPersistInfo(this.barDelete, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSave)});
            this.bar1.OptionsBar.AllowCollapse = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // barAdd
            // 
            this.barAdd.Caption = "添加(&A)";
            this.barAdd.Id = 2;
            this.barAdd.Name = "barAdd";
            this.barAdd.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAdd_ItemClick);
            // 
            // barDelete
            // 
            this.barDelete.Caption = "删除(&D)";
            this.barDelete.Id = 3;
            this.barDelete.Name = "barDelete";
            this.barDelete.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelete_ItemClick);
            // 
            // barSave
            // 
            this.barSave.Caption = "保存(&S)";
            this.barSave.Id = 5;
            this.barSave.Name = "barSave";
            this.barSave.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(387, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 310);
            this.barDockControlBottom.Size = new System.Drawing.Size(387, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 284);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(387, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 284);
            // 
            // CustomerManagerMemoListEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.Controls.Add(this.lwGrid);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "CustomerManagerMemoListEditPart";
            this.Size = new System.Drawing.Size(387, 310);
            ((System.ComponentModel.ISupportInitialize)(this.lwGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDataSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManagerContactList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
