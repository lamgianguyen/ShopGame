using DUCtrongAPI.Models;
using DUCtrongAPI.Repositories.EmplementedRepository.Paging;
using DUCtrongAPI.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DUCtrongAPI.Services.UserSevices
{
    public interface IUserService
    {
        Task<string> Login(UserLogin userlogin);
        Task<User> Register(UserRegis user);
        Task<PagedResult<UserViewPaging>> GetUserPaging(UserPaging userPaging);
    }
}
