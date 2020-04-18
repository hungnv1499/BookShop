using BookLibrarySystem.Data.Models;
using BookLibrarySystem.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrarySystem.Data.Services.Repositories
{
    public interface IOrderRepository : IRepository<Orders>
    {
        public IEnumerable<Orders> GetAllByUser(int id);
    }
    public class OrderRepository : IOrderRepository
    {
        private BookStoreDBContext Context;
        public OrderRepository(BookStoreDBContext context)
        {
            Context = context;
        }
        public Orders Add(Orders entity)
        {
            Context.Add(entity);
            Commit();
            return entity;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public Orders Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Orders> GetAll()
        {
            return Context.Orders.Where(a => a.Actived == true && a.Status != (int)OrderStatusEnum.Deleted);
        }

        public IEnumerable<Orders> GetAllByUser(int id)
        {
            return Context.Orders.OrderByDescending(d => d.DateCreated).Where(a => a.Actived == true && a.Status != (int)OrderStatusEnum.Deleted && a.UserID == id);
        }

        public Orders GetById(int id)
        {
            return Context.Orders.FirstOrDefault(o => o.ID == id);
        }

        public Orders Update(Orders entity)
        {
            throw new NotImplementedException();
        }
    }
}
