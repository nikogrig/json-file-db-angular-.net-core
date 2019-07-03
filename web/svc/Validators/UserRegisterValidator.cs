using FluentValidation;
using src.Services;
using src.ViewModels;

namespace src.Validators 
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterViewModel>
    {
        private readonly IValidationService _validationService;

        public UserRegisterValidator(IValidationService validationService)
        {
            this._validationService = validationService;

            RuleFor(u => u.UserName)
                .NotNull()
                .NotEmpty()
                .Length(3, 20)
                .WithMessage($"Username cannot be empty. The length of field must be between 3 and 20");

            RuleFor(u => u.Email)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5)
                .Must(ValidateMailIfExist)
                .WithMessage("Validation exception")
                .Matches(@"^[a-zA-Z0-9.!#$%&’*+=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$")
                .WithMessage($"Email cannot be empty and must be valid pattern.");

            RuleFor(u => u.FirstName)
               .NotNull()
               .NotEmpty()
               .Length(2, 30)
               .Matches(@"^[a-zA-Z]+$")
               .WithMessage($"FirstName cannot be empty. The length of field must be between 2 and 30 and should contain only letters.");

            RuleFor(u => u.LastName)
               .NotNull()
               .NotEmpty()
               .Length(2, 30)
               .Matches(@"^[a-zA-Z]+$")
               .WithMessage($"LastName cannot be empty. The length of field must be between 2 and 30 and should contain only letters.");

            RuleFor(u => u.Telephone)
                .NotNull()
                .NotEmpty()
                .Matches(@"^[+0-9]{10,14}$")
                .WithMessage("Phone number must contain only digits and start can be start with '+' sign");

            RuleFor(u => u.Password)
                .NotNull()
                .NotEmpty()
                .Length(6, 16)
                .Matches(@"^[0-9]{6,16}$")
                .WithMessage("Password or email does not match for eny user");
        }

        private bool ValidateMailIfExist(string email)
        {
            return this._validationService.IfEmailExist(email);
        }
    }
}