using FluentValidation;

namespace EvenDriven.App.User.DTOs
{
    public sealed record RegisterUserDtoReq(
        string? FirstName,
        string? LastName,
        string Email,
        string Password
    );

    public class RegisterUserDtoReqValidator : AbstractValidator<RegisterUserDtoReq>
    {
        public RegisterUserDtoReqValidator() 
        { 
            RuleFor(userReq => userReq.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email address");

            RuleFor(userReq => userReq.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long");
        }
    }
}
