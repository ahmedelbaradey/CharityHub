using FluentValidation;
using CharityHub.Application.Services.Authentications.Queries.Handlers.ValidateAccessToken;

namespace CharityHub.Application.Services.Authentications.Queries.Handlers.Validatiors
{
    public class AccessTokenQueryValidatior : AbstractValidator<AccessTokenQuery>
    {
        public AccessTokenQueryValidatior()
        {
            ApplyValidationsRules();
        }

        #region Functions
        public void ApplyValidationsRules()
        {

            RuleFor(x => x.Accesstoken)
                 .NotNull().WithMessage("Can't be empty.")
                 .NotEmpty().WithMessage("Can't be empty.");

        }
        #endregion
    }
}
