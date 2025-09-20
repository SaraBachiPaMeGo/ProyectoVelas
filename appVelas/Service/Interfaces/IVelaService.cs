using appVelas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Service.Interfaces
{
    public interface IVelaService
    {
        Task<List<Vela>> GetVelasAsync();
        Task<Vela> BuscarVelaAsync(Guid idVela);
        Task<bool> InsertarVelaAsync(Vela Vela);
        Task<bool> ActualizarVelaAsync(Vela Vela);
    }
}
