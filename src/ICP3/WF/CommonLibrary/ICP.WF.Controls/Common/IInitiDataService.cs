
//-----------------------------------------------------------------------
// <copyright file="IInitiDataService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.Controls
{
    using System.Collections.Generic;

    /// <summary>
    /// 对于控件要初始化数据的时候,必须实现该接口.(比如说从服务中获取数据的)
    /// </summary>
    public interface IInitiDataService
    {
        /// <summary>
        /// 初始化数据        /// </summary>
        /// <param name="containerService">服务容器</param>
        /// <param name="vars">其它参数</param>
        void InitData(
            IServiceContainerManager containerService, 
            IDictionary<string, object> vars);
    }
}
