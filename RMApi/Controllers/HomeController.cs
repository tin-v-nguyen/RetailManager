using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RMApi.Models;
using System.Diagnostics;

namespace RMApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public HomeController(ILogger<HomeController> logger, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        //#pragma warning disable CS1998
        public async Task<IActionResult> Privacy()
        {
            //string[] roles = { "Admin", "Manager", "Cashier" };
            //foreach (var role in roles)
            //{
            //    var roleExist = await roleManager.RoleExistsAsync(role);

            //    if (roleExist == false)
            //    {
            //        await roleManager.CreateAsync(new IdentityRole(role));
            //    }
            //}

            //var user = await userManager.FindByEmailAsync("tinnguyen2002@gmail.com");

            //if (user != null)
            //{
            //    await userManager.AddToRoleAsync(user, "Admin");
            //    await userManager.AddToRoleAsync(user, "Cashier");
            //}

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
