using appVelas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Service.Interfaces
{
    public interface IInventarioService
    {
        Task<CustomApiResponse<List<Inventario>>> GetInventariosAsync();
        Task<CustomApiResponse<Inventario>> BuscarInventarioAsync(Guid idInventario);
        Task<CustomApiResponse<Inventario>> InsertarInventarioAsync(Inventario inv);
        Task<CustomApiResponse<Inventario>> ActualizarInventarioAsync(Guid id, Inventario inv);
        Task<CustomApiResponse<bool>> EliminarInventarioAsync(Guid idInventario);
    }
}

