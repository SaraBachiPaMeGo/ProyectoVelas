using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using appVelas.Models;
using appVelas.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace appVelas.Service
{
    public class MoldeService : IMoldeService
    {
        private readonly HttpClient _httpClient;

        public MoldeService(HttpClient httpClient)
        {
             
            _httpClient = httpClient;
        }

        public async Task<CustomApiResponse<List<Molde>>> GetMoldesAsync()
        {
            var response = await Helper.ParseApiResponse<List<Molde>>(
                await _httpClient.GetAsync("/api/GetMoldes")
            );

            return response;
        }

        public async Task<CustomApiResponse<Molde>> BuscarMoldeAsync(Guid idMolde)
        {
            var response = await Helper.ParseApiResponse<Molde>(
                await _httpClient.GetAsync($"/api/BuscarMolde/{idMolde}")
            );

            return response;
        }

        public async Task<CustomApiResponse<Molde>> InsertarMoldeAsync(Molde molde)
        {
            var response = await Helper.ParseApiResponse<Molde>(
                await _httpClient.PostAsJsonAsync("/api/InsertarMolde", molde)
            );

            return response;
        }

        public async Task<CustomApiResponse<Molde>> ActualizarMoldeAsync(Molde molde)
        {
            var response = await Helper.ParseApiResponse<Molde>(
                await _httpClient.PutAsJsonAsync("/api/ActualizarMolde", molde)
            );

            return response;
        }
    }
}
