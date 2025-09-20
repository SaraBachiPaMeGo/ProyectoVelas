using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using appVelas.Models;
using appVelas.Service.Interfaces;

namespace appVelas.Repository
{
    public class RepositoryPedidos
    {
        private readonly IPedidoService _pedidoService;


        public RepositoryPedidos(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        // ------------------------------------- Pedido ---------------------------------------------
        public async Task<List<Pedido>> GetPedidosAsync()
        {
            return await _pedidoService.GetPedidosAsync();
        }

        public async Task<Pedido> BuscarPedidoAsync(Guid id)
        {
            return await _pedidoService.BuscarPedidoAsync(id);
        }

        public async Task<bool> InsertarPedidoAsync(Pedido pedido)
        {
            return await _pedidoService.InsertarPedidoAsync(pedido);
        }

        public async Task<bool> ActualizarPedidoAsync(Pedido pedido)
        {
            return await _pedidoService.ActualizarPedidoAsync(pedido);
        }


    }
}
