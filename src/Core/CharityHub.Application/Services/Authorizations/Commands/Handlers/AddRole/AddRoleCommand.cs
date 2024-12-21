using CharityHub.Application.Base;

namespace CharityHub.Application.Services.Authorizations.Commands.Handlers.AddRole
{
    public record AddRoleCommand : ICommand<BaseResponse<string>>
    {
        public string roleName { get; set; } = null!;
    }
}
