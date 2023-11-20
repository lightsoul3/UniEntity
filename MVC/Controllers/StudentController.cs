using Microsoft.AspNetCore.Mvc;
using MVC_StudentsRating.Models;
using System.Data.Common;
using MVC_StudentsRating.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MVC_StudentsRating.Controllers
{
	public class StudentController : Controller
	{
		private readonly SchoolContext _db;
		public StudentController(SchoolContext db)
		{
			_db = db;
		}

		public IActionResult Index()
		{
			IEnumerable<Student> studentList = _db.Students;
			return View(studentList);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Student student)
		{
			if (ModelState.IsValid)
			{
				await _db.Students.AddAsync(student);
				await _db.SaveChangesAsync();
				TempData["success"] = "Student was added successfully";
				return RedirectToAction("Index");
			}
			return View(student);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || id <= 0)
			{
				return NotFound();
			}

			var studentFromDb = await _db.Students.FindAsync(id);

			if (studentFromDb == null)
			{
				return NotFound();
			}

			return View(studentFromDb);
		}

		[HttpPost, ActionName("Edit")]
		public async Task<IActionResult> Edit(int id, Student updatedStudent)
		{
			if (id <= 0)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				var studentFromDb = await _db.Students.FindAsync(id);

				if (studentFromDb == null)
				{
					return NotFound();
				}
				studentFromDb.Fullname = updatedStudent.Fullname;
				studentFromDb.Address = updatedStudent.Address;
				studentFromDb.Phone = updatedStudent.Phone;
				studentFromDb.DateOfBirth = updatedStudent.DateOfBirth;

				_db.Students.Update(studentFromDb);
				await _db.SaveChangesAsync();
				TempData["success"] = "Student was updated successfully";
				return RedirectToAction("Index");

			}

			return View(updatedStudent);
		}

		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || id <= 0)
			{
				return NotFound();
			}

			var studentFromDb = await _db.Students.FindAsync(id);

			if (studentFromDb == null)
			{
				return NotFound();
			}

			return View(studentFromDb);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeletePOST(int? id)
		{
			var studentFromDb = await _db.Students.FindAsync(id);

			if (studentFromDb == null)
			{
				return NotFound();
			}
			_db.Students.Remove(studentFromDb);
			await _db.SaveChangesAsync();
			TempData["success"] = "Students was deleted successfully";
			return RedirectToAction("Index");
		}
	}
}