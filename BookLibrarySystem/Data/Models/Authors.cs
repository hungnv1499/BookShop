using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrarySystem.Data.Models
{
    public class Authors
    {
        public Authors()
        {
            Actived = true;
            Deleted = false;
            DateCreated = DateModified = DateTime.Now;
        }
        [Key]
        public int ID { get; set; }
        [Required]
        public string AuthorName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool Actived { get; set; }
        public bool Deleted { get; set; }
        ICollection<Books> Books { get; set; }
    }
}
