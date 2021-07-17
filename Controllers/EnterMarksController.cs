using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using grad2021.Data;
using grad2021.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace grad2021.Controllers
{
    [Authorize(Roles = "ControlAdmin")]
    public class EnterMarksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AcademicYearsController> _logger;

        public EnterMarksController(ApplicationDbContext context,
            ILogger<AcademicYearsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string BranchName, string LevelName)
        {
            var academicYear = await _context.AcademicYears
                .Include(a => a.StudentEnrollments)
                .ThenInclude(a => a.ApplicationUser)
                .OrderByDescending(a => a.AcademicYearID).FirstAsync();
            ViewData["BranchFilter"] = "";
            ViewData["LevelFilter"] = "";
            if (BranchName != null && LevelName != null)
            {
                var courseEnrollments = await _context.CourseEnrollments
                    .Where(a => a.BranchName == BranchName && a.LevelName == LevelName).ToListAsync();
                ViewData["BranchFilter"] = BranchName;
                ViewData["LevelFilter"] = LevelName;
                academicYear.CourseEnrollments = courseEnrollments;
            }
            ViewData["Title"] = "إدخال درجات الطلاب";
            var branches = await _context.Branches.ToListAsync();
            academicYear.Branches = branches;
            var levels = await _context.Levels.ToListAsync();
            academicYear.Levels = levels;
            return View(academicYear);
        }

        // GET: StudentEnrollments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var thisYearObject = await _context.AcademicYears
                .OrderByDescending(a => a.AcademicYearID).FirstAsync();
            var thisYear = thisYearObject.AcademicYearID;
            var studentCourses = await _context.StudentCourses
                .Include(a => a.ApplicationUser)
                .Include(a => a.CourseEnrollment)
                .ThenInclude(a => a.Course)
                .Where(a => a.AcademicYearID == thisYear &&
                a.CourseEnrollmentID == id &&
                a.AcademicYearID == a.StudentEnrollment.AcademicYearID)
                .ToListAsync();
            if (!studentCourses.Any())
            {
                return NotFound();
            }
            return View(studentCourses);
        }

        //POST: StudentEnrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, List<StudentCourse> studentCourses)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation($"Update database with studentcourses");

                    _context.UpdateRange(studentCourses);
                    await _context.SaveChangesAsync();
                    for (int i = 0; i < studentCourses.Count; i++)
                    {
                        
                        StudentCourse sc = await _context.StudentCourses
                            .Include(a => a.ApplicationUser).ThenInclude(b => b.StudentCourses).ThenInclude(a => a.CourseEnrollment).ThenInclude(a => a.Course)
                            .Include(a => a.StudentEnrollment)
                            .Include(a => a.CourseEnrollment)
                            .ThenInclude(a => a.Course)
                            .Where(a => a.StudentCourseID == studentCourses[i].StudentCourseID)
                            .FirstAsync();

                        //Course Grade Calculation
                        double ratio = studentCourses[i].TotalMark / sc.CourseEnrollment.Course.FullMark;
                        double finalRatio = 3 * sc.FinalExamMark / sc.CourseEnrollment.Course.TermExamMaxScore;
                        if (finalRatio < 1)
                        {
                            sc.CourseGrade = CourseGrade.راسب_لائحة;
                            sc.ApplicationUser.HonourDegree = false;
                        }
                        else if (ratio >= 0.85) { sc.CourseGrade = CourseGrade.امتياز; }
                        else if (ratio >= 0.75) { sc.CourseGrade = CourseGrade.جيد_جدا; }
                        else if (ratio >= 0.65) { sc.CourseGrade = CourseGrade.جيد; }
                        else if (ratio >= 0.5) { sc.CourseGrade = CourseGrade.مقبول; }
                        else
                        {
                            sc.CourseGrade = CourseGrade.ضعيف;
                            sc.ApplicationUser.HonourDegree = false;
                        }
                        StudentEnrollment se = await _context.StudentEnrollments
                            .Where(a => a.StudentEnrollmentID == sc.StudentEnrollmentID)
                            .Include(a => a.StudentCourses)
                            .Include(a => a.Level).FirstAsync();
                        if (sc.CourseGrade != CourseGrade.ضعيف && sc.CourseGrade != CourseGrade.راسب_لائحة)
                        {
                            //Correct the studentenrollment of studentcourse if needed
                            var a = await _context.StudentEnrollments
                                .Where(a => a.ApplicationUserID == sc.ApplicationUserID
                                && a.LevelName == sc.CourseEnrollment.LevelName).ToListAsync();
                            if (a.Any())
                            {
                                int studentEnrollmentId = a[0].StudentEnrollmentID;
                                sc.StudentEnrollmentID = studentEnrollmentId;
                                _context.Update(sc);
                                await _context.SaveChangesAsync();
                            }
                            //get the correct student enrollment after adding the studentcourse
                            se = await _context.StudentEnrollments
                                .Include(a => a.StudentCourses)
                                .Include(a => a.Level)
                                .Where(a => a.ApplicationUserID == sc.ApplicationUser.Id &&
                                a.LevelName == sc.CourseEnrollment.LevelName).FirstAsync();
                        }
                        se.CompleteLevelMark = se.StudentCourses
                            .Where(a => a.CourseGrade != CourseGrade.ضعيف &&
                            a.CourseGrade != CourseGrade.راسب_لائحة).Sum(a => a.TotalMark);
                        var courseEnrollments = await _context.CourseEnrollments
                            .Where(a => a.BranchName == se.BranchName &&
                            a.LevelName == se.LevelName).Include(a => a.Course).ToListAsync();
                        double FullLevelMark = courseEnrollments.Sum(a => a.Course.FullMark);

                        double percentage = se.CompleteLevelMark / FullLevelMark;
                        var failedCoursesCounter = se.StudentCourses.Where(a => a.CourseGrade == CourseGrade.ضعيف ||
                                a.CourseGrade == CourseGrade.راسب_لائحة).ToList().Count;
                        
                        _logger.LogInformation($"number of failed courses = {failedCoursesCounter}");
                            
                        if (se.StudentCourses.Count < sc.CourseEnrollment.StudentCourses.Count &&
                            se.StudentGrade == StudentGrade.بمواد)
                        { _logger.LogInformation("Student has succeeded an old course but still there is another course to succeed in the old enrollment"); }
                        else if ((failedCoursesCounter > 2 || sc.CourseEnrollment.IsEssential)
                        && se.Level.MaxFailures <= se.FailureNo)
                        {
                            _logger.LogInformation($"student grade is مفصول");
                            se.StudentGrade = StudentGrade.مفصول;
                        }
                        else if (failedCoursesCounter > 2 || sc.CourseEnrollment.IsEssential)
                        {
                            _logger.LogInformation($"student grade is راسب in year {se.AcademicYearID}");
                            se.StudentGrade = StudentGrade.راسب;
                        }
                        else if (failedCoursesCounter > 0)
                        {
                            _logger.LogInformation($"student grade is بمواد in year {se.AcademicYearID}");
                            se.StudentGrade = StudentGrade.بمواد; }
                        else if (percentage >= 0.85)
                        {
                            _logger.LogInformation($"student grade is امتياز in year {se.AcademicYearID}");
                            se.StudentGrade = StudentGrade.امتياز; }
                        else if (percentage >= 0.75)
                        {
                            _logger.LogInformation($"student grade is جيد_جدا in year {se.AcademicYearID}");
                            se.StudentGrade = StudentGrade.جيد_جدا; }
                        else if (percentage >= 0.65)
                        {
                            _logger.LogInformation($"student grade is جيد in year {se.AcademicYearID}");
                            se.StudentGrade = StudentGrade.جيد; }
                        else {
                            _logger.LogInformation($"student grade is مقبول in year {se.AcademicYearID}");
                            se.StudentGrade = StudentGrade.مقبول;
                        }
                        _context.Update(se);
                        await _context.SaveChangesAsync();

                        //حساب المجموع التراكمي والتقدير التراكمي
                        var studentEnrollments = _context.StudentEnrollments
                            .Where(a => a.ApplicationUserID == se.ApplicationUserID);
                        sc.ApplicationUser.FinalMark = studentEnrollments.Sum(a => a.CompleteLevelMark);
                        double FinalFullMark = sc.ApplicationUser.StudentCourses.Sum(a => a.CourseEnrollment.Course.FullMark);
                        double finalPercentage = sc.ApplicationUser.FinalMark / FinalFullMark;
                        if (finalPercentage >= 0.85)
                        { sc.ApplicationUser.FinalGrade = StudentGrade.امتياز; }
                        else if (finalPercentage >= 0.75)
                        { sc.ApplicationUser.FinalGrade = StudentGrade.جيد_جدا; }
                        else if (finalPercentage >= 0.65)
                        { sc.ApplicationUser.FinalGrade = StudentGrade.جيد; }
                        else if (finalPercentage >= 0.50)
                        { sc.ApplicationUser.FinalGrade = StudentGrade.مقبول; }
                        else
                        { sc.ApplicationUser.FinalGrade = StudentGrade.راسب; }
                    }
                    _context.UpdateRange(studentCourses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    _logger.LogInformation($"catch error");
                    if (!StudentEnrollmentExists(studentCourses.First().StudentEnrollmentID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Edit", new { id = $"{id}" });
            }
            _logger.LogInformation($"ModelState.IsValid status is {ModelState.IsValid}");
            return NotFound();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var studentCourses = await _context.StudentCourses
                .Include(a => a.ApplicationUser)
                .Include(a => a.CourseEnrollment)
                .ThenInclude(a => a.Course)
                .Where(a => a.CourseEnrollmentID == id && a.AcademicYearID == a.StudentEnrollment.AcademicYearID)
                .ToListAsync();
            if (!studentCourses.Any())
            {
                return NotFound();
            }
            int thisYearFirst = studentCourses.First().AcademicYearID;
            int thisYearSecond = studentCourses.First().AcademicYearID + 1;
            string courseName = studentCourses.First().CourseEnrollment.CourseName;
            ViewData["Title"] =
                $"{thisYearSecond} / {thisYearFirst} للعام الدراسي {courseName} بيان درجات مادة";
            return View(studentCourses);
        }



        private bool StudentEnrollmentExists(int id)
        {
            return _context.StudentEnrollments.Any(e => e.StudentEnrollmentID == id);
        }
    }
}
