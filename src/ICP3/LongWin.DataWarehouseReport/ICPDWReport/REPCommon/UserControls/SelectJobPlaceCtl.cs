using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace LongWin.DataWarehouseReport.WinUI
{
    public partial class SelectJobPlaceCtl : UserControl
    {
        public SelectJobPlaceCtl()
        {
            InitializeComponent();
            this.cmbJobPlace.Multiple = true;
            this.cmbUserDepartment.Multiple = true;

            this.cmbJobPlace.TreeView.AfterCheck += new TreeViewEventHandler(TreeView_AfterCheck);
            
            this.cmbUserDepartment.TreeView.AfterCheck +=new TreeViewEventHandler(TreeView_AfterCheck);
             
        }

        void TreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {

            if (e.Node.Checked == false)
            {
                UnCheckNode(e.Node);    
            }
        }


        void UnCheckNode(TreeNode nb)
        {
            if (!nb.Checked)
            {
                foreach (TreeNode node in nb.Nodes)
                {
                    node.Checked = false;

                    UnCheckNode(node);
                }
            }
        }



        private void SelectJobPlaceCtl_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                
            }
        }

        #region 属性

        private DataSet _dataSource;
        /// <summary>
        /// 数据源

        /// </summary>
        public DataSet DataSource
        {
            get
            {
                return this._dataSource;
            }
            set
            {
                this._dataSource = value;
                if (this._dataSource != null && this._dataSource.Tables[0].Rows.Count > 0)
                {
                    if (_dataSource.Tables[0].Rows.Count == 1)
                    {
                        if (_dataSource.Tables[0].Rows[0]["Type"].ToString() == "3")
                        {
                            this.rabJobPlace.Enabled = false;

                            this.rabJobPlace.Checked = false;
                            this.rabUserDepartment.Checked = true;

                            this.cmbUserDepartment.DisplayMember = Utility.IsEnglish ? "EName" : "CName";
                            this.cmbUserDepartment.ValueMember = "ID";
                            this.cmbUserDepartment.ParentMember = "ParentID";
                            this.cmbUserDepartment.BringToFront();
                            this.cmbUserDepartment.SelectedValue = _dataSource.Tables[0].Rows[0]["Id"];
                            this.cmbUserDepartment.DataSource = _dataSource.Tables[0];
                        }
                        else
                        {
                            this.cmbJobPlace.Enabled = false;

                            this.cmbJobPlace.DisplayMember = Utility.IsEnglish ? "EName" : "CName";
                            this.cmbJobPlace.ValueMember = "ID";
                            this.cmbJobPlace.ParentMember = "ParentID";

                            this.cmbJobPlace.DataSource = _dataSource.Tables[0];
                            this.cmbJobPlace.SelectedValue = _dataSource.Tables[0].Rows[0]["Id"];
                            this.cmbUserDepartment.DisplayMember = Utility.IsEnglish ? "EName" : "CName";
                            this.cmbUserDepartment.ValueMember = "ID";
                            this.cmbUserDepartment.ParentMember = "ParentID";
                            this.cmbUserDepartment.SelectedValue = _dataSource.Tables[0].Rows[0]["Id"];
                            this.cmbUserDepartment.DataSource = _dataSource.Tables[0];
                            this.cmbJobPlace.BringToFront();
                        }

                    }
                    else
                    {
                        object id = "";
                        string name = "";
                        string nodecode = "11111111111111111111111111111111111111111111111111111111111";
                        for (int i = 0; i < this._dataSource.Tables[0].Rows.Count; i++)
                        {
                            if (this._dataSource.Tables[0].Rows[i]["Id"].ToString() == this._dataSource.Tables[0].Rows[i]["ParentID"].ToString()
                                || this._dataSource.Tables[0].Select("Id='" + this._dataSource.Tables[0].Rows[i]["ParentID"].ToString() + "'").Length == 0)
                            {
                                if (nodecode.Length > this._dataSource.Tables[0].Rows[i]["NodeCode"].ToString().Trim().Length)
                                {
                                    nodecode = this._dataSource.Tables[0].Rows[i]["NodeCode"].ToString().Trim();
                                    id = this._dataSource.Tables[0].Rows[i]["Id"];
                                    name = this._dataSource.Tables[0].Rows[i]["CName"].ToString();
                                }
                                this._dataSource.Tables[0].Rows[i]["ParentID"] = (object)Guid.Empty;
                            }
                        }

                        DataView dv = this._dataSource.Tables[0].DefaultView;
                        dv.RowFilter = "Type<>3";

                        if (dv.Count > 0)
                        {
                            this.cmbJobPlace.DisplayMember = Utility.IsEnglish ? "EName" : "CName";
                            this.cmbJobPlace.ValueMember = "ID";
                            this.cmbJobPlace.ParentMember = "ParentID";
                            this.cmbJobPlace.SelectedValue = id;
                            this.cmbJobPlace.DataSource = dv;
                            this.cmbJobPlace.BringToFront();
                            this.rabJobPlace.Checked = true;
                        }
                        else
                        {
                            this.rabJobPlace.Checked = false;
                            this.rabJobPlace.Enabled = false;
                            this.rabUserDepartment.Checked = true;

                            this.cmbUserDepartment.BringToFront();
                        }
                        this.cmbUserDepartment.DisplayMember = Utility.IsEnglish ? "EName" : "CName";
                        this.cmbUserDepartment.ValueMember = "ID";
                        this.cmbUserDepartment.ParentMember = "ParentID";
                        this.cmbUserDepartment.DataSource = this._dataSource.Tables[0];
                        this.cmbUserDepartment.SelectedValue = id;
                    }
                }
            }
        }

        public override string Text
        {
            get
            {
                if (this.rabJobPlace.Checked)
                {
                    return this.cmbJobPlace.Text;
                }
                return this.cmbUserDepartment.Text;
            }
        }

        public object Value
        {
            get
            {
                if (this.rabJobPlace.Checked)
                {
                    return this.cmbJobPlace.SelectedValue;
                }
                return this.cmbUserDepartment.SelectedValue;
            }
        }

        public int StructType
        {
            get
            {
                if (this.rabJobPlace.Checked)
                {
                    return 0;
                }
                return 1;
            }
        }


        #endregion

        private void rabJobPlace_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rabJobPlace.Checked)
            {
                this.cmbJobPlace.BringToFront();
            }
        }

        private void rabUserDepartment_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rabUserDepartment.Checked)
            {
                this.cmbUserDepartment.BringToFront();
            }
        }

        bool _displayCompanyOnly=false;
        /// <summary>
        /// 只显示为业务发生地
        /// </summary>
        public bool DisplayCompanyOnly
        {
            set
            {
                _displayCompanyOnly = value;
                if (_displayCompanyOnly)
                {
                    this.rabJobPlace.Visible = false;
                    this.rabUserDepartment.Visible = false;
                    this.cmbUserDepartment.Visible = false;
                    this.cmbJobPlace.Location = new Point(0, 5);
                }
                else
                {
                    this.rabUserDepartment.Visible = true;
                    this.rabJobPlace.Visible = true;
                    this.cmbUserDepartment.Visible = true;
                    this.cmbJobPlace.Location = new Point(0, 36);
                }
            }
        }
    }
}
