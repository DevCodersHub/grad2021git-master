using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace grad2021.Models
{
    public class InstructorProfession
    {
        [DatabaseGenerat‌ed(DatabaseGeneratedOp‌​tion.Identity)]
        public int InstructorProfessionID { get; set; }

        [Display(Name = "الرقم القومي لعضو هيئة التدريس")]
        public string ApplicationUserID { get; set; }

        [Display(Name = "الدرجة الوظيفية")]
        public string ProfessionDegree { get; set; }

        [Display(Name = "تاريخ التعيين أو الترقية")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? PromotionDate { get; set; }

        [ForeignKey("ApplicationUserID")]
        public ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("ProfessionDegree")]
        public Level Level { get; set; }
    }
}
