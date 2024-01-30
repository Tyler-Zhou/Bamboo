using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ICP.Sys.ServiceInterface.DataObjects;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.StyleFormatConditions;
using ICP.Framework.ClientComponents.UIFramework;


namespace ICP.Sys.UI.PermissionManage.Function
{
    public partial class FunctionSelectPart : BaseListPart
    {
        #region 初始化

        public FunctionSelectPart()
        {
            InitializeComponent();
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate
            {
                this._originalList = null;
                this.treeMain.DataSource = null;
                this.treeMain.AfterCheckNode -= this.treeMain_AfterCheckNode;
                this.treeMain.BeforeCheckNode -= this.treeMain_BeforeCheckNode;
                this.treeMain.GetStateImage -= this.treeMain_GetStateImage;
                this.treeMain.MouseDown -= this.treeMain_MouseDown;
                this.txtFind.ButtonClick -= this.txtFind_ButtonClick;
                this.txtFind.TextChanged -= this.txtFind_TextChanged;
                this.bsList.DataSource = null;
                this.bsList.Dispose();
                this.CurrentChanged = null;
            };
        }

        private void SetCnText()
        {
            btnNext.Text = "下一个(&N)";
            colCName.Visible = true;
            colEName.Visible = false;

            barAntiSelect.Caption = "反选";
            barSelectAll.Caption = "全选";
            barUnSelectAll.Caption = "全部取消";

            chkbtnAll.ToolTip = "全部";
            chkbtnChecked.ToolTip = "已选";
            chkbtnUnChecked.ToolTip = "未选";
            chkbtnHold.ToolTip = "保留";
        }

        private void treeMain_GetStateImage(object sender, GetStateImageEventArgs e)
        {
            FunctionList data = treeMain.GetDataRecordByNode(e.Node) as FunctionList;
            if (data == null) return;
            e.Node.StateImageIndex = (short)data.FunctionType;
        }

        StyleFormatCondition commStyleFormatCondition = new StyleFormatCondition();
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                #region Finder Style

                treeMain.FormatConditions.Add(commStyleFormatCondition);

                commStyleFormatCondition.Appearance.Font = new Font(colCName.AppearanceCell.Font, FontStyle.Bold);
                commStyleFormatCondition.Appearance.BackColor = Color.Aqua;
                commStyleFormatCondition.Condition = DevExpress.XtraGrid.FormatConditionEnum.None;
                commStyleFormatCondition.ApplyToRow = false;
                commStyleFormatCondition.Column = colCName;

                #endregion

                Utility.ShowTreeRowNo(this.treeMain);
            }
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

                List<FunctionList> list = bsList.DataSource as List<FunctionList>;
                List<FunctionList> tagers = list.FindAll(delegate(FunctionList item)
                {
                    return item.FunctionType !=  FunctionType.Module
                    && (item.EName.ToUpper().Contains(txtFind.Text.Trim().ToUpper())||item.CName.ToUpper().Contains(txtFind.Text.Trim().ToUpper()));
                });

                if (tagers == null || tagers.Count == 0) return;

                foreach (var item in tagers)
                {
                    TreeListNode tn = treeMain.FindNodeByFieldValue("ID", item.ID);
                    if (tn != null) finderIndexs.Add(tn.Id);
                }
                Next();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Next();
        }
        private void Next()
        {
            if (finderIndexs == null || finderIndexs.Count == 0) return;

            int currentFoudIndex = -1;//当前行Index
            if (treeMain.FocusedNode != null) currentFoudIndex = treeMain.FocusedNode.Id;
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

            TreeListNode tn = treeMain.FindNodeByID(needFocusedIndex);
            if (tn != null) treeMain.SetFocusedNode(tn);
        }
        #endregion

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
                treeMain.ExpandAll();
            }
            else if (viewStyle == ViewStyle.Selected)
            {
                chkbtnChecked.Checked = true;
                chkbtnAll.Checked = chkbtnUnChecked.Checked = false;
                List<FunctionList> selectedList = _originalList.FindAll(delegate(FunctionList item)
                {
                    return _selectedID.Contains(item.ID);
                });
                bsList.DataSource = selectedList;
                bsList.ResetBindings(false);
                treeMain.ExpandAll();
            }
            else
            {
                chkbtnUnChecked.Checked = true;
                chkbtnChecked.Checked = chkbtnAll.Checked = false;

                List<FunctionList> unSelectedList = _originalList.FindAll(delegate(FunctionList item)
                {
                    return _selectedID.Contains(item.ID) == false;
                });

                bsList.DataSource = unSelectedList;
                bsList.ResetBindings(false);
                treeMain.ExpandAll();
            }
            RefreshChecked();

        }

        #endregion

        #region

        private void treeMain_BeforeCheckNode(object sender, CheckNodeEventArgs e)
        {
             if (_CanCheck == false) 
             {
                 e.CanCheck = false;
                 return;
             }

            FunctionList checkData = treeMain.GetDataRecordByNode(e.Node) as FunctionList;
            if (checkData == null || checkData.FunctionType == FunctionType.Module)
            {
                e.CanCheck = false;
                return;
            }
        }

        private void treeMain_AfterCheckNode(object sender, NodeEventArgs e)
        {
            FunctionList checkData = treeMain.GetDataRecordByNode(e.Node) as FunctionList;
            if (e.Node.Checked == true)
            {
                if (_selectedID.Contains(checkData.ID) == false) _selectedID.Add(checkData.ID);
            }
            else
                _selectedID.Remove(checkData.ID);

            treeMain.FocusedNode = e.Node;
        }

        private void treeMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (_CanCheck && e.Button == MouseButtons.Right)
            {
                popupMenu1.ShowPopup(MousePosition);
            }
        }

        private void barAntiSelect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            for (int i = 0; i < treeMain.AllNodesCount; i++)
            {
                TreeListNode tn = treeMain.GetNodeByVisibleIndex(i);
                if (tn == null) continue;

                FunctionList checkData = treeMain.GetDataRecordByNode(tn) as FunctionList;
                if (checkData == null || checkData.FunctionType == FunctionType.Module) continue;

                tn.Checked = !tn.Checked;
                if (tn.Checked == false)
                    _selectedID.Remove(checkData.ID);
                else if (_selectedID.Contains(checkData.ID) == false)
                    _selectedID.Add(checkData.ID);
            }
        }

        private void barSelectAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            for (int i = 0; i < treeMain.AllNodesCount; i++)
            {
                TreeListNode tn = treeMain.GetNodeByVisibleIndex(i);
                if (tn == null) continue;

                FunctionList checkData = treeMain.GetDataRecordByNode(tn) as FunctionList;
                if (checkData == null || checkData.FunctionType == FunctionType.Module) continue;

                tn.Checked = true;

                if (_selectedID.Contains(checkData.ID) == false)
                    _selectedID.Add(checkData.ID);
            }
        }

        private void barUnSelectAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            for (int i = 0; i < treeMain.AllNodesCount; i++)
            {
                TreeListNode tn = treeMain.GetNodeByVisibleIndex(i);
                if (tn == null) continue;

                FunctionList checkData = treeMain.GetDataRecordByNode(tn) as FunctionList;
                if (checkData == null || checkData.FunctionType == FunctionType.Module) continue;

                tn.Checked = false;
                _selectedID.Remove(checkData.ID);
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

                Dictionary<Guid,FunctionType> selection = new Dictionary<Guid,FunctionType>();
                List<FunctionList> list = _originalList.FindAll(delegate(FunctionList item) { return _selectedID.Contains(item.ID); });
                if (list != null && list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        selection.Add(item.ID, item.FunctionType);
                    }
                }

                return selection;
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
            for (int i = 0; i < treeMain.AllNodesCount; i++)
            {
                TreeListNode tn = treeMain.GetNodeByVisibleIndex(i);
                if (tn == null) continue;

                FunctionList checkData = treeMain.GetDataRecordByNode(tn) as FunctionList;
                if (checkData == null) continue;

                if (_selectedID.Contains(checkData.ID)) tn.Checked = true;
                else tn.Checked = false;
            }
        }

        public override object Current
        {
            get
            {
                return bsList.Current;
            }
        }

        public override event CurrentChangedHandler CurrentChanged;

        List<FunctionList> _originalList = new List<FunctionList>();
        bool _CanCheck = true;
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key == "OriginalList")
                {
                    _originalList = item.Value as List<FunctionList>;
                    bsList.DataSource = _originalList;
                    treeMain.ExpandAll();
                }
                else if (item.Key == "CanCheck")
                {
                    _CanCheck = bool.Parse(item.Value.ToString());
                }

            }
        }

        #endregion

    }
}
