using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace grad2021.Models
{
    public class Level
    {

        [Display(Name = "المرحلة / الوظيفة")]
        public string LevelName { get; set; }

        [Display(Name = "الحد الأقصى لعدد مرات الرسوب")]
        public int MaxFailures { get; set; } = 2;

        public ICollection<CourseEnrollment> CourseEnrollments { get; set; }
        public ICollection<StudentEnrollment> StudentEnrollments { get; set; }
        public ICollection<InstructorProfession> InstructorProfessions { get; set; }
    }
}
