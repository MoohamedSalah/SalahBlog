using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalahBlog.Models;
using SalahBlog.ViewModels;

namespace SalahBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly INotyfService _notyfService;
        public UserController(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager,
                              INotyfService notyfService) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _notyfService = notyfService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View(new LoginVM());
        }  
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if(!ModelState.IsValid) { return View(loginVM); }
            var userExist=await _userManager.Users.FirstOrDefaultAsync(x=>x.UserName==loginVM.Username);
            if (userExist == null) 
            {
                _notyfService.Error("Username does not exist");
                return View(loginVM); 
            }
            var checkPassword = await _userManager.CheckPasswordAsync(userExist, loginVM.Password);
            if(!checkPassword)
            {
                _notyfService.Error("password does not match");
                return View(loginVM);
            }
            await _signInManager.PasswordSignInAsync(loginVM.Username, loginVM.Password,loginVM.RememberMe, true);
            _notyfService.Success("Login successful");
            return RedirectToAction("Index", "User",new {area="Admin"});
        }
    }
}
