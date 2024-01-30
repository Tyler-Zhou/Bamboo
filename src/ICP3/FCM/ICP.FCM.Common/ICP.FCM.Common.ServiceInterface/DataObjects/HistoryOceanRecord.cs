using System;

namespace ICP.FCM.Common.ServiceInterface.DataObjects
{
    /// <summary>
    /// 分文件签收记录
    /// </summary>
    public class HistoryOceanRecord
    {

        private Guid id;
        /// <summary>
        /// Record Id
        /// </summary>
        public Guid ID
        {
            get { return id; }
            set { id = value; }
        }

        private Guid oeBookingID;
        /// <summary>
        /// 海出业务ID
        /// </summary>
        public Guid OEBookingID
        {
            get { return oeBookingID; }
            set { oeBookingID = value; }
        }
        int type;

        /// <summary>
        /// 签收类型（1为签收前，2为签收后）
        /// </summary>
        public int Type
        {
            get { return type; }
            set { type = value; }
        }

         int  operationType;
        /// <summary>
        /// 操作类型（1为海出，2为海进）
        /// </summary>
        public int OperationType
        {
            get { return operationType; }
            set { operationType = value; }
        }


        string dispatchBy;

        /// <summary>
        /// 分发人
        /// </summary>
       public string DispatchBy
        {
            get { return dispatchBy; }
            set { dispatchBy = value; }
        }

       DateTime? dispatchOn;
       /// <summary>
       /// 分发时间
       /// </summary>
       public DateTime? DispatchOn
       {
           get { return dispatchOn; }
           set { dispatchOn = value; }
       }
       string acceptBy;
       /// <summary>
       /// 签收人
       /// </summary>
       public string AcceptBy
       {
           get { return acceptBy; }
           set { acceptBy = value; }
       }

       DateTime? acceptOn;
       /// <summary>
       /// 签收时间
       /// </summary>
       public DateTime? AcceptOn
       {
           get { return acceptOn; }
           set { acceptOn = value; }
       }

       string  assignTo;
       /// <summary>
       /// 分发给
       /// </summary>
       public string AssignTo
       {
           get { return assignTo; }
           set { assignTo = value; }
       }

       string remark;
        /// <summary>
       /// 签收说明
       /// </summary>
       public string Remark
       {
           get { return remark; }
           set { remark = value; }
       }

       Guid? refID;
       /// <summary>
       /// 签收关联ID
       /// </summary>
       public Guid? RefID
       {
           get { return refID; }
           set { refID = value; }
       }


    }
}
