using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using appVelas.Models;
using appVelas.Service.Interfaces;
using System.Net.Http.Json;
using appVelas.Service;

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

        public async Task<CustomApiResponse<List<Cliente>>> GetClientesAsync()
        {
            var response = await Helper.ParseApiResponse<List<Cliente>>(
                await _httpClient.GetAsync("/api/GetClientes")
            );

            return response;
        }

        public async Task<CustomApiResponse<Cliente>> BuscarClienteAsync(Guid idCliente)
        {
            var response = await Helper.ParseApiResponse<Cliente>(
                await _httpClient.GetAsync($"/api/BuscarCliente/{idCliente}")
            );

            return response;
        }

        public async Task<CustomApiResponse<Cliente>> InsertarClienteAsync(Cliente cliente)
        {
            var response = await Helper.ParseApiResponse<Cliente>(
                await _httpClient.PostAsJsonAsync("/api/InsertarCliente", cliente)
            );

            return response;
        }

        public async Task<CustomApiResponse<Cliente>> ActualizarClienteAsync(Cliente cliente)
        {
            var response = await Helper.ParseApiResponse<Cliente>(
                await _httpClient.PutAsJsonAsync("/api/ActualizarCliente", cliente)
            );

            return response;
        }
    }
}
