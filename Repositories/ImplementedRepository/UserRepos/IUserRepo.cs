using DUCtrongAPI.Models;
using DUCtrongAPI.Repositories.EmplementedRepository.Paging;
using DUCtrongAPI.Repositories.GenericRepository;
using DUCtrongAPI.Requests;

namespace DUCtrongAPI.Repositories.EmplementedRepository.UserRepos
{
    public interface IUserRepo : IRepository<User>
    {
       
        public Task<User> Login(int phonenumber, string password);
        public Task<PagedResult<UserViewPaging>> GetUserPaging(UserPaging userPaging);
    }
}
