using CharityHub.Application.Base;
using CharityHub.Domain.Helpers;
using MediatR;

namespace CharityHub.Application.Services.Authentications.Commands.Handlers.SignIn
{
    public record SignInCommand : ICommand<BaseResponse<JwtAuthResponse>>
    {
        public string MobileNumber { get; set; } = null!;
        public string TOTP { get; set; } = null!;
    }
}
