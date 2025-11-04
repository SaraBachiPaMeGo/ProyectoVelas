using appVelas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Service
{
    public interface ICeraService
    {
        Task<CustomApiResponse<List<Cera>>> GetCerasAsync();
        Task<CustomApiResponse<Cera>> BuscarCeraAsync(Guid idCera);
        Task<CustomApiResponse<Cera>> InsertarCeraAsync(Cera cera);
        Task<CustomApiResponse<Cera>> ActualizarCeraAsync(Guid id, Cera cera);
    }
}
