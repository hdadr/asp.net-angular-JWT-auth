using backend.APIs.Auth;
using backend.Data.Repositories;
using backend.Models.ViewModels;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace backend.Features.auth.validators
{
    public class RefreshAccessTokenRequestValidator : AbstractValidator<RefreshAccessTokenRequest>
    {
        private readonly IRefreshTokenRepository _repository;

        public RefreshAccessTokenRequestValidator(TokenValidationParameters tokenValidationParameters, IRefreshTokenRepository repository)
        {
            _repository = repository;

            RuleFor(r => r.AccessToken).NotNull().WithMessage("AccessToken required.");
            RuleFor(r => r.RefreshToken).NotNull().WithMessage("RefreshToken required.");
            RuleFor(r => r.AccessToken).Must(accessToken => IsAccessTokenSignatureValid(accessToken, tokenValidationParameters))
                .WithName("InvalidSignature")
                .WithMessage("The access token signature is invalid.");
            RuleFor(r => r).Must(r => IsAccessTokenAndRefreshTokenUserNameMatch(r.AccessToken, r.RefreshToken))
                .WithName("UserNameNotMatch")
                .WithMessage("The accessToken's and refreshToken's UserName does not match.");
            RuleFor(r => r.RefreshToken.ID).MustAsync(async (id, cancellation) => await IsRefreshTokenValid(id))
               .WithName("InvalidRefreshToken")
               .WithMessage("The refreshToken does not exist or expired.");
        }

        private bool IsAccessTokenSignatureValid(string accessToken, TokenValidationParameters tokenValidationParameters)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(
                    accessToken, 
                    new TokenValidationParameters {
                        ValidateIssuer = tokenValidationParameters.ValidateIssuer,
                        ValidateAudience = tokenValidationParameters.ValidateAudience,
                        ValidAudience = tokenValidationParameters.ValidAudience,
                        ValidIssuer = tokenValidationParameters.ValidIssuer,
                        IssuerSigningKey = tokenValidationParameters.IssuerSigningKey,
                        ClockSkew = tokenValidationParameters.ClockSkew,
                        ValidateLifetime = false
                    }, 
                    out _
                );
                return true;
            }
            catch
            {
                return false;
            }
        }
        private bool IsAccessTokenAndRefreshTokenUserNameMatch(string accessToken, RefreshTokenViewModel refreshToken)
        {
            return string.Equals(GetUserNameFromClaim(accessToken), refreshToken.UserName);
        }
        private string GetUserNameFromClaim(string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(accessToken) as JwtSecurityToken;

            return securityToken.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;
        }
        private async Task<bool> IsRefreshTokenValid(Guid id)
        {
            var refreshToken = await _repository.GetRefreshTokenAsync(id);
            if (refreshToken == null)
                return false;

            var exists = refreshToken != null;
            var expired = refreshToken.IsExpired;
            return exists && !expired;
        }
    }
}
