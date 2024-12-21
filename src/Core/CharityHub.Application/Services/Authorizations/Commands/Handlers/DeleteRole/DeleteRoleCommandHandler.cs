using CharityHub.Application.Base;
using CharityHub.Application.Abstracts.Logger;
using CharityHub.DomainService.Abstractions.Services;

namespace CharityHub.Application.Services.Authorizations.Commands.Handlers.DeleteRole
{
    public class RoleCommandHandler : BaseResponseHandler,ICommandHandler<DeleteRoleCommand, BaseResponse<string>>
    {
        #region Fileds
        private readonly IAuthorizationService _authorizationService;
        private readonly ILoggerManager _logger;
        #endregion

        #region Constructors
        public RoleCommandHandler(IAuthorizationService authorizationService,ILoggerManager logger)
        {
            _authorizationService = authorizationService;
            _logger = logger;
        }
        #endregion

        #region Functions
        public async Task<BaseResponse<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var role = await _authorizationService.GetRoleByID(request.Id);
                if (role == null)
                    return NotFound<string>("role with this id not found!");

                var IsDeleted = await _authorizationService.DeleteRoleById(role);
                if (!IsDeleted)
                    return BadRequest<string>("Deleted Operation Failed.");

                return Success("Deleted Operation Successfully.");
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
