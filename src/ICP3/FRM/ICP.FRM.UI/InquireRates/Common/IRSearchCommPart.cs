using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraTreeList;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;

using ICP.Common.ServiceInterface.DataObjects;
using DevExpress.XtraTreeList.Nodes;
using System.Text.RegularExpressions;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Helper;
using StyleFormatCondition = DevExpress.XtraTreeList.StyleFormatConditions.StyleFormatCondition;

namespace ICP.FRM.UI.InquireRates
{
    [ToolboxItem(false)]
    public partial class IRSearchCommPart : BasePart
    {
        #region 服务注入

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region 

        List<CommodityList> _commoditys =null;

        string _commString =string.Empty;
        public string CommString { get { return _commString; } }

        StyleFormatCondition commStyleFormatCondition = new StyleFormatCondition();
        StyleFormatCondition exceptStyleFormatCondition = new StyleFormatCondition();

        #endregion

        #region init

        public IRSearchCommPart()
        {
            InitializeComponent();
            Disposed += delegate {
                commStyleFormatCondition = null;
                exceptStyleFormatCondition = null;
                treeComm.DataSource = null;
                treeExcept.DataSource = null;
                bsComm.DataSource = null;
                bsComm.Dispose();
                bsExceptComm.DataSource = null;
                bsExceptComm.Dispose();
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
            btnOk.Focus();
        }

        private void InitControls()
        {
 	        //_commoditys= tfService.GetCommodityList(string.Empty,true,0);
            bsComm.DataSource = _commoditys;
            bsExceptComm.DataSource = _commoditys;
            treeComm.ExpandAll();
            treeExcept.ExpandAll();

            BulidCheckedByCommString();


            treeComm.AfterCheckNode += new NodeEventHandler(treeNewComm_AfterCheckNode);
            treeExcept.AfterCheckNode += new NodeEventHandler(treeExcept_AfterCheckNode);

            #region Finder Style

            treeComm.FormatConditions.Add(commStyleFormatCondition);
            commStyleFormatCondition.Appearance.Font = new Font(colCommEName.AppearanceCell.Font, FontStyle.Bold);
            commStyleFormatCondition.Appearance.BackColor = Color.Aqua;
            commStyleFormatCondition.Condition = FormatConditionEnum.None ;
            commStyleFormatCondition.ApplyToRow = false;
            commStyleFormatCondition.Column = colCommEName;

            treeExcept.FormatConditions.Add(exceptStyleFormatCondition);
            exceptStyleFormatCondition.Appearance.Font = new Font(colCommEName.AppearanceCell.Font, FontStyle.Bold);
            exceptStyleFormatCondition.Appearance.BackColor = Color.Aqua;
            exceptStyleFormatCondition.Condition = FormatConditionEnum.None;
            exceptStyleFormatCondition.ApplyToRow = false;
            exceptStyleFormatCondition.Column = colExceptEName;

            #endregion
        }
      
        void BulidCheckedByCommString()
        {
            if (string.IsNullOrEmpty(_commString)) return;

            List<string> exceptCommodity = GetExceptCommodity(_commString);
            List<string> commodity = GetCommodity(_commString);

            #region BulidChecked

            for (int i = 0; i < treeComm.AllNodesCount; i++)
            {
                TreeListNode tn = treeComm.FindNodeByID(i);
                string ename = tn.GetValue(colCommEName).ToString();

                if (commodity.Contains(ename)) tn.Checked = true;
            }

            for (int i = 0; i < treeExcept.AllNodesCount; i++)
            {
                TreeListNode tn = treeExcept.FindNodeByID(i);
                string ename = tn.GetValue(colExceptEName).ToString();

                if (exceptCommodity.Contains(ename)) tn.Checked = true;
            }

            #endregion

            #region BulidText

            List<string> commNames = new List<string>();
            foreach (var item in _commoditys )
            {
                commNames.Add(item.EName.ToUpper());
            }

            StringBuilder errorText = new StringBuilder();
            StringBuilder exceptText = new StringBuilder();
            StringBuilder commText = new StringBuilder();

            foreach (var item in exceptCommodity)
            {
                if (commNames.Contains(item))
                    exceptText.Append(item + GlobalConstants.ShowDividedSymbol);
                else
                    errorText.Append(item + "\r\n");
            }

            foreach (var item in commodity)
            {
                if (commNames.Contains(item))
                    commText.Append(item + GlobalConstants.ShowDividedSymbol);
                else
                    errorText.Append(item + "\r\n");
            }

            txtErrorComm.Text = errorText.ToString();
            txtExcept.Text = exceptText.ToString();
            txtComm.Text = commText.ToString();

            #endregion
        }

        List<string> GetCommodity(string commodityString)
        {
            List<string> result = new List<string>();

            string temp = Regex.Replace(commodityString, @"(\[EXCEPT\s*:\s*).+?(\s*]\s*)+", UIConstants.DividedSymbol.ToString());
            string[] tager = temp.Split(UIConstants.DividedSymbol);
            if (tager == null) return result;
            foreach (var sItem in tager)
            {
                if (string.IsNullOrEmpty(sItem)) continue;
                result.Add(sItem.Trim().ToUpper());
            }

            return result;
        }
        List<string> GetExceptCommodity(string commodityString)
        {
            List<string> result = new List<string>();

            Regex r = new Regex(@"(\[EXCEPT\s*:\s*).+?(\s*]\s*)+");
            MatchCollection ms = r.Matches(commodityString, 0);
            foreach (var item in ms)
            {
                string temp = Regex.Replace(item.ToString(), @"(\[EXCEPT\s*:\s*)", string.Empty);
                temp = Regex.Replace(temp, @"(\s*\]\s*)", string.Empty);

                string[] tager = temp.Split(UIConstants.DividedSymbol);
                if (tager == null) continue;

                foreach (var sItem in tager)
                {
                    if (string.IsNullOrEmpty(sItem)) continue;
                    result.Add(sItem.Trim().ToUpper());
                }
            }

            return result;
        }

        #endregion

        #region btn

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FindForm().DialogResult = DialogResult.Cancel;
            FindForm().Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string comm = txtComm.Text.Trim();
            if (txtExcept.Text.Trim().IsNullOrEmpty() == false)
            {
                comm += "[EXCEPT:" + txtExcept.Text.Trim() + "]";
            }

            _commString = comm;
            FindForm().DialogResult = DialogResult.OK;
            FindForm().Close();
        }

        #region Selecte
        private void btnCommSelect_Click(object sender, EventArgs e)
        {
            if (treeComm.FocusedNode == null) return;
            treeComm.FocusedNode.Checked = true;
            treeNewComm_AfterCheckNode(null, new NodeEventArgs(treeComm.FocusedNode));
        }

        private void btnExceptSelect_Click(object sender, EventArgs e)
        {
            if (treeExcept.FocusedNode == null) return;
            treeExcept.FocusedNode.Checked = true;
            treeExcept_AfterCheckNode(null, new NodeEventArgs(treeComm.FocusedNode));
        }

        #endregion

        #endregion

        #region treeCheck

        private void treeNewComm_AfterCheckNode(object sender, NodeEventArgs e)
        {
            string ename = e.Node.GetValue(colCommEName).ToString() + GlobalConstants.ShowDividedSymbol;
            if (e.Node.Checked)
                txtComm.Text += ename;
            else
                txtComm .Text = txtComm.Text.Replace(ename, string.Empty);
        }

        void treeExcept_AfterCheckNode(object sender, NodeEventArgs e)
        {
            string ename = e.Node.GetValue(colExceptEName).ToString() + GlobalConstants.ShowDividedSymbol;
            if (e.Node.Checked)
                txtExcept.Text += ename;
            else
                txtExcept.Text = txtExcept.Text.Replace(ename, string.Empty);
        }


        #endregion

        #region Finder

        List<int> commFinderIndexs = new List<int>();
        private void btnCommNext_Click(object sender, EventArgs e)
        {
            CommNext();
        }
        private void CommNext()
        {
            if (commFinderIndexs == null || commFinderIndexs.Count == 0) return;

            int currentFoudIndex = -1;//当前行Index
            if (treeComm.FocusedNode != null) currentFoudIndex = treeComm.FocusedNode.Id;
            if (currentFoudIndex < 0) currentFoudIndex = 0;

            int needFocusedIndex = -1;
            if (commFinderIndexs.Contains(currentFoudIndex) == false)
            {
                needFocusedIndex = commFinderIndexs[0];
            }
            else
            {
                int tempIndex = commFinderIndexs.IndexOf(currentFoudIndex);
                if (tempIndex < 0 || tempIndex == commFinderIndexs.Count - 1) needFocusedIndex = commFinderIndexs[0];
                else needFocusedIndex = commFinderIndexs[tempIndex + 1];
            }

            if (needFocusedIndex < 0) return;

            TreeListNode tn = treeComm.FindNodeByID(needFocusedIndex);
            if (tn != null) treeComm.SetFocusedNode(tn);
        }
        
        private void txtCommFinder_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCommFinder.Text.Trim()))
            {
                commStyleFormatCondition.Condition = FormatConditionEnum.None;
                commFinderIndexs.Clear();
            }
            else
            {
                commFinderIndexs.Clear();
                commStyleFormatCondition.Expression = "Upper([EName]) Like '%" + txtCommFinder.Text.Trim().ToUpper() + "%'";
                commStyleFormatCondition.Condition = FormatConditionEnum.Expression;

                List<CommodityList> list = bsComm.DataSource as List<CommodityList>;
                List<CommodityList> tagers = list.FindAll(delegate(CommodityList item) { return item.EName.ToUpper().Contains(txtCommFinder.Text.Trim().ToUpper()); });
                if (tagers == null || tagers.Count == 0) return;

                foreach (var item in tagers)
                {
                    TreeListNode tn = treeComm.FindNodeByFieldValue("ID", item.ID);
                    if (tn != null) commFinderIndexs.Add(tn.Id);
                }
                CommNext();
            }
        }

        #endregion

        #region FinderExcept

        List<int> exceptFinderIndexs = new List<int>();

        private void btnExceptNext_Click(object sender, EventArgs e)
        {
            ExceptNext();
        }
        private void ExceptNext()
        {
            if (exceptFinderIndexs == null || exceptFinderIndexs.Count == 0) return;

            int currentFoudIndex = -1;//当前行Index
            if (treeExcept.FocusedNode != null) currentFoudIndex = treeExcept.FocusedNode.Id;
            if (currentFoudIndex < 0) currentFoudIndex = 0;

            int needFocusedIndex = -1;
            if (exceptFinderIndexs.Contains(currentFoudIndex) == false)
            {
                needFocusedIndex = exceptFinderIndexs[0];
            }
            else
            {
                int tempIndex = exceptFinderIndexs.IndexOf(currentFoudIndex);
                if (tempIndex < 0 || tempIndex == exceptFinderIndexs.Count - 1) needFocusedIndex = exceptFinderIndexs[0];
                else needFocusedIndex = exceptFinderIndexs[tempIndex + 1];
            }

            if (needFocusedIndex < 0) return;

            TreeListNode tn = treeExcept.FindNodeByID(needFocusedIndex);
            if (tn != null) treeExcept.SetFocusedNode(tn);
        }

        private void txtExceptFinder_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtExceptFind.Text.Trim()))
            {
                exceptStyleFormatCondition.Condition = FormatConditionEnum.None;
                exceptFinderIndexs.Clear();
            }
            else
            {
                exceptFinderIndexs.Clear();
                exceptStyleFormatCondition.Expression = "Upper([EName]) Like '%" + txtExceptFind.Text.Trim().ToUpper() + "%'";
                exceptStyleFormatCondition.Condition = FormatConditionEnum.Expression;

                List<CommodityList> list = bsComm.DataSource as List<CommodityList>;
                List<CommodityList> tagers = list.FindAll(delegate(CommodityList item) { return item.EName.ToUpper().Contains(txtExceptFind.Text.Trim().ToUpper()); });
                if (tagers == null || tagers.Count == 0) return;

                foreach (var item in tagers)
                {
                    TreeListNode tn = treeExcept.FindNodeByFieldValue("ID", item.ID);
                    if (tn != null) exceptFinderIndexs.Add(tn.Id);
                }
                CommNext();
            }
        }

        #endregion

        #region 接口

        public void SetSource(List<CommodityList> commoditys,string commString) 
        {
            _commoditys = commoditys;
            _commString = commString;
            //if (string.IsNullOrEmpty(commString))
            //    splitContainerControl1.Collapsed = true;
            //else
            //    txtOriginComm.Text = commString;

        }

        #endregion

    }
}