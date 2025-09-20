using appVelas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Service.Interfaces
{
    public interface IVelaFraganciaService
    {
        Task<List<VelaFragancia>> GetFraganciasPorVelaAsync();
        //Task<VelaFragancia> BuscarVelaFraganciaAsync(Guid idVelaFragancia);
        Task<bool> InsertarVelaFraganciaAsync(VelaFragancia velaFragancia);
        //Task<bool> ActualizarVelaFraganciaAsync(VelaFragancia velaFragancia);
    }
}
