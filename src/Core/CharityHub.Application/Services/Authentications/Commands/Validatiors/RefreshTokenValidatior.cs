using FluentValidation;
using CharityHub.Application.Services.Authentications.Commands.Handlers.Device;

namespace CharityHub.Application.Services.Authentications.Commands.Validatiors
{
    public class RefreshTokenValidatior : AbstractValidator<DeviceCommand>
    {
        public RefreshTokenValidatior()
        {
            ApplyValidationsRules();
        }

        #region Functions
        public void ApplyValidationsRules()
        {

            RuleFor(x => x.AccessToken)
                 .NotNull().WithMessage("Can't be empty.")
                 .NotEmpty().WithMessage("Can't be empty.");

            RuleFor(x => x.RefreshToken)
                 .NotNull().WithMessage("Can't be empty.")
                 .NotEmpty().WithMessage("Can't be empty.");
        }
        #endregion
    }
}
