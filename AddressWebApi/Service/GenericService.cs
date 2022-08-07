using AddressWebApi.Configuretion;
using AddressWebApi.Data;
using AddressWebApi.Interface;
using AddressWebApi.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressWebApi.Service
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        protected IGenericRepository<T> _repository;
        protected IUnitOfWork _unitOfWork;
        protected ApiDbContext _context;


        public GenericService(IGenericRepository<T> genericRepository, IUnitOfWork unitOfWork,ApiDbContext context)
        {
            _repository = genericRepository;
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public virtual void Add(T entity)
        {
            _repository.Add(entity);          
        }

        public virtual void Delete(T entity)
        {
            _repository.Delete(entity);
        }

        public virtual async Task<T> DeleteById(int Id)
        {
            var deleteById = _repository.GetById(Id);
            return await deleteById;
            //if (deleteById == null)
            //    return null;
            //return await _repository.DeleteById(deleteById);

        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _repository.GetAll();           
        }

        public virtual async Task<T> GetById(int Id)
        {
            return await _repository.GetById(Id);
        }

        public virtual void Update(T entity)
        {
            _repository.Update(entity);
        }
    }
}
