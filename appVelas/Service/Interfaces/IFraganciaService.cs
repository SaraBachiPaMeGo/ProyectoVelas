using appVelas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Service.Interfaces
{
    public interface IFraganciaService
    {
        Task<List<Fragancia>> GetFraganciasAsync();
        Task<Fragancia> BuscarFraganciaAsync(Guid idFragancia);
        Task<bool> InsertarFraganciaAsync(Fragancia fragancia);
        Task<bool> ActualizarFraganciaAsync(Fragancia fragancia);
    }
}
