using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Mapper
{
    public static class Registration
    {
        public static void AddCustomeMapper(this IServiceCollection services)
        {
            services.AddSingleton<Application.Interfaces.AutoMapper.IMapper, AutoMapper.Mapper>();
        }
    }
}
