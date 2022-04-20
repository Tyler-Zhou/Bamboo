using AutoMapper;
using Bamboo.Server.Context;
using Bamboo.Server.Core;
using Bamboo.Server.Filter;
using Bamboo.Server.Interface;
using Bamboo.Server.Models;
using Bamboo.Server.Repository;
using Bamboo.Server.Service;
using Bamboo.Server.SwaggerHelp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Bamboo.Server
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //添加异常处理过滤器
            services.AddControllers(options => options.Filters.Add(typeof(CustomerGlobalExceptionFilterAsync)));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ILogService, LogService>();

            services.AddDbContext<DefaultContext>(option =>
            {
                var connectionString = Configuration.GetConnectionString("BambooConnection");
                option.UseSqlServer(connectionString);
            }).AddUnitOfWork<DefaultContext>()
            .AddCustomRepository<UserEntity, UserRepository>()
            .AddCustomRepository<BookEntity, BookRepository>()
            .AddCustomRepository<ChapterEntity, ChapterRepository>()
            ;

          
            /*
             * AddTransient瞬时模式：每次请求，都获取一个新的实例。即使同一个请求获取多次也会是不同的实例
             * AddScoped：每次请求，都获取一个新的实例。同一个请求获取多次会得到相同的实例
             * AddSingleton单例模式：每次都获取同一个实例
             */
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IChapterService, ChapterService>();

            //添加AutoMapper
            var automapperConfog = new MapperConfiguration(config =>
            {
                config.AddProfile(new CustomProFile());
            });

            services.AddSingleton(automapperConfog.CreateMapper());

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Bamboo API",
                    Description = "Bamboo API",
                    Version = "v1"
                });

                //启用中文注释功能
                var dirs = Directory.GetFiles(AppContext.BaseDirectory, "Bamboo.*.xml").Where(item=> (new FileInfo(item).Attributes & FileAttributes.Hidden) == 0).ToArray();
                foreach (var item in dirs)
                {
                    c.IncludeXmlComments(item);
                }
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
                //排序
                c.OrderActionsBy(o => o.RelativePath);
                //添加对控制器的标签
                c.DocumentFilter<SwaggerDocTag>();
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bamboo API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
