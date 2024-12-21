using FluentValidation;
using CharityHub.Application.Services.Authorizations.Commands.Handlers.EditRole;

namespace CharityHub.Application.Services.Authorizations.Commands.Validatiors
{
    public class EditRoleValidator : AbstractValidator<EditRoleCommand>
    {
        #region Constructors
        public EditRoleValidator()
        {
            ApplyValidationsRules();
        }
        #endregion

        #region Handle Fnctions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Can't be balnk.");

            RuleFor(x => x.roleName)
                .NotNull().WithMessage("Can't be balnk.");
        }
        #endregion
    }
}
