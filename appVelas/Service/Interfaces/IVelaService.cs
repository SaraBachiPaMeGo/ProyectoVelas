using appVelas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Service.Interfaces
{
    public interface IVelaService
    {
        Task<CustomApiResponse<List<Vela>>> GetVelasAsync();
        Task<CustomApiResponse<Vela>> BuscarVelaAsync(Guid idVela);
        Task<CustomApiResponse<Vela>> InsertarVelaAsync(Vela Vela);
        Task<CustomApiResponse<Vela>> ActualizarVelaAsync(Guid idVela, Vela Vela);
    }
}
