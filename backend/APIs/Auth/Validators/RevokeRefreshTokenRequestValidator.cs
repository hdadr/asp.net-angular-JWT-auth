using FluentValidation;
using System;

namespace backend.APIs.Auth.Validators
{
    public class RevokeRefreshTokenRequestValidator : AbstractValidator<string>
    {
        public RevokeRefreshTokenRequestValidator()
        {
            RuleFor(id => id).Must(IsIdValidGuid)
                .WithName("InvalidGuid")
                .WithMessage("The given id is not a valid Guid.");
        }

        private bool IsIdValidGuid(string id)
        {
            return Guid.TryParse(id, out _);
        }
    }
}
