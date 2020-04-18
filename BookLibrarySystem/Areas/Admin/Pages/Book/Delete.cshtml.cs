using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookLibrarySystem.Data;
using BookLibrarySystem.Data.Models;
using BookLibrarySystem.Data.Services.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace BookLibrarySystem.Areas.Admin.Pages.Book
{
    [Authorize(Roles = "Admin")]

    public class DeleteModel : PageModel
    {
        public IBookRepository BookRepository;
        public DeleteModel(IBookRepository bookRepository)
        {
            BookRepository = bookRepository;
        }

        [BindProperty]
        public Books Books { get; set; }

        public IActionResult OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Books = BookRepository.GetById(id.Value);

            if (Books == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Books = BookRepository.GetById(id.Value);

            if (Books != null)
            {
                BookRepository.Delete(id.Value);
            }
            return RedirectToPage("./Index");
        }
    }
}
