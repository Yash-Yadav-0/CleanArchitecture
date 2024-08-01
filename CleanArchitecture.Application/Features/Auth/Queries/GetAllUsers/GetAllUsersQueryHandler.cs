using CleanArchitecture.Application.Helpers;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Application.Features.Auth.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : 
            SqlFunctionHandler<GetAllUsersQueryRequest, GetAllUsersQueryResponse>,
            IRequestHandler<GetAllUsersQueryRequest, IList<GetAllUsersQueryResponse>>
    {
        public GetAllUsersQueryHandler(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IList<GetAllUsersQueryResponse>> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
        {
            LoggerHelper.LogInformation("Handling GetAllUsersQueryRequest");

            IList<GetAllUsersQueryResponse> users;
            try
            {
                users = await HandleAsync(
                    request,
                    "get_all_users",
                    reader => new GetAllUsersQueryResponse
                    {
                        UserId = reader.GetGuid(0),
                        Email = reader.GetString(1),
                        TimeOfCodeExpiration = reader.IsDBNull(2) ? (DateTime?)null : reader.GetDateTime(2)
                    },
                    cancellationToken
                );

                LoggerHelper.LogInformation("Successfully retrieved {UserCount} users", users.Count);
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError("Error occurred while handling GetAllUsersQueryRequest",ex);
                throw; // Re-throw the exception after logging it
            }

            return users;
        }
    }
}
