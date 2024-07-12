namespace CleanArchitecture.Application.Features.Auth.Queries.GetAllUsers
{
    public class GetAllUsersQueryResponse
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public DateTime? TimeOfCodeExpiration { get; set; }
    }
}
