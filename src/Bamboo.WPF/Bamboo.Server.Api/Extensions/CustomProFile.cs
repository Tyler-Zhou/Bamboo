using AutoMapper;
using Bamboo.Entities;
using Bamboo.Library.Entities;
using Bamboo.Server.Entities;

namespace Bamboo.Server.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomProFile : MapperConfigurationExpression
    {
        /// <summary>
        /// 
        /// </summary>
        public CustomProFile()
        {
            CreateMap<UserEntity, UserDto>().ReverseMap();
            CreateMap<BookEntity, BookDto>().ReverseMap();
            CreateMap<ChapterEntity, ChapterDto>().ReverseMap();
        }
    }
}
