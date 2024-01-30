using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.ClientComponents.UIManagement;
using ICP.FAM.ServiceInterface;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.Common.UI;
using ICP.FAM.ServiceInterface.DataObjects;
using System.Diagnostics;
using System.Data.Odbc;

namespace ICP.FAM.UI.Invoice
{
    public partial class InvoiceContrast : BasePart
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public string Path = string.Empty;

        public IBusinessInfoProviderFactory BusinessInfoProviderFactory
        {
            get
            {
                return ServiceClient.GetClientService<IBusinessInfoProviderFactory>();
            }
        }

        public IConfigureService configureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }


        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }


        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }

        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }
        #endregion

        public InvoiceContrast()
        {
            InitializeComponent();
            Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }

            };
        }

        private void InvoiceContrast_Load(object sender, EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
            }
        }

        private void InitControls()
        {
            ICPCommUIHelper.BindCompanyByUser(cmbCompany, false);
            cmbCompany.ShowSelectedValue(LocalData.UserInfo.DefaultCompanyID, LocalData.UserInfo.DefaultCompanyName);
            DateEnd.DateTime = DateTime.Now;
            DateStart.DateTime = DateTime.Now.AddDays(1 - DateTime.Now.Day);
            GetPath();
        }

        private void GetPath()
        {
            Process[] pro = Process.GetProcesses();
            for (int i = 0; i < pro.Length; i++)
            {
                if (pro[i].ProcessName == "Kp.exe")
                {
                    Path = pro[i].MainModule.FileName.Replace("Kp.exe", @"DATABASE\DEFAULT\WORK\销项发票.DB");
                }
            }
        
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cmbCompany.EditValue == null)
            {
                MessageBox.Show(LocalData.IsEnglish ? "Please choose a company!" : "请选择公司！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            int startPosition = Path.LastIndexOf("\\");
            string dbConnStr = "Driver={Microsoft Paradox Driver (*.db )}; DriverID=538; Fil=Paradox 5.X; "
              + " DefaultDir=" + Path.Substring(0, startPosition + 1)
              + " ; Dbq=" + Path.Substring(0, startPosition + 1) + "; CollatingSequence=ASCII; PWD=1FFKEC123Q4C26G;";

            string strSelect = @"SELECT InvoiceNo=发票号码,Amount=合计金额 FROM 销项发票.DB 
	                                WHERE 开票日期>='开始日期' AND 开票日期<='结束日期'
	                                AND 发票种类='c'";
            OdbcConnection odbcConn = new OdbcConnection(dbConnStr);
            odbcConn.Open();
            OdbcDataAdapter odbcAdapter = new OdbcDataAdapter(strSelect, odbcConn);
            DataSet m_dsSrc = new DataSet();
            odbcAdapter.Fill(m_dsSrc, "InvoiceList");
            odbcConn.Close(); ;

            DataTable dt = m_dsSrc.Tables[0];

            List<ShortInvoiceInfo> list = (from d in dt.AsEnumerable()
                                           select new ShortInvoiceInfo
                                           {
                                               InvoiceNo = d.Field<string>("发票号码"),
                                               Amount = d.Field<string>("合计金额"),
                                           }).ToList();

            List<ShortInvoiceInfo> icpList = FinanceService.GetCompanyInvoiceListByDate((Guid)cmbCompany.EditValue, DateStart.DateTime, DateEnd.DateTime);


        }

        class InfoList
        {
            public string ICPNo { get; set; }

            public decimal ICPAmount { get; set; }

            public string SKNo { get; set; }

            public decimal SKAmount { get; set; }
        }
    }
}
