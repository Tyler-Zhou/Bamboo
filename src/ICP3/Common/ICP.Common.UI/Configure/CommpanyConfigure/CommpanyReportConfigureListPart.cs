﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;

namespace ICP.Common.UI.Configure.CommpanyConfigure
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class CommpanyReportConfigureListPart : BaseListEditPart
    {
        #region 服务

        [ServiceDependency]
        public WorkItem WorkItem { get; set; }

        public CompanyConfigureController Controller
        {
            get
            {
                return ClientHelper.Get<CompanyConfigureController, CompanyConfigureController>();
            }
        }

        #endregion 

        #region 初始化

        public CommpanyReportConfigureListPart()
        {
            InitializeComponent();
            this.Enabled = false;
            if (LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate {
                this._currentConfigureList = null;
                this._originalList = null;
                this.CurrentChanged = null;
                this.gvMain.CellValueChanged -= this.gvMain_CellValueChanged;
                this.gcMain.MouseDown -= this.gcMain_MouseDown;
                this.gvMain.CustomDrawRowIndicator -= this.mainGridView_CustomDrawRowIndicator;
                this.gcMain.DataSource = null;
                this.bsList.DataSource = null;
                this.bsList.Dispose();
                this.txtFind.ButtonClick -= this.txtFind_ButtonClick;
                this.txtFind.TextChanged -= this.txtFind_TextChanged;
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            };
        }

        private void SetCnText()
        {
            btnNext.Text = "下一个(&N)";
            colIsSelected.Caption = "选择";        

            barAntiSelect.Caption = "反选";
            barSelectAll.Caption = "全选";
            barUnSelectAll.Caption = "全部取消";

            chkbtnAll.ToolTip = "全部";
            chkbtnChecked .ToolTip = "已选";
            chkbtnUnChecked.ToolTip = "未选";
            chkbtnHold.ToolTip = "保留";
        }


        DevExpress.XtraGrid.StyleFormatCondition commStyleFormatCondition = new DevExpress.XtraGrid.StyleFormatCondition();
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            barSave.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Save_16;
            #region Finder Style

            gvMain.FormatConditions.Add(commStyleFormatCondition);
            commStyleFormatCondition.Appearance.Font = new Font(colEDescription.AppearanceCell.Font, FontStyle.Bold);
            commStyleFormatCondition.Appearance.BackColor = Color.Aqua;
            commStyleFormatCondition.Condition = DevExpress.XtraGrid.FormatConditionEnum.None;
            commStyleFormatCondition.ApplyToRow = true;

            #endregion
        }

        #endregion   

        #region Fast Find

        List<int> finderIndexs = new List<int>();
        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFind.Text.Trim()))
            {
                commStyleFormatCondition.Condition = DevExpress.XtraGrid.FormatConditionEnum.None;
                finderIndexs.Clear();
            }
            else
            {
                finderIndexs.Clear();            
                StringBuilder expressionBulider = new StringBuilder();
                expressionBulider.Append("Upper([CName]) Like '%");
                expressionBulider.Append(txtFind.Text.Trim().ToUpper());
                expressionBulider.Append("%' or Upper([EName]) Like '%");
                expressionBulider.Append(txtFind.Text.Trim().ToUpper());
                expressionBulider.Append("%'");

                commStyleFormatCondition.Expression = expressionBulider.ToString();
                commStyleFormatCondition.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;

                List<CompanyReportConfigureListClient> list = bsList.DataSource as List<CompanyReportConfigureListClient>;
                List<CompanyReportConfigureListClient> tagers = list.FindAll(delegate(CompanyReportConfigureListClient item)
                {
                    return item.Code.ToUpper().Contains(txtFind.Text.Trim().ToUpper())
                        || item.CDescription.ToUpper().Contains(txtFind.Text.Trim().ToUpper())
                        || item.EDescription.ToUpper().Contains(txtFind.Text.Trim().ToUpper());
                });

                if (tagers == null || tagers.Count == 0) return;

                List<Guid> ids = new List<Guid>();
                foreach (var item in tagers) { ids.Add(item.ID); }

                for (int i = 0; i < gvMain.RowCount; i++)
                {
                    CompanyReportConfigureListClient checkData = gvMain.GetRow(i) as CompanyReportConfigureListClient;
                    if (ids.Contains(checkData.ID)) finderIndexs.Add(i);
                }

                Next();
            }
        }

        private void Next()
        {
            if (finderIndexs == null || finderIndexs.Count == 0) return;

            int currentFoudIndex = -1;//当前行Index
            currentFoudIndex = gvMain.FocusedRowHandle;
            if (currentFoudIndex < 0) currentFoudIndex = 0;

            int needFocusedIndex = -1;
            if (finderIndexs.Contains(currentFoudIndex) == false)
            {
                needFocusedIndex = finderIndexs[0];
            }
            else
            {
                int tempIndex = finderIndexs.IndexOf(currentFoudIndex);
                if (tempIndex < 0 || tempIndex == finderIndexs.Count - 1) needFocusedIndex = finderIndexs[0];
                else needFocusedIndex = finderIndexs[tempIndex + 1];
            }

            if (needFocusedIndex < 0) return;

            gvMain.FocusedRowHandle = needFocusedIndex;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Next();
        }

        private void barSet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {           
            CompanyReportConfigureListClient itemSelectd = bsList.Current as CompanyReportConfigureListClient;
            if (itemSelectd != null)
            {
                SetReportParameterForm setParameterForm = WorkItem.Items.AddNew<SetReportParameterForm>();
                if (!itemSelectd.IsSelected)
                {
                    itemSelectd.IsSelected = true;
                }

                setParameterForm.CurrentReportConfigure = itemSelectd;
                setParameterForm.Text = "设置报表参数值";
                DialogResult dlg = setParameterForm.ShowDialog();
            }
            //foreach (CompanyReportConfigureListClient item in bsList.List)
            //{
            //    if (item.ID == setParameterForm.CurrentReportConfigure.ID)
            //    {
            //        item.Parameters = setParameterForm.CurrentReportConfigure.Parameters;
            //        item.UpdateDate = setParameterForm.CurrentReportConfigure.UpdateDate;
            //        break;
            //    }
            //}
        }

        /*保存*/
        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ValidateData() == false)
            {
                return;
            }

            DialogResult dlg = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Are you sure combine?" : "确认要选择这些报表配置?", LocalData.IsEnglish ? "Confirm" : "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dlg != DialogResult.OK)
            {
                return;
            }

            try
            {
                //ReportParameterList[][] parameterLists = new ReportParameterList[bsList.List.Count][];
                //foreach (CompanyReportConfigureListClient data in bsList.List)
                //{
                //    if (data.IsSelected)
                //    {
                //        reportConfigureIDs.Add(data.ConfigureID);
                //    }
                //}

                List<ReportParameterList> collect = new List<ReportParameterList>();

                for(int i=0; i< bsList.List.Count; i ++)
                {
                    CompanyReportConfigureListClient reportItem = bsList.List[i] as CompanyReportConfigureListClient;

                    if (reportItem.IsSelected)
                    {
                        if (reportItem.Parameters != null && reportItem.Parameters.Count > 0)
                        {
                            foreach (var parameter in reportItem.Parameters)
                            {
                                parameter.ReportID = reportItem.ID;
                            }
                        }

                        collect.AddRange(reportItem.Parameters);
                    }
                }

                this.Controller.SaveCompanyReportConfigureInfo(_currentConfigureList.ID
                    , collect
                    , LocalData.UserInfo.LoginID);

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

        #endregion

        /*合并前数据验证*/
        private bool ValidateData()
        {
            EndEdit();

            //if (this.bsDataSource.Count < 2)
            //{
            //    //this._statusBar.SetStatusBarPanel1(Utility.IsEnglish ? "Must have to have two or two above customers can merge" : "必须要有两个或两个以上的客户才能合并");
            //    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(
            //      this.FindForm(),
            //      "必须要有两个或两个以上的客户才能合并!");
            //    return false;
            //}

            //if ((bdsCustomerContact.List as List<PublicCustomerListData>).FindAll(delegate(PublicCustomerListData sourceitem) { return sourceitem.Selected == true; }).Count != 1)
            //{//UNDONE 翻译
            //    MessageBox.Show(Utility.IsEnglish ? "Has also only can have a retention customer!" : "有且只能有一个保留客户!", Utility.IsEnglish ? "Tip" : "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}

            return true;
        }

        #region Click View Style

        ViewStyle _viewStyle = ViewStyle.All;
        private void chkbtnAll_Click(object sender, EventArgs e)
        {
            SetListViewStyle(ViewStyle.All);
        }

        private void chkbtnUnChecked_Click(object sender, EventArgs e)
        {
            SetListViewStyle(ViewStyle.UnSelected);
        }

        private void chkbtnChecked_Click(object sender, EventArgs e)
        {
            SetListViewStyle(ViewStyle.Selected);
        }

        void SetListViewStyle(ViewStyle viewStyle)
        {
            _viewStyle = viewStyle;
            if (viewStyle == ViewStyle.All)
            {
                chkbtnAll.Checked = true;
                chkbtnChecked.Checked = chkbtnUnChecked.Checked = false;
                bsList.DataSource = _originalList;
                bsList.ResetBindings(false);
            }
            else if (viewStyle == ViewStyle.Selected)
            {
                chkbtnChecked.Checked = true;
                chkbtnAll.Checked = chkbtnUnChecked.Checked = false;
                List<CompanyReportConfigureListClient> selectedList = _originalList.FindAll(delegate(CompanyReportConfigureListClient item)
                {
                    return _selectedID.Contains(item.ID);
                });
                bsList.DataSource = selectedList;
                bsList.ResetBindings(false);
            }
            else
            {
                chkbtnUnChecked.Checked = true;
                chkbtnChecked.Checked = chkbtnAll.Checked = false;

                List<CompanyReportConfigureListClient> unSelectedList = _originalList.FindAll(delegate(CompanyReportConfigureListClient item)
                {
                    return _selectedID.Contains(item.ID) == false;
                });

                bsList.DataSource = unSelectedList;
                bsList.ResetBindings(false);
            }
            RefreshChecked();

        }

        #endregion

        #region 

        #region pop

        private void gcMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) popupMenu1.ShowPopup(MousePosition);
        }

        private void barAntiSelect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            for (int i = 0; i < gvMain.RowCount; i++)
            {
                CompanyReportConfigureListClient checkData = gvMain.GetRow(i) as CompanyReportConfigureListClient;
                if (checkData == null) continue;

                checkData.IsSelected = !checkData.IsSelected;

                if (checkData.IsSelected == false)
                    _selectedID.Remove(checkData.ID);
                else if (_selectedID.Contains(checkData.ID) == false)
                    _selectedID.Add(checkData.ID);
            }
            gvMain.RefreshData();
        }

        private void barSelectAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            for (int i = 0; i < gvMain.RowCount; i++)
            {
                CompanyReportConfigureListClient checkData = gvMain.GetRow(i) as CompanyReportConfigureListClient;
                if (checkData == null) continue;

                checkData.IsSelected = true;

                if (_selectedID.Contains(checkData.ID) == false)
                    _selectedID.Add(checkData.ID);
            }
            gvMain.RefreshData();
        }

        private void barUnSelectAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            for (int i = 0; i < gvMain.RowCount; i++)
            {
                CompanyReportConfigureListClient checkData = gvMain.GetRow(i) as CompanyReportConfigureListClient;
                checkData.IsSelected = false;
                _selectedID.Remove(checkData.ID);
            }
            gvMain.RefreshData();
        }

        #endregion

        private void gvMain_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0 || e.Column != colIsSelected) return;

            CompanyReportConfigureListClient checkData = gvMain.GetRow(e.RowHandle) as CompanyReportConfigureListClient;
            if (checkData == null) return;

            if (checkData.IsSelected)
            { if (_selectedID.Contains(checkData.ID) == false) _selectedID.Add(checkData.ID); }
            else
            { _selectedID.Remove(checkData.ID); }
        }

        private void mainGridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }

        private void txtFind_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            txtFind.Text = string.Empty;
        }

        bool _hold = false;
        private void chkbtnHold_Click(object sender, EventArgs e)
        {
            _hold = !_hold;
        }

        #endregion

        #region interface

        List<Guid> _selectedID = new List<Guid>();

        public override object DataSource
        {
            get
            {
                this.Validate();
                return _selectedID;
            }
            set
            {
                if (value == null)
                    _selectedID = new List<Guid>();
                else
                    _selectedID = value as List<Guid>;

                if (_hold == false) txtFind.Text = string.Empty;

                SetListViewStyle(_viewStyle);

                if (CurrentChanged != null) CurrentChanged(this, Current);
            }
        }

        private void RefreshChecked()
        {
            for (int i = 0; i < gvMain.RowCount; i++)
            {
                CompanyReportConfigureListClient checkData = gvMain.GetRow(i) as CompanyReportConfigureListClient;
                if (checkData == null) continue;

                if (_selectedID.Contains(checkData.ID)) checkData.IsSelected = true;
                else checkData.IsSelected = false;
            }
            gvMain.RefreshData();
        }

        public override object Current
        {
            get
            {
                return bsList.Current;
            }
        }

        public override event CurrentChangedHandler CurrentChanged;      

        public override void EndEdit()
        {
            this.Validate();
            bsList.EndEdit();
        }

        List<CompanyReportConfigureListClient> _originalList = new List<CompanyReportConfigureListClient>();
        private ConfigureList _currentConfigureList = null;
        public override void Init(IDictionary<string, object> values)
        {
            if (values != null && values.ContainsKey("ConfigureList"))
            {
                _currentConfigureList = (ConfigureList)values["ConfigureList"];
                if (_currentConfigureList == null)
                {
                    this.Enabled = false;
                    return;
                }
                else
                {
                    if (!_currentConfigureList.IsValid)
                    {
                        this.Enabled = false;
                    }
                    else
                    {
                        this.Enabled = true;
                    }
                }

                _originalList = new List<CompanyReportConfigureListClient>();
                List<ReportConfigureList> reportlistTotal = this.Controller.GetReportConfigureList(true);
                foreach (ReportConfigureList r in reportlistTotal)
                {
                    CompanyReportConfigureListClient tmp = new CompanyReportConfigureListClient();
                    ICP.Framework.CommonLibrary.Helper.ObjectHelper.CopyData(r, tmp);
                    //tmp.IsSelected = false;
                    _originalList.Add(tmp);
                }
               
                if (_currentConfigureList.ID != null && _currentConfigureList.ID != Guid.Empty)
                {
                    List<CompanyReportConfigureList> reportlistByCompany = this.Controller.GetCompanyReportConfigureList(_currentConfigureList.ID, true);
                    if (reportlistByCompany != null)
                    {
                        for (int i = 0; i < reportlistByCompany.Count; i++)
                        {
                            CompanyReportConfigureListClient item = _originalList.Find(delegate(CompanyReportConfigureListClient t) { return t.ID == reportlistByCompany[i].ID; });
                            if (item != null)
                            {
                                item.IsSelected = true;
                                ////item.Parameters = reportlistByCompany[i].Parameters;
                                foreach (var p in item.Parameters)
                                {
                                    foreach (var parameter in reportlistByCompany[i].Parameters)
                                    {
                                        if (p.ID == parameter.ID)
                                        {
                                            p.ParameterValue = parameter.ParameterValue;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                bsList.DataSource = _originalList;
                bsList.ResetBindings(false);
            }
        }
     
        #endregion     

    }

    public class CompanyReportConfigureListClient : CompanyReportConfigureList
    {
        public bool IsSelected { get; set; }
        //public Guid EDIConfigureID { get; set; }
    }
}
