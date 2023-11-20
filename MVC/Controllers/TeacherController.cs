using Microsoft.AspNetCore.Mvc;
using MVC_StudentsRating.Models;
using System.Data.Common;
using MVC_StudentsRating.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MVC_StudentsRating.Controllers
{
    public class TeacherController : Controller
    {
        private readonly SchoolContext _db;
        public TeacherController(SchoolContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Teacher> teachersList = _db.Teachers;
            return View(teachersList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                await _db.Teachers.AddAsync(teacher);
                await _db.SaveChangesAsync();
                TempData["success"] = "Techer was added successfully";
                return RedirectToAction("Index");
            }
            return View(teacher);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            var teacherFromDb = await _db.Teachers.FindAsync(id);

            if (teacherFromDb == null)
            {
                return NotFound();
            }

            return View(teacherFromDb);
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> Edit(int id, Teacher updatedTeacher)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var teacherFromDb = await _db.Teachers.FindAsync(id);

                if (teacherFromDb == null)
                {
                    return NotFound();
                }
                teacherFromDb.Name = updatedTeacher.Name;

                _db.Teachers.Update(teacherFromDb);
                await _db.SaveChangesAsync();
                TempData["success"] = "Teacher was updated successfully";
                return RedirectToAction("Index");

            }

            return View(updatedTeacher);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            var teacherFromDb = await _db.Teachers.FindAsync(id);

            if (teacherFromDb == null)
            {
                return NotFound();
            }

            return View(teacherFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePOST(int? id)
        {
            var teacherFromDb = await _db.Teachers.FindAsync(id);

            if (teacherFromDb == null)
            {
                return NotFound();
            }
            _db.Teachers.Remove(teacherFromDb);
            await _db.SaveChangesAsync();
            TempData["success"] = "Tecaher was deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
