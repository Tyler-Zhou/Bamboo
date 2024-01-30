using Altova.IO;
using ICP.EDI.PluginInterface;
using ICP.EDI.ServiceComponent.Rule;
using ICP.EDI.ServiceInterface;
using ICP.EDI.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Message.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using CommonLogHelper = ICP.Framework.CommonLibrary.LogHelper;

namespace ICP.EDI.ServiceComponent
{
    partial class EDIService
    {
        /// <summary>
        /// 发送EDI
        /// </summary>
        /// <param name="sendOption">发送选项</param>
        /// <param name="configOption">配置选项</param>
        /// <returns></returns>
        public void Send(EDISendOption sendOption, EDIConfigItem configOption)
        {
            if(configOption==null)
            {
                configOption = GetEDIConfigByOption(sendOption);
            }
            if (configOption == null)
            {
                throw new ApplicationException("没有找到该船东/承运人的EDI服务");
            }
            sendOption.ServiceKey = configOption.Code;

            for (int i = 0; i < sendOption.IDs.Length; i++)
            {
                loopContent(sendOption, configOption,i);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sendOption">发送选项</param>
        /// <param name="ediConfig">发送配置</param>
        void loopContent(EDISendOption sendOption, EDIConfigItem ediConfig,int idIndex)
        {
            DataSet ds = new DataSet();
            Guid idtemp = Guid.Empty;//idtemp用于最后记录日志
            string strCondition = string.Empty;

            byte[] tempBytes = null;
            try
            {
                //生成文档编号
                sendOption.DocumentNO = GetDocumentNo(sendOption.CompanyID);
                //获取数据
                ds = getDataSetContent(sendOption, ediConfig,idIndex);
                sendOption.CurrentContent = string.Empty;
                sendOption.CurrentData = null;

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
                //加载对应船东的插件
                switch(ediConfig.PluginType)
                {
                    case EDIPluginType.AltovaMapForce:
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
                        sendOption.CurrentData = outms.ToArray();
                        ms.Close();
                        outms.Close();
                        inputStream.Close();
                        outStream.Close();
                        sendOption.CurrentContent = Encoding.UTF8.GetString(sendOption.CurrentData);

                        #endregion;
                        break;
                    case EDIPluginType.ICPMapForce:
                        #region IEDIPluginService
                        object builderObj = ReflectHelper.GetPlugInInstance(_EDIServerDir, ediConfig.Component);
                        IEDIPluginService EDIBuilder = builderObj as IEDIPluginService;
                        if (EDIBuilder == null)
                        {
                            throw new ApplicationException("加载ICP EDI API 插件失败...");
                        }
                        EDIPluginInput source = new EDIPluginInput { DataSetXML = txml };
                        EDIPluginOut targe = new EDIPluginOut();
                        EDIBuilder.BuildData(source, targe);
                        sendOption.CurrentContent = targe.EDIData;
                        sendOption.CurrentData = Encoding.UTF8.GetBytes(sendOption.CurrentContent);
                        #endregion
                        break;
                    case EDIPluginType.Unknown:
                        #region Unknown
                        string nbEDI = ds.Tables[0].Rows.Cast<DataRow>().Aggregate(string.Empty, (current, dt) => current + (dt[0] + Environment.NewLine));
                        tempBytes = Encoding.UTF8.GetBytes(nbEDI);
                        sendOption.CurrentData = tempBytes;
                        sendOption.CurrentContent = Encoding.UTF8.GetString(sendOption.CurrentData);
                        #endregion
                        break;
                }
                transmitData(sendOption, ediConfig);

                #region LOG

                #region 记录日志
                Guid logID = SaveLog(sendOption);
                #endregion

                #region 保存发送消息
                try
                {
                    MessageUserPropertiesObject userObject = new MessageUserPropertiesObject
                    {
                        {"OperationId", sendOption.IDs[0]},
                        {"OperationType", sendOption.OperationType}
                    };
                    switch (sendOption.EdiMode)
                    {
                        case EDIMode.Booking:
                            userObject.Add("FormType", FormType.Booking);
                            userObject.Add("FormId", sendOption.IDs[0]);
                            break;
                        case EDIMode.SI:
                        case EDIMode.ContainerLoad:
                        case EDIMode.PCLP:
                            userObject.Add("FormType", FormType.MBL);
                            userObject.Add("FormId", sendOption.IDs[0]);
                            break;
                        default:
                            userObject.Add("FormType", FormType.HBL);
                            userObject.Add("FormId", sendOption.FIDs[0]);
                            break;
                    }

                    MessageEDILogRelation logRelation = new MessageEDILogRelation
                    {
                        EDIConfigId = ediConfig.ID,
                        Flag = sendOption.Flag,
                        Type = sendOption.EdiMode
                    };
                    userObject.Add("EDILog", logRelation);


                    
                    Message.ServiceInterface.Message message = new Message.ServiceInterface.Message
                    {
                        CreateBy = sendOption.SendByID,
                        CreateDate = DateTime.Now,
                        UserProperties = userObject,
                        BodyFormat = BodyFormat.olFormatHTML,
                        SendFrom = sendOption.CurrentSender,
                        SendTo = sendOption.CurrentReceiver,
                        Subject = sendOption.Subject,
                        Type = MessageType.EDI,
                        Body = sendOption.CurrentContent,
                        State = MessageState.Transmitted,
                    };

                    _messageService.Save(message);
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("EDI已发送，记录发送日志发生异常:{0}", ex.Message));
                }
                #endregion

                #region 状态更新
                try
                {
                    switch (sendOption.EdiMode)
                    {
                        case EDIMode.Booking:
                            break;
                        case EDIMode.SI:
                            UpdateMBLD(sendOption.FIDs[0], sendOption.SendByID);
                            break;
                        case EDIMode.AMS:
                            UpdateAMSACIISFState(sendOption.FIDs[0], sendOption.SendByID, true, false, false);
                            break;
                        case EDIMode.ACI:
                            UpdateAMSACIISFState(sendOption.FIDs[0], sendOption.SendByID, false, true, false);
                            break;
                        case EDIMode.ISF:
                            UpdateAMSACIISFState(sendOption.FIDs[0], sendOption.SendByID, false, false, true);
                            break;
                        case EDIMode.AMSACI:
                            UpdateAMSACIISFState(sendOption.FIDs[0], sendOption.SendByID, true, true, false);
                            break;
                        case EDIMode.AMSISF:
                            UpdateAMSACIISFState(sendOption.FIDs[0], sendOption.SendByID, true, false, true);
                            break;
                        case EDIMode.AMSACIISF:
                            UpdateAMSACIISFState(sendOption.FIDs[0], sendOption.SendByID, true, true, true);
                            break;
                        case EDIMode.VGM:
                            UpdateAMSACIISFState(sendOption.FIDs[0], sendOption.SendByID, true, true, true);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("EDI已发送，更新状态发生异常:{0}", ex.Message));
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
                        OperationID = sendOption.IDs[0],
                        OperationType = OperationType.OceanExport,
                        Owner = string.Empty,
                        Priority = MemoPriority.Normal,
                        Subject = sendOption.Subject,
                        Type = MemoType.EDILog,
                        UpdateBy = sendOption.SendByID,
                        UpdateDate = DateTime.Now
                    };
                    switch (sendOption.EdiMode)
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
                            break;
                        case EDIMode.ACI:
                            eventObject.Code = "ACI";
                            eventObject.FormType = FormType.HBL;
                            eventObject.CategoryName = "ACI";
                            break;
                        case EDIMode.ISF:
                            eventObject.Code = "ISF";
                            eventObject.FormType = FormType.HBL;
                            eventObject.CategoryName = "ISF";
                            break;
                        case EDIMode.AMSACI:
                            eventObject.Code = "AMSACI";
                            eventObject.FormType = FormType.HBL;
                            eventObject.CategoryName = "AMSACI";
                            break;
                        case EDIMode.AMSISF:
                            eventObject.Code = "AMSISF";
                            eventObject.FormType = FormType.HBL;
                            eventObject.CategoryName = "AMSISF";
                            break;
                        case EDIMode.AMSACIISF:
                            eventObject.Code = "AMSACIISF";
                            eventObject.FormType = FormType.HBL;
                            eventObject.CategoryName = "AMSACIISF";
                            break;
                        case EDIMode.VGM:
                            eventObject.Code = "VGM";
                            eventObject.FormType = FormType.MBL;
                            eventObject.CategoryName = "VGM";
                            break;
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
                    eventObject.Description = sendOption.Subject;
                    eventObject.FormID = sendOption.IDs[0];
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
        /// 获得文档号
        /// </summary>
        /// <returns></returns>
        string GetDocumentNo(Guid companyID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand dbCommand = db.GetStoredProcCommand("pub.uspGenerateSN");

            db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
            db.AddInParameter(dbCommand, "@Key", DbType.String, "EDI");
            db.AddInParameter(dbCommand, "@GenerateDate", DbType.DateTime, DateTime.Now);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, false);
            db.AddInParameter(dbCommand, "@No", DbType.String, string.Empty);
            db.AddInParameter(dbCommand, "@IsResult  ", DbType.Boolean, true);


            DataSet temp = db.ExecuteDataSet(dbCommand);

            if (temp != null && temp.Tables.Count > 0 && temp.Tables[0].Rows.Count > 0)
            {
                return temp.Tables[0].Rows[0][0].ToString();
            }

            return string.Empty;

        }

        /// <summary>
        /// 获取EDI内容(数据集)
        /// </summary>
        /// <param name="sendOption">发送选项</param>
        /// <param name="ediConfig">发送配置</param>
        /// <param name="idIndex">ID索引</param>
        /// <returns>EDI内容</returns>
        DataSet getDataSetContent(EDISendOption sendOption, EDIConfigItem ediConfig,int idIndex)
        {
            string condition = buildCondition(sendOption, idIndex);
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(ediConfig.StoredProcedure);

            db.AddInParameter(dbCommand, "@Condition", DbType.String, condition);

            DataSet temp = db.ExecuteDataSet(dbCommand);

            DataSet ds = new DataSet();

            string path = _EDIServerDir + "\\DataSchemas\\" + ediConfig.DataFormat;
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

        /// <summary>
        /// 构建查询参数
        /// </summary>
        /// <param name="sendOption"></param>
        /// <param name="idIndex"></param>
        /// <returns></returns>
        string buildCondition(EDISendOption sendOption, int idIndex)
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlNode checknode = CreateXmlNode(xmldoc, "EDI", "");
            if (sendOption.IDs != null)
            {
                CreateXmlNode(xmldoc, "MainID", ""+ sendOption.IDs[idIndex], ref checknode);
                CreateXmlNode(xmldoc, "IDs", "" + sendOption.IDs.Join(","), ref checknode);
                //TODO:统一配置后移除
                CreateXmlNode(xmldoc, "ID", "" + sendOption.IDs[idIndex], ref checknode);
            }
            if (sendOption.NOs != null)
            {
                CreateXmlNode(xmldoc, "MainNO", ""+ sendOption.NOs[idIndex], ref checknode);
            }
            CreateXmlNode(xmldoc, "CompanyID", "" + sendOption.CompanyID, ref checknode);
            if (sendOption.FIDs != null)
            {
                CreateXmlNode(xmldoc, "FormIDs", sendOption.FIDs.Join(","), ref checknode);
                //TODO:统一配置后移除
                CreateXmlNode(xmldoc, "MIDs", sendOption.FIDs.Join(), ref checknode);
            }
            
            CreateXmlNode(xmldoc, "DOCUMENTNO", sendOption.DocumentNO, ref checknode);
            CreateXmlNode(xmldoc, "DOCUMENTFLAG", ((short)sendOption.Flag).ToString(CultureInfo.InvariantCulture), ref checknode);
            CreateXmlNode(xmldoc, "SendID", ""+ sendOption.SendByID, ref checknode);
            CreateXmlNode(xmldoc, "DOCUMENTFLAG", ((short)sendOption.Flag).ToString(CultureInfo.InvariantCulture), ref checknode);
            CreateXmlNode(xmldoc, "PILCommodity", ""+ sendOption.PILCommodity, ref checknode);
            CreateXmlNode(xmldoc, "IsEnglish", ""+ IsEnglish, ref checknode);
            CreateXmlNode(xmldoc, "AMSEntryType", ((short)sendOption.AMSEntryType).ToString(CultureInfo.InvariantCulture), ref checknode);//AMS(60=港口到港口61=内陆运输62=运输/出口63=立即再出口64=留在船上)
            CreateXmlNode(xmldoc, "ACIEntryType", ((short)sendOption.AMSEntryType).ToString(CultureInfo.InvariantCulture), ref checknode);//23=转运到美国 24=进口 26=过境货(ACI专用)
            if (sendOption.ShipperName != null)
                CreateXmlNode(xmldoc, "shipperName", "" + sendOption.ShipperName[idIndex], ref checknode);
            if (sendOption.ShipperFormat != null)
                CreateXmlNode(xmldoc, "shipperFormat", "" + sendOption.ShipperFormat[idIndex], ref checknode);
            if (sendOption.ConsigneeName != null)
                CreateXmlNode(xmldoc, "consigneeName", "" + sendOption.ConsigneeName[idIndex], ref checknode);
            if (sendOption.ConsigneeFormat != null)
                CreateXmlNode(xmldoc, "consigneeFormat", "" + sendOption.ConsigneeFormat[idIndex], ref checknode);
            if (sendOption.NotifyName != null)
                CreateXmlNode(xmldoc, "notifyName", "" + sendOption.NotifyName[idIndex], ref checknode);
            if (sendOption.NotifyFormat != null)
                CreateXmlNode(xmldoc, "notifyFormat", "" + sendOption.NotifyFormat[idIndex], ref checknode);
            if (sendOption.GoodinfoFormat != null)
                CreateXmlNode(xmldoc, "goodinfoFormat", "" + sendOption.GoodinfoFormat[idIndex], ref checknode);
            if (sendOption.MarkFormat != null)
                CreateXmlNode(xmldoc, "markFormat", "" + sendOption.MarkFormat[idIndex], ref checknode);
            CreateXmlNode(xmldoc, "IsEnglish", false.ToString(), ref checknode);
            return checknode.OuterXml;
        }

        /// <summary>
        /// 通过邮件/ftp传输数据
        /// </summary>
        /// <param name="sendOption"></param>
        /// <param name="ediConfig"></param>
        /// <param name="fromEMailAddress"></param>
        /// <param name="toAddress"></param>
        /// <param name="tempstring"></param>
        /// <param name="datas"></param>
        void transmitData(EDISendOption sendOption, EDIConfigItem ediConfig)
        {
            sendOption.CurrentSender = CurrentUserEmail;
            switch (ediConfig.UploadMode)
            {
                case EDIUploadMode.Disk:
                    #region 写入本地磁盘
                    string xpath = ediConfig.DiskPath;
                    if (!Directory.Exists(xpath))
                    {
                        Directory.CreateDirectory(xpath);
                    }
                    sendOption.CurrentReceiver = xpath;
                    switch (ediConfig.FileFormat.ToLower())
                    {
                        case "txt":
                        case "json":
                        case "pms":
                            #region txt/json/pms
                            FileStream fstxt = new FileStream(string.Format("{0}{1}.{2}", xpath, sendOption.DocumentNO, ediConfig.FileFormat), FileMode.Create);
                            StreamWriter swtxt = new StreamWriter(fstxt);

                            try
                            {
                                swtxt.Write(sendOption.CurrentContent);
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
                            #region XML
                            
                            FileStream fsxml;
                            StreamWriter swxml;
                            Random r = new Random();
                            if (File.Exists(xpath + sendOption.Subject + ".XML"))
                            {
                                fsxml = new FileStream(xpath + sendOption.Subject + "_" + r.Next(10000).ToString() + ".XML", FileMode.Create);
                                swxml = new StreamWriter(fsxml, Encoding.GetEncoding("GB2312"));
                            }
                            else
                            {
                                fsxml = new FileStream(xpath + sendOption.Subject + ".XML", FileMode.Create);
                                swxml = new StreamWriter(fsxml, Encoding.GetEncoding("GB2312"));
                            }
                            try
                            {
                                swxml.Write(sendOption.CurrentContent);
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
                    byte[] temBytes = Encoding.UTF8.GetBytes(sendOption.CurrentContent);

                    //获得发送者的邮箱信息
                    List<UserMailAccountList> tos = _userService.GetUserMailAccountList(new[] { sendOption.SendByID });
                    int port = 587;
                    if (tos != null && tos.Count > 0)
                    {
                        ediConfig.UserName = sendOption.CurrentSender = tos[0].Email;
                        ediConfig.Password = tos[0].MailOutgoingPassword;
                        port = tos[0].MailOutgoingPort;
                    }
                    sendOption.CurrentReceiver = ediConfig.ReceiveAddress;
                    if (!string.IsNullOrEmpty(ediConfig.ToAddress))
                    {
                        sendOption.CurrentReceiver = ediConfig.ToAddress;
                    }

                    if (string.IsNullOrEmpty(sendOption.CurrentSender))
                    {
                        throw new ApplicationException("请配置您的邮箱信息");
                    }

                    if (string.IsNullOrEmpty(sendOption.CurrentReceiver))
                    {
                        throw new ApplicationException("请在EDI配置中设置接收人的邮箱地址");
                    }
                    //如果配置的有邮件，则通过邮件发送EDI
                    SendEMail(ediConfig, port, ediConfig.UserName, sendOption.CurrentReceiver, sendOption.Subject, sendOption.Content, new[] { temBytes }, new[] { sendOption.DocumentNO }, new[] { ediConfig.FileFormat });
                    #endregion
                    break;
                case EDIUploadMode.FTP:
                    #region FTP
                    //文件格式.txt .xml
                    string fileName = sendOption.DocumentNO + "." + ediConfig.FileFormat;
                    //如果配置了FTP则通过FTP发送EDI
                    sendOption.CurrentReceiver = ediConfig.ServerAddress;
                    if (!string.IsNullOrEmpty(ediConfig.ReceiveAddress))
                    {
                        sendOption.CurrentReceiver += ediConfig.ReceiveAddress;
                    }
                    if (string.IsNullOrEmpty(ediConfig.ServerAddress))
                    {
                        throw new ApplicationException("请在EDI配置中设置FTP服务器地址");
                    }

                    FtpHelper.SendFile(ediConfig.ServerAddress, ediConfig.ReceiveAddress, ediConfig.UserName, ediConfig.Password, sendOption.Content, fileName, sendOption.CurrentData);
                    #endregion
                    break;
                case EDIUploadMode.WebAPI:
                    #region Web API
                    object builderObj = ReflectHelper.GetPlugInInstance(_EDIServerDir, ediConfig.Component);
                    IEDIPluginService EDISender = builderObj as IEDIPluginService;
                    if (EDISender == null)
                    {
                        throw new ApplicationException("加载ICP EDI API 插件失败...");
                    }
                    IDictionary<string, object> values = new Dictionary<string, object>();
                    values.Add("API_URL", ediConfig.ServerAddress);
                    values.Add("API_User_ID", ediConfig.UserName);
                    values.Add("API_Key", ediConfig.Password);
                    values.Add("API_Data", sendOption.CurrentContent);
                    values.Add("API_Method", ediConfig.ReceiveAddress);
                    EDISender.SendData(values); 
                    #endregion
                    break;
                default:
                    throw new ApplicationException("请配置正确的发送EDI方式");
                    break;
            }
        }

        /// <summary>
        /// 发送EDI
        /// </summary>
        /// <param name="sendEDIItem"></param>
        /// <returns></returns>
        [Obsolete("方法已过时，请使用 Send(EDISendOption sendOption, EDIConfigItem configOption) 代替")]
        public void SendEDI(EDISendOption sendEDIItem)
        {
            if (sendEDIItem.EdiMode == EDIMode.ContainerLoad || sendEDIItem.EdiMode == EDIMode.PCLP)
            {
                EDIConfigItem ediConfigNew = GetEDIConfig(sendEDIItem.CompanyID, new[] { sendEDIItem.CarrierID, sendEDIItem.AgentOfCarrierID }, sendEDIItem.EdiMode);
                if (ediConfigNew == null)
                {
                    throw new ApplicationException("没有找到该船东的EDI服务");
                }
                sendEDIItem.ServiceKey = ediConfigNew.Code;
                EDILoopContent(sendEDIItem, ediConfigNew);
                return;
            }
            //获取指定配置
            EDIConfigItem ediConfig = GetEDIConfig(sendEDIItem.CompanyID, sendEDIItem.ServiceKey, sendEDIItem.CarrierID);
            if (ediConfig == null)
            {
                throw new ApplicationException("没有找到该船东的EDI服务");
            }

            if (sendEDIItem.EdiMode == EDIMode.Booking)
            {
                if (sendEDIItem.ServiceKey.Contains("NBEDI"))
                {
                    EDILoopContent(sendEDIItem, ediConfig, sendEDIItem.IDs[0], sendEDIItem.FIDs[0], sendEDIItem.NOs[0], 0);
                }
                else
                {
                    for (int i = 0; i < sendEDIItem.IDs.Length; i++)
                    {
                        EDILoopContent(sendEDIItem, ediConfig, sendEDIItem.IDs[i], Guid.Empty, sendEDIItem.NOs[i], i);
                    }
                }
            }
            else if (sendEDIItem.EdiMode == EDIMode.SI || sendEDIItem.EdiMode == EDIMode.VGM)
            {
                if (sendEDIItem.ServiceKey.Contains("NBEDI") && sendEDIItem.ServiceKey != "NBEDISIANL")
                {
                    EDILoopContent(sendEDIItem, ediConfig, sendEDIItem.IDs[0], sendEDIItem.FIDs[0], sendEDIItem.NOs[0], 0);
                }
                else
                {
                    for (int i = 0; i < sendEDIItem.IDs.Length; i++)
                    {
                        EDILoopContent(sendEDIItem, ediConfig, sendEDIItem.IDs[i], sendEDIItem.FIDs[i], sendEDIItem.NOs[i], i);
                    }
                }
            }
            else if (sendEDIItem.EdiMode == EDIMode.AMS || sendEDIItem.EdiMode == EDIMode.ACI || sendEDIItem.EdiMode == EDIMode.ISF
                || sendEDIItem.EdiMode == EDIMode.AMSACI || sendEDIItem.EdiMode == EDIMode.AMSISF || sendEDIItem.EdiMode == EDIMode.AMSACIISF)
            {
                for (int i = 0; i < sendEDIItem.IDs.Length; i++)
                {
                    EDILoopContent(sendEDIItem, ediConfig, sendEDIItem.IDs[i], sendEDIItem.FIDs[i], sendEDIItem.NOs[i], i);
                }
            }
        }

        /// <summary>
        /// 中海订舱
        /// </summary>
        /// <param name="sendEDIItem"></param>
        /// <returns></returns>
        public DataSet SendCSCLBookingEDI(EDISendOption sendEDIItem)
        {
            return CSCLMethod(sendEDIItem);
        }
        /// <summary>
        /// 中海补料
        /// </summary>
        /// <param name="sendEDIItem"></param>
        /// <returns></returns>
        public DataSet SendCSCLSIEDI(EDISendOption sendEDIItem)
        {
            return CSCLMethod(sendEDIItem);
        }

        #region 获取业务号
        /// <summary>
        /// 获取业务号
        /// </summary>
        /// <param name="mblno">mblno</param>
        /// <returns></returns>
        public string GetOPNOByMBONO(string mblno)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("fcm.uspGetOPNoByMBLNo");

                db.AddInParameter(dbCommand, "@MBLNO", DbType.String, mblno);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return string.Empty;
                }

                return ds.Tables[0].Rows[0][0].ToString();
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 通过提单获取订舱ID
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MBLID"></param>
        public Guid GetOceanBookingIDByMBLID(Guid MBLID)
        {
            string sql = "SELECT om.OceanBookingID FROM fcm.OceanMBLs om WHERE om.id='" + MBLID.ToString() + "'";
            Database db = DatabaseFactory.CreateDatabase();
            return new Guid(db.ExecuteScalar(CommandType.Text, sql).ToString());
        }
        public Guid GetOceanBookingIDByHBLID(Guid HBLID)
        {
            string sql = "SELECT oh.OceanBookingID FROM fcm.OceanHBLs oh WHERE oh.id='" + HBLID.ToString() + "'";
            Database db = DatabaseFactory.CreateDatabase();
            return new Guid(db.ExecuteScalar(CommandType.Text, sql).ToString());
        }
        #endregion
    }
}
