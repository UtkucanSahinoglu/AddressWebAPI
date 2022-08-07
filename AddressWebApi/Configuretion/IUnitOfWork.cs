using AddressWebApi.Interface;
using System;
using System.Threading.Tasks;

namespace AddressWebApi.Configuretion
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository userRepository { get; }
        IAddressRepository addressRepository { get; }
        Task CompleteAsync();
    }
}
