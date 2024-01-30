using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using LongWin.Framework.ClientComponents;

namespace LongWin.DataWarehouseReport.WinUI
{
    public partial class SelectCompanyCtl : UserControl
    {
        #region 初始化

        public SelectCompanyCtl()
        {
            InitializeComponent();
            this._isDisplayDepartment = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!this.DesignMode)
            {
                this.txtTreeTextBox.TreeDisplayMember =Utility.GetValueString("CName", "CName");
                this.txtTreeTextBox.TreeValueMember = "Id";
                this.txtTreeTextBox.TreeParentMember = "ParentID";

                this.txtTreeTextBox.SelectedChanged += new global::LongWin.Framework.ClientComponents.EventHandlerSelectedChanged(txtTreeTextBox_SelectedChanged);

            }
        }
        #endregion

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
                if (this._dataSource != null&&this._dataSource.Tables.Count==2)
                {
                    if (_dataSource.Tables[0].Rows.Count == 1)
                    {
                        this.txtTreeTextBox.Enabled = false;
                        
                        this.txtTreeTextBox.TreeInitParentKey = _dataSource.Tables[0].Rows[0]["Id"];
                        this.txtTreeTextBox.Value = _dataSource.Tables[0].Rows[0]["Id"];
                        this.txtTreeTextBox.Description = _dataSource.Tables[0].Rows[0][this.txtTreeTextBox.TreeDisplayMember].ToString();
                    }
                    else
                    {
                        object id = "";
                        string name = "";
                        string nodecode = "11111111111111111111111111111111111111111111111111111111111";
                        for (int i = 0; i < this._dataSource.Tables[0].Rows.Count; i++)
                        {
                            if (this._dataSource.Tables[0].Rows[i]["Id"].ToString()==this._dataSource.Tables[0].Rows[i]["ParentId"].ToString()
                                ||this._dataSource.Tables[0].Select("Id='" + this._dataSource.Tables[0].Rows[i]["ParentId"].ToString()+"'").Length == 0)
                            {
                                if (nodecode.Length > this._dataSource.Tables[0].Rows[i]["NodeCode"].ToString().Trim().Length)
                                {
                                    nodecode = this._dataSource.Tables[0].Rows[i]["NodeCode"].ToString().Trim();
                                    id = this._dataSource.Tables[0].Rows[i]["Id"];
                                    name = this._dataSource.Tables[0].Rows[i][this.txtTreeTextBox.TreeDisplayMember].ToString();
                                }
                                this._dataSource.Tables[0].Rows[i]["ParentId"] = (object)Guid.Empty;
                            }
                        }
                        this.txtTreeTextBox.TreeDataSource = this._dataSource.Tables[0];
                        //this.txtTreeTextBox.TreeInitParentKey = id;
                        this.txtTreeTextBox.Value = id;
                        this.txtTreeTextBox.Description = name;

 
                    }
                }
            }
        }

        private bool _isDisplayDepartment;
        /// <summary>
        /// 是否显示部门
        /// </summary>
        public bool IsDisplayDepartment
        {
            set { this._isDisplayDepartment = value; }
            get { return this._isDisplayDepartment; }
        }

       


        public override string Text
        {
            get { return this.txtTreeTextBox.Description; }
        }

        public object Value
        {
            get { return this.txtTreeTextBox.Value; }
        }

        
        #endregion

        #region 事件
        /// <summary>
        /// 选择的内容改变

        /// </summary>
        
        public event CompanySelectChangedEventHandler SelectedChanged;
        void txtTreeTextBox_SelectedChanged(object sender, object SelectedValue)
        {
            if (this.SelectedChanged != null)
            {
                CompanySelectChangedEventArgs args = new CompanySelectChangedEventArgs();
                args.SelectValue = SelectedValue;

                this.SelectedChanged(sender, args);
            }
        }
        #endregion

    }
}
