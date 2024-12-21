using FluentValidation;
using CharityHub.Application.Services.Authentications.Commands.Handlers.SignIn;

namespace CharityHub.Application.Services.Authentications.Commands.Validatiors
{
    public class SignInValidatior : AbstractValidator<SignInCommand>
    {

        public SignInValidatior()
        {
            ApplyValidationsRules();
        }

        #region Functions
        public void ApplyValidationsRules()
        {

            RuleFor(x => x.MobileNumber)
                 .NotEmpty().WithMessage("Can't be empty.")
                 .NotNull().WithMessage("Can't be empty.");

            RuleFor(x => x.TOTP)
                .NotEmpty().WithMessage("Can't be empty.")
                .NotNull().WithMessage("Can't be empty.");
        }
        #endregion
    }
}
