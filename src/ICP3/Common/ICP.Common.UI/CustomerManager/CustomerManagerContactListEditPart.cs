
//-----------------------------------------------------------------------
// <copyright file="CustomerManagerContactListEditPart.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.Common.UI.CustomerManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ComponentModel;
    using ICP.Framework.ClientComponents.UIFramework;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.ObjectBuilder;
    using ICP.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Client;
    using System.Windows.Forms;
    using ICP.Framework.CommonLibrary.Common;

    /// <summary>
    /// 客户管理-客户联系人维护列表
    /// </summary>
    [System.ComponentModel.ToolboxItem(false)]
    [SmartPart]
    public class CustomerManagerContactListEditPart:BaseListEditPart
    {
        #region 初始化
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public CustomerManagerContactListEditPart()
        {
            this.InitializeComponent();
            this.Enabled = false;
            this.Disposed += delegate {
                this.lwGrid.DataSource = null;
                this.gridView1.CustomDrawRowIndicator -= this.mainGridView_CustomDrawRowIndicator;
                this.bsDataSource.DataSource = null;
                this.bsDataSource.Dispose();
                this.Saved = null;
                this.Selected = null;
                this.Selecting = null;
                this.CurrentChanged = null;
                this.CurrentChanging = null;
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
        [CreateNew]
        public CustomerManagerController Controller { get; set; }

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

                List<CustomerContactList> contactList = new List<CustomerContactList>();
                if (_currentCustomer.ID != null && _currentCustomer.ID != Guid.Empty)
                {
                    contactList = this.Controller.GetCustomerContactList(_currentCustomer.ID);
                    if (contactList != null && contactList.Count > 0)
                    {
                        contactList = contactList.Where(s => s.IsValid).ToList();
                    }
                }

                this.bsDataSource.DataSource = contactList;
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
            string[] cNames = new string[bsDataSource.Count];
            string[] eNames = new string[bsDataSource.Count];
            string[] departments = new string[bsDataSource.Count];
            string[] positions = new string[bsDataSource.Count];
            string[] tels = new string[bsDataSource.Count];
            string[] faxs = new string[bsDataSource.Count];
            string[] mobiles = new string[bsDataSource.Count];
            string[] eMails = new string[bsDataSource.Count];
            string[] remarks = new string[bsDataSource.Count];
            CustomerContactType[] types = new CustomerContactType[bsDataSource.Count];
            DateTime?[] updateDates = new DateTime?[bsDataSource.Count];

            int i = 0;
            foreach (CustomerContactList cid in bsDataSource.List)
            {
                ids[i] = cid.ID;
                cNames[i] = cid.CName;
                eNames[i] = cid.EName;
                departments[i] = cid.Department;
                positions[i] = cid.Position;
                tels[i] = cid.Tel;
                faxs[i] = cid.Fax;
                mobiles[i] = cid.Mobile;
                eMails[i] = cid.EMail;
                remarks[i] = cid.Remark;
                types[i] = cid.Type;
                updateDates[i] = cid.UpdateDate;
                i++;
            }

            ManyResultData mans = this.Controller.SaveCustomerContactInfo(
            _currentCustomer.ID
            , ids
            , cNames
            , eNames
            , departments
            , positions
            , tels
            , faxs
            , mobiles
            , eMails
            , remarks
            , types
            , LocalData.UserInfo.LoginID
            , updateDates);

            if (mans == null || mans.ChildResults == null || mans.ChildResults.Count == 0)
            {
                return false;
            }
            else
            {
                i = 0;
                foreach (CustomerContactList cid in bsDataSource.List)
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

        /*添加联系人*/
        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EndEdit();
            CustomerContactList newContact = new CustomerContactList();
            newContact.CreateByID = LocalData.UserInfo.LoginID;
            newContact.ID = Guid.Empty;
            newContact.CreateByName = LocalData.UserInfo.LoginName;
            newContact.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
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
            foreach (CustomerContactList c in bsDataSource.List)
            {
                if (c.Validate() == false)
                {
                    return false;
                }
            }

            return true;
        }

        /*删除当前选择的联系人*/
        private void DeleteRow()
        {
            if (Current != null)
            {
                var contact = Current as CustomerContactList;
                if (contact != null)
                {
                    if (contact.ID != null && contact.ID != Guid.Empty)
                    {
                        this.Controller.ChangeCustomerContactState(contact.ID, false, LocalData.UserInfo.LoginID, contact.UpdateDate);
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
        private DevExpress.XtraGrid.Columns.GridColumn colCName;
        private DevExpress.XtraGrid.Columns.GridColumn colEName;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartment;
        private DevExpress.XtraGrid.Columns.GridColumn colPosition;
        private DevExpress.XtraGrid.Columns.GridColumn colTel;
        private DevExpress.XtraGrid.Columns.GridColumn colFax;
        private DevExpress.XtraGrid.Columns.GridColumn colMobile;
        private DevExpress.XtraGrid.Columns.GridColumn colEMail;
        private DevExpress.XtraGrid.Columns.GridColumn colRemark;
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
            this.lwGrid = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsDataSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartment = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPosition = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTel = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFax = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMobile = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEMail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRemark = new DevExpress.XtraGrid.Columns.GridColumn();
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
            ((System.ComponentModel.ISupportInitialize)(this.barManagerContactList)).BeginInit();
            this.SuspendLayout();
            // 
            // lwGrid
            // 
            this.lwGrid.DataSource = this.bsDataSource;
            this.lwGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lwGrid.Location = new System.Drawing.Point(0, 24);
            this.lwGrid.MainView = this.gridView1;
            this.lwGrid.Name = "lwGrid";
            this.lwGrid.Size = new System.Drawing.Size(387, 286);
            this.lwGrid.TabIndex = 1;
            this.lwGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // bsDataSource
            // 
            this.bsDataSource.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.CustomerContactList);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCName,
            this.colEName,
            this.colDepartment,
            this.colPosition,
            this.colTel,
            this.colFax,
            this.colMobile,
            this.colEMail,
            this.colRemark});
            this.gridView1.GridControl = this.lwGrid;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridView1.OptionsSelection.UseIndicatorForSelection = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.IndicatorWidth = 35;
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.mainGridView_CustomDrawRowIndicator);
            // 
            // colCName
            // 
            this.colCName.Caption = "中文名";
            this.colCName.FieldName = "CName";
            this.colCName.Name = "colCName";
            this.colCName.Visible = true;
            this.colCName.VisibleIndex = 0;
            // 
            // colEName
            // 
            this.colEName.Caption = "英文名";
            this.colEName.FieldName = "EName";
            this.colEName.Name = "colEName";
            this.colEName.Visible = true;
            this.colEName.VisibleIndex = 1;
            // 
            // colDepartment
            // 
            this.colDepartment.Caption = "部门";
            this.colDepartment.FieldName = "Department";
            this.colDepartment.Name = "colDepartment";
            this.colDepartment.Visible = true;
            this.colDepartment.VisibleIndex = 2;
            // 
            // colPosition
            // 
            this.colPosition.Caption = "职位";
            this.colPosition.FieldName = "Position";
            this.colPosition.Name = "colPosition";
            this.colPosition.Visible = true;
            this.colPosition.VisibleIndex = 3;
            // 
            // colTel
            // 
            this.colTel.Caption = "电话";
            this.colTel.FieldName = "Tel";
            this.colTel.Name = "colTel";
            this.colTel.Visible = true;
            this.colTel.VisibleIndex = 4;
            // 
            // colFax
            // 
            this.colFax.Caption = "传真";
            this.colFax.FieldName = "Fax";
            this.colFax.Name = "colFax";
            this.colFax.Visible = true;
            this.colFax.VisibleIndex = 5;
            // 
            // colMobile
            // 
            this.colMobile.Caption = "移动电话";
            this.colMobile.FieldName = "Mobile";
            this.colMobile.Name = "colMobile";
            this.colMobile.Visible = true;
            this.colMobile.VisibleIndex = 6;
            // 
            // colEMail
            // 
            this.colEMail.Caption = "邮箱";
            this.colEMail.FieldName = "EMail";
            this.colEMail.Name = "colEMail";
            this.colEMail.Visible = true;
            this.colEMail.VisibleIndex = 7;
            // 
            // colRemark
            // 
            this.colRemark.Caption = "备注";
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            this.colRemark.Visible = true;
            this.colRemark.VisibleIndex = 8;
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
            this.barDockControlTop.Size = new System.Drawing.Size(387, 24);
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
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 286);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(387, 24);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 286);
            // 
            // CustomerManagerContactListEditPart
            // 
            this.Controls.Add(this.lwGrid);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "CustomerManagerContactListEditPart";
            this.Size = new System.Drawing.Size(387, 310);
            ((System.ComponentModel.ISupportInitialize)(this.lwGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDataSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManagerContactList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
