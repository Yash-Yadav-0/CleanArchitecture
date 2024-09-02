using CleanArchitecture.Application.Interfaces.Mail;
using CleanArchitecture.Application.Interfaces.Storage;
using CleanArchitecture.Application.Interfaces.Tokens;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Mail;
using CleanArchitecture.Infrastructure.Storage;
using CleanArchitecture.Infrastructure.Tokens;
using CleanArchitecture.Infrastructure.Tokens.FlexibleAuth;
using CleanArchitecture.Persistence.Context;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace CleanArchitecture.Infrastructure
{
    public static class Registration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TokenSettings>(configuration.GetSection("JWT"));
            services.AddTransient<ITokenService, TokenService>();

            services.AddScoped<IMailService, MailService>();

            services.AddScoped<ILocalStorage, LocalStorage>();

            #region Hangfire
            services.AddHangfire(config =>
            {
                config.UsePostgreSqlStorage(configuration["ConnectionStrings:HangFireDb"], new PostgreSqlStorageOptions
                {
                    InvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.FromSeconds(1),
                    DistributedLockTimeout = TimeSpan.FromMinutes(10)
                })
                .WithJobExpirationTimeout(TimeSpan.FromDays(7));
            });

            services.AddHangfireServer(options =>
            {
                options.WorkerCount = 5;
            });
            #endregion

            //flexible api

            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, FlexibleAuthorizationPolicyProvider>();

            #region Authentication and Authorization

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
            {
                opt.SaveToken = true;
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"])),
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    ClockSkew = TimeSpan.Zero
                };
            });

            /*services.AddAuthorization(options =>
            {
                //policy based on specific claim
                options.AddPolicy("RequiredAdminRole", policy =>
                    policy.RequireClaim(ClaimTypes.Role, "Admin"));
                options.AddPolicy("RequiredVendorRole", policy =>
                    policy.RequireClaim(ClaimTypes.Role, "Vendor"));
                options.AddPolicy("RequiredUserRole", policy =>
                    policy.RequireClaim(ClaimTypes.Role, "USER"));
            });
*/
            #endregion
        }
    }
}
