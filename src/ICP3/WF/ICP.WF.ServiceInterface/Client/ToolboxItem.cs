
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
	/// ������.
	/// </summary>
    [Serializable]
    [XmlType]
    public class ToolboxItem
    {
        #region ���캯�� 

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

        #region ��������

        private string assembly;
        /// <summary>
        /// ����
        /// </summary>
        [XmlAttribute("Assembly")]
        public string Assembly
        {
            get { return assembly; }
            set { assembly = value; }
        }

        private string componentTypeName;
        /// <summary>
        /// �������
        /// </summary>
        [XmlAttribute("Type")]
        public string ComponentTypeName
        {
            get { return componentTypeName; }
            set { componentTypeName = value; }
        }


        //private Type componentType;
        ///// <summary>
        ///// �����������
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
        //                //�����ó����м�������
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

        //                //ֱ�Ӽ����ļ�����
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
        ///// ��������ʾ����
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
        ///// ��������ʾͼƬ
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

        #region ����ToString()

        public override string ToString()
		{
			return componentTypeName;
        }
        #endregion
    }
}
