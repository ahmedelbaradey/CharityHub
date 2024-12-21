using CharityHub.Application.Base;

namespace CharityHub.Application.Services.Authorizations.Commands.Handlers.DeleteRole
{
    public record DeleteRoleCommand : ICommand<BaseResponse<string>>
    {
        public int Id { get; set; }
    }
}
