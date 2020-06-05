using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TranslatorApi.Models;
using TranslatorApi.Services;

namespace TranslatorApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private UserService userService;

        
        public UserController(DataContext context)
        {
            this.userService = new UserService(context);
        }

        //登录 localhost:5000/user/login?userid=czz&password=123456
        [HttpGet("login")]
        public ActionResult<Token> GetLogin(string userid, string password)
        {
            return userService.UserLogin(userid, password);
        }

        //根据tokenid返回userid localhost:5000/user/userid?token=R864B2
        [HttpGet("userid")]
        public ActionResult<string> GetUserID(string token)
        {
            return userService.GetUserID(token);
        }

        //注册 localhost:5000/user/register
        [HttpPost("register")]
        public ActionResult<User> PostRegister(User newUser)
        {
            return userService.UserRegister(newUser.UserID, newUser.Password);
        }
    }
}
