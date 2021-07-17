using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace grad2021.Models
{
    public class StudentCourse
    {
        [DatabaseGenerat‌ed(DatabaseGeneratedOp‌​tion.Identity)]
        public int StudentCourseID { get; set; }

        [Display(Name = "رمز تسجيل الطالب للعام الدراسي")]
        public int StudentEnrollmentID { get; set; }

        [Display(Name = "رمز تعريف المادة في القسم")]
        public int CourseEnrollmentID { get; set; }

        [Display(Name = "العام الدراسي")]
        public int AcademicYearID { get; set; }

        public string ApplicationUserID { get; set; }

        [Display(Name = "درجة امتحان منتصف الفصل الدراسي")]
        public double MidTermMark { get; set; } = 0;

        [Display(Name = "درجة أعمال السنة")]
        public double CourseWorkMark { get; set; } = 0;

        [Display(Name = "درجة امتحان الشفوي")]
        public double OralExamMark { get; set; } = 0;

        [Display(Name = "درجة الامتحان النهائي")]
        public double FinalExamMark { get; set; } = 0;

        [Display(Name = "درجات الرأفة")]
        public double MerciMark { get; set; } = 0;

        [Display(Name = "الحالة")]
        public CourseGrade CourseGrade { get; set; } = CourseGrade.امتياز;

        [Display(Name = "المجموع الكلي للمقرر")]
        public double TotalMark { get; set; } = 0;

        //[Display(Name = "حالة النجاح")]
        //public bool SucceedInTotalMark { get; set; }

        [ForeignKey("AcademicYearID")]
        public AcademicYear AcademicYear { get; set; }
        [ForeignKey("StudentEnrollmentID")]
        public StudentEnrollment StudentEnrollment { get; set; }
        [ForeignKey("CourseEnrollmentID")]
        public CourseEnrollment CourseEnrollment { get; set; }
        [ForeignKey("ApplicationUserID")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
