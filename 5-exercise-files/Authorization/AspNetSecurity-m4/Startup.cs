using System;
using System.Net.Http;
using AspNetSecurity_m4.Api;
using AspNetSecurity_m4.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspNetSecurity_m4
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("OrganizerAccessPolicy", policy => policy.RequireClaim("role", "organizer"));

                options.AddPolicy("SpeakerAccessPolicy",
                    policy => policy.RequireAssertion(context => context.User.HasClaim("role", "speaker")));

                options.AddPolicy("YearsOfExperiencePolicy",
                    policy => policy.AddRequirements(new YearsOfExperienceRequirement(6)));

                options.AddPolicy("ProposalEditPolicy",
                    policy => policy.AddRequirements(new ProposalRequirement(false)));
            });

            services.AddSingleton<IAuthorizationHandler, YearsOfExperienceAuthorizationHandler>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ConferenceApiService>();
            services.AddTransient<ProposalApiService>();
            services.AddTransient<AttendeeApiService>();
            services.AddSingleton(x => new HttpClient {BaseAddress = new Uri("http://localhost:54438")});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, HttpClient httpClient)
        {
            loggerFactory.AddConsole();

            app.UseDeveloperExceptionPage();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
            {
                AuthenticationScheme = "oidc",
                SignInScheme = "Cookies",

                Authority = "http://localhost:5000",
                RequireHttpsMetadata = false,

                ClientId = "confarchweb",
                ClientSecret = "secret",

                ResponseType = "code id_token",
                Scope = { "confArchApi", "roles", "experience" },
                SaveTokens = true,
                GetClaimsFromUserInfoEndpoint = true
            });

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Conference}/{action=Index}/{id?}");
            });
        }
    }
}
