using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks; 
using appVelas.Service;
using appVelas.Models;

namespace appVelas.Repository
{
    public class RepositoryCeras
    {
        private readonly ICeraService _ceraService;


        public RepositoryCeras(ICeraService ceraService)
        {
            _ceraService = ceraService;
        }

        // ------------------------------------- CERA ---------------------------------------------
        public async Task<List<Cera>> GetCerasAsync()
        {
            return await _ceraService.GetCerasAsync();
        }

        public async Task<Cera> BuscarCeraAsync(Guid id)
        {
            return await _ceraService.BuscarCeraAsync(id);
        }

        public async Task<bool> InsertarCeraAsync(Cera cera)
        {
            return await _ceraService.InsertarCeraAsync(cera);
        }

        public async Task<bool> ActualizarCeraAsync(Cera cera)
        {
            return await _ceraService.ActualizarCeraAsync(cera);
        }
    }
}
