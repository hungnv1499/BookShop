using BookLibrarySystem.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrarySystem.Data.Services.Repositories
{
    public interface ICategoryRepository : IRepository<Categories>
    {
        public SelectList selectListItems();
    }
    public class CategoryRepository : ICategoryRepository
    {
        public CategoryRepository(BookStoreDBContext context)
        {
            Context = context;
        }

        private BookStoreDBContext Context;

        public Categories Add(Categories entity)
        {
            Context.Add(entity);
            Commit();
            return entity;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public Categories Delete(int id)
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


        public IEnumerable<Categories> GetAll()
        {
            return Context.Categories.Where(a => a.Actived == true);
        }

        public Categories GetById(int id)
        {
            return Context.Categories.Find(id);
        }

        public Categories Update(Categories entity)
        {
            var author = Context.Categories.Attach(entity);
            author.State = EntityState.Modified;
            Commit();
            return entity;
        }
        public SelectList selectListItems()
        {
            return new SelectList(Context.Categories, "ID", "CategoryName");
        }

    }
}
