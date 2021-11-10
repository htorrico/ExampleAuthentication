using ExampleLogin.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExampleLogin.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;


        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Index(AuthenticateViewModel model, string ReturnUrl)
        {
            try
            {
                model.Role = "Admin";
                model.UserId = 1;
                if (!string.IsNullOrEmpty(model.Password) && !string.IsNullOrEmpty(model.Username))
                {
                    //if (this._authService.Authenticate(model.Username, model.Password))
                        if (  model.Username =="htorrico" &&  model.Password=="123456")
                    {
                        var claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, Convert.ToString(model.UserId)),
                        new Claim(ClaimTypes.Name, model.Username),
                        new Claim(ClaimTypes.Role, model.Role),
                        new Claim("FavoriteDrink", "Tea")
                };
                        //Initialize a new instance of the ClaimsIdentity with the claims and authentication scheme    
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity    
                        var principal = new ClaimsPrincipal(identity);
                        //SignInAsync is a Extension method for Sign in a principal for the specified scheme.    
                         HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                        {
                            //IsPersistent = objLoginModel.RememberLogin
                            IsPersistent = false
                         });




                        return RedirectToAction("Index", "Home");
                        //if (user.RoleId == 2)
                        //{
                        //    return RedirectToAction("Index", "Home");
                        //}
                        //else
                        //{
                        //    return RedirectToAction("Dashboard", "Padres");
                        //}
                    }
                    else
                    {
                        ModelState.AddModelError("logiInval", "Username or password invalid.");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "You must introduce username and password.");
                    return View(model);
                }
            }
            catch (System.Exception e)
            {
                return View(model);
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            //SignOutAsync is Extension method for SignOut    
             HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //Redirect to home page    
            return LocalRedirect("/");
        }

    }
}
