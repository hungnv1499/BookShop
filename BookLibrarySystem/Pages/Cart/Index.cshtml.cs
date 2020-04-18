using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookLibrarySystem.Data.Models;
using BookLibrarySystem.Data.Services.Repositories;
using BookLibrarySystem.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookLibrarySystem.Pages.Cart
{
    [Authorize(Roles = "User")]
    public class IndexModel : PageModel
    {
        public IndexModel(IBookRepository bookRepository, IOrderDetailRepository orderDetailRepository,
                    IOrderRepository orderRepository, UserManager<AppUser> userManager)
        {
            BookRepository = bookRepository;
            OrderDetailRepository = orderDetailRepository;
            OrderRepository = orderRepository;
            UserManager = userManager;
        }

        public IBookRepository BookRepository;
        public IOrderDetailRepository OrderDetailRepository;
        public IOrderRepository OrderRepository;
        [BindProperty(SupportsGet = true)]
        public Carts Cart { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Address { get; set; }
        public float Total { get; set; }
        public string Message { get; set; }
        public UserManager<AppUser> UserManager;

        public IActionResult OnGet()
        {
            try
            {
                string id = UserManager.GetUserId(HttpContext.User);
                Cart = SessionHelper.GetObjectFromJson<Carts>(HttpContext.Session, "cart" + id);
                if (Cart != null)
                {
                    foreach (var item in Cart.CartItems)
                    {
                        Total += item.Book.UnitPrice * item.Quantity;
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToPage("/Error");
            }
            return Page();
        }
        public JsonResult OnGetAddToCart(int id)
        {
            var result = Enum.GetName(typeof(AjaxStatusEnum), AjaxStatusEnum.Success);
            try
            {
                string userId = UserManager.GetUserId(HttpContext.User);

                var book = BookRepository.GetById(id);
                var cart = SessionHelper.GetObjectFromJson<Carts>(HttpContext.Session, "cart" + userId);
                if (cart == null)
                {
                    cart = new Carts();
                }
                cart.AddToCart(book);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart" + userId, cart);
                result += " add " + book.BookName;
            }
            catch (Exception ex)
            {
                result = Enum.GetName(typeof(AjaxStatusEnum), AjaxStatusEnum.Fail);
            }
            return new JsonResult(result);
        }
        public JsonResult OnGetUpdate(int id, int quantity)
        {
            string userId = UserManager.GetUserId(HttpContext.User);

            Cart = SessionHelper.GetObjectFromJson<Carts>(HttpContext.Session, "cart" + userId);
            if (Cart != null)
            {
                Cart.Update(id, quantity);
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart" + userId, Cart);
            return new JsonResult("success");
        }
        public JsonResult OnGetDeleteItem(int id)
        {
            try
            {
                string userId = UserManager.GetUserId(HttpContext.User);
                Cart = SessionHelper.GetObjectFromJson<Carts>(HttpContext.Session, "cart" + userId);
                if (Cart != null)
                {
                    Cart.Remove(id);
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart" + userId, Cart);
            }
            catch (Exception ex)
            {
                return new JsonResult("error");
            }
            return new JsonResult("success");
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool checkOutOfQuantity = false;
                    string userId = UserManager.GetUserId(HttpContext.User);
                    Cart = SessionHelper.GetObjectFromJson<Carts>(HttpContext.Session, "cart" + userId);
                    if (Cart != null)
                    {
                        if (Cart != null)
                        {

                        }
                        Carts temp =  new Carts();
                        temp = Cart;
                        foreach (var item in temp.CartItems.ToList())
                        {
                            if (item.Book.Quantity < item.Quantity)
                            {
                                checkOutOfQuantity = true;
                                Cart.Update(item.Book.ID, item.Book.Quantity);
                            }
                        }
                        foreach (var item in Cart.CartItems)
                        {
                            Total += item.Book.UnitPrice * item.Quantity;
                        }
                        if (checkOutOfQuantity)
                        {
                            
                            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart" + userId, Cart);
                            Message = "out of quantity, set max quantity of book, try again";
                            return Page();
                        }
                        else
                        {
                            Orders orders = new Orders()
                            {
                                UserID = int.Parse(userId),
                                Address = Address,
                                Status = (int)OrderStatusEnum.New,
                                TotalPrice = Total
                               
                            };
                            orders = OrderRepository.Add(orders);
                            foreach (var item in Cart.CartItems.ToList())
                            {
                                Books book = BookRepository.GetById(item.Book.ID);
                                OrderDetails orderDetails = new OrderDetails()
                                {
                                    Order = orders,
                                    OrderID = orders.ID,
                                    Quantity = item.Quantity,
                                    TotalPrice = item.Quantity * item.Book.UnitPrice,
                                    UnitPrice = item.Book.UnitPrice,
                                    Book = book,
                                    BookID = item.Book.ID
                                };
                                OrderDetailRepository.Add(orderDetails);
                                book.Quantity -= orderDetails.Quantity;
                                BookRepository.Update(book);
                            }
                            SessionHelper.SetObjectAsJson(HttpContext.Session,"cart" + userId, null);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Message = "some thing wrong, try again";
                    return Page();
                }
            }
            return RedirectToPage("/Index");
        }
    }
}