using AddressWebApi.Interface;
using AddressWebApi.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpPost("AddAddress")]
        public void Add(Address address)
        {
            _addressService.Add(address);
        }
        [HttpDelete("Delete")]
        public void Delete(Address address)
        {
            _addressService.Delete(address);
        }
        [HttpGet("AllAddress")]
        public async Task<IEnumerable<Address>> GetAll()
        {
            return await _addressService.GetAll();
        }
        [HttpGet("GetById")]
        public async Task<Address> GetById(int Id)
        {
            return await _addressService.GetById(Id);
        }
        [HttpPut("Update")]
        public void Update(Address address)
        {
            _addressService.Update(address);
        }
        [HttpGet("GetByUserId")]
        public async Task <IEnumerable<Address>> GetByUserId(int UserId)
        {
            return await _addressService.GetByUserId(UserId);
        }
        [HttpDelete("DeleteByAddressId")]
        public async Task<Address> DeleteByAddressId(int Id)
        {
            return await _addressService.DeleteByAddressId(Id);
        }

    }
}
