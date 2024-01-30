
//-----------------------------------------------------------------------
// <copyright file="IBindingSourceTypeService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.Controls
{
    using System;

    /// <summary>
    /// 取BindingSource里面的数据对象类型    /// </summary>
    public interface IBindingSourceTypeService
    {
        Type DataType { get; }
    }
}
