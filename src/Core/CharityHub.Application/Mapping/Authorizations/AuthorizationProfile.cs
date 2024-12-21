using AutoMapper;

namespace CharityHub.Application.Mapping.Authorizations
{
    public partial class AuthorizationProfile : Profile
    {
        public AuthorizationProfile()
        {
            GetRoleByIdMapping();
            GetRoleListMapping();
        }
    }
}
