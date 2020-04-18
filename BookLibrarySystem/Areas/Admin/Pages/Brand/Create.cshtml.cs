using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookLibrarySystem.Data;
using BookLibrarySystem.Data.Models;
using BookLibrarySystem.Data.Services.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace BookLibrarySystem.Areas.Admin.Pages.Brand
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        public IBrandRepository BrandRepository;
        public CreateModel(IBrandRepository brandRepository)
        {
            BrandRepository = brandRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Brands Brands { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                var author = BrandRepository.Add(Brands);
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("duplicate"))
                {
                    ModelState.AddModelError(string.Empty, "Brand name is been duplicate");
                }
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
