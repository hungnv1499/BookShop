using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrarySystem.Data.Models
{
    public class Books
    {
        public Books()
        {
            Actived = true;
            Deleted = false;
            UnitPrice =  Quantity = 0;
            DateCreated = DateModified = DateTime.Now;
            Image = "default.png";

        }
        [Key]
        public int ID { get; set; }
        [Required, MaxLength(100,ErrorMessage = "Ten sach khong duoc dai qua 100 ki tu")]
        public string BookName { get; set; }
        [Required]
        public string Description  { get; set; }
        [ForeignKey("Brand")]
        public int BrandID { get; set; }
        public Brands Brand { get; set; }
        [ForeignKey("Author")]
        public int AuthorID { get; set; }
        public Authors Author { get; set; }
        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public Categories Category { get; set; }
        public Nullable<int> temp { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool Actived { get; set; }
        public bool Deleted { get; set; }
        ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
