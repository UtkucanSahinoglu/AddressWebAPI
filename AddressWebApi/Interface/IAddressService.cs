using AddressWebApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace AddressWebApi.Interface
{
    public interface IAddressService : IGenericService<Address>
    {
        Task <IEnumerable<Address>> GetByUserId(int UserId);
        Task <Address> DeleteByAddressId(int Id);
    }
}
