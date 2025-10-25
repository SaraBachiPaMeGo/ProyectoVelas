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
    public class PigmentoService : IPigmentoService
    {
        private readonly HttpClient _httpClient;

        public PigmentoService(HttpClient httpClient)
        {
             
            _httpClient = httpClient;
        }

        public async Task<CustomApiResponse<List<Pigmento>>> GetPigmentosAsync()
        {
            var response = await Helper.ParseApiResponse<List<Pigmento>>(
                await _httpClient.GetAsync("/api/GetPigmentos")
            );

            return response;
        }

        public async Task<CustomApiResponse<Pigmento>> BuscarPigmentoAsync(Guid idPigmento)
        {
            var response = await Helper.ParseApiResponse<Pigmento>(
                await _httpClient.GetAsync($"/api/BuscarPigmento/{idPigmento}")
            );

            return response;
        }

        public async Task<CustomApiResponse<Pigmento>> InsertarPigmentoAsync(Pigmento pigmento)
        {
            var response = await Helper.ParseApiResponse<Pigmento>(
                await _httpClient.PostAsJsonAsync("/api/InsertarPigmento", pigmento)
            );

            return response;
        }

        public async Task<CustomApiResponse<Pigmento>> ActualizarPigmentoAsync(Pigmento pigmento)
        {
            var response = await Helper.ParseApiResponse<Pigmento>(
                await _httpClient.PutAsJsonAsync("/api/ActualizarPigmento", pigmento)
            );

            return response;
        }
    }
}
