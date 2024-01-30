using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using System.IO;
using System.Data.Sql;
using Microsoft.Practices.CompositeUI;
using System.Data.SqlClient;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary;
using ICP.Framework.ClientComponents;

namespace ICP.ReportCenter.UI.FinanceOEReports
{
    [ToolboxItem(false)]
    public partial class UFServicePart : BasePart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region 属性
        /// <summary>
        /// 客户列表
        /// </summary>
        public List<UFData> UFCustomerList
        {
            get;
            set;
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        public List<UFData> UFPersonList
        {
            get;
            set;
        }
        /// <summary>
        /// 部门列表
        /// </summary>
        public List<UFData> UFDepartmentList
        {
            get;
            set;
        }

        /// <summary>
        /// 科目列表
        /// </summary>
        public List<UFData> UFGLCodeList { get; set; }


        DataTable _serverTable;
        string _xmlPath = AppDomain.CurrentDomain.BaseDirectory + "\\UFServer.xml";
        private bool _isCost = false;//是否是管理成本凭证
        #endregion

        #region init

        public UFServicePart()
        {
             InitializeComponent();
            this.Disposed += delegate
            {
                if (_conn != null && _conn.State == ConnectionState.Open) _conn.Dispose();
                this._serverTable = null;
                this.UFCustomerList = null;
                this._xmlPath = null;
                this.dxErrorProvider1.DataSource = null;
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
            InitServerTable();
            ShowServerName(_serverTable);
        }

        private void InitServerTable()
        {
            _serverTable = new DataTable();
            _serverTable.TableName = "ServerList";
            _serverTable.Columns.Add(new DataColumn("ServerName", typeof(string)));
            _serverTable.Columns.Add(new DataColumn("UserID", typeof(string)));
            _serverTable.Columns.Add(new DataColumn("PSWD", typeof(string)));

            if (File.Exists(_xmlPath))
            {
                _serverTable.ReadXml(_xmlPath);
            }
        }

        #endregion

        #region  Btn

        private void btnOK_Click(object sender, EventArgs e)
        {

            if (ValidateData() == false) return;

            try
            {
                using (new CursorHelper())
                {
                    if (_conn == null
                               || _conn.State == ConnectionState.Closed)
                    {
                        _conn = new System.Data.SqlClient.SqlConnection();
                        _conn.ConnectionString = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}"
                            , this.cmbServerList.SelectedText
                            , this.cmbAccount.SelectedText
                            , this.txtUserName.Text.Trim()
                            , this.txtPassword.Text.Trim());
                    }
                    else
                    {
                        _conn.ChangeDatabase(this.cmbAccount.EditValue.ToString());
                    }
                    string sql = string.Empty;
                    //转换客户信息
                    sql = "SELECT cCusCode as Code,cCusName as Name FROM Customer ";
                    DataTable customerList = new DataTable();
                    ExecSql(_conn, sql, customerList);
                    UFCustomerList = UFData.GetUFDataForDataTable(customerList);

                    //转换科目信息
                    sql = "SELECT ccode AS CODE,ccode_Name AS NAME FROM CODE ";
                    DataTable glList = new DataTable();
                    ExecSql(_conn, sql, glList);
                    UFGLCodeList = UFData.GetUFDataForDataTable(glList);
                    

                    if (_isCost)
                    {
                        //管理成本 需要加上部门跟个人
                        sql = @" SELECT  Person.cPersonName AS Code, Department.cDepName  AS Name
                             FROM Department RIGHT JOIN Person ON Department.cDepCode = Person.cDepCode 
                             order by Person.cPersonCode";

                        DataTable dtPerson =new DataTable();
                        ExecSql(_conn, sql, dtPerson);
                        UFPersonList = UFData.GetUFDataForDataTable(dtPerson);

                        //部门
                        sql = "SELECT cDepCode as Code,cDepName as Name FROM Department";
                        DataTable dtDept = new DataTable();
                        ExecSql(_conn, sql, dtDept);
                        UFDepartmentList = UFData.GetUFDataForDataTable(dtDept);
                    }
                    if (_conn.State == ConnectionState.Open)
                    {
                        _conn.Close();
                    }

                    #region 保存帐套信息
                    DataRow[] drs = _serverTable.Select("ServerName='" + cmbServerList.Text + "'");
                    if (drs != null && drs.Count<DataRow>() > 0)
                    {
                        drs[0]["UserID"] = this.txtUserName.Text;
                        drs[0]["PSWD"] = this.txtPassword.Text;
                        _serverTable.AcceptChanges();
                        _serverTable.WriteXml(_xmlPath);
                    }
                    else
                    {
                        DataRow dr = _serverTable.Rows.Add(cmbServerList.Text, this.txtUserName.Text, this.txtPassword.Text);
                        dr.EndEdit();
                        _serverTable.AcceptChanges();
                        _serverTable.WriteXml(_xmlPath);
                    }
                    #endregion

                    this.FindForm().DialogResult = DialogResult.OK;
                    this.FindForm().Close();
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(),ex);
            }
          

        }
        /// <summary>
        /// 执行查询语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sql"></param>
        private void ExecSql(SqlConnection _conn, string sql, DataTable dt)
        {
            if (_conn.State != ConnectionState.Open) _conn.Open();
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
        }

        private bool ValidateData()
        {
            dxErrorProvider1.ClearErrors();
            bool isSrcc = true;
            this.Validate();
            
            if (this.cmbServerList.Text.Trim() == string.Empty)
            {
                dxErrorProvider1.SetError(cmbServerList, LocalData.IsEnglish ? "服务不能为空,请选择" : "服务不能为空,请选择");
                isSrcc = false;
            }

            if (this.txtUserName.Text.Trim() == string.Empty)
            {
                dxErrorProvider1.SetError(cmbServerList, LocalData.IsEnglish ? "请输入登录名" : "请输入登录名");
                isSrcc = false;
            }

            if (this.txtPassword.Text.Trim() == string.Empty)
            {
                dxErrorProvider1.SetError(cmbServerList, LocalData.IsEnglish ? "请输入密码" : "请输入密码");
                isSrcc = false;
            }

            if (cmbAccount.EditValue == null || string.IsNullOrEmpty(cmbAccount.Text))
            {
                dxErrorProvider1.SetError(cmbServerList, LocalData.IsEnglish ? "帐套不能为空,请选择" : "帐套不能为空,请选择");
                isSrcc = false;
            }
            return isSrcc;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        #endregion

        #region event

        #region 刷新服务

        private void cmbServerList_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                DataTable dt = SqlDataSourceEnumerator.Instance.GetDataSources();
                ShowServerName(dt);
            }
        }

        #endregion

        #region 服务选择

        private void cmbServerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cmbAccount.Properties.Items.Clear();

            DataRow[] drs = _serverTable.Select("ServerName='" + cmbServerList.Text + "'");
            if (drs != null && drs.Count<DataRow>() > 0)
            {
                this.txtUserName.Text = drs[0]["UserID"].ToString();
                this.txtPassword.Text = drs[0]["PSWD"].ToString();
            }
            else
            {
                this.txtUserName.Text = "sa";
                this.txtPassword.Text = "ufpassword";
            }

        }

        private void ShowServerName(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.cmbAccount.ClosePopup();
                cmbAccount.Properties.Items.Clear();
                cmbServerList.ClosePopup();
                cmbServerList.Properties.Items.Clear();
                this.cmbServerList.Text = string.Empty;

                foreach (DataRow dr in dt.Rows)
                {
                    cmbServerList.Properties.Items.Add(dr["ServerName"]);
                }
                cmbServerList.SelectedIndexChanged -= new EventHandler(cmbServerList_SelectedIndexChanged);
                cmbServerList.SelectedIndexChanged += new EventHandler(cmbServerList_SelectedIndexChanged);
                this.cmbServerList.SelectedIndex = 0;
            }
            catch (Exception) { LocalCommonServices.ErrorTrace.SetErrorInfo(this, LocalData.IsEnglish ? "刷新服务器列表失败." : "刷新服务器列表失败."); }
            finally { this.Cursor = Cursors.Default; }
        }

        #endregion

        #region 刷新账套
        /// <summary>
        /// 账套的连接
        /// </summary>
        System.Data.SqlClient.SqlConnection _conn;
        private void cmbAccount_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)
            {
                cmbAccount.Properties.Items.Clear();
                RefershAccount();
            }
        }
        private void RefershAccount()
        {
            if (this.cmbServerList.Text.Trim() == string.Empty)
            {
                MessageBoxService.ShowInfo("服务不能为空,请选择");
                return;
            }
            try
            {
                if (_conn == null || _conn.State == ConnectionState.Closed)
                {
                    _conn = new System.Data.SqlClient.SqlConnection();
                    _conn.ConnectionString = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}"
                        , this.cmbServerList.Text
                        , "UFSystem"
                        , this.txtUserName.Text.Trim()
                        , this.txtPassword.Text.Trim());
                }
                if (_conn.State != ConnectionState.Open) _conn.Open();

                //ua_account_sub(账套年度表)-->UA_Period(会计期间表)-->ua_account(账套表)-->ua_holdauth(操作员权限分配表)  
                string sql = @"SELECT  '('+CAST(A.cAcc_Id AS VARCHAR(5))+')'+B.cAcc_name+'_'+CAST(A.iYear AS VARCHAR(5)) AS AccountName ,DbName = 'UFData_'+A.cAcc_Id+'_'+CAST(A.iYear AS VARCHAR(5))
                            FROM (SELECT DISTINCT cAcc_Id,iyear FROM UA_Period Where (bIsDelete=0 OR bIsDelete IS NULL) ) A
                            INNER JOIN dbo.UA_Account B ON A.cAcc_Id=B.cAcc_Id
                            ORDER BY 1 DESC ";
                SqlCommand cmd = new SqlCommand(sql, _conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count <= 0)
                    return;

                foreach (DataRow dr in dt.Rows)
                {
                    cmbAccount.Properties.Items.Add(new ImageComboBoxItem(dr["AccountName"].ToString(), dr["DbName"]));
                }
            }
            catch (Exception) { LocalCommonServices.ErrorTrace.SetErrorInfo(this, LocalData.IsEnglish ? "刷新帐套列表失败." : "刷新帐套列表失败."); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void cmbAccount_Enter(object sender, EventArgs e)
        {
            if (this.cmbAccount.Properties.Items.Count == 0) RefershAccount();
        }

        #endregion

        #endregion

        #region InterFace
        /// <summary>
        /// SetSouce
        /// </summary>
        /// <param name="isCost">是否是管理成本凭证</param>
        public void SetSouce(bool isCost)
        {
            _isCost = isCost;
        }

        #endregion
    }


    public class UFData
    {
        public UFData(string code, string name)
        {
            Code = code.ToUpper();
            Name = name;
        }
        public string Code { get; set; }
        public string Name { get; set; }

        public static List<UFData> GetUFDataForDataTable(DataTable dt)
        {
            List<UFData> list = new List<UFData>();
            if (dt == null || dt.Rows.Count == 0) { return list; }

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new UFData(DataTypeHelper.GetString(dr["Code"]), DataTypeHelper.GetString(dr["Name"])));
            }
            return list;
        }
    }
}
