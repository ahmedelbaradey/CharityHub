
using CharityHub.Application.Services.Accounts.Commands.Models;
using FluentValidation;

namespace CharityHub.Application.Services.Accounts.Commands.Validatiors
{
    public class EditAccountValidatior : AbstractValidator<EditAccountCommand>
    {

        #region Constructors
        public EditAccountValidatior()
        {
            Transform(x => x.MobileNumber, to: ModifyMobileNumber);
            ApplyValidationsRules();

        }
        #endregion

        #region Functions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Id)
             .NotNull().WithMessage("Can't be blank.");

            RuleFor(x => x.FullName)
              .NotEmpty().WithMessage("Can't be empty.")
              .NotNull().WithMessage("Can't be blank.")
              .MinimumLength(2).WithMessage("Can't less than {ComparisonValue} charachters")
              .MaximumLength(50).WithMessage("Can't exceed {ComparisonValue} charachters");

            RuleFor(x => x.MobileNumber)
             .NotEmpty().WithMessage("Can't be empty.")
             .NotNull().WithMessage("Can't be blank.")
             .Matches("^[0-9]*$").WithMessage("Must be  a valid Mobile number")
             .MinimumLength(7).WithMessage("Can't less than {ComparisonValue} charachters")
             .MaximumLength(15).WithMessage("Can't exceed {ComparisonValue} charachters");

            RuleFor(x => x.PhotoUrl)
             .NotEmpty().WithMessage("Can't be empty.")
             .NotNull().WithMessage("Can't be blank.")
             .Matches("https?://(www\\.)?[-a-zA-Z0-9@:%._+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b([-a-zA-Z0-9()@:%_+.~#?&//=]*)").WithMessage("Must be  a valid Url");

        }
        string ModifyMobileNumber(string value)
        {
            if (value.StartsWith("00"))
            {
                value = value.Replace("00", "");
            }
            if (value.StartsWith("+"))
            {
                value = value.Replace("+", "");
            }
            return value;
        }
        #endregion

    }
}
