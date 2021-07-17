using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using grad2021.Data;
using grad2021.Models;

namespace grad2021.Controllers
{
    public class StudentEnrollmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentEnrollmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StudentEnrollments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.StudentEnrollments.Include(s => s.AcademicYear).Include(s => s.ApplicationUser).Include(s => s.Branch).Include(s => s.Level);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: StudentEnrollments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentEnrollment = await _context.StudentEnrollments
                .Include(s => s.AcademicYear)
                .Include(s => s.ApplicationUser)
                .Include(s => s.Branch)
                .Include(s => s.Level)
                .FirstOrDefaultAsync(m => m.StudentEnrollmentID == id);
            if (studentEnrollment == null)
            {
                return NotFound();
            }

            return View(studentEnrollment);
        }

        // GET: StudentEnrollments/Create
        public IActionResult Create()
        {
            ViewData["AcademicYearID"] = new SelectList(_context.AcademicYears, "AcademicYearID", "AcademicYearID");
            ViewData["ApplicationUserID"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["BranchName"] = new SelectList(_context.Branches, "BranchName", "BranchName");
            ViewData["LevelName"] = new SelectList(_context.Levels, "LevelName", "LevelName");
            return View();
        }

        // POST: StudentEnrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentEnrollmentID,ApplicationUserID,BranchName,AcademicYearID,LevelName,CompleteLevelMark,IsNovember,FailureNo,YearPass")] StudentEnrollment studentEnrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentEnrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AcademicYearID"] = new SelectList(_context.AcademicYears, "AcademicYearID", "AcademicYearID", studentEnrollment.AcademicYearID);
            ViewData["ApplicationUserID"] = new SelectList(_context.Users, "Id", "Id", studentEnrollment.ApplicationUserID);
            ViewData["BranchName"] = new SelectList(_context.Branches, "BranchName", "BranchName", studentEnrollment.BranchName);
            ViewData["LevelName"] = new SelectList(_context.Levels, "LevelName", "LevelName", studentEnrollment.LevelName);
            return View(studentEnrollment);
        }

        // GET: StudentEnrollments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentEnrollment = await _context.StudentEnrollments.FindAsync(id);
            if (studentEnrollment == null)
            {
                return NotFound();
            }
            ViewData["AcademicYearID"] = new SelectList(_context.AcademicYears, "AcademicYearID", "AcademicYearID", studentEnrollment.AcademicYearID);
            ViewData["ApplicationUserID"] = new SelectList(_context.Users, "Id", "Id", studentEnrollment.ApplicationUserID);
            ViewData["BranchName"] = new SelectList(_context.Branches, "BranchName", "BranchName", studentEnrollment.BranchName);
            ViewData["LevelName"] = new SelectList(_context.Levels, "LevelName", "LevelName", studentEnrollment.LevelName);
            return View(studentEnrollment);
        }

        // POST: StudentEnrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentEnrollmentID,ApplicationUserID,BranchName,AcademicYearID,LevelName,CompleteLevelMark,IsNovember,FailureNo,YearPass")] StudentEnrollment studentEnrollment)
        {
            if (id != studentEnrollment.StudentEnrollmentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentEnrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentEnrollmentExists(studentEnrollment.StudentEnrollmentID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AcademicYearID"] = new SelectList(_context.AcademicYears, "AcademicYearID", "AcademicYearID", studentEnrollment.AcademicYearID);
            ViewData["ApplicationUserID"] = new SelectList(_context.Users, "Id", "Id", studentEnrollment.ApplicationUserID);
            ViewData["BranchName"] = new SelectList(_context.Branches, "BranchName", "BranchName", studentEnrollment.BranchName);
            ViewData["LevelName"] = new SelectList(_context.Levels, "LevelName", "LevelName", studentEnrollment.LevelName);
            return View(studentEnrollment);
        }

        // GET: StudentEnrollments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentEnrollment = await _context.StudentEnrollments
                .Include(s => s.AcademicYear)
                .Include(s => s.ApplicationUser)
                .Include(s => s.Branch)
                .Include(s => s.Level)
                .FirstOrDefaultAsync(m => m.StudentEnrollmentID == id);
            if (studentEnrollment == null)
            {
                return NotFound();
            }

            return View(studentEnrollment);
        }

        // POST: StudentEnrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentEnrollment = await _context.StudentEnrollments.FindAsync(id);
            _context.StudentEnrollments.Remove(studentEnrollment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentEnrollmentExists(int id)
        {
            return _context.StudentEnrollments.Any(e => e.StudentEnrollmentID == id);
        }
    }
}
