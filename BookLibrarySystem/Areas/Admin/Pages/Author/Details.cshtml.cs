using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookLibrarySystem.Data;
using BookLibrarySystem.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace BookLibrarySystem.Areas.Admin.Pages.Author
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        private readonly BookLibrarySystem.Data.BookStoreDBContext _context;

        public DetailsModel(BookLibrarySystem.Data.BookStoreDBContext context)
        {
            _context = context;
        }

        public Authors Authors { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Authors = await _context.Authors.FirstOrDefaultAsync(m => m.ID == id);

            if (Authors == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
