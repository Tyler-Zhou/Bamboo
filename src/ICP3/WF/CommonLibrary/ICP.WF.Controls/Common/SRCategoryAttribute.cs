
//-----------------------------------------------------------------------
// <copyright file="SRCategoryAttribute.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.Controls
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// 分类信息特性
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    internal sealed class SRCategoryAttribute : CategoryAttribute
    {
        private string resourceSet;

        public SRCategoryAttribute(string category)
            : base(category)
        {
            this.resourceSet = string.Empty;
        }

        public SRCategoryAttribute(string category, string resourceSet)
            : base(category)
        {
            this.resourceSet = string.Empty;
            this.resourceSet = resourceSet;
        }

        protected override string GetLocalizedString(string value)
        {
            return Utility.GetString(value, value);
        }
    }
}
