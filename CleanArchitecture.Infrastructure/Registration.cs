﻿using CleanArchitecture.Application.Interfaces.Mail;
using CleanArchitecture.Application.Interfaces.RedisCache;
using CleanArchitecture.Application.Interfaces.Storage;
using CleanArchitecture.Application.Interfaces.Tokens;
using CleanArchitecture.Infrastructure.Mail;
using CleanArchitecture.Infrastructure.RedisCache;
using CleanArchitecture.Infrastructure.Storage;
using CleanArchitecture.Infrastructure.Tokens;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CleanArchitecture.Infrastructure
{
    public static class Regestration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TokenSettings>(configuration.GetSection("JWT"));
            services.AddTransient<ITokenService, TokenService>();
            services.Configure<MailSettings>(configuration.GetSection("Smtp"));
            services.AddTransient<IRedisCacheService, RedisCacheService>();
            services.AddScoped<ILocalStorage, LocalStorage>();
            services.AddScoped<IMailService, MailService>();
            services.Configure<RedisCacheSettings>(configuration.GetSection("RedisCacheSettings"));

            #region Hangfire
            // Configure Hangfire
            services.AddHangfire(config =>
            {
                config.UsePostgreSqlStorage(configuration["ConnectionStrings:HangFireDb"], new PostgreSqlStorageOptions
                {
                    InvisibilityTimeout = TimeSpan.FromMinutes(5), // equivalent to SlidingInvisibilityTimeout
                    QueuePollInterval = TimeSpan.FromSeconds(1),
                    DistributedLockTimeout = TimeSpan.FromMinutes(10) // adjust based on your needs
                })
                .WithJobExpirationTimeout(TimeSpan.FromDays(7));
            });

            services.AddHangfireServer(options =>
            {
                options.WorkerCount = 5;
            });
            #endregion

            #region Authentication
            services.AddAuthentication
                (
                opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
                ).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
                opt =>
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
                }
                );
            #endregion

            #region Redis
            services.AddStackExchangeRedisCache
                (
                opt =>
                {
                    opt.Configuration = configuration["RedisCacheSettings:ConnectionString"];
                    opt.InstanceName = configuration["RedisCacheSettings:InstanceName"];
                }
                );
            #endregion
        }
    }
}
