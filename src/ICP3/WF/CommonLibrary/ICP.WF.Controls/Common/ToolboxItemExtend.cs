
//-----------------------------------------------------------------------
// <copyright file="ToolboxItemExtend.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.Controls.Common
{
    using System;
    using System.Reflection;
    using System.ComponentModel;
    using System.Drawing;
    using ICP.WF.ServiceInterface.Client;

    public static class ToolboxItemExtend
    {
        /// <summary>
        /// 获取标题
        /// </summary>
        /// <param name="item">工具项</param>
        /// <returns></returns>
        public static string GetTitle(this ToolboxItem item)
        {
            string title;
            if (item.GetComponentType() != null)
            {
                SRTitleAttribute attr = (SRTitleAttribute)TypeDescriptor.GetAttributes(item.GetComponentType())[typeof(SRTitleAttribute)];
                if (attr != null)
                {
                    title = attr.Description;
                }
                else
                {
                    title = item.GetComponentType().Name;
                }
            }
            else
            {
                title = "Unknown Item";
            }

            return title;
        }

        /// <summary>
        /// 获取描述信息
        /// </summary>
        /// <param name="item">工具项</param>
        /// <returns></returns>
        public static string GetDescription(this ToolboxItem item)
        {
            string title;
            if (item.GetComponentType() != null)
            {
                SRDescriptionAttribute attr = (SRDescriptionAttribute)TypeDescriptor.GetAttributes(item.GetComponentType())[typeof(SRDescriptionAttribute)];
                if (attr != null)
                {
                    title = attr.Description;
                }
                else
                {
                    title = item.GetComponentType().Name;
                }
            }
            else
            {
                title = "Unknown Item";
            }

            return title;
        }

        /// <summary>
        /// 获取工具项图标
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Image GetImage(this ToolboxItem item)
        {
            ToolboxBitmapAttribute attr = (ToolboxBitmapAttribute)TypeDescriptor.GetAttributes(item.GetComponentType())[typeof(ToolboxBitmapAttribute)];
            if (attr != null)
            {
                return attr.GetImage(item.GetComponentType(), false);
            }

            return null;
        }


        public static System.Type GetComponentType(this ToolboxItem item)
        {
            Type componentType = Type.GetType(item.ComponentTypeName);
            if (componentType == null)
            {
                //从引用程序集中加载类型
                foreach (AssemblyName referencedAssemblyName in System.Reflection.Assembly.GetExecutingAssembly().GetReferencedAssemblies())
                {
                    System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(referencedAssemblyName);
                    if (assembly != null)
                    {
                        componentType = assembly.GetType(item.ComponentTypeName);
                        if (componentType != null)
                        {
                            break;
                        }
                    }
                }

                //直接加载文件查找
                if (componentType == null)
                {
                    System.Reflection.Assembly assem = System.Reflection.Assembly.LoadFrom(item.Assembly);
                    if (assem != null)
                    {
                        componentType = assem.GetType(item.ComponentTypeName);
                    }
                }
            }

            return componentType;
        }

    }
}
