using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NTT.Entities.DbContexts;
using NTT.Interfaces;
using NTT.WebApi;
using NTT.Repository;
using System;
using System.Text.Json.Serialization;

namespace NTT
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
            string sqlConnectionStr = Configuration.GetConnectionString("SqlDbConnection");
            services.AddDbContextPool<NTTContext>(
                options => options.UseSqlServer(sqlConnectionStr));

            services.AddAutoMapper(typeof(AutoMapperConfig));

            services.AddScoped<DbContext, NTTContext>();
            services.AddScoped<IPersonaRepository, PersonaRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<ICuentaRepository, CuentaRepository>();
            services.AddScoped<IMovimientoRepostitory, MovimientoRepository>();
            services.AddScoped<IReporteRepository, ReporteRepository>();


            services.AddControllers()
                    .AddJsonOptions(options =>
                         {
                             options.JsonSerializerOptions
                                    .PropertyNamingPolicy = null;

                             options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                         });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NTT", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NTT v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
