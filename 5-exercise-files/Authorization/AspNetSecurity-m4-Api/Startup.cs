using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetSecurity_m4_Api.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspNetSecurity_m4_Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton<AttendeeRepo>();
            services.AddSingleton<ConferenceRepo>();
            services.AddSingleton<ProposalRepo>();

            services.AddAuthorization(o => o.AddPolicy("PostAttendee", 
                p => p.RequireClaim("scope", "confArchApiPostAttendee")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = "http://localhost:5000",
                ApiName = "confArchApi",
                AllowedScopes = new[] {"confArchApi", "confArchApiPostAttendee" },

                RequireHttpsMetadata = false
            });

            app.UseMvcWithDefaultRoute();
        }
    }
}
