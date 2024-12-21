using CharityHub.Domain.Entities.Identities;
using CharityHub.Domain.Helpers;
using CharityHub.DomainService.Abstractions.Logger;
using CharityHub.DomainService.Abstractions.Repository;
using CharityHub.DomainService.Abstractions.Services;
using CustomerRegisterationFlow.Infrastructure.OTP;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CharityHub.DomainService.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fileds
         
        private readonly JwtSettings _jwtSettings;
        private readonly IGenericRepository<Device> _deviceRepository;
        private readonly IGenericRepository<Role> _roleRepository;
        private readonly IGenericRepository<Account> _accountRepository;
        private readonly ILoggerManager _logger;

        #endregion

        #region Constructors

        public AuthenticationService(JwtSettings jwtSettings, IGenericRepository<Device> deviceRepository,ILoggerManager logger, IGenericRepository<Role> roleRepository, IGenericRepository<Account> accountRepository)
        {       
            _jwtSettings = jwtSettings;
            _deviceRepository = deviceRepository;
            _logger = logger;
            _roleRepository = roleRepository;   
            _accountRepository = accountRepository; 
        }

        #endregion

        #region Main_Functions
        public async Task<JwtAuthResponse> GetJwtToken(Account user)
        {
            try
            {


                //Generate Jwt Token..
                var (jwtToken, accessToken) = await GenerateJwtToken(user);

                //Generate Resfresh Token & Save To Database
                var refreshToken = GetRefreshToken(user.MobileNumber!);
                var userdeviceToken = new Device
                {
                    AddedTime = DateTime.UtcNow,
                    ExpiryDate = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpireDate),
                    IsUsed = true,
                    IsRevoked = false,
                    JwtId = jwtToken.Id,
                    RefreshToken = refreshToken.TokenString,
                    FCMToken = accessToken,
                    UserId = user.Id
                };
                await _deviceRepository.AddAsync(userdeviceToken);

                //return response
                var response = new JwtAuthResponse
                {
                    AccessToken = accessToken,
                    refreshToken = refreshToken
                };

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogDebug( "Error in GetJwtToken");
                throw;
            }
        }

        public async Task<bool> SignIn(string mobileNumber, string tOTP)
        {
            try
            {
                return TOTP.ValidateTOTP(tOTP);
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Error in GetJwtToken");
                throw;
            }
        }

        public async Task<JwtAuthResponse> GetRefreshToken(Account user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken)
        {
            try
            {
                var (jwtSecurityToken, newToken) = await GenerateJwtToken(user);

                var refreshTokenResult = new RefreshToken();
                refreshTokenResult.MobileNumber = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimsModel.MobileNumber))!.Value;
                refreshTokenResult.TokenString = refreshToken;
                refreshTokenResult.ExpireAt = (DateTime)expiryDate;

                var response = new JwtAuthResponse();
                response.AccessToken = newToken;
                response.refreshToken = refreshTokenResult;

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogDebug( "Error in GetRefreshToken");
                throw;
            }

        }


        public JwtSecurityToken ReadJwtToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }
            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);
            return response;
        }

        public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshTken)
        {
            try
            {

                if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
                {
                    return ("AlgorithmIsWrong", null);
                }

                if (jwtToken.ValidTo > DateTime.UtcNow)
                {
                    return ("TokenIsRunning", null);
                }

                //Get UserId From Glaims in jwtToken
                var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == "Id")!.Value;

                var userRefreshtoken = await _deviceRepository.GetTableNoTracking().FirstOrDefaultAsync(x => x.FCMToken == accessToken && x.RefreshToken == refreshTken && x.UserId == int.Parse(userId));

                if (userRefreshtoken == null)
                {
                    return ("RefreshTokenNotFound", null);
                }

                if (userRefreshtoken.ExpiryDate < DateTime.UtcNow)
                {
                    userRefreshtoken.IsRevoked = true;
                    userRefreshtoken.IsUsed = false;
                    await _deviceRepository.UpdateAsync(userRefreshtoken);
                    return ("RefreshTokenIsExpired", null);
                }

                var expiryDate = userRefreshtoken.ExpiryDate;
                return (userId, expiryDate);
            }
            catch (Exception ex)
            {
                _logger.LogDebug( "Error in ValidateDetails");
                throw;
            }
        }

        public async Task<string> ValidateToken(string accessToken)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var parameters = new TokenValidationParameters
                {
                    ValidateIssuer = _jwtSettings.ValidateIssure,
                    ValidIssuers = new[] { _jwtSettings.Issure },
                    ValidateIssuerSigningKey = _jwtSettings.ValidateIssureSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                    ValidateAudience = _jwtSettings.validateAudience,
                    ValidAudience = _jwtSettings.Audience,
                    ValidateLifetime = _jwtSettings.ValidateLifeTime
                };

                var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);
                if (validator == null)
                {
                    return "InvalidToken";
                }

                return "NotExpired";
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Error in ValidateToken");
                throw;
            }
        }
        #endregion

        #region Sub-Functions

        private async Task<(JwtSecurityToken, string)> GenerateJwtToken(Account user)
        {
            var roles = await _roleRepository.FindByCondition(c => c.AccountRoles.Any(x => x.AccountId.Equals(user.Id)), trackChanges: false).ToListAsync();


            var Claims = GetClaims(user, roles);

            var jwtToken = new JwtSecurityToken(_jwtSettings.Issure, _jwtSettings.Audience,
                claims: Claims, expires: DateTime.UtcNow.AddDays(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII
                .GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return (jwtToken, accessToken);
        }
        private List<Claim> GetClaims(Account user, IReadOnlyList<Role> roles)
        {
            //Add some properties to claims...
            var claims = new List<Claim>()
            {
            
                new Claim(nameof(UserClaimsModel.MobileNumber),user.MobileNumber),
                new Claim(nameof(UserClaimsModel.Id),user.Id.ToString())
            };

            //Add roles to claims...
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            } 

            return claims;
        }

        private RefreshToken GetRefreshToken(string mobileNumber)
        {
            var refreshToken = new RefreshToken
            {
                ExpireAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpireDate),
                MobileNumber = mobileNumber,
                TokenString = GenerateRefreshToken()
            };

            return refreshToken;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            var randomNumberGenerate = RandomNumberGenerator.Create();
            randomNumberGenerate.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        #endregion

    }
}
