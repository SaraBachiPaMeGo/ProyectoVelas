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
        public async Task<List<Pack>> GetPacksAsync()
        {
            return await _packService.GetPacksAsync();
        }

        public async Task<Pack> BuscarPackAsync(Guid id)
        {
            return await _packService.BuscarPackAsync(id);
        }

        public async Task<bool> InsertarPackAsync(Pack pack)
        {
            return await _packService.InsertarPackAsync(pack);
        }

        public async Task<bool> ActualizarPackAsync(Pack pack)
        {
            return await _packService.ActualizarPackAsync(pack);
        }
    }
}
