using AddressWebApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressWebApi.Interface
{
    public interface IUserService : IGenericService<User>
    {
        Task <IEnumerable<User>> GetByUserFirstNameAndLastName(string FirstName, string LastName);

    }
}
