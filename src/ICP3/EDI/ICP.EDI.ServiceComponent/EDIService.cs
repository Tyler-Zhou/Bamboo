using Altova.IO;
using ICP.EDI.ServiceComponent.Rule;
using ICP.EDI.ServiceInterface;
using ICP.EDI.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Server;
using ICP.Message.ServiceInterface;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using CommonLogHelper = ICP.Framework.CommonLibrary.LogHelper;

namespace ICP.EDI.ServiceComponent
{
    /// <summary>
    /// EDI日志服务
    /// </summary>
    public partial class EDIService : IEDIService
    {
        #region 字段、属性
        ISessionService _sessionService;
        IUserService _userService;
        IMessageService _messageService;
        IFCMCommonService FCMCommonService;
        /// <summary>
        /// EDI文件存放路径
        /// </summary>
        string _EDIServerDir;
        string _EDIXMLDir;
        
        #region Property
        /// <summary>
        /// 当前用户
        /// </summary>
        Guid CurrentUserId
        {
            get
            {
                if (_sessionService == null)
                {
                    return new Guid("530FE026-B2E3-45AB-8C95-A665574E923B");
                }
                if(_sessionService.CurrentSession==null)
                {
                    return ApplicationContext.Current.UserId;
                }
                return new Guid(_sessionService.CurrentSession[ServerVariables.CURRENT_USER].ToString());
            }
        }
        /// <summary>
        /// 当前用户Email
        /// </summary>
        string CurrentUserEmail
        {
            get
            {
                return ApplicationContext.Current.EmailAddress;
            }
        }

        /// <summary>
        /// 是否英文环境
        /// </summary>
        bool IsEnglish
        {
            get
            {
                return ApplicationContext.Current.IsEnglish;
            }
        }


        #endregion


        #region Service
        /// <summary>
        /// 发送邮件
        /// </summary>
        IMailBeeService MailBeeService
        {
            get { return ServiceClient.GetService<IMailBeeService>(); }
        }
        #endregion
        /// <summary>
        /// 保存批量中海订舱内容
        /// </summary>
        public DataSet CSCLResultDataSet { get; set; }
        #endregion

        #region 初始化

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ediServerDir"></param>
        /// <param name="ediXMLDir"></param>
        /// <param name="sessionService"></param>
        /// <param name="transportFoundationService"></param>
        /// <param name="userService"></param>
        /// <param name="configureService"></param>
        /// <param name="messageService"></param>
        /// <param name="fcmCommonService"></param>
        public EDIService(
            string ediServerDir,
            string ediXMLDir,
            ISessionService sessionService,
            IUserService userService,
            IMessageService messageService,
            IFCMCommonService fcmCommonService
            )
        {
            _sessionService = sessionService;
            _userService = userService;
            _messageService = messageService;
            _EDIServerDir = ediServerDir;
            _EDIXMLDir = ediXMLDir;
            FCMCommonService = fcmCommonService;
        }

        #endregion

        #region 中海(CSCL)
        /// <summary>
        /// 中海发送EDI并返回EDI结果集
        /// </summary>
        /// <param name="sendEDIItem"></param>
        /// <returns></returns>
        DataSet CSCLMethod(EDISendOption sendEDIItem)
        {
            CSCLResultDataSet = null;
            if (CSCLResultDataSet == null)
            {
                CSCLResultDataSet = new DataSet();
                DataTable dt = new DataTable("DataSource");
                DataColumn dc = new DataColumn();
                dc.DataType = Type.GetType("System.String");
                dt.Columns.Add(dc);
                CSCLResultDataSet.Tables.Add(dt);
            }
            SendEDI(sendEDIItem);
            return CSCLResultDataSet;
        }
        #endregion

        #region EDILoopContent
        [Obsolete("方法已过时，请使用 EDILoopContent(EDISendOption sendEDIItem, EDIConfigItem ediConfig) 代替")]
        void EDILoopContent(EDISendOption sendEDIItem, EDIConfigItem ediConfig)
        {
            DataSet ds = new DataSet();
            Guid idtemp = Guid.Empty;//idtemp用于最后记录日志
            string documentNo = string.Empty;
            string strCondition = string.Empty;
            string fromEMailAddress = LocalData.UserInfo.EmailAddress;
            string toAddress = string.Empty;

            string tempstring = string.Empty;
            byte[] datas = null;
            byte[] tempBytes = null;
            try
            {
                documentNo = GetDocumentNo(sendEDIItem.CompanyID);
                strCondition = BuildCondition(sendEDIItem, documentNo);
                ds = getEDIDataSetContent(ediConfig.StoredProcedure, strCondition, ediConfig.DataFormat);
                //规则验证
                if (string.IsNullOrEmpty(ediConfig.RegularFile) == false)
                {
                    StringBuilder errorInfo = new StringBuilder();
                    string rulePath = _EDIServerDir + "\\RuleFiles\\" + ediConfig.RegularFile;
                    RuleManager.Validate(rulePath, ds, ref errorInfo);
                    if (errorInfo.Length > 0)
                    {
                        throw new ApplicationException(errorInfo.ToString());
                    }
                }
                string txml = ds.GetXml();
                if (!string.IsNullOrEmpty(ediConfig.Component))
                {
                    //加载对应船东的插件
                    #region IMapping
                    IMapping mapping = ReflectHelper.GetEDIPlugIn(_EDIServerDir, ediConfig.Component);
                    if (mapping == null)
                    {
                        throw new ApplicationException("加载EDI插件失败...");
                    }
                    tempBytes = Encoding.UTF8.GetBytes(txml);
                    MemoryStream ms = new MemoryStream(tempBytes);
                    StreamInput inputStream = new StreamInput(ms);
                    //生成EDI文件
                    MemoryStream outms = new MemoryStream();
                    StreamOutput outStream = new StreamOutput(outms);
                    mapping.Run(inputStream, outStream);

                    //转成二进制通过FTP发送
                    datas = outms.ToArray();
                    ms.Close();
                    outms.Close();
                    inputStream.Close();
                    outStream.Close();
                    tempstring = Encoding.UTF8.GetString(datas);

                    #endregion;
                }
                else if ("json".Equals(ediConfig.FileFormat))
                {
                    switch (ediConfig.DataFormat)
                    {
                        case "ContainerLoadingPlan":
                            tempstring = ConvertCLPToJSONString(ds);
                            break;
                    }
                }
                else
                {
                    string nbEDI = ds.Tables[0].Rows.Cast<DataRow>().Aggregate(string.Empty, (current, dt) => current + (dt[0] + Environment.NewLine));
                    tempBytes = Encoding.UTF8.GetBytes(nbEDI);
                    datas = tempBytes;
                    tempstring = Encoding.UTF8.GetString(datas);
                }
                TransmitData(sendEDIItem, ediConfig, documentNo, ref fromEMailAddress, ref toAddress, tempstring, datas);

                #region LOG

                //记录日志
                Guid logID = Log(null,
                     sendEDIItem.EdiMode,
                     sendEDIItem.CompanyID,
                     sendEDIItem.FIDs,
                     sendEDIItem.FNOs,
                     documentNo,
                     sendEDIItem.Flag,
                     sendEDIItem.Subject,
                     fromEMailAddress,
                     toAddress,
                     sendEDIItem.Content,
                     tempstring,
                     DateTime.Now,
                     sendEDIItem.SendByID);

                #region 保存发送消息
                try
                {
                    MessageUserPropertiesObject userObject = new MessageUserPropertiesObject
                    {
                        {"OperationId", sendEDIItem.IDs[0]},
                        {"OperationType", sendEDIItem.OperationType}
                    };
                    userObject.Add("FormType", FormType.MBL);
                    userObject.Add("FormId", sendEDIItem.FIDs[0]);

                    MessageEDILogRelation logRelation = new MessageEDILogRelation
                    {
                        EDIConfigId = ediConfig.ID,
                        Flag = sendEDIItem.Flag,
                        Type = sendEDIItem.EdiMode
                    };
                    userObject.Add("EDILog", logRelation);


                    if (string.IsNullOrEmpty(fromEMailAddress))
                    {
                        fromEMailAddress = "icpsystem@cityocean.com";
                    }
                    Message.ServiceInterface.Message message = new Message.ServiceInterface.Message
                    {
                        CreateBy = sendEDIItem.SendByID,
                        CreateDate = DateTime.Now,
                        UserProperties = userObject,
                        BodyFormat = BodyFormat.olFormatHTML,
                        SendFrom = fromEMailAddress,
                        SendTo = toAddress,
                        Subject = sendEDIItem.Subject,
                        Type = MessageType.EDI,
                        Body = tempstring,
                        State = MessageState.Transmitted,
                    };

                    _messageService.Save(message);
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("EDI已发送，记录发送日志发生异常:{0}", ex.Message));
                }
                #endregion

                #region 事件
                try
                {
                    EventObjects eventObject = new EventObjects
                    {
                        Id = Guid.Empty,
                        IsShowAgent = false,
                        IsShowCustomer = false,
                        OperationID = sendEDIItem.IDs[0],
                        OperationType = OperationType.OceanExport,
                        Owner = string.Empty,
                        Priority = MemoPriority.Normal,
                        Subject = sendEDIItem.Subject,
                        Type = MemoType.EDILog,
                        UpdateBy = sendEDIItem.SendByID,
                        UpdateDate = DateTime.Now
                    };
                    switch (sendEDIItem.EdiMode)
                    {
                        case EDIMode.ContainerLoad:
                            eventObject.Code = "CLD";
                            eventObject.FormType = FormType.MBL;
                            eventObject.CategoryName = "ContainerLoad";
                            break;
                        case EDIMode.PCLP:
                            eventObject.Code = "PCLP";
                            eventObject.FormType = FormType.MBL;
                            eventObject.CategoryName = "PCLP";
                            break;
                    }

                    eventObject.CreateDate = DateTime.Now;
                    eventObject.Description = sendEDIItem.Subject;
                    eventObject.FormID = sendEDIItem.IDs[0];
                    FCMCommonService.SaveMemoInfo(eventObject);
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("EDI已发送，保存事件发生异常:{0}", ex.Message));
                }
                #endregion

                if (logID == Guid.Empty)
                {
                    throw new Exception("EDI已发送，记录日志发生异常");
                }
                #endregion
            }
            catch (ApplicationException appex)
            {
                throw appex;
            }
            catch (Exception ex)
            {
                CommonLogHelper.SaveLog("EDIService",CommonHelper.BuildExceptionString(ex));
                if (ex.Message.Contains("由于套接字没有连接"))
                {
                    throw new ApplicationException("由于网络延迟发送失败，请稍后重新发送！");
                }
                throw new ApplicationException(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sendEDIItem"></param>
        /// <param name="ediConfig"></param>
        /// <param name="id">业务ID</param>
        /// <param name="fid">MBLID/HBLID</param>
        /// <param name="businessNo">业务号</param>
        /// <param name="num"></param>
        [Obsolete("方法已过时，请使用 EDILoopContent(EDISendOption sendEDIItem, EDIConfigItem ediConfig) 代替")]
        void EDILoopContent(EDISendOption sendEDIItem, EDIConfigItem ediConfig, Guid id, Guid fid, string businessNo, int num)
        {
            DataSet ds = new DataSet();
            Guid idtemp = Guid.Empty;//idtemp用于最后记录日志
            string documentNo = string.Empty;
            string strCondition = string.Empty;

            try
            {
                documentNo = GetDocumentNo(sendEDIItem.CompanyID);
                idtemp = sendEDIItem.EdiMode == EDIMode.Booking ? id : fid;
                switch (sendEDIItem.ServiceKey)
                {
                    case "pil_si":
                    case "WorldexSO":
                    case "WorldexSI":
                    case "InttraSo":
                    case "InttraSi":
                        strCondition = BuildConditionForSpecial(sendEDIItem, idtemp, OperationType.Customs, documentNo, num);
                        break;
                    case "NBEDICenter":
                    case "NBEDIBookingANL":
                    case "NBEDISIANL":
                    case "NBEDIVGMANL":
                        strCondition = BuildConditionForNB(sendEDIItem, idtemp, OperationType.Customs, documentNo, num);
                        break;
                    default:
                        strCondition = BuildCondition(sendEDIItem, idtemp, OperationType.Customs, documentNo);
                        break;
                }
                ds = getEDIDataSetContent(ediConfig.StoredProcedure, strCondition, ediConfig.DataFormat);

                if (ds == null)
                {
                    throw new ApplicationException("获取指定的数据源失败!!");
                }


                //规则验证
                if (string.IsNullOrEmpty(ediConfig.RegularFile) == false)
                {
                    StringBuilder errorInfo = new StringBuilder();
                    string rulePath = _EDIServerDir + "\\RuleFiles\\" + ediConfig.RegularFile;
                    RuleManager.Validate(rulePath, ds, ref errorInfo);
                    if (errorInfo.Length > 0)
                    {
                        throw new ApplicationException(errorInfo.ToString());
                    }
                }

                //加载对应船东的插件
                IMapping mapping = ReflectHelper.GetEDIPlugIn(_EDIServerDir, ediConfig.Component);
                if (mapping == null)
                {
                    throw new ApplicationException("加载EDI插件失败...");
                }

                string txml = ds.GetXml();
                if (sendEDIItem.ServiceKey.ToUpper() == EDITypeByCarrier.AMS.ToString()
                    || sendEDIItem.ServiceKey.ToUpper() == EDITypeByCarrier.ACI.ToString()
                    || sendEDIItem.ServiceKey.ToUpper() == EDITypeByCarrier.ISF.ToString()
                    || sendEDIItem.ServiceKey.ToUpper() == EDITypeByCarrier.AMSACI.ToString()
                    || sendEDIItem.ServiceKey.ToUpper() == EDITypeByCarrier.AMSISF.ToString()
                    || sendEDIItem.ServiceKey.ToUpper() == EDITypeByCarrier.AMSACIISF.ToString())
                {
                    txml = txml.Replace("&amp;amp;", "&amp;");
                }

                string fromEMailAddress = LocalData.UserInfo.EmailAddress;
                string toAddress = string.Empty;

                string tempstring = string.Empty;
                byte[] datas = null;
                byte[] tempBytes = null;
                //因为中海订舱/中海装箱 有中文字符，所以独立操作
                if (sendEDIItem.ServiceKey == EDITypeByCarrier.CSCL_Booking_NorthChina.ToString()
                    || sendEDIItem.ServiceKey == EDITypeByCarrier.CSCL_SI_NorthChina.ToString())
                {
                    string cscl = ds.Tables[0].Rows.Cast<DataRow>().Aggregate(string.Empty, (current, dt) => current + (dt[0] + Environment.NewLine));
                    //保存到DS返回到客户端
                    CSCLResultDataSet.Tables["DataSource"].Rows.Add(cscl);
                    //添加返回中海登录URL/用户名/密码
                    if (!CSCLResultDataSet.Tables.Contains("CSCL"))
                    {
                        DataTable dt1 = new DataTable("CSCL");
                        dt1.Columns.Add("CSCLWebURL");
                        dt1.Columns.Add("CSCLLoginName");
                        dt1.Columns.Add("CSCLPassword");
                        DataRow NewRow = dt1.NewRow();
                        NewRow["CSCLWebURL"] = toAddress = ediConfig.CSCLWebURL;
                        NewRow["CSCLLoginName"] = ediConfig.CSCLLoginName;
                        NewRow["CSCLPassword"] = ediConfig.CSCLPassword;
                        dt1.Rows.Add(NewRow);
                        CSCLResultDataSet.Tables.Add(dt1);
                    }

                    tempBytes = Encoding.Default.GetBytes(cscl);
                    tempstring = Encoding.Default.GetString(tempBytes);
                    datas = tempBytes;
                }
                //宁波edi中心
                else if (sendEDIItem.ServiceKey == EDITypeByCarrier.NBEDICenter.ToString() || sendEDIItem.ServiceKey == EDITypeByCarrier.NBEDIBookingANL.ToString() || sendEDIItem.ServiceKey == EDITypeByCarrier.NBEDISIANL.ToString() || sendEDIItem.ServiceKey == EDITypeByCarrier.NBEDIVGMANL.ToString() || sendEDIItem.ServiceKey == "SMLINE_FFBOOKING" || sendEDIItem.ServiceKey == "InttraVGM")
                {
                    string nbEDI = ds.Tables[0].Rows.Cast<DataRow>().Aggregate(string.Empty, (current, dt) => current + (dt[0] + Environment.NewLine));
                    if (sendEDIItem.ServiceKey == "NBEDISIANL" || sendEDIItem.ServiceKey == EDITypeByCarrier.NBEDIVGMANL.ToString())
                    {
                        tempBytes = Encoding.GetEncoding("GB2312").GetBytes(nbEDI);
                        datas = tempBytes;
                        tempstring = Encoding.GetEncoding("GB2312").GetString(datas);
                    }
                    else
                    {
                        tempBytes = Encoding.UTF8.GetBytes(nbEDI);
                        datas = tempBytes;
                        tempstring = Encoding.UTF8.GetString(datas);
                    }
                    TransmitEDI(sendEDIItem, ediConfig, documentNo, ref fromEMailAddress, ref toAddress, tempstring, datas);
                }
                else
                {
                    tempBytes = Encoding.UTF8.GetBytes(txml);
                    MemoryStream ms = new MemoryStream(tempBytes);
                    StreamInput inputStream = new StreamInput(ms);
                    //生成EDI文件
                    MemoryStream outms = new MemoryStream();
                    StreamOutput outStream = new StreamOutput(outms);
                    mapping.Run(inputStream, outStream);

                    //转成二进制通过FTP发送
                    datas = outms.ToArray();
                    ms.Close();
                    outms.Close();
                    inputStream.Close();
                    outStream.Close();
                    tempstring = Encoding.UTF8.GetString(datas);

                    #region 处理特殊数据

                    tempstring = XMLEncode(tempstring);
                    string pattern = @"\<Manifest[^\>]*\>";
                    tempstring = Regex.Replace(tempstring, pattern, "<Manifest>");
                    if (sendEDIItem.ServiceKey.ToUpper() == EDITypeByCarrier.AMS.ToString()
                  || sendEDIItem.ServiceKey.ToUpper() == EDITypeByCarrier.ACI.ToString()
                  || sendEDIItem.ServiceKey.ToUpper() == EDITypeByCarrier.ISF.ToString()
                  || sendEDIItem.ServiceKey.ToUpper() == EDITypeByCarrier.AMSACI.ToString()
                  || sendEDIItem.ServiceKey.ToUpper() == EDITypeByCarrier.AMSISF.ToString()
                  || sendEDIItem.ServiceKey.ToUpper() == EDITypeByCarrier.AMSACIISF.ToString())
                    {
                        tempstring = tempstring.Replace("&", "&amp;");
                    }

                    if (sendEDIItem.ServiceKey == "WorldexSO" || sendEDIItem.ServiceKey == "WorldexSI" || sendEDIItem.ServiceKey.ToUpper() == "CSCL_SI" || sendEDIItem.ServiceKey == "InttraSi" || sendEDIItem.ServiceKey == "InttraSo")
                    {
                        //替换中海特殊格式
                        //1、替换客户信息的空行格式
                        //2、替换客户全部为空的格式
                        //替换INTTRA特殊格式
                        //去除空值
                        tempstring = tempstring.Replace("C&#$", "");
                        tempstring = tempstring.Replace("NAD'" + Environment.NewLine, "");
                        tempstring = tempstring.Replace("NAD+CZ+++'" + Environment.NewLine, "");
                        tempstring = tempstring.Replace("NAD+CN+++'" + Environment.NewLine, "");
                        tempstring = tempstring.Replace("NAD+CN'" + Environment.NewLine, "");
                        tempstring = tempstring.Replace("NAD+NI+++'" + Environment.NewLine, "");
                        tempstring = tempstring.Replace("NAD+SF+++'" + Environment.NewLine, "");
                        tempstring = tempstring.Replace("NAD+ST+++'" + Environment.NewLine, "");
                        tempstring = tempstring.Replace("|", ":");
                        tempstring = tempstring.Replace("?+", "+");
                        tempstring = tempstring.Replace("??*", "?*");
                        if (sendEDIItem.ServiceKey == "InttraSo" || sendEDIItem.ServiceKey == "WorldexSO" || sendEDIItem.ServiceKey == "WorldexSI")
                        {
                            tempstring = tempstring.Replace("?*", "*");
                        }

                    }
                    if (sendEDIItem.ServiceKey.ToUpper() == "HANJIN_BOOKING" || sendEDIItem.ServiceKey.ToUpper() == "HANJIN_SI")
                    {
                        if (tempstring.Contains("@"))
                        {
                            tempstring = tempstring.Replace("@", "?@");
                        }
                        if (tempstring.Contains("!"))
                        {
                            tempstring = tempstring.Replace("!", "?!");
                        }
                        if (tempstring.Contains("#"))
                        {
                            tempstring = tempstring.Replace("#", "?#");
                        }
                        if (tempstring.Contains("$"))
                        {
                            tempstring = tempstring.Replace("$", "?$");
                        }
                        if (tempstring.Contains("^"))
                        {
                            tempstring = tempstring.Replace("^", "?^");
                        }
                        if (tempstring.Contains("%"))
                        {
                            tempstring = tempstring.Replace(" %", "?%");
                        }
                    }
                    #endregion
                    datas = Encoding.GetEncoding("UTF-8").GetBytes(tempstring);
                    TransmitEDI(sendEDIItem, ediConfig, documentNo, ref fromEMailAddress, ref toAddress, tempstring, datas);
                }
                #region LOG

                //记录日志
                Guid logID = Log(null,
                     sendEDIItem.EdiMode,
                     sendEDIItem.CompanyID,
                     new Guid[] { idtemp },
                     new string[] { businessNo },
                     documentNo,
                     sendEDIItem.Flag,
                     sendEDIItem.Subject,
                     fromEMailAddress,
                     toAddress,
                     sendEDIItem.Content,
                     tempstring,
                     DateTime.Now,
                     sendEDIItem.SendByID);


                MessageUserPropertiesObject userObject = new MessageUserPropertiesObject();
                userObject.Add("OperationId", id);
                userObject.Add("OperationType", OperationType.OceanExport);
                if (sendEDIItem.EdiMode == EDIMode.Booking)
                {
                    userObject.Add("FormType", FormType.Booking);
                }
                else if (sendEDIItem.EdiMode == EDIMode.SI)
                {
                    userObject.Add("FormType", FormType.MBL);
                }
                else//AMS ACI ISF
                    userObject.Add("FormType", FormType.HBL);
                userObject.Add("FormId", fid);

                MessageEDILogRelation logRelation = new MessageEDILogRelation
                {
                    EDIConfigId = ediConfig.ID,
                    Flag = sendEDIItem.Flag,
                    Type = sendEDIItem.EdiMode
                };
                //userObject.EDILogRelation = logRelation;
                userObject.Add("EDILog", logRelation);


                if (string.IsNullOrEmpty(fromEMailAddress))
                {
                    fromEMailAddress = "icpsystem@cityocean.com";
                }
                Message.ServiceInterface.Message message = new Message.ServiceInterface.Message
                {
                    CreateBy = sendEDIItem.SendByID,
                    CreateDate = DateTime.Now,
                    UserProperties = userObject,
                    BodyFormat = BodyFormat.olFormatHTML,
                    SendFrom = fromEMailAddress,
                    SendTo = toAddress,
                    Subject = sendEDIItem.Subject
                };
                if (sendEDIItem.ServiceKey.ToUpper().Contains("INTTRAS0"))
                {
                    message.Type = MessageType.SOEDI;
                }
                else if (sendEDIItem.ServiceKey.ToUpper().Contains("INTTRASI"))
                {
                    message.Type = MessageType.SIEDI;
                }
                else if (sendEDIItem.ServiceKey.ToUpper().Contains("INTTRAVGM"))
                {
                    message.Type = MessageType.VGMEDI;
                }
                else if (sendEDIItem.ServiceKey.ToUpper().Contains("AMS"))
                {
                    message.Type = MessageType.AMSEDI;
                }
                else
                    message.Type = MessageType.EDI;

                message.Body = tempstring;
                message.State = MessageState.Transmitted;

                _messageService.Save(message);
                bool isUpdatedMBL = true; //MBL更新状态
                //更新跟踪状态MBLD
                if (sendEDIItem.EdiMode == EDIMode.SI && sendEDIItem.ServiceKey != EDITypeByCarrier.NBEDICenter.ToString())
                {
                    isUpdatedMBL = UpdateMBLD(fid, sendEDIItem.SendByID);
                }

                #region 事件
                try
                {
                    EventObjects eventObject = new EventObjects
                    {
                        Id = Guid.Empty,
                        IsShowAgent = false,
                        IsShowCustomer = false,
                        OperationID = id,
                        OperationType = OperationType.OceanExport,
                        Owner = string.Empty,
                        Priority = MemoPriority.Normal,
                        Subject = sendEDIItem.Subject,
                        Type = MemoType.EDILog,
                        UpdateBy = sendEDIItem.SendByID,
                        UpdateDate = DateTime.Now
                    };
                    switch (sendEDIItem.EdiMode)
                    {
                        case EDIMode.Booking:
                            eventObject.Code = "SOB";
                            eventObject.FormType = FormType.Booking;
                            eventObject.CategoryName = "SO";
                            break;
                        case EDIMode.SI:
                            eventObject.Code = "MBLD";
                            eventObject.FormType = FormType.MBL;
                            eventObject.CategoryName = "SI";
                            break;
                        case EDIMode.AMS:
                            eventObject.Code = "AMS";
                            eventObject.FormType = FormType.HBL;
                            eventObject.CategoryName = "AMS";
                            UpdateAMSACIISFState(fid, sendEDIItem.SendByID, true, false, false);
                            break;
                        case EDIMode.ACI:
                            eventObject.Code = "ACI";
                            eventObject.FormType = FormType.HBL;
                            eventObject.CategoryName = "ACI";
                            UpdateAMSACIISFState(fid, sendEDIItem.SendByID, false, true, false);
                            break;
                        case EDIMode.ISF:
                            eventObject.Code = "ISF";
                            eventObject.FormType = FormType.HBL;
                            eventObject.CategoryName = "ISF";
                            UpdateAMSACIISFState(fid, sendEDIItem.SendByID, false, false, true);
                            break;
                        case EDIMode.AMSACI:
                            eventObject.Code = "AMSACI";
                            eventObject.FormType = FormType.HBL;
                            eventObject.CategoryName = "AMSACI";
                            UpdateAMSACIISFState(fid, sendEDIItem.SendByID, true, true, false);
                            break;
                        case EDIMode.AMSISF:
                            eventObject.Code = "AMSISF";
                            eventObject.FormType = FormType.HBL;
                            eventObject.CategoryName = "AMSISF";
                            UpdateAMSACIISFState(fid, sendEDIItem.SendByID, true, false, true);
                            break;
                        case EDIMode.AMSACIISF:
                            eventObject.Code = "AMSACIISF";
                            eventObject.FormType = FormType.HBL;
                            eventObject.CategoryName = "AMSACIISF";
                            UpdateAMSACIISFState(fid, sendEDIItem.SendByID, true, true, true);
                            break;
                        case EDIMode.VGM:
                            eventObject.Code = "VGM";
                            eventObject.FormType = FormType.MBL;
                            eventObject.CategoryName = "VGM";
                            UpdateAMSACIISFState(fid, sendEDIItem.SendByID, true, true, true);
                            break;
                    }

                    eventObject.CreateDate = DateTime.Now;
                    eventObject.Description = sendEDIItem.Subject;
                    eventObject.FormID = idtemp;
                    FCMCommonService.SaveMemoInfo(eventObject);
                }
                catch (Exception ex)
                {
                    CommonLogHelper.SaveLog("EDIService", string.Format("{0}{1}", "SaveMemoInfo", ex.Message));
                    throw new Exception("EDI已发送，保存事件发生异常");
                }
                #endregion

                if (logID == Guid.Empty)
                {
                    throw new Exception("EDI已发送，记录日志发生异常");
                }
                if (!isUpdatedMBL)
                {
                    throw new Exception("EDI已发送，更新MBL状态发生异常");
                }
                #endregion
            }
            catch (ApplicationException appex)
            {
                throw appex;
            }
            catch (Exception ex)
            {
                CommonLogHelper.SaveLog("EDIService", CommonHelper.BuildExceptionString(ex));
                if (ex.Message.Contains("由于套接字没有连接"))
                {
                    throw new ApplicationException("由于网络延迟发送失败，请稍后重新发送！");
                }
                throw new ApplicationException(ex.Message);
            }
        }
        #endregion

        #region BuildCondition
        /// <summary>
        /// 构建查询参数
        /// </summary>
        /// <param name="sendItem"></param>
        /// <param name="documentno"></param>
        /// <returns></returns>
        [Obsolete("方法已过时，请使用 buildCondition(EDISendOption sendOption) 代替")]
        string BuildCondition(EDISendOption sendItem, string documentno)
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlNode checknode = CreateXmlNode(xmldoc, "EDI", "");
            CreateXmlNode(xmldoc, "MIDs", sendItem.FIDs.Join(), ref checknode);
            CreateXmlNode(xmldoc, "DOCUMENTNO", documentno, ref checknode);
            CreateXmlNode(xmldoc, "DOCUMENTFLAG", ((short)sendItem.Flag).ToString(CultureInfo.InvariantCulture), ref checknode);
            CreateXmlNode(xmldoc, "SendID", sendItem.SendByID.ToString(), ref checknode);
            CreateXmlNode(xmldoc, "IsEnglish", false.ToString(), ref checknode);
            return checknode.OuterXml;
        }
        /// <summary>
        /// 构建查询参数
        /// </summary>
        /// <param name="sendItem"></param>
        /// <param name="blids"></param>
        /// <param name="type"></param>
        /// <param name="documentno"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        [Obsolete("方法已过时，请使用 buildCondition(EDISendOption sendOption) 代替")]
        string BuildCondition(EDISendOption sendItem, Guid blids, OperationType type, string documentno)
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlNode checknode = CreateXmlNode(xmldoc, "EDI", "");
            CreateXmlNode(xmldoc, "ID", blids.ToString(), ref checknode);
            CreateXmlNode(xmldoc, "JOBTYPE", ((short)type).ToString(CultureInfo.InvariantCulture), ref checknode);
            CreateXmlNode(xmldoc, "DOCUMENTNO", documentno, ref checknode);
            CreateXmlNode(xmldoc, "DOCUMENTFLAG", ((short)sendItem.Flag).ToString(CultureInfo.InvariantCulture), ref checknode);
            CreateXmlNode(xmldoc, "SendID", sendItem.SendByID.ToString(), ref checknode);
            CreateXmlNode(xmldoc, "PILCommodity", sendItem.PILCommodity, ref checknode);
            CreateXmlNode(xmldoc, "IsEnglish", false.ToString(), ref checknode);
            CreateXmlNode(xmldoc, "AMSEntryType", ((short)sendItem.AMSEntryType).ToString(CultureInfo.InvariantCulture), ref checknode);//AMS(60=港口到港口61=内陆运输62=运输/出口63=立即再出口64=留在船上)
            CreateXmlNode(xmldoc, "ACIEntryType", ((short)sendItem.AMSEntryType).ToString(CultureInfo.InvariantCulture), ref checknode);//23=转运到美国 24=进口 26=过境货(ACI专用)
            CreateXmlNode(xmldoc, "consigneeName", documentno, ref checknode);
            CreateXmlNode(xmldoc, "shipperFormat", documentno, ref checknode);
            CreateXmlNode(xmldoc, "consigneeFormat", documentno, ref checknode);
            return checknode.OuterXml;
        }

        /// <summary>
        /// 构建查询参数(Inttra)
        /// </summary>
        /// <param name="sendItem"></param>
        /// <param name="blids"></param>
        /// <param name="type"></param>
        /// <param name="documentno"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        [Obsolete("方法已过时，请使用 buildCondition(EDISendOption sendOption) 代替")]
        string BuildConditionForSpecial(EDISendOption sendItem, Guid blids, OperationType type, string documentno, int num)
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlNode checknode = CreateXmlNode(xmldoc, "EDI", "");
            CreateXmlNode(xmldoc, "ID", blids.ToString(), ref checknode);
            CreateXmlNode(xmldoc, "JOBTYPE", ((short)type).ToString(CultureInfo.InvariantCulture), ref checknode);
            CreateXmlNode(xmldoc, "DOCUMENTNO", documentno, ref checknode);
            CreateXmlNode(xmldoc, "DOCUMENTFLAG", ((short)sendItem.Flag).ToString(CultureInfo.InvariantCulture), ref checknode);
            CreateXmlNode(xmldoc, "SendID", sendItem.SendByID.ToString(), ref checknode);
            CreateXmlNode(xmldoc, "PILCommodity", sendItem.PILCommodity, ref checknode);
            CreateXmlNode(xmldoc, "IsEnglish", false.ToString(), ref checknode);
            CreateXmlNode(xmldoc, "AMSEntryType", ((short)sendItem.AMSEntryType).ToString(CultureInfo.InvariantCulture), ref checknode);//AMS(60=港口到港口61=内陆运输62=运输/出口63=立即再出口64=留在船上)
            CreateXmlNode(xmldoc, "ACIEntryType", ((short)sendItem.AMSEntryType).ToString(CultureInfo.InvariantCulture), ref checknode);//23=转运到美国 24=进口 26=过境货(ACI专用)
            if (sendItem.ShipperName != null)
            {
                CreateXmlNode(xmldoc, "shipperName", sendItem.ShipperName[num], ref checknode);
                CreateXmlNode(xmldoc, "consigneeName", sendItem.ConsigneeName[num], ref checknode);
                CreateXmlNode(xmldoc, "shipperFormat", sendItem.ShipperFormat[num], ref checknode);
                CreateXmlNode(xmldoc, "consigneeFormat", sendItem.ConsigneeFormat[num], ref checknode);
                CreateXmlNode(xmldoc, "nitifyFormat", sendItem.NotifyFormat[num], ref checknode);
                CreateXmlNode(xmldoc, "nitifyName", sendItem.NotifyName[num], ref checknode);
                CreateXmlNode(xmldoc, "goodinfoFormat", sendItem.GoodinfoFormat[num], ref checknode);
                CreateXmlNode(xmldoc, "markFormat", sendItem.MarkFormat[num], ref checknode);
            }
            CreateXmlNode(xmldoc, "ServiceKey", sendItem.ServiceKey, ref checknode);
            return checknode.OuterXml;
        }
        /// <summary>
        /// 构建查询参数(宁波)
        /// </summary>
        /// <param name="NBObj"></param>
        /// <param name="blids"></param>
        /// <param name="type"></param>
        /// <param name="documentno"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        [Obsolete("方法已过时，请使用 buildCondition(EDISendOption sendOption) 代替")]
        string BuildConditionForNB(EDISendOption NBObj, Guid blids, OperationType type, string documentno, int num)
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlNode checknode = CreateXmlNode(xmldoc, "EDI", "");
            if (NBObj.ServiceKey == "NBEDIBookingANL" && NBObj.EdiMode == EDIMode.Booking)
            {
                CreateXmlNode(xmldoc, "ID", blids.ToString(), ref checknode);
            }
            else if (NBObj.ServiceKey == "NBEDISIANL")
            {
                CreateXmlNode(xmldoc, "ID", blids.ToString(), ref checknode);
            }
            else
            {
                string tempHblIds = NBObj.FIDs.Join();
                CreateXmlNode(xmldoc, "ID", NBObj.FIDs.Join(), ref checknode);
            }

            if ((NBObj.ServiceKey == "NBEDIBookingANL" && NBObj.EdiMode == EDIMode.SI) || NBObj.ServiceKey == "NBEDIVGMANL")
            {
                CreateXmlNode(xmldoc, "Agent", NBObj.Agent, ref checknode);
            }
            else
            {
                CreateXmlNode(xmldoc, "Agent", "", ref checknode);
            }

            CreateXmlNode(xmldoc, "AgentName", NBObj.AgentName, ref checknode);
            CreateXmlNode(xmldoc, "Contact", NBObj.Contact, ref checknode);
            CreateXmlNode(xmldoc, "ContactTel", NBObj.ContactTel, ref checknode);
            CreateXmlNode(xmldoc, "ContactEmail", NBObj.ContactEmail, ref checknode);
            CreateXmlNode(xmldoc, "Department", NBObj.Department, ref checknode);
            CreateXmlNode(xmldoc, "VGMDate", NBObj.VGMDate, ref checknode);
            CreateXmlNode(xmldoc, "VGMRrmark", NBObj.VGMRrmark, ref checknode);
            CreateXmlNode(xmldoc, "bookid", (NBObj.IDs[0]).ToString(), ref checknode);
            CreateXmlNode(xmldoc, "JOBTYPE", ((short)type).ToString(CultureInfo.InvariantCulture), ref checknode);
            CreateXmlNode(xmldoc, "DOCUMENTNO", documentno, ref checknode);
            CreateXmlNode(xmldoc, "DOCUMENTFLAG", ((short)NBObj.Flag).ToString(), ref checknode);
            CreateXmlNode(xmldoc, "SendID", NBObj.SendByID.ToString(), ref checknode);
            CreateXmlNode(xmldoc, "TYPE", NBObj.EdiMode.ToString(), ref checknode);

            if (NBObj.ServiceKey == "NBEDIVGMANL" || NBObj.ServiceKey == "NBEDISIANL")
            {
                CreateXmlNode(xmldoc, "shipperFormat", NBObj.ShipperFormat[num], ref checknode);
                CreateXmlNode(xmldoc, "consigneeFormat", NBObj.ConsigneeFormat[num], ref checknode);
                CreateXmlNode(xmldoc, "nitifyFormat", NBObj.NotifyFormat[num], ref checknode);
                CreateXmlNode(xmldoc, "otherFormat", NBObj.OtherFormat[num], ref checknode);
            }
            CreateXmlNode(xmldoc, "IsEnglish", false.ToString(), ref checknode);
            return checknode.OuterXml;
        }
        #endregion

        #region Transmit EDI Data
        /// <summary>
        /// 通过邮件/ftp传输数据
        /// </summary>
        /// <param name="sendEDIItem"></param>
        /// <param name="ediConfig"></param>
        /// <param name="documentNo"></param>
        /// <param name="fromEMailAddress"></param>
        /// <param name="toAddress"></param>
        /// <param name="tempstring"></param>
        /// <param name="datas"></param>
        [Obsolete("方法已过时，请使用 transmitData(EDISendOption sendOption, EDIConfigItem ediConfig,ref string fromEMailAddress, ref string toAddress, string tempstring, byte[] datas) 代替")]
        void TransmitEDI(EDISendOption sendEDIItem, EDIConfigItem ediConfig, string documentNo, ref string fromEMailAddress, ref string toAddress, string tempstring, byte[] datas)
        {
            #region 发送数据

            string xpath = _EDIXMLDir;
            switch (sendEDIItem.ServiceKey)
            {
                case "NBEDIBookingANL":
                    switch (sendEDIItem.EdiMode)
                    {
                        case EDIMode.Booking:
                            xpath += @"BookingXML\";
                            break;
                        case EDIMode.SI:
                            xpath += @"SI\";
                            break;
                    }
                    break;
                case "NBEDICenter":
                    xpath += @"EDICenter\";
                    break;
                case "NBEDISIANL":
                    xpath += @"SIXML\";
                    break;
                case "NBEDIVGMANL":
                    xpath += @"VGM\";
                    break;
            }

            switch (sendEDIItem.ServiceKey)
            {
                case "NBEDICenter":
                    {
                        #region Write Txt
                        if (!Directory.Exists(xpath))
                        {
                            Directory.CreateDirectory(xpath);
                        }

                        FileStream fs = new FileStream(xpath + sendEDIItem.Subject + ".txt", FileMode.Create);
                        StreamWriter sw = new StreamWriter(fs);

                        try
                        {
                            sw.Write(tempstring);
                            sw.Flush();

                            sw.Close();
                            fs.Close();
                        }
                        catch (Exception ex)
                        {
                            throw new ApplicationException(ex.Message);
                        }

                        return;
                        #endregion
                    }
                case "NBEDIVGMANL":
                case "NBEDISIANL":
                case "NBEDIBookingANL":
                    {
                        #region Write XML
                        if (!Directory.Exists(xpath))
                        {
                            Directory.CreateDirectory(xpath);
                        }
                        FileStream fs;
                        StreamWriter sw;
                        Random r = new Random();
                        if (File.Exists(xpath + sendEDIItem.Subject + ".XML"))
                        {
                            fs = new FileStream(xpath + sendEDIItem.Subject + "_" + r.Next(10000).ToString() + ".XML", FileMode.Create);
                            sw = new StreamWriter(fs, Encoding.GetEncoding("GB2312"));
                        }
                        else
                        {
                            fs = new FileStream(xpath + sendEDIItem.Subject + ".XML", FileMode.Create);
                            sw = new StreamWriter(fs, Encoding.GetEncoding("GB2312"));
                        }


                        try
                        {
                            sw.Write(tempstring);
                            sw.Flush();

                            sw.Close();
                            fs.Close();
                        }
                        catch (Exception ex)
                        {
                            throw new ApplicationException(ex.Message);
                        }
                        return;
                        #endregion
                    }
                case "SMLINE_FFBOOKING":
                    return;
            }


            //判断是发送邮件还是发送FTP文件
            if (ediConfig.UploadMode == EDIUploadMode.EMail && string.IsNullOrEmpty(ediConfig.ReceiveAddress) == false)
            {
                #region 邮箱
                byte[] temBytes = Encoding.UTF8.GetBytes(tempstring);

                //获得发送者的邮箱信息
                List<UserMailAccountList> tos = _userService.GetUserMailAccountList(new Guid[] { sendEDIItem.SendByID });
                int port = 587;
                if (tos != null && tos.Count > 0)
                {
                    ediConfig.UserName = fromEMailAddress = tos[0].Email;
                    ediConfig.Password = tos[0].MailOutgoingPassword;
                    port = tos[0].MailOutgoingPort;
                }
                toAddress = ediConfig.ReceiveAddress;
                if (!string.IsNullOrEmpty(ediConfig.ToAddress))
                {
                    toAddress = ediConfig.ToAddress;
                }

                if (string.IsNullOrEmpty(fromEMailAddress))
                {
                    throw new ApplicationException("请配置您的邮箱信息");
                }

                if (string.IsNullOrEmpty(toAddress))
                {
                    throw new ApplicationException("请在EDI配置中设置接收人的邮箱地址");
                }
                //如果配置的有邮件，则通过邮件发送EDI
                SendEMail(ediConfig, port, ediConfig.UserName, toAddress, sendEDIItem.Subject, sendEDIItem.Content, new[] { temBytes }, new[] { documentNo }, new[] { ediConfig.FileFormat });
                #endregion
            }
            else if (ediConfig.UploadMode == EDIUploadMode.FTP && string.IsNullOrEmpty(ediConfig.ServerAddress) == false)
            {
                #region FTP
                fromEMailAddress = ApplicationContext.Current.Username;
                //文件格式.txt .xml
                string fileName = documentNo + "." + ediConfig.FileFormat;
                //如果配置了FTP则通过FTP发送EDI
                if (!string.IsNullOrEmpty(ediConfig.FTPPath))
                {
                    toAddress = ediConfig.FTPPath;
                }
                if (!string.IsNullOrEmpty(ediConfig.ToAddress))
                {
                    if (!string.IsNullOrEmpty(toAddress))
                    {
                        toAddress = toAddress + "/" + ediConfig.ToAddress;
                    }
                    else
                    {
                        toAddress = ediConfig.ToAddress;
                    }
                }


                if (string.IsNullOrEmpty(ediConfig.ServerAddress))
                {
                    throw new ApplicationException("请在EDI配置中设置FTP服务器地址");
                }

                FtpHelper.SendFile(ediConfig.ServerAddress, toAddress, ediConfig.UserName, ediConfig.Password, sendEDIItem.Content, fileName, datas);

                #endregion
            }
            else
            {
                throw new ApplicationException("请配置正确的发送EDI方式(EMail或FTP)");
            }
            #endregion
        }
        /// <summary>
        /// 通过邮件/ftp传输数据
        /// </summary>
        /// <param name="sendEDIItem"></param>
        /// <param name="ediConfig"></param>
        /// <param name="documentNo"></param>
        /// <param name="fromEMailAddress"></param>
        /// <param name="toAddress"></param>
        /// <param name="tempstring"></param>
        /// <param name="datas"></param>
        [Obsolete("方法已过时，请使用 transmitData(EDISendOption sendOption, EDIConfigItem ediConfig,ref string fromEMailAddress, ref string toAddress, string tempstring, byte[] datas) 代替")]
        void TransmitData(EDISendOption sendEDIItem, EDIConfigItem ediConfig, string documentNo,
            ref string fromEMailAddress, ref string toAddress, string tempstring, byte[] datas)
        {
            switch (ediConfig.UploadMode)
            {
                case EDIUploadMode.Disk:
                    #region 写入本地磁盘
                    string xpath = _EDIXMLDir;
                    switch (sendEDIItem.ServiceKey)
                    {
                        case "NBEDIBookingANL":
                            switch (sendEDIItem.EdiMode)
                            {
                                case EDIMode.Booking:
                                    xpath += @"BookingXML\";
                                    break;
                                case EDIMode.SI:
                                    xpath += @"SI\";
                                    break;
                            }
                            break;
                        case "NBEDICenter":
                            xpath += @"EDICenter\";
                            break;
                        case "NBEDISIANL":
                            xpath += @"SIXML\";
                            break;
                        case "NBEDIVGMANL":
                            xpath += @"VGM\";
                            break;
                    }
                    switch (ediConfig.FileFormat.ToLower())
                    {
                        case "txt":
                            #region Write Txt
                            if (!Directory.Exists(xpath))
                            {
                                Directory.CreateDirectory(xpath);
                            }

                            FileStream fstxt = new FileStream(xpath + sendEDIItem.Subject + ".txt", FileMode.Create);
                            StreamWriter swtxt = new StreamWriter(fstxt);

                            try
                            {
                                swtxt.Write(tempstring);
                                swtxt.Flush();

                                swtxt.Close();
                                fstxt.Close();
                            }
                            catch (Exception ex)
                            {
                                throw new ApplicationException(ex.Message);
                            }
                            #endregion
                            break;
                        case "xml":
                            #region Write XML
                            if (!Directory.Exists(xpath))
                            {
                                Directory.CreateDirectory(xpath);
                            }
                            FileStream fsxml;
                            StreamWriter swxml;
                            Random r = new Random();
                            if (File.Exists(xpath + sendEDIItem.Subject + ".XML"))
                            {
                                fsxml = new FileStream(xpath + sendEDIItem.Subject + "_" + r.Next(10000).ToString() + ".XML", FileMode.Create);
                                swxml = new StreamWriter(fsxml, Encoding.GetEncoding("GB2312"));
                            }
                            else
                            {
                                fsxml = new FileStream(xpath + sendEDIItem.Subject + ".XML", FileMode.Create);
                                swxml = new StreamWriter(fsxml, Encoding.GetEncoding("GB2312"));
                            }


                            try
                            {
                                swxml.Write(tempstring);
                                swxml.Flush();

                                swxml.Close();
                                fsxml.Close();
                            }
                            catch (Exception ex)
                            {
                                throw new ApplicationException(ex.Message);
                            }
                            #endregion
                            break;
                    }
                    #endregion
                    break;
                case EDIUploadMode.EMail:
                    #region 邮箱
                    byte[] temBytes = Encoding.UTF8.GetBytes(tempstring);

                    //获得发送者的邮箱信息
                    List<UserMailAccountList> tos = _userService.GetUserMailAccountList(new[] { sendEDIItem.SendByID });
                    int port = 587;
                    if (tos != null && tos.Count > 0)
                    {
                        ediConfig.UserName = fromEMailAddress = tos[0].Email;
                        ediConfig.Password = tos[0].MailOutgoingPassword;
                        port = tos[0].MailOutgoingPort;
                    }
                    toAddress = ediConfig.ReceiveAddress;
                    if (!string.IsNullOrEmpty(ediConfig.ToAddress))
                    {
                        toAddress = ediConfig.ToAddress;
                    }

                    if (string.IsNullOrEmpty(fromEMailAddress))
                    {
                        throw new ApplicationException("请配置您的邮箱信息");
                    }

                    if (string.IsNullOrEmpty(toAddress))
                    {
                        throw new ApplicationException("请在EDI配置中设置接收人的邮箱地址");
                    }
                    //如果配置的有邮件，则通过邮件发送EDI
                    SendEMail(ediConfig, port, ediConfig.UserName, toAddress, sendEDIItem.Subject, sendEDIItem.Content, new[] { temBytes }, new[] { documentNo }, new[] { ediConfig.FileFormat });
                    #endregion
                    break;
                case EDIUploadMode.FTP:
                    #region FTP
                    fromEMailAddress = ApplicationContext.Current.Username;
                    //文件格式.txt .xml
                    string fileName = documentNo + "." + ediConfig.FileFormat;
                    //如果配置了FTP则通过FTP发送EDI
                    if (!string.IsNullOrEmpty(ediConfig.FTPPath))
                    {
                        toAddress = ediConfig.FTPPath;
                    }
                    if (!string.IsNullOrEmpty(ediConfig.ToAddress))
                    {
                        if (!string.IsNullOrEmpty(toAddress))
                        {
                            toAddress = toAddress + "/" + ediConfig.ToAddress;
                        }
                        else
                        {
                            toAddress = ediConfig.ToAddress;
                        }
                    }


                    if (string.IsNullOrEmpty(ediConfig.ServerAddress))
                    {
                        throw new ApplicationException("请在EDI配置中设置FTP服务器地址");
                    }

                    FtpHelper.SendFile(ediConfig.ServerAddress, toAddress, ediConfig.UserName, ediConfig.Password, sendEDIItem.Content, fileName, datas);
                    #endregion
                    break;
                case EDIUploadMode.WebAPI:
                    string postData = BuildPostData(ediConfig.FTPPath, tempstring, ediConfig.UserName, ediConfig.Password);
                    string resportString = HTTPHelper.HttpUploadData(ediConfig.ServerAddress, postData);
                    CLP_Result clpResult = JsonConvert.DeserializeObject<CLP_Result>(resportString);
                    if ("F".Equals(clpResult.data.result))
                    {
                        throw new Exception(clpResult.data.errorInfo);
                    }
                    break;
                default:
                    throw new ApplicationException("请配置正确的发送EDI方式(EMail或FTP)");
                    break;
            }

        } 
        #endregion

        
        

        /// <summary>
        /// 获取EDI内容(数据集)
        /// </summary>
        /// <param name="storedProcedure">存储过程</param>
        /// <param name="condition">存储过程参数条件</param>
        /// <param name="dataFormat">数据格式</param>
        /// <returns></returns>
        [Obsolete("方法已过时，请使用 getDataSetContent(EDISendOption sendOption, EDIConfigItem ediConfig) 代替")]
        DataSet getEDIDataSetContent(string storedProcedure, string condition, string dataFormat)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(storedProcedure);

            db.AddInParameter(dbCommand, "@Condition", DbType.String, condition);

            DataSet temp = db.ExecuteDataSet(dbCommand);

            DataSet ds = new DataSet();

            string path = _EDIServerDir + "\\DataSchemas\\" + dataFormat;
            ds.ReadXmlSchema(path);

            //处理关系
            DataRelation[] relations = new DataRelation[ds.Relations.Count];
            ds.Relations.CopyTo(relations, 0);
            ds.Relations.Clear();

            bool isProcess = relations.Any() && relations[0].ChildColumns[0].DataType == typeof(int);

            for (int i = 0; i < temp.Tables.Count; i++)
            {
                //首先修改表名,在合并的时候才能正确的合并到对应的表里。
                temp.Tables[i].TableName = ds.Tables[i].TableName;

                //处理所有列为字符串，且可为空。
                if (!isProcess) continue;
                foreach (DataColumn dc in ds.Tables[i].Columns)
                {
                    dc.DataType = typeof(string);
                    dc.AllowDBNull = true;
                }
            }
            ds.Relations.AddRange(relations);
            ds.Merge(temp);

            return ds;
        }

        #region XML操作
        /// <summary>
        /// 创建XML节点(返回节点)
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="nodeName"></param>
        /// <param name="nodeValue"></param>
        /// <returns></returns>
        XmlNode CreateXmlNode(XmlDocument xmlDoc, string nodeName, string nodeValue)
        {
            XmlNode tmpNode = xmlDoc.CreateNode(XmlNodeType.Element, nodeName, "");
            if (!string.IsNullOrEmpty(nodeValue)) tmpNode.InnerText = nodeValue;
            return tmpNode;
        }
        /// <summary>
        /// 创建XML节点(ref 节点)
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="nodeName"></param>
        /// <param name="nodeValue"></param>
        /// <param name="parentNode"></param>
        void CreateXmlNode(XmlDocument xmlDoc, string nodeName, string nodeValue, ref XmlNode parentNode)
        {
            XmlNode tmpNode = xmlDoc.CreateNode(XmlNodeType.Element, nodeName, "");
            if (!string.IsNullOrEmpty(nodeValue)) tmpNode.InnerText = nodeValue;
            parentNode.AppendChild(tmpNode);
        }

        /// <summary>
        /// 替换特殊字符
        /// </summary>
        /// <param name="sText"></param>
        /// <returns></returns>
        string XMLEncode(string sText)
        {
            sText = sText.Replace("&amp;", "&");
            sText = sText.Replace("&amp;", "&");
            sText = sText.Replace("&lt;", "<");
            sText = sText.Replace("&gt;", ">");
            sText = sText.Replace("&apos;", "?'");
            sText = sText.Replace("&quot;", "\"");
            return sText;
        }
        #endregion

        #region 发送邮件

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="item"></param>
        /// <param name="port"></param>
        /// <param name="sender"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="content"></param>
        /// <param name="attachFileContent"></param>
        /// <param name="attachFileNames"></param>
        /// <param name="attachFileType"></param>
        void SendEMail(EDIConfigItem item, int port, string sender, string to, string subject
            , string content, byte[][] attachFileContent, string[] attachFileNames, string[] attachFileType)
        {
            try
            {
                List<AttachmentContent> attachmentContents = new List<AttachmentContent>();
                if (attachFileNames.Length > 0)
                {
                    for (int i = 0; i < attachFileNames.Length; i++)
                    {
                        AttachmentContent ac = new AttachmentContent()
                        {
                            Name= attachFileNames[i] + "." + attachFileType[i],
                            DisplayName = attachFileNames[i] + "." + attachFileType[i],
                            Content = attachFileContent[i],
                        };
                        attachmentContents.Add(ac);
                    }
                }
                ICP.Message.ServiceInterface.Message message = new Message.ServiceInterface.Message
                {
                    Type = MessageType.Email,
                    SendFrom = sender,
                    SendTo = to,
                    Subject = subject,
                    BodyFormat = BodyFormat.olFormatHTML,
                    CreateBy = CurrentUserId,
                    CreatorName = "System",
                    CreateDate = DateTime.Now,
                    Attachments= attachmentContents,
                };
                message.Body = content.ToString();
                MailBeeService.Send(message);
            }
            catch
            {
                EMailHelper.SendMail(item.ServerAddress, item.UserName, item.Password, port, sender, to, string.Empty, string.Empty, subject, content, attachFileContent, attachFileNames, attachFileType);
            }

        }

        #endregion

        #region 补料后更新跟踪状态(事件)
        /// <summary>
        /// 补料后更新跟踪状态(事件)
        /// </summary>
        /// <param name="MBLID"></param>
        /// <param name="updateBy"></param>
        bool UpdateMBLD(Guid MBLID, Guid updateBy)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("fcm.uspUpdateMBLDByMBLID");

                db.AddInParameter(dbCommand, "@MBLID", DbType.Guid, MBLID);
                db.AddInParameter(dbCommand, "@UpdateBy", DbType.Guid, updateBy);
                db.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                CommonLogHelper.SaveLog("EDIService", string.Format("{0}{1}", "UpdateMBLD", ex.Message));
                return false;
            }
            return true;
        }
        #endregion

        #region AMSACIISF后更新跟踪状态(事件)
        /// <summary>
        /// AMSACIISF后更新跟踪状态(事件)
        /// </summary>
        /// <param name="HBLID"></param>
        /// <param name="updateBy"></param>
        /// <param name="ams"></param>
        /// <param name="aci"></param>
        /// <param name="isf"></param>
        void UpdateAMSACIISFState(Guid HBLID, Guid updateBy, bool ams, bool aci, bool isf)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("fcm.uspUpdateHBLState");

            db.AddInParameter(dbCommand, "@ID", DbType.Guid, HBLID);
            db.AddInParameter(dbCommand, "@UpdateBy", DbType.Guid, updateBy);
            db.AddInParameter(dbCommand, "@AMS", DbType.Boolean, ams);
            db.AddInParameter(dbCommand, "@ACI", DbType.Boolean, aci);
            db.AddInParameter(dbCommand, "@ISF", DbType.Boolean, isf);
            db.ExecuteNonQuery(dbCommand);
        }
        #endregion

        string ConvertCLPToJSONString(DataSet ds)
        {
            List<CLP_BillInfo> BillInfo = (from b in ds.Tables[0].AsEnumerable()
                            select new CLP_BillInfo
                            {
                                billNo = b.Field<string>("billNo"),
                                vesselName = b.Field<string>("vesselName"),
                                voyageNo = b.Field<string>("voyageNo"),
                                //lineCode =b.Field<string>("lineCode"),
                                loadPort = b.Field<string>("loadPort"),
                                //loadPortCode = b.Field<string>("loadPortCode"),
                                deliveryPlace = b.Field<string>("deliveryPlace"),
                                //deliveryPlaceCode = b.Field<string>("deliveryPlaceCode"),
                                destinationPort = b.Field<string>("destinationPort"),
                                //destinationPortCode = b.Field<string>("destinationPortCode"),
                                //receiptPlace = b.Field<string>("receiptPlace"),
                                transitPort = b.Field<string>("transitPort"),
                                clearingCode = b.Field<string>("clearingCode"),
                                carrierCode = b.Field<string>("carrierCode"),
                                carrier = b.Field<string>("carrier"),
                                //ctnOperatorCode = b.Field<string>("ctnOperatorCode"),
                                //forwarderContacts = b.Field<string>("forwarderContacts"),
                                //forwarderTel = b.Field<string>("forwarderTel"),
                                //forwarderName = b.Field<string>("forwarderName"),
                                //email = b.Field<string>("email"),
                                //consignmentNo = b.Field<string>("consignmentNo"),
                                ctnType = b.Field<string>("ctnType"),
                                costcoNo = b.Field<string>("costcoNo"),
                                firstForwarderName = b.Field<string>("firstForwarderName"),
                                //contactEmail = b.Field<string>("contactEmail"),
                                dataSource = b.Field<string>("dataSource"),
                                //bookingOrgName = b.Field<string>("bookingOrgName"),
                                //clientCode = b.Field<string>("clientCode"),
                                //carrierType = b.Field<string>("carrierType"),
                                //cargoType = b.Field<string>("cargoType"),
                                //place = b.Field<string>("place"),
                                //ctnLoadRequire = b.Field<string>("ctnLoadRequire"),
                                //ctnLoadTime = b.Field<string>("ctnLoadTime"),
                                //ctnLoadContact = b.Field<string>("ctnLoadContact"),
                                //ctnLoadTel = b.Field<string>("ctnLoadTel"),
                                //ctnRemark = b.Field<string>("ctnRemark"),
                                customerName = b.Field<string>("customerName"),
                                customerTel = b.Field<string>("customerTel"),
                                customerEmail = b.Field<string>("customerEmail"),
                                //documentName = b.Field<string>("documentName"),
                                //documentTel = b.Field<string>("documentTel"),
                                //documentEmail = b.Field<string>("documentEmail"),
                                //closingDate = b.Field<string>("closingDate"),
                                //cutOffTime = b.Field<string>("cutOffTime"),
                                isShutOut = b.Field<string>("isShutOut"),
                                //pickYardName = b.Field<string>("pickYardName"),
                                //dockName = b.Field<string>("dockName"),
                                //unCode = b.Field<string>("unCode"),
                                tradeFlag = b.Field<string>("tradeFlag"),
                                //fleetName = b.Field<string>("fleetName"),
                                //contractNo = b.Field<string>("contractNo"),
                                //contractCustomer = b.Field<string>("contractCustomer"),
                                //etd = b.Field<string>("etd"),
                                //bookingOrgCode = b.Field<string>("bookingOrgCode"),
                                //busClientCode = b.Field<string>("busClientCode"),
                                //yardClosingTime = b.Field<string>("yardClosingTime"),
                                //ensClosingTime = b.Field<string>("ensClosingTime"),
                                //amsClosingTime = b.Field<string>("amsClosingTime"),
                                //jobNo = b.Field<string>("jobNo"),
                                sendType = b.Field<string>("sendType"),

                            }).ToList();
            foreach (var item in BillInfo)
            {
                item.cargoList = (from b in ds.Tables[1].AsEnumerable()
                                  select new CLP_CargoInfo
                                  {
                                      otherBillNo = b.Field<string>("otherBillNo"),
                                      //packageType = b.Field<string>("packageType"),
                                      //packageTypeCode = b.Field<string>("packageTypeCode"),
                                      qty = b.Field<int>("qty"),
                                      grossWeight = b.Field<decimal>("grossWeight"),
                                      volume = b.Field<decimal>("volume"),
                                      //cargoName = b.Field<string>("cargoName"),
                                      //remark = b.Field<string>("remark"),
                                  }).ToList();
            }

            JsonSerializerSettings mJsonSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            return JsonConvert.SerializeObject(BillInfo, Newtonsoft.Json.Formatting.None, mJsonSettings);
        }

        private static string BuildPostData(string method, string data, string userID, string key)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string sign = string.Format("data={0}&format=json&method={1}&timestamp={2}&user_id={3}&version=2.0&key={4}", data, method, timestamp, userID, key);
            //Console.WriteLine("\r\nSign\r\n{0}", sign);
            //Console.WriteLine("\r\nEncrypt Sign\r\n{0}", Cryptography.EncryptByMD532Bit(sign));
            return string.Format("user_id={0}&version=2.0&format=json&timestamp={1}&sign={2}&method={3}&data={4}", userID, timestamp, Cryptography.EncryptByMD532Bit(sign), method, HttpUtility.UrlEncode(data));
        }
    }

}
