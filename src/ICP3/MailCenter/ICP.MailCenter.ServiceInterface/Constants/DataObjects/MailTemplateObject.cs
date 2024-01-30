using System;

namespace ICP.MailCenter.ServiceInterface.DataObjects
{
    /// <summary>
    /// 邮件模板参数实体
    /// </summary>
    [Serializable]
    public partial class MailTemplateObject
    {
        /// <summary>
        /// 业务号
        /// </summary>
        public String OperationNO
        {
            get;
            set;
        }
        /// <summary>
        /// 装货港
        /// </summary>
        public String POL
        {
            get;
            set;
        }

        /// <summary>
        ///  卸货港
        /// </summary>
        public String POD
        {
            get;
            set;
        }
        /// <summary>
        /// 箱型说明
        /// </summary>
        public String ContainerDescription
        {
            get;
            set;
        }
        /// <summary>
        /// 客户名称
        /// </summary>
        public String Customer
        {
            get;
            set;
        }
        /// <summary>
        /// 订舱号
        /// </summary>
        public String SONO
        {
            get;
            set;
        }
        /// <summary>
        /// 承运人
        /// </summary>
        public String Carrier
        {
            get;
            set;
        }
        /// <summary>
        /// 航线
        /// </summary>
        public String Vessel
        {
            get;
            set;
        }
        /// <summary>
        /// 失败说明
        /// </summary>
        public String FailureDescription
        {
            get;
            set;
        }

        /// <summary>
        /// 费用
        /// </summary>
        public object Fees
        {
            get;
            set;
        }
        /// <summary>
        /// 车牌号
        /// </summary>
        public String LicensePlateNO
        {
            get;
            set;
        }
        /// <summary>
        /// 封条号
        /// </summary>
        public String SealNO
        {
            get;
            set;
        }
    }
}
