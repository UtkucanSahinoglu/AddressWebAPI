using AddressWebApi.Configuretion;
using AddressWebApi.Data;
using AddressWebApi.Interface;
using AddressWebApi.Model;
using AddressWebApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressWebApi.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository 
    {
        private readonly ApiDbContext _context;

        public UserRepository(ApiDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<User>> GetAll()
        {
            var result = await _context.Set<User>().Include(x => x.Addresses).ToListAsync();
            return result;
        }

        public override async Task<User> GetById(int Id)
        {

            var result = await _context.users.SingleAsync(x => x.Id == Id);

            _context.Entry(result)
                    .Collection(x => x.Addresses)
                    .Load();
            return result;
        }

        public async Task <IEnumerable<User>> GetByUserFirstNameAndLastName(string FirstName, string LastName)
        {
            return await _context.Set<User>().Where(x => x.FirstName == FirstName && x.LastName == LastName).ToListAsync();
        }

        public void SaveChange()
        {
            _context.SaveChanges();
        }
    }
}
