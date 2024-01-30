using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface.DataObjects;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.StyleFormatConditions;

namespace ICP.Sys.UI.Job.Finder
{
    [ToolboxItem(false)]
    public partial class OrganizationJobMiniFinderListPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region 服务注入

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region 初始化

        public OrganizationJobMiniFinderListPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                Utility.RemoveSetControlKeyEnterToClickButton(new List<Control> { this.txtFind }, this.OnKeyDownHandle);
                this.treeMain.DataSource = null;
                this.treeMain.GetStateImage -= this.treeMain_GetStateImage;
                this.treeMain.KeyDown -= this.gvMain_KeyDown;
                this.treeMain.MouseDoubleClick -= this.treeMain_MouseDoubleClick;
                this.txtFind.TextChanged -= this.txtFind_TextChanged;
                this.bsList.DataSource = null;
                this.bsList.Dispose();
                this.commStyleFormatCondition = null;
                this.Selected = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        {
            btnConfirm.Text="确定(&O)";
            btnNext.Text = "下一个(&N)";
        }

        private void treeMain_GetStateImage(object sender, GetStateImageEventArgs e)
        {
            Organization2JobList data = treeMain.GetDataRecordByNode(e.Node) as Organization2JobList;
            if (data == null) return;
            e.Node.StateImageIndex = (short)data.Type;
        }

        StyleFormatCondition commStyleFormatCondition = new StyleFormatCondition();
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Utility.SetControlKeyEnterToClickButton(new List<Control> { this.txtFind }, this.btnConfirm, this.OnKeyDownHandle);
            #region Finder Style

            treeMain.FormatConditions.Add(commStyleFormatCondition);

            commStyleFormatCondition.Appearance.Font = new Font(colName.AppearanceCell.Font, FontStyle.Bold);
            commStyleFormatCondition.Appearance.BackColor = Color.Aqua;
            commStyleFormatCondition.Condition = DevExpress.XtraGrid.FormatConditionEnum.None;
            commStyleFormatCondition.ApplyToRow = false;
            commStyleFormatCondition.Column = colName;

            #endregion
        }
        private void OnKeyDownHandle(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                this.btnConfirm.PerformClick();
            }
           
        }

        #endregion

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }
        protected Organization2JobList CurrentRow
        {
            get { return Current as Organization2JobList; }
        }

        public override object DataSource
        {
            get { return bsList.DataSource; }
            set
            {
                bsList.DataSource = value;
                bsList.ResetBindings(false);
                treeMain.ExpandAll();
            }
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SelectedHandler Selected;

        public override void Clear() { txtFind.Text = string.Empty; }

        #endregion

        #region Event

      
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

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Confirm();
        }

        private void treeMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Confirm();
        }


        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) Confirm();
        }

        protected virtual void Confirm()
        {
            if (CurrentRow == null || CurrentRow.Type == OrganizationJobType.Organization) return;

            if (this.Selected != null)
                Selected(this, CurrentRow);
        }

        #endregion
    }

    [ToolboxItem(false)]
    public class OrganizationAndJobMiniFinderListPart : OrganizationJobMiniFinderListPart
    {
        protected override void Confirm()
        {
            if (CurrentRow == null) return;

            if (this.Selected != null)
                Selected(this, CurrentRow);
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SelectedHandler Selected;
    }
}
