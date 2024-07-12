using Microsoft.AspNetCore.Builder;

namespace CleanArchitecture.Application.Middlewares.GetDeviceInfoMiddleware
{
    public static class GetDeviceInfosHandleConfiguration
    {
        public static void GetDeviceInfoHandleConfiguration(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<GetDeviceInfoMiddleware>();
        }
    }
}
