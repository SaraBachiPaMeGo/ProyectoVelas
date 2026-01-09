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
        public async Task<CustomApiResponse<List<Cera>>> GetCerasAsync()
        {
            return await _ceraService.GetCerasAsync();
        }

        public async Task<CustomApiResponse<Cera>> BuscarCeraAsync(Guid id)
        {
            return await _ceraService.BuscarCeraAsync(id);
        }

        public async Task<CustomApiResponse<Cera>> InsertarCeraAsync(Cera cera)
        {
            return await _ceraService.InsertarCeraAsync(cera);
        }

        public async Task<CustomApiResponse<Cera>> ActualizarCeraAsync(Guid id, Cera cera)
        {
            return await _ceraService.ActualizarCeraAsync(id, cera);
        }

        public async Task<CustomApiResponse<bool>> EliminarAsync(Guid id)
        {
            return await _ceraService.EliminarCeraAsync(id);
        }
    }
}
