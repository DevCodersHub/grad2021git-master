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
    public class AcademicYearsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AcademicYearsController> _logger;

        public AcademicYearsController(ApplicationDbContext context,
            ILogger<AcademicYearsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: AcademicYears
        public async Task<IActionResult> Index()
        {
            return View(await _context.AcademicYears.ToListAsync());
        }

        // GET: AcademicYears/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicYear = await _context.AcademicYears
                .FirstOrDefaultAsync(m => m.AcademicYearID == id);
            if (academicYear == null)
            {
                return NotFound();
            }

            return View(academicYear);
        }

        // GET: AcademicYears/Create
        public async Task<IActionResult> Create()
        {
            var academicYear = await _context.AcademicYears
                .OrderByDescending(a => a.AcademicYearID).FirstAsync();
            if (academicYear == null)
            {
                return NotFound();
            }
            return View(academicYear);
        }

        // POST: AcademicYears/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("AcademicYearID,FirstSemesterStartDate,FirstSemesterExamsStartDate,FirstSemesterControlStartDate,FirstSemesterObjectionStartDate,FirstSemesterObjectionEndDate,SecondSemesterStartDate,SecondSemesterExamsStartDate,SecondSemesterControlStartDate,SecondSemesterObjectionStartDate,SecondSemesterObjectionEndDate,NovemberExamsStartDate,NovemberControlStartDate,NovemberObjectionStartDate,NovemberObjectionEndDate")] AcademicYear academicYear)
        public async Task<IActionResult> Create([Bind("AcademicYearID")] AcademicYear academicYear)
        {
            if (ModelState.IsValid)
            {
                _context.Add(academicYear);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"The new AcademicYear {academicYear.AcademicYearID} is added to the database");
                //get all students enrolled in the previous year
                var studentEnrollments2 = from a in _context.StudentEnrollments
                                         .Include(m => m.Level)
                                         .Include(m => m.Selections)
                                         .Include(m => m.StudentCourses)
                                         .ThenInclude(m => m.CourseEnrollment)
                                         .ThenInclude(m => m.Course)
                                         .Where(m => m.AcademicYearID == academicYear.AcademicYearID - 1)
                                          select a;
                var studentEnrollments = await studentEnrollments2.ToListAsync();
                var branches = await _context.Branches.ToListAsync();
                foreach (Branch branch in branches) { branch.CurrentCapacity = 0; }
                _context.UpdateRange(branches);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"current capacities for all branches are set to zero");

                foreach (StudentEnrollment studentEnrollment in
                    studentEnrollments.Where(s => s.LevelName == "الرابعة"))
                {

                    var failedCourses = studentEnrollment.StudentCourses
                        .Where(a => a.CourseGrade == CourseGrade.ضعيف ||
                    a.CourseGrade == CourseGrade.راسب_لائحة).ToList();

                    if (studentEnrollment.StudentGrade == StudentGrade.مفصول)
                    { studentEnrollment.FailureNo += 1; }
                    else if (studentEnrollment.StudentGrade == StudentGrade.راسب)
                    {
                        studentEnrollment.FailureNo += 1;
                        studentEnrollment.AcademicYearID = academicYear.AcademicYearID;
                        studentEnrollment.StudentStatus = StudentStatus.باق;
                        failedCourses.ForEach(a => a.AcademicYearID = academicYear.AcademicYearID);
                    }
                    _context.Update(studentEnrollment);
                    _context.UpdateRange(failedCourses);
                    await _context.SaveChangesAsync();
                }

                ///////////////////////////////////////////////////////////////////////////////////
                
                foreach (StudentEnrollment studentEnrollment in studentEnrollments
                    .Where(s => s.LevelName != "الإعدادية" &&
                !(s.LevelName == "الثالثة" && s.BranchName == "الهندسة الميكانيكية") &&
                s.LevelName != "الرابعة"))
                {
                    var failedCourses = studentEnrollment.StudentCourses
                        .Where(a => a.CourseGrade == CourseGrade.ضعيف ||
                    a.CourseGrade == CourseGrade.راسب_لائحة).ToList();

                    if (studentEnrollment.StudentGrade == StudentGrade.مفصول)
                    {
                        studentEnrollment.FailureNo += 1;
                        _context.Update(studentEnrollment);
                        await _context.SaveChangesAsync();
                    }
                    else if (studentEnrollment.StudentGrade == StudentGrade.راسب)
                    {
                        studentEnrollment.FailureNo += 1;
                        studentEnrollment.AcademicYearID = academicYear.AcademicYearID;
                        studentEnrollment.StudentStatus = StudentStatus.باق;
                        failedCourses.ForEach(a => a.AcademicYearID = academicYear.AcademicYearID);
                        _context.Update(studentEnrollment);
                        _context.UpdateRange(failedCourses);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        //studentEnrollment.YearPass = true;
                        StudentEnrollment se1 = new()
                        {
                            AcademicYearID = academicYear.AcademicYearID,
                            BranchName = studentEnrollment.BranchName,
                            ApplicationUserID = studentEnrollment.ApplicationUserID,
                            LevelName = NextLevel(studentEnrollment.LevelName),
                            StudentStatus = StudentStatus.مستجد,
                            FailureNo = 0,
                        };
                        _context.Add(se1);
                        await _context.SaveChangesAsync();
                        var se = await _context.StudentEnrollments
                            .FirstAsync(a => a.ApplicationUserID == se1.ApplicationUserID &&
                            a.AcademicYearID == se1.AcademicYearID);
                        failedCourses = studentEnrollment.StudentCourses
                            .Where(a => a.CourseGrade == CourseGrade.ضعيف ||
                        a.CourseGrade == CourseGrade.راسب_لائحة).ToList();
                        failedCourses.ForEach(x =>
                        {
                            x.AcademicYearID = academicYear.AcademicYearID;
                            x.StudentEnrollmentID = se.StudentEnrollmentID;
                        });
                        _context.UpdateRange(failedCourses);
                        await _context.SaveChangesAsync();

                        List<StudentCourse> newStudentCourses = new();
                        var courseEnrollments = await _context.CourseEnrollments
                            .Where(m => m.LevelName == se.LevelName && m.BranchName == se.BranchName)
                            .ToListAsync();
                        foreach (CourseEnrollment courseEnrollment in courseEnrollments)
                        {
                            StudentCourse studentCourse = new()
                            {
                                StudentEnrollmentID = se.StudentEnrollmentID,
                                AcademicYearID = academicYear.AcademicYearID,
                                CourseEnrollmentID = courseEnrollment.CourseEnrollmentID,
                                ApplicationUserID = se.ApplicationUserID,
                            };
                            newStudentCourses.Add(studentCourse);
                        }
                        _context.AddRange(newStudentCourses);
                        await _context.SaveChangesAsync();
                    }
                }

                /////////////////////////////////////////////////////////////////////////////////

                // here we will deal with students that will branch next year
                //we should order them first to give priority to higher grades

                foreach (StudentEnrollment studentEnrollment in
                studentEnrollments.Where(s => s.LevelName == "الإعدادية" ||
                (s.LevelName == "الثالثة" && s.BranchName == "الهندسة الميكانيكية"))
                .OrderByDescending(a => a.CompleteLevelMark))
                {
                    _logger.LogInformation("foreach of the branching students");

                    var failedCourses = studentEnrollment.StudentCourses
                        .Where(a => a.CourseGrade == CourseGrade.ضعيف ||
                    a.CourseGrade == CourseGrade.راسب_لائحة).ToList();


                    if (studentEnrollment.StudentGrade == StudentGrade.مفصول)
                    {
                        _logger.LogInformation("foreach of the branching students and will leave");
                        studentEnrollment.FailureNo += 1;
                        _context.Update(studentEnrollment);
                        await _context.SaveChangesAsync();
                    }
                    else if (studentEnrollment.StudentGrade == StudentGrade.راسب)
                    {
                        _logger.LogInformation("foreach of the branching students and failed");
                        studentEnrollment.FailureNo += 1;
                        studentEnrollment.AcademicYearID = academicYear.AcademicYearID;
                        studentEnrollment.StudentStatus = StudentStatus.باق;
                        failedCourses = studentEnrollment.StudentCourses
                            .Where(a => a.CourseGrade == CourseGrade.ضعيف ||
                        a.CourseGrade == CourseGrade.راسب_لائحة).ToList();
                        failedCourses.ForEach(a => a.AcademicYearID = academicYear.AcademicYearID);
                        _context.UpdateRange(failedCourses);
                        _context.Update(studentEnrollment);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        _logger.LogInformation("foreach of the branching students and succeeded");
                        StudentEnrollment se1 = new()
                        {
                            AcademicYearID = academicYear.AcademicYearID,
                            ApplicationUserID = studentEnrollment.ApplicationUserID,
                            LevelName = NextLevel(studentEnrollment.LevelName),
                            StudentStatus = StudentStatus.مستجد,
                            FailureNo = 0,
                        };
                        //-----------------------------------------------------------------------------//
                        List<string> branchNames = new();
                        List<Branch> Branches = new();
                        if (studentEnrollment.LevelName == "الثالثة" && studentEnrollment.BranchName == "الهندسة الميكانيكية")
                        {
                            
                            var selectionBranchNamesPrep = Branches
                             .Where(b => b.SelectionStatus == SelectionStatus.رابعة_ميكانيكا).ToList();
                            foreach (Branch branch in selectionBranchNamesPrep) { branchNames.Add(branch.BranchName); }
                        }
                        List<Selection> selections = new();
                        for (int j = 0; j < branchNames.Count; j++)
                        {
                            //Random rnd = new Random();
                            Selection selection1 = new()
                            {
                                //SelectionID = rnd.Next(1, int.MaxValue),
                                SelectionBranchName = branchNames[j],
                                CurrentBranchName = studentEnrollment.BranchName,
                                SelectionNo = j + 1,
                                StudentEnrollmentID = studentEnrollment.StudentEnrollmentID ,
                            };
                            selections.Add(selection1);
                        }
                        _context.AddRange(selections);
                        await _context.SaveChangesAsync();

                        //-----------------------------------------------------------------------------//
                        int selection = 1;

                        int selectionMax = studentEnrollment.Selections
                            .Where(a => a.StudentEnrollmentID == studentEnrollment.StudentEnrollmentID)
                            .OrderByDescending(a => a.SelectionNo).First().SelectionNo;

                        string selectedBranchName = (studentEnrollment.Selections.
                            First(a => a.SelectionNo == 1 &&
                            a.StudentEnrollmentID == studentEnrollment.StudentEnrollmentID))
                            .SelectionBranchName;
                        branches.First(s => s.BranchName == selectedBranchName).CurrentCapacity += 1;
                        while (branches.First(s => s.BranchName == selectedBranchName).CurrentCapacity >
                            branches.First(s => s.BranchName == selectedBranchName).FullCapacity &&
                            selection < selectionMax)
                        {
                            branches.First(s => s.BranchName == selectedBranchName).CurrentCapacity -= 1;
                            selection++;
                            selectedBranchName = (studentEnrollment.Selections.
                            First(a => a.SelectionNo == selection &&
                            a.StudentEnrollmentID == studentEnrollment.StudentEnrollmentID)).SelectionBranchName;
                            branches.First(s => s.BranchName == selectedBranchName).CurrentCapacity += 1;
                        }
                        se1.BranchName = selectedBranchName;
                        _context.Add(se1);
                        await _context.SaveChangesAsync();
                        var se = await _context.StudentEnrollments
                            .FirstAsync(a => a.ApplicationUserID == se1.ApplicationUserID &&
                            a.AcademicYearID == se1.AcademicYearID);
                        failedCourses = studentEnrollment.StudentCourses
                            .Where(a => a.CourseGrade == CourseGrade.ضعيف ||
                        a.CourseGrade == CourseGrade.راسب_لائحة).ToList();
                        failedCourses.ForEach(x =>
                        {
                            x.AcademicYearID = academicYear.AcademicYearID;
                            x.StudentEnrollmentID = se.StudentEnrollmentID;
                        });
                        _context.UpdateRange(failedCourses);
                        await _context.SaveChangesAsync();

                        List<StudentCourse> newStudentCourses = new();
                        var courseEnrollments = await _context.CourseEnrollments
                            .Where(m => m.LevelName == se.LevelName && m.BranchName == se.BranchName)
                            .ToListAsync();
                        foreach (CourseEnrollment courseEnrollment in courseEnrollments)
                        {
                            StudentCourse studentCourse = new()
                            {
                                StudentEnrollmentID = se.StudentEnrollmentID,
                                AcademicYearID = academicYear.AcademicYearID,
                                CourseEnrollmentID = courseEnrollment.CourseEnrollmentID,
                                ApplicationUserID = se.ApplicationUserID,
                            };
                            newStudentCourses.Add(studentCourse);
                        }
                        _context.AddRange(newStudentCourses);
                        await _context.SaveChangesAsync();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(academicYear);
        }

        // GET: AcademicYears/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicYear = await _context.AcademicYears.FindAsync(id);
            if (academicYear == null)
            {
                return NotFound();
            }
            return View(academicYear);
        }

        // POST: AcademicYears/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AcademicYearID,FirstSemesterStartDate,FirstSemesterExamsStartDate,FirstSemesterControlStartDate,FirstSemesterObjectionStartDate,FirstSemesterObjectionEndDate,SecondSemesterStartDate,SecondSemesterExamsStartDate,SecondSemesterControlStartDate,SecondSemesterObjectionStartDate,SecondSemesterObjectionEndDate,NovemberExamsStartDate,NovemberControlStartDate,NovemberObjectionStartDate,NovemberObjectionEndDate")] AcademicYear academicYear)
        {
            if (id != academicYear.AcademicYearID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(academicYear);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcademicYearExists(academicYear.AcademicYearID))
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
            return View(academicYear);
        }

        // GET: AcademicYears/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicYear = await _context.AcademicYears
                .FirstOrDefaultAsync(m => m.AcademicYearID == id);
            if (academicYear == null)
            {
                return NotFound();
            }

            return View(academicYear);
        }

        // POST: AcademicYears/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var academicYear = await _context.AcademicYears.FindAsync(id);
            _context.AcademicYears.Remove(academicYear);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AcademicYearExists(int id)
        {
            return _context.AcademicYears.Any(e => e.AcademicYearID == id);
        }
        public string NextLevel(string a)
        {
            string nextLevel = "الأولى";
            switch (a)
            {
                case "الإعدادية":
                    break;
                case "الأولى":
                    nextLevel = "الثانية";
                    break;
                case "الثانية":
                    nextLevel = "الثالثة";
                    break;
                case "الثالثة":
                    nextLevel = "الرابعة";
                    break;
            }
            return nextLevel;
        }
        //public double FullMark(Course course)
        //{
        //    double fullMark = course.CourseWorkMaxScore + course.MidTermExamMaxScore +
        //        course.OralExamMaxScore + course.TermExamMaxScore;
        //    //_logger.LogInformation($"full mark = {fullMark}");
        //    return fullMark;
        //}
        //public double TotalMark(StudentCourse studentCourse)
        //{
        //    double totalMark = studentCourse.MidTermMark + studentCourse.CourseWorkMark + studentCourse.OralExamMark +
        //        studentCourse.MerciMark + studentCourse.FinalExamMark;
        //    //_logger.LogInformation($"Total Mark = {totalMark}");
        //    return totalMark;
        //}
        //public double CompleteLevelMark(StudentEnrollment studentEnrollment)
        //{
        //    double completeLevelMark = 0;
        //    foreach (StudentCourse studentCourse in studentEnrollment.StudentCourses)
        //    {
        //        if (studentCourse.CourseEnrollment.LevelName == studentEnrollment.LevelName &&
        //            !CourseFail(studentCourse))
        //        {
        //            completeLevelMark += TotalMark(studentCourse);
        //        }
        //    }
        //    return completeLevelMark;
        //}

        //public bool CourseFail(StudentCourse studentCourse)
        //{
        //    double totalMark = TotalMark(studentCourse);
        //    double halfMark = 0.5 * FullMark(studentCourse.CourseEnrollment.Course);
        //    double finalExamMark = studentCourse.FinalExamMark;
        //    double oneThirdOfTerExamMaxScore = 1 / 3 * studentCourse.CourseEnrollment.Course.TermExamMaxScore;
        //    _logger.LogInformation($"totalMark = {totalMark}, halfMark = {halfMark}");
        //    _logger.LogInformation($"finalExamMark = {finalExamMark}, oneThirdOfTerExamMaxScore = {oneThirdOfTerExamMaxScore}");
        //    bool courseFail = totalMark < halfMark || finalExamMark < oneThirdOfTerExamMaxScore;
        //    _logger.LogInformation($"course fail status is {courseFail}");
        //    return courseFail;
        //}
        //public bool Branching(string branchName, string levelName)
        //{
        //    return branchName == "الرياضيات والفيزيقا الهندسية" ||
        //        (branchName == "الهندسة الميكانيكية" && levelName == "الثالثة");
        //}

        //(List<StudentCourse>, bool, bool)
        //    IfCourseFailed(StudentCourse studentCourse, List<StudentCourse> failedCourses, bool studentFailed)
        //{
        //    bool studentDrop = false;
        //    failedCourses.Add(studentCourse);
        //    if (failedCourses.Count > 2 || studentCourse.CourseEnrollment.IsEssential)
        //    {
        //        StudentEnrollment studentEnrollment = studentCourse.StudentEnrollment;
        //        studentFailed = true;
        //        if (studentEnrollment.Level.MaxFailures <= studentEnrollment.FailureNo)
        //        { studentDrop = true; }
        //    }
        //    return (failedCourses, studentFailed, studentDrop);

        //}

        //public bool CurrentCourse(StudentCourse studentCourse)
        //{
        //    StudentEnrollment studentEnrollment = studentCourse.StudentEnrollment;
        //    return studentCourse.CourseEnrollment.LevelName == studentEnrollment.LevelName;
        //}
        
        //public void IfCurrentCourseSucceeded(StudentCourse studentCourse)
        //{
        //    StudentEnrollment studentEnrollment = studentCourse.StudentEnrollment;
        //    studentCourse.TotalMark = TotalMark(studentCourse);
        //    _context.Update(studentCourse);
        //    studentEnrollment.CompleteLevelMark += studentCourse.TotalMark;
        //    _context.Update(studentEnrollment);
        //    return;
        //}

        //public void IfOldCourseSucceeded(StudentCourse studentCourse, StudentEnrollment oldYear)
        //{
        //    oldYear.CompleteLevelMark += 0.5 * FullMark(studentCourse.CourseEnrollment.Course);
        //    _context.Update(oldYear);

        //    //return back the studentcourse to its original course enrollment
        //    //this is for the student courses that were failed in the previous attempt
        //    studentCourse.TotalMark = 0.5 * FullMark(studentCourse.CourseEnrollment.Course);
        //    studentCourse.StudentEnrollmentID = oldYear.StudentEnrollmentID;
        //    _context.Update(studentCourse);
        //    return;
        //}
        //public void IfStudentDrop(StudentEnrollment studentEnrollment)
        //{
        //    //studentEnrollment.YearPass = false;
        //    studentEnrollment.FailureNo += 1;
        //    //studentEnrollment.IsNovember = false;
        //    _context.Update(studentEnrollment);
        //    return;
        //}
        //public void IfStudentFailed(StudentEnrollment studentEnrollment,
        //    List<StudentCourse> failedCourses, int thisYear)
        //{
        //    //studentEnrollment.YearPass = false;
        //    studentEnrollment.FailureNo += 1;
        //    //studentEnrollment.IsNovember = false;
        //    studentEnrollment.AcademicYearID = thisYear;
        //    _context.Update(studentEnrollment);
        //    foreach (StudentCourse studentCourse in failedCourses)
        //    {
        //        studentCourse.AcademicYearID = thisYear;
        //    }
        //    _context.UpdateRange(failedCourses);
        //    return;
        //}
    }
}
