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

namespace BookLibrarySystem.Areas.Admin.Pages.Author
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        public IAuthorRepository AuthorRepository;
        public IndexModel(IAuthorRepository authorRepository)
        {
            AuthorRepository = authorRepository;
        }

        public IEnumerable<Authors> Authors { get;set; }

        public IActionResult OnGet()
        {
            Authors = AuthorRepository.GetAll();
            return Page();
        }
    }
}
