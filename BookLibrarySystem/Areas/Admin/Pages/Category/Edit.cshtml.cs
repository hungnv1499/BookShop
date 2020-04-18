using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookLibrarySystem.Data;
using BookLibrarySystem.Data.Models;
using BookLibrarySystem.Data.Services.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace BookLibrarySystem.Areas.Admin.Pages.Category
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        public EditModel(ICategoryRepository categoryRepository)
        {
            CategoryRepository = categoryRepository;
        }

        [BindProperty]
        public Categories Categories { get; set; }
        public ICategoryRepository CategoryRepository;

        public IActionResult OnGetAsync(int? id)
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
                var cate = CategoryRepository.Update(Categories);
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
