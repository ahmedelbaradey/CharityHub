using CharityHub.Application.Base;
using CharityHub.Domain.Helpers;
using CharityHub.DomainService.Abstractions.Logger;
using CharityHub.DomainService.Abstractions.Services;

namespace CharityHub.Application.Services.Authentications.Commands.Handlers.SignIn
{
    public class SignInCommandHandler : BaseResponseHandler,ICommandHandler<SignInCommand, BaseResponse<JwtAuthResponse>>
    {
        #region Fileds
        private readonly IAuthenticationService _authenticationService;
        private readonly IAccountService _accountService;
        private readonly ILoggerManager _logger;
        #endregion

        #region Constructors
        public SignInCommandHandler( IAuthenticationService authenticationService,IAccountService accountService, ILoggerManager logger )
        {
            
            _authenticationService = authenticationService;
            _accountService = accountService;   
           _logger = logger;    
        }
        #endregion

        #region Functions
        public async Task<BaseResponse<JwtAuthResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _accountService.GetAccountByMobileNumber(request.MobileNumber);
                if (user == null)
                    return NotFound<JwtAuthResponse>("User with this username not found!");

                var signInResult = await _authenticationService.SignIn(request.MobileNumber,request.TOTP);
                if (!signInResult)
                {
                    return BadRequest<JwtAuthResponse>("TOTP is't correct.");
                }
                var accessToken = await _authenticationService.GetJwtToken(user);
                return Success(accessToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: in EditAccoutCommand");
                return ServerError<JwtAuthResponse>(ex.Message);
            }
        }
        #endregion

    }
}
