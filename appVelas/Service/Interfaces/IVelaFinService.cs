using appVelas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Service.Interfaces
{
    public interface IVelaFinService
    {
        Task<CustomApiResponse<List<VelaFinalizada>>> GetVelaFinalizadasAsync();
        Task<CustomApiResponse<VelaFinalizada>> BuscarVelaFinalizadaAsync(Guid idVelaFinalizada);
        Task<CustomApiResponse<VelaFinalizada>> InsertarVelaFinalizadaAsync(VelaFinalizada VelaFinalizada);
        Task<CustomApiResponse<VelaFinalizada>> ActualizarVelaFinalizadaAsync(Guid idVelaFinalizada, VelaFinalizada VelaFinalizada);
    }
}
