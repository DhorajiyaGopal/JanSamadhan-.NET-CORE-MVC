using JanSamadhan.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace JanSamadhan.Controllers
{
    public class IssueController : Controller
    {
        private readonly IIssue _issueRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public IssueController(IIssue issueRepo, IWebHostEnvironment webHostEnvironment)
        {
            _issueRepo = issueRepo;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Issues
        public IActionResult Index()
        {
            var issues = _issueRepo.GetAll();
            return View(issues);
        }

        // GET: Issues/Details/5
        public IActionResult Details(int id)
        {
            var issue = _issueRepo.GetById(id);
            if (issue == null) return NotFound();

            return View(issue);
        }

        // GET: Issues/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Issues/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateIssueViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                if (model.RelatedImage != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.RelatedImage.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.RelatedImage.CopyTo(fileStream);
                    }
                }

                var issue = new Issue
                {
                    Title = model.Title,
                    Description = model.Description,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    UserId = model.UserId,
                    RelatedImageUrl = uniqueFileName
                };

                _issueRepo.Add(issue);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Issues/Edit/5
        public IActionResult Edit(int id)
        {
            var issue = _issueRepo.GetById(id);
            if (issue == null) return NotFound();

            var model = new UpdateIssueViewModel
            {
                Id = issue.Id,
                Title = issue.Title,
                Description = issue.Description,
                UserId = issue.UserId
            };

            return View(model);
        }

        // POST: Issues/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UpdateIssueViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var issue = _issueRepo.GetById(model.Id);
            if (issue == null) return NotFound();

            issue.Title = model.Title;
            issue.Description = model.Description;
            issue.UpdatedAt = DateTime.UtcNow;
            issue.UserId = model.UserId;

            if (model.RelatedImage != null)
            {
                // delete old image if exists
                if (!string.IsNullOrEmpty(issue.RelatedImageUrl))
                {
                    string oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", issue.RelatedImageUrl);
                    if (System.IO.File.Exists(oldFilePath))
                        System.IO.File.Delete(oldFilePath);
                }

                // save new image
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.RelatedImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.RelatedImage.CopyTo(fileStream);
                }

                issue.RelatedImageUrl = uniqueFileName;
            }

            _issueRepo.Update(issue);
            return RedirectToAction(nameof(Index));
        }

        // GET: Issues/Delete/5
        public IActionResult Delete(int id)
        {
            var issue = _issueRepo.GetById(id);
            if (issue == null) return NotFound();

            return View(issue);
        }

        // POST: Issues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var issue = _issueRepo.GetById(id);
            if (issue == null) return NotFound();

            if (!string.IsNullOrEmpty(issue.RelatedImageUrl))
            {
                string oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", issue.RelatedImageUrl);
                if (System.IO.File.Exists(oldFilePath))
                    System.IO.File.Delete(oldFilePath);
            }

            _issueRepo.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
