using JanSamadhan.Helpers;
using JanSamadhan.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JanSamadhan.Controllers
{
    public class McpOfficerController : Controller
    {
        private readonly IMcpOfficer _mcpOfficerRepo;
        private readonly IIssue _issueRepo;

        public McpOfficerController(IMcpOfficer mcpOfficerRepo, IIssue issueRepo)
        {
            _mcpOfficerRepo = mcpOfficerRepo;
            _issueRepo = issueRepo;
        }

        public IActionResult Index()
        {
            var officers = _mcpOfficerRepo.GetAll();
            return View(officers);
        }

        public IActionResult Details(int id)
        {
            var officer = _mcpOfficerRepo.GetById(id);
            if (officer == null) return NotFound();

            return View(officer);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(McpOfficerRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_mcpOfficerRepo.GetByEmail(model.Email) != null)
                {
                    ModelState.AddModelError("Email", "Email is already in use.");
                    return View(model);
                }

                PasswordHelper.CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);

                var officer = new McpOfficer
                {
                    Name = model.Name,
                    Email = model.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    PhoneNumber = model.PhoneNumber,
                    Designation = model.Designation
                };

                _mcpOfficerRepo.Add(officer);

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
        public IActionResult Login(McpOfficerLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var officer = _mcpOfficerRepo.GetByEmail(model.Email);
                if (officer == null || !PasswordHelper.VerifyPasswordHash(model.Password, officer.PasswordHash, officer.PasswordSalt))
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }

                HttpContext.Session.Remove("UserId");
                HttpContext.Session.SetInt32("OfficerId", officer.Id);
                HttpContext.Session.SetString("OfficerName", officer.Name);


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
            var officerId = HttpContext.Session.GetInt32("OfficerId");
            if (officerId == null)
            {
                return RedirectToAction("Login");
            }
            var issues = _issueRepo.GetAll();
            return View(issues);
        }

        public IActionResult ComplaintDetails(int id)
        {
            var issue = _issueRepo.GetById(id);
            if (issue == null)
            {
                return NotFound();
            }
            return View(issue);
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var officerId = HttpContext.Session.GetInt32("OfficerId");
            if (officerId == null)
            {
                return RedirectToAction("Login");
            }
            var officer = _mcpOfficerRepo.GetById(officerId.Value);
            if (officer == null) return NotFound();

            var model = new UpdateMcpOfficerViewModel
            {
                Name = officer.Name,
                Email = officer.Email,
                PhoneNumber = officer.PhoneNumber,
                Designation = officer.Designation
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProfile(UpdateMcpOfficerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var officerId = HttpContext.Session.GetInt32("OfficerId");
                if (officerId == null)
                {
                    return RedirectToAction("Login");
                }

                var officer = _mcpOfficerRepo.GetById(officerId.Value);
                if (officer == null) return NotFound();

                officer.Name = model.Name;
                officer.Email = model.Email;
                officer.PhoneNumber = model.PhoneNumber;
                officer.Designation = model.Designation;

                _mcpOfficerRepo.Update(officer);

                return RedirectToAction("Profile");
            }

            return View("Profile", model);
        }
    }
}
