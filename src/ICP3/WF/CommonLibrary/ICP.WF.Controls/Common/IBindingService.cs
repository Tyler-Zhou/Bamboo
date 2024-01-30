//-----------------------------------------------------------------------
// <copyright file="IBindingService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.Controls
{
    using System;

    /// <summary>
    /// 需要绑定数据源的控件必须实现该接口
    /// </summary>
    public interface IBindingService
    {
        /// <summary>
        /// 数据源里面的属性
        /// </summary>
        string DataProperty { get; set; }

        /// <summary>
        /// 对应数据的类型
        /// </summary>
        Type DataPropertyType { get; }

        /// <summary>
        /// 控件属性
        /// </summary>
        string ControlProperty { get; set; }

        /// <summary>
        /// 支持绑定的控件属性
        /// </summary>
        /// <returns></returns>
        string[] GetCanBindingControlProperty();

        /// <summary>
        /// 绑定到对应的数据源中
        /// </summary>
        /// <param name="datasource"></param>
        void Binding(object datasource);
    }

    
}
