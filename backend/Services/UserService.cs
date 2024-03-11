using AutoMapper;
using backend.Exceptions;
using backend.Features.users;
using backend.Models.Entities;
using backend.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserViewModel> CreateUserAsync(CreateUserRequest request)
        {
            var user = _mapper.Map<AppUser>(request);
            var identityResult = await _userManager.CreateAsync(user, request.Password);
            if (!identityResult.Succeeded)
                throw new RequestHandlingException("User could not be added to database.", identityResult.Errors);

            await _userManager.AddToRoleAsync(user, "User");

            return _mapper.Map<UserViewModel>(GetUserByUserName(request.UserName));
        }
        public async Task<IList<string>> GetUserRolesAsync(AppUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }
        public AppUser GetUserByUserName(string userName)
        {
            return _userManager.Users.FirstOrDefault(user => user.UserName == userName);
        }

    }
}
