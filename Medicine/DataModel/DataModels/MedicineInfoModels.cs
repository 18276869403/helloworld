using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.DataModels
{
    public class MedicineInfoModels
    {

        [Required]
        [StringLength(50)]
        public string ClassifyName { get; set; }

        public int UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string DosageName { get; set; }

        [Required]
        [StringLength(50)]
        public string E_Name { get; set; }

        public int Number { get; set; }

        [Required]
        [StringLength(50)]
        public string PackName { get; set; }

        [Required]
        [StringLength(50)]
        public string ChineseName { get; set; }

        [Required]
        [StringLength(50)]
        public string ForeignName { get; set; }

        [Required]
        [StringLength(50)]
        public string MedicineID { get; set; }

        [Required]
        [StringLength(255)]
        public string M_Function { get; set; }

        [Required]
        [StringLength(255)]
        public string Ingredient { get; set; }

        public DateTime? StateDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        [StringLength(50)]
        public string Etalon { get; set; }

        [Required]
        [StringLength(255)]
        public string Taboo { get; set; }

        [Required]
        [StringLength(255)]
        public string Notes { get; set; }

        [StringLength(255)]
        public string Pharmacology { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(50)]
        public string RepositName { get; set; }

    }
}
