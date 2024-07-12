﻿using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Features.Auth.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQueryRequest, IList<GetAllUsersQueryResponse>>
    {
        private readonly UserManager<User> userManager;
        public GetAllUsersQueryHandler(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IList<GetAllUsersQueryResponse>> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
        {
            IList<User> users = await userManager.Users.ToListAsync(cancellationToken);

            IList<GetAllUsersQueryResponse> getAllUsers = users.Select(x => new GetAllUsersQueryResponse
            {
                Email = x.Email,
                TimeOfCodeExpiration = x.TimeOfCodeExpiration,
                UserId = x.Id
            }).ToList();
            return getAllUsers;
        }
    }
}
