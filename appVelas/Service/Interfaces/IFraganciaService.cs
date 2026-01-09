using appVelas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Service.Interfaces
{
    public interface IFraganciaService
    {
        Task<CustomApiResponse<List<Fragancia>>> GetFraganciasAsync();
        Task<CustomApiResponse<Fragancia>> BuscarFraganciaAsync(Guid idFragancia);
        Task<CustomApiResponse<Fragancia>> InsertarFraganciaAsync(Fragancia fragancia);
        Task<CustomApiResponse<Fragancia>> ActualizarFraganciaAsync(Guid id, Fragancia fragancia);

        Task<CustomApiResponse<bool>> EliminarFraganciaAsync(Guid idFragancia);

    }
}
