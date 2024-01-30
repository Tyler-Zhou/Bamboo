using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ICP.Sys.ServiceInterface.DataObjects;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.StyleFormatConditions;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.Sys.UI.Job
{
    public partial class JobOrgSelectPart : BaseListPart
    {
        #region 初始化

        public JobOrgSelectPart()
        {
            InitializeComponent();
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate {
                this._originalList = null;
                this.txtFind.ButtonClick -= this.txtFind_ButtonClick;
                this.txtFind.TextChanged -= this.txtFind_TextChanged;
                this.treeMain.DataSource = null;
                this.treeMain.AfterCheckNode -= this.treeMain_AfterCheckNode;
                this.treeMain.BeforeCheckNode -= this.treeMain_BeforeCheckNode;
                this.treeMain.GetStateImage -= this.treeMain_GetStateImage;
                this.treeMain.MouseDown -= this.treeMain_MouseDown;
                this.treeMain.MouseMove -= this.treeMain_MouseMove;

                this.bsList.PositionChanged -= this.bsList_PositionChanged;
                this.bsList.DataSource = null;
                this.bsList.Dispose();
            
            };
        }

        private void SetCnText()
        {
            btnNext.Text = "下一个(&N)";

            barAntiSelect.Caption = "反选";
            barSelectAll.Caption = "全选";
            barUnSelectAll.Caption = "全部取消";

            chkbtnAll.ToolTip = "全部";
            chkbtnChecked.ToolTip = "已选";
            chkbtnUnChecked.ToolTip = "未选";
            chkbtnHold.ToolTip = "保留";
        }

        StyleFormatCondition commStyleFormatCondition = new StyleFormatCondition();
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                #region Finder Style

                treeMain.FormatConditions.Add(commStyleFormatCondition);

                commStyleFormatCondition.Appearance.Font = new Font(colName.AppearanceCell.Font, FontStyle.Bold);
                commStyleFormatCondition.Appearance.BackColor = Color.Aqua;
                commStyleFormatCondition.Condition = DevExpress.XtraGrid.FormatConditionEnum.None;
                commStyleFormatCondition.ApplyToRow = false;
                commStyleFormatCondition.Column = colName;

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
                commStyleFormatCondition.Expression = "Upper([Name]) Like '%" + txtFind.Text.Trim().ToUpper() + "%'";
                commStyleFormatCondition.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;

                List<Organization2JobList> list = bsList.DataSource as List<Organization2JobList>;
                List<Organization2JobList> tagers = list.FindAll(delegate(Organization2JobList item)
                                                    {
                                                        return item.Type == OrganizationJobType.Job
                                                        && item.Name.ToUpper().Contains(txtFind.Text.Trim().ToUpper());
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
            }
            else if (viewStyle == ViewStyle.Selected)
            {
                chkbtnChecked.Checked = true;
                chkbtnAll.Checked = chkbtnUnChecked.Checked = false;
                List<Organization2JobList> selectedList = _originalList.FindAll(delegate(Organization2JobList item)
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
                List<Organization2JobList> unSelectedList = _originalList.FindAll(delegate(Organization2JobList item)
                {
                    return _selectedID.Contains(item.ID) == false;
                });
                bsList.DataSource = unSelectedList;
                bsList.ResetBindings(false);
            }

            RefreshChecked();
            //treeMain.HorzScrollVisibility = ScrollVisibility.Never;
            treeMain.ExpandAll();
            //treeMain.HorzScrollVisibility = ScrollVisibility.Auto;

            //this.Scroll 

        }

        #endregion

        #region

        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        private void treeMain_GetStateImage(object sender, GetStateImageEventArgs e)
        {
            Organization2JobList organization2Job = treeMain.GetDataRecordByNode(e.Node) as Organization2JobList;

            if (organization2Job == null) return;

            if (_defaultID != null && organization2Job.ID == _defaultID)
                e.Node.StateImageIndex = 2;
            else
                e.Node.StateImageIndex = (short)organization2Job.Type;
        }

        private void treeMain_BeforeCheckNode(object sender, CheckNodeEventArgs e)
        {
            Organization2JobList checkData = treeMain.GetDataRecordByNode(e.Node) as Organization2JobList;
            if (checkData == null || checkData.Type == OrganizationJobType.Organization)
            {
                e.CanCheck = false;
                return;
            }
        }

        private void treeMain_AfterCheckNode(object sender, NodeEventArgs e)
        {
            Organization2JobList checkData = treeMain.GetDataRecordByNode(e.Node) as Organization2JobList;
            if (e.Node.Checked == true)
            { if (_selectedID.Contains(checkData.ID) == false) _selectedID.Add(checkData.ID); }
            else
                _selectedID.Remove(checkData.ID);

            treeMain.FocusedNode = e.Node;
        }

        private void treeMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
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

                Organization2JobList checkData = treeMain.GetDataRecordByNode(tn) as Organization2JobList;
                if (checkData == null || checkData.Type == OrganizationJobType.Organization) continue;

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

                Organization2JobList checkData = treeMain.GetDataRecordByNode(tn) as Organization2JobList;
                if (checkData == null || checkData.Type == OrganizationJobType.Organization) continue;

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

                Organization2JobList checkData = treeMain.GetDataRecordByNode(tn) as Organization2JobList;
                if (checkData == null || checkData.Type == OrganizationJobType.Organization) continue;

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
            for (int i = 0; i < treeMain.AllNodesCount; i++)
            {
                TreeListNode tn = treeMain.GetNodeByVisibleIndex(i);
                if (tn == null) continue;

                Organization2JobList checkData = treeMain.GetDataRecordByNode(tn) as Organization2JobList;
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

        List<Organization2JobList> _originalList = new List<Organization2JobList>();
        Guid? _defaultID = null;

        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key == "OriginalList")
                {
                    _originalList = item.Value as List<Organization2JobList>;
                    bsList.DataSource = _originalList;
                    treeMain.ExpandAll();
                }
                else if (item.Key == "DefaultID")
                {
                    if (item.Value == null) _defaultID = null;
                    else
                    {
                        _defaultID = new Guid(item.Value.ToString());
                    }
                    this.treeMain.RefreshDataSource();
                }
            }
        }

        #endregion

        private void treeMain_MouseMove(object sender, MouseEventArgs e)
        {
            TreeListHitInfo hitInfo = treeMain.CalcHitInfo(e.Location);

            Organization2JobList checkData = treeMain.GetDataRecordByNode(hitInfo.Node) as Organization2JobList;
            if (checkData == null)
            {
                return;
            }

            string s = toolTip1.GetToolTip(this);

            if (s==checkData.StructureFullName)
            {
                return;
            }

            Point pt = treeMain.PointToClient(MousePosition);
            pt.X += 20;
            pt.Y += 30;
            this.toolTip1.Show(checkData.StructureFullName, this, pt, 5000);
        }

    }
}
