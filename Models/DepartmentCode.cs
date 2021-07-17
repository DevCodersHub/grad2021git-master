using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace grad2021.Models
{
    public class DepartmentCode
    {
        [Display(Name = "الرمز")]
        public string DepartmentCodeValue { get; set; }

        [Display(Name = "قسم")]
        public string DepartmentName { get; set; }

        [ForeignKey("DepartmentName")]
        public Department Department { get; set; }

        public ICollection<Course> Courses { get; set; }

    }
}