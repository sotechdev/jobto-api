using JobTo.API.Configurations;
using JobTo.Commom.Data;
using JobTo.Common.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace JobTo.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("JobToDatabase");
            services.AddDbContext<JobToDbContext>(x =>
            {
                x.UseNpgsql(connectionString, x => 
                { 
                    x.SetPostgresVersion(new Version(9, 6));
                    x.MigrationsAssembly("JobTo.API");
                });
            });
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddSwagger();
            services.AddControllers();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => 
                { 
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "JobTo v1");
                });
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
