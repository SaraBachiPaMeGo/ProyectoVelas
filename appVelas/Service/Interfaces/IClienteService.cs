using appVelas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Service.Interfaces
{
    public interface IClienteService
    {
        Task<CustomApiResponse<List<Cliente>>> GetClientesAsync();
        Task<CustomApiResponse<Cliente>> BuscarClienteAsync(Guid idCliente);
        Task<CustomApiResponse<Cliente>> InsertarClienteAsync(Cliente cliente);
        Task<CustomApiResponse<Cliente>> ActualizarClienteAsync(Guid id, Cliente cliente);
        Task<CustomApiResponse<bool>> EliminarClienteAsync(Guid idCliente);

    }
}
