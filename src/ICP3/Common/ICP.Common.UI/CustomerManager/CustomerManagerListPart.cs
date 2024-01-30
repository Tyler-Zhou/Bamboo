
//-----------------------------------------------------------------------
// <copyright file="CustomerManagerListPart.cs" company="LongWin">
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
    using ICP.Framework.CommonLibrary.Helper;
    using DevExpress.XtraGrid.Views.Grid.ViewInfo;
    using System.Drawing;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.Commands;
    using ICP.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Common.ServiceInterface;
    using ICP.Framework.CommonLibrary.Client;
    using ICP.Framework.ClientComponents.Controls;

    /// <summary>
    /// 客户管理-主列表
    /// </summary>
    [System.ComponentModel.ToolboxItem(false)]
    [SmartPart]
    public class CustomerManagerListPart : BaseListPart
    {
        #region 服务

        /// <summary>
        /// 服务
        /// </summary>
        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }


        [ServiceDependency]
        public WorkItem WorkItem { get; set; }

        #endregion

        #region 初始化

        public CustomerManagerListPart()
        {
            this.InitializeComponent();
            
            this.Disposed += delegate {
                this.mainGridList.DataSource = null;
                this.mainGridList.MouseDown -= this.mainGridList_MouseDown;
                this.mainGridList.MouseLeave -= this.mainGridList_MouseLeave;
                this.mainGridList.MouseMove -= this.mainGridList_MouseMove;
                this.mainGridList.MouseUp -= this.mainGridList_MouseUp;
                this.mainGridView.BeforeLeaveRow -= this.mainGridView_BeforeLeaveRow;
                this.mainGridView.CustomDrawRowIndicator -= this.mainGridView_CustomDrawRowIndicator;
                this.mainGridView.DoubleClick -= this.mainGridView_DoubleClick;
                this.mainGridView.RowStyle -= this.mainGridView_RowStyle;
                this.bsDataSource.CurrentChanged -= this.bsDataSource_CurrentChanged;
                this.bsDataSource.DataSource = null;
                this.Selected = null;
                this.Selecting = null;
                this.CurrentChanged = null;
                this.CurrentChanging = null;
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

            this.mainGridView.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(mainGridView_BeforeLeaveRow);
            this.bsDataSource.CurrentChanged += new EventHandler(bsDataSource_CurrentChanged);
            ////this.bsDataSource.CurrentChanged += new EventHandler(bsDataSource_PositionChanged);
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
            this.WorkItem.Commands[CustomerManagerConstants.CMD_EditCustomer].Execute();            
        }

        private void mainGridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
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

        protected CustomerList CurrentRow
        {
            get { return Current as CustomerList; }
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
                this.mainGridList.BeginUpdate();
                this.mainGridView.BeginUpdate();


                bsDataSource.DataSource = value;

                string message = string.Empty;
                List<CustomerList> list = value as List<CustomerList>;
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

                //this.mainGridView.EndDataUpdate();
                this.mainGridView.EndUpdate();
                this.mainGridList.EndUpdate();

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
            bsDataSource.Position = index;
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
        }


        public override void RaiseSelected()
        {
            if (this.Selected != null)
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
            //bsDataSource.ResetCurrentItem();
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
        private DevExpress.XtraGrid.Columns.GridColumn colKeyWord;
        private DevExpress.XtraGrid.Columns.GridColumn colCShortName;
        private DevExpress.XtraGrid.Columns.GridColumn colEShortName;
        private DevExpress.XtraGrid.Columns.GridColumn colCName;
        private DevExpress.XtraGrid.Columns.GridColumn colEName;
        private DevExpress.XtraGrid.Columns.GridColumn colTel1;
        private DevExpress.XtraGrid.Columns.GridColumn colFax;
        private DevExpress.XtraGrid.Columns.GridColumn colCountryProvinceName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateByName;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateByName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateDate;
        private DevExpress.XtraGrid.Columns.GridColumn colTypeDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckedStateDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colIsDangerous;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private System.ComponentModel.IContainer components;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainGridList = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsDataSource = new System.Windows.Forms.BindingSource(this.components);
            this.mainGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKeyWord = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEShortName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCShortName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTel1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFax = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTypeDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCountryProvinceName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateByName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateByName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCheckedStateDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsDangerous = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.mainGridList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDataSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
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
            this.mainGridList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mainGridList_MouseDown);
            this.mainGridList.MouseLeave += new System.EventHandler(this.mainGridList_MouseLeave);
            this.mainGridList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mainGridList_MouseMove);
            this.mainGridList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mainGridList_MouseUp);
            // 
            // bsDataSource
            // 
            this.bsDataSource.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.CustomerList);
            // 
            // mainGridView
            // 
            this.mainGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCode,
            this.colKeyWord,
            this.colEShortName,
            this.colEName,
            this.colCShortName,
            this.colCName,
            this.colTel1,
            this.colFax,
            this.colTypeDescription,
            this.colCountryProvinceName,
            this.colCreateByName,
            this.colUpdateByName,
            this.colCreateDate,
            this.colUpdateDate,
            this.colCheckedStateDescription,
            this.colIsDangerous});
            this.mainGridView.GridControl = this.mainGridList;
            this.mainGridView.IndicatorWidth = 35;
            this.mainGridView.Name = "mainGridView";
            this.mainGridView.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.mainGridView.OptionsBehavior.Editable = false;
            this.mainGridView.OptionsSelection.MultiSelect = true;
            this.mainGridView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.mainGridView.OptionsView.ColumnAutoWidth = false;
            this.mainGridView.OptionsView.EnableAppearanceEvenRow = true;
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
            this.colCode.Width = 92;
            // 
            // colKeyWord
            // 
            this.colKeyWord.Caption = "关键字";
            this.colKeyWord.FieldName = "KeyWord";
            this.colKeyWord.Name = "colKeyWord";
            this.colKeyWord.Visible = true;
            this.colKeyWord.VisibleIndex = 1;
            this.colKeyWord.Width = 105;
            // 
            // colEShortName
            // 
            this.colEShortName.Caption = "英文简称";
            this.colEShortName.FieldName = "EShortName";
            this.colEShortName.Name = "colEShortName";
            this.colEShortName.Visible = true;
            this.colEShortName.VisibleIndex = 3;
            this.colEShortName.Width = 120;
            // 
            // colEName
            // 
            this.colEName.Caption = "英文名";
            this.colEName.FieldName = "EName";
            this.colEName.Name = "colEName";
            this.colEName.Visible = true;
            this.colEName.VisibleIndex = 5;
            // 
            // colCShortName
            // 
            this.colCShortName.Caption = "中文简称";
            this.colCShortName.FieldName = "CShortName";
            this.colCShortName.Name = "colCShortName";
            this.colCShortName.Visible = true;
            this.colCShortName.VisibleIndex = 2;
            this.colCShortName.Width = 113;
            // 
            // colCName
            // 
            this.colCName.Caption = "中文名";
            this.colCName.FieldName = "CName";
            this.colCName.Name = "colCName";
            this.colCName.Visible = true;
            this.colCName.VisibleIndex = 4;
            this.colCName.Width = 100;
            // 
            // colTel1
            // 
            this.colTel1.Caption = "电话";
            this.colTel1.FieldName = "Tel1";
            this.colTel1.Name = "colTel1";
            this.colTel1.Visible = true;
            this.colTel1.VisibleIndex = 6;
            // 
            // colFax
            // 
            this.colFax.Caption = "传真";
            this.colFax.FieldName = "Fax";
            this.colFax.Name = "colFax";
            this.colFax.Visible = true;
            this.colFax.VisibleIndex = 7;
            // 
            // colTypeDescription
            // 
            this.colTypeDescription.Caption = "类型";
            this.colTypeDescription.FieldName = "TypeDescription";
            this.colTypeDescription.Name = "colTypeDescription";
            this.colTypeDescription.OptionsColumn.ReadOnly = true;
            this.colTypeDescription.Visible = true;
            this.colTypeDescription.VisibleIndex = 11;
            // 
            // colCountryProvinceName
            // 
            this.colCountryProvinceName.Caption = "国家";
            this.colCountryProvinceName.FieldName = "CountryProvinceName";
            this.colCountryProvinceName.Name = "colCountryProvinceName";
            this.colCountryProvinceName.Visible = true;
            this.colCountryProvinceName.VisibleIndex = 8;
            // 
            // colCreateByName
            // 
            this.colCreateByName.Caption = "创建人";
            this.colCreateByName.FieldName = "CreateByName";
            this.colCreateByName.Name = "colCreateByName";
            this.colCreateByName.Visible = true;
            this.colCreateByName.VisibleIndex = 9;
            // 
            // colUpdateByName
            // 
            this.colUpdateByName.Caption = "更新人";
            this.colUpdateByName.FieldName = "updatebyname";
            this.colUpdateByName.Name = "colUpdateByName";
            this.colUpdateByName.Visible = true;
            this.colUpdateByName.VisibleIndex = 10;
            // 
            // colCreateDate
            // 
            this.colCreateDate.Caption = "创建时间";
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 12;
            // 
            // colUpdateDate
            // 
            this.colUpdateDate.Caption = "更新时间";
            this.colUpdateDate.FieldName = "UpdateDate";
            this.colUpdateDate.Name = "colUpdateDate";
            this.colUpdateDate.Visible = true;
            this.colUpdateDate.VisibleIndex = 12;
            // 
            // colCheckedStateDescription
            // 
            this.colCheckedStateDescription.Caption = "审核状态";
            this.colCheckedStateDescription.FieldName = "CheckedStateDescription";
            this.colCheckedStateDescription.Name = "colCheckedStateDescription";
            this.colCheckedStateDescription.OptionsColumn.ReadOnly = true;
            this.colCheckedStateDescription.Visible = true;
            this.colCheckedStateDescription.VisibleIndex = 13;
            // 
            // colIsDangerous
            // 
            this.colIsDangerous.Caption = "危险客户";
            this.colIsDangerous.FieldName = "BizIsDangerous";
            this.colIsDangerous.Name = "colIsDangerous";
            this.colIsDangerous.OptionsColumn.AllowEdit = false;
            this.colIsDangerous.Visible = true;
            this.colIsDangerous.VisibleIndex = 14;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", true, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", false, -1)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            // 
            // CustomerManagerListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.Controls.Add(this.mainGridList);
            this.Name = "CustomerManagerListPart";
            this.Size = new System.Drawing.Size(549, 273);
            ((System.ComponentModel.ISupportInitialize)(this.mainGridList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDataSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        #region 处理拖放事件

        GridHitInfo hitInfo = null;
        private void mainGridList_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            
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
            CustomerList item = mainGridView.GetRow(e.RowHandle) as CustomerList;
            if (item == null) return;

            if (item.State == CustomerStateType.Invalid)
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Disabled);

            if (item.IsDangerous)
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Error);

            if (item.CheckedState == CustomerCodeApplyState.Processing)
            {
                e.Appearance.BackColor = System.Drawing.Color.AliceBlue;
            }
            else if (item.CheckedState == CustomerCodeApplyState.Unpassed)
            {
                e.Appearance.BackColor = System.Drawing.Color.Gainsboro;
            }
            else if (item.CheckedState == CustomerCodeApplyState.UnApply)
            {
                e.Appearance.BackColor = System.Drawing.Color.Linen;
            }
            //else
            //{
            //    e.Appearance.BackColor = System.Drawing.Color.White;
            //}
        }

        #endregion

        /// <summary>
        /// 新增客户
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CustomerManagerConstants.CMD_AddCustomer)]
        public void CMD_AddCustomer(object s, EventArgs e)
        {
            CustomerManagerCheckForm checkForm = WorkItem.Items.AddNew<CustomerManagerCheckForm>();

            string title = LocalData.IsEnglish ? "Add Customer" : "新增客户";
            DialogResult dlg = PartLoader.ShowDialog(checkForm, title);
            if (dlg != DialogResult.OK)
            {
                return;
            }

            CustomerList customer = new CustomerList();
            if (LocalData.IsEnglish)
            {
                customer.EName = checkForm.CustomerName;
            }
            else
            {
                customer.CName = checkForm.CustomerName;
            }
            using (new CursorHelper())
            {
                CustomerManagerEditPart editPart = WorkItem.Items.AddNew<CustomerManagerEditPart>();
                editPart.Saved += new SavedHandler(AfterEditPartSaved);
                Dictionary<string, object> values = new Dictionary<string, object>();
                values.Add("CustomerList", customer);
                editPart.Init(values);
                ISmartPartInfo info = new SmartPartInfo();
                info.Title = title;
                IWorkspace mainWorkspace = WorkItem.RootWorkItem.Workspaces[ClientConstants.MainWorkspace];
                mainWorkspace.Show(editPart, info);
            }
        }

        /// <summary>
        /// 编辑客户
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CustomerManagerConstants.CMD_EditCustomer)]
        public void CMD_EditCustomer(object s, EventArgs e)
        {
            if (CurrentRow == null) return;
            using (new CursorHelper())
            {
                CustomerManagerEditPart editPart = WorkItem.Items.AddNew<CustomerManagerEditPart>();
                editPart.Saved += new SavedHandler(AfterEditPartSaved);
                Dictionary<string, object> values = new Dictionary<string, object>();
                values.Add("CustomerList", CurrentRow);
                editPart.Init(values);
                ISmartPartInfo info = new SmartPartInfo();
                info.Title = LocalData.IsEnglish ? "Edit Customer" : "编辑客户";
                IWorkspace mainWorkspace = WorkItem.RootWorkItem.Workspaces[ClientConstants.MainWorkspace];
                mainWorkspace.Show(editPart, info);
            }
        }

        private void AfterEditPartSaved(object[] prams)
        {
            if (prams == null || prams.Length == 0) return;
            CustomerList listdata = prams[0] as CustomerList;
            List<CustomerList> source = this.DataSource as List<CustomerList>;
            if (source == null || source.Count == 0)
            {
                bsDataSource.DataSource = new List<CustomerList>();
                bsDataSource.Insert(0, listdata);
                bsDataSource.ResetBindings(false);
            }
            else
            {
                CustomerList tager = source.Find(delegate(CustomerList item) { return item.ID == listdata.ID; });
                if (tager == null)
                {
                    bsDataSource.Insert(0, listdata);
                    bsDataSource.ResetBindings(false);

                }
                else
                {
                    Utility.CopyToValue(listdata, tager, typeof(CustomerList));
                    bsDataSource.ResetItem(bsDataSource.IndexOf(tager));
                }
            }

            // if (CurrentChanged != null) CurrentChanged(this, bsDataSource.Current as CustomerList);
        }

        /// <summary>
        /// 申请代码
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CustomerManagerConstants.CMD_ApplyCode)]
        public void CMD_ApplyCode(object s, EventArgs e)
        {
            CustomerList currentRow = Current as CustomerList;
            if (currentRow == null) return;
            using (new CursorHelper())
            {
                ApplyCustomerCodeForm applyCodeForm = WorkItem.Items.AddNew<ApplyCustomerCodeForm>();
                applyCodeForm.CurrentCustomer = currentRow;
                string title = LocalData.IsEnglish ? "Apply Code" : "申请代码";

                DialogResult dlg = PartLoader.ShowDialog(applyCodeForm, title);
                if (dlg != DialogResult.OK)
                {
                    return;
                }

                if (applyCodeForm.IsApplyDone)
                {
                    currentRow.CheckedState = CustomerCodeApplyState.Processing;
                    bsDataSource.ResetCurrentItem();
                }
            }
        }

        /// <summary>
        /// 作废客户
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CustomerManagerConstants.CMD_Disable)]
        public void CMD_Disable(object s, EventArgs e)
        {
            CustomerList currentRow = Current as CustomerList;
            if (currentRow == null) return;

            CustomerStateType state = CustomerStateType.Invalid;
            if (currentRow.State == CustomerStateType.Invalid)
            {
                state = CustomerStateType.Valid;
            }
            using (new CursorHelper())
            {
                DisableCustomerMemoContentForm memoContentForm = WorkItem.Items.AddNew<DisableCustomerMemoContentForm>();
                if (state == CustomerStateType.Valid)
                {
                    memoContentForm.IsDisable = false;
                    memoContentForm.Text = LocalData.IsEnglish ? "Activate Customer" : "激活客户";
                }
                else
                {
                    memoContentForm.IsDisable = true;
                    memoContentForm.Text = LocalData.IsEnglish ? "Disuse Customer" : "作废客户";
                }

                DialogResult dlg = memoContentForm.ShowDialog();
                if (dlg != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    SingleResultData result = CustomerService.ChangeCustomerState(currentRow.ID, state, LocalData.UserInfo.LoginID, memoContentForm.Memo, currentRow.UpdateDate);
                    currentRow.State = state;
                    currentRow.UpdateDate = result.UpdateDate;
                    bsDataSource.ResetCurrentItem();
                    bsDataSource_CurrentChanged(this, null);
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Disuse Successfully" : "作废/激活成功");
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
            }
        }

        /// <summary>
        /// 设置/取消危险客户
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(CustomerManagerConstants.CMD_SetDangerous)]
        public void CMD_SetDangerous(object s, EventArgs e)
        {
            CustomerList currentRow = Current as CustomerList;
            if (currentRow == null) return;

            SetCustomerIsDangerousForm memoContentForm = WorkItem.Items.AddNew<SetCustomerIsDangerousForm>();
            memoContentForm.Text = LocalData.IsEnglish ? "Risk Clients" : "危险客户";
            // memoContentForm.Text = Utility.GetString("RiskClients");
            if (currentRow.IsDangerous)
            {
                memoContentForm.IsDangerous = true;
            }
            else
            {
                memoContentForm.IsDangerous = false;
            }

            DialogResult dlg = memoContentForm.ShowDialog();
            if (dlg != DialogResult.OK)
            {
                return;
            }

            try
            {
                SingleResultData result = CustomerService.SetCustomerIsDangerous(currentRow.ID, !currentRow.IsDangerous, LocalData.UserInfo.LoginID, memoContentForm.Memo, currentRow.UpdateDate);
                currentRow.IsDangerous = !currentRow.IsDangerous;
                currentRow.UpdateDate = result.UpdateDate;
                bsDataSource.ResetCurrentItem();
                bsDataSource_CurrentChanged(this, null);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Disuse Successfully" : "设置/取消危险客户成功");
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }
    }
}
