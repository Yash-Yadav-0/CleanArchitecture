using CleanArchitecture.Application.Features.Auth.Rules;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Application.Features.Auth.Commands.ResetPassword.ConfirmResetPassword
{
    public class ConfirmResetPasswordCommandHandler : IRequestHandler<ConfirmResetPasswordCommandRequest, Unit>
    {
        private readonly UserManager<User> userManager;
        private readonly AuthRules authRules;

        public ConfirmResetPasswordCommandHandler(UserManager<User> userManager, AuthRules authRules)
        {
            this.userManager = userManager;
            this.authRules = authRules;
        }
        public async Task<Unit> Handle(ConfirmResetPasswordCommandRequest request, CancellationToken cancellationToken)
        {

            User? user = await userManager.FindByEmailAsync(request.Email);

            await authRules.UserShouldBeFoundAsync(user);

            if (user.IsCodeOfResetPasswordTrue == false)
                await authRules.NotAllowedResetPasswordBeforeConfirmAsync();

            await userManager.RemovePasswordAsync(user);

            if (!await userManager.HasPasswordAsync(user))
            {
                await userManager.AddPasswordAsync(user, request.Password);
                await userManager.UpdateAsync(user);
            }
            return Unit.Value;
        }
    }
}
