using AddressWebApi.Configuretion;
using AddressWebApi.Data;
using AddressWebApi.Interface;
using AddressWebApi.Middleware;
using AddressWebApi.Model;
using AddressWebApi.Repository;
using AddressWebApi.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressWebApi
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
            services.AddMvc(option => option.EnableEndpointRouting = false)
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            //services.AddCors();

            services.AddCors(options => options.AddDefaultPolicy(
                builder => builder
                           .AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod()));

            services.AddScoped<ApiDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IAddressRepository, AddressRepository>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped(typeof(IGenericRepository<User>), typeof(GenericRepository<User>));
            services.AddScoped(typeof(IGenericService<User>), typeof(GenericService<User>));

            services.AddScoped(typeof(IGenericRepository<Address>), typeof(GenericRepository<Address>));
            services.AddScoped(typeof(IGenericService<Address>), typeof(GenericService<Address>));

            ConfigureSwagger(services);
        }
        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            services.AddLogging();
        }

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            //app.UseMiddleware<ExceptionHandlerMiddleware>();

            //app.UseMiddleware<RequestResponseMiddleware>();

            app.UseRouting();

            //app.UseCors(
            //    x => x
            //    .AllowAnyMethod()
            //    .AllowAnyHeader()
            //    .SetIsOriginAllowed(origin => true) // allow any origin
            //    .AllowCredentials());


            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
