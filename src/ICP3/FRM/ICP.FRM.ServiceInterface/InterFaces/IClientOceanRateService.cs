using ICP.Framework.CommonLibrary.Common;
using ICP.FRM.ServiceInterface.DataObjects;
using System;

namespace ICP.FRM.ServiceInterface
{
    /// <summary>
    /// 询价客户端服务接口
    /// </summary>

    public interface IClientInquireRateService
    {
        /// <summary>
        /// 弹出新的窗体海出询价界面
        /// </summary>
        void InquireOceanRate();

        void InquireOceanRate(InquierOceanRate inquieroceanrate, PartDelegate.EditPartSaved editPartSaved);

        /// <summary>
        /// 打开海运询价列表界面
        /// </summary>
        void OpenOceanInquireRateListPart();

        /// <summary>
        /// 根据询价ID查询出当前询问人，并新建一份空白的邮件
        /// </summary>
        /// <param name="inquerireid">询价的ID</param>
        void EmailMailtoAskpeople(Guid inquerireid);
        /// <summary>
        /// 邮件发给承运人
        /// </summary>
        /// <param name="inquerireid">询价的ID</param>
        void EmailtoCarrier(Guid inquerireid);
    }
}
