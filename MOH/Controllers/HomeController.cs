using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MOH.Data;
using MOH.Models;

namespace MOH.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MOHDbContext _context;

        public HomeController(ILogger<HomeController> logger, MOHDbContext context)
        {
            _logger = logger;
            _context = context;
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
