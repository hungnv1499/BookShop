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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace BookLibrarySystem.Areas.Admin.Pages.Book
{
    [Authorize(Roles ="Admin")]
    public class CreateModel : PageModel
    {
        private IAuthorRepository AuthorRepository;
        private IBookRepository BookRepository;
        private IBrandRepository BrandRepository;
        private ICategoryRepository CategoryRepository;
        public IWebHostEnvironment WebHostEnvironment;

        public CreateModel(IBookRepository bookRepository, IAuthorRepository authorRepository,
                            IBrandRepository brandRepository, ICategoryRepository categoryRepository,
                            IWebHostEnvironment webHostEnvironment)
        {
            BookRepository = bookRepository;
            AuthorRepository = authorRepository;
            BrandRepository = brandRepository;
            CategoryRepository = categoryRepository;
            WebHostEnvironment = webHostEnvironment;

        }

        public IActionResult OnGet()
        {
            ViewData["AuthorID"] = AuthorRepository.selectListItems();
        ViewData["BrandID"] = BrandRepository.selectListItems();
            ViewData["CategoryID"] = CategoryRepository.selectListItems();
            return Page();
        }

        [BindProperty]
        public Books Books { get; set; }
        [BindProperty]
        public IFormFile Photo { get; set; }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                if (Photo != null)
                {
                    // If a new photo is uploaded, the existing photo must be
                    // deleted. So check if there is an existing photo and delete
                    //if (Books.Image != null)
                    //{
                    //    string filePath = Path.Combine(WebHostEnvironment.WebRootPath,
                    //        "images", Books.Image);
                    //    System.IO.File.Delete(filePath);
                    //}
                    // Save the new photo in wwwroot/images folder and update
                    // PhotoPath property of the employee object
                    Books.Image = ProcessUploadedFile();
                }
                var entity = BookRepository.Add(Books);
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("duplicate"))
                {
                    ModelState.AddModelError(string.Empty, "Book name is been duplicate");
                }
                return Page();
            }
            return RedirectToPage("./Index");
        }
        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;

            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(WebHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
