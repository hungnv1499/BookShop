using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrarySystem.Data.Models
{
    public class Carts
    {
        public List<Item> CartItems { get; set; }
        public void AddToCart(Books book)
        {
            var quantity = 0 ;
            if (CartItems == null)
            {
                CartItems = new List<Item>();
            }
            else
            {
                var item = CartItems.FirstOrDefault(c => c.Book.ID == book.ID);
                if (item != null)
                {
                    quantity = item.Quantity;
                    CartItems.Remove(item);
                }
            }
            quantity++;
            CartItems.Add(new Item()
            {
                Book = book,
                Quantity = quantity,
            });
        }
        public bool Remove(int id)
        {
            var item = CartItems.FirstOrDefault(i => i.Book.ID == id);
            if (item == null)
            {
                return false;
            }
            return CartItems.Remove(item);
        }
        public bool Update(int id, int quantity)
        {
            var item = CartItems.FirstOrDefault(i => i.Book.ID == id);
            CartItems.Remove(item);
            item.Quantity = quantity;
            CartItems.Add(item);
            return true;
        }
        public bool Update(int id)
        {
            var item = CartItems.FirstOrDefault(i => i.Book.ID == id);
            if (item != null)
            {
                CartItems.Remove(item);
            }
            return true;
        }
    }
    public class Item
    {
        public Books Book { get; set; }
        public int Quantity { get; set; }
    }
}
