using CharityHub.Application.Base;
using CharityHub.Application.Services.Authorizations.Queries.Responses;
using MediatR;

namespace CharityHub.Application.Services.Authorizations.Queries.Handlers.GetRoleList
{
    public class GetRoleListQuery : IQuery<BaseResponse<List<GetRoleListResponse>>>
    {

    }
}
