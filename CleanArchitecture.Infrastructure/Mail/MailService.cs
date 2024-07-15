using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Infrastructure.Mail
{
    public class MailService : Application.Interfaces.Mail.IMailService
    {
        private readonly MailSettings mailSettings;
        public MailService(IOptions<MailSettings> configuration)
        {
            mailSettings = configuration.Value;
        }
        public async Task SendMessageAsync(string to, string subject, string body, IFormFileCollection? files = null)
        {
            await SendMessageAsync(new List<string> { to }, subject, body, files);
        }
        public async Task SendMessageAsync(IList<string> tos, string subject, string body, IFormFileCollection? files = null)
        {
            MimeMessage email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(mailSettings.Email));
            foreach (var emailTo in tos)
            {
                email.To.Add(MailboxAddress.Parse(emailTo));
            }
            email.Subject = subject;

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;

            if (files?.Count > 0)
            {
                foreach (var file in files)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        bodyBuilder.Attachments.Add(file.FileName, memoryStream.ToArray(), ContentType.Parse(file.ContentType));
                    }
                }
            }

            email.Body = bodyBuilder.ToMessageBody();

            using (var smtpClient = new SmtpClient())
            {
                await smtpClient.ConnectAsync(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);
                await smtpClient.AuthenticateAsync(mailSettings.Email, mailSettings.Password);
                await smtpClient.SendAsync(email);
                await smtpClient.DisconnectAsync(true);
            }
        }
    }
}

