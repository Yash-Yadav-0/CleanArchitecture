using CleanArchitecture.Application.Features.Auth.Rules;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Application.Features.Auth.Commands.EmailConfirmation
{
    public class EmailConfirmationCommandHandler : IRequestHandler<EmailConfirmationCommandRequest, Unit>
    {
        private readonly UserManager<User> userManager;
        private readonly AuthRules authRules;

        public EmailConfirmationCommandHandler(UserManager<User> userManager, AuthRules authRules)
        {
            this.userManager = userManager;
            this.authRules = authRules;
        }
        public async Task<Unit> Handle(EmailConfirmationCommandRequest request, CancellationToken cancellationToken)
        {
            LoggerHelper.LogInformation("Handling EmailConfirmation for Email:{Email}", request.Email);
            User? user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                LoggerHelper.LogError("User not found for Email: {Email}",
                    new Exception("user not found"),
                    request.Email);
                throw new Exception("User not found");
            }
            var decodedToken = EncoderHelper.UrlDecoder(request.Token);
            var result = await userManager.ConfirmEmailAsync(user, decodedToken);
            if (result.Succeeded)
            {
                LoggerHelper.LogInformation("Email confirmation succeded for Email:{Email}", request.Email);
                await authRules.EmailShouldBeConfirmedAsync(result);
            }
            return Unit.Value;
        }
    }
}
