using CharityHub.Application.Base;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CharityHub.Application.Services.Authentications.Queries.Handlers.ValidateAccessToken
{
    public record AccessTokenQuery : IQuery<BaseResponse<string>>
    {
        public string Accesstoken { get; set; } = null!;
    }
}
