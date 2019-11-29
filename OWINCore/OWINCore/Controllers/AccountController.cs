using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using OWINCore.Common;
using OWINCore.Data;
using OWINCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace OWINCore.Controllers
{
    public class AccountController : ApiController
    {


        private readonly UserManagerUtil _appUserManager1;
        public AccountController(UserManagerUtil appUserManager1)
        {
            _appUserManager1 = appUserManager1;
        }

        [System.Web.Http.Route("api/User/Register")]
        [System.Web.Http.HttpPost]
        [EnableCors("MyPolicy")]

        public IActionResult Register(AccountModel model)
        {
            var user = new ApplicationUser() { UserName = model.UserName, Email = model.Email };
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            //_appUserManager1.PasswordValidators = new PasswordValidator
            //{
            //    RequiredLength = 3
            //};

            var result = _appUserManager1.CreateAsync(user, model.Password);
            return (IActionResult)result;
        }



        [System.Web.Http.HttpGet]
        [Authorize]
        [Microsoft.AspNetCore.Mvc.Route("api/GetUserClaims")]
        public AccountModel GetUserClaims()
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identityClaims.Claims;
            AccountModel model = new AccountModel()
            {
                UserName = identityClaims.FindFirst("Username").Value,
                Email = identityClaims.FindFirst("Email").Value,
                FirstName = identityClaims.FindFirst("FirstName").Value,
                LastName = identityClaims.FindFirst("LastName").Value,
                //LoggedOn = identityClaims.FindFirst("LoggedOn").Value
            };
            return model;
        }

    }
}
