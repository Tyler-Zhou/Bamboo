
//-----------------------------------------------------------------------
// <copyright file="LWTypeDiscoveryService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.FormDesigner
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.Design;
    using System.Reflection;

    /// <summary>
    /// 发现设计时可用的类型。
    /// </summary>
    public class LWTypeDiscoveryService : ITypeDiscoveryService
    {
        public ICollection GetTypes(
            Type baseType,
            bool excludeGlobalTypes)
        {
            if (baseType == null)
            {
                throw new ArgumentNullException("baseType");
            }

            List<Type> derivedTypes = new List<Type>();
            AssemblyName baseAssemblyName = baseType.Assembly.GetName();
            Assembly[] assemblys = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblys)
            {
                bool needSearch = false;
                if (!excludeGlobalTypes
                    || !assembly.GlobalAssemblyCache)
                {
                    if (string.Equals(
                        assembly.FullName,
                        baseAssemblyName.FullName,
                        StringComparison.OrdinalIgnoreCase))
                    {
                        needSearch = true;
                    }
                    else
                    {
                        AssemblyName[] referencedAssemblies = assembly.GetReferencedAssemblies();
                        foreach (AssemblyName assemblyName in referencedAssemblies)
                        {
                            if (string.Equals(
                                assemblyName.FullName,
                                baseAssemblyName.FullName,
                                StringComparison.OrdinalIgnoreCase))
                            {
                                needSearch = true;
                                break;
                            }
                        }
                    }
                }

                if (needSearch)
                {
                    Type[] types = assembly.GetTypes();
                    foreach (Type type in types)
                    {
                        if ((type.IsPublic || type.IsNestedPublic)
                            && baseType.IsAssignableFrom(type)
                            && !derivedTypes.Contains(type))
                        {
                            if (type.Equals(typeof(ICP.WF.Controls.LWDateTimeColumn))
                                || type.Equals(typeof(ICP.WF.Controls.LWChargeCodeColumn))
                                || type.Equals(typeof(ICP.WF.Controls.LWComboxColumn))
                                || type.Equals(typeof(ICP.WF.Controls.LWVehicleHeadColumn))
                                || type.Equals(typeof(ICP.WF.Controls.LWCurrencyColumn))
                                || type.Equals(typeof(ICP.WF.Controls.LWNumericColumn))
                                || type.Equals(typeof(ICP.WF.Controls.LWStringColumn))
                                || type.Equals(typeof(ICP.WF.Controls.LWCostItemColumn))
                                || type.Equals(typeof(ICP.WF.Controls.LWGLCodeColumn))
                                || type.Equals(typeof(ICP.WF.Controls.LWFindTextColumn))
                                || type.Equals(typeof(ICP.WF.Controls.LWDeptColumn)))
                            {
                                derivedTypes.Add(type);
                            }
                        }
                    }
                }
            }

            return derivedTypes.ToArray();
        }
    }
}
