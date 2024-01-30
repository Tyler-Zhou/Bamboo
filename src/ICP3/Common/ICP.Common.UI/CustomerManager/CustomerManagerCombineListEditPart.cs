//-----------------------------------------------------------------------
// <copyright file="CustomerManagerCombineListPart.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.Common.UI.CustomerManager
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using ICP.Framework.ClientComponents.UIFramework;
    using Microsoft.Practices.CompositeUI;
    using ICP.Common.ServiceInterface.DataObjects;
    using System.Windows.Forms;
    using DevExpress.XtraEditors;
    using ICP.Framework.CommonLibrary.Client;
    using ICP.Framework.CommonLibrary.Common;

    /// <summary>
    /// 客户合并列表
    /// </summary>
    [System.ComponentModel.ToolboxItem(false)]
    public partial class CustomerManagerCombineListEditPart : BaseListEditPart
    {
        #region 初始化
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public CustomerManagerCombineListEditPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.Selected = null;
                this.Selecting = null;
                this.CurrentChanged = null;
                this.CurrentChanging = null;
                this.repositoryItemImageComboBox1.SelectedIndexChanged -= this.repositoryItemImageComboBox1_SelectedIndexChanged;
                this.mainGridList.DragDrop -= this.mainGridList_DragDrop;
                this.mainGridList.DragEnter -= this.mainGridList_DragEnter;
                this.mainGridView.CustomDrawRowIndicator -= this.mainGridView_CustomDrawRowIndicator;

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
            if (!DesignMode)
            {
                this.InitControls();
                this.Enabled = false;
            }
        }

        private void InitControls()
        {
            barCombine.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Plus_16;
            barDelete.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Delete_16;
            repositoryItemImageComboBox1.Items.Clear();
            repositoryItemImageComboBox1.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem((LocalData.IsEnglish ? "View" : "查看"), 0, 0));
            repositoryItemImageComboBox1.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem((LocalData.IsEnglish ? "Combine" : "合并"), 1, 0));
            barEditItem1.EditValue = 0;
            repositoryItemImageComboBox1.SelectedIndexChanged += new EventHandler(repositoryItemImageComboBox1_SelectedIndexChanged);

            if (!LocalCommonServices.PermissionService.HaveActionPermission(CustomerManagerConstants.Common_mergerCustomer))
            {
                repositoryItemImageComboBox1.Enabled = false;
                barCombine.Enabled = false;
                barDelete.Enabled = false;
            }

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

        /*返回网格当前行*/
        public CustomerCombineList CurrentRow
        {
            get
            {
                if (bsDataSource.Count > 0 && bsDataSource.Current != null)
                {
                    return bsDataSource.Current as CustomerCombineList;
                }
                else
                {
                    return null;
                }
            }
        }

        #region IListEditPart接口

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
        private bool _isCombineMode = false;
        public override void Init(IDictionary<string, object> values)
        {
            if (!_isCombineMode && values != null)
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

                BindingData();
            }

            barCombine.Enabled = false;
            barDelete.Enabled = false;
        }

        /// <summary>
        /// 添加行
        /// </summary>
        /// <param name="item">数据</param>
        public override void AddItem(object item)
        {
            if (_isCombineMode && item != null)
            {
                var returndata = item as CustomerList;

                List<CustomerCombineList> currentData = bsDataSource.DataSource as List<CustomerCombineList>;
                if (currentData != null && currentData.Count > 0)
                {
                    foreach (var combineItem in currentData)
                    {
                        if (combineItem.ID == returndata.ID)
                        {
                            return;
                        }
                    }

                    //if (returndata == null || returndata.ID == _currentCustomer.ID)
                    //{
                    //    return;
                    //}
                }

                _currentCustomer = returndata;
                CustomerCombineList newdata = new CustomerCombineList();

                ICP.Framework.CommonLibrary.Helper.ObjectHelper.CopyData(_currentCustomer, newdata);
                newdata.IsAddTemp = true;
                bsDataSource.Add(newdata);
                RefreshToolBars();
            }
        }

        #endregion

        #region 私有方法

        private void BindingData()
        {
            List<CustomerCombineList> list = new List<CustomerCombineList>();
            if (_currentCustomer.ID != null && _currentCustomer.ID != Guid.Empty)
            {
                list = this.Controller.GetCustomerCombineList(_currentCustomer.ID);
            }
            else
            {
                CustomerCombineList self = new CustomerCombineList();
                ICP.Framework.CommonLibrary.Helper.ObjectHelper.CopyData(_currentCustomer, self);
                list.Add(self);
            }

            this.bsDataSource.DataSource = list;
            RefreshToolBars();
        }

        /*合并前数据验证*/
        private bool ValidateData()
        {
            EndEdit();

            if (this.bsDataSource.Count < 2)
            {
                //this._statusBar.SetStatusBarPanel1(Utility.IsEnglish ? "Must have to have two or two above customers can merge" : "必须要有两个或两个以上的客户才能合并");
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(
                  this.FindForm(),
                  "必须要有两个或两个以上的客户才能合并!");
                return false;
            }

            //if ((bdsCustomerContact.List as List<PublicCustomerListData>).FindAll(delegate(PublicCustomerListData sourceitem) { return sourceitem.Selected == true; }).Count != 1)
            //{//UNDONE 翻译
            //    MessageBox.Show(Utility.IsEnglish ? "Has also only can have a retention customer!" : "有且只能有一个保留客户!", Utility.IsEnglish ? "Tip" : "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}
            int flag = 0;
            foreach (CustomerCombineList data in bsDataSource.List)
            {
                if (data.IsMain)
                {
                    flag++;
                }
            }

            if (flag != 1)
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(
                 this.FindForm(),
                 "有且只能有一个保留客户!");
                return false;
            }

            return true;
        }


        /*刷新工具条*/
        private void RefreshToolBars()
        {
            if (bsDataSource.Count > 1 && LocalCommonServices.PermissionService.HaveActionPermission(CustomerManagerConstants.Common_mergerCustomer))
            {
                barCombine.Enabled = true;
            }
            else
            {
                barCombine.Enabled = false;
            }

            if (CurrentRow == null)
            {
                barDelete.Enabled = false;
            }
            else
            {
                barDelete.Enabled = true;
            }
        }

        /*清除所有够选*/
        private void ClearAllSelect()
        {
            foreach (CustomerCombineList data in bsDataSource.List)
            {
                data.IsMain = false;
                bsDataSource.ResetItem(bsDataSource.IndexOf(data));
            }
            //bdsCustomerContact.EndEdit();
            //bdsCustomerContact.ResetBindings(false);
        }

        #endregion

        #region 事件处理

        /*合并*/
        private void barCombine_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ValidateData() == false)
            {
                return;
            }

            DialogResult dlg = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Are you sure combine?" : "确认要合并这些客户?", LocalData.IsEnglish ? "Confirm" : "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dlg != DialogResult.OK)
            {
                return;
            }

            try
            {
                Guid mainCustomerId = Guid.Empty;
                List<Guid> combineCustomerIds = new List<Guid>();
                List<DateTime?> updateDates = new List<DateTime?>();
                foreach (CustomerCombineList data in bsDataSource.List)
                {
                    if (data.IsMain)
                    {
                        mainCustomerId = data.ID;
                    }

                    combineCustomerIds.Add(data.ID);
                    updateDates.Add(data.UpdateDate);
                }

                ManyResultData mans = this.Controller.CombineCustomers(mainCustomerId
                    , combineCustomerIds.ToArray()
                    , LocalData.UserInfo.LoginID
                    , updateDates.ToArray());

                //Clear();
                RefreshToolBars();

                //this.Cursor = Cursors.Default;
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(
                    this.FindForm(),
                    "客户合并申请保存成功!");
            }
            catch (Exception ex)
            {
                //设置错误信息
                LocalCommonServices.ErrorTrace.SetErrorInfo(
                    this.FindForm(),
                    ex);
            }
        }

        /*删除*/
        private void barDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (CurrentRow == null) return;

            if (bsDataSource.Count == 1)
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(
                  this.FindForm(),
                  "需要有一个保留客户!");

                return;
            }

            DialogResult dlg = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Are you sure to delete current row?" : "确信要从列表移除当前客户?", LocalData.IsEnglish ? "Confirm" : "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dlg != DialogResult.OK)
            {
                return;
            }

            try
            {
                if (!CurrentRow.IsMain)
                {
                    List<CustomerCombineList> combinelists = new List<CustomerCombineList>();
                    combinelists.Add(CurrentRow);

                    List<Guid> ids = new List<Guid>();
                    List<DateTime?> updateDatas = new List<DateTime?>();
                    foreach (var item in combinelists)
                    {
                        if (item.IsNew || item.IsMain) continue;
                        ids.Add(item.ID);
                        updateDatas.Add(item.UpdateDate);
                    }

                    this.Controller.CancelCombineCustomers(ids.ToArray(), LocalData.UserInfo.LoginID, updateDatas.ToArray());
                }

                bsDataSource.RemoveCurrent();
                if (bsDataSource.Count > 0)
                {
                    bsDataSource.MoveFirst();
                }

                RefreshToolBars();
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(
                   this.FindForm(),
                   "移出成功!");
            }
            catch (Exception ex)
            {
                //设置错误信息
                LocalCommonServices.ErrorTrace.SetErrorInfo(
                    this.FindForm(),
                    ex);
            }
        }

        void repositoryItemImageComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var currentItem = sender as ComboBoxEdit;
            if (currentItem.SelectedIndex == 0)
            {
                //BindingData();
                _isCombineMode = false;
                barCombine.Enabled = false;
                barDelete.Enabled = false;
            }
            else if (currentItem.SelectedIndex == 1)
            {
                _isCombineMode = true;
                //barCombine.Enabled = true;
                //barDelete.Enabled = true;
                RefreshToolBars();
            }
        }

        #endregion

        private void mainGridList_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            CustomerList customer = (CustomerList)e.Data.GetData(typeof(CustomerList));

            bsDataSource.Add(customer);
        }

        private void mainGridList_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void mainGridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }
    }
}
