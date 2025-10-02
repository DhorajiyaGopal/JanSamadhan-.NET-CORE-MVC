using JanSamadhan.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace JanSamadhan.Controllers
{
    public class ReplyController : Controller
    {
        private readonly IReply _replyRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ReplyController(IReply replyRepo, IWebHostEnvironment webHostEnvironment)
        {
            _replyRepo = replyRepo;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var replies = _replyRepo.GetAll();
            return View(replies);
        }

        public IActionResult Details(int id)
        {
            var reply = _replyRepo.GetById(id);
            if (reply == null) return NotFound();

            return View(reply);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(CreateReplyViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                if (model.AttachImage != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.AttachImage.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.AttachImage.CopyTo(fileStream);
                    }
                }

                Reply newReply = new Reply
                {
                    Description = model.Description,
                    McpOfficerId = model.McpOfficerId,
                    IssueId = model.IssueId,
                    AttachImage = uniqueFileName
                };

                _replyRepo.Add(newReply);

                return RedirectToAction("Index"); 
            }

            return View(model);
        }

        private string ProcessUploadedFile(UpdateReplyViewModel model)
        {
            string uniqueFileName = null;

            if (model.AttachImage != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.AttachImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.AttachImage.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var reply = _replyRepo.GetById(id);
            if (reply == null) return NotFound();

            var vm = new UpdateReplyViewModel
            {
                Id = reply.Id, 
                Description = reply.Description,
                ExistingAttachImage = reply.AttachImage
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UpdateReplyViewModel model) 
        {
            if (!ModelState.IsValid) return View(model);

            var reply = _replyRepo.GetById(model.Id);
            if (reply == null) return NotFound();

            reply.Description = model.Description;
            reply.LastUpdatedAt = DateTime.UtcNow;

            if (model.AttachImage != null)
            {
                
                if (model.ExistingAttachImage != null)
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", model.ExistingAttachImage);
                    if (System.IO.File.Exists(filePath)) 
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
                reply.AttachImage = ProcessUploadedFile(model);
            }


            _replyRepo.Update(reply);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var reply = _replyRepo.GetById(id);
            if (reply == null) return NotFound();

            return View(reply);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var reply = _replyRepo.GetById(id);
            if (reply == null) return NotFound();

            _replyRepo.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
