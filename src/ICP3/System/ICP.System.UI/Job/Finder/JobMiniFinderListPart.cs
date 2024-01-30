using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.StyleFormatConditions;

namespace ICP.Sys.UI.Job.Finder
{
    [ToolboxItem(false)]
    public partial class JobMiniFinderListPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region 服务注入

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region 初始化

        public JobMiniFinderListPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                Utility.RemoveSetControlKeyEnterToClickButton(new List<Control> { this.txtFind }, OnKeyDownHandle);
                this.commStyleFormatCondition = null;
                this.Selected = null;
                this.txtFind.TextChanged -= this.txtFind_TextChanged;
                this.btnConfirm.Click -= this.btnConfirm_Click;
                this.btnNext.Click -= this.btnNext_Click;
                this.treeOrgJob.KeyDown -= this.gvMain_KeyDown;
                this.treeOrgJob.MouseDoubleClick -= this.treeOrgJob_MouseDoubleClick;
                this.treeOrgJob.DataSource = null;
                this.bsList.DataSource = null;
                this.bsList.Dispose();
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

            colCName.Caption = "名称";
            colCName.Visible = true;
            colEName.Visible = false;
        }

        StyleFormatCondition commStyleFormatCondition = new StyleFormatCondition();
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Utility.SetControlKeyEnterToClickButton(new List<Control> { this.txtFind }, this.btnConfirm, OnKeyDownHandle);
            #region Finder Style

            treeOrgJob.FormatConditions.Add(commStyleFormatCondition);

            commStyleFormatCondition.Appearance.Font = new Font(colCName.AppearanceCell.Font, FontStyle.Bold);
            commStyleFormatCondition.Appearance.BackColor = Color.Aqua;
            commStyleFormatCondition.Condition = DevExpress.XtraGrid.FormatConditionEnum.None;
            commStyleFormatCondition.ApplyToRow = false;
            commStyleFormatCondition.Column = LocalData.IsEnglish ? colEName:colEName;

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
        private JobList CurrentRow
        {
            get { return Current as JobList; }
        }

        public override object DataSource
        {
            get { return bsList.DataSource; }
            set
            {
                bsList.DataSource = value;
                bsList.ResetBindings(false);
                treeOrgJob.ExpandAll();
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

                StringBuilder expressionBulider = new StringBuilder();
                expressionBulider.Append("Upper([CName]) Like '%");
                expressionBulider.Append(txtFind.Text.Trim().ToUpper());
                expressionBulider.Append("%' or Upper([EName]) Like '%");
                expressionBulider.Append(txtFind.Text.Trim().ToUpper());
                expressionBulider.Append("%'");

                commStyleFormatCondition.Expression = expressionBulider.ToString();

                List<JobList> list = bsList.DataSource as List<JobList>;
                List<JobList> tagers = list.FindAll(delegate(JobList item) 
                                                    {
                                                        return
                                                       (item.CName.ToUpper().Contains(txtFind.Text.Trim().ToUpper())
                                                        || item.EName.ToUpper().Contains(txtFind.Text.Trim().ToUpper()));
                                                    });

                if (tagers == null || tagers.Count == 0) return;

                foreach (var item in tagers)
                {
                    TreeListNode tn = treeOrgJob.FindNodeByFieldValue("ID", item.ID);
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
            if (treeOrgJob.FocusedNode != null) currentFoudIndex = treeOrgJob.FocusedNode.Id;
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

            TreeListNode tn = treeOrgJob.FindNodeByID(needFocusedIndex);
            if (tn != null) treeOrgJob.SetFocusedNode(tn);
        }
        #endregion

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Confirm();
        }

        private void treeOrgJob_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Confirm();
        }


        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) Confirm();
        }

        private void Confirm()
        {
            if (CurrentRow == null) return;

            if (this.Selected != null)
                Selected(this, CurrentRow);
        }

        #endregion
    }
}
