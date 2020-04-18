using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrarySystem.Data.Models
{
    public class AppUser : IdentityUser<int>
    {
        public AppUser() : base()
        {
            Balance = 0;
        }
        public string Fullname { get; set; }
        public string Lastname { get; set; }
        public float Balance { get; set; }
        public string Address { get; set; }

    }
}
