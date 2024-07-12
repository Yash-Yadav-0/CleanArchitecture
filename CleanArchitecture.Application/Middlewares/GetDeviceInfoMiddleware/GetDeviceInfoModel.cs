
namespace CleanArchitecture.Application.Middlewares.GetDeviceInfoMiddleware
{
    public class GetDeviceInfoModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string DeviceType { get; set; }
        public string IpAddress { get; set; }
        public string Browser { get; set; }
        public DateTime LoggedInAt { get; set; }
    }
}
