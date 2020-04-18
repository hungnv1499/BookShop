using BookLibrarySystem.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrarySystem.Data.Services.Repositories
{
    public interface IAuthorRepository : IRepository<Authors>
    {
        public SelectList selectListItems();

    }
    public class AuthorRepository : IAuthorRepository
    {
        public AuthorRepository(BookStoreDBContext context)
        {
            Context = context;
        }

        private BookStoreDBContext Context;

        public Authors Add(Authors entity)
        {
            Context.Add(entity);
            Commit();
            return entity;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public Authors Delete(int id)
        {
            var author = GetById(id);
            if (author != null)
            {
                author.Actived = false;
                author.Deleted = true;
                author = Update(author);
            }
            return author;
        }

        
        public IEnumerable<Authors> GetAll()
        {
            return Context.Authors.Where(a => a.Actived == true);
        }

        public Authors GetById(int id)
        {
            return Context.Authors.Find(id);
        }

        public Authors Update(Authors entity)
        {
            entity.DateModified = DateTime.Now;
            var author = Context.Authors.Attach(entity);
            author.State = EntityState.Modified;
            Commit();
            return entity;
        }
        public SelectList selectListItems()
        {
            return new SelectList(Context.Authors, "ID", "AuthorName");
        }

    }
}
