using CharityHub.Application.Base;

namespace CharityHub.Application.Services.Authorizations.Commands.Handlers.EditRole
{
    public record EditRoleCommand : ICommand<BaseResponse<string>>
    {
        public int Id { get; set; }
        public string roleName { get; set; } = null!;
    }
}
