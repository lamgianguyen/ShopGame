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

        public async Task<PagedResult<UserViewPaging>> GetUserPaging(UserPaging userPaging)
        {
            var query = from user in context.Users
                        select user;
            if (!string.IsNullOrEmpty(userPaging.NameOrPhone))
            {
                query = query.Where(x => x.UserName.Contains(userPaging.NameOrPhone) || x.PhoneNumber.Equals(userPaging.NameOrPhone));
            }

            var totalRow = await query.CountAsync();

            var list = await query
                .Skip((userPaging.pageIndex - 1) * userPaging.pageItems)
                .Take(userPaging.pageItems)
                .Select(selector => new UserViewPaging()
                {
                    UserId = selector.UserId,
                    UserName = selector.UserName,
                    PhoneNumber = selector.PhoneNumber,
                    Address = selector.Address,
                    RoleId = selector.RoleId,
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
