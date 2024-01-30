using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.EDIManager.ServiceInterface.Entity
{

    /// <summary>
    /// 消息状态
    /// </summary>
    public enum MessageState
    {
        /// <summary>
        /// 发送中
        /// </summary>
        Sending = 1,
        /// <summary>
        /// 发送成功
        /// </summary>
        Success = 2,
        /// <summary>
        /// 发送失败
        /// </summary>
        Failure =3,
        /// <summary>
        /// 草稿
        /// </summary>
        Draft =4,
        /// <summary>
        /// 已发送
        /// </summary>
        Transmitted =5,
        ///<summary>
        /// EDI失败
        /// </summary>
        EdiFailure = 6,
        /// <summary>
        /// EDI成功 
        /// </summary>
        EdiSuccess = 7,
        /// <summary>
        /// 船东认可 
        /// </summary>
        CarrierAccepted = 8,
    }
}
