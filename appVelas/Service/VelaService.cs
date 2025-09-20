using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using appVelas.Models;
using appVelas.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;


namespace appVelas.Services
{
    public class VelaService : IVelaService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public VelaService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:BaseUrl"];

            _httpClient.BaseAddress = new Uri(_baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Vela>> GetVelasAsync()
        {
            var response = await _httpClient.GetAsync("/api/GetVelas");

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<List<Vela>>();

            return new List<Vela>();
        }

        public async Task<Vela> BuscarVelaAsync(Guid idVela)
        {
            var response = await _httpClient.GetAsync($"/api/BuscarVela/{idVela}");

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<Vela>();

            return null;
        }

        public async Task<bool> InsertarVelaAsync(Vela Vela)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/InsertarVela", Vela);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarVelaAsync(Vela Vela)
        {
            var response = await _httpClient.PutAsJsonAsync("/api/ActualizarVela", Vela);
            return response.IsSuccessStatusCode;
        }
    }
}
