using BookLibrarySystem.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrarySystem.Data.Services.Repositories
{
    public interface IBrandRepository : IRepository<Brands>
    {
        public SelectList selectListItems();
    }
    public class BrandRepository : IBrandRepository
    {
        public BrandRepository(BookStoreDBContext context)
        {
            Context = context;
        }

        private BookStoreDBContext Context;

        public Brands Add(Brands entity)
        {
            Context.Add(entity);
            Commit();
            return entity;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public Brands Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                entity.Actived = false;
                entity.Deleted = true;
                entity = Update(entity);
            }
            return entity;
        }


        public IEnumerable<Brands> GetAll()
        {
            return Context.Brands.Where(a => a.Actived == true);
        }

        public Brands GetById(int id)
        {
            return Context.Brands.Find(id);
        }

        public Brands Update(Brands entity)
        {
            entity.DateModified = DateTime.Now;
            var author = Context.Brands.Attach(entity);
            author.State = EntityState.Modified;
            Commit();
            return entity;
        }
        public SelectList selectListItems()
        {
            return new SelectList(Context.Brands, "ID", "BrandName");
        }

    }
}
