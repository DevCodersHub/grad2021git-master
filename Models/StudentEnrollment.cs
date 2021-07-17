using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace grad2021.Models
{
    public class StudentEnrollment
    {
        [DatabaseGenerat‌ed(DatabaseGeneratedOp‌​tion.Identity)]
        public int StudentEnrollmentID { get; set; }
        
        public string ApplicationUserID { get; set; }

        [Display(Name = "شعبة")]
        public string BranchName { get; set; }

        [Display(Name = "العام الدراسي")]
        public int AcademicYearID { get; set; }

        [Display(Name = "الفرقة")]
        public string LevelName { get; set; }

        [Display(Name = "المجموع الكلي")]
        public double CompleteLevelMark { get; set; } = 0;

        [Display(Name = "تقدير الطالب")]
        public StudentGrade StudentGrade { get; set; }

        [Display(Name = "مستجد / باق")]
        public StudentStatus StudentStatus { get; set; }

        [Display(Name = "عدد مرات الرسوب")]
        public int FailureNo { get; set; } = 0;

        //These classes are retreived to 
        [ForeignKey("ApplicationUserID")]
        public ApplicationUser ApplicationUser { get; set; }
        
        [ForeignKey("BranchName")]
        public Branch Branch { get; set; }
        
        [ForeignKey("LevelName")]
        public Level Level { get; set; }
        
        [ForeignKey("AcademicYearID")]
        public AcademicYear AcademicYear { get; set; }

        public List<StudentCourse> StudentCourses { get; set; }
        public List<Selection> Selections { get; set; }

        [NotMapped]
        public double FullLevelMark { get { return 1500; } }
    }
}
