using appVelas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Service.Interfaces
{
    public interface IMechaService
    {
        Task<CustomApiResponse<List<Mecha>>> GetMechasAsync();
        Task<CustomApiResponse<Mecha>> BuscarMechaAsync(Guid idMecha);
        Task<CustomApiResponse<Mecha>> InsertarMechaAsync(Mecha Mecha);
        Task<CustomApiResponse<Mecha>> ActualizarMechaAsync(Mecha Mecha); 
    }
}
