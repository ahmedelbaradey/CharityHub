using CharityHub.Application.Base;
using MediatR;

namespace CharityHub.Application.Services.Accounts.Commands.Models
{
    public class DeleteAccountCommand : IRequest<BaseResponse<string>>
    {
        public int Id { get; set; }

    }
}
