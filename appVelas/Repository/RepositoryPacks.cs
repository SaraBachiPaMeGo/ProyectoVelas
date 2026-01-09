using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using appVelas.Models;
using appVelas.Service.Interfaces;

namespace appVelas.Repository
{
    public class RepositoryPacks
    {
        private readonly IPackService _packService;


        public RepositoryPacks(IPackService packService)
        {
            _packService = packService;
        }

        // ------------------------------------- Pack ---------------------------------------------
        public async Task<CustomApiResponse<List<Pack>>> GetPacksAsync()
        {
            return await _packService.GetPacksAsync();
        }

        public async Task<CustomApiResponse<Pack>> BuscarPackAsync(Guid id)
        {
            return await _packService.BuscarPackAsync(id);
        }

        public async Task<CustomApiResponse<Pack>> InsertarPackAsync(Pack pack)
        {
            return await _packService.InsertarPackAsync(pack);
        }

        public async Task<CustomApiResponse<Pack>> ActualizarPackAsync(Guid id, Pack pack)
        {
            return await _packService.ActualizarPackAsync(id, pack);
        }

        public async Task<CustomApiResponse<bool>> EliminarAsync(Guid id)
        {
            return await _packService.EliminarPackAsync(id);
        }
    }
}
