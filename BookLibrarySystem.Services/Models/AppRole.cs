using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibrarySystem.Models
{
    class AppRole : IdentityUser<int>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
