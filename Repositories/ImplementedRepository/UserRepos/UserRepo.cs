using AutoMapper;
using DUCtrongAPI.Models;
using DUCtrongAPI.Repositories.EmplementedRepository.Paging;
using DUCtrongAPI.Repositories.GenericRepository;
using DUCtrongAPI.Requests;
using Microsoft.EntityFrameworkCore;
using Polly;


namespace DUCtrongAPI.Repositories.EmplementedRepository.UserRepos
{
    public class UserRepo : Repository<User> ,IUserRepo
    {
        public UserRepo(ShopingContext context, IMapper mapper) : base(context, mapper)
        {
            
        }
        public async Task<UserViewPaging> GetUserById(string id)
        {
            var query = from u in context.Users
                        where u.UserId.Equals(id)
                        join role in context.Roles on u.RoleId equals role.RoleId
                        select new UserViewPaging()
                        {
                            UserId = u.UserId,
                            UserName = u.UserName,
                            Address = u.Address,
                            PhoneNumber = u.PhoneNumber,
                            roleName = role.RoleName
                        };
            UserViewPaging user = await query.SingleOrDefaultAsync();
            return user;
        }

        public async Task<PagedResult<UserViewPaging>> GetUserPaging(UserPaging userPaging)
        {
            var query = from user in context.Users
                        join role in context.Roles on user.RoleId equals role.RoleId
                        select new { User2 = user, Role2 = role };

            if (!string.IsNullOrEmpty(userPaging.NameOrPhone))
            {
               query = query.Where(x => x.User2.UserName.Contains(userPaging.NameOrPhone) || x.User2.PhoneNumber.Equals(userPaging.NameOrPhone));
            }

            var totalRow = await query.CountAsync();

            var list = await query
                .Skip((userPaging.pageIndex - 1) * userPaging.pageItems)
                .Take(userPaging.pageItems)
                .Select(selector => new UserViewPaging()
                {
                    UserId = selector.User2.UserId,
                    UserName = selector.User2.UserName,
                    PhoneNumber = selector.User2.PhoneNumber,
                    Address = selector.User2.Address,
                    roleName = selector.Role2.RoleName,
                }).ToListAsync();

            var pageResult = new PagedResult<UserViewPaging>(list,  totalRow, userPaging.pageIndex, userPaging.pageItems);

            return pageResult;

        }

        public async Task<User> Login(string phonenumber, string password)
        {

            var query = from user in context.Users
                        where user.PhoneNumber == phonenumber && user.Password == password
                        select user;
            var value = await query.FirstOrDefaultAsync();
            return value;

        }
    }

        
        
    
}
