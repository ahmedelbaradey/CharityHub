
using CharityHub.Application.Services.Accounts.Commands.Models;
using FluentValidation;

namespace CharityHub.Application.Services.Accounts.Commands.Validatiors
{
    public class EditAccountValidatior : AbstractValidator<EditAccountCommand>
    {

        #region Constructors
        public EditAccountValidatior()
        {
            ApplyValidationsRules();
        }
        #endregion

        #region Functions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Can't be empty.")
                .NotNull().WithMessage("Can't be blank.");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Can't be empty.")
                .NotNull().WithMessage("Can't be blank.")
                .MaximumLength(200);

            RuleFor(x => x.UserName)
               .NotEmpty().WithMessage("Can't be empty.")
               .NotNull().WithMessage("Can't be blank.")
               .MaximumLength(100);

            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Can't be empty.")
               .NotNull().WithMessage("Can't be blank.")
               .MaximumLength(100);


        }
        #endregion

    }
}
