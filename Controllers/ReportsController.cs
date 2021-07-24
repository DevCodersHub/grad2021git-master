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
    [Authorize(Roles = "StudentAffairs")]
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AcademicYearsController> _logger;

        public ReportsController(ApplicationDbContext context,
            ILogger<AcademicYearsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "قائمة التقارير والمطبوعات";
            return View();
        }
        public async Task<IActionResult> Lists(  )
        {
            //   //ViewData["Title"] = "قائمة التقارير";
            var succeededFourth = await _context.StudentEnrollments
                .Where(a => a.LevelName == "الرابعة" &&
                a.StudentGrade != StudentGrade.راسب &&
                a.StudentGrade != StudentGrade.مفصول&&
                a.StudentGrade != StudentGrade.بمواد).ToListAsync();

            List<StudentEnrollment> studentEnrollments = new();
            for (int i = 0; i < succeededFourth.Count; i++)
            {
                var se = await _context.StudentEnrollments
                       .Include(a => a.ApplicationUser)
                       .Include(a => a.Level)
                       .Include(a => a.AcademicYear)
                       .Where(a => a.ApplicationUserID == succeededFourth[i].ApplicationUserID)
                       .ToListAsync();

                studentEnrollments.AddRange(se);
            }
            studentEnrollments.OrderBy(a => a.ApplicationUserID).ThenByDescending(a =>a.LevelName);
            return View(studentEnrollments );
        }
        public async Task<IActionResult> Certs(string searchString)
        {
            ViewData["Title"] = "بيان حالة";
            if(searchString != null)
            {
                ViewData["CurrentFilter"] = searchString;
            }
            else { ViewData["CurrentFilter"] = "أدخل اسم الطالب"; }
            var students = from s in _context.Users
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                var student = await students
                    .Where(s => s.UserName.Contains(searchString)).FirstAsync();

                var appUserId = student.Id;
                var studentEnrollments = await _context.StudentEnrollments
                    .Include(a => a.StudentCourses)
                    .ThenInclude(a =>a.CourseEnrollment.Course)
                    .Where(a => a.ApplicationUserID == appUserId).ToListAsync();
                return View(studentEnrollments);
            }
            return View();
        }

        public async Task<IActionResult> AllResults(string BranchName, string LevelName,Term Term)
        {
           
                

           var academicYear = await _context.AcademicYears
                .Include(a => a.StudentEnrollments)
                .ThenInclude(a => a.ApplicationUser)
                .ThenInclude(a => a.StudentCourses)
                .ThenInclude(a => a.CourseEnrollment)
                .OrderByDescending(a => a.AcademicYearID).FirstAsync();
            ViewData["BranchFilter"] = "";
            ViewData["LevelFilter"] = "";
            ViewData["TermFilter"] = "";
            if (BranchName != null && LevelName != null && Term != null)
            {
                var studentEnrollments = await _context.StudentEnrollments
                    .Where(a => a.BranchName == BranchName && a.LevelName == LevelName ).ToListAsync();
                ViewData["BranchFilter"] = BranchName;
                ViewData["LevelFilter"] = LevelName;
                ViewData["TermFilter"] = Term;
                academicYear.StudentEnrollments = studentEnrollments;
            

                var courseEnrollments = await _context.CourseEnrollments
                    .Where(a => a.BranchName == BranchName && a.LevelName == LevelName && a.Term == Term ).ToListAsync();
                ViewData["BranchFilter"] = BranchName;
                ViewData["LevelFilter"] = LevelName;
                ViewData["TermFilter"] = Term;
                academicYear.CourseEnrollments = courseEnrollments;
            }
            var branches = await _context.Branches.ToListAsync();
            academicYear.Branches = branches;
            var levels = await _context.Levels.ToListAsync();
            academicYear.Levels = levels;
            return View(academicYear);
        }
    }
}
