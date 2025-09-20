using appVelas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Service.Interfaces
{
    public interface IPigmentoService
    {
        Task<List<Pigmento>> GetPigmentosAsync();
        Task<Pigmento> BuscarPigmentoAsync(Guid idPigmento);
        Task<bool> InsertarPigmentoAsync(Pigmento Pigmento);
        Task<bool> ActualizarPigmentoAsync(Pigmento Pigmento);
    }
}
