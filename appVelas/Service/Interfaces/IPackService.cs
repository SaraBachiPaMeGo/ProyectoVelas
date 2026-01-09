using appVelas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Service.Interfaces
{
    public interface IPackService
    {
        Task<CustomApiResponse<List<Pack>>> GetPacksAsync();
        Task<CustomApiResponse<Pack>> BuscarPackAsync(Guid idPack);
        Task<CustomApiResponse<Pack>> InsertarPackAsync(Pack Pack);
        Task<CustomApiResponse<Pack>> ActualizarPackAsync(Guid id, Pack Pack);
        Task<CustomApiResponse<bool>> EliminarPackAsync(Guid idPack);

    }
}
