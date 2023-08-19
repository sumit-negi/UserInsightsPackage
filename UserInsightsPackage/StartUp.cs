using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserInsightsPackage.Extensions;
using UserInsightsPackage.Logging;
using UserInsightsPackage.Models;
using UserInsightsPackage.Services;

namespace UserInsightsPackage
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<UserTrackingOptions>(Configuration.GetSection("UserTracking"));

            string instrumentationKey = Configuration["ApplicationInsights:InstrumentationKey"];
            services.AddSingleton(new EventLogger(instrumentationKey));

            services.AddScoped<UserEventService>();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseUserTrackingMiddleware();
            //app.UsePrivacyMiddleware();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
