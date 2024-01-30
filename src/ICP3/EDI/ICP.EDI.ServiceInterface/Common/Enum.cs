
using System;
using ICP.Framework.CommonLibrary.Attributes;

namespace ICP.EDI.ServiceInterface
{
    #region EDI插件类型
    /// <summary>
    /// EDI插件类型
    /// </summary>
    [Flags]
    [Serializable]
    public enum EDIPluginType
    {
        /// <summary>
        /// 不确定
        /// </summary>
        [MemberDescription("Unknown", "Unknown")]
        Unknown = 0,
        /// <summary>
        /// Altova MapForce
        /// </summary>
        [MemberDescription("Altova MapForce", "Altova MapForce")]
        AltovaMapForce = 1,
        /// <summary>
        /// ICP MapForce
        /// </summary>
        [MemberDescription("ICP MapForce", "ICP MapForce")]
        ICPMapForce = 2,
    }
    #endregion

    /// <summary>
    /// EDI状态
    /// </summary>
    [Flags]
    [Serializable]
    public enum EDIStatus
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
        Failure = 3,
        /// <summary>
        /// 草稿
        /// </summary>
        Draft = 4,
        /// <summary>
        /// 已发送
        /// </summary>
        Transmitted = 5,
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
