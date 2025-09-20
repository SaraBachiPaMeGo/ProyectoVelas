using System;
using System.Collections.Generic;
using appVelas.Models;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Service.Interfaces
{
    public interface IPedidoService
    {
        Task<List<Pedido>> GetPedidosAsync();
        Task<Pedido> BuscarPedidoAsync(Guid idPedido);
        Task<bool> InsertarPedidoAsync(Pedido Pedido);
        Task<bool> ActualizarPedidoAsync(Pedido Pedido);
    }
}
