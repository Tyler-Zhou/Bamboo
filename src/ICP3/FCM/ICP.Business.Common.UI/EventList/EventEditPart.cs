using ICP.Business.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Operation.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ICP.Business.Common.UI.EventList
{
    /// <summary>
    /// 事件编辑
    /// </summary>
    [SmartPart]
    public partial class EventEditPart : BaseEditPart, IDisposable
    {
        #region 服务注入

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public EventHandler<CommonEventArgs<EventObjects>> InsertEventObjectEvent;

        /// <summary>
        /// FCM公共服务
        /// </summary>
        IFCMCommonService FCMCommonService
        {
            get { return ServiceClient.GetService<IFCMCommonService>(); }
        }

        /// <summary>
        /// 事件集合
        /// </summary>
        List<EventCode> eventCodeList = new List<EventCode>();

        /// <summary>
        /// 公共服务接口
        /// </summary>
        IICPCommonOperationService IcpCommonOperationService
        {
            get
            {
                return ServiceClient.GetService<IICPCommonOperationService>();
            }
        }
        #endregion

        /// <summary>
        /// 编辑还是新增
        /// </summary>
        public bool isNew { get; set; }
        /// <summary>
        /// 列表的数据源
        /// </summary>
        public BindingSource bindingSource { get; set; }
        /// <summary>
        /// 业务
        /// </summary>
        public BusinessOperationContext BusinessEventObjects { get; set; }

        /// <summary>
        /// 列表选中列
        /// </summary>
        public EventObjects BusinessEventDataSource { get; set; }

        /// <summary>
        /// EventCodeID
        /// </summary>
        public string EventCodeID
        {
            get
            {
                if (cmbCodeSubject.EditValue == null)
                {
                    return null;
                }
                else
                {
                    return cmbCodeSubject.EditValue.ToString();
                }
            }
        }
        /// <summary>
        /// 添加事件的业务类型
        /// </summary>
        public OperationType OperationType { get; set; }

        #region 初始化

        public EventEditPart()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                if (LocalData.IsEnglish) SetCnText();
            }
            this.Disposed += delegate
            {
                this.InsertEventObjectEvent = null;
                this.eventCodeList = null;
                if (this.Workitem != null)
                {
                    this.Workitem.Items.Remove(this);
                    this.Workitem = null;
                }

            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }

        private void SetCnText()
        {
            this.Text = "New Event";
            labCode.Text = "Subject:";
            chkShowAgent.Text = "This event can be seem to the agent";
            chkWhowCustomer.Text = "This event can be seem to the customers";
            checkImportant.Text = "Is Important";
            lblDescription.Text = "Description:";
            btnSave.Text = "Save(&S)";
            btnCan.Text = "Close(&C)";
            labelOccurrenceTime.Text = "Occur At";
        }

        #endregion

        #region 加载

        private void InitControls()
        {
            OperationType operationType = BusinessEventObjects == null ? BusinessEventDataSource.OperationType : BusinessEventObjects.OperationType;
            eventCodeList = FCMCommonService.GetEventCodeList(operationType);
            if (isNew && bindingSource != null)
            {
                List<EventObjects> eventObjectses = bindingSource.DataSource as List<EventObjects>;
                //结果过滤为已经发生的事件
                eventObjectses = eventObjectses.Where(n => n.Logged == true).ToList();
                foreach (var events in eventObjectses)
                {
                    eventCodeList = (from even in eventCodeList
                                     where even.Code != events.Code
                                     select even).ToList();
                }
            }

            Dictionary<string, string> col = new Dictionary<string, string>();
            col.Add("Category", LocalData.IsEnglish ? "Stages" : "阶段");
            col.Add("Show", LocalData.IsEnglish ? "Event" : "事件");
            cmbCodeSubject.InitSource<EventCode>(eventCodeList, col, "Show", "Id");
            if (BusinessEventDataSource != null)
            {
                chkShowAgent.Checked = BusinessEventDataSource.IsShowAgent;
                chkWhowCustomer.Checked = BusinessEventDataSource.IsShowCustomer;
                checkImportant.Checked = BusinessEventDataSource.ManualImportant;
                txtDescription.Text = BusinessEventDataSource.Description;
                if (isNew == false)
                {
                    cmbCodeSubject.EditValue = BusinessEventDataSource.EventID;
                    cmbCodeSubject.EditText = BusinessEventDataSource.Subject;
                    if (string.IsNullOrEmpty(cmbCodeSubject.EditText))
                    {
                        cmbCodeSubject.EditText = BusinessEventDataSource.Code;
                    }
                }
                dteOccurrenceTime.EditValue = BusinessEventDataSource.OccurrenceTime;

                if (cmbCodeSubject.EditValue == null && !string.IsNullOrEmpty(BusinessEventDataSource.Code))
                {
                    Guid eventId = eventCodeList.Where(c => c.Code.Trim().ToUpper() == BusinessEventDataSource.Code.Trim().ToUpper()).Select(n => n.Id).FirstOrDefault();
                    if (eventId != Guid.Empty)
                    {
                        cmbCodeSubject.EditValue = eventId;
                    }
                }
            }
        }

        #endregion

        #region 保存

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                EventObjects eventobject = CreateEventObjects();
                try
                {
                    //针对SOD的操作
                    if (eventobject.Code != null && eventobject.Code.ToUpper() == "SOD" && isNew)
                    {
                        OperationType = OperationType.OceanExport;
                        eventobject.ModifyValue = "1";
                        BusinessEventDataSource = eventobject;
                    }
                    else
                    {
                        eventobject.OperationType = BusinessEventObjects == null ? BusinessEventDataSource.OperationType : BusinessEventObjects.OperationType;
                        eventobject.Type = ReturnMemoType();
                        eventobject.Priority = MemoPriority.Low;
                        eventobject.FormID = eventobject.OperationID;
                        eventobject.FormType = FormType.Booking;

                        ManyResultData mans = this.FCMCommonService.SaveMemoList(eventobject.OperationID,
                                                                      BusinessEventObjects == null ?
                                                                      BusinessEventDataSource.OperationType :
                                                                      BusinessEventObjects.OperationType,
                                                                      eventobject,
                                                                      new string[] { eventobject.Code },
                                                                      new string[] { eventobject.Id.ToString() },
                                                                      new string[] { eventobject.FormID.ToString() },
                                                                      new string[]
                                                                                 {
                                                                                     eventobject.FormType.GetHashCode()
                                                                                                .ToString()
                                                                                 },
                                                                      new string[]
                                                                                 {
                                                                                     eventobject.IsShowAgent.GetHashCode
                                                                                 ().ToString()
                                                                                 },
                                                                      new string[]
                                                                                 {
                                                                                     eventobject.IsShowCustomer
                                                                                                .GetHashCode()
                                                                                                .ToString()
                                                                                 },
                                                                      new string[] { eventobject.Subject.ToString() },
                                                                      new string[] { eventobject.Description },
                                                                      new MemoType[] { ReturnMemoType() },
                                                                      new MemoPriority[] { MemoPriority.Low },
                                                                      new string[] { eventobject.CategoryName },
                                                                      eventobject.Owner,
                                                                      new DateTime?[] { eventobject.UpdateDate },
                                                                      eventobject.UpdateBy, true, eventobject.OccurrenceTime.ToString(), eventobject.ManualImportant,
                                                                      new string[] { eventobject.MessageID == Guid.Empty ? string.Empty : eventobject.MessageID.ToString() });
                        if (mans != null)
                        {
                            eventobject.Id = mans.ChildResults[0].ID;
                            eventobject.UpdateDate = mans.ChildResults[0].UpdateDate;

                        }
                        //向事件列表插入数据 
                        if (InsertEventObjectEvent != null)
                        {
                            InsertEventObjectEvent(this, new CommonEventArgs<EventObjects>(eventobject));
                        }
                    }

                    #region  任务中心 手动添加代码 新增的情况
                    if (isNew)
                    {
                        if (IcpCommonOperationService.TaskCenterSaveEvent(OperationType, BusinessEventDataSource))
                        {
                            Workitem.State["Indicates"] = true;
                            BusinessEventDataSource = null;
                        }
                    }
                    #endregion



                    //提示保存成功
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save the data successfully." : "保存数据成功!");
                    this.FindForm().Close();
                }
                catch (Exception ex)
                {
                    //设置错误信息
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(),ex);
                }
            }
        }
        #endregion

        #region 根据判断的条件返回当前的事件的类别
        /// <summary>
        /// 根据判断的条件返回当前的事件的类别
        /// </summary>
        /// <returns>任务中心点击添加的事件为系统事件,手动直接添加的事件的为手动添加事件</returns>
        public MemoType ReturnMemoType()
        {
            MemoType memoType = MemoType.Manually;
            if (BusinessEventDataSource != null)
            {
                if (!string.IsNullOrEmpty(BusinessEventDataSource.ModifyValue))
                {
                    memoType = MemoType.Memo;
                }
            }
            return memoType;
        } 
        #endregion

        #region 获取EventObjects对象
        /// <summary>
        /// 获取EventObjects对象
        /// </summary>
        /// <returns></returns>
        private EventObjects CreateEventObjects()
        {
            EventObjects eventobject = new EventObjects();
            eventobject.OperationID = BusinessEventDataSource == null ? BusinessEventObjects.OperationID : BusinessEventDataSource.OperationID;
            eventobject.FormType = BusinessEventDataSource == null ? BusinessEventObjects.FormType : BusinessEventDataSource.FormType;
            eventobject.FormID = BusinessEventDataSource == null ? BusinessEventObjects.FormId : BusinessEventDataSource.FormID;
            eventobject.MessageID = BusinessEventDataSource == null ? Guid.Empty : BusinessEventDataSource.MessageID;
            if (cmbCodeSubject.EditValue != null)
            {
                EventCode code = eventCodeList.First(c => c.Id == new Guid(cmbCodeSubject.EditValue.ToString()));
                eventobject.CategoryName = code.Category;
                eventobject.Code = code.Code;
                eventobject.EventID = code.Id;
                eventobject.IsShowCSPEvent = code.IsShowCSPEvent;
                eventobject.IsShowCSPActivity = code.IsShowCSPActivity;
            }

            eventobject.Subject = cmbCodeSubject.EditText;
            eventobject.Description = txtDescription.Text;
            #region  事件代码为SOD的时候，生成的描述做特殊的处理
            if (eventobject.Code != null && eventobject.Code.ToUpper() == "SOD" && isNew)
            {
                eventobject.Description = eventobject.Description + "(Check the Manual)";
            }
            #endregion
            eventobject.UpdateBy = LocalData.UserInfo.LoginID;
            eventobject.Owner = LocalData.UserInfo.LoginName;
            eventobject.IsShowAgent = chkShowAgent.Checked;
            eventobject.IsShowCustomer = chkWhowCustomer.Checked;
            eventobject.ManualImportant = checkImportant.Checked;
            if (BusinessEventDataSource != null)
            {
                eventobject.UpdateDate = BusinessEventDataSource.UpdateDate;
                eventobject.Id = BusinessEventDataSource.Id == Guid.Empty ? Guid.NewGuid() : BusinessEventDataSource.Id;
                eventobject.CreateDate = BusinessEventDataSource.CreateDate;
            }
            else
            {
                eventobject.UpdateDate = DateTime.Now;
                eventobject.CreateDate = DateTime.Now;
            }
            if (dteOccurrenceTime.EditValue != null)
            {
                eventobject.OccurrenceTime = DateTime.Parse(dteOccurrenceTime.EditValue.ToString());
            }
            else
            {
                eventobject.OccurrenceTime = DateTime.Now;
            }
            #region 处理当前添加的事件状态和样式的处理
            eventobject.Logged = true;
            eventobject.EventIndex = 0;
            eventobject.Type = MemoType.Manually;
            if (string.IsNullOrEmpty(eventobject.CategoryName))
            {
                //手动添加的事件
                eventobject.CategoryName = "Other";
                eventobject.UIIndex = 1;
            }
            else
            {
                //系统自有事件
                eventobject.UIIndex = 2;
            }
            eventobject.CategoryName = eventobject.UIIndex + eventobject.CategoryName;
            if (bindingSource != null && !string.IsNullOrEmpty(eventobject.Code))
            {
                List<EventObjects> eventObjectses = bindingSource.DataSource as List<EventObjects>;
                //找到当前的必须完成的事件是否已经完成
                eventObjectses = eventObjectses.Where(n => n.Code != null).ToList();
                //移除已完成的事件 刷新列表
                if (eventObjectses != null)
                {
                    EventObjects RemoveeventObjects = eventObjectses.FirstOrDefault(n => n.Code.ToLower() == eventobject.Code.ToLower() && n.Logged == false);
                    if (RemoveeventObjects != null)
                    {
                        eventobject.Important = RemoveeventObjects.Important;
                        eventObjectses.Remove(RemoveeventObjects);
                        bindingSource.DataSource = eventObjectses;
                        bindingSource.ResetBindings(false);
                    }

                }
            }
            #endregion
            return eventobject;
        } 
        #endregion
    }
}


