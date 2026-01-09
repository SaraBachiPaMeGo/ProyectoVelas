using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using appVelas.Models;
using appVelas.Service.Interfaces;

namespace appVelas.Repository
{
    public class RepositoryMoldes
    {
        private readonly IMoldeService _moldeService;


        public RepositoryMoldes(IMoldeService moldeService)
        {
            _moldeService = moldeService;
        }

        // ------------------------------------- Molde ---------------------------------------------
        public async Task<CustomApiResponse<List<Molde>>> GetMoldesAsync()
        {
            return await _moldeService.GetMoldesAsync();
        }

        public async Task<CustomApiResponse<Molde>> BuscarMoldeAsync(Guid id)
        {
            return await _moldeService.BuscarMoldeAsync(id);
        }

        public async Task<CustomApiResponse<Molde>> InsertarMoldeAsync(Molde molde)
        {
            return await _moldeService.InsertarMoldeAsync(molde);
        }

        public async Task<CustomApiResponse<Molde>> ActualizarMoldeAsync(Guid id, Molde molde)
        {
            return await _moldeService.ActualizarMoldeAsync(id, molde);
        }

        public async Task<CustomApiResponse<bool>> EliminarAsync(Guid id)
        {
            return await _moldeService.EliminarMoldeAsync(id);
        }
    }
}
