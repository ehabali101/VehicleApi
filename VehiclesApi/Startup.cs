using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VehiclesApi.Core;
using VehiclesApi.Core.Repositories;
using VehiclesApi.Persistence;
using VehiclesApi.Persistence.Repositories;

namespace VehiclesApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IVehicleDbContext, VehicleDbContext>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IVehicleOwnersRepository, VehicleOwnersRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddControllers();

            var CosmosSettings = Configuration.GetSection("CosmosSettings");
            services.AddDbContext<VehicleDbContext>(options => {
                options.UseCosmos(
                        CosmosSettings.GetValue<string>("ServiceEndpoint"),
                        CosmosSettings.GetValue<string>("AuthKey"),
                        CosmosSettings.GetValue<string>("DatabaseName")
                    );
            });

            services.AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHealthChecks("/health");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
