using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManager.Data;
using TeamTaskManager.DTOS.user;
using TeamTaskManager.Models;

namespace TeamTaskManager.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class authController : ControllerBase
    {
        private readonly IAuthRepository _rpo;
        private readonly IConfiguration _conf;

        public authController(IAuthRepository Repository, IConfiguration configuration)
        {
            _rpo = Repository;
            _conf = configuration;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(userForLogins userForLogin)
        {
            //throw new Exception("api says noooo !!!");
            try
            {
                var userFromRepo = await _rpo.Login(userForLogin);
                if (userFromRepo == null) return NotFound();
                //create token...
                var claims = new[]//token body
               {
                new Claim(ClaimTypes.NameIdentifier,userFromRepo.id.ToString()),
                new Claim(ClaimTypes.Name,userFromRepo.userName)
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_conf.GetSection("AppSettings:Token").Value));
                //ecryption key of token
                var cride = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = null,
                    SigningCredentials = cride//key of encription
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescription);

                return Ok(new { Token = tokenHandler.WriteToken(token) });

            }
            catch (Exception ex)
            {
                return StatusCode(500, "api is vary tired" + ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(userForRegistory userForRegistory)
        {
            userForRegistory.username = userForRegistory.username.ToLower();
            if (await _rpo.UserExists(userForRegistory.username,userForRegistory.email)) return BadRequest("هذا الاسم موجود ");
            var newUser = new MUser {name= userForRegistory.name, userName = userForRegistory.username ,email= userForRegistory.email,phoneNumber= userForRegistory.phoneNumber,teamIdFK= userForRegistory.teamIdFK};
            var makePassword_register = await _rpo.Register(newUser, userForRegistory.password);
            return StatusCode(200);
        }
        [HttpPost("getuserid")]
        public async Task<int> GetUserId(MUser user)
        {
            user.userName = user.userName.ToLower();
            int id = await _rpo.GetUserId(user);
            return id;
        }
        [HttpPost("getuserinfo")]
        public async Task<IActionResult> GetUserinfo(MUser user)
        {
            var userInfo=await _rpo.GetUserInfo(user.id);
            if (userInfo == null) return NotFound();
            return Ok(new { userInfo.id, userInfo.name , userInfo.userName, userInfo.email, userInfo.phoneNumber , userInfo.teamIdFK });
        }
        [HttpPut("update")]
        public async Task<IActionResult> update(int id,MUser user)
        {
            try
            {
                var userNew = _rpo.UPdate(id, user);
                return Ok(userNew);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
