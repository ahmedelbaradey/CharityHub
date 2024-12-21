using CharityHub.Application.Base;
using CharityHub.Domain.Entities.Identities;
using CharityHub.Domain.Helpers;
using Microsoft.AspNetCore.Identity;
using CharityHub.Application.Services.Authentications.Commands.Handlers.Device;
using CharityHub.DomainService.Abstractions.Services;

namespace CharityHub.Application.Services.Authentications.Commands.Handlers
{
    public class DeviceCommandHandler : BaseResponseHandler,ICommandHandler<DeviceCommand, BaseResponse<JwtAuthResponse>>
    {
        #region Fileds
        private readonly IAuthenticationService _authenticationService;
        private readonly IAccountService _accountService;
        #endregion

        #region Constructors
        public DeviceCommandHandler( IAuthenticationService authenticationService, IAccountService accountService)
        {
            _authenticationService = authenticationService;
            _accountService = accountService;
        }
        #endregion

        #region Functions
        public async Task<BaseResponse<JwtAuthResponse>> Handle(DeviceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var jwtToken = _authenticationService.ReadJwtToken(request.AccessToken);
                var userIdAndExpiryDate = await _authenticationService.ValidateDetails(jwtToken,request.AccessToken, request.RefreshToken);

                switch (userIdAndExpiryDate)
                {
                    case ("AlgorithmIsWrong", null):
                        return Unauthorized<JwtAuthResponse>("Algorithm is wrong.");

                    case ("TokenIsRunning", null):
                        return Unauthorized<JwtAuthResponse>("Toekn is not expired.");

                    case ("RefreshTokenNotFound", null):
                        return Unauthorized<JwtAuthResponse>("Refresh token not found.");

                    case ("RefreshTokenIsExpired", null):
                        return Unauthorized<JwtAuthResponse>("Refresh token is expired.");
                }

                var (userId, ExpiryDate) = userIdAndExpiryDate;

                var user = await _accountService.GetAccountById(Convert.ToInt32(userId));
                if (user == null)
                    return NotFound<JwtAuthResponse>("User not found!");

                var result = await _authenticationService.GetRefreshToken(user, jwtToken, ExpiryDate, request.RefreshToken);
                return Success(result);
            }
            catch (Exception ex)
            {
                return ServerError<JwtAuthResponse>(ex.Message);
            }
        }
        #endregion


    }
}
