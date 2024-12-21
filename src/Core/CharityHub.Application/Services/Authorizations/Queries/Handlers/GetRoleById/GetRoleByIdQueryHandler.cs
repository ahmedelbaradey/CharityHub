using AutoMapper;
using CharityHub.Application.Base;
using CharityHub.Application.Services.Authorizations.Queries.Responses;
using CharityHub.Application.Abstracts.Logger;
using CharityHub.DomainService.Abstractions.Services;

namespace CharityHub.Application.Services.Authorizations.Queries.Handlers.GetRoleById
{
    public class GetRoleByIdQueryHandler : BaseResponseHandler,IQueryHandler<GetRoleByIdQuery, BaseResponse<GetRoleByIdResponse>>
        
    {
        #region Fileds
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        #endregion

        #region Constructors
        public GetRoleByIdQueryHandler(IAuthorizationService authorizationService,IMapper mapper,ILoggerManager logger)
        {
            _authorizationService = authorizationService;
            _mapper = mapper;
            _logger = logger;   
        }
        #endregion

        #region Functions
        public async Task<BaseResponse<GetRoleByIdResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var role = await _authorizationService.GetRoleByID(request.Id);
                if (role == null)
                    return NotFound<GetRoleByIdResponse>("role with this Id not Found!");

                var roleMapper = _mapper.Map<GetRoleByIdResponse>(role);
                return Success(roleMapper);
            }
            catch (Exception ex)
            {
               _logger.LogError(ex,ex.Message);
                return ServerError<GetRoleByIdResponse>(ex.Message);
            }
        }
        #endregion
    }
}
