using CleanArchitecture.Application.Interfaces.Tokens;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CleanArchitecture.Infrastructure.Tokens
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<User> userManager;
        private readonly TokenSettings tokenSettings;
        public TokenService(UserManager<User> userManager, IOptions<TokenSettings> options)
        {
            this.userManager = userManager;
            tokenSettings = options.Value;
        }
        public async Task<JwtSecurityToken> CreateToken(User user, IList<string> roles)
        {
            #region claims
            IList<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes. NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            #endregion

            #region  sign credintial

            SecurityKey Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.SecretKey));

            SigningCredentials credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            #endregion

            #region Token

            JwtSecurityToken token = new JwtSecurityToken
                (
                audience: tokenSettings.Audience,
                issuer: tokenSettings.Issuer,
                claims: claims,
                signingCredentials: credentials,
                expires: DateTime.UtcNow.AddMinutes(tokenSettings.TokenValidityInMinutes)
                );

            #endregion

            await userManager.AddClaimsAsync(user, claims);

            return token;
        }
        public string GenerateRefreshToken()
        {
            var RandomNumber = new byte[64];
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(RandomNumber);
            return Convert.ToBase64String(RandomNumber);
        }

        public ClaimsPrincipal? GetClaimsPrincipalFromExpiredToken(string? token)
        {
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.SecretKey)),

            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            var principle = handler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken
                ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("");
            return principle;
        }
    }
}
