using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.Framework.ClientComponents.UIManagement;
using System.Data.Odbc;
using DevExpress.XtraEditors.Controls;
using System.Data.SqlClient;
using System.IO;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FAM.UI.Invoice
{
    public partial class ImportTaxInfoPart : BasePart
    {
        public ImportTaxInfoPart()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
                //获取税控客户资料
                GetCustomer();
            }
        }
        private void InitControls()
        {
            rgType.SelectedIndex = 0;
            cmbImportType.Properties.Items.Clear();

            txtImportName.Text = LocalData.UserInfo.DefaultCompanyName;
            txtImportName.Properties.ReadOnly = true;

            cmbImportType.Properties.Items.Add(new ImageComboBoxItem("专用发票"));
            cmbImportType.Properties.Items.Add(new ImageComboBoxItem("普通发票"));
            cmbImportType.SelectedIndex = 0;
        }
        private int RGType
        {
            get
            {
                return rgType.SelectedIndex;
            }
        }
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
             openFileDialog1.Filter = "Paradox db文件|*.db";
           
            if(RGType==0)
            {
                openFileDialog1.FileName="客户编码.DB";
            }
            else if(RGType==1)
            {
                openFileDialog1.FileName="操作员.DB";
            }
            else if(RGType==2)
            {
                openFileDialog1.FileName="系统税务信息.DB";
            }
            else if (RGType == 3)
            {
                openFileDialog1.FileName = "商品编码.DB";
            }
            if (DialogResult.OK != openFileDialog1.ShowDialog())
            {
                return;
            }

            txtFileName.Text = openFileDialog1.FileName;
            string fileName = txtFileName.Text.Trim().ToLower();

            string dbConnStr = "";

            int startPosition = fileName.LastIndexOf("\\");

             dbConnStr = "Driver={Microsoft Paradox Driver (*.db )}; DriverID=538; Fil=Paradox 5.X; "
                    + " DefaultDir=" + fileName.Substring(0, startPosition + 1)
                    + " ; Dbq=" + fileName.Substring(0, startPosition + 1) + "; CollatingSequence=ASCII; PWD=1FFKEC123Q4C26G;";

             if (RGType == 0)
             {
                 SearchCustomer(dbConnStr, fileName, startPosition);
             }
             else if (RGType == 1)
             {
                 ImportUserInfo(dbConnStr, fileName, startPosition);
             }
             else if (RGType == 2)
             {
                 ImportBankAccountInfo(dbConnStr, fileName, startPosition);
             }
             else if (RGType == 3)
             {
                 ImportCargoInfo(dbConnStr, fileName, startPosition);
             }
              

        }

        string xmlCustomerCode = Application.StartupPath + "\\CustomerCode.xml";
        private void GetCustomer()
        {
            DataSet ds = new DataSet();
            ds.ReadXml(xmlCustomerCode);

            DataTable dt = ds.Tables[0];
            List<ImportTaxCustomerInfo> list = (from d in dt.AsEnumerable()
                                                select new ImportTaxCustomerInfo
                                                {
                                                    IsSelect = false,
                                                    Code = d.Field<string>("编码"),
                                                    Name = d.Field<string>("名称"),
                                                    ShortName = d.Field<string>("简码"),
                                                    TaxNo = d.Field<string>("税号"),
                                                    AddressTel = d.Field<string>("地址电话"),
                                                    BankAccountNo = d.Field<string>("银行帐号"),
                                                    Remark = d.Field<string>("备注")
                                                }).ToList();

            bsList.DataSource = list;
            bsList.ResetBindings(false);
        }
        /// <summary>
        /// 查询客户信息
        /// </summary>
        private void SearchCustomer(string con, string fileName, int startPosition)
        {
            string strSelect = "select * from " + fileName.Substring(startPosition + 1);
            OdbcConnection odbcConn = new OdbcConnection(con);
            odbcConn.Open();
            OdbcDataAdapter odbcAdapter = new OdbcDataAdapter(strSelect, odbcConn);
            DataSet m_dsSrc = new DataSet();
            odbcAdapter.Fill(m_dsSrc, "PadaboxDB");
            odbcConn.Close(); ;

            DataTable dt = m_dsSrc.Tables[0];

            List<ImportTaxCustomerInfo> list = (from d in dt.AsEnumerable()
                                             select new ImportTaxCustomerInfo
                                             {
                                                 IsSelect = false,
                                                 Code = d.Field<string>("编码"),
                                                 Name = d.Field<string>("名称"),
                                                 TaxNo = d.Field<string>("税号"),
                                                 AddressTel = d.Field<string>("地址电话"),
                                                 BankAccountNo = d.Field<string>("银行帐号")
                                             }).ToList();

            bsList.DataSource = list;
            bsList.ResetBindings(false);
        }
        /// <summary>
        /// 导入用户信息
        /// </summary>
        private void ImportUserInfo(string con, string fileName, int startPosition)
        {
            string strSelect = "select 0 as ID,名称 as Name from " + fileName.Substring(startPosition + 1);
            OdbcConnection odbcConn = new OdbcConnection(con);
            odbcConn.Open();
            OdbcDataAdapter odbcAdapter = new OdbcDataAdapter(strSelect, odbcConn);
            DataSet m_dsSrc = new DataSet("TaxUserList");
            odbcAdapter.Fill(m_dsSrc, "TaxUserInfo");
            odbcConn.Close(); ;

            DataTable dt = m_dsSrc.Tables[0];
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                dr["ID"] = i;
                i++;
            }
            string userFileName =AppDomain.CurrentDomain.BaseDirectory+"\\TaxUserInfo.xml";
            if (File.Exists(userFileName))
            {
                File.Delete(userFileName);
            }
            dt.WriteXml(userFileName);

            XtraMessageBox.Show("导入成功");

        }
        /// <summary>
        /// 导入银行帐号信息
        /// </summary>
        private void ImportBankAccountInfo(string con, string fileName, int startPosition)
        {
            string strSelect = "select 银行帐号 from " + fileName.Substring(startPosition + 1);
            OdbcConnection odbcConn = new OdbcConnection(con);
            odbcConn.Open();
            OdbcDataAdapter odbcAdapter = new OdbcDataAdapter(strSelect, odbcConn);
            DataSet m_dsSrc = new DataSet();
            odbcAdapter.Fill(m_dsSrc);
            odbcConn.Close(); ;

            DataTable dt = m_dsSrc.Tables[0];
            string bankAccountList = dt.Rows[0][0].ToString();
            string[] bankAccounts = bankAccountList.Split(Environment.NewLine.ToCharArray());

            DataTable dtBankInfo = new DataTable("TaxBankAccountInfo");
            DataColumn dcID = new DataColumn("ID", typeof(int));
            DataColumn dcName = new DataColumn("Name", typeof(string));
            dtBankInfo.Columns.Add(dcID);
            dtBankInfo.Columns.Add(dcName);

            int i = 0;
            foreach (string item in bankAccounts)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    DataRow dr = dtBankInfo.NewRow();
                    dr["ID"] = i;
                    dr["Name"] = item;

                    dtBankInfo.Rows.Add(dr);
                    i++;
                }
            }

            string taxBankAccountName = AppDomain.CurrentDomain.BaseDirectory + "\\TaxBankAccountInfo.xml";
            if (File.Exists(taxBankAccountName))
            {
                File.Delete(taxBankAccountName);
            }

            dtBankInfo.WriteXml(taxBankAccountName);

            XtraMessageBox.Show("导入成功");
        }

        /// <summary>
        /// 导入商品编码
        /// </summary>
        private void ImportCargoInfo(string con, string fileName, int startPosition)
        {
            string strSelect = "select 编码 as Code,名称 as Name from " + fileName.Substring(startPosition + 1);
            OdbcConnection odbcConn = new OdbcConnection(con);
            odbcConn.Open();
            OdbcDataAdapter odbcAdapter = new OdbcDataAdapter(strSelect, odbcConn);
            DataSet m_dsSrc = new DataSet("TaxCargoList");
            odbcAdapter.Fill(m_dsSrc);
            odbcConn.Close(); ;


            DataTable dt = m_dsSrc.Tables[0];
            dt.TableName = "TaxCargoInfo";

            string taxCargoName = AppDomain.CurrentDomain.BaseDirectory + "\\TaxCargoInfo.xml";
            if (File.Exists(taxCargoName))
            {
                File.Delete(taxCargoName);
            }

            dt.WriteXml(taxCargoName);

            XtraMessageBox.Show("导入成功");
        }
        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAll_Click(object sender, EventArgs e)
        {
            int[] rowIndexs = gvMain.GetSelectedRows();

            foreach (var item in rowIndexs)
            {
                ImportTaxCustomerInfo dr = gvMain.GetRow(item) as ImportTaxCustomerInfo;
                if (dr != null)
                {
                    dr.IsSelect = true;
                }
            }
            bsList.ResetBindings(false);   
        }
        /// <summary>
        /// 反选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUnAll_Click(object sender, EventArgs e)
        {
            int[] rowIndexs = gvMain.GetSelectedRows();

            foreach (var item in rowIndexs)
            {
                ImportTaxCustomerInfo dr = gvMain.GetRow(item) as ImportTaxCustomerInfo;
                if (dr != null)
                {
                    dr.IsSelect = !dr.IsSelect;
                }
            }
            bsList.ResetBindings(false);  
        }
        /// <summary>
        /// 导入客户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbImportType.Text))
            {
                XtraMessageBox.Show("请选择导入发票类型");
                cmbImportType.Focus();
            }
            //if (string.IsNullOrEmpty(this.txtImportName.Text))
            //{
            //    DevExpress.XtraEditors.XtraMessageBox.Show("请输入创建人");
            //    this.txtImportName.Focus();
            //}
            List<ImportTaxCustomerInfo> list = bsList.DataSource as List<ImportTaxCustomerInfo>;
            if (list == null)
            {
                list = new List<ImportTaxCustomerInfo>();
            }
            list = (from d in list where d.IsSelect == true select d).ToList();

            SqlConnection sqlCon = new SqlConnection("server=cndb.cityocean.com,3344;database=ICP3;user id=icp3;Pwd=LONGWINicp@1124;integrated security=true;Trusted_Connection=false;");            
            //SqlConnection sqlCon = new SqlConnection("server=192.168.0.15,3344;database=ICP3_Test;user id=sa;Pwd=longwin;integrated security=true;Trusted_Connection=false;");
            string sqlSelect = "select * from pub.TaxCtustomerInvoiceTitles";
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlSelect, sqlCon);
            DataSet ds = new DataSet();
            sqlDa.Fill(ds);

            DataTable dt = ds.Tables[0];
            int type = 1;
            if (cmbImportType.Text.Contains("普通发票"))
            {
                type = 2;
            }

            List<ImportTaxCustomerInfo> DBList = (from d in dt.AsEnumerable()
                                                  select new ImportTaxCustomerInfo
                                               {
                                                   Code = d.Field<string>("Code"),
                                                   Name = d.Field<string>("Name"),
                                                   TaxNo = d.Field<string>("TaxNo"),
                                                   AddressTel = d.Field<string>("AddressTel"),
                                                   BankAccountNo = d.Field<string>("BankAccountNo"),
                                                   CreateBy = d.Field<string>("CreateBy")
                                               }).ToList();

            int insertCount = 0;
            int updateCount = 0;
            foreach (ImportTaxCustomerInfo item in list)
            {      
                if (string.IsNullOrEmpty(item.Name) ||
                   string.IsNullOrEmpty(item.Code) ||
                    string.IsNullOrEmpty(item.AddressTel) ||
                    string.IsNullOrEmpty(item.BankAccountNo))
                {
                    //continue;
                }
                string strVal = string.Empty;
                int i = (from b in DBList where b.TaxNo == item.TaxNo && b.CreateBy == txtImportName.Text select b).Count();
                if (i == 0)
                {
                    strVal = "insert into pub.TaxCtustomerInvoiceTitles values(newID(),'" + item.Code + "','" + type + "','" + item.Name + "','" + item.TaxNo + "','" + item.AddressTel + "','" + item.BankAccountNo + "','" + txtImportName.Text + "',GetDate())";
                    insertCount++;
                }
                else
                {
                    strVal = "update  pub.TaxCtustomerInvoiceTitles set Code='" + item.Code
                        + "',Type='" + type + "',Name='" + item.Name + "',AddressTel='" + item.AddressTel + "',BankAccountNo='" + item.BankAccountNo
                        + "' where TaxNo='" + item.TaxNo + "' and  CreateBy='" + txtImportName.Text + "'";

                    updateCount++;
                }
                //int i = (from b in DBList where b.TaxNo == item.TaxNo && b.CreateBy == LocalData.UserInfo.DefaultCompanyID.ToString() select b).Count();
                //if (i == 0)
                //{
                //    strVal = "insert into pub.TaxCtustomerInvoiceTitles values(newID(),'" + item.Code + "','" + type + "','" + item.Name + "','" + item.TaxNo + "','" + item.AddressTel + "','" + item.BankAccountNo + "','" + LocalData.UserInfo.DefaultCompanyID + "',GetDate())";
                //    insertCount++;
                //}
                //else
                //{
                //    strVal = "update  pub.TaxCtustomerInvoiceTitles set Code='" + item.Code
                //        + "',Type='" + type + "',Name='" + item.Name + "',AddressTel='" + item.AddressTel + "',BankAccountNo='" + item.BankAccountNo
                //        + "' where TaxNo='" + item.TaxNo + "' and  CreateBy='" + LocalData.UserInfo.DefaultCompanyID + "'";

                //    updateCount++;
                //}
                sqlCon.Open();
                SqlCommand sqlComm = new SqlCommand(strVal, sqlCon);
                sqlComm.ExecuteNonQuery();
                sqlCon.Close();

                DBList.Add(item);
            }
            string message="导入成功"+Environment.NewLine+"本次导入:"+insertCount.ToString()+"条数据."+Environment.NewLine+"本次更新:"+updateCount.ToString()+"条数据.";
            XtraMessageBox.Show(message);
        }
        


    }
    /// <summary>
    /// 导入税控系统客户信息
    /// </summary>
    public class ImportTaxCustomerInfo
    {
        public bool IsSelect
        {
            get;
            set;
        }
        public string Code
        { get; set; }

        public string Name
        {
            get;
            set;
        }
        public string ShortName
        {
            get;
            set;
        }
        public string AddressTel
        {
            get;
            set;
        }
        public string TaxNo
        {
            get;
            set;
        }
        public string BankAccountNo
        {
            get;
            set;
        }
        public string Remark
        {
            get;
            set;
        }
        public string CreateBy
        {
            get;
            set;
        }
    }
}
