using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Behaviors;
using CleanArchitecture.Application.Features.Auth.Commands.EmailConfirmation;
using CleanArchitecture.Application.Features.Orders.Comments.DeleteOrder;
using CleanArchitecture.Application.Features.Orders.Rules;
using CleanArchitecture.Application.Features.Products.Rules;
using CleanArchitecture.Application.Features.UserFeature.Queries;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Application.Middlewares.ExceptionMiddleware;
using CleanArchitecture.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Globalization;
using System.Reflection;


namespace CleanArchitecture.Application
{
    public static class Regestration
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            //mediatr registration
            services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));

            //fluentValidation
            services.AddValidatorsFromAssembly(assembly);
            services.AddTransient<ExceptionMiddleware>();

            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en-US");

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehaviors<,>));

            // HTTP context and action context accessor
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();
            services.AddScoped<ILinkGeneratorHelper, LinkGeneratorHelper>();

            // Add additional service registrations

            services.AddSingleton<EmailConfirmationCommandRequest>();
            services.AddTransient<ProductRules>();
            services.AddTransient<IOrderRules,OrderRules>();
            services.AddTransient<IRequestHandler<DeleteOrderCommandRequest, Unit>, DeleteOrderCommandHandler>();
            services.AddTransient<SignInManager<User>>();
            services.AddRulesFromAssemblyContaining(assembly, typeof(BaseRule));
            services.AddSingleton<IProductRules, ProductRules>();


            //Opentelemetry configuration
            services.AddOpenTelemetry()
                .ConfigureResource(resource => resource.AddService("CleanArchitectureService"))
                .WithMetrics(metrics =>
                {
                    metrics
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddMeter("CleanArchitectureMeter")
                        .AddOtlpExporter();
                })
                .WithTracing(tracing =>
                {
                    tracing
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddEntityFrameworkCoreInstrumentation()
                        .AddOtlpExporter();
                });
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new PermissionsConverter());
                });
            //Serilog Configuration
            /*services.AddSingleton<Logger>(provider =>
            {
                Logger.LoggerMethod();
                return new Logger();
            });*/
            Logger.LoggerMethod();
        }
        public static IServiceCollection AddRulesFromAssemblyContaining(this IServiceCollection services, Assembly assembly, Type type)
        {
            var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && t != type).ToList();
            foreach (var item in types)
                services.AddTransient(item);

            return services;
        }
    }
}