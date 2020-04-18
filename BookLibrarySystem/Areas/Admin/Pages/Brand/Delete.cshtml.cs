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

namespace BookLibrarySystem.Areas.Admin.Pages.Brand
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        public IBrandRepository BrandRepository;

        public DeleteModel(IBrandRepository brandRepository)
        {
            BrandRepository = brandRepository;
        }

        [BindProperty]
        public Brands Brands { get; set; }

        public IActionResult OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Brands = BrandRepository.GetById(id.Value);

            if (Brands == null)
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

            Brands = BrandRepository.GetById(id.Value);

            if (Brands != null)
            {
                BrandRepository.Delete(id.Value);
            }
            return RedirectToPage("./Index");
        }
    }
}
