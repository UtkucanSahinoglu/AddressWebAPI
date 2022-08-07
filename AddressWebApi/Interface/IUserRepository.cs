using AddressWebApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressWebApi.Interface
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task <IEnumerable<User>> GetByUserFirstNameAndLastName(string FirstName, string LastName);
        void SaveChange();
    }
}
