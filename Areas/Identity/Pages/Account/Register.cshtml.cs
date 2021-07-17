using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using grad2021.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations.Schema;
using grad2021.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace grad2021.Areas.Identity.Pages.Account
{
    //[AllowAnonymous]
    [Authorize(Policy = "RegisterUser")]
    public class RegisterModel : PageModel
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }
        public Branch Branch { get; set; }
        public List<Branch> Branches { get; set; }
        public List<Level> LevelList { get; set; }
       
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            [Display(Name = "الرقم القومي")]
            public string NatId { get; set; }

            [Display(Name = "تاريخ الميلاد")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public DateTime? BirthDate { get; set; }

            [Display(Name = "محل الميلاد")]
            public string? BirthPlace { get; set; }

            [Display(Name = "النوع")]
            public Gender Gender { get; set; }

            [Display(Name = "تاريخ الالتحاق بالكلية")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public DateTime? EnrollmentDate { get; set; }

            [Display(Name = "رقم الجلوس")]
            public int? SeatNo { get; set; }

            [Display(Name = "قسم / شعبة")]
            public string BranchName { get; set; } = "الرياضيات والفيزيقا الهندسية";

            [Display(Name = "الفرقة / الوظيفة")]
            public string LevelName { get; set; }

            [Required]
            //[EmailAddress]
            [Display(Name = "الاسم بالكامل")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "رمز المرور")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "تأكيد رمز المرور")]
            [Compare("Password", ErrorMessage = "رمز مرور غير مطابق")]
            public string ConfirmPassword { get; set; }

            //[Display(Name = "الوظيفة")]
            //public string Role { get; set; }

            [Display(Name = "العام الدراسي")]
            public int AcademicYearID { get; set; } = 2021;
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            Branches = await _context.Branches.ToListAsync();
            LevelList = await _context.Levels.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            Branches = await _context.Branches.ToListAsync();
            LevelList = await _context.Levels.ToListAsync();

            string Role = "Instructor";
            if (Input.LevelName == "إداري") { Role = "StudentAffairs"; }
            else if (Input.LevelName == "الإعدادية" || Input.LevelName == "الأولى"
                || Input.LevelName == "الثانية" || Input.LevelName == "الثالثة"
                || Input.LevelName == "الرابعة") { Role = "Student"; }

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {
                    UserName = Input.Email,
                    Email = Input.Email,
                    EmailConfirmed = true,
                    NatId = Input.NatId,
                    BirthDate = Input.BirthDate,
                    BirthPlace = Input.BirthPlace,
                    Gender = Input.Gender,
                    EnrollmentDate = Input.EnrollmentDate,
                    SeatNo = Input.SeatNo,
                    BranchName = Input.BranchName,
                    AcademicYearID = Input.AcademicYearID,
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    if (Role == "Student")
                    {
                        StudentEnrollment studentEnrollment = new()
                        {
                            AcademicYearID = Input.AcademicYearID,
                            BranchName = Input.BranchName,
                            LevelName = Input.LevelName,
                            ApplicationUserID = user.Id,
                        };
                        _context.Add(studentEnrollment);
                        await _context.SaveChangesAsync();

                        int studentEnrollmentID = (await _context.StudentEnrollments.FirstAsync(s => s.ApplicationUserID == user.Id)).StudentEnrollmentID;
                        var courseEnrollments = await _context.CourseEnrollments
                            .Where(m => m.LevelName == Input.LevelName && m.BranchName == Input.BranchName)
                            .ToListAsync();
                        List<StudentCourse> studentCourses = new();
                        foreach (CourseEnrollment courseEnrollment in courseEnrollments)
                        {
                            StudentCourse studentCourse = new()
                            {
                                StudentEnrollmentID = studentEnrollmentID,
                                AcademicYearID = studentEnrollment.AcademicYearID,
                                CourseEnrollmentID = courseEnrollment.CourseEnrollmentID,
                                ApplicationUser = user,
                            };
                            studentCourses.Add(studentCourse);

                        }
                        _context.AddRange(studentCourses);
                        await _context.SaveChangesAsync();


                        List<string> branchNames = new();
                        if (Input.BranchName == "الرياضيات والفيزيقا الهندسية")
                        {
                            var selectionBranchNamesPrep = Branches.Where(b => b.SelectionStatus == SelectionStatus.أولى
                            || b.SelectionStatus == SelectionStatus.ثالثة_ميكانيكا).ToList();
                            foreach (Branch branch in selectionBranchNamesPrep) { branchNames.Add(branch.BranchName); }
                        }
                        else if (Input.BranchName == "الهندسة الميكانيكية" && Input.LevelName == "الثالثة")
                        {
                            var selectionBranchNamesPrep = Branches
                             .Where(b => b.SelectionStatus == SelectionStatus.رابعة_ميكانيكا).ToList();
                            foreach (Branch branch in selectionBranchNamesPrep) { branchNames.Add(branch.BranchName); }
                        }
                        List<Selection> selections = new();
                        for (int j = 0; j < branchNames.Count; j++)
                        {
                            
                            Selection selection = new()
                            {
                                SelectionBranchName = branchNames[j],
                                CurrentBranchName = Input.BranchName,
                                SelectionNo = j + 1,
                                StudentEnrollmentID = studentEnrollmentID,
                            };
                            selections.Add(selection);
                        }
                        _context.AddRange(selections);
                        await _context.SaveChangesAsync();
                    }
              
            else if (Role == "Instructor")
            {
                InstructorProfession instructorProfession = new()
                {
                    ApplicationUserID = user.Id,
                    ProfessionDegree = Input.LevelName,
                    PromotionDate = Input.EnrollmentDate,
                };
                _context.Add(instructorProfession);
                await _context.SaveChangesAsync();
            }

                    await _userManager.AddToRoleAsync(await _userManager.FindByIdAsync(user.Id), Role);
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    //else
                    //{
                    //    await _signInManager.SignInAsync(user, isPersistent: false);
                    //    return LocalRedirect(returnUrl);
                    //}
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
//List<string> selectionBranchNamesPrep = new()
//{
//    "الهندسة المدنية",
//    "الهندسة المعمارية",
//    "هندسة القوى والآلات الكهربية",
//    "هندسة الإلكترونيات والاتصالات الكهربية",
//    "هندسة الحاسبات والنظم",
//    "الهندسة الميكانيكية",
//};