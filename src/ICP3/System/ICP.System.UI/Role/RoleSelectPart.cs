using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;

namespace ICP.Sys.UI.Role
{
    public partial class RoleSelectPart : BaseListPart
    {   
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        #region 初始化

        public RoleSelectPart()
        {
            InitializeComponent();
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate {
                this._originalList = null;
                this.gcMain.MouseDown -= this.gcMain_MouseDown;
               
                this.gcMain.DataSource = null;
                this.gvMain.CellValueChanged -= this.gvMain_CellValueChanged;
                this.txtFind.ButtonClick -= this.txtFind_ButtonClick;
                this.txtFind.TextChanged -= this.txtFind_TextChanged;
                this.bsList.DataSource = null;
                this.bsList.Dispose();
                this.CurrentChanged = null;
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

            colSelect.Caption = "选择";
            colEName.Caption = "英文名";
            colCName.Caption = "中文名";
            colDescription.Caption = "描述";

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

            #region Finder Style

            gvMain.FormatConditions.Add(commStyleFormatCondition);
            commStyleFormatCondition.Appearance.Font = new Font(colEName.AppearanceCell.Font, FontStyle.Bold);
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

                List<RoleListClient> list = bsList.DataSource as List<RoleListClient>;
                List<RoleListClient> tagers = list.FindAll(delegate(RoleListClient item)
                {
                    return item.CName.ToUpper().Contains(txtFind.Text.Trim().ToUpper())
                        || item.EName.ToUpper().Contains(txtFind.Text.Trim().ToUpper());
                });

                if (tagers == null || tagers.Count == 0) return;

                List<Guid> ids = new List<Guid>();
                foreach (var item in tagers) { ids.Add(item.ID); }

                for (int i = 0; i < gvMain.RowCount; i++)
                {
                    RoleListClient checkData = gvMain.GetRow(i) as RoleListClient;
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
                List<RoleListClient> selectedList = _originalList.FindAll(delegate(RoleListClient item)
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

                List<RoleListClient> unSelectedList = _originalList.FindAll(delegate(RoleListClient item)
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
                RoleListClient checkData = gvMain.GetRow(i) as RoleListClient;
                if (checkData == null) continue;

                checkData.Selected = !checkData.Selected;

                if (checkData.Selected == false)
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
                RoleListClient checkData = gvMain.GetRow(i) as RoleListClient;
                if (checkData == null) continue;

                checkData.Selected = true;

                if (_selectedID.Contains(checkData.ID) == false)
                    _selectedID.Add(checkData.ID);
            }
            gvMain.RefreshData();
        }

        private void barUnSelectAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            for (int i = 0; i < gvMain.RowCount; i++)
            {
                RoleListClient checkData = gvMain.GetRow(i) as RoleListClient;
                checkData.Selected = false;
                _selectedID.Remove(checkData.ID);
            }
            gvMain.RefreshData();
        }

        #endregion

        private void gvMain_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0 || e.Column != colSelect) return;

            RoleListClient checkData = gvMain.GetRow(e.RowHandle) as RoleListClient;
            if (checkData == null) return;

            if (checkData.Selected)
            { if (_selectedID.Contains(checkData.ID) == false) _selectedID.Add(checkData.ID); }
            else
            { _selectedID.Remove(checkData.ID); }
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
                RoleListClient checkData = gvMain.GetRow(i) as RoleListClient;
                if (checkData == null) continue;

                if (_selectedID.Contains(checkData.ID)) checkData.Selected = true;
                else checkData.Selected = false;
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

        List<RoleListClient> _originalList = new List<RoleListClient>();
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key == "OriginalList")
                {
                    List<RoleList> lists = item.Value as List<RoleList>;
                    List<RoleListClient> roleListClients = new List<RoleListClient>();

                    foreach (RoleList r in lists)
                    {
                        RoleListClient tmp = new RoleListClient();
                        Utility.CopyToValue(r,tmp,typeof(RoleList));
                        roleListClients.Add(tmp);
                    }

                    _originalList = roleListClients;
                    bsList.DataSource = _originalList;
                    bsList.ResetBindings(false);
                }
            }
        }

        #endregion

        private void gvMain_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            RoleListClient list =gvMain.GetRow(e.RowHandle) as RoleListClient;
            if (list == null) return;

            Font font = e.Appearance.Font;

            if (list.Selected)
            {
                e.Appearance.ForeColor = Color.Red;
            }
            else
            {
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void gvMain_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            //if (e.Column == this.colSelect)
            //{
            //    RoleListClient data = bsList.Current as RoleListClient;
            //    if (data != null)
            //    {
            //        data.Selected = !data.Selected;
            //        bsList.ResetCurrentItem();
            //    }
            //}
        }

    }

    public class RoleListClient : RoleList
    {
        public bool Selected { get; set; }
    }
}
