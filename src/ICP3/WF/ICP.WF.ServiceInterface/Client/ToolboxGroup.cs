


//-----------------------------------------------------------------------
// <copyright file="ToolboxTab.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.ServiceInterface.Client
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

	/// <summary>
	/// 工具项组
	/// </summary>
	public class ToolboxGroup
    {
        #region 构造函数

        public ToolboxGroup()
		{
        }


        #endregion

        #region 公共属性
        private string ctitle;
        /// <summary>
        /// 组中文标题
        /// </summary>
        [XmlAttribute("CTitle")]
        public string CTitle
		{
			get
			{
				return ctitle;
			}
			set
			{
				ctitle = value;
			}
		}

        private string etitle;
        /// <summary>
        /// 组英文标题
        /// </summary>
        [XmlAttribute("ETitle")]
        public string ETitle
        {
            get
            {
                return etitle;
            }
            set
            {
                etitle = value;
            }
        }

        private ToolboxGroupType type;
        /// <summary>
        /// 组中文标题
        /// </summary>
        [XmlAttribute("Type")]
        public ToolboxGroupType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

       

        private List<ToolboxItem> items = new List<ToolboxItem>();

        /// <summary>
        /// 工具项列表
        /// </summary>
        [System.Xml.Serialization.XmlElement("Item")]
        public List<ToolboxItem> Items
		{
			get
			{
				return items;
			}
			set
			{
				items = value;
			}
        }

        #endregion
    }

    /// <summary>
    /// 工具组类型
    /// </summary>
    public enum ToolboxGroupType
    {
        /// <summary>
        /// 表单
        /// </summary>
        [System.Xml.Serialization.XmlEnum(Name = "Form")]
        Form = 1,

        /// <summary>
        /// 工作流
        /// </summary>
        [System.Xml.Serialization.XmlEnum(Name = "Workflow")]
        Workflow = 2,
    }
}
