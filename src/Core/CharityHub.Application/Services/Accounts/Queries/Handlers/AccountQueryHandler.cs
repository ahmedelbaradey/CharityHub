using AutoMapper;
using CharityHub.Application.Base;
using CharityHub.Application.Services.Accounts.Queries.Models;
using CharityHub.Application.Services.Accounts.Queries.Responses;
using CharityHub.Application.Wappers;
using CharityHub.DomainService.Abstractions.Logger;
using CharityHub.DomainService.Abstractions.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CharityHub.Application.Services.Accounts.Queries.Handlers
{
    public class AccountQueryHandler : BaseResponseHandler,IRequestHandler<GetAccountPaginatedListQuery, PaginatedResult<GetAccountPaginatedListResponse>>,IRequestHandler<GetUserByIdQuery, BaseResponse<GetAccountByIdResponse>>
    {
        #region Fields
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        #endregion

        #region Constructors
        public AccountQueryHandler(ILoggerManager logger, IMapper mapper, IAccountService accountService)
        {
            _logger = logger;
            _mapper = mapper;
            _accountService = accountService;
        }
        #endregion

        #region Functions
        public async Task<PaginatedResult<GetAccountPaginatedListResponse>> Handle(GetAccountPaginatedListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var users =  _accountService.GetAllAccounts();
                if (!users.Any())
                {
                    return new PaginatedResult<GetAccountPaginatedListResponse>(new());
                }

                var paginatedList = await _mapper.ProjectTo<GetAccountPaginatedListResponse>(users)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);

                return paginatedList;
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Error in GetUserPaginatedListQuery");
                throw;
            }
        }

        public async Task<BaseResponse<GetAccountByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _accountService.GetAccountById(request.Id);
                if (user == null)
                    return NotFound<GetAccountByIdResponse>($"Account with Id: {request.Id} not found!");

                var usermapper = _mapper.Map<GetAccountByIdResponse>(user);
                return Success(usermapper);
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Error in GetAccountPaginatedListQuery");
                return ServerError<GetAccountByIdResponse>(ex.Message);
            }
        }



        #endregion

    }
}
