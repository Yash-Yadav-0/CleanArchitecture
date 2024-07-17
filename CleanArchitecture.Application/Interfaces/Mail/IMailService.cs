using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Application.Interfaces.Mail
{
    public interface IMailService
    {
        Task SendMailAsync(string to, string subject, string body, IFormFileCollection? files = null);
        Task SendMailAsync(IList<string> tos, string subject, string body, IFormFileCollection? files = null);
    }
}
