using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamTaskManager.Models;

namespace TeamTaskManager.Data
{
   public interface IAuthRepository
    {
        Task<MUser> Register(MUser user, string password);
        Task<MUser> Login(DTOS.user.userForLogins userLoging);
        Task<bool> UserExists(string userName ,string email);
        Task<MUser> GetUserInfo(int userId);
        Task<int> GetUserId(MUser user);
        Task<MUser> UPdate(int id, MUser user);
    }
}
