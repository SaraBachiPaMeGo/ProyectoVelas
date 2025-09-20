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
        //Task<CustomApiResponse<VelaPigmento>> BuscarVelaPigmentoAsync(Guid idVelaPigmento);
        Task<CustomApiResponse<VelaPigmento>> InsertarVelaPigmentoAsync(VelaPigmento velaPigmento);
        //Task<CustomApiResponse<VelaPigmento>> ActualizarVelaPigmentoAsync(VelaPigmento velaPigmento);
    }
}
