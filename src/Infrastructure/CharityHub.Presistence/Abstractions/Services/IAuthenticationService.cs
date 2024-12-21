using CharityHub.Domain.Entities.Identities;
using CharityHub.Domain.Helpers;
using System.IdentityModel.Tokens.Jwt;

namespace CharityHub.DomainService.Abstractions.Services
{
    public interface IAuthenticationService
    {
        public Task<JwtAuthResponse> GetJwtToken(Account user);
        public JwtSecurityToken ReadJwtToken(string accessToken);
        public Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshTken);
        public Task<JwtAuthResponse> GetRefreshToken(Account user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken);
        public Task<string> ValidateToken(string accessToken);
    }
}
