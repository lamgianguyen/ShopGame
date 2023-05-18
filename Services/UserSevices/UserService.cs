using DUCtrongAPI.Models;
using DUCtrongAPI.Repositories.EmplementedRepository.UserRepos;
using DUCtrongAPI.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using DUCtrongAPI.Repositories.EmplementedRepository.Paging;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace DUCtrongAPI.Services.UserSevices
{
    public class UserService : IUserService
    {
     
        private readonly IUserRepo _userRepo;
        private readonly IConfiguration _configuration;
        public UserService(IUserRepo _userRepo, IConfiguration _configuration)
        {
            this._userRepo = _userRepo;
            this._configuration = _configuration;
        }
        public async Task<string> Login(UserLogin userlogin)
        {
           User user = await _userRepo.Login(userlogin.phonenumber, userlogin.password);
            if (user == null)
                return null;
            //return result user
            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
            UserJWT userjwt = new UserJWT
            {
                UserID = user.UserId,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };


            if (!VerifyPasswordHass(user.Password, userjwt.PasswordHash, userjwt.PasswordSalt))
            {
                return null;
            }
            string token = CreateToken(user);
            return token;


          
            
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                
                 new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                 new Claim("Userid",user.UserId.ToString()),
                 new Claim("UserName",user.UserName.ToString()),
                 new Claim("RoleId",user.RoleId.ToString()),
                 new Claim("PhoneNumber",user.PhoneNumber.ToString()),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token")?.Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        private bool VerifyPasswordHass(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {

                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }

        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<User> Register(UserRegis userRegis)
        {
            User user =new User();
            user.UserId = Guid.NewGuid().ToString();
            user.PhoneNumber = userRegis.PhoneNumber;
            user.Password = userRegis.Password;
            user.UserName = userRegis.UserName;
            user.RoleId = 2;
           var check =   await _userRepo.Insert(user);
            if (check != true)
                return null;
            return user;
          
        }

        public Task<PagedResult<UserViewPaging>> GetUserPaging(UserPaging userPaging)
        {
            var listUser =  _userRepo.GetUserPaging(userPaging);
            return listUser;
        }

        public async Task<UserViewPaging> GetUserById(string id)
        {

            var userviewpaging = await _userRepo.GetUserById(id);
            return userviewpaging;
        }
    }
}
