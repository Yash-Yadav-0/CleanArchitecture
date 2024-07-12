using CleanArchitecture.Application.Features.Auth.Rules;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Application.Features.Auth.Commands.ResetPassword.ISCodeForResetPassword
{
    public class ISCodeForResetPasswordCommandHandler : IRequestHandler<ISCodeForResetPasswordCommandRequest, Unit>
    {
        private readonly UserManager<User> userManager;
        private readonly AuthRules authRules;

        public ISCodeForResetPasswordCommandHandler(UserManager<User> userManager, AuthRules authRules)

        {
            this.userManager = userManager;
            this.authRules = authRules;
        }

        public async Task<Unit> Handle(ISCodeForResetPasswordCommandRequest request, CancellationToken cancellationToken)
        {
            User? user = await userManager.FindByEmailAsync(request.Email);

            await authRules.UserShouldBeFoundAsync(user);

            if (request.CodeOfConfirm != user.CodeForResetPassword)
            {
                user.IsCodeOfResetPasswordTrue = false;
                await authRules.BothOfCodeShouldBeIdenticalAsync();
            }


            user.IsCodeOfResetPasswordTrue = true;

            await userManager.UpdateAsync(user);

            return Unit.Value;
        }
    }
}
