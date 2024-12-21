using CharityHub.Application.Services.Authorizations.Queries.Responses;
using CharityHub.Domain.Entities.Identities;

namespace CharityHub.Application.Mapping.Authorizations
{
    public partial class AuthorizationProfile
    {
        public void GetRoleByIdMapping()
        {
            CreateMap<Role, GetRoleByIdResponse>()
                .ForMember(dest => dest.roleName, opt => opt.MapFrom(src => src.Name));
        }
    }
}
