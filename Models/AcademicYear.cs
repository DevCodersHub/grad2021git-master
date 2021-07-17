using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace grad2021.Models
{
    public class AcademicYear
    {
        [Display(Name = "العام الدراسي")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AcademicYearID { get; set; }

// الفصل الدراسي الأول

        [Display(Name = "تاريخ بداية الفصل الدراسي الأول")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FirstSemesterStartDate { get; set; }

        [Display(Name = "تاريخ بداية امتحانات الفصل الدراسي الأول")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FirstSemesterExamsStartDate { get; set; }


        [Display(Name = "تاريخ بداية رصد درجات الفصل الدراسي الأول")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FirstSemesterControlStartDate { get; set; }

        [Display(Name = "تاريخ بداية تظلمات الفصل الدراسي الأول")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FirstSemesterObjectionStartDate { get; set; }

        [Display(Name = "تاريخ غلق باب تظلمات الفصل الدراسي الأول")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FirstSemesterObjectionEndDate { get; set; }


// الفصل الدراسي الثاني


        [Display(Name = "تاريخ بداية الفصل الدراسي الثاني")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? SecondSemesterStartDate { get; set; }

        [Display(Name = "تاريخ بداية امتحانات الفصل الدراسي الثاني")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? SecondSemesterExamsStartDate { get; set; }


        [Display(Name = "تاريخ بداية رصد درجات الفصل الدراسي الثاني")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? SecondSemesterControlStartDate { get; set; }

        [Display(Name = "تاريخ بداية تظلمات الفصل الدراسي الثاني")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? SecondSemesterObjectionStartDate { get; set; }

        [Display(Name = "تاريخ غلق باب تظلمات الفصل الدراسي الثاني")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? SecondSemesterObjectionEndDate { get; set; }


// دور نوفمبر


        [Display(Name = "تاريخ بداية امتحانات دور نوفمبر")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? NovemberExamsStartDate { get; set; }


        [Display(Name = "تاريخ بداية رصد درجات دور نوفمبر")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? NovemberControlStartDate { get; set; }

        [Display(Name = "تاريخ بداية تظلمات دور نوفمبر")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? NovemberObjectionStartDate { get; set; }

        [Display(Name = "تاريخ غلق باب تظلمات دور نوفمبر")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? NovemberObjectionEndDate { get; set; }

        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public ICollection<InstructorEnrollment> InstructorEnrollments { get; set; }
        public IEnumerable<StudentEnrollment> StudentEnrollments { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }

        [NotMapped]
        public List<Branch> Branches { get; set; }
        [NotMapped]
        [Display(Name = "الشعبة")]
        public string BranchName { get; set; }
        [NotMapped]
        public List<Level> Levels { get; set; }
        [NotMapped]
        [Display(Name = "الفرقة الدراسية")]
        public string LevelName { get; set; }
        [NotMapped]
        public List<CourseEnrollment> CourseEnrollments { get; set; }

    }
}
