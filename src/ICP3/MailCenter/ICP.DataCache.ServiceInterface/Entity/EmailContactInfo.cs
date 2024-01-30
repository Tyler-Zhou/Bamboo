using System;

namespace ICP.DataCache.ServiceInterface
{
    /// <summary>
    /// 联系人信息
    /// </summary>
    [Serializable]
    public class EmailContactInfo : OperationContactInfo
    {
        private string _EnName;
        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnName
        {
            get
            {
                return _EnName;
            }
            set
            {
                if (value == null)
                    _EnName = "";
                else
                    _EnName = value;
            }
        }

        private string _CnName;
        /// <summary>
        /// 中文
        /// </summary>
        public string CnName
        {

            get
            {
                return _CnName;
            }
            set
            {
                if (value == null)
                    _CnName = "";
                else
                    _CnName = value;
            }
        }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string Email1Address { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string BusinessTelephoneNumber { get; set; }

        /// <summary>
        /// 传真号码
        /// </summary>
        public string BusinessFaxNumber { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobileTelephoneNumber { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string BusinessAddressStreet { get; set; }

        /// <summary>
        /// 公司
        /// </summary>
        public string JobTitle { get; set; }

        /// <summary>
        /// 在职
        /// </summary>
        public bool IsInOffice { get; set; }
    }
}