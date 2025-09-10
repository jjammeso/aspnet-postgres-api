using FluentValidation;
using RestApiTemplate.DTOs;

namespace RestApiTemplate.Validators
{
    public class UserRegisterDtoValidator:AbstractValidator<UserRegisterDTO>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.").
                MinimumLength(2).WithMessage("Name must be at least 2 characters long.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("At least 8 characters required.")
                .Matches("[A-Z]").WithMessage("Include an uppercase letter.")
                .Matches("[a-z]").WithMessage("Include a lowercase letter.")
                .Matches("[0-9]").WithMessage("Include a number.")
                .Matches("[!@#$%^&*]").WithMessage("Include a special character (!@#$%^&*).");

        }
    }
}
