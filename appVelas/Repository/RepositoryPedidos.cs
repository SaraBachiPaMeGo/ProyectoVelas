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
        public async Task<CustomApiResponse<List<Pedido>>> GetPedidosAsync()
        {
            return await _pedidoService.GetPedidosAsync();
        }

        public async Task<CustomApiResponse<Pedido>> BuscarPedidoAsync(Guid id)
        {
            return await _pedidoService.BuscarPedidoAsync(id);
        }

        public async Task<CustomApiResponse<Pedido>> InsertarPedidoAsync(Pedido pedido)
        {
            return await _pedidoService.InsertarPedidoAsync(pedido);
        }

        public async Task<CustomApiResponse<Pedido>> ActualizarPedidoAsync(Guid id, Pedido pedido)
        {
            return await _pedidoService.ActualizarPedidoAsync(id, pedido);
        }

        public async Task<CustomApiResponse<bool>> EliminarAsync(Guid id)
        {
            return await _pedidoService.EliminarPedidoAsync(id);
        }
    }
}
