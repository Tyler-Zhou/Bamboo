using ICP.EDI.ServiceInterface.DataObjects;
using System;

namespace ICP.EDI.ServiceInterface
{
    /// <summary>
    /// EDI客户端服务
    /// </summary>
    public interface IEDIClientService
    {
        /// <summary>
        /// 发送EDI
        /// </summary>
        /// <param name="sendOption">发送选项</param>
        /// <param name="isPreview">是否预览</param>
        /// <returns>是否发送成功</returns>
        bool ShowForm(EDISendOption sendOption, bool isPreview);
        /// <summary>
        /// 通过EDI发送选项获取EDI配置
        /// </summary>
        /// <param name="sendOption"></param>
        /// <returns></returns>
        EDIConfigItem GetEDIConfigByOption(EDISendOption sendOption);
        /// <summary>
        /// 发送EDI
        /// </summary>
        /// <param name="sendEDIItem">EDI发送参数</param>
        /// <returns></returns>
        [Obsolete("方法已过时，请使用 ShowForm(EDISendOption sendOption, bool isPreview) 代替")]
        bool SendEDI(EDISendOption sendEDIItem);

         /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="val">文本内容</param>
        /// <param name="rowLength">行长度</param>
        /// <param name="maxRow">最大行数</param>
        /// <returns></returns>
        string SplitString(string val, int rowLength, int maxRow);


        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="val">文本内容</param>
        /// <param name="rowLength">行长度</param>
        /// <param name="maxRow">最大行数</param>
        /// <param name="isRemoveBlankLines">是否需要去空行</param>
        /// <returns></returns>
        string SplitString(string val, int rowLength, int maxRow, bool isRemoveBlankLines);
    }
}
