using System.IO;
using System.Reflection;
using AspNetSecurity_m3.Data;
using AspNetSecurity_m3.Repositories;
using AspNetSecurity_m3.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspNetSecurity_m3
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var config = builder.Build();

            services.AddMvc();

            services.AddSingleton<ConferenceRepo>();
            services.AddSingleton<ProposalRepo>();
            services.AddSingleton<AttendeeRepo>();

            services.AddDbContext<ConfArchDbContext>(options =>
                    options.UseSqlServer(config.GetConnectionString("ConfArchConnection"), sqlOptions => sqlOptions.MigrationsAssembly("AspNetSecurity-m3")));

            services.AddIdentity<ConfArchUser, IdentityRole>((options =>
                        options.Password.RequireNonAlphanumeric = false))
                .AddEntityFrameworkStores<ConfArchDbContext>();
            services.AddTransient<IUserClaimsPrincipalFactory<ConfArchUser>,
                ConfArchUserClaimsPrincipalFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Conference}/{action=Index}/{id?}");
            });
        }
    }
}
