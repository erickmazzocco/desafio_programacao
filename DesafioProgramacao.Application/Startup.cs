using AutoMapper;
using DesafioProgramacao.Application.Models;
using DesafioProgramacao.Domain.Dtos;
using DesafioProgramacao.Domain.Entities;
using DesafioProgramacao.Domain.Interfaces;
using DesafioProgramacao.Infra.Data.Context;
using DesafioProgramacao.Infra.Data.Repository;
using DesafioProgramacao.Service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioProgramacao.Application
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
            services.AddControllers();

            services.AddDbContext<DataContext>(
                options => options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IBaseRepository<Product>, BaseRepository<Product>>();
            services.AddScoped<IBaseService<Product>, BaseService<Product>>();
            services.AddScoped<IBaseRepository<Provider>, BaseRepository<Provider>>();
            services.AddScoped<IBaseService<Provider>, BaseService<Provider>>();

            services.AddSingleton(new MapperConfiguration(cfg => { cfg.AddProfile(new Profiles()); }).CreateMapper());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DesafioProgramacao.Application", Version = "v1" });
            });            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DesafioProgramacao.Application v1"));
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
