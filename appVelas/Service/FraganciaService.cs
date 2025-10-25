using appVelas.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using appVelas.Service.Interfaces;
using System.Net.Http.Json;

namespace appVelas.Service
{
    public class FraganciaService : IFraganciaService
    {
        private readonly HttpClient _httpClient;

        public FraganciaService(HttpClient httpClient)
        {
             
            _httpClient = httpClient;
        }

        public async Task<CustomApiResponse<List<Fragancia>>> GetFraganciasAsync()
        {
            var response = await Helper.ParseApiResponse<List<Fragancia>>(
                await _httpClient.GetAsync("/api/GetFragancias")
            );

            return response;
        }

        public async Task<CustomApiResponse<Fragancia>> BuscarFraganciaAsync(Guid idFragancia)
        {
            var response = await Helper.ParseApiResponse<Fragancia>(
                await _httpClient.GetAsync($"/api/BuscarFragancia/{idFragancia}")
            );

            return response;
        }

        public async Task<CustomApiResponse<Fragancia>> InsertarFraganciaAsync(Fragancia fragancia)
        {
            var response = await Helper.ParseApiResponse<Fragancia>(
                await _httpClient.PostAsJsonAsync("/api/InsertarFragancia", fragancia)
            );

            return response;
        }

        public async Task<CustomApiResponse<Fragancia>> ActualizarFraganciaAsync(Fragancia fragancia)
        {
            var response = await Helper.ParseApiResponse<Fragancia>(
                await _httpClient.PutAsJsonAsync("/api/ActualizarFragancia", fragancia)
            );

            return response;
        }
    }
}
