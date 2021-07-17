using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace grad2021.Models
{
    public class Course
    {

        [Display(Name = "اسم المقرر")]
        public string CourseName { get; set; }

        [Display(Name = "الرمز الرقمي للمقرر"), RegularExpression("([0-9]+)"), MinLength(3), MaxLength(3)]
        public string CourseCode { get; set; }

        [Display(Name = "الرمز بالحروف")]
        public string DepartmentCodeValue { get; set; }

        [Display(Name = "وصف المادة")]
        public string? CourseDescription { get; set; }

        //عدد الساعات أسبوعيا
        [Display(Name = "عدد الساعات الأسبوعية للمحاضرة")]
        public int? LectureWeeklyDuration { get; set; }
        [Display(Name = "عدد الساعات الأسبوعية للتمارين العملية")]
        public int? SectionWeeklyDuration { get; set; }

        //النهاية العظمى توزيع الدرجات

        [Display(Name = "النهاية العظمى لأعمال السنة")]
        public double CourseWorkMaxScore { get; set; } = 15;

        [Display(Name = "النهاية العظمى لامتحان منتصف الفصل الدراسي")]
        public double MidTermExamMaxScore { get; set; } = 15;

        [Display(Name = "النهاية العظمى لامتحان الشفوي أو العملي")]
        public double OralExamMaxScore { get; set; } = 15;

        [Display(Name = "النهاية العظمى للامتحان التحريري")]
        public double TermExamMaxScore { get; set; } = 80;

        //النهاية الصغرى للنجاح في الامتحان التحريري والمقرر

        //[Display(Name = "النهاية الصغرى للامتحان التحريري")]
        //public double TermExamMinScore { get; set; }

        [Display(Name = "النهاية العظمى الكلية لللمقرر")]
        public double FullMark { get; }

        //[Display(Name = "النهاية الصغرى للمجموع الكلي")]
        //public double CourseMinScore { get; set; }

        [ForeignKey("DepartmentCodeValue")]
        public DepartmentCode DepartmentCode { get; set; }


        public ICollection<CourseEnrollment> CourseEnrollments { get; set; }
    }
}
