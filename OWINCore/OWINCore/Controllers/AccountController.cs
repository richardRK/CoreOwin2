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
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace OWINCore.Controllers
{
    public class AccountController : ApiController
    {


        private readonly UserManagerUtil _appUserManager1;

        private readonly RoleManagerUtil _roleManagerUtil;
        public AccountController(UserManagerUtil appUserManager1, RoleManagerUtil roleManagerUtil)
        {
            _appUserManager1 = appUserManager1;
            _roleManagerUtil = roleManagerUtil;
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
            _appUserManager1.AddToRoleAsync(user,model.Role);
            return (IActionResult)result;
        }



        [System.Web.Http.HttpGet]
        [Authorize]
        [System.Web.Http.Route("api/GetUserClaims")]
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


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetAllRoles")]
        [AllowAnonymous]
        public HttpResponseMessage GetRoles()
        {
            //var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
            //var roleMngr = new RoleManager<IdentityRole>(roleStore);
            var roles = _roleManagerUtil.Roles
                .Select(x => new { x.Id, x.Name })
                .ToList();
            return this.Request.CreateResponse(HttpStatusCode.OK, roles);
        }

    }
}
