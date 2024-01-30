
//-----------------------------------------------------------------------
// <copyright file="CommonController.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.Common.UI.Common
{
    using Microsoft.Practices.CompositeUI;
    using ICP.Common.ServiceInterface;
    using ICP.Framework.CommonLibrary.Client;

    /// <summary>
    /// 封装其它模块的接口方法.()
    /// </summary>
    public class CommonController:Controller
    {
        #region 注入服务

        //地点管理服务
        public IGeographyService GeographyService 
        {
            get
            {
                return ServiceClient.GetService<IGeographyService>();
            }
        }

        #endregion

        #region 地点管理接口方法


        #endregion
    }
}
