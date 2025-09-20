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
    public class CeraService : ICeraService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public CeraService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:BaseUrl"];

            _httpClient.BaseAddress = new Uri(_baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Cera>> GetCerasAsync()
        {
            var response = await _httpClient.GetAsync("/api/GetCeras");

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<List<Cera>>();

            return new List<Cera>();
        }

        public async Task<Cera> BuscarCeraAsync(Guid idCera)
        {
            var response = await _httpClient.GetAsync($"/api/BuscarCera/{idCera}");

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<Cera>();

            return null;
        }

        public async Task<bool> InsertarCeraAsync(Cera cera)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/InsertarCera", cera);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarCeraAsync(Cera cera)
        {
            var response = await _httpClient.PutAsJsonAsync("/api/ActualizarCera", cera);
            return response.IsSuccessStatusCode;
        }
    }
}

