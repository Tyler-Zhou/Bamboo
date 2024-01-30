using AutoMapper;
using CRM.Client.Entities;
using CRM.Client.Models;

namespace CRM.Client.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class CRMProfile : MapperConfigurationExpression
    {
        public CRMProfile()
        {
            //System
            CreateMap<UserEntity, UserInfo>().ReverseMap();
            //Basic
            CreateMap<CityEntity, CityInfo>().ReverseMap();
        }
    }
}
