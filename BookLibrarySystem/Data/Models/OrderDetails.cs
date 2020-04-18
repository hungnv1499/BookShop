using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrarySystem.Data.Models
{
    public class OrderDetails
    {
        public OrderDetails()
        {
            Actived = true;
            Deleted = false;
            UnitPrice = TotalPrice = Quantity = 0;
            DateCreated = DateTime.Now;

        }
        [Key]
        public int ID { get; set; }
        [ForeignKey("Order")]
        public int OrderID { get; set; }
        public Orders Order  { get; set; }
        [ForeignKey("Book")]
        public int BookID { get; set; }
        public Books Book{ get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
        public float TotalPrice { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Actived { get; set; }
        public bool Deleted { get; set; }
    }
}
