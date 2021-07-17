using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace grad2021.Models
{
    public class InstructorEnrollment
    {
        [DatabaseGenerat‌ed(DatabaseGeneratedOp‌​tion.Identity)]
        public int InstructorEnrollmentID { get; set; }

        [Display(Name = "اسم عضو هيئة التدريس")]
        public string ApplicationUserID { get; set; }

        [Display(Name = "رمز تعريف المادة في القسم")]
        public int CourseEnrollmentID { get; set; }

        [ForeignKey("ApplicationUserID")]
        public ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("CourseEnrollmentID")]
        public CourseEnrollment CourseEnrollment { get; set; }
    }
}
