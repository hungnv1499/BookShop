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
    public class IndexModel : PageModel
    {
        public IBookRepository BookRepository;
        public IndexModel(IBookRepository bookRepository)
        {
            BookRepository = bookRepository;
        }

        public IEnumerable<Books> Books { get;set; }

        public IActionResult OnGetAsync()
        {
            Books = BookRepository.GetAll();
            return Page();
        }
    }
}
