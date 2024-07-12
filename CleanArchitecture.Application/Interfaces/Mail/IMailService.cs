using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Interfaces.Mail
{
    public interface IMailService
    {
        Task SendMessageAsync(string to, string subject, string body, IFormFileCollection? files = null);
        Task SendMessageAsync(IList<string> tos, string subject, string body, IFormFileCollection? files = null);
    }
}
