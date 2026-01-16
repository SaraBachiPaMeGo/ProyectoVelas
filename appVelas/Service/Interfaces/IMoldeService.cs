using appVelas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace appVelas.Service.Interfaces
{
    public interface IMoldeService
    {
        Task<CustomApiResponse<List<Molde>>> GetMoldesAsync();
        Task<CustomApiResponse<Molde>> BuscarMoldeAsync(Guid idMolde);
        Task<CustomApiResponse<Molde>> InsertarMoldeAsync(MultipartFormDataContent form);
        Task<CustomApiResponse<Molde>> ActualizarMoldeAsync(Guid id, Molde Molde);

        Task<CustomApiResponse<bool>> EliminarMoldeAsync(Guid idMolde);

    }
}
