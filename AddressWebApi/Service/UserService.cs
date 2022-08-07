using AddressWebApi.Configuretion;
using AddressWebApi.Data;
using AddressWebApi.Interface;
using AddressWebApi.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressWebApi.Service
{
    public class UserService : GenericService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGenericRepository<User> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApiDbContext _context;
        private readonly ILogger<UserService> _logger;


        public UserService(IGenericRepository<User> genericRepository, IUnitOfWork unitOfWork, ApiDbContext context, ILogger<UserService> logger, IUserRepository userRepository) : base(genericRepository, unitOfWork, context)
        {
            _repository = genericRepository;
            _unitOfWork = unitOfWork;
            _context = context;
            _logger = logger;
            _userRepository = userRepository;


        }
        public override async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetAll();
        }
        public override async Task<User> GetById(int Id)
        {
            return await _userRepository.GetById(Id);
        }
        public override void Add(User user)
        {
        
            using var transaction= _context.Database.BeginTransaction();
            try
            {
                _repository.Add(user);
                _unitOfWork.userRepository.SaveChange();
                transaction.Commit();
                _logger.LogInformation("User successfully added");
            }
            catch(Exception ex)
            {
                transaction.Rollback();
                _logger.LogWarning("User can not added");
            }

        }
        public override void Update(User user)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _repository.Update(user);
                _unitOfWork.userRepository.SaveChange();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }

        }
        public override void Delete(User user)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _repository.Delete(user);
                _unitOfWork.userRepository.SaveChange();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }
        public override async Task<User> DeleteById(int Id)
        {
           var deleteByUserId = await _repository.GetById(Id);
           if(deleteByUserId != null)
            {
                return await _repository.DeleteById(deleteByUserId);
            }
            throw new Exception("DeleteById can not be succeed");
        }
        public async Task <IEnumerable<User>> GetByUserFirstNameAndLastName(string FirstName, string LastName)
        {
            return await _userRepository.GetByUserFirstNameAndLastName(FirstName, LastName);
        }

    }
}
