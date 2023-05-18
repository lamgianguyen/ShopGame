using DUCtrongAPI.Models;
using DUCtrongAPI.Repositories.EmplementedRepository.Paging;
using DUCtrongAPI.Repositories.GenericRepository;
using DUCtrongAPI.Requests;

namespace DUCtrongAPI.Repositories.EmplementedRepository.UserRepos
{
    public interface IUserRepo : IRepository<User>
    {
       
        public Task<User> Login(string phonenumber, string password);
        public Task<PagedResult<UserViewPaging>> GetUserPaging(UserPaging userPaging);
        public Task<UserViewPaging> GetUserById(string id);
    }
}
