using appVelas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Service.Interfaces
{
    public interface IVelaFraganciaService
    {
        Task<CustomApiResponse<List<VelaFragancia>>> GetFraganciasPorVelaAsync();
        //Task<CustomApiResponse<VelaFragancia>> BuscarVelaFraganciaAsync(Guid idVelaFragancia);
        Task<CustomApiResponse<VelaFragancia>> InsertarVelaFraganciaAsync(VelaFragancia velaFragancia);
        //Task<CustomApiResponse<bool>> ActualizarVelaFraganciaAsync(VelaFragancia velaFragancia);
    }
}
