using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public string? Picture { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public string? CodeForResetPassword { get; set; }
        public DateTime? TimeOfCodeExpiration { get; set; }
        public bool? IsCodeOfResetPasswordTrue { get; set; }
    }
}
