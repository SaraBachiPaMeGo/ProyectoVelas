using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using appVelas.Models;
using appVelas.Service.Interfaces;

namespace appVelas.Repository
{
    public class RepositoryClientes
    {
        private readonly IClienteService _clienteService;


        public RepositoryClientes(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // ------------------------------------- Cliente ---------------------------------------------
        public async Task<CustomApiResponse<List<Cliente>>> GetClientesAsync()
        {
            return await _clienteService.GetClientesAsync();
        }

        public async Task<CustomApiResponse<Cliente>> BuscarClienteAsync(Guid id)
        {
            return await _clienteService.BuscarClienteAsync(id);
        }

        public async Task<CustomApiResponse<Cliente>> InsertarClienteAsync(Cliente cliente)
        {
            return await _clienteService.InsertarClienteAsync(cliente);
        }

        public async Task<CustomApiResponse<Cliente>> ActualizarClienteAsync(Cliente cliente)
        {
            return await _clienteService.ActualizarClienteAsync(cliente);
        }

    }
}
