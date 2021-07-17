using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace grad2021.Models
{
    public class CourseEnrollment
    {
        [DatabaseGenerat‌ed(DatabaseGeneratedOp‌​tion.Identity)]
        public int CourseEnrollmentID { get; set; }

        [Display(Name = "اسم المادة")]
        public string CourseName { get; set; }

        [Display(Name = "شعبة")]
        public string BranchName { get; set; }

        [Display(Name = "الفرقة")]
        public string LevelName { get; set; }

        [Display(Name = "الفصل الدراسي")]
        public Term Term { get; set; }

        [Display(Name = "هل المادة ضرورية للنجاح؟")]
        public bool IsEssential { get; set; } = false;

        [ForeignKey("CourseName")]
        public Course Course { get; set; }
        [ForeignKey("BranchName")]
        public Branch Branch { get; set; }
        [ForeignKey("LevelName")]
        public Level Level { get; set; }


        public ICollection<InstructorEnrollment> InstructorEnrollments { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}