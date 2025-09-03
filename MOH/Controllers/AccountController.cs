using Microsoft.AspNetCore.Mvc;
using MOH.Models;
using MOH.Data;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Linq;

namespace MOH.Controllers
{
    public class AccountController : Controller
    {
        private readonly MOHDbContext _context;

        public AccountController(MOHDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Signup
        public IActionResult Signup()
        {
            return View();
        }

        // POST: /Account/Signup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Signup(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // ✅ Check if Email already exists
                if (_context.User.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "Email already exists.");
                    return View(model);
                }

                // ✅ Check if Username already exists
                if (_context.User.Any(u => u.Username == model.Username))
                {
                    ModelState.AddModelError("Username", "Username already exists.");
                    return View(model);
                }

                // ✅ Generate salt
                byte[] salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }

                // ✅ Hash the password
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: model.Password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

                // ✅ Save user as deactivated by default
                var user = new User
                {
                    UserId = Guid.NewGuid(),
                    Name = model.Name,
                    Surname = model.Surname,
                    Age = model.Age,
                    Gender = model.Gender,
                    Email = model.Email,
                    Username = model.Username,
                    Password = hashed,
                    IsActive = false // 👈 add this field in User.cs
                };

                _context.User.Add(user);
                _context.SaveChanges();

                // redirect to login or success page
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
