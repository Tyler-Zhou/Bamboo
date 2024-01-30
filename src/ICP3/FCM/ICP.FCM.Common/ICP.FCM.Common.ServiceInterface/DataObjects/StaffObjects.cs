using System;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.Common.ServiceInterface.DataObjects
{
    /// <summary>
    /// 员工列表数据类
    /// </summary>
    [Serializable]
    public class StaffObjects
    {
        /// <summary>
        /// 
        /// </summary>
        public StaffObjects()
        {
            IsDirty = false;
            CanUpdate = true;
        }
        /// <summary>
        /// 是否是新增的数据
        /// </summary>
        public bool IsDirty { get; set; }
        /// <summary>
        /// 可以更改数据源
        /// </summary>
        public bool CanUpdate { get; set; }

        private Guid id;
        /// <summary>
        /// 
        /// </summary>
        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }


        private Guid operationID;
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OperationID
        {
            get { return operationID; }
            set { operationID = value; }
        }


        private Guid? formID;
        /// <summary>
        /// FormID
        /// </summary>
        public Guid? FormID
        {
            get { return formID; }
            set { formID = value; }
        }

        private FormType? formType;
        /// <summary>
        /// 表单类型
        /// </summary>
        public FormType? FormType
        {
            get { return formType; }
            set { formType = value; }
        }

        private Guid? ownerid;
        /// <summary>
        /// 托运单ID，即表单id
        /// </summary>
        public Guid? Ownerid
        {
            get { return ownerid; }
            set { ownerid = value; }
        }

        private OperationType ownersource;
        /// <summary>
        /// 备注所属业务
        /// </summary>
        public OperationType Ownersource
        {
            get { return ownersource; }
            set { ownersource = value; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate { get; set; }
        /// <summary>
        /// 业务更新时间
        /// </summary>
        public DateTime? OperationUpdateDate { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public Guid? UpdateBy { get; set; }
        /// <summary>
        /// 新增时间
        /// </summary>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 新增人
        /// </summary>
        public Guid? CreateBy { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserID { get; set; }

        private string role;
        /// <summary>
        /// 角色
        /// </summary>
        public string Role
        {
            get { return role; }
            set { role = value; }
        }
        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid RoleID { get; set; }

        private string name;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string mail;
        /// <summary>
        /// 邮件
        /// </summary>
        public string Mail
        {
            get { return mail; }
            set { mail = value; }
        }
        private string tel;
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel
        {
            get { return tel; }
            set { tel = value; }
        }
    }
}
