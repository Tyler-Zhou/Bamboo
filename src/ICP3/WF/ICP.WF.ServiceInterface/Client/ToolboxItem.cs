
//-----------------------------------------------------------------------
// <copyright file="ToolboxItem.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.ServiceInterface.Client
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Reflection;
    using System.Xml.Serialization;

	/// <summary>
	/// 工具项.
	/// </summary>
    [Serializable]
    [XmlType]
    public class ToolboxItem
    {
        #region 构造函数 

        public ToolboxItem()
        {
        }

        public ToolboxItem(
            string componentClassName,
            string assemblyName)
		{
			this.componentTypeName = componentClassName;
            this.assembly = assemblyName;
        }

        #endregion

        #region 公共属性

        private string assembly;
        /// <summary>
        /// 程序集
        /// </summary>
        [XmlAttribute("Assembly")]
        public string Assembly
        {
            get { return assembly; }
            set { assembly = value; }
        }

        private string componentTypeName;
        /// <summary>
        /// 组件类名
        /// </summary>
        [XmlAttribute("Type")]
        public string ComponentTypeName
        {
            get { return componentTypeName; }
            set { componentTypeName = value; }
        }


        //private Type componentType;
        ///// <summary>
        ///// 所属组件类型
        ///// </summary>
        //[XmlIgnore]
        //public Type ComponentType
        //{
        //    get
        //    {
        //        if ( componentType == null )
        //        {
        //            componentType = Type.GetType( componentTypeName );
        //            if ( componentType == null )
        //            {
        //                //从引用程序集中加载类型
        //                foreach (AssemblyName referencedAssemblyName in System.Reflection.Assembly.GetExecutingAssembly().GetReferencedAssemblies())
        //                {
        //                    System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(referencedAssemblyName);
        //                    if ( assembly != null )
        //                    {
        //                        componentType = assembly.GetType( componentTypeName );
        //                        if (componentType != null)
        //                        {
        //                            break;
        //                        }
        //                    }
        //                }

        //                //直接加载文件查找
        //                if (componentType == null)
        //                {
        //                    System.Reflection.Assembly assem = System.Reflection.Assembly.LoadFrom(assembly);
        //                    if (assembly != null)
        //                    {
        //                        componentType = assem.GetType(componentTypeName);
        //                    }
        //                }
        //            }
        //        }

        //        return componentType;
        //    }
        //}

        //private string title;
        ///// <summary>
        ///// 工具项显示标题
        ///// </summary>
        //[XmlIgnore]
        //public string Title
        //{
        //    get
        //    {
        //        if ( string.IsNullOrEmpty(title) )
        //        {
        //            if (ComponentType != null)
        //            {
        //                DescriptionAttribute attr = (DescriptionAttribute)TypeDescriptor.GetAttributes(this.ComponentType)[typeof(SRDescriptionAttribute)];
        //                if (attr != null)
        //                {
        //                    title = attr.Description;
        //                }
        //                else
        //                {
        //                    title = ComponentType.Name;
        //                }
        //            }
        //            else
        //            {
        //                title = "Unknown Item";
        //            }
        //        }

        //        return title;
        //    }
        //}

        //private Image glyph = null;
        ///// <summary>
        ///// 工具项显示图片
        ///// </summary>
        //[XmlIgnore]
        //public System.Drawing.Image Glyph
        //{
        //    get
        //    {
        //        if ( glyph == null )
        //        {
        //            ToolboxBitmapAttribute attr = (ToolboxBitmapAttribute)TypeDescriptor.GetAttributes(this.ComponentType)[typeof(ToolboxBitmapAttribute)];

        //            if (attr != null)
        //            {
        //                glyph = attr.GetImage(this.ComponentType, false);
        //            }
        //        }

        //        return glyph;
        //    }
        //}

        #endregion

        #region 重载ToString()

        public override string ToString()
		{
			return componentTypeName;
        }
        #endregion
    }
}
