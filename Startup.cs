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
using NorthWindApiSample.Common.Swagger;
using NorthWindApiSample.Data.Database.Models;
using NorthWindApiSample.Data.Repository.Orders.Implementation;
using NorthWindApiSample.Data.Repository.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWindApiSample
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

            services.AddDbContext<NorthWindContext>(options =>
            options
            .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
            //.EnableSensitiveDataLogging()
            .UseSqlServer(Configuration.GetValue<string>("ConnectionStrings:Default"))
            );

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NorthWindSampleRestApi", Version = "v1" });
                c.UseInlineDefinitionsForEnums();

                c.SchemaFilter<EnumSchemaFilter>();

               
            });

            services.AddTransient<IOrdersRepository, OrdersRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AnyOrigin");
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "NorthWindSample v1");

            });

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
