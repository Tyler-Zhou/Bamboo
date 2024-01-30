using ICP.Framework.CommonLibrary.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ICP.FCM.Common.ServiceInterface.DataObjects
{
    /// <summary>
    /// 通知AMS提单
    /// </summary>
    public class NoticeAMSBL
    {
        /// <summary>
        /// 操作口岸
        /// </summary>
        public Guid CompanyID { get; set; }
        /// <summary>
        /// 业务号
        /// </summary>
        public string OperationNO { get; set; }
        /// <summary>
        /// 提单号
        /// </summary>
        public string BLNO { get; set; }

        /// <summary>
        /// ETA
        /// </summary>
        public DateTime ETA { get; set; }
        /// <summary>
        /// 间隔天数
        /// </summary>
        public int Interval { get; set; }
        /// <summary>
        /// 通知邮箱列表
        /// </summary>
        string _NoticeMails = string.Empty;
        /// <summary>
        /// 通知邮箱列表
        /// </summary>
        public string NoticeMails
        {
            get
            {
                return _NoticeMails;
            }
            set
            {
                if(value.IsNullOrEmpty())
                {
                    _NoticeMails = value;
                }
                else
                {
                    string[] addresses = value.Split(';');
                    List<string> listAddress = new List<string>();
                    foreach (string address in addresses)
                    {
                        if (address.IsNullOrEmpty() || listAddress.Contains(address))
                            continue;
                        listAddress.Add(address);
                    }
                    _NoticeMails= listAddress.Aggregate((a, b) => a + ";" + b);
                }
                
            }
        }
    }
}
