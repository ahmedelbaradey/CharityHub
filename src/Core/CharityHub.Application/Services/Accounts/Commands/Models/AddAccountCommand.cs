using CharityHub.Application.Base;
using MediatR;

namespace CharityHub.Application.Services.Accounts.Commands.Models
{
    public record AddAccountCommand : ICommand<BaseResponse<string>>
    {
        public string MobileNumber { get; set; }
        public string FullName { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime JoinedDate { get; set; }
    }
}
