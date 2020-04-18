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

namespace BookLibrarySystem.Areas.Admin.Pages.Category
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        public ICategoryRepository CategoryRepository;
        public DeleteModel(ICategoryRepository categoryRepository)
        {
            CategoryRepository = categoryRepository;
        }

        [BindProperty]
        public Categories Categories { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Categories = CategoryRepository.GetById(id.Value);

            if (Categories == null)
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

            Categories = CategoryRepository.GetById(id.Value);

            if (Categories != null)
            {
                CategoryRepository.Delete(id.Value);
            }
            return RedirectToPage("./Index");
        }
    }
}
