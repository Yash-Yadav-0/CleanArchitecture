using CleanArchitecture.Application.Features.Auth.Queries.GetAllUsers;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.ScheduleServices
{
    public class CheckTimeOfSendingUsersCodeScheduleService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        public CheckTimeOfSendingUsersCodeScheduleService(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        //[AutomaticRetry(Attempts = 3)]
        public async Task CheckTimeOfSendingUsersAsync()
        {
            using var resource = serviceScopeFactory.CreateScope();

            var mediatR = resource.ServiceProvider.GetRequiredService<IMediator>();

            var userManager = resource.ServiceProvider.GetRequiredService<UserManager<User>>();

            var mapper = resource.ServiceProvider.GetRequiredService<IMapper>();

            try
            {
                var userQueryResponse = await mediatR.Send(new GetAllUsersQueryRequest());

                var users = userQueryResponse.Select(x => new User
                {
                    Id = x.UserId,
                    Email = x.Email,
                    TimeOfCodeExpiration = x.TimeOfCodeExpiration,
                }).ToList();

                /*Task<IList<GetAllUsersQueryResponse>> getAllUsersQueryResponsesTask = mediatR.Send(new GetAllUsersQueryRequest());

                IList<GetAllUsersQueryResponse> GetAllUsersQueryResponse = await getAllUsersQueryResponsesTask;

                List<User> users = GetAllUsersQueryResponse.Select(x => new User() { Id = x.UserId, Email = x.Email, TimeOfCodeExpiration = x.TimeOfCodeExpiration }).ToList();*/

                foreach (var user in users)
                {
                    if (user.TimeOfCodeExpiration < DateTime.UtcNow)
                    {
                        
                        User _user = await userManager.FindByEmailAsync(user.Email);
                        if (_user != null)
                        {
                            _user.CodeForResetPassword = null;
                            _user.IsCodeOfResetPasswordTrue = null;
                            _user.TimeOfCodeExpiration = null;
                            await userManager.UpdateAsync(_user);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occured: {ex.Message}");
            }
        }
    }
}
