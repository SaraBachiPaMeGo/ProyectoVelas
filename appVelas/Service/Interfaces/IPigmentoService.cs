using appVelas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Service.Interfaces
{
    public interface IPigmentoService
    {
        Task<CustomApiResponse<List<Pigmento>>> GetPigmentosAsync();
        Task<CustomApiResponse<Pigmento>> BuscarPigmentoAsync(Guid idPigmento);
        Task<CustomApiResponse<Pigmento>> InsertarPigmentoAsync(Pigmento Pigmento);
        Task<CustomApiResponse<Pigmento>> ActualizarPigmentoAsync(Pigmento Pigmento);
    }
}
