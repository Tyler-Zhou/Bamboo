/**
 *  创建时间:2014-07-17
 *  创建人:Joabwang    
 *  描  述:面板按钮使用方法集合
 **/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using ICP.Business.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.DataCache.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Server;
using ICP.Message.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;
using EnumActionType = ICP.Common.ServiceInterface.DataObjects.ActionType;

namespace ICP.MailCenterFramework.UI
{
    /// <summary>
    /// 面板使用方法集合
    /// </summary>
    public class Assist
    {
        private Thread _Thread = null;
        private string _Text = string.Empty;

        private string _TemplateCode = string.Empty;
        private Message.ServiceInterface.Message _Message = null;
        private Guid _OperationId = Guid.Empty;
        private string _OperationNo = string.Empty;
        private DateTime? _UpdateDateTime = null;
        private OperationType? _OperationTypes = null;
        private AssociateType? _AssociateTypes = null;
        private List<BusinessOperationContext> _BussOperationContexts;
        private ContactType _ContactType;

        /// <summary>
        /// 缓存数据库服务接口
        /// </summary>
        public IDataCacheOperationService DataCacheOperationService
        {
            get
            {
                IDataCacheOperationService temp = null;
                try
                {
                    temp = ServiceClient.GetService<IDataCacheOperationService>();
                }
                catch (Exception ex)
                {
                    ToolUtility.WriteLog("Assist DataCacheOperationService", ex);
                    temp = null;
                }
                return temp;
            }
        }

        /// <summary>
        /// FCM 公共服务接口
        /// </summary>
        public IICPCommonOperationService ICPCommonOperationService
        {
            get
            {
                IICPCommonOperationService temp = null;
                try
                {
                    temp = ServiceClient.GetService<IICPCommonOperationService>();
                }
                catch (Exception ex)
                {
                    ToolUtility.WriteLog("Assist ICPCommonOperationService", ex);
                    temp = null;
                }
                return temp;
            }
        }

        /// <summary>
        /// 新增海进订舱单
        /// </summary>
        /// <param name="templateCode">数据库对应的视图CODE</param>
        /// <param name="message">message对象</param>
        public void AddSoForOe(string templateCode, Message.ServiceInterface.Message message)
        {
            _TemplateCode = templateCode;
            _Message = message;
            _Thread = new Thread(new ThreadStart(SoForOe));
            _Thread.Name = "SoForOe";
            _Thread.Start();
            _Text = LocalData.IsEnglish ? "Cancel So Add" : "取消新增订舱单";
            MessageBoxShow(_Thread, _Text);
           
        }
        /// <summary>
        /// 海出订舱打开编辑界面
        /// </summary>
        /// <param name="operationId">业务的ID</param>
        /// <param name="operationNo">业务号</param>
        /// <param name="dateTime">修改时间</param>
        /// <param name="templateCode">事务的CODE</param>
        /// <param name="message">邮件对象</param>
        public void EditOeBooking(Guid operationId, string operationNo, DateTime? dateTime, string templateCode,
            Message.ServiceInterface.Message message)
        {
            _OperationId = operationId;
            _OperationNo = operationNo;
            _UpdateDateTime = dateTime;
            _TemplateCode = templateCode;
            _Message = message;
            _Thread = new Thread(new ThreadStart(EditOeBookingMethods));
            _Thread.Name = "EditOeBookingMethods";
            _Thread.Start();
            _Text = LocalData.IsEnglish ? "Cancel So Edit" : "取消编辑订舱单";
            MessageBoxShow(_Thread, _Text);

        }

        /// <summary>
        /// 空出订舱打开编辑界面
        /// </summary>
        /// <param name="operationId">业务的ID</param>
        /// <param name="operationNo">业务号</param>
        /// <param name="dateTime">修改时间</param>
        /// <param name="templateCode">事务的CODE</param>
        /// <param name="message">邮件对象</param>
        public void AirExportEditData(Guid operationId, string operationNo, DateTime? dateTime, string templateCode,
            Message.ServiceInterface.Message message)
        {
            _OperationId = operationId;
            _OperationNo = operationNo;
            _UpdateDateTime = dateTime;
            _TemplateCode = templateCode;
            _Message = message;
            _Thread = new Thread(new ThreadStart(AirExportEditDataMethods));
            _Thread.Name = "AirExportEditDataMethods";
            _Thread.Start();
            _Text = LocalData.IsEnglish ? "Cancel So Edit" : "取消编辑订舱单";
            MessageBoxShow(_Thread, _Text);
        }
      

        /// <summary>
        /// 空进订舱打开编辑界面
        /// </summary>
        /// <param name="operationId">业务的ID</param>
        /// <param name="operationNo">业务号</param>
        /// <param name="dateTime">修改时间</param>
        /// <param name="templateCode">事务的CODE</param>
        /// <param name="message">邮件对象</param>
        public void AirImportEditBooking(Guid operationId, string operationNo, DateTime? dateTime, string templateCode,
            Message.ServiceInterface.Message message)
        {
            _OperationId = operationId;
            _OperationNo = operationNo;
            _UpdateDateTime = dateTime;
            _TemplateCode = templateCode;
            _Message = message;
            _Thread = new Thread(new ThreadStart(AirImportEditBookingMethods));
            _Thread.Name = "AirImportEditBookingMethods";
            _Thread.Start();
            _Text = LocalData.IsEnglish ? "Cancel So Edit" : "取消编辑订舱单";
            MessageBoxShow(_Thread, _Text);
        }

        /// <summary>
        /// 海进订舱打开编辑界面
        /// </summary>
        /// <param name="operationId">业务的ID</param>
        /// <param name="operationNo">业务号</param>
        /// <param name="dateTime">修改时间</param>
        /// <param name="templateCode">事务的CODE</param>
        /// <param name="message">邮件对象</param>
        public void EditOiBusiness(Guid operationId, string operationNo, DateTime? dateTime, string templateCode,
            Message.ServiceInterface.Message message)
        {
            _OperationId = operationId;
            _OperationNo = operationNo;
            _UpdateDateTime = dateTime;
            _TemplateCode = templateCode;
            _Message = message;
            _Thread = new Thread(new ThreadStart(EditOiBusinessMethods));
            _Thread.Name = "EditOiBusinessMethods";
            _Thread.Start();
            _Text = LocalData.IsEnglish ? "Cancel So Edit" : "取消编辑订舱单";
            MessageBoxShow(_Thread, _Text);
        }


        #region   方法
        /// <summary>
        /// 新增海进订舱单
        /// </summary>
        public void SoForOe()
        {
            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Parameter.Performflg = false;
                    Parameter.Calledtime = DateTime.Now;
                    Dictionary<string, object> dictionary = CreateBusinessParameter(EnumActionType.Create, true, Guid.Empty,
                        string.Empty, OperationType.OceanExport, null, _TemplateCode, _Message);
                    object value;
                    dictionary.TryGetValue("businessOperationParameter", out value);
                    if (value.GetType() == typeof(BusinessOperationParameter))
                    {
                        BusinessOperationParameter businessOperation = value as BusinessOperationParameter;
                        businessOperation.ContactStage = ContactStage.SO;
                    }
                    ICPCommonOperationService.MailCenterRefresh(false);
                    ICPCommonOperationService.AddBooking(dictionary);
                    Parameter.Performflg = true;
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("Assist---SoForOe" , ex);
            }
        }

        /// <summary>
        /// 海出订舱打开编辑界面
        /// </summary>
        public void EditOeBookingMethods()
        {
            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Parameter.Performflg = false;
                    Parameter.Calledtime = DateTime.Now;
                    if (_OperationId != Guid.Empty && _OperationNo != string.Empty)
                    {
                        var editPartShowCriteria = new EditPartShowCriteria
                        {
                            BillNo = _OperationId,
                            OperationNo = _OperationNo
                        };
                        Dictionary<string, object> dictionary = CreateBusinessParameter(EnumActionType.Edit, false, _OperationId,
                            _OperationNo, OperationType.OceanExport, _UpdateDateTime, _TemplateCode, _Message);
                        ICPCommonOperationService.EditBooking(editPartShowCriteria, dictionary);
                        Parameter.Performflg = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("Assist---EditOeBookingMethods" , ex);
            }
        }

        /// <summary>
        /// 空出订舱打开编辑界面
        /// </summary>
        public void AirExportEditDataMethods()
        {
            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Parameter.Performflg = false;
                    Parameter.Calledtime = DateTime.Now;
                    if (_OperationId != Guid.Empty && _OperationNo != string.Empty)
                    {
                        var editPartShowCriteria = new EditPartShowCriteria
                        {
                            BillNo = _OperationId,
                            OperationNo = _OperationNo
                        };
                        Dictionary<string, object> dictionary = CreateBusinessParameter(EnumActionType.Edit, false, _OperationId,
                            _OperationNo, OperationType.AirExport, _UpdateDateTime, _TemplateCode, _Message);
                        ICPCommonOperationService.AirExportEditData(editPartShowCriteria, dictionary);
                        Parameter.Performflg = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("Assist---AirExportEditDataMethods" , ex);
            }
        }

        /// <summary>
        /// 空进订舱打开编辑界面
        /// </summary>
        public void AirImportEditBookingMethods()
        {
            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Parameter.Performflg = false;
                    Parameter.Calledtime = DateTime.Now;
                    if (_OperationId != Guid.Empty && _OperationNo != string.Empty)
                    {
                        var editPartShowCriteria = new EditPartShowCriteria
                        {
                            BillNo = _OperationId,
                            OperationNo = _OperationNo
                        };
                        Dictionary<string, object> dictionary = CreateBusinessParameter(EnumActionType.Edit,
                            false, _OperationId, _OperationNo, OperationType.AirExport, _UpdateDateTime, _TemplateCode, _Message);
                        ICPCommonOperationService.AirImportEditBooking(editPartShowCriteria, dictionary);
                        Parameter.Performflg = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("Assist---AirImportEditBookingMethods" , ex);
            }
        }

        /// <summary>
        /// 海进订舱打开编辑界面
        /// </summary>
        public void EditOiBusinessMethods()
        {
            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Parameter.Performflg = false;
                    Parameter.Calledtime = DateTime.Now;
                    if (_OperationId != Guid.Empty && _OperationNo != string.Empty)
                    {
                        var editPartShowCriteria = new EditPartShowCriteria
                        {
                            BillNo = _OperationId,
                            OperationNo = _OperationNo
                        };
                        Dictionary<string, object> dictionary = CreateBusinessParameter(EnumActionType.Edit,
                            false, _OperationId, _OperationNo, OperationType.OceanImport, _UpdateDateTime, _TemplateCode, _Message);
                        ICPCommonOperationService.EditOIBusiness(editPartShowCriteria, dictionary);
                        Parameter.Performflg = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("Assist---EditOiBusinessMethods" , ex);
            }

        }

        /// <summary>
        /// 关联的方法
        /// </summary>
        public void ClikckAssociate(AssociateType associateType, ContactType contactType, List<BusinessOperationContext> bussOperationContexts,
            Message.ServiceInterface.Message message, Guid operationId,
            OperationType operation)
        {
            try
            {
                if (DataCacheOperationService != null)
                {
                    _BussOperationContexts = bussOperationContexts;
                    _AssociateTypes = associateType;
                    _Message = message;
                    _OperationId = operationId;
                    _OperationTypes = operation;
                    _ContactType = contactType;
                    AssociateMethod();
                    //_Thread = new Thread(new ThreadStart(AssociateMethod));
                    //_Thread.Name = "AssociateMethod";
                    //_Thread.Start();
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("ClikckAssociate" , ex);
            }
        }

        /// <summary>
        /// 关联的方法
        /// </summary>
        public void AssociateMethod()
        {
            try
            {
                if (_BussOperationContexts.Any() == false)
                {
                    MessageBox.Show(LocalData.IsEnglish ? "selected  at least one shipment!" : "至少选择一票业务!");
                    return;
                }
                if (string.IsNullOrEmpty(_Message.MessageId))
                {
                    MessageBox.Show(LocalData.IsEnglish ? "MessageID is null,Can't associate to the shipment!" : "MessageID为空，不能关联到业务!");
                    return;
                }
                OperationSaveController operationSaveController = new OperationSaveController();

                //普通关联需判断是否包含外部联系人
                if (_AssociateTypes == AssociateType.Normal && operationSaveController.IsExternalMail(_Message))
                {
                    MessageBox.Show(LocalData.IsEnglish ? "It contains external mail addresses, please associate it as Customer/Carrier Mail."
                        : "这封邮件包含了外部邮箱，请关联为客户邮件或承运人邮件。");
                    return;
                }
                

                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Stopwatch totalWatch = StopwatchHelper.StartStopwatch();

                    MessageRelationParameter messageRelationParameter = new MessageRelationParameter();
                    if (!ICPCommonOperationService.SaveOperationMessage((AssociateType)_AssociateTypes, _Message,
                        _OperationId, (OperationType)_OperationTypes))
                    {
                        DataCacheOperationService.SaveContactList(_ContactType, _BussOperationContexts, _Message);
                        //保存外部联系人到内存
                        operationSaveController.SaveContactToMailStore((AssociateType) _AssociateTypes, _Message);
                    }
                    operationSaveController.SaveAsMail(_Message.EntryID, _Message.BackupMailState, HelpMailStore.CurrentMailItem);
                    messageRelationParameter = GetMessageRelationParameter(_Message, (AssociateType)_AssociateTypes);
                    operationSaveController.InnerSaveOperationMessageRelation(messageRelationParameter,
                        _BussOperationContexts);

                    totalWatch.Stop();
                    MethodBase methodother = MethodBase.GetCurrentMethod();
                    DataCacheOperationService.WriteStopwatchLog(methodother.DeclaringType.FullName,"PLUGIN", "手动关联邮件",totalWatch.ElapsedMilliseconds.ToString());
                    totalWatch = null;
                }
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("Assist---ClikckAssociate", ex);
            }
        }

        /// <summary>
        ///  创造业务参数
        /// </summary>
        /// <param name="actionType">动作类型</param>
        /// <param name="isNewOrder">是否为新增</param>
        /// <param name="operationId">业务ID</param>
        /// <param name="operationNo">业务号</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="upDateTime">修改时间</param>
        /// <param name="templateCode">数据库对应的视图CODE</param>
        /// <param name="message">message对象</param>
        /// <returns></returns>
        public Dictionary<string, object> CreateBusinessParameter(
            EnumActionType actionType, bool isNewOrder,
            Guid operationId, string operationNo, OperationType operationType, DateTime? upDateTime,
            string templateCode, Message.ServiceInterface.Message message)
        {
            BusinessOperationParameter businessOperation = new BusinessOperationParameter();
            businessOperation.ContactStage = ContactStage.Unknown;
            businessOperation.TemplateCode = templateCode;
            businessOperation.Message = message;

            if (actionType == EnumActionType.Edit)
            {
                businessOperation.Context = CreateBusinessOperationContext(operationId, operationNo, operationType,
                    upDateTime);
            }
            else
            {
                if (isNewOrder)
                {
                    businessOperation.Context = new BusinessOperationContext();
                }
                else
                {
                    businessOperation.Context = CreateBusinessOperationContext(operationId, operationNo, operationType,
                        upDateTime);
                }
            }
            businessOperation.ActionType = actionType;
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("businessOperationParameter", businessOperation);
            return dic;
        }

        /// <summary>
        /// 构造上下文实体类
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="operationNo">业务号</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="upDateTime">修改时间</param>
        /// <returns></returns>
        public BusinessOperationContext CreateBusinessOperationContext(Guid operationId, string operationNo,
            OperationType operationType, DateTime? upDateTime)
        {
            return new BusinessOperationContext
            {
                OperationID = operationId,
                OperationNO = operationNo,
                OperationType = operationType,
                UpdateDate = upDateTime
            };
        }


        /// <summary>
        /// 对话框弹出
        /// </summary>
        /// <param name="thread">当前使用的线程</param>
        /// <param name="text">文本信息</param>
        public void MessageBoxShow(Thread thread, string text)
        {
            //线程执行完,隐藏对话框
            if (thread.IsAlive)
            {
                var messageBoxFrom = new MessageBoxFrom
                {
                    Thread = thread,
                    Text = text
                };
                messageBoxFrom.Hide();
            }
        }


        /// <summary>
        /// 构造关联参数
        /// </summary>
        /// <param name="messageinfo">邮件实体</param>
        /// <param name="associateType">关联类型</param>
        /// <returns></returns>
        public MessageRelationParameter GetMessageRelationParameter(Message.ServiceInterface.Message messageinfo, AssociateType associateType)
        {
            if (messageinfo != null)
            {
                MessageRelationParameter messageRelationParameter = new MessageRelationParameter
                {
                    AssociateType = associateType,
                    MailContactInfos = null,
                    RelationType = MessageRelationType.Hand,
                    UpdateDataType = UpdateDataType.MainForMessageID,
                    MessageID = messageinfo.MessageId,
                    MessageInfo = messageinfo,
                    BusinessContactType = ((associateType == AssociateType.AsCarrier)?2:1)
                };
                return messageRelationParameter;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 高级查询返回查询结果集合
        /// </summary>
        /// <param name="operationType">业务类型</param>
        /// <returns></returns>
        public BusinessQueryCriteria Advancedquery(OperationType operationType)
        {
            BusinessQueryCriteria businessQueryCriteria = new BusinessQueryCriteria();
            try
            {
                businessQueryCriteria.AdvanceQueryString = ICPCommonOperationService.Advancedquery(operationType);
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("Assist---Advancedquery" , ex);
            }
            return businessQueryCriteria;
        }
        /// <summary>
        /// 返回文本框搜索条件
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public BusinessQueryCriteria AdvanceQueryString(string query)
        {
            BusinessQueryCriteria businessQueryCriteria = new BusinessQueryCriteria();
            try
            {
                string advanceQueryString = "1=1";
                string[] queryspit = query.Split('/');
                //执行多条
                for (int i = 0; i < queryspit.Count(); i++)
                {
                    //单号的查询
                    if (advanceQueryString == "1=1")
                    {
                        advanceQueryString += " and ($@NO@ like  '%" + queryspit[i] + "%'";
                        advanceQueryString += " or $@RefNO@ like '%" + queryspit[i] + "%'";
                    }
                    else
                    {
                        advanceQueryString += " or $@NO@ like  '%" + queryspit[i] + "%'";
                        advanceQueryString += " or $@RefNO@ like '%" + queryspit[i] + "%'";
                    }
                }
                advanceQueryString += ")";
                string companyId = string.Empty;
                var copany = LocalData.UserInfo.UserOrganizationList.Where(o => o.Type == LocalOrganizationType.Company).ToList();
                if (copany.Any() && copany.Count > 1)
                {
                    foreach (var c in copany)
                    {
                        if (string.IsNullOrEmpty(companyId))
                        {
                            companyId = "'" + c.ID + "'";
                        }
                        else
                        {
                            companyId = companyId + "," + "'" + c.ID + "'";
                        }

                    }
                }
                else
                {
                    companyId = copany[0].ID.ToString();
                }
                if (companyId.Contains(",") == false)
                {
                    advanceQueryString += " and $@CompanyID@ = '" + companyId + "'";
                }
                else
                {
                    advanceQueryString += " and $@CompanyID@ in (" + companyId + ")";
                }
                businessQueryCriteria.AdvanceQueryString = advanceQueryString;
            }
            catch (Exception ex)
            {
                ToolUtility.WriteLog("Assist---AdvanceQueryString" , ex);
            }
            return businessQueryCriteria;
        }
        #endregion
    }
}
