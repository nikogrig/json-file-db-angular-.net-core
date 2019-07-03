using FluentValidation;
using src.Services;
using src.ViewModels;

namespace src.Validators
{
    public class UserLoginValidator : AbstractValidator<UserLoginViewModel>
    {
        private readonly IValidationService _validationService;

        public UserLoginValidator(IValidationService validationService)
        {
            this._validationService = validationService;

            RuleFor(u => u.Email)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5)
                .Must(ValidateMailIfExist)
                .WithMessage("Validation exception")
                .Matches(@"^[a-zA-Z0-9.!#$%&’*+=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$")
                .WithMessage($"Email cannot be empty and must be valid pattern.");

            RuleFor(u => u.Password)
                .NotNull()
                .NotEmpty()
                .Length(6, 16)
                .Matches(@"^[0-9]{6,16}$")
                .WithMessage("Password or email does not match for eny user");
        }

        private bool ValidateMailIfExist(string email)
        {
            return !this._validationService.IfEmailExist(email);
        }
    }
}
