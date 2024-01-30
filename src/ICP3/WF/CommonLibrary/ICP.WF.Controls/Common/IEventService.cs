//-----------------------------------------------------------------------
// <copyright file="IEventService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.Controls
{
    /// <summary>
    /// 需要处理事件的控件，必须实现该接口 
    /// </summary>
    public interface IEventService
    {
        EventData Event { get; set; }

        /// <summary>
        /// 可以处理的事件类型        /// </summary>
        EventType EventType
        {
            get;
            set;
        }

        /// <summary>
        /// 目标控件名        /// </summary>
        string TargetControlName
        {
            get;
            set;
        }

        /// <summary>
        /// 要绑定的原控件数据源里面对应的属性。        /// </summary>
        string SourceDataProperty
        {
            get;
            set;
        }


        /// <summary>
        /// 目标控件的属性        /// </summary>
        string TargetControlProperty
        {
            get;
            set;
        }

        /// <summary>
        /// 显示格式传txtName.Text=Name:{Name},Tel:{Tel}.
        /// </summary>
        string FormartString
        {
            get;
        }

        /// <summary>
        /// 目标控件绑定数据源信息        /// </summary>
        IBindingService TargetControl
        {
            get;
        }
    }
}
