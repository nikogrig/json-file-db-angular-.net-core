using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using src.DataAccess.DTO;
using src.Services;
using src.ViewModels;

namespace src.Controllers
{
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            this._accountService = accountService;
        }

        // /api/account/login
        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody]UserLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = this._accountService.Login(model);

            if(result == null) 
            {
                return Unauthorized();
            }

            return Ok(result);
        }

        // /api/account/register
        [Route("register")]
        [HttpPost]
        public IActionResult Register([FromBody]UserRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = this._accountService.Register(model);

            if(result == null) 
            {
                return Unauthorized();
            }

            return Ok(result);
        }

    }
}