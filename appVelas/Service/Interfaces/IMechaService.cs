using appVelas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Service.Interfaces
{
    public interface IMechaService
    {
        Task<List<Mecha>> GetMechasAsync();
        Task<Mecha> BuscarMechaAsync(Guid idMecha);
        Task<bool> InsertarMechaAsync(Mecha Mecha);
        Task<bool> ActualizarMechaAsync(Mecha Mecha); 
    }
}
