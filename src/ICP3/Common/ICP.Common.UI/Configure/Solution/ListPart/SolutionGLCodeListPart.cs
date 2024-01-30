using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using System.Windows.Forms;

namespace ICP.Common.UI.Configure.Solution
{
    [System.ComponentModel.ToolboxItem(false)]
    [SmartPart]
    public partial class SolutionGLCodeListPart : DevExpress.XtraEditors.XtraUserControl, ICP.Framework.ClientComponents.UIManagement.IDataContentPart
    {
        #region 服务注入

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        #endregion

        #region init

        GLCodeParent _GLCodeParent;

        List<SolutionGLCodeList> _CodeList = new List<SolutionGLCodeList>();

        public SolutionGLCodeListPart()
        {
            InitializeComponent();

            if (LocalData.IsEnglish == false) this.SetCnText();
            this.Disposed += delegate
            {
                this._CodeList = null;
                this._GLCodeParent = null;
                this.dxErrorProvider1.DataSource = null;
                this.gridView1.InitNewRow -= this.gridView1_InitNewRow;
                this.gridView1.RowCellStyle -= this.gridView1_RowCellStyle;
                this.gridView1.CustomDrawRowIndicator -= this.mainGridView_CustomDrawRowIndicator;
                this.gridControl1.DataSource = null;
                this.bindingSource1.PositionChanged -= this.bindingSource1_PositionChanged;
                this.bindingSource1.DataSource = null;
                this.bindingSource1.Dispose();
                this.DataChanged = null;
                if (this.Workitem != null)
                {
                    this.Workitem.Items.Remove(this);
                    this.Workitem = null;
                }

            };
        }




        private void SetCnText()
        {
            colCName.Caption = "中文名";
            colCode.Caption = "代码";
            colDescription.Caption = "描述";
            colEName.Caption = "英文名";
            colCreateByName.Caption = "创建人";
            colCreateDate.Caption = "创建日期";
            gridView1.NewItemRowText = "点击这里以新增一行.";

            barSave.Caption = "保存(&S)";
            barDisuse.Caption = "作废(&S)";
        }

        public void BindingData(object data)
        {
            GLCodeParent glCodeParent = data as GLCodeParent;

            if (glCodeParent == null || glCodeParent.GLGroupID == Guid.Empty || glCodeParent.SolutionID == Guid.Empty)
            {
                this.Enabled = false;
            }
            else
            {
                //if (glCodeParent.SolutionID != (_GLCodeParent == null ? Guid.Empty : _GLCodeParent.SolutionID) || _CodeList == null)
                //{
                //    _CodeList = ConfigureService.GetSolutionGLCodeList(glCodeParent.SolutionID, null);
                //}

                _CodeList = ConfigureService.GetSolutionGLCodeList(glCodeParent.SolutionID, null);

                List<SolutionGLCodeList> list = _CodeList.FindAll(delegate(SolutionGLCodeList item) { return item.ParentID == glCodeParent.GLGroupID; });
                this.bindingSource1.DataSource = list;

                this.Enabled = true;
                _GLCodeParent = glCodeParent;
            }

            this.RefreshToolBars();
        }

        #endregion

        #region IDataContentPart 成员
        public bool AutoWidth { get; set; }
        public object Current { get { return null; } }
        public event EventHandler<ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>> DataChanged;
        public object DataSource
        {
            get { return this.bindingSource1.DataSource; }
            set { BindingData(value); }
        }


        public void EndEdit()
        {
            this.Validate();
            bindingSource1.EndEdit();
        }
        #endregion

        #region event

        #region Save

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveData();
        }

        private void SaveData()
        {
            this.EndEdit();
            if (this.ValidateData() == false) return;

            List<SolutionGLCodeList> currentList = bindingSource1.DataSource as List<SolutionGLCodeList>;

            List<Guid?> ids = new List<Guid?>();
            List<DateTime?> versions = new List<DateTime?>();
            List<string> codes = new List<string>(), cNames = new List<string>(), eNames = new List<string>(), descriptions = new List<string>();

            for (int i = 0; i < currentList.Count; i++)
            {
                ids.Add(currentList[i].ID);
                versions.Add(currentList[i].UpdateDate);

                descriptions.Add(currentList[i].Description);
                codes.Add(currentList[i].Code);

                cNames.Add(currentList[i].CName);
                eNames.Add(currentList[i].EName);
            }
            try
            {
                ManyResultData result = ConfigureService.SaveSolutionGLCodeInfo(_GLCodeParent.SolutionID
                                                                                , ids.ToArray()
                                                                                , _GLCodeParent.GLGroupID
                                                                                , codes.ToArray()
                                                                                , cNames.ToArray()
                                                                                , eNames.ToArray()
                                                                                , descriptions.ToArray()
                                                                                , LocalData.UserInfo.LoginID
                                                                                , versions.ToArray());
                for (int i = 0; i < currentList.Count; i++)
                {
                    currentList[i].ID = result.ChildResults[i].ID;
                    currentList[i].UpdateDate = result.ChildResults[i].UpdateDate;
                }
                AfterSave();

            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }
        }

        private void AfterSave()
        {
            List<SolutionGLCodeList> currentList = bindingSource1.DataSource as List<SolutionGLCodeList>;
            List<Guid> ids = new List<Guid>();
            foreach (var item in _CodeList)
                ids.Add(item.ID);

            if (ids.Count > 0)
            {
                List<SolutionGLCodeList> tager = currentList.FindAll(delegate(SolutionGLCodeList item) { return ids.Contains(item.ID) == false; });
                if (tager != null && tager.Count > 0)
                    _CodeList.AddRange(tager);
            }
            else
                _CodeList.AddRange(currentList);


            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
        }

        private bool ValidateData()
        {
            List<SolutionGLCodeList> currentList = bindingSource1.DataSource as List<SolutionGLCodeList>;
            if (_GLCodeParent == null || _GLCodeParent.SolutionID == Guid.Empty
                || currentList == null || currentList.Count == 0) return false;

            for (int i = 0; i < currentList.Count; i++)
            {
                if (currentList[i].Validate(delegate(ValidateEventArgs e)
                {
                }) == false)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        private void bindingSource1_PositionChanged(object sender, EventArgs e)
        {
            RefreshToolBars();
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var obj = gridView1.GetRow(e.RowHandle);
            if (_GLCodeParent == null) throw new ApplicationException("InitNewItem error at SolutionCurrencyUIProxyLogic");

            SolutionGLCodeList newData;
            if (obj == null)
                newData = new SolutionGLCodeList();
            else
                newData = obj as SolutionGLCodeList;
            newData.IsValid = true;
            newData.SolutionID = _GLCodeParent.SolutionID;
            newData.ParentID = _GLCodeParent.GLGroupID;
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            newData.GLCodeNodeType = GLCodeNodeType.Item;
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

            SolutionGLCodeList data = this.gridView1.GetRow(e.RowHandle) as SolutionGLCodeList;
            if (data == null) return;
            if (data.IsValid == false)
            {
                CommonUtility.SetRowDisuseStyle(e.Appearance);
            }
        }

        private void mainGridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }

        private void barDisuse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SolutionGLCodeList currentRow = bindingSource1.Current as SolutionGLCodeList;
            if (currentRow == null) return;
            if (currentRow.ID == Guid.Empty)
            {
                bindingSource1.RemoveCurrent();
                return;
            }

            SingleResultData result = ConfigureService.ChangeSolutionGLCodeState(currentRow.ID, !currentRow.IsValid, LocalData.UserInfo.LoginID, currentRow.UpdateDate);
            currentRow.IsValid = !currentRow.IsValid;
            currentRow.UpdateDate = result.UpdateDate;
            this.RefreshToolBars();

            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Change State Successfully" : "更改状态成功");

        }

        void RefreshToolBars()
        {
            SolutionGLCodeList currentRow = bindingSource1.Current as SolutionGLCodeList;
            if (currentRow == null)
            {
                this.barDisuse.Enabled = false;
            }
            else
            {
                this.barDisuse.Enabled = true;

                if (currentRow.ID == Guid.Empty)
                {
                    barDisuse.Caption = LocalData.IsEnglish ? "&Delete" : "删除(&D)";
                }
                else
                {
                    if (currentRow.IsValid)
                        barDisuse.Caption = LocalData.IsEnglish ? "&Disuse" : "作废(&D)";
                    else
                        barDisuse.Caption = LocalData.IsEnglish ? "Available(&D)" : "激活(&D)";
                }
            }

            if (this.bindingSource1.Count > 0)
            {
                this.barSave.Enabled = true;
            }
            else
            {
                this.barSave.Enabled = false;
            }

        }
        #endregion

        private void SolutionGLCodeListPart_Load(object sender, EventArgs e)
        { 
            List<SolutionGLGroupList> list = ConfigureService.GetSolutionGLGroupList(string.Empty, string.Empty, 0);

            repositoryItemLookUpEdit1.DataSource = list;
            repositoryItemLookUpEdit1.ValueMember = "ID";
            repositoryItemLookUpEdit1.DisplayMember = LocalData.IsEnglish ? "EName" : "CName";
        }

        private void barGo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.SelectedRowsCount <= 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("请选择要转移的科目", "Tips", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            List<SolutionGLGroupList> list = repositoryItemLookUpEdit1.DataSource as List<SolutionGLGroupList>;

            string gltext = LocalData.IsEnglish ? list.Find(r => r.ID == ((Guid)barGoList.EditValue)).EName : list.Find(r => r.ID == ((Guid)barGoList.EditValue)).CName;

            if (DevExpress.XtraEditors.XtraMessageBox.Show("确定要转移到科目：" + gltext, "Tips", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                List<Guid?> ids = new List<Guid?>();
                List<DateTime?> versions = new List<DateTime?>();
                List<string> codes = new List<string>(), cNames = new List<string>(), eNames = new List<string>(), descriptions = new List<string>();
                int[] selectRowsHandle = gridView1.GetSelectedRows();

                foreach (var row in selectRowsHandle)
                {
                    SolutionGLCodeList item = gridView1.GetRow(row) as SolutionGLCodeList;
                    ids.Add(item.ID);
                    versions.Add(item.UpdateDate);

                    descriptions.Add(item.Description);
                    codes.Add(item.Code);

                    cNames.Add(item.CName);
                    eNames.Add(item.EName);
                }
                try
                {
                    ManyResultData result = ConfigureService.SaveSolutionGLCodeInfo(_GLCodeParent.SolutionID
                                                                                    , ids.ToArray()
                                                                                    , (Guid)barGoList.EditValue
                                                                                    , codes.ToArray()
                                                                                    , cNames.ToArray()
                                                                                    , eNames.ToArray()
                                                                                    , descriptions.ToArray()
                                                                                    , LocalData.UserInfo.LoginID
                                                                                    , versions.ToArray());
                    List<SolutionGLCodeList> currentList = bindingSource1.DataSource as List<SolutionGLCodeList>;
                    currentList.RemoveAll(r => ids.Exists(j => j == r.ID));
                    bindingSource1.ResetBindings(false);
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
                }
            }
        }
    }
}


