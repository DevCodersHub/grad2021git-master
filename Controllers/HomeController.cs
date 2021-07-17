using grad2021.Data;
using grad2021.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace grad2021.Controllers
{
    public class HomeController  : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            if (User.IsInRole("Student")) { return Redirect("~/Home/Student"); }
            else if (User.IsInRole("Administrator")) { return Redirect("~/Home/Administrator"); }
            else if (User.IsInRole("ControlAdmin")) { return Redirect("~/Home/ControlAdmin"); }
            else if (User.IsInRole("StudentAffairs")) { return Redirect("~/Home/StudentAffairs"); }
            return View();
        }

        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Student()
        {
            var studentEnrollments = await _context.StudentEnrollments
                .Include(m => m.AcademicYear)
                .Include(m => m.Selections)
                .Include(m => m.StudentCourses)
                .ThenInclude(m => m.CourseEnrollment)
                .ThenInclude(m => m.Course)
                .Where(m => m.ApplicationUserID == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .ToListAsync();
            return View(studentEnrollments);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Administrator()
        {
            return View();
        }

        [Authorize(Roles = "ControlAdmin")]
        public async Task<IActionResult> ControlAdmin()
        {
            ViewData["Title"] = "الصفحة الرئيسية لأعمال  للكنترول";
            AcademicYear academicYear = await _context.AcademicYears.OrderByDescending(a => a.AcademicYearID).FirstAsync();
            ViewData["thisYear"] = academicYear.AcademicYearID;
            int nextYear = academicYear.AcademicYearID + 1;
            ViewData["nextYear"] = nextYear;
            return View();
        }


        [Authorize(Roles = "StudentAffairs")]
        public async Task<IActionResult> StudentAffairs()
        {
            ViewData["Title"] = "الصفحة الرئيسية لأعمال شؤون الطلاب";
            AcademicYear academicYear = await _context.AcademicYears.OrderByDescending(a => a.AcademicYearID).FirstAsync();
            ViewData["thisYear"] = academicYear.AcademicYearID;
            int nextYear = academicYear.AcademicYearID + 1;
            ViewData["nextYear"] = nextYear;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
