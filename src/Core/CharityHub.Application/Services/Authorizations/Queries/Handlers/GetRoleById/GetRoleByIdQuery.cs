using CharityHub.Application.Base;
using CharityHub.Application.Services.Authorizations.Queries.Responses;
using MediatR;

namespace CharityHub.Application.Services.Authorizations.Queries.Handlers.GetRoleById
{
    public class GetRoleByIdQuery : IQuery<BaseResponse<GetRoleByIdResponse>>
    {
        public int Id { get; set; }
    }
}
