
//-----------------------------------------------------------------------
// <copyright file="WorkFlowClientService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.FormDesigner
{
    using System.Collections;
    using System.Windows.Forms;
    using ICP.WF.Controls;
    using ICP.WF.ServiceInterface;

    /// <summary>
    /// 流程客户短服务    /// </summary>
    public class WorkFlowClientService:IFormDesignClientService
    {
        /// <summary>
        /// 获取设计时控件 
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <returns></returns>
        public Control BuildFormFromXml(string xmldata)
        {
            return (UserControl)FormBuildService.CreateObjectFromXmlData(
                null,
                xmldata, 
               new ArrayList());
        }


        ///// <summary>
        ///// 获取运行时候控件 
        ///// </summary>
        ///// <param name="xmlFile"></param>
        ///// <returns></returns>
        //public UserControl GetFormFromXml(string xmlFile)
        //{
        //    //return (UserControl)ICP.WF.Controls.FormBuildService.CreateObjectFromFile(xmlFile,new System.Collections.ArrayList());
        //}
    }
}
