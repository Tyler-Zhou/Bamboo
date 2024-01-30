using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Common;


namespace ICP.FCM.Common.ServiceInterface.DataObjects
{
    /// <summary>
    /// 联系人列表实体类
    /// </summary>
    [Serializable]
    public class ContactObjects : BaseDataObject
    {
        private List<StaffObjects> staffList;

        /// <summary>
        /// 员工列表集合
        /// </summary>
        public List<StaffObjects> StaffList
        {
            get { return staffList; }
            set
            {
                staffList = value;           
            }
        }

        private List<CustomerCarrierObjects> customerCarrier;
        /// <summary>
        /// 顾客及承运人列表集合
        /// </summary>
        public List<CustomerCarrierObjects> CustomerCarrier
        {
            get { return customerCarrier; }
            set
            {
                customerCarrier = value;
            }
        }
    }

    /// <summary>
    /// 放单和收款联系人列表实体类
    /// </summary>
    [Serializable]
    public class ReleaseAndArContact : BaseDataObject
    {
        private Guid operationid;

        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OperationID
        {
            get { return operationid; }
            set
            {
                operationid = value;
            }
        }

        private Guid? customerid;

        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid? CustomerID
        {
            get { return customerid; }
            set
            {
                customerid = value;
            }
        }

        private string mail;
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Mail
        {
            get { return mail; }
            set
            {
                mail = value;
            }
        }

        private bool isar;
        /// <summary>
        /// 是否收款联系人
        /// </summary>
        public bool IsAR
        {
            get { return isar; }
            set
            {
                isar = value;
            }
        }

        private bool isrelease;
        /// <summary>
        /// 是否放单联系人
        /// </summary>
        public bool IsRelease
        {
            get { return isrelease; }
            set
            {
                isrelease = value;
            }
        }

        private bool islast;
        /// <summary>
        /// 是否上次联系人
        /// </summary>
        public bool IsLast
        {
            get { return islast; }
            set
            {
                islast = value;
            }
        }
    }
}
