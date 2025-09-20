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

        public async Task<CustomApiResponse<List<Pedido>>> GetPedidosAsync()
        {
            var response = await Helper.ParseApiResponse<List<Pedido>>(
                await _httpClient.GetAsync("/api/GetPedidos")
            );

            return response;
        }

        public async Task<CustomApiResponse<Pedido>> BuscarPedidoAsync(Guid idPedido)
        {
            var response = await Helper.ParseApiResponse<Pedido>(
                await _httpClient.GetAsync($"/api/BuscarPedido/{idPedido}")
            );

            return response;
        }

        public async Task<CustomApiResponse<Pedido>> InsertarPedidoAsync(Pedido pedido)
        {
            var response = await Helper.ParseApiResponse<Pedido>(
                await _httpClient.PostAsJsonAsync("/api/InsertarPedido", pedido)
            );

            return response;
        }

        public async Task<CustomApiResponse<Pedido>> ActualizarPedidoAsync(Pedido pedido)
        {
            var response = await Helper.ParseApiResponse<Pedido>(
                await _httpClient.PutAsJsonAsync("/api/ActualizarPedido", pedido)
            );

            return response;
        }
    }
}
