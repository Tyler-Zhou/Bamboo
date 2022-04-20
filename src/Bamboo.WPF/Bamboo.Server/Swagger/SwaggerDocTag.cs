using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace Bamboo.Server.Swagger
{
    /// <summary>
    /// Swagger注释帮助类
    /// </summary>
    public class SwaggerDocTag : IDocumentFilter
    {
        /// <summary>
        /// 添加附加注释
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Tags = new List<OpenApiTag>
            {
                new OpenApiTag { Name = "Authentication", Description = "验证控制器" },
                new OpenApiTag { Name = "Book", Description = "书籍控制器" },
                new OpenApiTag { Name = "Chapter", Description = "书籍章节控制器" },
            };
        }
    }
}
