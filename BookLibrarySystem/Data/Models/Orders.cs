using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrarySystem.Data.Models
{
    public class Orders
    {
        public Orders()
        {
            Actived = true;
            Deleted = false;
            TotalPrice = Quantity = 0;
            Status = 0;
            DateCreated = DateTime.Now;
        }
        [Key]
        public int ID { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        public AppUser User { get; set; }
        public int Quantity { get; set; }
        public float TotalPrice { get; set; }
        public DateTime DateCreated { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }
        public bool Actived { get; set; }
        public bool Deleted { get; set; }
        ICollection<OrderDetails> OrderDetails { get; set; }
        ICollection<Payments> Payments { get; set; }
    }
}
