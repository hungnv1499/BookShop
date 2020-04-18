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
    public class IndexModel : PageModel
    {
        public ICategoryRepository CategoryRepository;
        public IndexModel(ICategoryRepository categoryRepository)
        {
            CategoryRepository = categoryRepository;
        }

        public IEnumerable<Categories> Categories { get;set; }

        public IActionResult OnGetAsync()
        {
            Categories = CategoryRepository.GetAll();
            return Page();
        }
    }
}
