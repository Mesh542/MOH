using Microsoft.AspNetCore.Mvc;
using MOH.Data;
using MOH.Models;

namespace MOH.Controllers
{
    public class UserController : Controller
    {
        private readonly MOHDbContext _context;

        public UserController(MOHDbContext context)
        {
            _context = context;
        }

        // GET: User/Signup
        public IActionResult Signup()
        {
            return View();
        }

        // POST: User/Signup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Signup(User user)
        {
            if (ModelState.IsValid)
            {
                user.UserId = Guid.NewGuid(); // generate unique ID
                _context.User.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home"); // after signup go home
            }
            return View(user); // return form with errors if validation fails
        }
    }
}
