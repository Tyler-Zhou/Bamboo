using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;

namespace ICP.Common.UI.Configure.EDIConfigure
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class EDICommpanyListEditPart : BaseListEditPart
    {
        #region 初始化

        public EDICommpanyListEditPart()
        {
            InitializeComponent();
            this.Enabled = false;
            if (LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate {
                this.commStyleFormatCondition = null;
                this.CurrentChanged = null;
                this.gcMain.MouseDown -= this.gcMain_MouseDown;
                this.gvMain.CellValueChanged -= this.gvMain_CellValueChanged;
                this.gvMain.CustomDrawRowIndicator -= this.mainGridView_CustomDrawRowIndicator;
                this.txtFind.ButtonClick -= this.txtFind_ButtonClick;
                this.txtFind.TextChanged -= this.txtFind_TextChanged;
                this._currentConfigureList = null;
                this._originalList = null;
                this.gcMain.DataSource = null;
                this.bsList.DataSource = null;
                this.bsList.Dispose();
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
            commStyleFormatCondition.Appearance.Font = new Font(colCompanyName.AppearanceCell.Font, FontStyle.Bold);
            commStyleFormatCondition.Appearance.BackColor = Color.Aqua;
            commStyleFormatCondition.Condition = DevExpress.XtraGrid.FormatConditionEnum.None;
            commStyleFormatCondition.ApplyToRow = true;

            #endregion
        }

        #endregion

        #region 控制器


        public EDIConfigureController Controller 
        {
            get
            {
                return ClientHelper.Get<EDIConfigureController, EDIConfigureController>();
            }
        }
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
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
                expressionBulider.Append("Upper([CompanyName]) Like '%");
                expressionBulider.Append(txtFind.Text.Trim().ToUpper());
                expressionBulider.Append("%'");

                commStyleFormatCondition.Expression = expressionBulider.ToString();
                commStyleFormatCondition.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;

                List<ConfigureListForEDI> list = bsList.DataSource as List<ConfigureListForEDI>;
                List<ConfigureListForEDI> tagers = list.FindAll(delegate(ConfigureListForEDI item)
                {
                    return item.CompanyName.ToUpper().Contains(txtFind.Text.Trim().ToUpper());
                });

                if (tagers == null || tagers.Count == 0) return;

                List<Guid> ids = new List<Guid>();
                foreach (var item in tagers) { ids.Add(item.ID); }

                for (int i = 0; i < gvMain.RowCount; i++)
                {
                    ConfigureListForEDI checkData = gvMain.GetRow(i) as ConfigureListForEDI;
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

        /*保存联系人*/
        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ValidateData() == false)
            {
                return;
            }

            DialogResult dlg = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Are you sure combine?" : "确认要选择这些公司?", LocalData.IsEnglish ? "Confirm" : "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dlg != DialogResult.OK)
            {
                return;
            }

            try
            {
                List<Guid> configureIDs = new List<Guid>();
                List<string> toAddress = new List<string>();
                List<string> csclWebURL = new List<string>();
                List<string> csclLoginName = new List<string>();
                List<string> csclPassword = new List<string>();
                List<string> companyAddressList = new List<string>();
                foreach (ConfigureListForEDI data in bsList.List)
                {
                    if (data.IsSelected)
                    {
                        configureIDs.Add(data.ID);
                        toAddress.Add(data.ToAddress);
                        csclWebURL.Add(data.CSCLWebURL);
                        csclLoginName.Add(data.CSCLLoginName);
                        csclPassword.Add(data.CSCLPassword);
                        companyAddressList.Add(data.CompanyAddress);
                    }
                }

                ManyResultData mans = this.Controller.SaveEDICompanyConfigureInfo(_currentConfigureList.ID
                    , configureIDs.ToArray()
                    , toAddress.ToArray()
                    ,csclWebURL.ToArray()
                    ,csclLoginName.ToArray()
                    ,csclPassword.ToArray()
                    ,companyAddressList.ToArray()
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
                List<ConfigureListForEDI> selectedList = _originalList.FindAll(delegate(ConfigureListForEDI item)
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

                List<ConfigureListForEDI> unSelectedList = _originalList.FindAll(delegate(ConfigureListForEDI item)
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
                ConfigureListForEDI checkData = gvMain.GetRow(i) as ConfigureListForEDI;
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
                ConfigureListForEDI checkData = gvMain.GetRow(i) as ConfigureListForEDI;
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
                ConfigureListForEDI checkData = gvMain.GetRow(i) as ConfigureListForEDI;
                checkData.IsSelected = false;
                _selectedID.Remove(checkData.ID);
            }
            gvMain.RefreshData();
        }

        #endregion

        private void gvMain_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0 || e.Column != colIsSelected) return;

            ConfigureListForEDI checkData = gvMain.GetRow(e.RowHandle) as ConfigureListForEDI;
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
                ConfigureListForEDI checkData = gvMain.GetRow(i) as ConfigureListForEDI;
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

        //List<ConfigureListClient> _originalList = new List<ConfigureListClient>();
        //private EDIConfigureList _currentConfigureList = null;
        //public override void Init(IDictionary<string, object> values)
        //{
        //    if (values != null && values.ContainsKey("EDIConfigureList"))
        //    {
        //        _currentConfigureList = (EDIConfigureList)values["EDIConfigureList"];
        //        if (_currentConfigureList == null)
        //        {
        //            this.Enabled = false;
        //            return;
        //        }
        //        else
        //        {
        //            this.Enabled = true;
        //        }

        //        List<ConfigureList> list = this.Controller.GetConfigureList(true);
        //        List<ConfigureListClient> clientList = new List<ConfigureListClient>();
        //        foreach (ConfigureList r in list)
        //        {
        //            ConfigureListClient tmp = new ConfigureListClient();
        //            ICP.Framework.CommonLibrary.Helper.ObjectHelper.CopyData(r, tmp);
        //            clientList.Add(tmp);
        //        }

        //        _originalList = clientList;
        //        bsList.DataSource = _originalList;
        //        bsList.ResetBindings(false);
        //    }
        //}
        List<ConfigureListForEDI> _originalList = new List<ConfigureListForEDI>();
        private EDIConfigureList _currentConfigureList = null;
        public override void Init(IDictionary<string, object> values)
        {
            if (values != null && values.ContainsKey("EDIConfigureList"))
            {
                _currentConfigureList = (EDIConfigureList)values["EDIConfigureList"];
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

                if (_originalList.Count == 0)
                {
                    //List<EDIConfigureList> edilistTotal = this.Controller.GetEDIConfigureList(null);
                    List<ConfigureList> configureListTotal = this.Controller.GetConfigureList(true);
                    foreach (ConfigureList r in configureListTotal)
                    {
                        ConfigureListForEDI tmp = new ConfigureListForEDI();
                        ICP.Framework.CommonLibrary.Helper.ObjectHelper.CopyData(r, tmp);
                        _originalList.Add(tmp);
                    }
                }
                else
                {
                    foreach (var originalItem in _originalList)
                    {
                        originalItem.IsSelected = false;
                    }
                }

                if (_currentConfigureList.ID != null && _currentConfigureList.ID != Guid.Empty)
                {
                    List<ConfigureListForEDI> companyByEDI = this.Controller.GetEDICompanyConfigureList(_currentConfigureList.ID);
                    if (companyByEDI != null)
                    {
                        for (int i = 0; i < companyByEDI.Count; i++)
                        {
                            ConfigureListForEDI item = _originalList.Find(delegate(ConfigureListForEDI t) { return t.ID == companyByEDI[i].ID; });
                            if (item != null)
                            {
                                item.IsSelected = true;
                                item.ToAddress = companyByEDI[i].ToAddress;
                                item.CSCLWebURL = companyByEDI[i].CSCLWebURL;
                                item.CSCLLoginName = companyByEDI[i].CSCLLoginName;
                                item.CSCLPassword = companyByEDI[i].CSCLPassword;
                                item.CompanyAddress = companyByEDI[i].CompanyAddress;
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


}
