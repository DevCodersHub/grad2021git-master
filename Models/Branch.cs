using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace grad2021.Models
{
    public class Branch
    {
        [Display(Name = "شعبة")]
        public string BranchName { get; set; }

        [Display(Name = "قسم")]
        public string DepartmentName { get; set; }

        [Display(Name = "نبذة عن الشعبة")]
        public string? BranchDescription { get; set; }

        [Display(Name = " السعة الكلية للسنة الأولى")]
        public int FullCapacity { get; set; }

        [Display(Name = " السعة الحالية للسنة الأولى")]
        public int CurrentCapacity { get; set; } = 0;

        [Display(Name = "نبذة عن الشعبة")]
        public SelectionStatus SelectionStatus { get; set; }

        [ForeignKey("DepartmentName")]
        public Department Department { get; set; }


        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public ICollection<CourseEnrollment> CourseEnrollments { get; set; }
        public ICollection<StudentEnrollment> StudentEnrollments { get; set; }
        public ICollection<Selection> CurrentBranches { get; set; }
        public ICollection<Selection> SelectionBranches { get; set; }
    }
}
