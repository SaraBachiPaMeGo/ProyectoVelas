using appVelas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Service
{
    public interface ICeraService
    {
        Task<List<Cera>> GetCerasAsync();
        Task<Cera> BuscarCeraAsync(Guid idCera);
        Task<bool> InsertarCeraAsync(Cera cera);
        Task<bool> ActualizarCeraAsync(Cera cera);
    }
}
