using appVelas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Service.Interfaces
{
    public interface IMoldeService
    {
        Task<List<Molde>> GetMoldesAsync();
        Task<Molde> BuscarMoldeAsync(Guid idMolde);
        Task<bool> InsertarMoldeAsync(Molde Molde);
        Task<bool> ActualizarMoldeAsync(Molde Molde); 
    }
}
