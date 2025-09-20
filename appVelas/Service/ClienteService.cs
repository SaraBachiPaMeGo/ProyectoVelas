using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using appVelas.Models;
using appVelas.Service.Interfaces;
using System.Net.Http.Json;


namespace appVelas.Services
{
    public class ClienteService : IClienteService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
             

        public ClienteService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:BaseUrl"];

            _httpClient.BaseAddress = new Uri(_baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Cliente>> GetClientesAsync()
        {
            var response = await _httpClient.GetAsync("/api/GetClientes");

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<List<Cliente>>();

            return new List<Cliente>();
        }

        public async Task<Cliente> BuscarClienteAsync(Guid idCliente)
        {
            var response = await _httpClient.GetAsync($"/api/BuscarCliente/{idCliente}");

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<Cliente>();

            return null;
        }

        public async Task<bool> InsertarClienteAsync(Cliente Cliente)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/InsertarCliente", Cliente);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarClienteAsync(Cliente Cliente)
        {
            var response = await _httpClient.PutAsJsonAsync("/api/ActualizarCliente", Cliente);
            return response.IsSuccessStatusCode;
        }
    }
}
