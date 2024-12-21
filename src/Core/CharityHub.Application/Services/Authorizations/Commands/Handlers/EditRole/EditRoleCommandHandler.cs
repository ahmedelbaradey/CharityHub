using CharityHub.Application.Base;
using CharityHub.Application.Abstracts.Logger;
using CharityHub.DomainService.Abstractions.Services;

namespace CharityHub.Application.Services.Authorizations.Commands.Handlers.EditRole
{
    public class RoleCommandHandler : BaseResponseHandler,ICommandHandler<EditRoleCommand, BaseResponse<string>>
    {
        #region Fileds
        private readonly IAuthorizationService _authorizationService;
        private readonly ILoggerManager _logger;
        #endregion

        #region Constructors
        public RoleCommandHandler(IAuthorizationService authorizationService ,  ILoggerManager logger)
        {
            _authorizationService = authorizationService;
            _logger = logger;
        }
        #endregion

        #region Functions
 

        public async Task<BaseResponse<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var isExist = await _authorizationService.IsRoleNameExist(request.roleName);
                if (isExist)
                    return BadRequest<string>("this role name is already exist.");

                var IsEdited = await _authorizationService.EditRoleById(request.Id, request.roleName);

                if (!IsEdited) return BadRequest<string>("Edited operation failed.");

                return Success("Edited Operation Successfully.");
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
