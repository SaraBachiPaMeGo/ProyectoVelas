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
    public class PackService : IPackService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public PackService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:BaseUrl"];

            _httpClient.BaseAddress = new Uri(_baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Pack>> GetPacksAsync()
        {
            var response = await _httpClient.GetAsync("/api/GetPacks");

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<List<Pack>>();

            return new List<Pack>();
        }

        public async Task<Pack> BuscarPackAsync(Guid idPack)
        {
            var response = await _httpClient.GetAsync($"/api/BuscarPack/{idPack}");

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<Pack>();

            return null;
        }

        public async Task<bool> InsertarPackAsync(Pack Pack)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/InsertarPack", Pack);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarPackAsync(Pack Pack)
        {
            var response = await _httpClient.PutAsJsonAsync("/api/ActualizarPack", Pack);
            return response.IsSuccessStatusCode;
        }
    }
}
