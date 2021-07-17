using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace grad2021.Models
{
    public class Department
    {
        [Display(Name = "قسم")]
        public string DepartmentName { get; set; }

        [Display(Name = "نبذة عن القسم")]
        public string? DepartmentDescription { get; set; }

        public ICollection<Branch> Branches { get; set; }
        public ICollection<DepartmentCode> DepartmentCodes { get; set; }
    }
}
