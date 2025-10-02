using JanSamadhan.Models;
using Microsoft.AspNetCore.Mvc;

namespace JanSamadhan.Controllers
{
    public class McpOfficerController : Controller
    {
        private readonly IMcpOfficer _mcpOfficerRepo;

        public McpOfficerController(IMcpOfficer mcpOfficerRepo)
        {
            _mcpOfficerRepo = mcpOfficerRepo;
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

        public IActionResult Create()
        {
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateMcpOfficerViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var officer = new McpOfficer
            {
                Name = model.Name,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Designation = model.Designation,
                Password = model.Password
            };

            _mcpOfficerRepo.Add(officer);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var officer = _mcpOfficerRepo.GetById(id);
            if (officer == null) return NotFound();

            var vm = new UpdateMcpOfficerViewModel
            {
                Name = officer.Name,
                Email = officer.Email,
                PhoneNumber = officer.PhoneNumber,
                Designation = officer.Designation
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UpdateMcpOfficerViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var officer = _mcpOfficerRepo.GetById(model.Id);
            if (officer == null) return NotFound();

            officer.Name = model.Name;
            officer.Email = model.Email;
            officer.PhoneNumber = model.PhoneNumber;
            officer.Designation = model.Designation;

            _mcpOfficerRepo.Update(officer);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var officer = _mcpOfficerRepo.GetById(id);
            if (officer == null) return NotFound();

            return View(officer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var officer = _mcpOfficerRepo.GetById(id);
            if (officer == null) return NotFound();

            _mcpOfficerRepo.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
