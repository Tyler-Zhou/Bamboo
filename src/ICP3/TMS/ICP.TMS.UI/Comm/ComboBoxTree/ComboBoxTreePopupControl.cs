//-----------------------------------------------------------------------
// <copyright file="ComboBoxTreePopupControl.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.TMS.UI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;
    using System.Windows.Forms;
    using DevExpress.XtraTreeList;
    using DevExpress.XtraTreeList.Nodes;

    /// <summary>
    /// 弹出树控件
    /// </summary>
    public partial class ComboBoxTreePopupControl : UserControl
    {
        #region 本地变量

        LWComboBoxTree _ownerEditControl;
        bool _isFromQueryResultValueEvent = false;
 
        #endregion

        #region 初始化

        /// <summary>
        /// 
        /// </summary>
        public ComboBoxTreePopupControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ownerEditControl"></param>
        public ComboBoxTreePopupControl(LWComboBoxTree ownerEditControl)
            : this()
        {
            this._ownerEditControl = ownerEditControl;

            this.Init();
        }

        private void Init()
        {
            this._ownerEditControl.QueryResultValue += new DevExpress.XtraEditors.Controls.QueryResultValueEventHandler(_ownerEditControl_QueryResultValue);
            this._ownerEditControl.TextChanged += new EventHandler(_ownerEditControl_TextChanged);
            this.treeList1.MouseDoubleClick += new MouseEventHandler(treeList1_MouseDoubleClick);
            this.treeList1.BeforeCheckNode += new DevExpress.XtraTreeList.CheckNodeEventHandler(treeList1_BeforeCheckNode);
            this.treeList1.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(treeList1_AfterCheckNode);
            this.Separator = ",";
            this.ParentSeparator = "-";
        }
     
        #endregion

        #region 事件处理

        void _ownerEditControl_QueryResultValue(object sender, DevExpress.XtraEditors.Controls.QueryResultValueEventArgs e)
        {
            if (_ownerEditControl != null)
            {                        
                this._ownerEditControl.TextChanged -= new EventHandler(_ownerEditControl_TextChanged);
                //if (string.IsNullOrEmpty(_combineTextString))
                //{
                    e.Value = this.GetDispayText();
                //}
                //else
                //{
                //    e.Value = _combineTextString;
                //}

                _ownerEditControl.SelectedValue = this.GetCheckValues(this.ValueMember);
                //if (!string.IsNullOrEmpty(_combineTextString))
                //{
                //    _ownerEditControl.Text = _combineTextString;
                //}
               
                this._ownerEditControl.TextChanged += new EventHandler(_ownerEditControl_TextChanged);
          
                foreach (Binding b in _ownerEditControl.DataBindings)
                {
                    b.WriteValue();
                }

                _isFromQueryResultValueEvent = true;
            }
        }

        private void _ownerEditControl_TextChanged(object sender, EventArgs e)
        {
            if (_isFromQueryResultValueEvent)
            {
                _isFromQueryResultValueEvent = false;
                return;
            }

            if (!_ownerEditControl.ContainsFocus || string.IsNullOrEmpty(_ownerEditControl.Text.Trim()))
            {
                return;
            }

            //if (treeList1.Nodes.Count > 0 && !_ownerEditControl.IsPopupOpen)
            //{
            //    _ownerEditControl.ShowPopup();
            //}

            foreach (TreeListNode node in treeList1.Nodes)
            {
                //string nodeValue = node.GetDisplayText(this.DisplayMember);

                if (node.Tag != null && node.Tag.ToString().ToUpper().Contains(_ownerEditControl.Text.Trim().ToUpper()))
                {
                    this.treeList1.Selection.Clear();
                    node.Selected = true;
                    if (!_ownerEditControl.IsPopupOpen)
                    {
                        _ownerEditControl.ShowPopup();
                    }

                    treeList1.SetFocusedNode(node);
                    _ownerEditControl.Focus();
                    return;
                }

                if (node.HasChildren && node.Nodes.Count > 0)
                {
                    foreach (TreeListNode childNode in node.Nodes)
                    {
                        if (childNode.Tag != null && childNode.Tag.ToString().ToUpper().Contains(_ownerEditControl.Text.Trim().ToUpper()))
                        {
                            this.treeList1.Selection.Clear();
                            childNode.Selected = true;
                            if (!_ownerEditControl.IsPopupOpen)
                            {
                                _ownerEditControl.ShowPopup();
                            }

                            treeList1.SetFocusedNode(childNode);
                            _ownerEditControl.Focus();
                            return;
                        }
                    }
                }
            }
         }

        void treeList1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (_ownerEditControl != null)
            {
                _ownerEditControl.ClosePopup();
            }
        }

        void treeList1_AfterCheckNode(object sender, NodeEventArgs e)
        {
            SetCheckedChildNodes(e.Node, e.Node.CheckState);
            SetCheckedParentNodes(e.Node, e.Node.CheckState);
        }

        void treeList1_BeforeCheckNode(object sender, DevExpress.XtraTreeList.CheckNodeEventArgs e)
        {
            e.State = (e.PrevState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked);
        }

        #endregion

        #region 公共接口

        /// <summary>
        /// 显示成员
        /// </summary>
        [Browsable(true)]
        public string DisplayMember
        {
            get
            {
                return this.colName.FieldName;
            }
            set
            {
                this.colName.FieldName = value;
            }
        }

        /// <summary>
        /// 父子分割符
        /// </summary>
        public string ParentSeparator { get; set; }

        /// <summary>
        /// 同级分割符
        /// </summary>
        public string Separator { get; set; }

        /// <summary>
        /// 父键
        /// </summary>
        [Browsable(true)]
        public string ParentMember
        {
            get
            {
                return treeList1.ParentFieldName;
            }
            set
            {
                treeList1.ParentFieldName = value;
            }
        }

        /// <summary>
        /// ID
        /// </summary>
        [Browsable(true)]
        public string ValueMember
        {
            get
            {
                return treeList1.KeyFieldName;
            }
            set
            {
                treeList1.KeyFieldName = value;
            }
        }

        /// <summary>
        /// 数据源
        /// </summary>
        [Browsable(true)]
        public object DataSource
        {
            get
            {
                return treeList1.DataSource;
            }
            set
            {
                treeList1.BeginUpdate();
                treeList1.BeginUnboundLoad();
                treeList1.DataSource = value;
                treeList1.EndUnboundLoad();
                treeList1.EndUpdate();
            }
        }

        public object RootValue
        {
            get
            {
                return treeList1.RootValue;
            }
            set
            {
                treeList1.RootValue = value;
            }
        }

        /// <summary>
        /// 允许多选
        /// </summary>
        public bool AllowMultSelect
        {
            get
            {
                return treeList1.OptionsView.ShowCheckBoxes;
            }
            set
            {
                treeList1.OptionsView.ShowCheckBoxes = value;
            }
        }

        public string GetDispayText()
        {
            if (this.AllowMultSelect)
            {
                List<string> values = new List<string>();
                foreach (TreeListNode node in treeList1.Nodes)
                {
                    if (node.CheckState == CheckState.Checked)
                    {
                        string nodeValue = node.GetDisplayText(this.DisplayMember);
                        values.Add(nodeValue);
                    }

                    GetCheckedChildNodeDispayTexts(node, this.DisplayMember, values);
                }

                return this.Join(values);
            }
            else
            {
                if (this.treeList1.Selection.Count == 0) return null;

                //if (string.IsNullOrEmpty(_combineTextString))
                //{
                    //return treeList1.Selection[0].GetDisplayText(this.DisplayMember);
                return treeList1.Selection[0].Tag.ToString();
                //}
                //else
                //{
                //    return _combineTextString;
                //}            
            }
        }

        #endregion

        #region 本地方法,属性


        /*如果选择父节点，同时也选择所有子节点*/
        private void SetCheckedChildNodes(TreeListNode node, CheckState check)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                node.Nodes[i].CheckState = check;
                SetCheckedChildNodes(node.Nodes[i], check);
            }
        }

        /*如果所有的子节点都选择了，则父节点也选择*/
        private void SetCheckedParentNodes(TreeListNode node, CheckState check)
        {
            if (node.ParentNode != null)
            {
                bool b = false;
                CheckState state = CheckState.Checked;
                for (int i = 0; i < node.ParentNode.Nodes.Count; i++)
                {
                    state = (CheckState)node.ParentNode.Nodes[i].CheckState;
                    if (!check.Equals(state))
                    {
                        b = !b;
                        break;
                    }
                }

                node.ParentNode.CheckState = b ? CheckState.Indeterminate : check;
                SetCheckedParentNodes(node.ParentNode, check);
            }
        }

        private object EditValue
        {
            get { return this.GetCheckValues(this.ValueMember); }
            set
            {
                List<string> displayTests = new List<string>();
                if (this.AllowMultSelect)
                {
                    foreach (TreeListNode node in treeList1.Nodes)
                    {
                        System.Collections.IList values = (System.Collections.IList)value;

                        object nodeValue = node.GetValue(this.ValueMember);
                        if (values.Contains(nodeValue))
                        {
                            displayTests.Add(node.GetDisplayText(this.DisplayMember));
                            node.CheckState = CheckState.Checked;
                        }
                        else
                        {
                            node.CheckState = CheckState.Unchecked;
                        }

                        this.SetCheckValues(node, this.ValueMember, values);
                    }
                }
                else
                {
                    foreach (TreeListNode node in treeList1.Nodes)
                    {
                        object nodeValue = node.GetValue(this.ValueMember);
                        if (Object.Equals(value, nodeValue))
                        {
                            displayTests.Add(node.GetDisplayText(this.DisplayMember));
                            node.Selected = true;
                        }
                        else
                        {
                            node.Selected = false;
                        }

                        this.SetSelectValue(node, this.ValueMember, value);
                    }
                }

                if (_ownerEditControl != null)
                {
                    _ownerEditControl.Text = this.Join(displayTests);
                }
            }
        }


        string Join(List<string> values)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string value in values)
            {
                if (sb.Length > 0)
                {
                    sb.Append(this.Separator);
                }

                sb.Append(value);
            }

            return sb.ToString();
        }

        public object GetCheckValues(string filedName)
        {
            if (this.AllowMultSelect)
            {
                List<object> values = new List<object>();
                foreach (TreeListNode node in treeList1.Nodes)
                {
                    if (node.CheckState == CheckState.Checked)
                    {
                        object nodeValue = node.GetValue(filedName);
                        values.Add(nodeValue);
                    }

                    GetCheckedChildNodeValues(node, filedName, values);
                }

                return values.ToArray();
            }
            else
            {
                if (this.treeList1.Selection.Count == 0) return null;

                return treeList1.Selection[0].GetValue(filedName);
            }
        }

        public void InitSelectedNode(object nodeId)
        {
            if (treeList1.Nodes != null)
            {
                foreach (TreeListNode node in treeList1.Nodes)
                {
                    string nodeValue = node.GetDisplayText(this.DisplayMember);
                    node.Tag = nodeValue;
                    
                    if (node.HasChildren && node.Nodes.Count > 0)
                    {
                        foreach (TreeListNode childNode in node.Nodes)
                        {
                            string childNodeValue = childNode.GetDisplayText(this.DisplayMember);
                            childNode.Tag = nodeValue + " " + childNodeValue;
                        }
                    }
                }
            }

            if (nodeId != null && (Guid)nodeId != Guid.Empty)
            {
                TreeListNode findNode = treeList1.FindNodeByFieldValue(this.ValueMember, nodeId);
                if (findNode != null)
                {
                    treeList1.Selection.Clear();
                    findNode.Selected = true;
                    treeList1.SetFocusedNode(findNode);
                }
            }
        }

        public object GetNodeValueById(string fieldName,object id)
        {
            TreeListNode node = treeList1.FindNodeByFieldValue(this.ValueMember, id);
            if (node != null)
            {
                object nodeValue = node.GetValue(fieldName);
                return nodeValue;
            }

            return null;
        }


        private void SetCheckValues(TreeListNode node, string fieldName, System.Collections.IList values)
        {
            foreach (TreeListNode tempNode in node.Nodes)
            {
                object nodeValue = tempNode.GetValue(fieldName);
                if (values.Contains(nodeValue))
                {
                    tempNode.CheckState = CheckState.Checked;
                }
                else
                {
                    tempNode.CheckState = CheckState.Unchecked;
                }

                SetCheckValues(tempNode, fieldName, values);
            }
        }

        private void SetSelectValue(TreeListNode node, string fieldName, object value)
        {
            foreach (TreeListNode tempNode in node.Nodes)
            {
                object nodeValue = tempNode.GetValue(fieldName);
                if (value == nodeValue)
                {
                    tempNode.Selected = true;
                }
                else
                {
                    tempNode.Selected = false;
                }

                SetSelectValue(tempNode, fieldName, value);
            }
        }


        private void GetCheckedChildNodeValues(TreeListNode node, string fieldName, List<object> values)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                TreeListNode tempNode = node.Nodes[i];

                if (tempNode.CheckState == CheckState.Checked)
                {
                    object nodeValue = tempNode.GetValue(fieldName);
                    values.Add(nodeValue);
                }
                GetCheckedChildNodeValues(tempNode, fieldName, values);
            }
        }

        private void GetCheckedChildNodeDispayTexts(TreeListNode node, string fieldName, List<string> values)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                TreeListNode tempNode = node.Nodes[i];

                if (tempNode.CheckState == CheckState.Checked)
                {
                    string nodeValue = tempNode.GetDisplayText(fieldName);
                    values.Add(nodeValue);
                }
                GetCheckedChildNodeDispayTexts(tempNode, fieldName, values);
            }
        }

        #endregion
    }
}
