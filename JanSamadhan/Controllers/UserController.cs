using JanSamadhan.Helpers;
using JanSamadhan.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace JanSamadhan.Controllers
{
    public class UserController : Controller
    {
        private readonly IUser _userRepo;
        private readonly IIssue _issueRepo; // Add this line
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserController(IUser userRepo, IIssue issueRepo, IWebHostEnvironment webHostEnvironment) // Modify this line
        {
            _userRepo = userRepo;
            _issueRepo = issueRepo; // Add this line
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var users = _userRepo.GetAll();
            return View(users);
        }

        public IActionResult Details(int id)
        {
            var user = _userRepo.GetById(id);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_userRepo.GetByEmail(model.Email) != null)
                {
                    ModelState.AddModelError("Email", "Email is already in use.");
                    return View(model);
                }

                PasswordHelper.CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);

                var user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Address = model.Address,
                    Phone = model.Phone
                };

                _userRepo.Add(user);

                return RedirectToAction("Login");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userRepo.GetByEmail(model.Email);
                if (user == null || !PasswordHelper.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }

                HttpContext.Session.Remove("OfficerId");
                HttpContext.Session.Remove("OfficerName");
                HttpContext.Session.SetInt32("UserId", user.Id);

                return RedirectToAction("Dashboard");
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Dashboard()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            var issues = _issueRepo.GetByUserId(userId.Value);
            return View(issues);
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            var user = _userRepo.GetById(userId.Value);
            if (user == null) return NotFound();

            var model = new UpdateUserViewModel
            {
                Name = user.Name,
                Email = user.Email,
                Address = user.Address,
                Phone = user.Phone
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProfile(UpdateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                if (userId == null)
                {
                    return RedirectToAction("Login");
                }

                var user = _userRepo.GetById(userId.Value);
                if (user == null) return NotFound();

                user.Name = model.Name;
                user.Email = model.Email;
                user.Address = model.Address;
                user.Phone = model.Phone;

                _userRepo.Update(user);

                return RedirectToAction("Profile");
            }

            return View("Profile", model);
        }

        // Existing Edit and Delete actions would go here, but will need to be secured.
    }
}
