using anaokuluWeb.Data;
using anaokuluWeb.Data.Entity;
using anaokuluWeb.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace anaokuluWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly AnaokuluContext _context = new AnaokuluContext();

        public IActionResult Index()
        {
            
            return View();
        }

        public ActionResult Login()
        {
            if (((System.Security.Claims.ClaimsIdentity)User.Identity).Claims.Any())
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginAsync(LoginViewModel model, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                var user = _context.Kullanicis.Include(x => x.Rol).FirstOrDefault(u => u.KullaniciAdi == model.Username && u.Sifre == model.Password);
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, model.Username),
                        new Claim(ClaimTypes.Name, model.Username),
                        new Claim(ClaimTypes.Sid, user.Id.ToString()),
                        //new Claim(ClaimTypes.Email, "email")
                        //new Claim("FullName", user.FullName),
                        new Claim(ClaimTypes.Role, user.Rol.Ad),
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    { };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                                  new ClaimsPrincipal(claimsIdentity),
                                                  authProperties);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Yanlış Kullanıcı Adı veya Şifre");
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }



    }
}
