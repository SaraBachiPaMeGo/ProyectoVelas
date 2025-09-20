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
        private readonly string _baseUrl;

        public MoldeService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:BaseUrl"];

            _httpClient.BaseAddress = new Uri(_baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Molde>> GetMoldesAsync()
        {
            var response = await _httpClient.GetAsync("/api/GetMoldes");

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<List<Molde>>();

            return new List<Molde>();
        }

        public async Task<Molde> BuscarMoldeAsync(Guid idMolde)
        {
            var response = await _httpClient.GetAsync($"/api/BuscarMolde/{idMolde}");

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<Molde>();

            return null;
        }

        public async Task<bool> InsertarMoldeAsync(Molde Molde)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/InsertarMolde", Molde);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarMoldeAsync(Molde Molde)
        {
            var response = await _httpClient.PutAsJsonAsync("/api/ActualizarMolde", Molde);
            return response.IsSuccessStatusCode;
        }
    }
}
