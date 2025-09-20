using appVelas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Service.Interfaces
{
    public interface IEndurecedorService
    {
        Task<List<Endurecedor>> GetEndurecedorsAsync();
        Task<Endurecedor> BuscarEndurecedorAsync(Guid idEndurecedor);
        Task<bool> InsertarEndurecedorAsync(Endurecedor endurecedor);
        Task<bool> ActualizarEndurecedorAsync(Endurecedor endurecedor);
    }
}
