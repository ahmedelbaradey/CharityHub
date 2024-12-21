using AutoMapper;
using CharityHub.Application.Base;
using CharityHub.Application.Services.Accounts.Commands.Models;
using CharityHub.Domain.Entities.Identities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CharityHub.Application.Services.Accounts.Commands.Handlers
{
    public class AccountCommandHandler : BaseResponseHandler,
        IRequestHandler<AddAccountCommand, BaseResponse<string>>,
        IRequestHandler<EditAccountCommand, BaseResponse<string>>,
        IRequestHandler<DeleteAccountCommand, BaseResponse<string>>
    {
        #region Fields
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public AccountCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }
        #endregion

        #region Functions

        public async Task<BaseResponse<string>> Handle(AddAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //write this logic if program is needed.
                var IsUserExistByEmail = await _userManager.FindByEmailAsync(request.Email);
                if (IsUserExistByEmail != null)
                    return BadRequest<string>("this email already before used.");

                //write this logic if program is needed.
                var IsUserExistByUserName = await _userManager.FindByNameAsync(request.UserName);
                if (IsUserExistByUserName != null) return BadRequest<string>("this username already before used.");

                var user = _mapper.Map<Account>(request);
                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                {
                    string errorMessage = "Errors occurred while creating the user: ";
                    foreach (var error in result.Errors)
                    {
                        errorMessage += "\n" + $"{error.Description}";
                    }
                    return new BaseResponse<string>(errorMessage);
                }

                //Add role for this user
                await _userManager.AddToRoleAsync(user, "user");

                return Created<string>("User Added Successfully.");
            }
            catch (Exception ex)
            {
                return ServerError<string>(ex.Message);
            }
        }

        public async Task<BaseResponse<string>> Handle(EditAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var oldUser = await _userManager.FindByIdAsync(request.Id.ToString());
                if (oldUser == null)
                    return NotFound<string>($"User with id: {request.Id} not found!");

                var IsUserExistByEmail = await _userManager.FindByEmailAsync(request.Email);
                if (IsUserExistByEmail != null && IsUserExistByEmail.Id != request.Id)
                    return BadRequest<string>("this email already before used.");

                var IsUserExistByUserName = await _userManager.FindByNameAsync(request.UserName);
                if (IsUserExistByUserName != null && IsUserExistByUserName.UserName != request.UserName)
                    return BadRequest<string>("this username already before used.");

                // استخدم الـ Mapper لتحديث الحقول فقط على الكائن القديم
                var userMapper = _mapper.Map(request, oldUser);

                //في الحالة ده هو ببنشالك كائن جديد من user مما يسبب بعض المشاكل 
                //var UserMapper = _mapper.Map<User>(request);

                var result = await _userManager.UpdateAsync(userMapper);
                if (!result.Succeeded)
                    return BadRequest<string>(result.Errors.FirstOrDefault()?.Description);

                return Success("Updated User is Successfully");
            }
            catch (Exception ex)
            {
                return ServerError<string>(ex.Message);
            }
        }

        public async Task<BaseResponse<string>> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.Id.ToString());
                if (user == null)
                    return NotFound<string>($"User with Id: {request.Id} not found!");

                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                    return BadRequest<string>("Deleted Operation Failed.");

                return Success("Deleted Operation Successfully.");
            }
            catch (Exception ex)
            {
                return ServerError<string>(ex.Message);
            }
        }

      
        #endregion




    }
}
