using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookLibrarySystem.Data;
using BookLibrarySystem.Data.Services.Repositories;
using BookLibrarySystem.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace BookLibrarySystem.Areas.Admin.Pages.Category
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        public CreateModel(ICategoryRepository categoryRepository)
        {
            CategoryRepository = categoryRepository;
        }

        public ICategoryRepository CategoryRepository;

        public IActionResult OnGet()
        {
            return Page();
        }
        [BindProperty]
        public Categories Categories { get; set; }
    
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                var author = CategoryRepository.Add(Categories);
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
