using AddressWebApi.Configuretion;
using AddressWebApi.Data;
using AddressWebApi.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AddressWebApi.Repository
{
    public class GenericRepository <T> : IGenericRepository<T> where T : class
    {
        protected ApiDbContext _context { get; set; }

        public GenericRepository(ApiDbContext context)
        {
            this._context = context;
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetById(int Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }

        public void Add(T entity)
        {
          _context.Set<T>().AddAsync(entity);
            //_context.SaveChanges();
   
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            //_unitOfWork.CompleteAsync();
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            return _context.Set<T>().FirstOrDefault(filter);        
        }

        public async Task<T> DeleteById(T entity)
        {
            //var delete = await _context.Set<T>().FindAsync(Id);
            //if (delete == null)
            //{
            //    return null;
            //}
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
