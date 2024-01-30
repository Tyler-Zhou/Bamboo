#region Comment

/*
 * 
 * FileName:    IOutlookPlugInManageService.cs
 * CreatedOn:   2014/7/11 9:23:43
 * CreatedBy:   taylor 
 * 
 * 
 * Description：
 *      ->OutLook插件管理服务接口
 * History：
 *      ->
 * 
 * 
 * 
 * 
 */

#endregion

using System.ServiceModel;
using ICP.Framework.CommonLibrary.Attributes;

namespace ICP.MailCenterFramework.ServiceInterface
{
    /// <summary>
    /// 询价的接口
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IOutlookPlugInManageService
    {
        /// <summary>
        /// 初始化控件
        /// </summary>
        void InitControl();
    }
}
