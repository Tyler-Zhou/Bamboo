using System;
using System.ComponentModel;
using System.Reflection;
using System.Resources;
using DevExpress.Utils.About;

namespace ICP.WF.Activities
{
    /// <summary>
    /// 描述信息特性
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    internal sealed class SRDescriptionAttribute : DescriptionAttribute
    {
        public SRDescriptionAttribute(string description)
        {
            base.DescriptionValue = SR.GetString(description,description);
        }

        public SRDescriptionAttribute(string description, string resourceSet)
        {
            base.DescriptionValue = new ResourceManager(resourceSet, Assembly.GetExecutingAssembly()).GetString(description);
        }
    }

    [AttributeUsage(AttributeTargets.All)]
    internal sealed class SRDisplayNameAttribute : DisplayNameAttribute
    {
        public SRDisplayNameAttribute(string description)
        {
            base.DisplayNameValue = SR.GetString(description, description);
        }

        public SRDisplayNameAttribute(string description, string resourceSet)
        {
            base.DisplayNameValue = new ResourceManager(resourceSet, Assembly.GetExecutingAssembly()).GetString(description);
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
                return SR.GetString(ttl, ttl);
            }
        }

    }

}
