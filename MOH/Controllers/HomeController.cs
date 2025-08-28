using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MOH.Data;
using MOH.Models;

namespace MOH.Controllers
{
    public class HomeController : Controller
    {
        private readonly MOHDbContext _context;

        public HomeController(MOHDbContext context)
        {
            _context = context;
        }

        // GET: Login page
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Process login
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _context.User
                .FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

            if (user != null)
            {
                TempData["Username"] = user.Username;
                return RedirectToAction("Welcome");
            }

            ViewBag.Error = "Invalid Username or Password!";
            return View(model);
        }

        // Welcome page
        public IActionResult Welcome()
        {
            if (TempData["Username"] == null)
                return RedirectToAction("Login");

            // Keep TempData for view
            TempData.Keep("Username");
            return View();
        }

        public IActionResult Logout()
        {
            // Clear session/TempData
            TempData.Clear();

            // Redirect to login page
            return RedirectToAction("Login");
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Users()
        {
            List<User> users = _context.User.ToList();
            return View(users);
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            var hasher = new Microsoft.AspNetCore.Identity.PasswordHasher<User>();
            user.Password = hasher.HashPassword(user, user.Password);

            user.UserId = Guid.NewGuid();
            _context.User.Add(user);
            _context.SaveChanges();

            return RedirectToAction(nameof(Users));
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

