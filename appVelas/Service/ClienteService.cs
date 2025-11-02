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

        public ClienteService(HttpClient httpClient)
        {
             
            _httpClient = httpClient;
        }

        public async Task<CustomApiResponse<List<Cliente>>> GetClientesAsync()
        {
            var response = new CustomApiResponse<List<Cliente>>();

            try
            {
                var respons = await _httpClient.GetAsync($"/api/Cliente/GetClientes");

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<List<Cliente>>(
                  dos
                );

                return response;
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };

                return response;

            }

        }

        public async Task<CustomApiResponse<Cliente>> BuscarClienteAsync(Guid idCliente)
        {
            var response = new CustomApiResponse<Cliente>();

            try
            {
                var respons = await _httpClient.GetAsync($"/api/Cliente/BuscarCliente/{idCliente}");

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<Cliente>(
                  dos
                );

                return response;
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };

                return response;

            }
           
        }

        public async Task<CustomApiResponse<Cliente>> InsertarClienteAsync(Cliente cliente)
        {
            var response = new CustomApiResponse<Cliente>();

            try
            {
                var respons = await _httpClient.PostAsJsonAsync($"/api/Cliente/InsertarCliente", cliente);

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<Cliente>(
                  dos
                );

                return response;
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };

                return response;

            }
            
        }

        public async Task<CustomApiResponse<Cliente>> ActualizarClienteAsync(Cliente cliente)
        {
            var response = new CustomApiResponse<Cliente>();

            try
            {
                var respons = await _httpClient.PutAsJsonAsync($"/api/Cliente/ActualizarCliente", cliente);

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<Cliente>(
                  dos
                );

                return response;
            }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };

                return response;

            }

        }
    }
}
