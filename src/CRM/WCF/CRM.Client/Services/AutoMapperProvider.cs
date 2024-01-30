using AutoMapper;
using CRM.Client.Interfaces;
using Prism.Ioc;
using System;

namespace CRM.Client.Services
{
    public class AutoMapperProvider : IAutoMapperProvider
    {
        private readonly MapperConfiguration _configuration;

        public AutoMapperProvider(IContainerProvider container)
        {
            _configuration = new MapperConfiguration(configure =>
            {
                configure.ConstructServicesUsing(container.Resolve);

                //扫描profile文件
                configure.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
            });
        }

        public IMapper GetMapper()
        {
            return _configuration.CreateMapper();
        }
    }
}
