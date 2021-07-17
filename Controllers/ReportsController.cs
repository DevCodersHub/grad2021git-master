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
        public async Task<IActionResult> Certs()
        {
            ViewData["Title"] = "قائمة المطبوعات";
            var studentEnrollments = await _context.StudentEnrollments.ToListAsync();
            return View(studentEnrollments);
        }
    }
}
