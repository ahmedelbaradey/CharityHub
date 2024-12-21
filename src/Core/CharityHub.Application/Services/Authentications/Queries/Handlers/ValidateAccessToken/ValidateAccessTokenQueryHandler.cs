using CharityHub.Application.Base;
using CharityHub.DomainService.Abstractions.Logger;
using CharityHub.DomainService.Abstractions.Services;

namespace CharityHub.Application.Services.Authentications.Queries.Handlers.ValidateAccessToken
{
    public class ValidateAccessTokenQueryHandler : BaseResponseHandler, IQueryHandler<AccessTokenQuery, BaseResponse<string>>
    {
        #region Fields
        private readonly IAuthenticationService _authenticationService;
        private readonly ILoggerManager _logger;
        #endregion

        #region Constructors
        public ValidateAccessTokenQueryHandler(IAuthenticationService authenticationService, ILoggerManager logger)
        {
            _authenticationService = authenticationService;
            _logger = logger;
        }
        #endregion

        #region Functions
        public async Task<BaseResponse<string>> Handle(AccessTokenQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _authenticationService.ValidateToken(request.Accesstoken);
                if (result == "NotExpired")
                {
                    return Success("this token is not expired.");
                }

                return Unauthorized<string>("this token is expired.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: in ValidateAccessTokenQuery");
                return ServerError<string>(ex.Message);
            }
        }
        #endregion
    }
}
