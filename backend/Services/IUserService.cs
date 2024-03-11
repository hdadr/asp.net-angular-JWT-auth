using backend.Features.users;
using backend.Models.Entities;
using backend.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Services
{
    public interface IUserService
    {
        Task<UserViewModel> CreateUserAsync(CreateUserRequest model);
        Task<IList<string>> GetUserRolesAsync(AppUser user);
        AppUser GetUserByUserName(string userName);
    }
}
