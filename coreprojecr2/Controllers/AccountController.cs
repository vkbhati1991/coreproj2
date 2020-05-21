using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using coreprojecr2.Models;
using coreprojecr2.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
namespace coreprojecr2.Controllers
{
    public class AccountController : Controller
    {
        string connectionstring = ConnectionString.CName;
        private RegisterDataAccess registerDataAccess = null;

        public AccountController()
        {
            registerDataAccess = new RegisterDataAccess();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(Login model, string returnUrl)
        {
            IEnumerable<Register> users = registerDataAccess.GetAllUsers();
            bool isValid = false;

            Register user = users.Where(user => user.Email == model.Email && user.Password == model.Password).SingleOrDefault();
            if (user != null)
            {
                isValid = true;
              
            }
            if (ModelState.IsValid && isValid)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, user.Email));

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                var props = new AuthenticationProperties();
                props.IsPersistent = true;

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();
                return RedirectToAction("start", "student");

            }

            ModelState.AddModelError("", "Invalid login Attempt");
            return View(model);
        }

        [HttpGet]
        public IActionResult Users()
        {
            IEnumerable<Register> users = registerDataAccess.GetAllUsers();
            return View(users);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Register(Register user)
        {
            try {
                if (ModelState.IsValid)
                {
                    registerDataAccess.AddUser(user);
                    return RedirectToAction("start", "student");
                }
                return View(user);
            }
            catch {
                return View();
            }
           
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("login", "account");
        }
    }
}