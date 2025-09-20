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
        private readonly string _baseUrl;

        public PigmentoService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:BaseUrl"];

            _httpClient.BaseAddress = new Uri(_baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Pigmento>> GetPigmentosAsync()
        {
            var response = await _httpClient.GetAsync("/api/GetPigmentos");

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<List<Pigmento>>();

            return new List<Pigmento>();
        }

        public async Task<Pigmento> BuscarPigmentoAsync(Guid idPigmento)
        {
            var response = await _httpClient.GetAsync($"/api/BuscarPigmento/{idPigmento}");

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<Pigmento>();

            return null;
        }

        public async Task<bool> InsertarPigmentoAsync(Pigmento Pigmento)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/InsertarPigmento", Pigmento);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarPigmentoAsync(Pigmento Pigmento)
        {
            var response = await _httpClient.PutAsJsonAsync("/api/ActualizarPigmento", Pigmento);
            return response.IsSuccessStatusCode;
        }
    }
}
