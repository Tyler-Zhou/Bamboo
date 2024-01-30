using AutoMapper;

namespace CRM.Client.Interfaces
{
    public interface IAutoMapperProvider
    {
        IMapper GetMapper();
    }
}
