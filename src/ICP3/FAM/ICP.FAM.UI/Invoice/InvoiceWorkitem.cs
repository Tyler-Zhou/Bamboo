using System.Collections.Generic;
using DevExpress.XtraEditors;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using System.Diagnostics;
using System.Windows.Forms;
using System;
using System.IO;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using ICP.Business.Common.UI.Document;

namespace ICP.FAM.UI
{
    class InvoiceWorkitem : WorkItem
    {

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            InvoiceMainWorkSpace mainSpce = SmartParts.Get<InvoiceMainWorkSpace>("InvoiceMainWorkSpace");
            if (mainSpce == null)
            {
                mainSpce = SmartParts.AddNew<InvoiceMainWorkSpace>("InvoiceMainWorkSpace");

                #region AddPart

                InvoiceToolBar toolBar = SmartParts.AddNew<InvoiceToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)Workspaces[InvoiceWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                InvoiceListPart listPart = SmartParts.AddNew<InvoiceListPart>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[InvoiceWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                InvoiceSearchPart searchPart = SmartParts.AddNew<InvoiceSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)Workspaces[InvoiceWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);

                UCDocumentList documentListPart = Items.AddNew<UCDocumentList>();
                documentListPart.simple = true;
                DocumentListPresenter documentPresenter = Items.AddNew<DocumentListPresenter>();
                documentPresenter.ucList = documentListPart;
                documentListPart.Presenter = documentPresenter;
                IWorkspace documentListWorkspace = (IWorkspace)Workspaces[InvoiceWorkSpaceConstants.DocumentListWorkspace];
                documentListWorkspace.Show(documentListPart);
                documentListPart.SetSimple();

                #endregion

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Invoice List" : "发票列表";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                InvoiceUIAdapter bookingAdapter = new InvoiceUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(documentListPart.GetType().Name, documentListPart);

                bookingAdapter.Init(dic);
            }
            else
            {
                Workspaces[ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }
    }

    /// <summary>
    /// 命常量
    /// </summary>
    public class InvoiceCommandConstants
    {

        public const string Command_InvoiceAdd = "Command_InvoiceAdd";

        public const string Command_InvoiceEdit = "Command_InvoiceEdit";

        public const string Command_InvoiceCancel = "Command_InvoiceCancel";

        public const string Command_Express = "Command_Express";

        public const string Command_InvoiceRefreshData = "Command_InvoiceRefreshData";

        public const string Command_InvoiceShowSearch = "Command_InvoiceShowSearch";

        public const string Command_PrintInvoice = "Command_PrintInvoice";

        public const string Command_PrintVientam = "Command_PrintVientam";

        public const string Command_FinderConfirm = "Command_FinderConfirm";

        public const string Command_PreviewInvoice = "Command_PreviewInvoice";

        public const string Command_InvoiceCount = "Command_PreviewCount";

        public const string Command_GetInvoiceNo = "Command_GetInvoiceNo";

        public const string Command_DutyFreeDetail = "Command_DutyFreeDetail";

        public const string Command_OperationInvoice = "Command_OperationInvoice";

        public const string InvoiceReviewName = "InvoiceReviewName";
        public const string InvoiceReceivablesName = "InvoiceReceivablesName";

        public const string ClassName = "ClassName";
        public const string UpdateTime = "UpdateTime";
        public const string KPGoods_RMB = "KPGoods_RMB";
        public const string KPGoods_USD = "KPGoods_USD";

        public const string KPGoods_RMB_P = "KPGoods_RMB_P";
        public const string KPGoods_USD_P = "KPGoods_USD_P";

        public const string KPGoods_RMB_Z = "KPGoods_RMB_Z";
        public const string KPGoods_USD_Z = "KPGoods_USD_Z";
    }
    /// <summary>
    /// WorkSpace常量
    /// </summary>
    public class InvoiceWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";
        public const string DocumentListWorkspace = "DocumentListWorkspace";

        public const string InvoiceDetailListWorkspace = "InvoiceDetailListWorkspace";
    }

    /// <summary>
    /// UI适配器
    /// </summary>
    public class InvoiceUIAdapter:IDisposable
    {

        #region parts

        IToolBar _toolBar;
        ISearchPart _searchPart;
        InvoiceListPart _mainListPart;
        UCDocumentList _DocumentListPart;


        #endregion

        #region interface

        public void Init(Dictionary<string, object> controls)
        {
            _toolBar = (IToolBar)controls[typeof(InvoiceToolBar).Name];
            _searchPart = (ISearchPart)controls[typeof(InvoiceSearchPart).Name];
            _mainListPart = (InvoiceListPart)controls[typeof(InvoiceListPart).Name];
            _DocumentListPart = (UCDocumentList)controls[typeof(UCDocumentList).Name];
            RefreshBarEnabled(_toolBar, null);
            #region Connection

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                InvoiceList listData = data as InvoiceList;

                RefreshBarEnabled(_toolBar, listData);

                BusinessOperationContext context = new BusinessOperationContext();
                context.OperationID = listData.ID;
                context.FormId = listData.ID;
                context.OperationType = OperationType.Other;
                FCM.Common.UI.FCMUIUtility.SetDocumentListDataSource(_DocumentListPart, context);
                

            };

            _mainListPart.Selected += delegate(object sender, object data)
            {

            };
            #endregion

            #region _mainListPart.InvokeGetData
            _mainListPart.InvokeGetData += delegate(object sender, object data)
            {
                _searchPart.RaiseSearched(data);//快捷键
            };
            #endregion

            #region
            _mainListPart.KeyDown += new KeyEventHandler(_mainListPart_KeyDown);
            #endregion

            #region _searchPart.OnSearched
            _searchPart.OnSearched += delegate(object sender, object results)
            {
                _mainListPart.DataSource = results;
            };

            #endregion


            #endregion
        }
        void _mainListPart_KeyDown(object sender, KeyEventArgs e)
        {

            if (sender != null)
            {
                Dictionary<string, object> keyValue = sender as Dictionary<string, object>;
                if (keyValue != null)
                {
                    _searchPart.Init(keyValue);
                    _searchPart.RaiseSearched();
                }
            }
        }
        private void RefreshBarEnabled(IToolBar toolBar, InvoiceList listData)
        {
            if (listData == null || listData.IsNew)
            {
                toolBar.SetEnable("barEdit", false);
                toolBar.SetEnable("barPrint", false);
                toolBar.SetEnable("barCancel", false);
                toolBar.SetEnable("barExpress", false);
                toolBar.SetEnable("barPreview1", false);
                toolBar.SetEnable("barRefresh", false);
            }
            else
            {
                toolBar.SetEnable("barEdit", true);
                toolBar.SetEnable("barPrint", true);
                toolBar.SetEnable("barCancel", true);
                toolBar.SetEnable("barExpress", true);
                toolBar.SetEnable("barPreview1",true);
                toolBar.SetEnable("barRefresh", true);
                if (listData.IsValid)
                {
                    toolBar.SetText("barCancel", LocalData.IsEnglish ? "Invalid" : "作废");
                }
                else
                {
                    toolBar.SetText("barCancel", LocalData.IsEnglish ? "Activation" : "激活");
                    toolBar.SetEnable("barPrint", false);
                    toolBar.SetEnable("barExpress", false);
                    toolBar.SetEnable("barEditInvoiceNo", false);
                    toolBar.SetEnable("barSave", false);
                    toolBar.SetEnable("barPreview1", false);
                    //toolBar.SetEnable("barEdit",false);
                }
            }

        }
        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            if (_mainListPart != null)
            {
                _mainListPart.KeyDown -= _mainListPart_KeyDown;
                _mainListPart = null;
            }
            _searchPart = null;
            _toolBar = null;
        }

        #endregion
    }

    /// <summary>
    /// 税务系统公共类
    /// </summary>
    public class TasSystemCommon
    {
        static string xmlFileName = Application.StartupPath + "\\TaxArguments.xml";
        static string invoiceFileName = Application.StartupPath + "\\TaxInvoiceNoInfo.xml";
        static Timer timer;
        static int tcount = 0;
        static int processID = 0;
        static IFinanceService finService;
        public static List<InvoiceInfo> List = new List<InvoiceInfo>();
        static Control control;
        public static bool isSuccess = false;
        /// <summary>
        /// 线程ID
        /// </summary>
        static int theradID = 0;

        /// <summary>
        /// 获得发票号
        /// </summary>
        /// <param name="systemNoList"></param>
        /// <returns></returns>
        public static void GetTaxInvoiceNo(Control cl,IFinanceService financeService, List<string> noList)
        {
            theradID = LoadingServce.ShowLoadingForm("正在获取发票号...");
            control = cl;
            finService = financeService;
            isSuccess = false;

            List<string> systemNoList = new List<string>();
            List<string> invoiceNoList = new List<string>();

            string strSql = string.Empty;

            #region 获得配置信息
            ///路径库路径
            string kpDBPath = string.Empty;

            //通过KP.EXE进来获取数据库路径
            Process[] ps = Process.GetProcessesByName("kp");
            if (ps != null && ps.Length > 0)
            {
                //KP已启动，
                foreach (Process p in ps)
                {
                    kpDBPath = p.MainModule.FileName.ToString();
                    kpDBPath = kpDBPath.Substring(0, kpDBPath.Length - 10);
                    kpDBPath = kpDBPath + "DATABASE\\DEFAULT\\WORK\\销项发票明细.DB";
                    break;
                }
            }
               
            if (string.IsNullOrEmpty(kpDBPath))
            {
                try
                {
                    LoadingServce.CloseLoadingForm(theradID);
                }
                catch { }
                //由于KP数据库路径无法保存在配置文件中，所以要求先启动税控系统.
                XtraMessageBox.Show("请先登陆税控系统,以便ICP获得税控系统信息");
                return ;
            }              
            #endregion

            #region 调用程序读取数据
            string inString = string.Empty;
            for (int i = 0; i < noList.Count; i++)
            {
                if (i != noList.Count - 1)
                {
                    inString = inString + "'" + noList[i].ToString() + "',";
                }
                else
                {
                    inString = inString + "'" + noList[i].ToString() + "'";
                }
            }

            strSql = "select 发票号码 as Invoice,规格型号 as SystemNo from 销项发票明细.DB where 规格型号 in(" + inString + ")";
            if (File.Exists(xmlFileName))
            {
                File.Delete(xmlFileName);
            }

            XElement rootArgument = new XElement("PArguments",
                  new XElement("PArgumentInfo",
                  new XElement("DBFileName", kpDBPath),
                  new XElement("StrSql", strSql)
            ));

            rootArgument.Save(xmlFileName);

            Process process = new Process();
            process.StartInfo.FileName =Application.StartupPath+"\\WinGetInvoiceInfo.exe";
            process.Start();
            processID = process.Id;
            tcount = 0;

            timer = new Timer();
            timer.Interval = 1000;
            timer.Enabled = true;
            timer.Tick -= new EventHandler(timer_Tick);
            timer.Tick += new EventHandler(timer_Tick);
            #endregion

        
        }
        /// <summary>
        /// 计时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void timer_Tick(object sender, EventArgs e)
        {
            tcount++;
            if (tcount > 60)
            {
                try
                {
                    LoadingServce.CloseLoadingForm(theradID);
                }
                catch { }
                timer.Enabled = false;
                isSuccess = true;

                LocalCommonServices.ErrorTrace.SetErrorInfo(control,"获取超时,请稍候重试");
            }

            Process[] processes = Process.GetProcesses();

            int n = (from d in processes where d.Id == processID select d).Count();
            if (n == 0)
            {
                timer.Enabled = false;
                GetInvoiceNo();
            }
        }
       /// <summary>
       /// 获得发票号
       /// </summary>
       public static void GetInvoiceNo()
        {
            List<string> invoiceNoList = new List<string>();
            List<string> systemNoList = new List<string>();
            XElement rootInvoice = XElement.Load(invoiceFileName);
            foreach (XElement item in rootInvoice.Elements())
            {
                string invoiceNo = item.Element("Invoice").Value;
                string systemNo = item.Element("SystemNo").Value;

                if (!string.IsNullOrEmpty(systemNo) &&
                   !string.IsNullOrEmpty(invoiceNo) &&
                    !systemNoList.Contains(systemNo))
                {
                    systemNoList.Add(systemNo);
                    invoiceNoList.Add(invoiceNo);
                }
            }

            #region 更新数据库

            if (invoiceNoList.Count > 0)
            {
                List = finService.SaveInvoiceNo(systemNoList.ToArray(),
                                                  invoiceNoList.ToArray(),
                                                  LocalData.UserInfo.LoginID,
                                                  LocalData.IsEnglish);
            }
            #endregion

            try
            {
                LoadingServce.CloseLoadingForm(theradID);
            }
            catch { }
            isSuccess = true;
            LocalCommonServices.Statusbar.SetStatusBarPanel("获取成功.");

        }

       private static List<Guid> OrdinaryInvoiceCompanyIDList;
        /// <summary>
        /// 只开普票的公司列表
        /// </summary>
        /// <returns></returns>
       public static List<Guid> GetOrdinaryInvoiceCompanyIDList
       {
           get
           {
               if (OrdinaryInvoiceCompanyIDList == null || OrdinaryInvoiceCompanyIDList.Count == 0)
               {
                   OrdinaryInvoiceCompanyIDList = new List<Guid>();
                   OrdinaryInvoiceCompanyIDList.Add(new Guid("41D7D3FE-183A-41CD-A725-EB6F728541EC"));//深圳
                   OrdinaryInvoiceCompanyIDList.Add(new Guid("D8D57403-D663-4A93-A927-144907B7963B"));//天津
                   OrdinaryInvoiceCompanyIDList.Add(new Guid("F289109A-C29E-4B0B-A41A-C22D9E70A72F"));//青岛
                   OrdinaryInvoiceCompanyIDList.Add(new Guid("62D46581-B6CC-477E-8A60-7375FACD9813"));//连云港
                   OrdinaryInvoiceCompanyIDList.Add(new Guid("A62A9F8E-E69C-4E6E-AD85-E75AED3C6CF9"));//宁波
               }

               return OrdinaryInvoiceCompanyIDList;
           }
       }

       private static List<Guid> ParallelInvoiceCompanyIDList;
        /// <summary>
        /// 专用发票与普通发票并行的公司
        /// </summary>
       public static List<Guid> GetParallelInvoiceCompanyIDList
       {
           get
           {
               if (ParallelInvoiceCompanyIDList == null || ParallelInvoiceCompanyIDList.Count == 0)
               {
                   ParallelInvoiceCompanyIDList = new List<Guid>();

                   ParallelInvoiceCompanyIDList.Add(new Guid("B13FAC2D-8250-4990-A622-5ECA00D3A030"));//上海
                   ParallelInvoiceCompanyIDList.Add(new Guid("B1AFAD8F-55DD-4E29-A250-EB82AB3971FE"));//大连
                   ParallelInvoiceCompanyIDList.Add(new Guid("FD69B51B-E71F-4040-8F4B-28447A003C93"));//广州
                   ParallelInvoiceCompanyIDList.Add(new Guid("2AFAC53A-2AF9-46ED-8C4B-7035FEDC0279"));//厦门
               
               }

               return ParallelInvoiceCompanyIDList;
           }
       }


    }


    public class LogHelper
    {
        public static void SaveLog(string message)
        {
            string str = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ":" + message + Environment.NewLine;
            string path = @"C:\log.txt";//文件路径

            StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8);
            sw.Write(str);
            sw.Close();


        }
    }
}
