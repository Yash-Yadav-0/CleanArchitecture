using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Features.Auth.Exceptions;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;


namespace CleanArchitecture.Application.Features.Auth.Rules
{
    public class AuthRules : BaseRule
    {
        public Task UserShouldnotBeExistsAsync(User? user)
        {
            if (user is not null)
                throw new UserAlreadyExistsException("This User has registered before, use another Values");
            return Task.CompletedTask;
        }
        public Task EmailOrPasswordShouldnotbeInvalidAsync(User user, bool checkPassword)
        {
            if (user is null || !checkPassword)
                throw new EmailOrPasswordShouldnotbeInvalidException("This User has registered before, use another Values");
            return Task.CompletedTask;
        }

        public Task RefreshTokenExpiryTimeShouldnotbeExpiredAsync(DateTime? RefreshTokenExpiryTime)
        {
            if (RefreshTokenExpiryTime < DateTime.Now)
                throw new RefreshTokenExpiryTimeShouldnotbeExpiredAsyncException();
            return Task.CompletedTask;
        }

        public Task EmailShouldnotbeInvalidAsync(User user)
        {
            if (user is null)
                throw new EmailShouldnotbeInvalidException("This User has registered before, use another Values");
            return Task.CompletedTask;
        }

        public Task EmailShouldBeConfirmedAsync(IdentityResult identityResult)
        {
            if (!identityResult.Succeeded || identityResult is null)
                throw new EmailShouldBeConfirmedException("Your Email is`t confirmed,\n\tyou must confirm it for continue");
            return Task.CompletedTask;
        }

        public Task NoMistakeShouldHappenWhileEmailConfirmationAsync(bool value)
        {
            if (!value)
                throw new NoMistakeShouldHappenWhileEmailConfirmationException("there is Some Thing Wrong While Email Confirmation Process\n\tPlease try again😊");
            return Task.CompletedTask;
        }
        public Task UserShouldBeFoundAsync(User user)
        {
            if (user is null)
                throw new UserShouldBeFoundException("User isn`t exist");
            return Task.CompletedTask;
        }

        public Task UserManagerShouldBeUpdatedAsync(IdentityResult identityResult)
        {
            if (!identityResult.Succeeded || identityResult is null)
                throw new UserManagerShouldBeUpdatedException("Your Email is`t Updated,\n\tyou must try again for continue");
            return Task.CompletedTask;
        }

        public Task BothOfCodeShouldBeIdenticalAsync()
        {
            throw new BothOfCodeShouldBeIdenticalException("Both Of Code Should Be Identical,\n\tyou must try again for continue");
        }
        public Task NotAllowedResetPasswordBeforeConfirmAsync()
        {
            throw new NotAllowedResetPasswordBeforeConfirmException("Not Allowed Reset Password Before Confirm,\n\tyou must try again for continue");
        }
    }
}
