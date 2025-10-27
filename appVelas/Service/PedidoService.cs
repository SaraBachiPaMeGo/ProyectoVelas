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

        public PedidoService(HttpClient httpClient)
        {
             
            _httpClient = httpClient;
        }

        public async Task<CustomApiResponse<List<Pedido>>> GetPedidosAsync()
        {
            var respons = await _httpClient.GetAsync("/api/Pedido/GetPedidos");

            var dos = await respons.Content.ReadAsStringAsync();

            var response = await Helper.ParseApiResponse<List<Pedido>>(
                dos
            );

            return response;
        }

        public async Task<CustomApiResponse<Pedido>> BuscarPedidoAsync(Guid idPedido)
        {
            var respons = await _httpClient.GetAsync($"/api/Pedido/BuscarPedido/{idPedido}");

            var dos = await respons.Content.ReadAsStringAsync();

            var response = await Helper.ParseApiResponse<Pedido>(
              dos
            );

            return response;
        }

        public async Task<CustomApiResponse<Pedido>> InsertarPedidoAsync(Pedido pedido)
        {
            var respons = await _httpClient.PostAsJsonAsync("/api/Pedido/InsertarPedido", pedido);

            var dos = await respons.Content.ReadAsStringAsync();

            var response = await Helper.ParseApiResponse<Pedido>(
              dos
            );

            return response;
        }

        public async Task<CustomApiResponse<Pedido>> ActualizarPedidoAsync(Pedido pedido)
        {
            var respons = await _httpClient.PutAsJsonAsync("/api/Pedido/ActualizarPedido", pedido);

            var dos = await respons.Content.ReadAsStringAsync();

            var response = await Helper.ParseApiResponse<Pedido>(
              dos
            );

            return response;
        }
    }
}
