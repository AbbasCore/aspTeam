using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamTaskManager.DTOS.user;
using TeamTaskManager.Models;

namespace TeamTaskManager.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<int> GetUserId(MUser user)
        {
            var userId = await _context.User.FirstOrDefaultAsync(x => x.name == user.name || x.phoneNumber == user.phoneNumber || x.userName == user.userName); ;
            if (userId == null) return 0;
            return userId.id;
        }

        public async Task<MUser> GetUserInfo(int userId)
        {
            var userInfo = await _context.User.FirstOrDefaultAsync(x => x.id==userId); ;
            if (userInfo == null) return null;
            return userInfo;
        }

        public async Task<MUser> Login(DTOS.user.userForLogins userLogin)
        {
            if (userLogin.username != null)
            {
                var user = await _context.User.FirstOrDefaultAsync(x => x.userName == userLogin.username);
                if (user == null) return null;
                if (!VerifyPassword(userLogin.password, user.passwordSalt, user.passwordHash)) return null;
                return user;
            }
            else if (userLogin.email != null)
            {
                var user = await _context.User.FirstOrDefaultAsync(x => x.email == userLogin.email);
                if (user == null) return null;
                if (!VerifyPassword(userLogin.password, user.passwordSalt, user.passwordHash)) return null;
                return user;
            }
            else if (userLogin.email != null && userLogin.username != null)
            {
                var user = await _context.User.FirstOrDefaultAsync(x => x.email == userLogin.email || x.userName == userLogin.username);
                if (user == null) return null;
                if (!VerifyPassword(userLogin.password, user.passwordSalt, user.passwordHash)) return null;
                return user;
            }
            else return null;
        }

        public async Task<MUser> Register(MUser user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.passwordSalt = passwordSalt;
            user.passwordHash = passwordHash;
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> UserExists(string userName ,string email)
        {
            if (await _context.User.AnyAsync(x => x.userName == userName || x.email==email)) return true;
            return false;
        }

        public async Task<MUser> UPdate(int userId, MUser user)
        {
                var userInfo = await _context.User.FirstOrDefaultAsync(x => x.id == userId); ;
                if (userInfo != null)
                {
                    userInfo.name = user.name;
                    userInfo.email = user.email;
                    userInfo.phoneNumber = user.phoneNumber;
                    userInfo.teamIdFK = user.teamIdFK;
                    userInfo.userName = user.userName;
                
                    return userInfo;
                }
                else
                    return userInfo;

        }

        //************************
        private bool VerifyPassword(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var com = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < com.Length; i++)
                {
                    if (com[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }
        //************************
    }
}
