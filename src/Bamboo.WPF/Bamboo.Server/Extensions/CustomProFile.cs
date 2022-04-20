using AutoMapper;
using Bamboo.Common;
using Bamboo.Library.Common.Dto;
using Bamboo.Server.Models;

namespace Bamboo.Server
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
