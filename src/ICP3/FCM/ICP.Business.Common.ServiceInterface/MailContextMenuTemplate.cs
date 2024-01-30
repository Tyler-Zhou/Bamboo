#region Comment

/*
 * 
 * FileName:    MailContextMenuTemplate.cs
 * CreatedOn:   2015/9/30 16:17:57
 * CreatedBy:   taylor 
 * 
 * 
 * Description：
 *      ->
 * History：
 *      ->
 * 
 * 
 * 
 * 
 */

#endregion

using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Operation.Common.ServiceInterface;
using System.Collections.Generic;

namespace ICP.Business.Common.ServiceInterface
{
    /// <summary>
    /// 
    /// </summary>
    public class MailContextMenuTemplate
    {
        #region 全局变量，服务
        /// <summary>
        /// 
        /// </summary>
        public IICPCommonOperationService ICPCommonOperationService
        {
            get
            {
                return ServiceClient.GetClientService<IICPCommonOperationService>();
            }
        }


        #endregion

        /// <summary>
        /// 生成ToolStripMenuItem集合
        /// </summary>
        /// <returns></returns>
        public List<OperationToolbarCommand> GetToolbarCommands(BusinessOperationContext context)
        {
            List<OperationToolbarCommand> commandList = ToolbarTemplateLoader.Current["CommunicationHistory" + context.OperationType];

            if (commandList == null || commandList.Count <= 0)
            {
                commandList = new List<OperationToolbarCommand>();
            }
            return commandList;
        }
    }
}
