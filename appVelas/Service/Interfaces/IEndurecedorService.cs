using appVelas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Service.Interfaces
{
    public interface IEndurecedorService
    {
        Task<CustomApiResponse<List<Endurecedor>>> GetEndurecedorsAsync();
        Task<CustomApiResponse<Endurecedor>> BuscarEndurecedorAsync(Guid idEndurecedor);
        Task<CustomApiResponse<Endurecedor>> InsertarEndurecedorAsync(Endurecedor endurecedor);
        Task<CustomApiResponse<Endurecedor>> ActualizarEndurecedorAsync(Guid id, Endurecedor endurecedor);
    }
}
