using AddressWebApi.Configuretion;
using AddressWebApi.Data;
using AddressWebApi.Interface;
using AddressWebApi.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressWebApi.Repository
{
    public class AddressRepository : GenericRepository<Address> , IAddressRepository
    {
        private readonly ApiDbContext _context;

        public AddressRepository(ApiDbContext context) : base(context)
        {
            _context = context;
        }

        public void SaveChange()
        {
            _context.SaveChanges();
        }

        public override async Task<IEnumerable<Address>> GetAll()
        {
            var result = await _context.addresses.Include(x => x.user).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Address>> GetByUserId(int UserId)
        {
            return await _context.Set<Address>().Where(x => x.UserId == UserId).ToListAsync();
        }

        public async Task<Address> DeleteByAddressId(Address address)
        {
            _context.Set<Address>().Remove(address);
            await _context.SaveChangesAsync();
            return address;
        }
    }
}
