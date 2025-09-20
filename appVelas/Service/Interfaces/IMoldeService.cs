using appVelas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Service.Interfaces
{
    public interface IMoldeService
    {
        Task<CustomApiResponse<List<Molde>>> GetMoldesAsync();
        Task<CustomApiResponse<Molde>> BuscarMoldeAsync(Guid idMolde);
        Task<CustomApiResponse<Molde>> InsertarMoldeAsync(Molde Molde);
        Task<CustomApiResponse<Molde>> ActualizarMoldeAsync(Molde Molde); 
    }
}
