using Microsoft.AspNetCore.Mvc;
using MVC_StudentsRating.Models;
using System.Data.Common;
using MVC_StudentsRating.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MVC_StudentsRating.Controllers
{
    public class SubjectController : Controller
    {
        private readonly SchoolContext _db;
        public SubjectController(SchoolContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Subject> subjectList = _db.Subjects;
            return View(subjectList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Subject subject)
        {
            if (ModelState.IsValid)
            {
                await _db.Subjects.AddAsync(subject);
                await _db.SaveChangesAsync();
                TempData["success"] = "Subject was added successfully";
                return RedirectToAction("Index");
            }
            return View(subject);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            var subjectFromDb = await _db.Subjects.FindAsync(id);

            if (subjectFromDb == null)
            {
                return NotFound();
            }

            return View(subjectFromDb);
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> Edit(int id, Subject updatedSubject)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var subjectFromDb = await _db.Subjects.FindAsync(id);

                if (subjectFromDb == null)
                {
                    return NotFound();
                }
                subjectFromDb.Name = updatedSubject.Name;
                subjectFromDb.Rating= updatedSubject.Rating;

                _db.Subjects.Update(subjectFromDb);
                await _db.SaveChangesAsync();
                TempData["success"] = "Subject was updated successfully";
                return RedirectToAction("Index");

            }

            return View(updatedSubject);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            var subjectFromDb = await _db.Subjects.FindAsync(id);

            if (subjectFromDb == null)
            {
                return NotFound();
            }

            return View(subjectFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePOST(int? id)
        {
            var subjectFromDb = await _db.Subjects.FindAsync(id);

            if (subjectFromDb == null)
            {
                return NotFound();
            }
            _db.Subjects.Remove(subjectFromDb);
            await _db.SaveChangesAsync();
            TempData["success"] = "Subject was deleted successfully";
            return RedirectToAction("Index");
        }
    }
}