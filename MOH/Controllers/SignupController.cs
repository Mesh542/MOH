using Microsoft.AspNetCore.Mvc;
using MOH.Data;   // for MOHDbContext
using MOH.Models;

namespace MOH.Controllers
{
    public class SignupController : Controller
    {
        private readonly MOHDbContext _context;

        public SignupController(MOHDbContext context)
        {
            _context = context;
        }

        // GET: Signup (shows the form)
        public IActionResult Index()
        {
            return View();
        }

        // POST: Signup (handles form submission)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(User user)
        {
            if (ModelState.IsValid)
            {
                user.UserId = Guid.NewGuid(); // assign new ID
                _context.User.Add(user);
                _context.SaveChanges();

                // after successful signup, redirect (e.g., to home page)
                return RedirectToAction("Index", "Home");
            }

            // if validation fails, return form with errors
            return View(user);
        }
    }
}
