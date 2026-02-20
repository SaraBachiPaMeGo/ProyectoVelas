using appVelas.Models;
using appVelas.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace appVelas.Service.Interfaces
{
    public interface IVelaService
    {
        Task<CustomApiResponse<List<VelaDTO>>> GetVelasAsync();
        Task<CustomApiResponse<VelaDTO>> BuscarVelaAsync(Guid idVela);
        Task<CustomApiResponse<VelaDTO>> InsertarVelaAsync(MultipartFormDataContent form);
        Task<CustomApiResponse<VelaDTO>> ActualizarVelaAsync(Guid idVela, MultipartFormDataContent form);

        Task<CustomApiResponse<bool>> EliminarVelaAsync(Guid idVela);

    }
}
