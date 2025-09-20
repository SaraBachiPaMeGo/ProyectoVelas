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

        public async Task<CustomApiResponse<List<Vela>>> GetVelasAsync()
        {
            var response = await Helper.ParseApiResponse<List<Vela>>(await _httpClient.GetAsync("/api/GetVelas"));
            return response;
        }

        public async Task<CustomApiResponse<Vela>> BuscarVelaAsync(Guid idVela)
        {
            var response = await Helper.ParseApiResponse<Vela>(
                await _httpClient.GetAsync($"/api/BuscarVela/{idVela}")
            );
            return response;
        }

        public async Task<CustomApiResponse<Vela>> InsertarVelaAsync(Vela vela)
        {
            var response = await Helper.ParseApiResponse<Vela>(
                await _httpClient.PostAsJsonAsync("/api/InsertarVela", vela)
            );
            return response;
        }

        public async Task<CustomApiResponse<Vela>> ActualizarVelaAsync(Vela vela)
        {
            var response = await Helper.ParseApiResponse<Vela>(
                await _httpClient.PutAsJsonAsync("/api/ActualizarVela", vela)
            );
            return response;
        }
    }
}
