
//-----------------------------------------------------------------------
// <copyright file="IContainerService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.Controls
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 服务容器管理接口
    /// </summary>
    public interface IServiceContainerManager
    {
        void Add(Type type, object instance);

        void Remove(Type type);

        object Get(Type type);

        void Clear();
    }

    /// <summary>
    /// 服务容器管理实现
    /// </summary>
    public class ServiceContainerManager : IServiceContainerManager
    {
        Dictionary<Type, object> services = new Dictionary<Type, object>();

        public void Add(Type type, object instance)
        {
            if (services.ContainsKey(type) == false)
            {
                services.Add(type,instance);
            }
        }

        public void Remove(Type type)
        {
            if (services.ContainsKey(type))
            {
                services.Remove(type);
            }
        }

        public object Get(Type type)
        {
            if (services.ContainsKey(type))
            {
                return services[type];
            }
            else
            {
                return null;
            }
        }

        public void Clear()
        {
            services.Clear();
        }
    }


    /// <summary>
    /// 服务容器
    /// </summary>
    public interface IServiceContainer
    {
        IServiceContainerManager ServiceContainer { get; set; }
    }
}
