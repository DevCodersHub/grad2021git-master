using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace grad2021.Models
{
    public class Selection
    {
        [DatabaseGenerat‌ed(DatabaseGeneratedOp‌​tion.Identity)]
        public int SelectionID { get; set; }

        [Display(Name = "رمز تسجيل الطالب للعام الدراسي")]
        public int StudentEnrollmentID { get; set; }

        [Display(Name = "رقم الرغبة")]
        public int SelectionNo { get; set; }

        [Display(Name = "القسم الحالي للطالب")]
        public string CurrentBranchName { get; set; }

        [Display(Name = "القسم أو الشعبة المرغوب الالتحاق بها")]
        public string SelectionBranchName { get; set; }

        [ForeignKey("StudentEnrollmentID")]
        public StudentEnrollment StudentEnrollment { get; set; }

        [ForeignKey("CurrentBranchName")]
        public Branch CurrentBranch { get; set; }

        [ForeignKey("SelectionBranchName")]
        public Branch SelectionBranch { get; set; }
    }
}
