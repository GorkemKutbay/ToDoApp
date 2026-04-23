using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Core.DTOs.Auth;
using ToDoApp.Core.Entities;
using ToDoApp.DataAccess.Abstract;

namespace ToDoApp.Mvc.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _auth;
        private readonly SignInManager<AppUser> _user;

        public AuthController(IAuthService auth, SignInManager<AppUser> user)
        {
            _auth = auth;
            _user = user;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _auth.RegisterAsync(user);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginDto login)
        {
            if(!ModelState.IsValid)
            {
                return View(login);
            }
            try
            {
                await _auth.LoginAsync(login);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(login);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _user.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
