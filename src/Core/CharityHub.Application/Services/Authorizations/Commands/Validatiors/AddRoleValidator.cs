using FluentValidation;
using CharityHub.Application.Services.Authorizations.Commands.Handlers.AddRole;

namespace CharityHub.Application.Services.Authorizations.Commands.Validatiors
{
    public class AddRoleValidator : AbstractValidator<AddRoleCommand>
    {

        #region Constructors
        public AddRoleValidator()
        {
            ApplyValidationsRules();
        }
        #endregion

        #region Handle Fnctions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.roleName)
                .NotNull().WithMessage("Can't be balnk.");
        }
        #endregion
    }
}
