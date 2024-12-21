using AutoMapper;
using CharityHub.Application.Base;
using CharityHub.Application.Services.Accounts.Commands.Models;
using CharityHub.Domain.Entities.Identities;
using CharityHub.DomainService.Abstractions.Logger;
using CharityHub.DomainService.Abstractions.Services;

namespace CharityHub.Application.Services.Accounts.Commands.Handlers
{
    public class AccountCommandHandler : BaseResponseHandler,ICommandHandler<AddAccountCommand, BaseResponse<string>>, ICommandHandler<EditAccountCommand, BaseResponse<string>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IAccountService _accountService;
        private readonly IRoleService _roleService;
        private readonly IAccountRolesService _accountRolesService;
        #endregion

        #region Constructors
        public AccountCommandHandler(IMapper mapper,    IAccountService accountService, IRoleService roleService, IAccountRolesService accountRolesService ,ILoggerManager logger)
        {
            _mapper = mapper;
            _logger = logger;   
            _accountService = accountService;
            _roleService = roleService;
            _accountRolesService = accountRolesService;
        }
        #endregion

        #region Functions

        public async Task<BaseResponse<string>> Handle(AddAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //write this logic if program is needed.
                var IsUserExist = await _accountService.GetAccountByMobileNumber(request.MobileNumber);
                if (IsUserExist is null)
                    return BadRequest<string>("this Mobile Number already before used.");
                var user = _mapper.Map<Account>(request);
                var result = await _accountService.CreateAccountAsync(user);
                if (!result)
                    return BadRequest<string>("Clinic Added Operation is Failed.");


                //Add role for this user
                var viewRole = await _roleService.GetRoleByNameAsync("Viewer");
                await _accountRolesService.AddToRoleAsync(viewRole,user);

                return Created<string>("Account Added Successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: in AddAccoutCommand");
                return ServerError<string>(ex.Message);
            }
        }

        public async Task<BaseResponse<string>> Handle(EditAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var oldUser = await _accountService.GetAccountById(request.Id);
                if (oldUser == null)
                    return NotFound<string>($"User with id: {request.Id} not found!");

                var IsUserExistl = await _accountService.GetAccountByMobileNumber(request.MobileNumber);
                if (IsUserExistl != null)
                    return BadRequest<string>("this Mobile Number already before used.");

                var userMapper = _mapper.Map(request, oldUser);
                var result = await _accountService.UpdateAccountAsync(userMapper);
                if (!result)
                    return BadRequest<string>("Updated Operation Failed.");

                return Success("Updated Operation Successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: in EditAccoutCommand");
                return ServerError<string>(ex.Message);
            }
        }

   
        #endregion




    }
}
