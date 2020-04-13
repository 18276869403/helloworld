using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.DataModels
{
    public class MarketInfoModels
    {

        [Required]
        [StringLength(50)]
        public string ChineseName { get; set; }

        [Required]
        [StringLength(50)]
        public string ForeignName { get; set; }

        [Required]
        [StringLength(50)]
        public string MedicineID { get; set; }

        public int UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        public int? MarketNumber { get; set; }

        public int ID { get; set; }

        public DateTime? SellTime { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }
    }
}
