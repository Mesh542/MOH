using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MOH.Data;
using MOH.Models;
using static MOH.Models.Enum;

namespace MOH.Controllers
{
    public class AdminController : Controller
    {


        private readonly ILogger<HomeController> _logger;
        private readonly MOHDbContext _context;

        public AdminController(ILogger<HomeController> logger, MOHDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
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

        public IActionResult DeleteUser(Guid id)
        {
            User? user = _context.User.Find(id);
            if (user != null)
            {
                _context.User.Remove(user);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Users));
        }


        
        public IActionResult ActivateUser(Guid id)
        {
            User? user = _context.User.Find(id);
            if (user != null)
            {
                user.status = user.status == Status.Active ? Status.Inactive : Status.Active;
                _context.User.Update(user);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Users));
        }
    }
}
