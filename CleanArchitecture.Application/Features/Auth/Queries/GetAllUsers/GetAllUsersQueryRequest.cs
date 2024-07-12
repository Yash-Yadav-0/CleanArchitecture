using MediatR;

namespace CleanArchitecture.Application.Features.Auth.Queries.GetAllUsers
{
    public class GetAllUsersQueryRequest: IRequest<IList<GetAllUsersQueryResponse>>
    {
    }
}
