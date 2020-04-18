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
using Microsoft.AspNetCore.Identity;

namespace BookLibrarySystem.Pages.Order
{
    public class IndexModel : PageModel
    {
        public IOrderRepository OrderRepository;
        public IOrderDetailRepository OrderDetailRepository;
        public UserManager<AppUser> UserManager;


        public IndexModel(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository,
            UserManager<AppUser> userManager)
        {
            OrderRepository = orderRepository;
            OrderDetailRepository = orderDetailRepository;
            UserManager = userManager;

        }

        public IEnumerable<Orders> Orders { get;set; }

        public IActionResult OnGet()
        {
            int id;
            int.TryParse(UserManager.GetUserId(HttpContext.User), out id);
            Orders = OrderRepository.GetAllByUser(id);
            return Page();
        }
    }
}
