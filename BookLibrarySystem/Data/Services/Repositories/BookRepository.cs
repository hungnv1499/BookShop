using BookLibrarySystem.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrarySystem.Data.Services.Repositories
{
    public interface IBookRepository : IRepository<Books>
    {
        public IEnumerable<Books> GetListConstraint(string entityName, int index, int size, string cate);
        public int GetCountConstraint(string entityName, string cate);
        public IEnumerable<GroupCount> GetListCountByCategory();

    }
    public class BookRepository : IBookRepository
    {
        public BookRepository(BookStoreDBContext context)
        {
            Context = context;
        }

        private BookStoreDBContext Context;

        public Books Add(Books entity)
        {
            Context.Add(entity);
            Commit();
            return entity;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public Books Delete(int id)
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


        public IEnumerable<Books> GetAll()
        {
            return Context.Books.Include(a => a.Author)
                                 .Include(b => b.Brand)
                                 .Include(c => c.Category).OrderByDescending(b => b.DateCreated)
                                    .Where(a => a.Actived == true);
        }
        public Books GetById(int id)
        {
            return Context.Books.Include(a => a.Author)
                                 .Include(b => b.Brand)
                                 .Include(c => c.Category)
                                 .FirstOrDefault(b => b.ID == id);
        }

        public Books Update(Books entity)
        {
            entity.DateModified = DateTime.Now;
            var author = Context.Books.Attach(entity);
            author.State = EntityState.Modified;
            Commit();
            return entity;
        }

        public IEnumerable<Books> GetListConstraint(string entityName, int index, int size, string cate)
        {
            return Context.Books.Include(a => a.Author)
                                .Include(b => b.Brand)
                                .Include(c => c.Category)
                                .Where(p => p.Quantity > 0 && p.Actived && !p.Deleted && p.BookName.Contains(entityName.Trim())
                                && p.Category.CategoryName.Contains(cate.Trim())).OrderByDescending(b => b.DateCreated)
                                .Skip(index*size).Take(size);
        }
        public IEnumerable<GroupCount> GetListCountByCategory()
        {
            return Context.Books.Include(c => c.Category).Where(b => b.Category.Actived && b.Actived && b.Quantity > 0).GroupBy(b => b.Category.CategoryName)
                        .Select(g => new GroupCount()
                        {
                            Name = g.Key,
                            Quantity = g.Count()
                        });
        }

        public int GetCountConstraint(string entityName, string cate)
        {
            return Context.Books.Include(a => a.Author)
                                .Include(b => b.Brand)
                                .Include(c => c.Category)
                                .Where(p => p.Quantity > 0 && p.Actived && !p.Deleted && p.BookName.Contains(entityName.Trim())
                                && p.Category.CategoryName.Contains(cate.Trim()))
                                .Count();
        }
    }
}
