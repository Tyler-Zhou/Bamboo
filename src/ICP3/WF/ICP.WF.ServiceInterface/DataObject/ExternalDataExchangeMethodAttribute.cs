using System;

namespace ICP.WF.ServiceInterface.DataObject
{
    /// <summary>
    /// 如果是业务接口,标上该特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, Inherited = true)]
    public class ExternalMehtodInterfaceAttribute : Attribute
    {
        string _description;
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        string _aliasName;
        /// <summary>
        /// 接口别名
        /// </summary>
        public string AliasName
        {
            get { return _aliasName; }
            set { _aliasName = value; }
        }

        public ExternalMehtodInterfaceAttribute()
        {
        }

        public ExternalMehtodInterfaceAttribute(string description)
        {
            _description = description;
        }

        public ExternalMehtodInterfaceAttribute(string aliasName, string description)
        {
            _aliasName = aliasName;
            _description = description;
        }
    }

    /// <summary>
    /// 如果是要在CallExternalMethod活动中调用的方法,必须标上该特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class ExternalMethodAttribute : Attribute
    {
        string _description;
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        string _aliasName;
        /// <summary>
        /// 别名
        /// </summary>
        public string AliasName
        {
            get { return _aliasName; }
            set { _aliasName = value; }
        }

        public ExternalMethodAttribute()
        {
        }

        public ExternalMethodAttribute(string description)
        {
            _description = description;
        }

        public ExternalMethodAttribute(string aliasName, string description)
        {
            _aliasName = aliasName;
            _description = description;
        }
    }

    /// <summary>
    /// 在标BusinessMethodAttribute特性的方法后,
    /// 可以在阐述上标上BusinessMetodParameterAttribute,以便描述该参数
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, Inherited = true)]
    public class ExternalMetodParameterAttribute : Attribute
    {
        string _description;
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        string _aliasName;
        /// <summary>
        /// 别名
        /// </summary>
        public string AliasName
        {
            get { return _aliasName; }
            set { _aliasName = value; }
        }

        public ExternalMetodParameterAttribute()
        {
        }

        public ExternalMetodParameterAttribute(string description)
        {
            _description = description;
        }

        public ExternalMetodParameterAttribute(
            string aliasName, 
            string description)
        {
            _aliasName = aliasName;
            _description = description;
        }
    }
}
