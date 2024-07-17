using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Application.Middlewares.ExceptionMiddleware;
using CleanArchitecture.Application;
using CleanArchitecture.Infrastructure.ScheduleServices;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Mapper;
using CleanArchitecture.Persistence;
using Hangfire;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Hangfire.PostgreSql;

namespace CleanArchitecture.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            #region Custom Configuration

            builder.Services.AddHangfire(configuration =>
                    configuration.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("HangfireConnection"), new Hangfire.PostgreSql.PostgreSqlStorageOptions
                    {
                        DistributedLockTimeout = TimeSpan.FromMinutes(5)  // Set your desired lock timeout
                    })
            );

            builder.Services.AddHangfireServer();

            builder.Services.AddCors();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddPersistence(builder.Configuration);
            builder.Services.AddApplication();
            builder.Services.AddCustomeMapper();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            builder.Services.AddSingleton<IUrlHelperFactory, UrlHelperFactory>();

            // Register LinkGenerator.
            builder.Services.AddScoped<LinkGeneratorHelper>();

            builder.Services.AddControllers()
                .AddJsonOptions(config =>
                    config.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            #endregion

            #region Swagger Configuration

            builder.Services.AddSwaggerGen(c =>
            {
                c.UseInlineDefinitionsForEnums();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Clean Architecture", Version = "v1", Description = "ECommerce Clean Arch. swagger client." });

                // Adding Bearer token authentication
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "'You can type 'Bearer' and enter the token after leaving a space \r\n\r\n For example: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\""
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
                recurringJobManager.AddOrUpdate<CheckTimeOfSendingUsersCodeScheduleService>(
                    "CheckUserCodesJob",
                    service => service.CheckTimeOfSendingUsersAsync(),
                    "*/5 * * * *",
                    new RecurringJobOptions
                    {
                        TimeZone = TimeZoneInfo.Local,
                        QueueName = "custom-queue"  // Increased lock timeout
                    }
                );
            }

            app.ExceptionHandleConfiguration();
            app.Run();
        }
    }
}
