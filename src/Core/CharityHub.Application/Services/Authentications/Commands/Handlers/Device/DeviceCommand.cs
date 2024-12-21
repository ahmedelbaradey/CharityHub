using CharityHub.Application.Base;
using CharityHub.Domain.Helpers;
using MediatR;

namespace CharityHub.Application.Services.Authentications.Commands.Handlers.Device
{
    public record DeviceCommand : ICommand<BaseResponse<JwtAuthResponse>>
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
