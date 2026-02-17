using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using appVelas.Models;
using appVelas.Service.Interfaces;
using Microsoft.AspNetCore.Http;

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

        public async Task<CustomApiResponse<Molde>> InsertarMoldeAsync(MultipartFormDataContent form)
        {
            return await _moldeService.InsertarMoldeAsync(form);
        }

        public async Task<CustomApiResponse<Molde>> ActualizarMoldeAsync(Guid id, MultipartFormDataContent form)
        {
            return await _moldeService.ActualizarMoldeAsync(id, form);
        }

        public async Task<CustomApiResponse<bool>> EliminarAsync(Guid id)
        {
            return await _moldeService.EliminarMoldeAsync(id);
        }
    }
}
