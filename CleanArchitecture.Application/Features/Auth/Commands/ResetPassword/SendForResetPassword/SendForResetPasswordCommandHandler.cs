﻿using CleanArchitecture.Application.Features.Auth.Rules;
using CleanArchitecture.Application.Interfaces.Mail;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Application.Features.Auth.Commands.ResetPassword.SendForResetPassword
{
    public class SendForResetPasswordHandler : IRequestHandler<SendForResetPasswordCommandsRequest, Unit>
    {
        private readonly UserManager<User> userManager;
        private readonly AuthRules authRules;
        private readonly IMailService mailService;

        public SendForResetPasswordHandler(UserManager<User> userManager, AuthRules authRules, IMailService mailService)

        {
            this.userManager = userManager;
            this.authRules = authRules;
            this.mailService = mailService;
        }

        public async Task<Unit> Handle(SendForResetPasswordCommandsRequest request, CancellationToken cancellationToken)
        {
            User? user = await userManager.FindByEmailAsync(request.Email);

            await authRules.UserShouldBeFoundAsync(user);

            Random random = new Random();

            const string stringForGenerateCode = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz01234567899";

            user.CodeForResetPassword = new string(Enumerable.Range(0, 7).Select(index => stringForGenerateCode[random.Next(0, stringForGenerateCode.Length - 1)]).ToArray());

            user.TimeOfCodeExpiration = DateTime.UtcNow.AddMinutes(1);

            IdentityResult result = await userManager.UpdateAsync(user);

            await authRules.UserManagerShouldBeUpdatedAsync(result);

            #region Html body
            string emailBody = GenerateEmailBody(user.CodeForResetPassword, user.FullName);
            #endregion
            await mailService.SendMailAsync(user.Email, "Code For Reset Password", emailBody);
            return Unit.Value;

        }
        static string GenerateEmailBody(string resetCode, string name)
        {
            return $@"
        <html>
        <body>
            <h2>Password Reset Request</h2>
            <p>Dear {name},</p>
            <p>We received a request to reset your password. Please use the following code to reset your password:</p>
            <h3>{resetCode}</h3>
            <p>If you did not request a password reset, please ignore this email.</p>
            <p>Thank you !!</p>
        </body>
        </html>";
        }
    }
}
