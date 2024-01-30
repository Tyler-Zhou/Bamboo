using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.ClientComponents.Resources;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.MailCenter.ServiceInterface;
using ICP.MailCenter.UI;
using ICP.Message.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Exception = System.Exception;

namespace ICP.Business.Common.UI.EventList
{

    [ToolboxItem(false)]
    [SmartPart]
    public partial class EventListPart : BaseListEditPart, IDataBind
    {
        #region Fields

        private DataSearchType _DataSourceType = DataSearchType.Local;
        #endregion

        #region 服务注入

        [ServiceDependency]
        public WorkItem Workitem
        {
            get;
            set;
        }
        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public EventPartPresenter EventPartPresenter { get; set; }

        public IFCMCommonService FCMCommonService
        {
            get { return ServiceClient.GetService<IFCMCommonService>(); }

        }
        public IMailCenterTemplateService MailCenterTemplateService
        {
            get
            {
                ClientHelper.EnsureEmailCenterAppStarted();
                return new MailCenterTemplateService();
            }
        }

        public IClientMessageService ClientMessageService
        {
            get
            {
                return ServiceClient.GetClientService<IClientMessageService>();
            }
        }
        #endregion

        #region 初始化
        public RepositoryItem emptyEditor;
        public EventListPart()
        {
            InitializeComponent();
            //this.Load += (sender, e) =>
            //    {
            //        //分组的条件
            //        gvMain.GroupSummary.Clear();
            //        gvMain.SortInfo.Clear();
            //        gvMain.OptionsCustomization.AllowGroup = true;     //是否允许分组
            //        gvMain.OptionsView.ShowGroupedColumns = true;     //显示分组的列
            //        gvMain.OptionsView.ShowGroupPanel = true;          // 显示分组panel
            //        //排序的条件
            //        GridColumn columnNo = this.gvMain.Columns.ColumnByFieldName("UIIndex");
            //        GridColumnSortInfo sortInfo = new GridColumnSortInfo(columnNo, DevExpress.Data.ColumnSortOrder.Ascending);
            //        this.gvMain.SortInfo.Add(sortInfo);
            //        //分组
            //        DevExpress.XtraGrid.Columns.GridColumn column = gvMain.Columns["CategoryName"];
            //        column.GroupIndex = 0;
            //        DevExpress.XtraGrid.GridGroupSummaryItem item = new DevExpress.XtraGrid.GridGroupSummaryItem();
            //        //展开分组的节点
            //        gvMain.OptionsBehavior.AutoExpandAllGroups = true;
            //        gvMain.GroupSummary.Add(item);



            //    };
            Disposed += delegate
            {
                gcMain.DataSource = null;
                bsList.DataSource = null;
                bsList = null;
                EventPartPresenter = null;
                CurrentChanged = null;
                CurrentChanging = null;
                Saved = null;
                Selected = null;
                Selecting = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
                if (EventPartPresenter != null)
                {
                    EventPartPresenter.Dispose();
                    EventPartPresenter = null;
                }
            };
            Enabled = false;
            if (!LocalData.IsDesignMode)
            {
                if (!LocalData.IsEnglish) SetCnText();
            }
            CreateHandle();
        }

        private void SetCnText()
        {
            colCode.Caption = "事件代码";
            colDescription.Caption = "描述";
            colCreateDate.Caption = "发生时间";
            colOwner.Caption = "创建人";
            colSubject.Caption = "主题";
            barAdd.Caption = "新增";
            barEdit.Caption = "编辑";
            barDelete.Caption = "删除";
            barRefresh.Caption = "刷新";
            Type.Caption = "详细";
        }

        protected override void OnLoad(EventArgs e)
        {
            InitControls();

        }

        private void InitControls()
        {
            barAdd.Glyph = GlobalResource.Plus_16;
            barDelete.Glyph = GlobalResource.Delete_16;
            barEdit.Glyph = GlobalResource.Edit_16;
            barRefresh.Glyph = GlobalResource.Refresh_16;

            List<EnumHelper.ListItem<DataSearchType>> dateSearchTypes = EnumHelper.GetEnumValues<DataSearchType>(LocalData.IsEnglish);
            comboBoxRangs.BeginUpdate();
            comboBoxRangs.Items.Clear();
            foreach (var item in dateSearchTypes)
            {
                comboBoxRangs.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            comboBoxRangs.EndUpdate();
            comboBoxRangs.SelectedIndexChanged += comboBoxRangs_SelectedIndexChanged;
            barEditItemRangs.EditValue = DataSearchType.Local;
        }

        void comboBoxRangs_SelectedIndexChanged(object sender, EventArgs e)
        {
            _DataSourceType=(DataSearchType)((ImageComboBoxEdit)sender).EditValue;
            barRefresh.PerformClick();
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
                return bsList.Current;
            }
        }

        public EventObjects CurrentRow
        {
            get
            {
                return bsList.Current as EventObjects;
            }
        }
        /// <summary>
        /// 数据源
        /// </summary>
        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                BindingData(value);
            }
        }

        BusinessOperationContext context = null;
        private delegate void DataBindDelegate(List<EventObjects> eventList);
        private void InnerBind(List<EventObjects> eventList)
        {
            bsList.DataSource = eventList;
            gvMain.ExpandAllGroups();
            //gvMain.AutoExpandAllGroups = True 
            bsList.ResetBindings(true);
            RefreshToolBars();
        }
        private void BindingData(object value)
        {

            context = value as BusinessOperationContext;
            if (context == null)
            {
                bsList.DataSource = typeof(EventObjects);
                Enabled = false;
            }
            else
            {
                Enabled = true;
                List<EventObjects> eventList = new List<EventObjects>();
                if (context.OperationID == Guid.Empty)
                {
                    bsList.DataSource = eventList;
                    return;
                }
                ThreadPool.QueueUserWorkItem((obj) =>
                {
                    try
                    {
                        ClientHelper.SetApplicationContext();

                        List<EventObjects> temp = FCMCommonService.GetMemoList(context.OperationID, _DataSourceType, null);
                        if (IsDisposed)
                            return;

                        DataBindDelegate bindDelegate = new DataBindDelegate(InnerBind);
                        //if (temp.Any())
                        //{
                        //    if (temp[0].OperationID != context.OperationID)
                        //    {
                        //        temp = new List<EventObjects>();
                        //    }
                        //}
                        Invoke(bindDelegate, temp);

                    }
                    catch (Exception ex)
                    {
                        if (!IsDisposed)
                        {
                            LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
                        }
                    }
                });
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
            bsList.Add(item);
        }

        public override void Clear()
        {
            bsList.Clear();
        }

        public override void EndEdit()
        {
            Validate();
            bsList.EndEdit();
        }

        public override void InsertItem(int index, object item)
        {
            bsList.Insert(index, item);
        }

        public override void RaiseCurrentChanged()
        {
        }

        public override void RaiseCurrentChanging()
        {
        }

        public override void RaiseSaved()
        {
            SaveData();

            if (Saved != null)
            {
                Saved(DataSource);
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
            int ListCount = bsList.Count;
            Guid?[] ids = new Guid?[ListCount];
            Guid?[] formIDs = new Guid?[ListCount];
            string[] codes = new string[ListCount];
            string[] descriptions = new string[ListCount];
            string[] owner = new string[ListCount];
            string[] orderNames = new string[ListCount];
            DateTime?[] createDates = new DateTime?[ListCount];
            FormType[] formTypes = new FormType[ListCount];
            MemoType[] types = new MemoType[ListCount];
            bool[] isShowAgents = new bool[ListCount];
            bool[] isShowCustomers = new bool[ListCount];
            DateTime?[] updateDates = new DateTime?[ListCount];

            int i = 0;
            foreach (EventObjects cid in bsList.List)
            {
                ids[i] = cid.Id;  //备注ID
                formIDs[i] = context.FormId;
                formTypes[i] = context.FormType;
                isShowAgents[i] = false;
                isShowCustomers[i] = false;
                codes[i] = cid.Code;
                orderNames[i] = cid.CategoryName;
                descriptions[i] = cid.Description;
                types[i] = cid.Type;
                createDates[i] = cid.CreateDate;
                updateDates[i] = cid.UpdateDate;
                i++;
            }

            return true;
        }

        #endregion

        #region 本地控制逻辑

        /*添加备注*/
        private void barAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (context != null)
            {
                EventPartPresenter.Opent(context, Workitem, bsList, true);
            }
        }

        /*删除备注信息*/
        private void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            var memo = Current as EventObjects;
            if (memo.Type != MemoType.Manually)
            {
                XtraMessageBox.Show(LocalData.IsEnglish ? "System events can not be deleted!" : "系统事件不允许删除！", LocalData.IsEnglish ? "Confirm" : "确认", MessageBoxButtons.OK);
                return;
            }
            DialogResult dlg = XtraMessageBox.Show(LocalData.IsEnglish ? "Are you sure to delete?" : "确认删除当前数据?", LocalData.IsEnglish ? "Confirm" : "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
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
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }

        private void InsertEventObject(object sender, CommonEventArgs<EventObjects> args)
        {
            if (args.Data != null)
            {
                EventObjects newContact = args.Data;
                bsList.Insert(0, newContact);
                bsList.MoveFirst();
            }

        }

        /*保存备注信息*/
        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            EndEdit();
            if (!ValidateData())
            {
                return;
            }

            if (bsList.Count < 1) return;
            if (Current == null) return;

            try
            {
                //保存数据
                SaveData();
                RefreshToolBars();

                //触发保存成功事件
                if (Saved != null)
                {
                    Saved(bsList.DataSource);
                }

                //提示保存成功
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(
                    FindForm(),
                    "保存成功!");
            }
            catch (Exception ex)
            {
                //设置错误信息
                LocalCommonServices.ErrorTrace.SetErrorInfo(
                    FindForm(),
                    ex);
            }
        }

        /*验证表单数据*/
        private bool ValidateData()
        {
            //foreach (EventObjects c in bsList.List)
            //{
            //    if (c.Validate() == false)
            //    {
            //        return false;
            //    }
            //}

            return true;
        }

        /*删除当前选择的备注*/
        private void DeleteRow()
        {
            if (Current != null)
            {
                var memo = Current as EventObjects;
                if (memo != null)
                {
                    if (!ArgumentHelper.GuidIsNullOrEmpty(memo.Id))
                    {
                        FCMCommonService.RemoveMemoInfo(memo.Id, LocalData.UserInfo.LoginID, memo.UpdateDate);
                    }
                    bsList.RemoveCurrent();
                    BindingData(context);
                    bsList.MoveFirst();
                }
            }
        }

        /*刷新工具栏*/
        private void RefreshToolBars()
        {
            //if (Current == null)
            //{
            //    barDelete.Enabled = false;
            //}
            //else
            //{
            //    barDelete.Enabled = true;
            //}

            //if (bsList.Count > 0)
            //{
            //    barSave.Enabled = true;
            //}
            //else
            //{
            //    barSave.Enabled = false;
            //}

            //if (CurrentRow != null && _memoParam != null)
            //{
            //    if (CurrentRow.FormID == _memoParam.FormID)
            //    {
            //        this.barDelete.Enabled = true;
            //    }
            //    else
            //    {
            //        this.barDelete.Enabled = false;
            //    }
            //}
        }

        private void mainGridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }

        #endregion

        #region 换行
        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            //if (CurrentRow != null && _memoParam!=null)
            //{
            //    if (CurrentRow.FormID == _memoParam.FormID)
            //    {
            //        SetColumnEnb(true);

            //        this.barDelete.Enabled = true;
            //    }
            //    else
            //    {
            //        SetColumnEnb(false);
            //        this.barDelete.Enabled = false;
            //    }
            //}
        }
        private void SetColumnEnb(bool isEdit)
        {
            //foreach (GridColumn col in this.gvMain.Columns)
            //{
            //    col.OptionsColumn.AllowEdit = isEdit;
            //}
        }
        #endregion

        private void gvMain_DoubleClick(object sender, EventArgs e)
        {

        }

        private void barEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (CurrentRow != null)
            {
                if (CurrentRow.Type == MemoType.Manually || CurrentRow.Type == MemoType.Memo)
                {
                    EventPartPresenter.Opent(context, Workitem, bsList, false);
                    BindingData(context);
                }
                else
                {
                    XtraMessageBox.Show(LocalData.IsEnglish ? "System events can not be Update!" : "系统生成事件不能修改!", LocalData.IsEnglish ? "Confirm" : "确认", MessageBoxButtons.OK);
                }
            }
        }


        #region IDataBind 成员

        public void DataBind(BusinessOperationContext context)
        {
            BindingData(context);
        }
        /// <summary>
        /// 设置只读
        /// </summary>
        /// <param name="flg"></param>
        public void ControlsReadOnly(bool flg)
        {
            barAdd.Enabled = flg;
            barDelete.Enabled = flg;
            barEdit.Enabled = flg;
        }
        #endregion
        /// <summary>
        /// 列点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                if (CurrentRow != null && CurrentRow.MessageID != Guid.Empty)
                {
                    Message.ServiceInterface.Message messageInfo = ClientMessageService.GetMessageInfoById((Guid)CurrentRow.MessageID);
                    if (messageInfo != null)
                    {
                        ServiceClient.GetClientService<IClientMessageService>()
                            .GetMailItemAndOpen(messageInfo.EntryID, messageInfo.Id.ToString());
                    }
                }
            }
        }

        private void barRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            BindingData(context);
        }
        /// <summary>
        /// 设置行样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            var row = gvMain.GetRow(e.RowHandle) as EventObjects;
            if (row == null) return;
            //必须完成的事件
            if (row.Logged == false)
            {
                e.Appearance.ForeColor = Color.DarkGray;

                //e.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            }
            //当前是否为重要的事件 EventIndex为0表示当前的事件不是必须完成事件
            if ((row.Important || row.ManualImportant) && row.Logged)
            {
                e.Appearance.ForeColor = Color.Blue;
            }
        }
        /// <summary>
        /// 列表显示值处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "CategoryName")
            {
                e.DisplayText = e.DisplayText.Replace("1", string.Empty)
                                 .Replace("2", string.Empty)
                                 .Replace("3", string.Empty);
            }


        }
        /// <summary>
        /// 分组名称的显示值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_CustomDrawGroupRow(object sender, RowObjectCustomDrawEventArgs e)
        {
            GridGroupRowInfo GridGroupRowInfo = e.Info as GridGroupRowInfo;
            GridGroupRowInfo.GroupText = GridGroupRowInfo.GroupText.Replace("1", string.Empty)
                                 .Replace("2", string.Empty)
                                 .Replace("3", string.Empty);
        }
    }
}
