using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Fuzzy.Api.Controllers;
using Fuzzy.Api.Infra.CrossCutting.InversionOfControl;

namespace Fuzzy.Api
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
            services.AddMvc(config =>
            {
                config.EnableEndpointRouting = false;
            });

            //services.Configure<MvcOptions>(options => {
            //    options.Filters.Add(new CorsAuthorizationFilterFactory("AllowSpecificOrigin"));
            //});
            services.AddCors();
            //services.AddCors(options =>
            //{
            //    options.AddDefaultPolicy(
            //        builder =>
            //        {
            //            builder
            //                .WithOrigins("https://localhost:3000", "http://localhost:3000", "http://localhost:3006")
            //                .AllowAnyHeader().AllowAnyMethod().AllowCredentials();
            //        });
            //});

            // Added for functional tests
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                )
                .AddApplicationPart(typeof(AssetController).Assembly)
                .AddApplicationPart(typeof(WalletController).Assembly);

            services.AddSqlDependency(Configuration);
            services.AddSqlRepositoryDependency();
            services.AddServiceDependency();

            //job consume data
            //services.Configure<BackgroundTaskSettings>(this.Configuration)
            //    .AddOptions()
            //    .AddHostedService<GracePeriodManagerService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseCors(option => option.AllowAnyOrigin());

            app.UseMvc();
            app.UseHttpsRedirection();
            app.UseRouting();
            //app.UseCors();
        }
    }
}
