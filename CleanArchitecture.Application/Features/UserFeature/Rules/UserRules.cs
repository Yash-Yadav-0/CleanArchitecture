using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Features.Auth.Exceptions;
using CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.UserFeature.Rules
{
    public class UserRules : BaseRule
    {
        public Task UserShouldnotBeExistsAsync(User? user)
        {
            if (user is not null)
                throw new UserAlreadyExistsException("This User has registered before, use another Values");
            return Task.CompletedTask;
        }
    }
}
