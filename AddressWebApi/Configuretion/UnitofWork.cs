using AddressWebApi.Data;
using AddressWebApi.Interface;
using AddressWebApi.Repository;
using System.Threading.Tasks;

namespace AddressWebApi.Configuretion
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly ApiDbContext _context;
        public IUserRepository userRepository { get; private set; }
        public IAddressRepository addressRepository { get; private set; }

        public UnitOfWork(ApiDbContext context)
        {
            _context = context;
            userRepository = new UserRepository(_context);
            addressRepository = new AddressRepository(_context);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
