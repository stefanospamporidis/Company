using CompanyClassLibrary.Data;
using CompanyClassLibrary.Model;
using CompanyClassLibrary.Services.EquipmentCategoryData;
using CompanyClassLibrary.Services.EquipmentData;
using CompanyClassLibrary.Validators;
using CompanyWebApi.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CompanyWebApi
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

            services.AddDbContextPool<CompanyContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            /*
             Transient objects are always different; a new instance is provided to every controller and every service.
             Scoped objects are the same within a request, but different across different requests.
             Singleton objects are the same for every object and every request.
             */
            services.AddScoped<IEquipmentCategoryService, EquipmentCategoryService>();

            services.AddScoped<IEquipmentCategoryDeleter, EquipmentCategoryDeleter>();

            services.AddScoped<IEquipmentDeleter, EquipmentDeleter>();

            services.AddScoped<IEquipmentCategoryModelBuilder, EquipmentCategoryModelBuilder>();

            services.AddTransient<IValidator<EquipmentCategoryPersist>, EquipmentCategoryValidator>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CompanyWebApi", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CompanyWebApi v1"));
            }

            //app.UseExceptionHandler(err => err.UseCustomErrors(env));
            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            //Adds middleware for redirecting HTTP Requests to HTTPS
            app.UseHttpsRedirection();

            //Matches request to an endpoint.
            app.UseRouting();

            app.UseAuthorization();

            //Execute the matched endpoint.
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
