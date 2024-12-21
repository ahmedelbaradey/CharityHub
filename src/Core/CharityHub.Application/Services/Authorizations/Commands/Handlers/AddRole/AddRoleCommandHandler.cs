using CharityHub.Application.Base;
using CharityHub.Application.Abstracts.Logger;
using CharityHub.DomainService.Abstractions.Services;

namespace CharityHub.Application.Services.Authorizations.Commands.Handlers.AddRole
{
    public class AddRoleCommandHandler : BaseResponseHandler,ICommandHandler<AddRoleCommand, BaseResponse<string>>
    {
        #region Fileds
        private readonly IAuthorizationService _authorizationService;
        private readonly ILoggerManager _logger;
        #endregion

        #region Constructors
        public AddRoleCommandHandler(IAuthorizationService authorizationService , ILoggerManager logger)
        {
            _authorizationService = authorizationService;
            _logger = logger;   
        }
        #endregion

        #region Functions
        public async Task<BaseResponse<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var isExist = await _authorizationService.IsRoleNameExist(request.roleName);
                if (isExist)
                    return BadRequest<string>("this role name is already exist.");

                var IsAdded = await _authorizationService.AddRoleAsync(request.roleName);

                if (!IsAdded) return BadRequest<string>("Added operation failed.");

                return Success("Added Operation Successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ServerError<string>(ex.Message);
            }
        }
        #endregion

    }
}
