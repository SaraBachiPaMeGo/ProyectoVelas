using appVelas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Service.Interfaces
{
    public interface IVelaPigmentoService
    {
        Task<List<VelaPigmento>> GetPigmentosPorVelaAsync();
        //Task<VelaPigmento> BuscarVelaPigmentoAsync(Guid idVelaPigmento);
        Task<bool> InsertarVelaPigmentoAsync(VelaPigmento velaPigmento);
        //Task<bool> ActualizarVelaPigmentoAsync(VelaPigmento velaPigmento);
    }
}
