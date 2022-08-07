using AddressWebApi.Configuretion;
using AddressWebApi.Data;
using AddressWebApi.Interface;
using AddressWebApi.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressWebApi.Service
{
    public class AddressService : GenericService<Address>, IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IGenericRepository<Address> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApiDbContext _context;

        public AddressService(IGenericRepository<Address> genericRepository, IUnitOfWork unitOfWork, ApiDbContext context,IAddressRepository addressRepository) : base(genericRepository, unitOfWork, context)
        {
            _unitOfWork = unitOfWork;
            _repository = genericRepository;   
            _context = context;
            _addressRepository = addressRepository;
        }
        public override async Task<IEnumerable<Address>> GetAll()
        {
            return await _addressRepository.GetAll();
        }
        public override async Task<Address> GetById(int Id)
        {
            return await _addressRepository.GetById(Id);
        }

        public override void Add(Address address)
        {
            using var Transaction = _context.Database.BeginTransaction();
            try
            {
                _repository.Add(address);
                _unitOfWork.addressRepository.SaveChange();
                Transaction.Commit();
            }
            catch(Exception ex)
            {
                Transaction.Rollback();
            }
        }
        public override void Update(Address address)
        {
            using var Transaction = _context.Database.BeginTransaction();
            try
            {
                _repository.Update(address);
                _unitOfWork.addressRepository.SaveChange();
            }
            catch (Exception ex)
            {
                Transaction.Rollback();
            }

        }
        public override void Delete(Address address)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _repository.Delete(address);
                _unitOfWork.addressRepository.SaveChange();
            }
            catch(Exception ex)
            {
                transaction.Rollback();
            }

        }

        public async Task <IEnumerable<Address>> GetByUserId(int UserId)
        {
            return await _addressRepository.GetByUserId(UserId);
        }

        public async Task<Address> DeleteByAddressId(int Id)
        {
            var deleteById = await _repository.GetById(Id);
            if (deleteById != null)
            {
                return await _addressRepository.DeleteByAddressId(deleteById);
            }
            throw new Exception();
        }
    }
}
