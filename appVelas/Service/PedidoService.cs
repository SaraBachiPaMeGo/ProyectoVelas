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
    public class PedidoService : IPedidoService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public PedidoService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:BaseUrl"];

            _httpClient.BaseAddress = new Uri(_baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Pedido>> GetPedidosAsync()
        {
            var response = await _httpClient.GetAsync("/api/GetPedidos");

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<List<Pedido>>();

            return new List<Pedido>();
        }

        public async Task<Pedido> BuscarPedidoAsync(Guid idPedido)
        {
            var response = await _httpClient.GetAsync($"/api/BuscarPedido/{idPedido}");

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<Pedido>();

            return null;
        }

        public async Task<bool> InsertarPedidoAsync(Pedido Pedido)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/InsertarPedido", Pedido);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarPedidoAsync(Pedido Pedido)
        {
            var response = await _httpClient.PutAsJsonAsync("/api/ActualizarPedido", Pedido);
            return response.IsSuccessStatusCode;
        }
    }
}
