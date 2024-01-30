
//-----------------------------------------------------------------------
// <copyright file="SRDescriptionAttribute.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.Controls
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// 描述信息特性
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    internal sealed class SRDescriptionAttribute : DescriptionAttribute
    {
        string desc;

        public SRDescriptionAttribute(string description)
        {
            desc = description;
        }

        public override string Description
        {
            get
            {
                return Utility.GetString(desc, desc);
            }
        }

    }


    /// <summary>
    /// 标题信息特性
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    internal sealed class SRTitleAttribute : DescriptionAttribute
    {
        string ttl;

        public SRTitleAttribute(string title)
        {
            ttl = title;
        }

        public override string Description
        {
            get
            {
                return Utility.GetString(ttl, ttl);
            }
        }

    }


    [AttributeUsage(AttributeTargets.All)]
    internal sealed class SRDisplayNameAttribute : DisplayNameAttribute
    {
        public SRDisplayNameAttribute(string description)
        {
            base.DisplayNameValue = Utility.GetString(description, description);
        }
    }

}
