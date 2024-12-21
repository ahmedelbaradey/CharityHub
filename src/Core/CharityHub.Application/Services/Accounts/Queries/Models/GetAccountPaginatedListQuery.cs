using CharityHub.Application.Services.Accounts.Queries.Responses;
using CharityHub.Application.Wappers;
using MediatR;

namespace CharityHub.Application.Services.Accounts.Queries.Models
{
    public class GetAccountPaginatedListQuery : IRequest<PaginatedResult<GetAccountPaginatedListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

}
