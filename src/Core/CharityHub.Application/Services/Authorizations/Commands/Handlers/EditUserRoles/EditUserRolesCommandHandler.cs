using CharityHub.Application.Base;
using CharityHub.Application.Abstracts.Logger;
using CharityHub.DomainService.Abstractions.Services;

namespace CharityHub.Application.Services.Authorizations.Commands.Handlers.EditUserRoles
{
    public class RoleCommandHandler : BaseResponseHandler,ICommandHandler<EditUserRolesCommand, BaseResponse<string>>
    {
        #region Fileds
        private readonly IAuthorizationService _authorizationService;
        private readonly ILoggerManager _logger;
        #endregion

        #region Constructors
        public RoleCommandHandler(IAuthorizationService authorizationService , ILoggerManager logger)
        {
            _authorizationService = authorizationService;
            _logger = logger;
        }
        #endregion

        #region Functions
        public async Task<BaseResponse<string>> Handle(EditUserRolesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _authorizationService.UpdateUserRoles(request);
                switch (result)
                {
                    case "UserNotFound":
                        return NotFound<string>("user with this id not found");
                    case "FailedDeleted":
                        return BadRequest<string>("Deleted Operation Failed.");
                    case "FailedAdded":
                        return Success("Added Operation Failed.");
                }

                return Success("Updated Operation Successfully.");
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
