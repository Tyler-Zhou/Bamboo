using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace LongWin.DataWarehouseReport.WinUI
{
    public partial class UFServerForm : Form
    {
        DataTable _serverTable;
        string _xmlPath;
        private bool _isCost = false;//是否是管理成本凭证
        /// <summary>
        /// 是否是管理成本凭证
        /// </summary>
        /// <param name="IsCost"></param>

        public UFServerForm(bool IsCost):this()
        {
            _isCost = IsCost;
        }
        public UFServerForm()
        {
             InitializeComponent();
            
            this.Text = "用友服务器";
            _serverTable = new DataTable();
            _serverTable.TableName = "ServerList";
            _serverTable.Columns.Add(new DataColumn("ServerName", typeof(string)));
            _serverTable.Columns.Add(new DataColumn("UserID", typeof(string)));
            _serverTable.Columns.Add(new DataColumn("PSWD", typeof(string)));
            _xmlPath = AppDomain.CurrentDomain.BaseDirectory + "\\UFServer.xml";
            if (File.Exists(_xmlPath))
            {
                _serverTable.ReadXml(_xmlPath);
            }
            //{
            //    _serverTable.WriteXml(_xmlPath);
            //}
            //
            ShowServerName(_serverTable);    
            this.Disposed += new EventHandler(UFServerForm_Disposed);
        }

        void UFServerForm_Disposed(object sender, EventArgs e)
        {
            if (_conn != null && _conn.State == ConnectionState.Open)
            {
                _conn.Dispose();
            }
        }

        private void tbFindServer_Click(object sender, EventArgs e)
        {
            //ServerName/InstanceName/IsClustered/Version
            DataTable dt = SqlDataSourceEnumerator.Instance.GetDataSources();
            ShowServerName(dt);
            this.cmbServerList.DroppedDown = true;

        }

        private void ShowServerName(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return;
            this.cmbAccount.DataSource = null;
            this.cmbServerList.Items.Clear();
            this.cmbServerList.Text = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                this.cmbServerList.Items.Add(dr["ServerName"]);
            }
            this.cmbServerList.SelectedIndex = 0;
        }

        private void cmbServerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cmbAccount.DataSource = null;
            DataRow[] drs = _serverTable.Select("ServerName='" + cmbServerList.Text + "'");
            if (drs != null && drs.Count<DataRow>() > 0)
            {
                this.tbUser.Text = drs[0]["UserID"].ToString();
                this.tbpswd.Text = drs[0]["PSWD"].ToString();
            }
            else
            {
                this.tbUser.Text = "sa";
                this.tbpswd.Text = "longwin20020426";
            }

        }

        List<UFCustomer> _uFCustomerList;

        public List<UFCustomer> UFCustomerList
        {
            get { return _uFCustomerList; }
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if ( this.cmbServerList.Text.Trim() == string.Empty)
            {
                MessageBox.Show("服务不能为空,请选择", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.tbUser.Text.Trim() == string.Empty)
            {
                MessageBox.Show("登录名为不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.tbpswd.Text.Trim() == string.Empty)
            {
                MessageBox.Show("密码名不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.cmbAccount.SelectedValue == null
                || this.cmbAccount.SelectedValue.ToString() == string.Empty)
            {
                MessageBox.Show("帐套不能为空,请选择", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (_conn == null
                           || _conn.State == ConnectionState.Closed)
                {
                    _conn = new System.Data.SqlClient.SqlConnection();
                    _conn.ConnectionString = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}"
                        , this.cmbServerList.Text
                        , this.cmbAccount.SelectedValue.ToString()
                        , this.tbUser.Text.Trim()
                        , this.tbpswd.Text.Trim());
                }
                else
                {
                    _conn.ChangeDatabase(this.cmbAccount.SelectedValue.ToString());
                }

                if (_conn.State != ConnectionState.Open)
                    _conn.Open();
                DataTable ufCustomerTable;
                string sql = "SELECT cCusCode as Code,cCusName as Name FROM Customer ";
                if ( _isCost)
                {
                    sql = @" SELECT  Person.cPersonName AS Code, Department.cDepName  AS Name
                             FROM Department RIGHT JOIN Person ON Department.cDepCode = Person.cDepCode 
                             order by Person.cPersonCode" ;
                }
                SqlCommand cmd = new SqlCommand(sql, _conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                ufCustomerTable = new DataTable();
                da.Fill(ufCustomerTable);
                _conn.Close();
                _uFCustomerList = UFCustomer.GetUFCustomerForDataTable(ufCustomerTable);

                DataRow[] drs = _serverTable.Select("ServerName='" + cmbServerList.Text + "'");
                if (drs != null && drs.Count<DataRow>() > 0)
                {
                    drs[0]["UserID"] = this.tbUser.Text;
                    drs[0]["PSWD"] = this.tbpswd.Text;
                    _serverTable.AcceptChanges();
                    _serverTable.WriteXml(_xmlPath);
                }
                else
                {
                    DataRow dr = _serverTable.Rows.Add(cmbServerList.Text, this.tbUser.Text, this.tbpswd.Text);
                    dr.EndEdit();
                    _serverTable.AcceptChanges();
                    _serverTable.WriteXml(_xmlPath);
                }
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                if (_conn != null & _conn.State == ConnectionState.Open)
                    _conn.Close();
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        System.Data.SqlClient.SqlConnection _conn;
        private void cmbAccount_DropDown(object sender, EventArgs e)
        {
            if (this.cmbAccount.DataSource == null)
                RefershAccount();

            //foreach (DataRow dr in dt.Rows)
            //{
            //    this.cmbAccount.Items.Add(dr["ServerName"]);
            //}    
            

        }

        private void RefershAccount()
        {

            if (this.cmbServerList.Text.Trim() == string.Empty)
            {
                MessageBox.Show("服务不能为空,请选择", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_conn == null
            || _conn.State == ConnectionState.Closed)
            {
                _conn = new System.Data.SqlClient.SqlConnection();
                _conn.ConnectionString = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}"
                    , this.cmbServerList.Text
                    , "UFSystem"
                    , this.tbUser.Text.Trim()
                    , this.tbpswd.Text.Trim());
            }
            if (_conn.State != ConnectionState.Open)
                _conn.Open();
            //ua_account_sub(账套年度表)-->UA_Period(会计期间表)-->ua_account(账套表)-->ua_holdauth(操作员权限分配表)  
            string sql = @"SELECT CAST(A.iYear AS VARCHAR(5))+'_'+B.cAcc_name AS AccountName ,DbName = 'UFData_'+A.cAcc_Id+'_'+CAST(A.iYear AS VARCHAR(5))
                            FROM (SELECT DISTINCT cAcc_Id,iyear FROM UA_Period Where (bIsDelete=0 OR bIsDelete IS NULL) ) A
                            INNER JOIN dbo.UA_Account B ON A.cAcc_Id=B.cAcc_Id
                            ORDER BY 1 DESC ";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (_conn != null & _conn.State == ConnectionState.Open)
                _conn.Close();
            if (dt.Rows.Count <= 0)
                return;
            this.cmbAccount.DataSource = dt;
            this.cmbAccount.DisplayMember = "AccountName";
            this.cmbAccount.ValueMember = "DbName";
        }

        private void btAccount_Click(object sender, EventArgs e)
        {
            this.cmbAccount.DataSource = null;
            RefershAccount();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }


    public class UFCustomer
    {
        string _customerCode;
        public UFCustomer(string customerCode, string customerName)
        {
            _customerCode = customerCode.ToUpper();
            _customerName = customerName;
        }
        public string CustomerCode
        {
            get { return _customerCode; }
            set { _customerCode = value.ToUpper(); }
        }
        string _customerName;

        public string CustomerName
        {
            get { return _customerName; }
            set { _customerName = value; }
        }

        public static List<UFCustomer> GetUFCustomerForDataTable(DataTable  dt)
        {
            List<UFCustomer> list = new List<UFCustomer>();
            if (dt == null
                || dt.Rows.Count == 0)
            {
                return list;
            }

            
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new UFCustomer(dr["Code"].ToString(), dr["Name"].ToString()));
            }
            return list;
        }
    }
}
