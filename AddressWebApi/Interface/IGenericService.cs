using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AddressWebApi.Interface
{
    public interface IGenericService <T> where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        Task<T> DeleteById(int Id);
        void Update(T entity);        
        Task<IEnumerable<T>> GetAll();
        Task <T> GetById(int Id);

    }
}
