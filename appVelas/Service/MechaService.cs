using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using appVelas.Models;
using appVelas.Service.Interfaces;
using System.Net.Http.Json;

using Microsoft.Extensions.Configuration;

namespace appVelas.Service
{
    public class MechaService : IMechaService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public MechaService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:BaseUrl"];

            _httpClient.BaseAddress = new Uri(_baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Mecha>> GetMechasAsync()
        {
            var response = await _httpClient.GetAsync("/api/GetMechas");

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<List<Mecha>>();

            return new List<Mecha>();
        }

        public async Task<Mecha> BuscarMechaAsync(Guid idMecha)
        {
            var response = await _httpClient.GetAsync($"/api/BuscarMecha/{idMecha}");

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<Mecha>();

            return null;
        }

        public async Task<bool> InsertarMechaAsync(Mecha Mecha)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/InsertarMecha", Mecha);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarMechaAsync(Mecha Mecha)
        {
            var response = await _httpClient.PutAsJsonAsync("/api/ActualizarMecha", Mecha);
            return response.IsSuccessStatusCode;
        }
    }
}
