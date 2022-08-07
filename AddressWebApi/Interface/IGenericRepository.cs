using AddressWebApi.Configuretion;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AddressWebApi.Interface
{
    public interface IGenericRepository <T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int Id);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T> DeleteById(T entity);
    }
}
