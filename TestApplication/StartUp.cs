using UserInsightsPackage.Extensions;
namespace TestApplication
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
            // Add any services your application needs here

            // Add Application Insights integration
            services.AddApplicationInsightsTelemetry(Configuration["ApplicationInsights:InstrumentationKey"]);

            // Add other services as needed
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

            // Use the User Tracking Middleware from your class library
            app.UseUserTrackingMiddleware();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});
        }
    }
}
