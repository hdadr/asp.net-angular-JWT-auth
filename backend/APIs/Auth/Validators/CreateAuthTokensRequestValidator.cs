using backend.APIs.Auth;
using backend.Models.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace backend.Features.auth.validators
{
    public class CreateAuthTokensRequestValidator : AbstractValidator<CreateAuthTokensRequest>
    {

        public UserManager<AppUser> _userManager { get; }
        public CreateAuthTokensRequestValidator(UserManager<AppUser> userManager)
        {
            _userManager = userManager;

            RuleFor(user => user.UserName).NotNull();
            RuleFor(user => user.Password).NotNull();
            RuleFor(user => user).MustAsync(async (user, cancellation) => await IsLoginCredentialsValidAsync(user.UserName, user.Password))
                .WithName("InvalidCredentials")
                .WithMessage("Invalid username or password.");

        }
        private async Task<bool> IsLoginCredentialsValidAsync(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                return false;

            return await _userManager.CheckPasswordAsync(user, password);
        }

    }
}
