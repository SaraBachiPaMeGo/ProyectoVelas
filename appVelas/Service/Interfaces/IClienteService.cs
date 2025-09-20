using appVelas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Service.Interfaces
{
    public interface IClienteService
    {
        Task<List<Cliente>> GetClientesAsync();
        Task<Cliente> BuscarClienteAsync(Guid idCliente);
        Task<bool> InsertarClienteAsync(Cliente cliente);
        Task<bool> ActualizarClienteAsync(Cliente cliente);
    }
}
