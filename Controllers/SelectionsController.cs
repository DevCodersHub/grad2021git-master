using grad2021.Data;
using grad2021.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace grad2021.Controllers
{
    [Authorize(Roles = "Student")]
    public class Selections : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AcademicYearsController> _logger;

        public Selections(ApplicationDbContext context,
            ILogger<AcademicYearsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            _logger.LogInformation($"User id = {ApplicationUserId}");

            try
            {
                var se = await _context.StudentEnrollments
                    .OrderByDescending(a => a.AcademicYearID)
                    .Where(a => a.ApplicationUserID == ApplicationUserId).FirstAsync();

                _logger.LogInformation($"enrollment id = {se.StudentEnrollmentID}");

                var currentSelections = await _context.Selections.
                    Where(a => a.StudentEnrollmentID == se.StudentEnrollmentID).ToListAsync();

                return View(currentSelections);
            }
            catch (NullReferenceException e)
            {
                return RedirectToAction("~/Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(List<Selection> selections)
        {
            var ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger.LogInformation($"User id = {ApplicationUserId}");
            var se = await _context.StudentEnrollments
                .OrderByDescending(a => a.AcademicYearID)
                .Where(a => a.ApplicationUserID == ApplicationUserId).FirstAsync();

            _logger.LogInformation($"enrollment id = {se.StudentEnrollmentID}");

            var currentSelections = await _context.Selections.
                Where(a => a.StudentEnrollmentID == se.StudentEnrollmentID).ToListAsync();

            _logger.LogInformation($"current database contains elements? {currentSelections.Count()}");
            _logger.LogInformation($"current post contains elements? {selections.Count()}");

            if (currentSelections.Count == currentSelections.Count)
            {
                for (int i = 0; i < selections.Count; i++)
                {
                    _logger.LogInformation($"enter the for loop, iteration {i}");
                    currentSelections[i].SelectionBranchName = selections[i].SelectionBranchName;
                }
                _context.UpdateRange(currentSelections);
                await _context.SaveChangesAsync();
            }
            return Ok("success");
        }
    }
}
