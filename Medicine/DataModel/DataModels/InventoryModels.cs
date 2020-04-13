using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.DataModels
{
    public class InventoryModels
    {
        public int ID { get; set; }

        public int MarketNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string E_Name { get; set; }
        [Required]
        [StringLength(50)]
        public string MedicineID { get; set; }

        [Required]
        [StringLength(50)]
        public string ChineseName { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(50)]
        public string ForeignName { get; set; }

        public DateTime EnterDate { get; set; }

        public int PriceID { get; set; }

        [Column("Price", TypeName = "money")]
        public decimal Price { get; set; }

        public int Number { get; set; }

        [Required]
        [StringLength(255)]
        public string M_Function { get; set; }

        public string Guarantee { get; set; }
    }
}
