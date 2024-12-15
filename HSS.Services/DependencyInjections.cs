using HSS.Domain.Helpers;
using HSS.Services.Abstractions;
using HSS.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace HSS.Services
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddServiceLayerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenServices, TokenServices>();

            services.Configure<JwtHelper>(configuration.GetSection("Jwt"));

            var jwtConfig = new JwtHelper();
            configuration.GetSection("Jwt").Bind(jwtConfig);
            services.AddAuthServices(jwtConfig);

            return services;
        }

        private static IServiceCollection AddAuthServices(this IServiceCollection services, JwtHelper jwtConfig)
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
                        ValidIssuer = jwtConfig.JwtIssuer,
                        ValidAudience = jwtConfig.JwtAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.JwtKey)),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });
            return services;
        }
    }
}
 