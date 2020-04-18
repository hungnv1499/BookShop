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
    public class IndexModel : PageModel
    {
        public IBrandRepository BrandRepository;
        public IndexModel(IBrandRepository brandRepository)
        {
            BrandRepository = brandRepository;
        }

        public IEnumerable<Brands> Brands { get; set; }

        public IActionResult OnGetAsync()
        {
            Brands = BrandRepository.GetAll();
            return Page();
        }
    }
}
