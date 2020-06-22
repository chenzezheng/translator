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

        string DecodeFromBase64(string str)
        {
            byte[] temp = Convert.FromBase64String(str);  
            return System.Text.Encoding.Default.GetString(temp);
        }

        //登录 localhost:5000/user/login?userid=czz&password=123456
        [HttpGet("login")]
        public ActionResult<User> GetLogin(string userid, string password)
        {
            password = DecodeFromBase64(password);
            User newUser = null;
            try
            {
                newUser = userService.UserLogin(userid, password);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return newUser;
        }


        //注册 localhost:5000/user/register?userid=czz&password=123456
        [HttpPost("register")]
        public ActionResult<User> PostRegister(User newUser)
        {
            try
            {
                newUser = userService.UserRegister(newUser.UserID, newUser.Password);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return newUser;
        }
    }
}
