using BookLibrarySystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrarySystem.Data.Services.Repositories
{

    public interface IOrderDetailRepository : IRepository<OrderDetails>
    {
        public IEnumerable<OrderDetails> GetByOrderId(int id);
    }
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private BookStoreDBContext Context;
        public OrderDetailRepository(BookStoreDBContext context)
        {
            Context = context;
        }

        public OrderDetails Add(OrderDetails entity)
        {
            Context.Add(entity);
            Commit();
            return entity;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public OrderDetails Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderDetails> GetAll()
        {
            throw new NotImplementedException();

        }

        public OrderDetails GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderDetails> GetByOrderId(int id)
        {
            return Context.OrderDetails.Where(o => o.Order.ID == id).OrderByDescending(o => o.DateCreated);
        }

        public OrderDetails Update(OrderDetails entity)
        {
            throw new NotImplementedException();
        }
    }
}
