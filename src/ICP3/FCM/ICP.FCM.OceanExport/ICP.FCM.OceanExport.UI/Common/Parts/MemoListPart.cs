namespace ICP.FCM.OceanExport.UI.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using ICP.Framework.ClientComponents.UIFramework;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI;
    using ICP.Framework.CommonLibrary.Client;
    using System.Windows.Forms;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.FCM.Common.ServiceInterface.DataObjects;

    [System.ComponentModel.ToolboxItem(false)]
    [SmartPart]
    public partial class MemoListPart : BaseListEditPart
    {
        #region 服务注入

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        public ICP.FCM.Common.ServiceInterface.IFCMCommonService fcmCommonService
        {
            get
            {
                return ServiceClient.GetService<ICP.FCM.Common.ServiceInterface.IFCMCommonService>();
            }
        }

        #endregion

        #region 初始化

        public MemoListPart()
        {
            this.InitializeComponent();
            this.Enabled = false;
            this.Disposed += delegate
            {
                if (Workitem != null)
                { 
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            this.InitControls();
        }

        private void InitControls()
        {
            barAdd.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Plus_16;
            barDelete.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Delete_16;
            barSave.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Save_16;
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

        MemoParam _memoParam = null;
        private void BindingData(object value)
        {
           _memoParam = value as MemoParam;
            if (_memoParam == null)
            {
                this.bsList.DataSource = typeof(CommonMemoList);
                this.Enabled = false;
            }
            else
            {
                this.Enabled = true;
               
                List<EventObjects> memoList = fcmCommonService.GetMemoList(_memoParam.OperationId, _memoParam.FormID);
                bsList.DataSource = memoList;
                bsList.ResetBindings(false);
                RefreshToolBars();
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
            this.Validate();
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
            Guid?[] ids = new Guid?[bsList.Count];
            Guid?[] formIDs = new Guid?[bsList.Count];
            string[] subjects = new string[bsList.Count];
            string[] contents = new string[bsList.Count];
            ICP.Framework.CommonLibrary.Common.FormType[] formTypes = new ICP.Framework.CommonLibrary.Common.FormType[bsList.Count];
            MemoPriority[] prioritys = new MemoPriority[bsList.Count];
            MemoType[] types = new MemoType[bsList.Count];
            bool[] isShowAgents = new bool[bsList.Count];
            bool[] isShowCustomers = new bool[bsList.Count];
            DateTime?[] updateDates = new DateTime?[bsList.Count];

            int i = 0;
            foreach (CommonMemoList cid in bsList.List)
            {
                ids[i] = cid.ID;  //备注ID
                formIDs[i] = _memoParam.FormID;
                formTypes[i] = _memoParam.FormType;
                isShowAgents[i] = false;
                isShowCustomers[i] = false;
                subjects[i] = cid.Subject;
                contents[i] = cid.Content;
                types[i] = MemoType.Memo;
                prioritys[i] = MemoPriority.Normal;
                updateDates[i] = cid.UpdateDate;

                i++;
            }
            return true;
            //ManyResultData mans = this.fcmCommonService.SaveMemoList(
            //_memoParam.OperationId,
            //_memoParam.OperationType,
            //ids,
            //formIDs,
            //formTypes,
            //isShowAgents,
            //isShowCustomers,
            //subjects,
            //contents,
            //types,
            //prioritys,
            //LocalData.UserInfo.LoginID,
            //updateDates);


            //if (mans == null || mans.ChildResults == null || mans.ChildResults.Count == 0)
            //{
            //    return false;
            //}
            //else
            //{
            //    i = 0;
            //    foreach (CommonMemoList cid in bsList.List)
            //    {
            //        cid.ID = mans.ChildResults[i].ID;
            //        cid.UpdateDate = mans.ChildResults[i].UpdateDate;
            //        i++;
            //    }

            //    return true;
            //}
        }

        #endregion

        #region 本地控制逻辑

        /*添加备注*/
        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //EndEdit();
            gvMain.CloseEditor();
            CommonMemoList newContact = new CommonMemoList();
            newContact.CreateByID = LocalData.UserInfo.LoginID;
            newContact.ID = Guid.Empty;
            newContact.CreateByName = LocalData.UserInfo.LoginName;
            newContact.CreateDate = DateTime.Now;
            newContact.IsDirty = false;
            bsList.Insert(0, newContact);
            bsList.MoveFirst();
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

            if (bsList.Count < 1) return;
            if (Current == null) return;

            try
            {
                //保存数据
                this.SaveData();
                RefreshToolBars();

                //触发保存成功事件
                if (this.Saved != null)
                {
                    this.Saved(this.bsList.DataSource);
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
            foreach (CommonMemoList c in bsList.List)
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
                var memo = Current as CommonMemoList;
                if (memo != null)
                {
                    //if (memo.ID != null && memo.ID != Guid.Empty)
                    //{
                    //    if (memo.Type == MemoType.System)
                    //    {
                    //        return;
                    //    }

                        fcmCommonService.RemoveMemoInfo(memo.ID, LocalData.UserInfo.LoginID, memo.UpdateDate);
                    //}

                    bsList.RemoveCurrent();
                    bsList.MoveFirst();
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

            if (bsList.Count > 0)
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

    }
}
