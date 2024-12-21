using AutoMapper;
using CharityHub.Application.Base;
using CharityHub.Application.Services.Accounts.Queries.Models;
using CharityHub.Application.Services.Accounts.Queries.Responses;
using CharityHub.Application.Wappers;
using CharityHub.Domain.Entities.Identities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CharityHub.Application.Services.Accounts.Queries.Handlers
{
    public class AccountQueryHandler : BaseResponseHandler,
        IRequestHandler<GetAccountPaginatedListQuery, PaginatedResult<GetAccountPaginatedListResponse>>,
        IRequestHandler<GetUserByIdQuery, BaseResponse<GetAccountByIdResponse>>
    {
        #region Fields
        private readonly ILogger<AccountQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<Account> _userManager;
        #endregion

        #region Constructors
        public AccountQueryHandler(ILogger<AccountQueryHandler> logger, IMapper mapper, UserManager<Account> userManager)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }
        #endregion

        #region Functions
        public async Task<PaginatedResult<GetAccountPaginatedListResponse>> Handle(GetAccountPaginatedListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var users = _userManager.Users.AsQueryable();
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
                _logger.LogDebug(ex, "Error in GetUserPaginatedListQuery");
                throw;
            }
        }

        public async Task<BaseResponse<GetAccountByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (user == null)
                    return NotFound<GetAccountByIdResponse>($"User with Id: {request.Id} not found!");

                var usermapper = _mapper.Map<GetAccountByIdResponse>(user);
                return Success(usermapper);
            }
            catch (Exception ex)
            {

                return ServerError<GetAccountByIdResponse>(ex.Message);
            }
        }



        #endregion

    }
}
