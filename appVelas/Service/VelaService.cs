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
    public class VelaService : IVelaService
    {
        private readonly HttpClient _httpClient;

        public VelaService(HttpClient httpClient)
        {
             
            _httpClient = httpClient;
        }

        //(IHttpClientFactory httpClientFactory)
        //{
        //    _httpClient = httpClientFactory.CreateClient("ApiClient");
        //}

        public async Task<CustomApiResponse<List<Vela>>> GetVelasAsync()        
        {
            var respons = await _httpClient.GetAsync("/api/Vela/GetVelas");

            var dos = await respons.Content.ReadAsStringAsync();

            var response = await Helper.ParseApiResponse<List<Vela>>(
              dos
            );

            return response;
        }

        public async Task<CustomApiResponse<Vela>> BuscarVelaAsync(Guid idVela)
        {
            var respons = await _httpClient.GetAsync($"/api/Vela/BuscarVela/{idVela}");

            var dos = await respons.Content.ReadAsStringAsync();

            var response = await Helper.ParseApiResponse<Vela>(
              dos
            );

            return response;
        }

        public async Task<CustomApiResponse<Vela>> InsertarVelaAsync(Vela vela)
        {
            var respons = await _httpClient.PostAsJsonAsync($"/api/Vela/InsertarVela", vela);

            var dos = await respons.Content.ReadAsStringAsync();

            var response = await Helper.ParseApiResponse<Vela>(
              dos
            );

            return response;
        }

        public async Task<CustomApiResponse<Vela>> ActualizarVelaAsync(Vela vela)
        {
            var respons = await _httpClient.PutAsJsonAsync($"/api/Vela/ActualizarVela", vela);

            var dos = await respons.Content.ReadAsStringAsync();

            var response = await Helper.ParseApiResponse<Vela>(
              dos
            );

            return response;
        }
    }
}
