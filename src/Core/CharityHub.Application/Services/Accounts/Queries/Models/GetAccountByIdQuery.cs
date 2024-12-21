using CharityHub.Application.Base;
using CharityHub.Application.Services.Accounts.Queries.Responses;
using MediatR;

namespace CharityHub.Application.Services.Accounts.Queries.Models
{
    public class GetUserByIdQuery : IRequest<BaseResponse<GetAccountByIdResponse>>
    {
        public int Id { get; set; }
 
    }
}
