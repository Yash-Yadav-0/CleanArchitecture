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
            return await HandleAsync(
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
        }
    }
}
