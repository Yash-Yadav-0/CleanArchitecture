using Microsoft.AspNetCore.Http;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace CleanArchitecture.Infrastructure.Mail
{
    public class MailService : Application.Interfaces.Mail.IMailService
    {
        private readonly string _email;
        private readonly string _password;

        public MailService(IConfiguration config)
        {
            _email = config["EmailCredentials:username"];
            _password = config["EmailCredentials:password"];
        }
        public MailService()
        {
        }
        public async Task SendMailAsync(string to, string subject, string body, IFormFileCollection? attachments = null)
        {
            try
            {
                using (var client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(_email, _password);
                    client.UseDefaultCredentials = false;

                    var mailMessage = new MailMessage(_email, to, subject, body);
                    if (attachments != null)
                    {
                        foreach (var file in attachments)
                        {
                            using (var stream = file.OpenReadStream())
                            {
                                var attachment = new Attachment(stream, file.FileName);
                                mailMessage.Attachments.Add(attachment);
                            }
                        }
                    }

                    await client.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while sending mail: {@Message}", ex.Message);
            }
        }

        public async Task SendMailAsync(IList<string> tos, string subject, string body, IFormFileCollection? attachments = null)
        {
            foreach (var to in tos)
            {
                await SendMailAsync(to, subject, body, attachments);
            }
        }

        public async Task UserRegisteredEmailAsync(string email)
        {
            string subject = "Welcome to BuyNinja";
            string body = "User has been registered successfully to BuyNinja!";
            await SendMailAsync(email, subject, body);
        }

        public async Task UnAuthenticatedUserTriedToLoggInAsync(string email)
        {
            string subject = "Unauthenticated user login detected";
            string body = "Unknown person trying to login to BuyNinja!";
            await SendMailAsync(email, subject, body);
        }

        public async Task UserLoggedInEmailAsync(string email)
        {
            string subject = "Signed In to BuyNinja";
            string body = "New Login to BuyNinja! Try resetting the password if it was not you.";
            await SendMailAsync(email, subject, body);
        }
    }
}

