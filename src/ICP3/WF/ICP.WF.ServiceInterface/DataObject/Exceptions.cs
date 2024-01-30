using System;
using System.Runtime.Serialization;

namespace ICP.WF.ServiceInterface.DataObject
{
    /// <summary>
    /// 下一步审批人没找到的情况下..抛出的异常
    /// </summary>
    [Serializable]
    public class WorkflowExecutorNullException : ApplicationException, ISerializable
    {
        string name;

        public WorkflowExecutorNullException()
        {
        }


        public WorkflowExecutorNullException(string name, Guid workitemId, Guid callerId)
        {
            this.name = name;
            this._callerId = callerId;
            this._workItemId = workitemId;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        Guid _workItemId = Guid.Empty;
        /// <summary>
        /// 任务Id
        /// </summary>
        public Guid WorkItemId
        {
            get { return _workItemId; }
        }

        Guid _callerId = Guid.Empty;
        /// <summary>
        /// 执行人
        /// </summary>
        public Guid CallerId
        {
            get { return _callerId; }
        }

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            info.AddValue("Name", name);
            info.AddValue("CallerId", _callerId);
            info.AddValue("WorkItemId", _workItemId);
            base.GetObjectData(info, context);
        }

        public WorkflowExecutorNullException(System.Runtime.Serialization.SerializationInfo si, System.Runtime.Serialization.StreamingContext context)
        {
            this.name = si.GetString("Name");
            this._callerId = new Guid(si.GetString("CallerId"));
            this._workItemId = new Guid(si.GetString("WorkItemId"));
        }
    }


    /// <summary>
    /// 异常错误参数类型
    /// </summary>
    [Serializable]
    public class ExecutorWrongEventArgs : EventArgs
    {
        public ExecutorWrongEventArgs(Guid callerId, Guid workitemId, string name)
        {
            this.callerId = callerId;
            this.workitemId = workitemId;
            this.name = name;
        }

        Guid callerId;
        /// <summary>
        /// 调用人
        /// </summary>
        public Guid CallerId
        {
            get { return callerId; }
        }

        Guid workitemId;
        /// <summary>
        /// 任务id
        /// </summary>
        public Guid WorkitemId
        {
            get { return workitemId; }
        }

        string name;
        /// <summary>
        /// 任务名
        /// </summary>
        public string Name
        {
            get { return name; }
        }
    }

    /// <summary>
    /// 找不到下一步执行的异常信息
    /// </summary>
    [DataContract]
    public class WorkflowExecutorNullExceptionInfo
    {
        private string wName = string.Empty;
        /// <summary>
        /// 任务名称
        /// </summary>
        [DataMember]
        public string WName
        {
            get { return wName; }
            set { wName = value; }
        }

        Guid callerId;
        /// <summary>
        /// 调用人
        /// </summary>
        [DataMember]
        public Guid CallerId
        {
            get { return callerId; }
            set { callerId = value; }
        }

        Guid workitemId;
        /// <summary>
        /// 任务id
        /// </summary>
        [DataMember]
        public Guid WorkitemId
        {
            get { return workitemId; }
            set { workitemId = value; }
        }

    }
    /// <summary>
    /// 完成时执行保存SQL时的异常信息
    /// </summary>
    [DataContract]
    public class WorkflowSqlExceptionInfo
    {
         [DataMember]
        public string Message;
    }



}



