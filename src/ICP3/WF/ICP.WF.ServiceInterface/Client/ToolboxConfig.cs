

//-----------------------------------------------------------------------
// <copyright file="ToolboxTabCollection.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.ServiceInterface.Client
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// 工具栏列表对象
    /// </summary>
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute()]
    [System.Xml.Serialization.XmlRootAttribute(IsNullable = false, ElementName = "ToolboxConfig")]
    public class ToolboxConfig 
    {
        public ToolboxConfig()
        {
            this.Groups = new List<ToolboxGroup>();
        }

        /// <summary>
        /// 组列表
        /// </summary>
        [System.Xml.Serialization.XmlElement("Group")]
        public List<ToolboxGroup> Groups { get; set; }

        /// <summary>
        /// 查找指定类型的组
        /// </summary>
        /// <param name="type">组类型</param>
        /// <returns></returns>
        public List<ToolboxGroup> FindGroups(ToolboxGroupType type)
        {
            if (this.Groups.Count == 0)
            {
                return this.Groups;
            }

            return this.Groups.FindAll(delegate(ToolboxGroup g)
            {
                return g.Type == type;
            });
        }
    }
}
