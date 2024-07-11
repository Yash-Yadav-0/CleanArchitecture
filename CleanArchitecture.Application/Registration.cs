using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Behaviors;
using CleanArchitecture.Application.Features.Auth.Commands.EmailConfirmation;
using CleanArchitecture.Application.Features.Orders.Rules;
using CleanArchitecture.Application.Features.Products.Rules;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Application.Middlewares.ExceptionMiddleware;
using CleanArchitecture.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application
{
    public static class Regestration
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var assemlty = Assembly.GetExecutingAssembly();
            services.AddMediatR
                (
                config => config.RegisterServicesFromAssembly(assemlty)
                );

            services.AddTransient<ExceptionMiddleware>();

            services.AddValidatorsFromAssembly(assemlty);

            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en-US");

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehaviors<,>));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RedisCacheBehaviors<,>));

            #region Url
            //services.AddHttpContextAccessor();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();


            services.AddScoped<UrlFactoryHelper>();

            services.AddSingleton<EmailConfirmationCommandRequest>();

            #endregion

            services.AddTransient<ProductRules>();

            services.AddTransient<OrderRules>();


            services.AddTransient<SignInManager<User>>();

            services.AddRulesFromAssemblyContaining(assemlty, typeof(BaseRule));

            //services.AddControllers()
            //            .AddJsonOptions(config =>
            //                config.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
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
