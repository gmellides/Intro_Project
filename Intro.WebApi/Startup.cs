using AutoMapper;
using Intro.Models.Model;
using Intro.WebApi.Repositories;
using Intro.WebApi.Repositories.Interfaces;
using Intro.WebApi.Services;
using Intro.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Intro.WebApi
{
    public class Startup
    {
        private const string CORS_POLICY_NAME = "allowAllPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(
                options =>
                {
                    options.AddPolicy(CORS_POLICY_NAME,
                                      builder =>
                                      {
                                          builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                                      });
                }
            );

            // AutoMapper configuration
            var userMappingConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new UserMappingProfile());
            });
            IMapper mapper = userMappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Repositories dependency injection
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserTitleRepository, UserTitleRepository>();
            services.AddScoped<IUserTypeRepository, UserTypeRepository>();

            // Services dependency injection
            services.AddScoped<IUserService, UserServices>();
            services.AddScoped<IUserTitleService, UserTitleService>();
            services.AddScoped<IUserTypeService, UserTypeService>();

            // Database Context
            services.AddDbContext<IntroProjectContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Intro")));

            services.AddControllers();

            // Swagger configuration load xml documentation
            services.AddSwaggerGen(options =>
            options.IncludeXmlComments(".\\Intro.WebApi.xml")
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(CORS_POLICY_NAME); // Use Cors

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Intro Web API");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}