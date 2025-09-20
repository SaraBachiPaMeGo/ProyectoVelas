using appVelas.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly string _baseUrl;

        public FraganciaService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:BaseUrl"];

            _httpClient.BaseAddress = new Uri(_baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Fragancia>> GetFraganciasAsync()
        {
            var response = await _httpClient.GetAsync("/api/GetFragancias");

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<List<Fragancia>>();

            return new List<Fragancia>();
        }

        public async Task<Fragancia> BuscarFraganciaAsync(Guid idFragancia)
        {
            var response = await _httpClient.GetAsync($"/api/BuscarFragancia/{idFragancia}");

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<Fragancia>();

            return null;
        }

        public async Task<bool> InsertarFraganciaAsync(Fragancia Fragancia)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/InsertarFragancia", Fragancia);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarFraganciaAsync(Fragancia Fragancia)
        {
            var response = await _httpClient.PutAsJsonAsync("/api/ActualizarFragancia", Fragancia);
            return response.IsSuccessStatusCode;
        }

    }
}
