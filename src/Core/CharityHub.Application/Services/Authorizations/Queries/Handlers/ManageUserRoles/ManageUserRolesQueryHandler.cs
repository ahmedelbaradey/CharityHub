using AutoMapper;
using CharityHub.Application.Base;
using CharityHub.Domain.Entities.Identities;
using CharityHub.Domain.Helpers;
using CharityHub.Application.Abstracts.Logger;
using Microsoft.AspNetCore.Identity;
using CharityHub.DomainService.Abstractions.Services;

namespace CharityHub.Application.Services.Authorizations.Queries.Handlers.ManageUserRoles
{
    public class ManageUserRolesQueryHandler : BaseResponseHandler,IQueryHandler<ManageUserRolesQuery, BaseResponse<ManageUserRolesResponse>>
    {
        #region Fileds
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;
        private readonly UserManager<Account> _userManager;
        private readonly ILoggerManager _logger;
        #endregion

        #region Constructors
        public ManageUserRolesQueryHandler(IAuthorizationService authorizationService,IMapper mapper, UserManager<Account> userManager,  ILoggerManager logger)
        {
            _authorizationService = authorizationService;
            _mapper = mapper;
            _userManager = userManager;
            _logger = logger;   
        }
        #endregion

        #region Functions
        public async Task<BaseResponse<ManageUserRolesResponse>> Handle(ManageUserRolesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.Id.ToString());
                if (user == null)
                    return NotFound<ManageUserRolesResponse>("user with this Id not Found!");

                var result = await _authorizationService.GetManagerAccountsRolesData(user);
                return Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ServerError<ManageUserRolesResponse>(ex.Message);
            }
        }
        #endregion
    }
}
