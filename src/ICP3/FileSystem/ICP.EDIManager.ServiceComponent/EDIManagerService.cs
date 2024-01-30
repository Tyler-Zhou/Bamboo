using System.Text;
using ICP.EDIManager.ServiceInterface;
using ICP.EDIManager.ServiceInterface.Entity;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Server;
using ICP.Message.ServiceInterface;
using Microsoft.Practices.EnterpriseLibrary.Data;
using OpenPop.Mime;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using MessageState = ICP.EDIManager.ServiceInterface.Entity.MessageState;


namespace ICP.EDIManager.ServiceComponent
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Single)]
    public class EDIManagerService : IEDIManagerService
    {
        /// <summary>
        /// Inttra FTP Schedule URL
        /// </summary>
        string _urlSchedule4Inttra
        {
            get
            {
                string configValue = "";
                try
                {
                    configValue = INIConfig.IniReadValue(CommonConstants.SECTION_INTTRA_FTP, CommonConstants.INTTRA_FTP_SCHEDULEURL);
                }
                catch
                {
                    configValue = "";
                }
                return configValue;
            }
        }

        /// <summary>
        /// Inttra FTP UserID
        /// </summary>
        string _ftpUserID4Inttra
        {
            get
            {
                string configValue = "";
                try
                {
                    configValue = INIConfig.IniReadValue(CommonConstants.SECTION_INTTRA_FTP, CommonConstants.INTTRA_FTP_USERID);
                }
                catch
                {
                    configValue = "";
                }
                return configValue;
            }
        }

        /// <summary>
        /// Inttra FTP Password
        /// </summary>
        string _ftpPassword4Inttra
        {
            get
            {
                string configValue = "";
                try
                {
                    configValue = INIConfig.IniReadValue(CommonConstants.SECTION_INTTRA_FTP, CommonConstants.INTTRA_FTP_PASSWORD);
                }
                catch
                {
                    configValue = "";
                }
                return configValue;
            }
        }

        /// <summary>
        /// 是否下载船期
        /// </summary>
        bool _IsDownloadSchedule
        {
            get
            {
                bool configValue = false;
                try
                {
                    configValue = Convert.ToBoolean(INIConfig.IniReadValue(FrameworkCommandConstants.SECTION_TASKSCHEDULE, CommonConstants.TASKSCHEDULE_DOWNLOADSCHEDULE));
                }
                catch
                {
                    configValue = false;
                }
                return configValue;
            }
        }

        /// <summary>
        /// 是否解析船期
        /// </summary>
        bool _IsResolveSchedule
        {
            get
            {
                bool configValue = false;
                try
                {
                    configValue = Convert.ToBoolean(INIConfig.IniReadValue(FrameworkCommandConstants.SECTION_TASKSCHEDULE, CommonConstants.TASKSCHEDULE_RESOLVESCHEDULE));
                }
                catch
                {
                    configValue = false;
                }
                return configValue;
            }
        }

        /// <summary>
        /// 是否解析邮件
        /// </summary>
        bool _IsResolveEmail
        {
            get
            {
                bool configValue = false;
                try
                {
                    configValue = Convert.ToBoolean(INIConfig.IniReadValue(FrameworkCommandConstants.SECTION_TASKSCHEDULE, CommonConstants.TASKSCHEDULE_RESOLVEEMAIL));
                }
                catch
                {
                    configValue = false;
                }
                return configValue;
            }
        }
        /// <summary>
        /// 是否发送EDI通知
        /// </summary>
        bool _IsSendEDINotice
        {
            get
            {
                bool configValue = false;
                try
                {
                    configValue = Convert.ToBoolean(INIConfig.IniReadValue(FrameworkCommandConstants.SECTION_TASKSCHEDULE, CommonConstants.TASKSCHEDULE_SENDEDINOTICE));
                }
                catch
                {
                    configValue = false;
                }
                return configValue;
            }
        }

        Guid AdministratorID
        {
            get { return new Guid("4047CFAD-ECC8-E111-9D0D-0026551CA87B"); }
        }
        /// <summary>
        /// 口岸ID
        /// </summary>
        Guid CompanyID
        {
            get { return new Guid("41D7D3FE-183A-41CD-A725-EB6F728541EC"); }
        }

        Timer _timerDownloadSchedule;
        Timer _timerResolveSchedule;
        Timer _timerResolveEmail;
        Timer _timerSendEDINotice;
        /// <summary>
        /// 发送邮件
        /// </summary>
        IAuthenticateService AuthenticateService
        {
            get { return ServiceClient.GetService<IAuthenticateService>(); }
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        IMailBeeService MailBeeService
        {
            get { return ServiceClient.GetService<IMailBeeService>(); }
        }
        INIHelper iniConfig;
        /// <summary>
        /// INI 配置文件
        /// </summary>
        INIHelper INIConfig
        {
            get {
                return iniConfig ?? (iniConfig = new INIHelper(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FrameworkCommandConstants.CONFIG_ICPFILESERVICE)));
            }
        }

        public EDIManagerService()
        {
        }

        public void Download()
        {
            //下载船期:启动服务3小时后开始计时器;每次间隔12小时
            if (_IsDownloadSchedule)
            {
                TimeSpan tsDelayed = INIConfig.IniReadValue(FrameworkCommandConstants.SECTION_TASKSCHEDULE, CommonConstants.TASKSCHEDULE_DOWNLOADSCHEDULE_DELAYED).ToTimeSpan();
                TimeSpan tsInterval = INIConfig.IniReadValue(FrameworkCommandConstants.SECTION_TASKSCHEDULE, CommonConstants.TASKSCHEDULE_DOWNLOADSCHEDULE_INTERVAL).ToTimeSpan();
                _timerDownloadSchedule = new Timer(OnTimedEventDownLoadSchedule, this, tsDelayed, tsInterval);
            }

            //导入船期:启动服务4小时后开始计时器;每次间隔12小时
            if (_IsResolveSchedule)
            {
                TimeSpan tsDelayed = INIConfig.IniReadValue(FrameworkCommandConstants.SECTION_TASKSCHEDULE, CommonConstants.TASKSCHEDULE_RESOLVESCHEDULE_DELAYED).ToTimeSpan();
                TimeSpan tsInterval = INIConfig.IniReadValue(FrameworkCommandConstants.SECTION_TASKSCHEDULE, CommonConstants.TASKSCHEDULE_RESOLVESCHEDULE_INTERVAL).ToTimeSpan();
                _timerResolveSchedule = new Timer(OnTimedEventResolveSchedule, this, tsDelayed, tsInterval);
            }

            //读取EDI邮件:启动服务1小时后开始计时器;每次间隔10分钟
            if (_IsResolveEmail)
            {
                TimeSpan tsDelayed = INIConfig.IniReadValue(FrameworkCommandConstants.SECTION_TASKSCHEDULE, CommonConstants.TASKSCHEDULE_RESOLVEEMAIL_DELAYED).ToTimeSpan();
                TimeSpan tsInterval = INIConfig.IniReadValue(FrameworkCommandConstants.SECTION_TASKSCHEDULE, CommonConstants.TASKSCHEDULE_RESOLVEEMAIL_INTERVAL).ToTimeSpan();
                _timerResolveEmail = new Timer(OnTimedEventResolveEmail, this,tsDelayed, tsInterval);
            }

            //发送EDI通知:启动服务1小时后开始计时器;每次间隔10分钟
            if (_IsSendEDINotice)
            {
                SetApplicationContent();
                TimeSpan tsDelayed = INIConfig.IniReadValue(FrameworkCommandConstants.SECTION_TASKSCHEDULE, CommonConstants.TASKSCHEDULE_SENDEDINOTICE_DELAYED).ToTimeSpan();
                TimeSpan tsInterval = INIConfig.IniReadValue(FrameworkCommandConstants.SECTION_TASKSCHEDULE, CommonConstants.TASKSCHEDULE_SENDEDINOTICE_INTERVAL).ToTimeSpan();
                _timerSendEDINotice = new Timer(OnTimedEventSendEDINotice, this, tsDelayed, tsInterval);
            }
        }

        public void Dispose()
        {
            if (_timerDownloadSchedule != null) _timerDownloadSchedule.Dispose();
            if (_timerResolveSchedule != null) _timerResolveSchedule.Dispose();
            if (_timerResolveEmail != null) _timerResolveEmail.Dispose();
            if (_timerSendEDINotice != null) _timerSendEDINotice.Dispose();
        }


        /// <summary>
        /// 从FTP下载BookConfirm,TT数据  已弃用下载BookConfirm
        /// </summary>
        private void OnTimedEventDownLoadSchedule(object source)
        {
            try
            {
                string saveDir = INIConfig.IniReadValue(CommonConstants.SECTION_INTTRA_SCHEDULE, CommonConstants.INTTRASCHEDULE_SENDEDINOTICE);
                string regexScheduleFileName = INIConfig.IniReadValue(CommonConstants.SECTION_INTTRA_SCHEDULE, CommonConstants.INTTRASCHEDULE_REGEX_SCHEDULE_FILENAME);
                string pathSchedule = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, saveDir);

                if (!Directory.Exists(pathSchedule))
                {
                    Directory.CreateDirectory(pathSchedule);
                }

                FtpWebRequest reqFTPTT;
                FtpWebRequest reqFTPFileTT;
                string filename;
                //读取列表
                reqFTPTT = (FtpWebRequest)FtpWebRequest.Create(new Uri(_urlSchedule4Inttra));
                reqFTPTT.UseBinary = true;
                reqFTPTT.Credentials = new NetworkCredential(_ftpUserID4Inttra, _ftpPassword4Inttra);
                reqFTPTT.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                using (WebResponse resposett = reqFTPTT.GetResponse())
                {
                    using (StreamReader readertt = new StreamReader(resposett.GetResponseStream()))
                    {
                        string linett = readertt.ReadLine();

                        while (linett != null)
                        {
                            if (linett.Contains(DateTime.Now.ToString("yyyyMMdd")) || linett.Contains(DateTime.Now.AddDays(-1).ToString("yyyyMMdd")))
                            {
                                filename = Regex.Match(linett, regexScheduleFileName, RegexOptions.IgnoreCase).Value;
                                if (!File.Exists(pathSchedule + filename))
                                {
                                    #region 下载文件
                                    try
                                    {
                                        reqFTPFileTT = (FtpWebRequest)FtpWebRequest.Create(new Uri(_urlSchedule4Inttra + filename));
                                        reqFTPFileTT.Method = WebRequestMethods.Ftp.DownloadFile;
                                        reqFTPFileTT.UseBinary = true;
                                        reqFTPFileTT.Credentials = new NetworkCredential(_ftpUserID4Inttra, _ftpPassword4Inttra);
                                        using (FtpWebResponse responseFile = (FtpWebResponse)reqFTPFileTT.GetResponse())
                                        {
                                            using (FileStream outputStream = new FileStream(pathSchedule + filename, FileMode.Create))
                                            {
                                                using (Stream ftpStream = responseFile.GetResponseStream())
                                                {
                                                    long cl = responseFile.ContentLength;
                                                    int bufferSize = 2048;
                                                    int readCount;
                                                    byte[] buffer = new byte[bufferSize];
                                                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                                                    while (readCount > 0)
                                                    {
                                                        outputStream.Write(buffer, 0, readCount);
                                                        readCount = ftpStream.Read(buffer, 0, bufferSize);
                                                    }
                                                    outputStream.Flush();

                                                    Thread.Sleep(1000);

                                                    if (File.Exists(pathSchedule + filename))
                                                    {
                                                        long lSize = new FileInfo(pathSchedule + filename).Length;
                                                        if (cl <= lSize)//下载文件大小相同的话 删除FTP上记录
                                                        {
                                                            FtpWebRequest ftpWebRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(_urlSchedule4Inttra + filename));
                                                            ftpWebRequest.Credentials = new NetworkCredential(_ftpUserID4Inttra, _ftpPassword4Inttra);
                                                            ftpWebRequest.KeepAlive = false;
                                                            ftpWebRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                                                            FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
                                                            long size = ftpWebResponse.ContentLength;
                                                            Stream ftpResponseStream = ftpWebResponse.GetResponseStream();
                                                            StreamReader streamReader = new StreamReader(ftpResponseStream);
                                                            string result = String.Empty;
                                                            result = streamReader.ReadToEnd();
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        LogHelper.SaveLog("DownLoadSchedule", filename + " " + CommonHelper.BuildExceptionString(ex));
                                    } 
                                    #endregion
                                }
                            }

                            Thread.Sleep(2000);
                            linett = readertt.ReadLine();
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                LogHelper.SaveLog("DownLoadSchedule", CommonHelper.BuildExceptionString(ex));
            }
            catch (Exception ex)
            {
                LogHelper.SaveLog("DownLoadSchedule", CommonHelper.BuildExceptionString(ex));
            }
        }

        /// <summary>
        /// 解析船期XML
        /// </summary>
        private void OnTimedEventResolveSchedule(object source)
        {
            try
            {
                string saveDir = INIConfig.IniReadValue(CommonConstants.SECTION_INTTRA_SCHEDULE, CommonConstants.INTTRASCHEDULE_SENDEDINOTICE);
                string pathSchedule = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, saveDir);
                DataTable dtCrawlResult = new DataTable("SailingSchedule");
                dtCrawlResult.Columns.Add("CarrierCode", typeof(string));
                dtCrawlResult.Columns.Add("OriginCode", typeof(string));
                dtCrawlResult.Columns.Add("DestinationCode", typeof(string));
                dtCrawlResult.Columns.Add("IMO", typeof(string));
                dtCrawlResult.Columns.Add("VesselName", typeof(string));
                dtCrawlResult.Columns.Add("VoyageNumber", typeof(string));
                dtCrawlResult.Columns.Add("DepartureDate", typeof(DateTime));
                dtCrawlResult.Columns.Add("ArrivalDate", typeof(DateTime));
                dtCrawlResult.Columns.Add("TransitTime", typeof(Int32));
                dtCrawlResult.Columns.Add("RefID", typeof(Guid));
                dtCrawlResult.Columns.Add("OriginUnType", typeof(string));
                dtCrawlResult.Columns.Add("DestinationType", typeof(string));
                dtCrawlResult.Columns.Add("INTTRAID", typeof(string));
                dtCrawlResult.Columns.Add("Act", typeof(string));

                string INTTRAID;
                DirectoryInfo folderSchedule = new DirectoryInfo(pathSchedule);
                foreach (FileInfo file in folderSchedule.GetFiles("*.xml"))
                {
                    dtCrawlResult.Clear();
                    #region Foreach XML
                    bool isdown = CheckDown(file.Name);
                    if (isdown)
                    {
                        continue;
                    }

                    try
                    {
                        XmlDocument dom = new XmlDocument();
                        dom.Load(file.FullName);

                        XmlNode root = dom.ChildNodes[1];
                        XmlNode header = root.SelectSingleNode(@"Header");
                        XmlNode ID = header.SelectSingleNode(@"ID");
                        INTTRAID = ""+ID.InnerText;
                        XmlNode transactions = root.SelectSingleNode(@"Transactions");

                        XmlNodeList transactionList = transactions.SelectNodes(@"Transaction");
                        SaveFileLog(INTTRAID, file.Name, 0);
                        foreach (XmlNode transaction in transactionList)
                        {
                            XmlNodeList schsList = transaction.SelectNodes(@"Schs");
                            foreach (XmlNode schs in schsList)
                            {
                                XmlNodeList schList = schs.SelectNodes(@"Sch");
                                foreach (XmlNode sch in schList)
                                {
                                    #region Foreach SCH

                                    Guid refid = new Guid(sch.Attributes["Id"].Value);
                                    DataRow row = dtCrawlResult.NewRow();

                                    XmlNode carrier = transaction.SelectSingleNode(@"Carrier");
                                    row["INTTRAID"] = INTTRAID;
                                    row["CarrierCode"] = carrier.InnerText;
                                    row["VesselName"] = schs.Attributes["Vsl"].Value;
                                    row["VoyageNumber"] = schs.Attributes["Voy"].Value;
                                    if (schs.Attributes["Lloyds"] != null)
                                    {
                                        row["IMO"] = schs.Attributes["Lloyds"].Value;
                                    }
                                    else
                                    {
                                        row["IMO"] = "";
                                    }

                                    if (sch.Attributes["Act"] != null)
                                    {
                                        row["Act"] = sch.Attributes["Act"].Value;
                                    }
                                    else
                                    {
                                        row["Act"] = "C";
                                    }

                                    row["RefID"] = refid;

                                    XmlNode Orig = sch.SelectSingleNode(@"Orig");
                                    row["OriginCode"] = Orig.Attributes["Code"].Value;
                                    row["DepartureDate"] = Orig.Attributes["Departs"].Value;
                                    row["OriginUnType"] = Orig.Attributes["Type"].Value;
                                    XmlNode Dest = sch.SelectSingleNode(@"Dest");
                                    row["DestinationCode"] = Dest.Attributes["Code"].Value;
                                    row["ArrivalDate"] = Dest.Attributes["Arrives"].Value;
                                    row["DestinationType"] = Dest.Attributes["Type"].Value;
                                    row["TransitTime"] =
                                        (DateTime.Parse(Dest.Attributes["Arrives"].Value) -
                                         DateTime.Parse(Orig.Attributes["Departs"].Value)).Days;

                                    dtCrawlResult.Rows.Add(row);

                                    #endregion
                                }
                            }
                        }
                        Down(dtCrawlResult);
                        SaveFileLog(INTTRAID, file.Name, 1);
                        LogHelper.SaveLog("Import", string.Format("Import:{0} count:{1}" ,file.Name, dtCrawlResult.Rows.Count.ToString(CultureInfo.InvariantCulture)));
                    }
                    catch (Exception loadEx)
                    {
                        LogHelper.SaveLog("Import", string.Format("Import:{0} Ex:{1}", file.Name, CommonHelper.BuildExceptionString(loadEx)));
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveLog("ImportError", CommonHelper.BuildExceptionString(ex));
            }
        }

        /// <summary>
        /// 解析邮件
        /// </summary>
        private void OnTimedEventResolveEmail(object source)
        {

            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                string PopServer = INIConfig.IniReadValue(CommonConstants.SECTION_INTTRA_MAIL, CommonConstants.INTTRAMAIL_POPSERVER);
                string User = INIConfig.IniReadValue(CommonConstants.SECTION_INTTRA_MAIL, CommonConstants.INTTRAMAIL_LOGIN);
                string Pass = INIConfig.IniReadValue(CommonConstants.SECTION_INTTRA_MAIL, CommonConstants.INTTRAMAIL_PASSWORD);
                //string PopServer = config.AppSettings.Settings["EDIReceiptPOP"].Value;
                //string User = config.AppSettings.Settings["EDIReceiptUserID"].Value;
                //string Pass = config.AppSettings.Settings["EDIReceiptUserPass"].Value;
                int count = 0;
                string inttraNum = string.Empty;
                string refNo = string.Empty;
                string booknum = string.Empty;
                int numstart = int.Parse(INIConfig.IniReadValue(CommonConstants.SECTION_INTTRA_MAIL, CommonConstants.INTTRAMAIL_RESOLVEEMAILCOUNT));

                Pop3Client objPOP3 = new Pop3Client();

                if (objPOP3.Connected)
                {
                    objPOP3.Disconnect();
                }

                // Connect to the server, false means don't use ssl
                objPOP3.Connect(PopServer, 110, false);

                // Authenticate ourselves towards the server by email account and password
                objPOP3.Authenticate(User, Pass);

                ShippingOrder contents = null;
                //email body, 
                string body = "";
                string MBLNO = "";
                string Subject = "";
                string From = "";

                count = objPOP3.GetMessageCount();
                LogHelper.SaveLog("EDIManagerDownLoadEmail", "Mail Count:" + count);
                //以下的FOR循环是显示出所有收件箱里面的邮件信息
                for (int i = numstart + 1; i <= count; i++)
                {
                    try
                    {
                        OpenPop.Mime.Message objEmail = objPOP3.GetMessage(i); // use true to get headers only
                        if (objEmail == null)
                        {
                            LogHelper.SaveLog("EDIManagerDownLoadEmail", string.Format("Index:{0} No mail found", i));
                            continue;
                        }
                        LogHelper.SaveLog("EDIManagerDownLoadEmail", string.Format("Index:{0} Subject:{1}", i, objEmail.Headers.Subject));
                        Subject = objEmail.Headers.Subject;
                        From = objEmail.Headers.From.Address;

                        MessagePart messagePart = objEmail.MessagePart;

                        if (messagePart.IsText)
                        {
                            body = messagePart.GetBodyAsText();
                        }
                        else if (messagePart.IsMultiPart)
                        {
                            MessagePart plainTextPart = objEmail.FindFirstPlainTextVersion();
                            if (plainTextPart != null)
                            {
                                // The message had a text/plain version - show that one
                                body = plainTextPart.GetBodyAsText();
                            }
                            else
                            {
                                // Try to find a body to show in some of the other text versions
                                List<MessagePart> textVersions = objEmail.FindAllTextVersions();
                                body = textVersions.Count >= 1 ? textVersions[0].GetBodyAsText() : "<<OpenPop>> Cannot find a text version body in this message.";
                            }
                        }
                        IDictionary<string, string> dicOrder;
                        IDictionary<string, string> dicContent;
                        #region 处理邮件内容
                        if (From.ToLower().Contains("service@inttra.com"))//INTTRA SERVICE 邮箱 一般是
                        {
                            if (Subject.Contains("ERROR-I_SID")) //SI错误
                            {
                                #region SI传输错误
                                string regexSIError = INIConfig.IniReadValue(CommonConstants.SECTION_INTTRA_MAIL, CommonConstants.INTTRAMAIL_REGEX_TSIERROR);
                                string regexSIErrorContent = INIConfig.IniReadValue(CommonConstants.SECTION_INTTRA_MAIL, CommonConstants.INTTRAMAIL_REGEX_TSIERRORCONTENT);
                                dicOrder = Subject.MatchByRegex(regexSIError);
                                MBLNO = dicOrder["BLNo"];
                                if (MBLNO.IsNullOrEmpty())
                                {
                                    throw new Exception("未解析到BL No");
                                }
                                string errorSubject = dicOrder["ReceiptSubject"];
                                dicContent = body.MatchByRegex(regexSIErrorContent);
                                string errorContent = dicContent["ReceiptContent"];
                                if (!errorSubject.IsNullOrEmpty() || !errorContent.IsNullOrEmpty())
                                {
                                    SaveEDIStateLog(null, MBLNO, (int)EDIMode.SI, (int)MessageState.EdiFailure, errorSubject + errorContent, i);
                                }
                                #endregion
                            }
                            else if (Subject.Contains("Failed Transaction Due to EDI Error"))
                            {
                                #region EDI传输失败
                                string regexFError = INIConfig.IniReadValue(CommonConstants.SECTION_INTTRA_MAIL, CommonConstants.INTTRAMAIL_REGEX_TFERROR);
                                string regexFErrorContent = INIConfig.IniReadValue(CommonConstants.SECTION_INTTRA_MAIL, CommonConstants.INTTRAMAIL_REGEX_TFERRORCONTENT);
                                dicOrder = Subject.MatchByRegex(regexFError);
                                MBLNO = dicOrder["BLNo"];
                                if (MBLNO.IsNullOrEmpty())
                                {
                                    throw new Exception("未解析到BL No");
                                }
                                dicContent = body.MatchByRegex(regexFErrorContent);
                                string errorContent =  dicOrder["ReceiptSubject"] + dicContent["ReceiptContent"];
                                if (errorContent.IsNullOrEmpty())
                                    errorContent = "未解析到错误信息";
                                SaveEDIStateLog(null, MBLNO, (int)EDIMode.SI, (int)MessageState.EdiFailure, errorContent, i);
                                #endregion
                            }
                            else
                            {
                                throw new Exception("此邮件不支持解析内容");
                            }
                            #region Comment Code

                            //string siReferenceRegex = "Reference:";
                            //string errorReference = @"Dear[\s]*Customer[\,]{1}[\s]*(?<Notes>[\w\W]+)[\s]*Reference[\:]{1}";
                            //string errorReferences = @"Dear[\s]*Customer[\,]{1}[\s]*(?<Notes>[\w\W]+)[\s]*References[\:]{1}";
                            //string mblRegex = @"[^\:]+ERROR-I_SID[\:][\s]*(?<MBLNo>[\w]+)";

                            

                            //if (Subject.Contains("ERROR-I_SID")) //SI错误
                            //{
                            //    #region Comment code
                            //    //int start = body.IndexOf("below:");
                            //    //int end = body.IndexOf("References");
                            //    //error = body.Substring(start + 7, end - start - 7).Trim();

                            //    //start = Subject.IndexOf(":");
                            //    //end = Subject.Length - 1;
                            //    //MBLNO = Subject.Substring(start + 1, end - start).Trim();

                            //    //if (Subject.Contains("INTTRA SI TRANSACTION ERROR-I_SID"))//SI错误
                            //    //{
                            //    //    int start = Subject.IndexOf("-");
                            //    //    int end = Subject.IndexOf("::");
                            //    //    MBLNO = Subject.Substring(start + 7, end - start - 7).Trim();
                            //    //    string error;
                            //    //    start = body.IndexOf("below:");
                            //    //    end = body.IndexOf("References");
                            //    //    error = body.Substring(start + 7, end - start - 7).Trim();

                            //    //    SaveEDIStateLog(null, MBLNO, (int)EDIMode.SI, (int)MessageState.EdiFailure, error, i);
                            //    //} 
                            //    #endregion

                            //    Match mblM = Regex.Match(Subject, mblRegex, RegexOptions.Singleline);
                            //    if (mblM != null) MBLNO = mblM.Groups["MBLNo"].Value;
                            //    if (string.IsNullOrEmpty(MBLNO))
                            //    {
                            //        throw new Exception("未解析到MBL No");
                            //    }
                            //    string error = "";
                            //    MatchCollection matchs = null;
                            //    if (Regex.IsMatch(body, siReferenceRegex, RegexOptions.Singleline))
                            //    {
                            //        matchs = Regex.Matches(body, errorReference, RegexOptions.Singleline);
                            //        foreach (Match m in matchs)
                            //        {
                            //            error = m.Groups["Notes"].Value;
                            //        }
                            //    }
                            //    else
                            //    {
                            //        matchs = Regex.Matches(body, errorReferences,
                            //            RegexOptions.IgnoreCase | RegexOptions.Singleline);
                            //        foreach (Match m in matchs)
                            //        {
                            //            error = m.Groups["Notes"].Value;
                            //        }
                            //    }
                            //    if (string.IsNullOrEmpty(error))
                            //    {
                            //        error = "未解析到详细错误，请联系IT";
                            //    }
                            //    SaveEDIStateLog(null, MBLNO, (int)EDIMode.SI, (int)MessageState.EdiFailure, error, i);
                            //}

                            //else if (Subject.Contains("Failed Transaction Due to EDI Error"))
                            //{
                            //    int start = Subject.IndexOf("-");
                            //    int end = Subject.IndexOf("::");
                            //    MBLNO = Subject.Substring(start + 1, end - start - 1).Trim();
                            //    string BlockName;
                            //    start = body.IndexOf("Block Name:"); //11位
                            //    //end = objEmail.Body.IndexOf("References");
                            //    BlockName = body.Substring(start + 11 + 1, 5).Trim();
                            //    string error = string.Empty;
                            //    if (string.IsNullOrEmpty(BlockName))
                            //    {
                            //        error = BlockName + "含有非法字符或者超长，请检查！";
                            //    }

                            //    SaveEDIStateLog(null, MBLNO, (int)EDIMode.SI, (int)MessageState.EdiFailure, error, i);
                            //}
                            //else
                            //{
                            //    throw new Exception("未知异常");
                            //} 
                            #endregion
                        }
                        else if (From.ToLower().Contains("so@inttra.com")) // 订舱相关邮件
                        {
                            #region SO
                            if (Subject.Contains("Error")) //出错信息
                            {

                            }
                            else if (Subject.Contains("Booking"))//成功信息
                            {
                                #region Booking
                                //string[] subjects = Subject.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                //foreach (string subject in subjects)
                                //{
                                //    if (subject.Contains("BN"))
                                //    {
                                //        booknum = subject.Substring(3, subject.Length - 3);
                                //    }
                                //}

                                //if (!string.IsNullOrEmpty(booknum))
                                //{
                                //    Database dbSO = DatabaseFactory.CreateDatabase();
                                //    DbCommand dbCommandSO = dbSO.GetStoredProcCommand("fcm.uspBookingConfirm");
                                //    dbSO.AddInParameter(dbCommandSO, "@InttraNum", DbType.String, inttraNum);
                                //    dbSO.AddInParameter(dbCommandSO, "@BookNo", DbType.String, refNo);
                                //    dbSO.AddInParameter(dbCommandSO, "@IsEnglish", DbType.Boolean, null);

                                //    DataSet bookinfo = dbSO.ExecuteDataSet(dbCommandSO);
                                //    if (bookinfo != null && bookinfo.Tables.Count >= 1)
                                //    {
                                //        contents = new ShippingOrder();
                                //        contents.Id = new Guid(bookinfo.Tables[0].Rows[0]["ID"].ToString());
                                //        contents.No = bookinfo.Tables[0].Rows[0]["No"].ToString();
                                //        contents.UpdateDate = Convert.ToDateTime(bookinfo.Tables[0].Rows[0]["UpdateDate"]);
                                //    }

                                //    if (contents != null)
                                //    {
                                //        EventObjects eventObject = new EventObjects();
                                //        eventObject.Id = Guid.Empty;
                                //        eventObject.IsShowAgent = false;
                                //        eventObject.IsShowCustomer = false;
                                //        eventObject.OperationID = contents.Id;
                                //        eventObject.OperationType = OperationType.OceanExport;
                                //        eventObject.Owner = string.Empty;
                                //        eventObject.Priority = MemoPriority.Normal;
                                //        eventObject.Subject = "订舱确认";
                                //        eventObject.Type = MemoType.EDILog;
                                //        eventObject.Code = "BookingConfirm";
                                //        eventObject.FormType = FormType.Booking;
                                //        eventObject.CategoryName = "SO";
                                //        eventObject.UpdateBy = new Guid("4047CFAD-ECC8-E111-9D0D-0026551CA87B");
                                //        eventObject.UpdateDate = contents.UpdateDate;
                                //        eventObject.CreateDate = DateTime.Now;
                                //        eventObject.Description = "订舱确认" + contents.No;
                                //        eventObject.FormID = contents.Id;


                                //        Database dbmemo = DatabaseFactory.CreateDatabase();
                                //        DbCommand dbCommandmemo = dbmemo.GetStoredProcCommand("[fcm].[uspSaveOperationMemoInfo]");

                                //        dbmemo.AddInParameter(dbCommandmemo, "@OperationID", DbType.Guid, eventObject.OperationID);   //业务ID
                                //        dbmemo.AddInParameter(dbCommandmemo, "@OperationType", DbType.Int16, eventObject.OperationType.GetHashCode());
                                //        dbmemo.AddInParameter(dbCommandmemo, "@OperationEventCodes", DbType.String, eventObject.Code.ToString());//CODE
                                //        dbmemo.AddInParameter(dbCommandmemo, "@MessageIDs", DbType.String, eventObject.MessageID == Guid.Empty ? null : eventObject.MessageID.ToString());
                                //        dbmemo.AddInParameter(dbCommandmemo, "@Ids", DbType.String, eventObject.Id.ToString());
                                //        dbmemo.AddInParameter(dbCommandmemo, "@FormIDs", DbType.String, eventObject.FormID.ToString());
                                //        dbmemo.AddInParameter(dbCommandmemo, "@FormTypes", DbType.String, eventObject.FormType.GetHashCode().ToString());
                                //        dbmemo.AddInParameter(dbCommandmemo, "@IsShowAgents", DbType.String, eventObject.IsShowAgent.ToString());
                                //        dbmemo.AddInParameter(dbCommandmemo, "@IsShowCustomers", DbType.String, eventObject.IsShowCustomer.ToString());
                                //        dbmemo.AddInParameter(dbCommandmemo, "@Subjects", DbType.String, eventObject.Subject);
                                //        dbmemo.AddInParameter(dbCommandmemo, "@Contents", DbType.String, eventObject.Description);
                                //        dbmemo.AddInParameter(dbCommandmemo, "@Prioritys", DbType.String, eventObject.Priority.GetHashCode().ToString());
                                //        dbmemo.AddInParameter(dbCommandmemo, "@Types", DbType.String, eventObject.Type.GetHashCode().ToString());
                                //        dbmemo.AddInParameter(dbCommandmemo, "@UpdateDates", DbType.String, eventObject.UpdateDate.ToString());
                                //        dbmemo.AddInParameter(dbCommandmemo, "@SaveByID", DbType.Guid, eventObject.UpdateBy);
                                //        dbmemo.AddInParameter(dbCommandmemo, "@IsEnglish", DbType.Boolean, false);
                                //        dbmemo.AddInParameter(dbCommandmemo, "@ReturnResult", DbType.Boolean, false);
                                //        dbmemo.AddInParameter(dbCommandmemo, "@MailMessageIDs", DbType.String, eventObject.MailMsgID);
                                //        dbmemo.ExecuteNonQuery(dbCommandmemo);
                                //    }
                                //} 
                                #endregion
                            }
                            #endregion
                        }
                        else if (From.ToLower().Contains("si@inttra.com")) // 补料相关邮件
                        {
                            #region SI
                            if (Subject.Contains("INTTRA SI"))
                            {
                                string regexSI = INIConfig.IniReadValue(CommonConstants.SECTION_INTTRA_MAIL, CommonConstants.INTTRAMAIL_REGEX_SI);
                                string regexSIContent = INIConfig.IniReadValue(CommonConstants.SECTION_INTTRA_MAIL, CommonConstants.INTTRAMAIL_REGEX_SICONTENT);
                                dicContent = body.MatchByRegex(regexSIContent);
                                string content = dicContent["ReceiptContent"];
                                if (!content.IsNullOrEmpty())
                                {
                                    dicOrder = content.MatchByRegex(regexSI);
                                    MBLNO = dicOrder["BLNo"];
                                    if (MBLNO.IsNullOrEmpty())
                                    {
                                        throw new Exception("未解析到提单号");
                                    }
                                    if (objEmail.MessagePart.MessageParts.Count > 1)
                                    {
                                        if (objEmail.MessagePart.MessageParts[1].IsAttachment)
                                        {
                                            SaveEDIStateLog4File(null, MBLNO, (int)EDIMode.SI, (int)MessageState.EdiSuccess, "", objEmail.MessagePart.MessageParts[1].FileName, objEmail.MessagePart.MessageParts[1].Body, i);
                                            goto Log;
                                        }
                                    }
                                    SaveEDIStateLog(null, MBLNO, (int)EDIMode.SI, (int)MessageState.EdiSuccess, "", i);
                                }

                                #region Comment Code
                                //body = body.Replace('\n', '|');

                                //string[] subjectstr = body.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);//以换行分割
                                //string MBLNOstr = (from g in subjectstr where g.Contains("Shippers Ref") select g).First();//找到包含M单号的分组


                                //int start = MBLNOstr.IndexOf("Shippers Ref:");// 13位
                                //int length = MBLNOstr.Length - start - 12;//计算截取长度
                                //MBLNO = MBLNOstr.Substring(start + 13).Trim();//截取冒号之后内容再去除空格

                                //if (!string.IsNullOrEmpty(MBLNO))
                                //{
                                //    if (objEmail.MessagePart.MessageParts.Count > 1)
                                //    {
                                //        if (objEmail.MessagePart.MessageParts[1].IsAttachment)
                                //        {
                                //            SaveEDIStateLog4File(null, MBLNO, (int)EDIMode.SI, (int)MessageState.EdiSuccess, "", objEmail.MessagePart.MessageParts[1].FileName, objEmail.MessagePart.MessageParts[1].Body, i);
                                //            goto Log;
                                //        }
                                //    }
                                //    SaveEDIStateLog(null, MBLNO, (int)EDIMode.SI, (int)MessageState.EdiSuccess, "", i);
                                //} 
                                #endregion

                            }
                            else if (Subject.Contains("SDR"))
                            {
                                string regexSISDR = INIConfig.IniReadValue(CommonConstants.SECTION_INTTRA_MAIL, CommonConstants.INTTRAMAIL_REGEX_SISDR);
                                string regexSISDRContent = INIConfig.IniReadValue(CommonConstants.SECTION_INTTRA_MAIL, CommonConstants.INTTRAMAIL_REGEX_SISDRCONTENT);
                                dicOrder = Subject.MatchByRegex(regexSISDR);
                                MBLNO = dicOrder["BLNo"];
                                if (MBLNO.IsNullOrEmpty())
                                {
                                    throw new Exception("未解析到BL No");
                                }
                                dicContent = body.MatchByRegex(regexSISDRContent);
                                string content = dicContent["ReceiptContent"];
                                if (content.IsNullOrEmpty())
                                    content = "未解析到错误信息";
                                SaveEDIStateLog(null, MBLNO, (int)EDIMode.SI, (int)MessageState.EdiFailure, content, i);

                                #region Comment Code
                                ////截取M单号示例 
                                ////SDR: CITYOCEAN; CARRIER: CMA CGM; I_SID: CMDUSHZ2141167; CNTL #: 17050394_001_CITYOCEAN
                                //string[] subjectstr = Subject.Split(new char[] {';'},
                                //    StringSplitOptions.RemoveEmptyEntries); //以；分割
                                //string MBLNOstr = (from g in subjectstr where g.Contains("I_SID") select g).First();
                                //    //找到包含M单号的分组


                                //int start = MBLNOstr.IndexOf(":"); // I_SID: CMDUSHZ2141167 找到：位置
                                //int length = MBLNOstr.Length - start - 1; //计算截取长度
                                //MBLNO = MBLNOstr.Substring(start + 1, length).Trim(); //截取冒号之后内容再去除空格

                                ////截取错误信息示例 
                                ////The following error(s) and warning(s) occurred:
                                ////Error   : Shipping Instruction amendment was requested without an original
                                ////Inbound File Name:            COLSC_IFTMIN_I.1962794953.1807949482.1.txt
                                ////Interchange Control Number:   17050394
                                ////Message Control Number:       001

                                //string error;
                                //string[] bodystr = body.Split(new string[] {Environment.NewLine},
                                //    StringSplitOptions.RemoveEmptyEntries); //以；分割body获得每行内容数组
                                //error = string.Join(Environment.NewLine,
                                //    (from g in bodystr where g.StartsWith("Error") select g).ToArray()); //将ERROR行拼起来

                                //SaveEDIStateLog(null, MBLNO, (int) EDIMode.SI, (int) MessageState.EdiFailure, error, i); 
                                #endregion
                            }
                            #endregion
                        }
                        else if (From.ToLower().Contains("evgm_prod@inttra.com")) // VGM相关邮件
                        {
                            #region VGM

                            if (Subject.Contains("INTTRA eVGM Error")) //VGM错误
                            {

                            }
                            else if (Subject.Contains("VGM")) //VGM
                            {
                                string regexVGM = INIConfig.IniReadValue(CommonConstants.SECTION_INTTRA_MAIL, CommonConstants.INTTRAMAIL_REGEX_VGM);
                                string regexVGMContent = INIConfig.IniReadValue(CommonConstants.SECTION_INTTRA_MAIL, CommonConstants.INTTRAMAIL_REGEX_VGMCONTENT);
                                dicOrder = Subject.MatchByRegex(regexVGM);
                                MBLNO = dicOrder["BLNo"];
                                if (MBLNO.IsNullOrEmpty())
                                {
                                    throw new Exception("未解析到BL No");
                                }
                                string receiptSubject = dicOrder["ReceiptSubject"];
                                dicContent = body.MatchByRegex(regexVGMContent);
                                string content = dicContent["ReceiptContent"];
                                if (content.IsNullOrEmpty())
                                    content = "未定义解析的邮件内容";
                                if (receiptSubject.Contains("Original VGM"))
                                {
                                    SaveEDIStateLog(null, MBLNO, (int)EDIMode.VGM, (int)MessageState.EdiSuccess, content, i);
                                }
                                else if (receiptSubject.Contains("Carrier Acknowledged")) //船东承认VGM
                                {
                                    SaveEDIStateLog(null, MBLNO, (int)EDIMode.VGM, (int)MessageState.CarrierAccepted, content, i);
                                }
                                else if (Subject.Contains("Carrier Accepted")) //船东认可VGM
                                {
                                    SaveEDIStateLog(null, MBLNO, (int)EDIMode.VGM, (int)MessageState.CarrierAccepted, content, i);
                                }
                                else
                                {
                                    SaveEDIStateLog(null, MBLNO, (int) EDIMode.VGM, (int) MessageState.EdiSuccess, "未定义的回执类型", i);
                                }
                            }
                            else
                            {
                                throw new Exception("此VGM不支持解析内容");
                            }
                            #region Comment Code
                            //if (Subject.Contains("Original VGM"))//INTTRA已转发VGM
                            //{
                            //    //截取M单号示例 
                            //    //Original VGM, CN=GESU5463450, BN=Not Provided, BL=CMDUSHZ2148250, VID=CMDUSHZ2148250, CARR=CMDU, VDL=Not Provided, eVGMID=4833847
                            //    string[] subjectstr = Subject.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);//以，分割主题
                            //    string MBLNOstr = (from g in subjectstr where g.Contains("BL=") select g).First();//找到包含M单号的分组
                            //    MBLNO = MBLNOstr.Substring(4, MBLNOstr.Length - 4).Trim();

                            //}
                            //else if (Subject.Contains("Carrier Acknowledged"))//船东承认VGM
                            //{
                            //    //截取M单号示例 
                            //    //Carrier Accepted: Original VGM, CN=TCNU9799146, BN=Not Provided, BL=HLCUSZX1705BESF3, VID=HLCUSZX1705BESF3, CARR=HLCU, VDL=Not 
                            //    string[] subjectstr = Subject.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);//以，分割主题
                            //    string MBLNOstr = (from g in subjectstr where g.Contains("BL=") select g).First();//找到包含M单号的分组
                            //    MBLNO = MBLNOstr.Substring(4, MBLNOstr.Length - 4).Trim();

                            //    SaveEDIStateLog(null, MBLNO, (int)EDIMode.VGM, (int)MessageState.CarrierAccepted, "Carrier Acknowledged", i);
                            //}
                            //else if (Subject.Contains("Carrier Accepted"))//船东认可VGM
                            //{
                            //    //截取M单号示例 
                            //    //Carrier Acknowledged: Original VGM, CN=FCIU8168496, BN=Not Provided, BL=CMDUSHZ2148250, VID=CMDUSHZ2148250, CARR=CMDU, VDL=Not Provided
                            //    string[] subjectstr = Subject.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);//以，分割主题
                            //    string MBLNOstr = (from g in subjectstr where g.Contains("BL=") select g).First();//找到包含M单号的分组
                            //    MBLNO = MBLNOstr.Substring(4, MBLNOstr.Length - 4).Trim();

                            //    SaveEDIStateLog(null, MBLNO, (int)EDIMode.VGM, (int)MessageState.CarrierAccepted, "Carrier Accepted", i);
                            //}
                            //else if (Subject.Contains("INTTRA eVGM Error"))//VGM错误
                            //{
                            //    //截取M单号示例 
                            //    //INTTRA eVGM Error: Submitter VGM Reference: CMDUUSH0215046; Container Number: TGHU9401737;
                            //    string[] subjectstr = Subject.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);//以，分割主题
                            //    string MBLNOstr = (from g in subjectstr where g.Contains("Reference") select g).First();//找到包含M单号的分组

                            //    int start = MBLNOstr.LastIndexOf(":");// INTTRA eVGM Error: Submitter VGM Reference: CMDUUSH0215046 找到最后：位置
                            //    int length = MBLNOstr.Length - start - 1;//计算截取长度
                            //    MBLNO = MBLNOstr.Substring(start, length).Trim();

                            //    //The following error(s) occurred when processing eVGM transaction received by INTTRA on 05/17/2017 at 02:03:24.
                            //    //If you are not the correct recipient to address this error message, please route this email to the technical EDI contact within your company.
                            //    //-----------------------------------------------
                            //    //ERROR MESSAGES: (LOG ID: c8b16d11-5e61-4753-83a5-7763af07985c)
                            //    //-----------------------------------------------
                            //    //ERROR : Invalid EDI structure found in file: 17050431.170517020205715.503962.txt.
                            //    //      Error encountered during parsing. The Edifact transaction set with id '001' contained in interchange (without group) with id '20170517001', with sender id 'CITYOCEAN', receiver id 'INTTRA' is being suspended with following errors:Error: 1 (Field level error)	SegmentID: NAD	Position in TS: 13	Data Element ID: C08002	Position in Segment: 5	Position in Field: 2	Data Value: RT CO.,LTD. CHINA 	37: Invalid character(s) found in data element. The sequence number of the suspended message is 1. 
                            //    //-----------------------------------------------
                            //    //TRANSACTION SUMMARY
                            //    //-----------------------------------------------
                            //    string error;
                            //    string[] bodystr = body.Split(new string[] { "-----------------------------------------------" }, StringSplitOptions.RemoveEmptyEntries);//以分割线分割body获得每行内容数组
                            //    error = string.Join(Environment.NewLine, (from g in bodystr where g.StartsWith("ERROR :") select g).ToArray());//将ERROR行拼起来

                            //    SaveEDIStateLog(null, MBLNO, (int)EDIMode.VGM, (int)MessageState.EdiFailure, error, i);
                            //} 
                            #endregion
                            #endregion

                        }
                        else if (From.ToLower() == "noreply@communications.inttra.com") //一般都是通知 没什么作用
                        {

                        }

                    Log:
                        INIConfig.IniWriteValue(CommonConstants.SECTION_INTTRA_MAIL, CommonConstants.INTTRAMAIL_RESOLVEEMAILCOUNT, i.ToString(CultureInfo.InvariantCulture));
                        //config.AppSettings.Settings["EmailNum"].Value = i.ToString(CultureInfo.InvariantCulture);
                        //config.Save(ConfigurationSaveMode.Modified);
                        //ConfigurationManager.RefreshSection("appSettings");

                        #endregion
                    }
                    catch (Exception ex)
                    {
                        LogHelper.SaveLog("EDIManagerResolveEmail", string.Format("Index:{0} Error:{1}", i, ex.Message));
                    }
                }

                objPOP3.Dispose();
            }
            catch (Exception ex)
            {
                LogHelper.SaveLog("EDIManagerDownLoadEmail", CommonHelper.BuildExceptionString(ex));
            }
        }

        /// <summary>
        /// 发送EDI通知
        /// </summary>
        /// <param name="source"></param>
        private void OnTimedEventSendEDINotice(object source)
        {
            try
            {
                IEnumerable<EDINotice> ediNotices = GetNoReceiptEDI2Send();
                var gbEDINotice = ediNotices.GroupBy(gItem => new{gItem.NoticeEmail,gItem.Sender});
                
                foreach (var gnItem in gbEDINotice)
                {
                    try
                    {
                        ICP.Message.ServiceInterface.Message message = new Message.ServiceInterface.Message
                        {
                            Type = MessageType.Email,
                            SendFrom="icpsystem@cityocean.com",
                            SendTo = gnItem.Key.NoticeEmail,
                            Subject = "未收到回执通知",
                            BodyFormat = BodyFormat.olFormatHTML,
                            CreateBy = AdministratorID,
                            CreatorName = "System",
                            CreateDate = DateTime.Now,
                        };
                        StringBuilder body = new StringBuilder();
                        body.AppendFormat("Hi&nbsp;{0},<br />&nbsp;&nbsp;&nbsp;&nbsp;您发送的以下EDI未收到回执，请及时查验:<br />", gnItem.Key.Sender);
                        List<Guid> sendIDs = new List<Guid>();
                        foreach (EDINotice nItem in gnItem)
                        {
                            sendIDs.Add(nItem.SendID);
                            body.AppendFormat("&nbsp;&nbsp;&nbsp;&nbsp;发送主题:{0}发送时间：{1}EDI类型:{2}<br />",nItem.SendSubject,nItem.SendDate.ToString("yyyy-MM-dd HH:mm:ss"),nItem.EDIType);
                        }
                        body.Append("Thanks!");
                        message.Body = body.ToString();
                        MailBeeService.Send(message);
                        string tempsendids = sendIDs.ToArray().Join();
                        SaveEDILogState(tempsendids);
                        Thread.Sleep(new TimeSpan(0, 0, 30));
                    }
                    catch (Exception sEx)
                    {
                        LogHelper.SaveLog("SendEDINotice", CommonHelper.BuildExceptionString(sEx));
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.SaveLog("SendEDINotice", CommonHelper.BuildExceptionString(ex));
            }
        }

        private bool CheckDown(string filename)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("CSP");
                DbCommand dbCommand = db.GetStoredProcCommand("[csp].[uspChencINTTRAFileInfo]");
                db.AddInParameter(dbCommand, "@FileName", DbType.String, filename);
                db.AddParameter(dbCommand, "IsExist", DbType.Boolean, ParameterDirection.ReturnValue, "", DataRowVersion.Current, null);
                db.ExecuteNonQuery(dbCommand);
                bool tempIsExist = Convert.ToBoolean(db.GetParameterValue(dbCommand, "@IsExist"));
                return tempIsExist;
            }
            catch (Exception ex)
            {
                LogHelper.SaveLog("CheckDown", filename + " " + CommonHelper.BuildExceptionString(ex));
            }

            return true;
        }

        private void SaveFileLog(string InntraID, string FileName, int state)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("CSP");
                DbCommand dbCommand = db.GetStoredProcCommand("[csp].[uspSaveINTTRAFileInfo]");
                db.AddInParameter(dbCommand, "@InntraID", DbType.String, InntraID);
                db.AddInParameter(dbCommand, "@FileName", DbType.String, FileName);
                db.AddInParameter(dbCommand, "@Status", DbType.Byte, state);
                db.AddInParameter(dbCommand, "@InputDatetime", DbType.DateTime, DateTime.Now);
                db.AddInParameter(dbCommand, "@CreateByID", DbType.Guid, new Guid("24D0CCAD-D7FA-E411-9225-0026551CA878"));
                db.AddInParameter(dbCommand, "@Remark", DbType.String, "");

                db.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                LogHelper.SaveLog("EDIManagerSchedule", CommonHelper.BuildExceptionString(ex));
            }
        }

        private void Down(DataTable dtCrawlResult)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("CSP");
                DbCommand dbCommand = db.GetStoredProcCommand("[csp].[uspBulkSaveSailingsSchedule]");
                dbCommand.CommandTimeout = 0;
                SqlParameter parameterConfig = new SqlParameter("@CrawlConfig", null)
                {
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "[csp].[CrawlConfig_SailingSchedule_Type]"
                };

                SqlParameter parameterData = new SqlParameter("@CrawlResult", dtCrawlResult)
                {
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "[csp].[CrawlResult_SailingSchedule_Type]"
                };

                dbCommand.Parameters.Add(parameterConfig);
                dbCommand.Parameters.Add(parameterData);
                db.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                LogHelper.SaveLog("EDIManagerSchedule", CommonHelper.BuildExceptionString(ex));
            }
        }

        /// <summary>
        /// 回写EDI状态
        /// </summary>
        /// <param name="OperationNo"></param>
        /// <param name="BLNO"></param>
        /// <param name="type"></param>
        /// <param name="state"></param>
        /// <param name="remark"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        private void SaveEDIStateLog(string OperationNo, string BLNO, int type, int state, string remark, int number)
        {
            if (("" + remark).Length > 500)
            {
                remark = remark.Substring(0, 500);
            }
            Database db = DatabaseFactory.CreateDatabase("ICP3");
            DbCommand dbCommand = db.GetStoredProcCommand("pub.uspEdiStateLog");
            db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, null);
            db.AddInParameter(dbCommand, "@OperationNO", DbType.String, OperationNo);
            db.AddInParameter(dbCommand, "@BLID", DbType.Guid, null);
            db.AddInParameter(dbCommand, "@BLNO", DbType.String, BLNO);
            db.AddInParameter(dbCommand, "@Type", DbType.Int32, type);
            db.AddInParameter(dbCommand, "@State", DbType.Int32, state);
            db.AddInParameter(dbCommand, "@remark", DbType.String, remark);
            db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, new Guid("4047CFAD-ECC8-E111-9D0D-0026551CA87B"));
            db.AddInParameter(dbCommand, "@number", DbType.Int32, number);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, false);

            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 回写EDI状态
        /// </summary>
        /// <param name="OperationNo"></param>
        /// <param name="BLNO"></param>
        /// <param name="type"></param>
        /// <param name="state"></param>
        /// <param name="remark"></param>
        /// <param name="filename"></param>
        /// <param name="file"></param>
        /// <param name="number"></param>
        private void SaveEDIStateLog4File(string OperationNo, string BLNO, int type, int state, string remark, string filename, byte[] file, int number)
        {
            Database db = DatabaseFactory.CreateDatabase("ICP3");
            DbCommand dbCommand = db.GetStoredProcCommand("pub.uspEdiStateLog");
            db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, null);
            db.AddInParameter(dbCommand, "@OperationNO", DbType.String, OperationNo);
            db.AddInParameter(dbCommand, "@BLID", DbType.Guid, null);
            db.AddInParameter(dbCommand, "@BLNO", DbType.String, BLNO);
            db.AddInParameter(dbCommand, "@Type", DbType.Int32, type);
            db.AddInParameter(dbCommand, "@State", DbType.Int32, state);
            db.AddInParameter(dbCommand, "@remark", DbType.String, remark);
            db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, new Guid("4047CFAD-ECC8-E111-9D0D-0026551CA87B"));
            db.AddInParameter(dbCommand, "@filename", DbType.String, filename);
            db.AddInParameter(dbCommand, "@files", DbType.Binary, file);
            db.AddInParameter(dbCommand, "@number", DbType.Int32, number);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, false);

            db.ExecuteNonQuery(dbCommand);
        }
        /// <summary>
        /// 获取无回执EDI准备发送
        /// </summary>
        /// <returns></returns>
        IEnumerable<EDINotice> GetNoReceiptEDI2Send()
        {
            try
            {
                int day = Convert.ToInt32(INIConfig.IniReadValue(CommonConstants.SECTION_SENDEDINOTICE, CommonConstants.SENDEDINOTICE_BEFOREDAY));
                int hour = Convert.ToInt32(INIConfig.IniReadValue(CommonConstants.SECTION_SENDEDINOTICE, CommonConstants.SENDEDINOTICE_BEFOREHOUR));

                Database db = DatabaseFactory.CreateDatabase("ICP3");
                DbCommand dbCommand = db.GetStoredProcCommand("[fcm].[uspGetNoReceiptEDI]");
                db.AddInParameter(dbCommand, "@BeginDate", DbType.DateTime, DateTime.Now.AddDays(-day));
                db.AddInParameter(dbCommand, "@EndDate", DbType.DateTime, DateTime.Now.AddHours(-hour));
                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                List<EDINotice> results = (ds.Tables[0].AsEnumerable().Select(b => new EDINotice
                {
                    SendID = b.Field<Guid>("SendID"),
                    SendSubject = b.Field<string>("SendSubject"),
                    SendDate = b.Field<DateTime>("SendDate"),
                    EDIType = b.Field<string>("EDIType"),
                    Sender = b.Field<string>("Sender"),
                    NoticeEmail = b.Field<string>("NoticeEmail"),
                    IsSuccess = false,
                })).ToList();
                return results;
            }
            catch (Exception ex)
            {
                LogHelper.SaveLog("GetNoReceiptEDI2Send", CommonHelper.BuildExceptionString(ex));
            }
            return new List<EDINotice>();
        }
        /// <summary>
        /// 保存EDI日志状体
        /// 是否发送通知
        /// </summary>
        /// <param name="sendIDs"></param>
        void SaveEDILogState(string sendIDs)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase("ICP3");
                DbCommand dbCommand = db.GetStoredProcCommand("[fcm].[uspSaveEDILogState]");
                db.AddInParameter(dbCommand, "@SendIDs", DbType.String, sendIDs);
                db.AddInParameter(dbCommand, "@SaveBy", DbType.Guid, AdministratorID);
                db.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                LogHelper.SaveLog("GetNoReceiptEDI2Send", CommonHelper.BuildExceptionString(ex));
            }
        }

        /// <summary>
        /// 设置应用程序上下文
        /// </summary>
        void SetApplicationContent()
        {
            string macAddress = HardwareInfoHelper.GetMacByNetworkInterface();
            string ipAddress = CommonHelper.GetLocalIpAddress();
            ApplicationContext.Current.IsEnglish = true;
            ApplicationContext.Current.CultureName = "zh-cn";
            ApplicationContext.Current.SessionId = "" + AdministratorID;
            ApplicationContext.Current.UserId = AdministratorID;
            ApplicationContext.Current.Username = "ICPFileSystem";
            ApplicationContext.Current.MacAddress = macAddress;
            ApplicationContext.Current.PublicIpAddress = ApplicationContext.Current.LocalIpAddress = ipAddress;
            ApplicationContext.Current.DefaultCompanyId = CompanyID;
            ApplicationContext.Current.ClientId = AdministratorID;
        }

        #region 已弃用
        private static void OnTimedEventDownConfirm(object source)
        {
            //string path = AppDomain.CurrentDomain.BaseDirectory + "\\BookingConfirm\\";
            //string pathmbl = AppDomain.CurrentDomain.BaseDirectory + "\\BookingConfirmBookNO\\";

            //try
            //{
            //    DirectoryInfo folder = new DirectoryInfo(path);
            //    DirectoryInfo foldermbl = new DirectoryInfo(pathmbl);
            //    string files = "";
            //    ShippingOrder contents = null;
            //    FileInfo[] fileinfos = foldermbl.GetFiles("*.txt");
            //    string[] contentArray;
            //    InttraNum result = new InttraNum();
            //    string inttraNum = string.Empty;
            //    string refNo = string.Empty;

            //    Database db = DatabaseFactory.CreateDatabase();
            //    DbCommand dbCommand = db.GetStoredProcCommand("fcm.uspGetUnBookingConfirm");
            //    db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, null);

            //    DataSet set = db.ExecuteDataSet(dbCommand);
            //    if (set != null && set.Tables.Count >= 1 && set.Tables[0].Rows.Count > 0)
            //    {
            //        foreach (DataRow row in set.Tables[0].Rows)
            //        {
            //            result.Id = new Guid(row["ID"].ToString());
            //            result.No = row["No"].ToString();
            //            result.InttraNumber = row["INTTRANUMBER"].ToString();

            //            if (fileinfos == null || fileinfos.Length < 1)
            //            {
            //                return;
            //            }

            //            foreach (FileInfo file in fileinfos)
            //            {
            //                if (file.Name.EndsWith("BookingConfirm"))
            //                {
            //                    continue;
            //                }

            //                StreamReader sreader = new StreamReader(file.FullName);
            //                files = sreader.ReadToEnd();
            //                if (files.Contains("BOOKING CANCEL"))
            //                {
            //                    File.Delete(file.FullName);
            //                    continue;
            //                }

            //                contentArray = files.Split(new string[] { "'" }, StringSplitOptions.RemoveEmptyEntries);

            //                foreach (string content in contentArray)
            //                {
            //                    if (content.StartsWith("RFF+ZZZ:"))
            //                    {
            //                        int i = content.IndexOf("RFF+ZZZ:") + 1;
            //                        refNo = content.Substring(i + 7, 8);
            //                    }
            //                    else if (content.StartsWith("RFF+BN:"))
            //                    {
            //                        int i = content.IndexOf("RFF+BN:") + 1;
            //                        refNo = content.Substring(i + 6, content.Length - 7);
            //                    }
            //                    if (refNo.ToUpper() == result.No)
            //                    {
            //                        Database dbConfirm = DatabaseFactory.CreateDatabase("ICP3");
            //                        DbCommand dbCommandConfirm = dbConfirm.GetStoredProcCommand("fcm.uspBookingConfirm");
            //                        db.AddInParameter(dbCommand, "@InttraNum", DbType.String, inttraNum);
            //                        db.AddInParameter(dbCommand, "@BookNo", DbType.String, refNo);
            //                        dbConfirm.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, null);

            //                        DataSet bookinfo = db.ExecuteDataSet(dbCommand);
            //                        if (bookinfo != null && bookinfo.Tables.Count >= 1)
            //                        {
            //                            contents = new ShippingOrder();
            //                            contents.Id = new Guid(bookinfo.Tables[0].Rows[0]["ID"].ToString());
            //                            contents.No = bookinfo.Tables[0].Rows[0]["No"].ToString();
            //                            contents.UpdateDate = Convert.ToDateTime(bookinfo.Tables[0].Rows[0]["UpdateDate"]);
            //                        }

            //                        if (contents != null)
            //                        {
            //                            EventObjects eventObject = new EventObjects();
            //                            eventObject.Id = Guid.Empty;
            //                            eventObject.IsShowAgent = false;
            //                            eventObject.IsShowCustomer = false;
            //                            eventObject.OperationID = contents.Id;
            //                            eventObject.OperationType = OperationType.OceanExport;
            //                            eventObject.Owner = string.Empty;
            //                            eventObject.Priority = MemoPriority.Normal;
            //                            eventObject.Subject = "订舱确认";
            //                            eventObject.Type = MemoType.EDILog;
            //                            eventObject.Code = "BookingConfirm";
            //                            eventObject.FormType = FormType.Booking;
            //                            eventObject.CategoryName = "SO";
            //                            eventObject.UpdateBy = new Guid("4047CFAD-ECC8-E111-9D0D-0026551CA87B");
            //                            eventObject.UpdateDate = contents.UpdateDate;
            //                            eventObject.CreateDate = DateTime.Now;
            //                            eventObject.Description = "订舱确认" + contents.No;
            //                            eventObject.FormID = contents.Id;


            //                            Database dbmemo = DatabaseFactory.CreateDatabase("ICP3");
            //                            DbCommand dbCommandmemo = dbmemo.GetStoredProcCommand("[fcm].[uspSaveOperationMemoInfo]");

            //                            dbmemo.AddInParameter(dbCommandmemo, "@OperationID", DbType.Guid, eventObject.OperationID);   //业务ID
            //                            dbmemo.AddInParameter(dbCommandmemo, "@OperationType", DbType.Int16, eventObject.OperationType.GetHashCode());
            //                            dbmemo.AddInParameter(dbCommandmemo, "@OperationEventCodes", DbType.String, eventObject.Code.ToString());//CODE
            //                            dbmemo.AddInParameter(dbCommandmemo, "@MessageIDs", DbType.String, eventObject.MessageID == Guid.Empty ? null : eventObject.MessageID.ToString());
            //                            dbmemo.AddInParameter(dbCommandmemo, "@Ids", DbType.String, eventObject.Id.ToString());
            //                            dbmemo.AddInParameter(dbCommandmemo, "@FormIDs", DbType.String, eventObject.FormID.ToString());
            //                            dbmemo.AddInParameter(dbCommandmemo, "@FormTypes", DbType.String, eventObject.FormType.GetHashCode().ToString());
            //                            dbmemo.AddInParameter(dbCommandmemo, "@IsShowAgents", DbType.String, eventObject.IsShowAgent.ToString());
            //                            dbmemo.AddInParameter(dbCommandmemo, "@IsShowCustomers", DbType.String, eventObject.IsShowCustomer.ToString());
            //                            dbmemo.AddInParameter(dbCommandmemo, "@Subjects", DbType.String, eventObject.Subject);
            //                            dbmemo.AddInParameter(dbCommandmemo, "@Contents", DbType.String, eventObject.Description);
            //                            dbmemo.AddInParameter(dbCommandmemo, "@Prioritys", DbType.String, eventObject.Priority.GetHashCode().ToString());
            //                            dbmemo.AddInParameter(dbCommandmemo, "@Types", DbType.String, eventObject.Type.GetHashCode().ToString());
            //                            dbmemo.AddInParameter(dbCommandmemo, "@UpdateDates", DbType.String, eventObject.UpdateDate.ToString());
            //                            dbmemo.AddInParameter(dbCommandmemo, "@SaveByID", DbType.Guid, eventObject.UpdateBy);
            //                            dbmemo.AddInParameter(dbCommandmemo, "@IsEnglish", DbType.Boolean, false);
            //                            dbmemo.AddInParameter(dbCommandmemo, "@ReturnResult", DbType.Boolean, false);
            //                            dbmemo.AddInParameter(dbCommandmemo, "@MailMessageIDs", DbType.String, eventObject.MailMsgID);
            //                            dbmemo.ExecuteNonQuery(dbCommandmemo);


            //                            File.Move(file.FullName, pathmbl + contents.No + "BookingConfirm");
            //                        }
            //                    }

            //                }
            //                sreader.Close();
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    LogHelper.SaveLog("EDIManagerBookConfirm", CommonHelper.BuildExceptionString(ex));
            //}
        }

        /// <summary>
        /// 获取发送EDI的执行人邮箱
        /// </summary>
        /// <param name="mblno"></param>
        /// <returns></returns>
        private EdiSender GetMail(string mblno)
        {
            //EdiSender sender = null;
            //Database db = DatabaseFactory.CreateDatabase();
            //DbCommand dbCommand = db.GetStoredProcCommand("fcm.uspGetEdiMailEnventAndUserByMblNo");
            //db.AddInParameter(dbCommand, "@MblNo", DbType.String, mblno);
            //db.AddInParameter(dbCommand, "@MemoType", DbType.Byte, 2);

            //DataSet ds = db.ExecuteDataSet(dbCommand);
            //if (ds != null && ds.Tables != null)
            //{
            //    sender = (from b in ds.Tables[0].AsEnumerable()
            //              select new EdiSender
            //              {
            //                  Id = b.Field<Guid>("id"),
            //                  Ename = b.Field<string>("EName"),
            //                  Email = b.Field<string>("Email")
            //              }).First();
            //}
            //return sender;
            return null;
        }

        /// <summary>
        /// 发送Email
        /// </summary>
        /// <param name="sendTo">接收人</param>
        /// <param name="subject">主题</param>
        /// <param name="content">内容</param>
        private void SendEmail(string sendTo, string subject, string content)
        {
            //try
            //{
            //    SmtpClient client = new SmtpClient("smtp.cityocean.com");
            //    client.UseDefaultCredentials = false;
            //    client.Credentials = new NetworkCredential("icpsystem@cityocean.com", "ICPOceanCityCo123");
            //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //    MailMessage message = new MailMessage();
            //    message.Subject = subject;
            //    message.Body = content;

            //    message.SubjectEncoding = message.BodyEncoding = UnicodeEncoding.GetEncoding("GB2312");

            //    //收件人
            //    message.To.Add(new MailAddress(sendTo));

            //    message.Priority = MailPriority.Normal;
            //    message.IsBodyHtml = true;

            //    message.Sender = new MailAddress("icpsystem@cityocean.com", "icpsystem@cityocean.com");
            //    message.From = message.Sender;
            //    client.Send(message);

            //    message.Dispose();
            //}
            //catch (Exception ex)
            //{
            //    LogHelper.SaveLog(ex.Message + ex.StackTrace);
            //}
        }

        //private void OnTimedEventTT(object source)
        //{
        //    string pathtt = System.AppDomain.CurrentDomain.BaseDirectory + "\\TT\\";
        //    string pathts = System.AppDomain.CurrentDomain.BaseDirectory + "\\TTs\\";
        //    try
        //    {
        //        DirectoryInfo folder = new DirectoryInfo(pathtt);
        //        DirectoryInfo foldermbl = new DirectoryInfo(pathts);
        //        string carriercode = "";
        //        string bookno = "";
        //        string mblno = "";
        //        string containerno = "";
        //        string containerstate = "";
        //        DateTime date = DateTime.Now;
        //        string location = "";
        //        string pol = "";
        //        string pod = "";
        //        string deliveryport = "";
        //        string receiptport = "";
        //        DateTime createdate = DateTime.Now;
        //        string files;
        //        CargoTracking contents = null;
        //        FileInfo[] fileinfos = foldermbl.GetFiles("*.txt");
        //        string[] contentArray;
        //        string[] containerArray;
        //        if (fileinfos == null || fileinfos.Length < 1)
        //        {
        //            return;
        //        }

        //        foreach (FileInfo file in fileinfos)
        //        {
        //            if (file.Name.EndsWith("Complete.txt"))
        //            {
        //                continue;
        //            }

        //            System.IO.StreamReader sreader = new StreamReader(file.FullName);
        //            files = sreader.ReadToEnd().ToUpper();

        //            containerArray = files.Split(new string[] { "UNH" }, StringSplitOptions.RemoveEmptyEntries);
        //            List<string> containerList = containerArray.ToList();
        //            containerList.RemoveAt(0);
        //            containerArray = containerList.ToArray();

        //            foreach (string container in containerArray)
        //            {
        //                contentArray = container.Split(new string[] { "'" }, StringSplitOptions.RemoveEmptyEntries);
        //                foreach (string content in contentArray)
        //                {
        //                    if (content.StartsWith("NAD+CA"))
        //                    {
        //                        int i = content.IndexOf(":");
        //                        carriercode = content.Substring(7, i - 7);
        //                    }
        //                    else if (content.StartsWith("RFF+BN:"))
        //                    {
        //                        int i = content.IndexOf("RFF+BN:") + 1;
        //                        bookno = content.Substring(i + 6, content.Length - 7);
        //                    }
        //                    else if (content.StartsWith("RFF+BM:"))
        //                    {
        //                        int i = content.IndexOf("RFF+BM:") + 1;
        //                        mblno = content.Substring(i + 6, content.Length - 7);
        //                    }
        //                    else if (content.StartsWith("STS"))
        //                    {
        //                        string[] array = content.Split(new string[] { "+" }, StringSplitOptions.RemoveEmptyEntries);
        //                        containerstate = array[2].Substring(0, array[2].IndexOf(":"));
        //                    }
        //                    else if (content.StartsWith("DTM+334"))
        //                    {
        //                        string[] array = content.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
        //                        int year = int.Parse(array[1].Substring(0, 4));
        //                        int month = int.Parse(array[1].Substring(4, 2));
        //                        int day = int.Parse(array[1].Substring(6, 2));
        //                        int hour = int.Parse(array[1].Substring(8, 2));
        //                        int minute = int.Parse(array[1].Substring(10, 2));
        //                        int second = 0;
        //                        DateTime dt = new DateTime(year, month, day, hour, minute, second);
        //                        date = dt;
        //                    }
        //                    else if (content.StartsWith("LOC+175+"))
        //                    {
        //                        int i = content.IndexOf(":");
        //                        location = content.Substring(8, 5);
        //                    }
        //                    else if (content.StartsWith("LOC+7+"))
        //                    {
        //                        int i = content.IndexOf(":");
        //                        deliveryport = content.Substring(6, 5);
        //                    }
        //                    else if (content.StartsWith("LOC+9+"))
        //                    {
        //                        int i = content.IndexOf(":");
        //                        pol = content.Substring(6, 5);
        //                    }
        //                    else if (content.StartsWith("LOC+11+"))
        //                    {
        //                        int i = content.IndexOf(":");
        //                        pod = content.Substring(7, 5);
        //                    }
        //                    else if (content.StartsWith("LOC+88+"))
        //                    {
        //                        int i = content.IndexOf(":");
        //                        receiptport = content.Substring(7, 5);
        //                    }
        //                    else if (content.StartsWith("EQD+CN"))
        //                    {
        //                        string[] array = content.Split(new string[] { "+" }, StringSplitOptions.RemoveEmptyEntries);
        //                        containerno = array[2];
        //                    }
        //                }
        //                Database db = DatabaseFactory.CreateDatabase();
        //                DbCommand dbCommand = db.GetStoredProcCommand("fcm.SaveCargoTracking");
        //                db.AddInParameter(dbCommand, "@CarrierCode", DbType.String, carriercode);
        //                db.AddInParameter(dbCommand, "@BookNo", DbType.String, bookno);
        //                db.AddInParameter(dbCommand, "@MblNo", DbType.String, mblno);
        //                db.AddInParameter(dbCommand, "@ContainerNo", DbType.String, containerno);
        //                db.AddInParameter(dbCommand, "@ContainerState", DbType.String, containerstate);
        //                db.AddInParameter(dbCommand, "@Date", DbType.DateTime, date);
        //                db.AddInParameter(dbCommand, "@Location", DbType.String, location);
        //                db.AddInParameter(dbCommand, "@DeliveryPort", DbType.String, deliveryport);
        //                db.AddInParameter(dbCommand, "@POL", DbType.String, pol);
        //                db.AddInParameter(dbCommand, "@POD", DbType.String, pod);
        //                db.AddInParameter(dbCommand, "@ReceiptPort", DbType.String, receiptport);

        //                db.ExecuteNonQuery(dbCommand);
        //            }
        //            sreader.Close();
        //            File.Move(file.FullName, pathts + bookno + "-" + containerstate + DateTime.Now.ToString("-yyyyMMddHHmmss") + "Complete.txt");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.SaveLog("EDIManagerTT", CommonHelper.BuildExceptionString(ex));
        //    }
        //}
        #endregion
    }
}
