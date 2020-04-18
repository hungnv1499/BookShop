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

namespace BookLibrarySystem.Pages.Order
{
    [Authorize(Roles ="User")]
    public class DetailsModel : PageModel
    {
        public IOrderRepository OrderRepository;
        public IOrderDetailRepository OrderDetailRepository;
        public DetailsModel(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository,
            IBookRepository bookRepository)
        {
            OrderRepository = orderRepository;
            OrderDetailRepository = orderDetailRepository;
            BookRepository = bookRepository;
        }

        public Orders Orders { get; set; }
        public IList<OrderDetails> OrderDetail { get; set; }
        public IBookRepository BookRepository;

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Orders = OrderRepository.GetById(id.Value);
            if (Orders != null)
            {
                OrderDetail = OrderDetailRepository.GetByOrderId(id.Value).ToList();
                for (int i = 0; i < OrderDetail.Count(); i++)
                {
                    OrderDetail[i].Book = BookRepository.GetById(OrderDetail[i].BookID);
                }
            }

            if (Orders == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
