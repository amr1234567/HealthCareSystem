using HSS.DataAccess.Contexts;
using HSS.DataAccess.Helpers;
using HSS.DataAccess.Repositories;
using HSS.Domain.Helpers;
using HSS.Services.Abstractions;
using HSS.Services.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using HSS.DataAccess;

namespace HSS.Services
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddServiceLayerServicesForApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtHelper>(configuration.GetSection("Jwt"));

            var jwtConfig = new JwtHelper();
            configuration.GetSection("Jwt").Bind(jwtConfig);
            services.AddServiceLayerServices(configuration);
            services.AddAuthServicesForApi(jwtConfig);

            return services;
        }

        public static IServiceCollection AddServiceLayerServicesForMvc(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CookieConfiguration>(configuration.GetSection(nameof(CookieConfiguration)));
            services.AddHttpContextAccessor();
            services.AddAuthServicesForMvc();
            services.AddScoped<CustomSignInManager>();
            services.AddServiceLayerServices(configuration);

            return services;
        }

        
        public static async Task<WebApplication> UseApiAuth(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var accountServices = scope.ServiceProvider.GetRequiredService<AccountServicesHelpers>();

                await context.Database.MigrateAsync();
                await context.SeedRolesAsync();
                await context.SeedAccountsAsync(accountServices);
                await context.SeedSystemDetailsAsync();
                await context.SeedPatients();
                await context.SeedDoctorsAndClinicsAsync();
                await context.SeedClinicAppointmentsAsync();
                await context.SeedDiseasesAndRelatedDataAsync();
                await context.SeedMedicineAsync();
                await context.SeedLabTestsAsync();
                await context.SeedRadiologyTestTypesAsync();
            }

            return app;
        }

        public static async Task<WebApplication> UseMvcAuth(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            var cookiePolicyOptions = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                Secure = CookieSecurePolicy.SameAsRequest
            };
            app.UseCookiePolicy(cookiePolicyOptions);
            
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var accountServices = scope.ServiceProvider.GetRequiredService<AccountServicesHelpers>();

                if (!context.Database.CanConnect() || !context.IdentityUsers.Any())
                {
                    await context.Database.MigrateAsync();
                    await context.SeedRolesAsync();
                    await context.SeedAccountsAsync(accountServices);
                    await context.SeedSystemDetailsAsync();
                    await context.SeedPatients();
                    await context.SeedDoctorsAndClinicsAsync();
                    await context.SeedClinicAppointmentsAsync();
                    await context.SeedDiseasesAndRelatedDataAsync();
                    await context.SeedMedicineAsync();
                    await context.SeedLabTestsAsync();
                    await context.SeedRadiologyTestTypesAsync();
                }
            }
            return app;
        }



        private static IServiceCollection AddServiceLayerServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenServices, TokenServices>();
            services.AddScoped<IClinicService, ClinicService>();
            services.AddScoped<IReceptionServices, ReceptionServices>();
            services.AddScoped(typeof(IUserIdentityServices<>), typeof(UserIdentityServices<>));
            services.AddAutoMapper(r => { });
            return services;
        }

        private static IServiceCollection AddAuthServicesForApi
            (this IServiceCollection services, JwtHelper jwtConfig)
        {

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = !string.IsNullOrEmpty(jwtConfig.JwtIssuer),
                        ValidateAudience = !string.IsNullOrEmpty(jwtConfig.JwtAudience),
                        ValidIssuer = jwtConfig.JwtIssuer,
                        ValidAudience = jwtConfig.JwtAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.JwtKey)),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });
            return services;
        }
   
        private static IServiceCollection AddAuthServicesForMvc(this IServiceCollection services)
        {

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {

                    options.LoginPath = "/Home/Login";
                    options.AccessDeniedPath = "/Home/UnAuthAccess";
                    options.LogoutPath = "/Home/Logout";

                });
            return services;
        }
    
        
    }
}
 