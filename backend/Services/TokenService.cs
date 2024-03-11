using AutoMapper;
using backend.APIs.auth;
using backend.APIs.Auth;
using backend.Data.Repositories;
using backend.Models.Entities;
using backend.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace backend.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public TokenService(
            IConfiguration configuration,
            IRefreshTokenRepository refreshTokenRepository,
            IUserService userService,
            IMapper mapper
        )
        {
            _configuration = configuration;
            _refreshTokenRepository = refreshTokenRepository;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<CreateAuthTokensResponse> CreateAuthTokensAsync(CreateAuthTokensRequest request)
        {
            var user = _userService.GetUserByUserName(request.UserName);

            return new CreateAuthTokensResponse
            {
                AccessToken = await CreateAccessToken(user),
                RefreshToken = _mapper.Map<RefreshTokenViewModel>(await CreateRefreshTokenAsync(user))
            };


        }
        public async Task<string> RefreshAccessTokenAsync(RefreshAccessTokenRequest request)
        {
            var user = _userService.GetUserByUserName(request.RefreshToken.UserName);
            return await CreateAccessToken(user);
        }
        public async Task RevokeRefreshTokenAsync(string refreshTokenID)
        {
            await _refreshTokenRepository.DeleteRefreshTokenAsync(new Guid(refreshTokenID));
            await _refreshTokenRepository.SaveAsync();
        }
        private async Task<string>CreateAccessToken(AppUser user)
        {
            var userRoles = await _userService.GetUserRolesAsync(user);
            var userRoleClaims = new List<Claim>();
            userRoles.ToList().ForEach(role => userRoleClaims.Add(new Claim(ClaimTypes.Role, role)));

            DateTime jwtDate = DateTime.UtcNow;
            int validityDuration = int.Parse(_configuration["Jwt:jwtValidityDurationInMins"]);
            var jwt = new JwtSecurityToken(
                audience: _configuration["Jwt:Issuer"],
                issuer: _configuration["Jwt:Audiance"],
                claims: new List<Claim> { new Claim(ClaimTypes.Name, user.UserName) }.Concat(userRoleClaims),
                notBefore: jwtDate,
                expires: jwtDate.AddMinutes(validityDuration),
                signingCredentials: new SigningCredentials(
                    key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"])),
                    algorithm: SecurityAlgorithms.HmacSha256
                )
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
        private async Task<RefreshToken> CreateRefreshTokenAsync(AppUser user)
        {
            RefreshToken token = GenerateRefreshToken(user);
            await _refreshTokenRepository.InsertRefreshTokenAsync(token);
            await _refreshTokenRepository.SaveAsync();

            return token;
        }
        private RefreshToken GenerateRefreshToken(AppUser user)
        {
            var randomNumber = new byte[32];
            using var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(randomNumber);

            int validityDuration = int.Parse(_configuration["Jwt:refreshTokenValidityDurationInDays"]);
            return new RefreshToken
            {
                UserName = user.UserName,
                Token = Convert.ToBase64String(randomNumber),
                Created = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(validityDuration),
            };
        }
    }
}
