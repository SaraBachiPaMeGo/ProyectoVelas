using appVelas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Service.Interfaces
{
    public interface IVelaPigmentoService
    {
        Task<CustomApiResponse<List<VelaPigmento>>> GetPigmentosPorVelaAsync();
        Task<CustomApiResponse<VelaPigmento>> BuscarVelaPigmentoAsync(Guid idVelaPigmento);
        Task<CustomApiResponse<List<VelaPigmento>>> InsertarVelaPigmentoAsync(List<VelaPigmento> velaPigmento);
        Task<CustomApiResponse<List<VelaPigmento>>> ActualizarVelaPigmentoAsync(List<VelaPigmento> velaPigmento);
        Task<CustomApiResponse<List<VelaPigmento>>> EliminarRelacionesPigmentosAsync(List<VelaPigmento> velaPigmento);
        
    }
}
