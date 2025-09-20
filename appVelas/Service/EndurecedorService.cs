using appVelas.Models;
using appVelas.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http.Json;


namespace appVelas.Service
{
    public class EndurecedorService : IEndurecedorService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public EndurecedorService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:BaseUrl"];

            _httpClient.BaseAddress = new Uri(_baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Endurecedor>> GetEndurecedorsAsync()
        {
            var response = await _httpClient.GetAsync("/api/GetEndurecedors");

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<List<Endurecedor>>();

            return new List<Endurecedor>();
        }

        public async Task<Endurecedor> BuscarEndurecedorAsync(Guid idEndurecedor)
        {
            var response = await _httpClient.GetAsync($"/api/BuscarEndurecedor/{idEndurecedor}");

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<Endurecedor>();

            return null;
        }

        public async Task<bool> InsertarEndurecedorAsync(Endurecedor Endurecedor)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/InsertarEndurecedor", Endurecedor);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarEndurecedorAsync(Endurecedor Endurecedor)
        {
            var response = await _httpClient.PutAsJsonAsync("/api/ActualizarEndurecedor", Endurecedor);
            return response.IsSuccessStatusCode;
        }
    }
}
