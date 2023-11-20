using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_StudentsRating.Data;
using MVC_StudentsRating.Models;

namespace MVC_StudentsRating.Controllers
{
	public class RatingController : Controller
	{
		private readonly SchoolContext _db;
		public RatingController(SchoolContext db)
		{
			_db = db;
		}

		public IActionResult Index()
		{
			IEnumerable<Rating> studentList = _db.Ratings.Include(prop => prop.Student).
				Include(prop => prop.Teacher).Include(prop => prop.Subject).OrderBy(prop => prop.ID);
			return View(studentList);
		}

        public IActionResult Create()
        {
            ViewBag.Students = new SelectList(_db.Students, "ID", "Fullname");
            ViewBag.Teachers = new SelectList(_db.Teachers, "ID", "Name");
            ViewBag.Subjects = new SelectList(_db.Subjects, "ID", "Name", "Rating");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Rating rating)
        {
            if (rating.StudentID != 0 && rating.SubjectID != 0 && rating.TeacherID != 0)
            {
                await _db.Ratings.AddAsync(rating);
                await _db.SaveChangesAsync();
                TempData["success"] = "New information about rating was added successfully";
                return RedirectToAction("Index");
            }

            ViewBag.Students = new SelectList(_db.Students, "ID", "Fullname");
            ViewBag.Teachers = new SelectList(_db.Teachers, "ID", "Name");
            ViewBag.Subjects = new SelectList(_db.Subjects, "ID", "Name", "Rating");

            return View(rating);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || id <= 0)
			{
				return NotFound();
			}
			var ratingFromDb = await _db.Ratings.FindAsync(id);

			if (ratingFromDb == null)
			{
				return NotFound();
			}

			ViewBag.Students = new SelectList(_db.Students, "ID", "Fullname", ratingFromDb.StudentID);
			ViewBag.Teachers = new SelectList(_db.Teachers, "ID", "Name", ratingFromDb.TeacherID);
			ViewBag.Subjects = new SelectList(_db.Subjects, "ID", "Name", ratingFromDb.SubjectID);

			return View(ratingFromDb);
		}

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> Edit(int id, RatingDB updatedRating)
        {
            if (id <= 0)
            {
                return NotFound();
            }

        
            var ratingFromDb = await _db.Ratings.FindAsync(id);

            if (ratingFromDb == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
			{ 

                ratingFromDb.StudentID = updatedRating.StudentID;
                ratingFromDb.SubjectID = updatedRating.SubjectID;
                ratingFromDb.Month = updatedRating.Month;
                ratingFromDb.CurrentRating = updatedRating.CurrentRating;
                ratingFromDb.TeacherID = updatedRating.TeacherID;

                _db.Ratings.Update(ratingFromDb);
                await _db.SaveChangesAsync();

                TempData["success"] = "Rating was updated successfully";
                return RedirectToAction("Index");
            }

            ViewBag.Students = new SelectList(_db.Students, "ID", "Fullname", ratingFromDb.StudentID);
            ViewBag.Teachers = new SelectList(_db.Teachers, "ID", "Name", ratingFromDb.TeacherID);
            ViewBag.Subjects = new SelectList(_db.Subjects, "ID", "Name", ratingFromDb.SubjectID);

            return View(updatedRating);
        }


        public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || id <= 0)
			{
				return NotFound();
			}

			var ratingFromDb = await _db.Ratings.FindAsync(id);

			if (ratingFromDb == null)
			{
				return NotFound();
			}

			return View(ratingFromDb);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeletePOST(int? id)
		{
			var ratingFromDb = await _db.Ratings.FindAsync(id);

			if (ratingFromDb == null)
			{
				return NotFound();
			}
			_db.Ratings.Remove(ratingFromDb);
			await _db.SaveChangesAsync();
			TempData["success"] = "Rating was deleted successfully";
			return RedirectToAction("Index");
		}

	}
}
