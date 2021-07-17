using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace grad2021.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string NatId { get; set; }
        public DateTime? BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public Gender Gender { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        [Display(Name = "رقم الجلوس")]
        public int? SeatNo { get; set; }
        public string BranchName { get; set; }
        public int AcademicYearID { get; set; }
        [Display(Name = "المجموع التراكمي")]
        public double FinalMark { get; set; } = 0;
        [Display(Name = "التقدير التراكمي")]
        public StudentGrade FinalGrade { get; set; } = 0;
        [Display(Name = "مرتبة الشرف")]
        public bool HonourDegree { get; set; } = true;

        [ForeignKey("AcademicYearID")]
        public AcademicYear AcademicYear { get; set; }
        [ForeignKey("BranchName")]
        public Branch Branch { get; set; }

        public ICollection<InstructorEnrollment> InstructorEnrollments { get; set; }
        public ICollection<InstructorProfession> InstructorProfessions { get; set; }
        public ICollection<StudentEnrollment> StudentEnrollments { get; set; }
        public List<StudentCourse> StudentCourses { get; set; }
    }
}
