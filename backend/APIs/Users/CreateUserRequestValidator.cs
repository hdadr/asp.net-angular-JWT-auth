using backend.Models.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace backend.Features.users
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        private readonly UserManager<AppUser> _userManager;

        public CreateUserRequestValidator(UserManager<AppUser> userManager)
        {
            _userManager = userManager;

            RuleFor(user => user.FirstName).NotNull().NotEmpty();
            RuleFor(user => user.LastName).NotNull().NotEmpty();
            RuleFor(user => user.UserName).NotNull().NotEmpty();
            RuleFor(user => user.UserName).Must(userName => !IsUserNameTaken(userName))
                .WithMessage("The username is already taken.");
            RuleFor(user => user.Email).NotNull().EmailAddress();
            RuleFor(user => user.Password).NotNull().MinimumLength(6);
            RuleFor(user => user.ConfirmPassword).Equal(user => user.Password).WithMessage("Passwords do not match.");
        }
        private bool IsUserNameTaken(string userName)
        {
            return _userManager.Users.Any(user => user.UserName == userName);
        }
    }
}
