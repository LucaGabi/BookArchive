using BookArchive.API.Config;
using BookArchive.Application;
using BookArchive.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Security.Policy;

namespace BookArchive.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostEnvironment { get; }
        public IServer Server { get; }
        public IHttpContextAccessor HttpContextAccessor { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddApplication();
            services.AddDatabase(Configuration.GetConnectionString("DefaultConnection"));

            services.AddControllers(c => c.ModelBinderProviders.Insert(0, new FormDataJsonBinderProvider()))
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Include;
                        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                        //options.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
                    });

            //services.AddSwagger();
            services.AddSwaggerGen(
            //o => o.OperationFilter<IOperationFilter>()
            );

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(typeof(IFileStorageService),
                _ =>
                {
                    var url = Configuration[WebHostDefaults.ServerUrlsKey];
                    var accessURI = new Uri(url.Split(';').Where(x => x.StartsWith("https")).First());
                    var webRootPath = HostEnvironment.WebRootPath;

                    return new LocalWebFileStorageService(webRootPath, "images", accessURI);
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("MyPolicy");
            app.UseSwaggerPage();

            app.UseHttpsRedirection();
            
            app.UseStaticFiles();

            app.UseCustomExceptionHandler();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}