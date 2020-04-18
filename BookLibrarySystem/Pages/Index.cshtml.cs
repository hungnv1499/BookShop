using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookLibrarySystem.Data.Models;
using BookLibrarySystem.Data.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BookLibrarySystem.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly int size = 10;
        public IBookRepository BookRepository;
        public ICategoryRepository CategoryRepository;


        public IndexModel(ILogger<IndexModel> logger, IBookRepository bookRepository,  ICategoryRepository categoryRepository)
        {
            _logger = logger;
            BookRepository = bookRepository;
            CategoryRepository = categoryRepository;
        }
        [BindProperty]
        public IEnumerable<Books> Books { get; set; }
        [BindProperty]
        public IEnumerable<GroupCount> Categories { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchMessage { get; set; }
        public int PageNumber { get; set; }
        public int PageIndex { get; set; }
        [BindProperty(SupportsGet = true)]
        public string CateName { get; set; }
        public IActionResult OnGet(int index = 0, string search = "", string cate = "")
        {
            PageIndex = index;
            SearchMessage = search==""?SearchMessage:search;
            if (SearchMessage == null)
            {
                SearchMessage = "";
            }
            CateName = cate == "" ? CateName : cate;
            if (CateName == null)
            {
                CateName = "";
            }
            try
            {
               Books = BookRepository.GetListConstraint(SearchMessage.Trim(), index, size, CateName);
                int count = BookRepository.GetCountConstraint(SearchMessage.Trim(), CateName);
               PageNumber = count / size;
                if (count % size != 0) PageNumber++;
                Categories = BookRepository.GetListCountByCategory();
            }
            catch (Exception ex)
            {
                throw;
            }
            return Page();
        }
        public JsonResult OnGetAddToCart(int id)
        {
            return new JsonResult("sucessful");
        }
    }
}
