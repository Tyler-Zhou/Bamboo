
//-----------------------------------------------------------------------
// <copyright file="LWTypeDiscoveryService.cs" company="ICP">
//     Copyright (c) ICP. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.WorkFlowDesigner
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
                            if (type.Equals(typeof(ICP.WF.Activities.ApplicationActivity))
                                || type.Equals(typeof(ICP.WF.Activities.ApproveActivity))
                                || type.Equals(typeof(ICP.WF.Activities.LWIfElseActivity))
                                || type.Equals(typeof(ICP.WF.Activities.SendMessageActivity))
                                || type.Equals(typeof(ICP.WF.Activities.SendEMailActivity))
                                || type.Equals(typeof(ICP.WF.Activities.LWCallExternalMethodActivity)))
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
