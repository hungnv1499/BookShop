using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookLibrarySystem.Data;
using BookLibrarySystem.Data.Models;
using Microsoft.AspNetCore.Authorization;
using BookLibrarySystem.Data.Services.Repositories;
using Microsoft.Data.SqlClient;
namespace BookLibrarySystem.Areas.Admin.Pages.Author
{
    [Authorize(Roles ="Admin")]
    public class CreateModel : PageModel
    {
        public IAuthorRepository AuthorRepository;
        public CreateModel(IAuthorRepository authorRepository)
        {
            AuthorRepository = authorRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Authors Authors { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                var author = AuthorRepository.Add(Authors);
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("duplicate"))
                {
                    ModelState.AddModelError(string.Empty, "Author name is been duplicate");
                }
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
