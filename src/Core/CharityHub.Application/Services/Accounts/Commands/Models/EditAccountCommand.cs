using CharityHub.Application.Base;
using CharityHub.Domain.Entities.Identities;
using MediatR;

namespace CharityHub.Application.Services.Accounts.Commands.Models
{
    public record EditAccountCommand : ICommand<BaseResponse<string>>
    {
        public int Id { get; set; }
        public string MobileNumber { get; set; }
        public string FullName { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime JoinedDate { get; set; }
        public bool Blocked { get; set; }
 
    }
}
