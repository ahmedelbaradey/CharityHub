using CharityHub.Application.Base;
using CharityHub.Domain.Helpers;

namespace CharityHub.Application.Services.Authorizations.Commands.Handlers.EditUserRoles
{
    public class EditUserRolesCommand : EditUserRolesRequest, ICommand<BaseResponse<string>>
    {

    }
}
