using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrarySystem.Data.Models
{
    public class Payments
    {
        public Payments()
        {
            Actived = true;
            Deleted = false;
            TotalPrice = 0;
            Status = 0;
            DateCreated = DateTime.Now;
        }
        [Key]
        public int ID { get; set; }
        [ForeignKey("Order")]
        public int OrderID { get; set; }
        public Orders Order { get; set; }
        public float TotalPrice { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Actived { get; set; }
        public bool Deleted { get; set; }
    }
}
