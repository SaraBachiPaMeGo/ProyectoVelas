using appVelas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Service.Interfaces
{
    public interface IPackService
    {
        Task<List<Pack>> GetPacksAsync();
        Task<Pack> BuscarPackAsync(Guid idPack);
        Task<bool> InsertarPackAsync(Pack Pack);
        Task<bool> ActualizarPackAsync(Pack Pack);
    }
}
