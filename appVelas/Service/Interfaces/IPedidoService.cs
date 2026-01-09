using System;
using System.Collections.Generic;
using appVelas.Models;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Service.Interfaces
{
    public interface IPedidoService
    {
        Task<CustomApiResponse<List<Pedido>>> GetPedidosAsync();
        Task<CustomApiResponse<Pedido>> BuscarPedidoAsync(Guid idPedido);
        Task<CustomApiResponse<Pedido>> InsertarPedidoAsync(Pedido Pedido);
        Task<CustomApiResponse<Pedido>> ActualizarPedidoAsync(Guid id, Pedido Pedido);

        Task<CustomApiResponse<bool>> EliminarPedidoAsync(Guid idPedido);

    }
}
