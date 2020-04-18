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

namespace BookLibrarySystem.Areas.Admin.Pages.Book
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        private readonly BookLibrarySystem.Data.BookStoreDBContext _context;

        public DetailsModel(BookLibrarySystem.Data.BookStoreDBContext context)
        {
            _context = context;
        }

        public Books Books { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Books = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Brand)
                .Include(b => b.Category).FirstOrDefaultAsync(m => m.ID == id);

            if (Books == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
