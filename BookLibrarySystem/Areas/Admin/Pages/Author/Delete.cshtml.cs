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
    public class DeleteModel : PageModel
    {
        public IAuthorRepository AuthorRepository;
        public DeleteModel(IAuthorRepository authorRepository)
        {
            AuthorRepository = authorRepository;
        }

        [BindProperty]
        public Authors Authors { get; set; }

        public IActionResult OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Authors = AuthorRepository.GetById(id.Value);

            if (Authors == null)
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

            Authors = AuthorRepository.GetById(id.Value);

            if (Authors != null)
            {
                AuthorRepository.Delete(id.Value);
            }
            return RedirectToPage("./Index");
        }
    }
}
