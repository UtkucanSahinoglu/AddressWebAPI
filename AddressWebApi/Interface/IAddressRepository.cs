using AddressWebApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressWebApi.Interface
{
    public interface IAddressRepository : IGenericRepository<Address>
    {
        Task <IEnumerable<Address>> GetByUserId(int UserId);
        Task<Address> DeleteByAddressId(Address address);
        void SaveChange();
    }
}
