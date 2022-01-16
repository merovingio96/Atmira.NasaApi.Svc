using Atmira.NasaApi.Svc.Services.V1.Asteroids;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Atmira.NasaApi.Svc
{
    public partial class Startup
    {
        public static IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            IMvcBuilder builder = services.AddControllers();
            ConfigureJsonSettings(builder);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddControllers();

            ConfigureSwagger(services);
            AddHttpClients(services);
            RegisterApplicationServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseExceptionHandler(api =>
                api.Run(async context => await ExceptionHandler.Run(context, env.IsDevelopment()))
            );

            app.UseHttpsRedirection();

            ConfigureSwagger(app);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void RegisterApplicationServices(IServiceCollection services)
        {
            services.AddTransient<IAsteroidsService, AsteroidsService>();
        }
    }
}
