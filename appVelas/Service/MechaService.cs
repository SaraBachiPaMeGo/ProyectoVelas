using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using appVelas.Models;
using appVelas.Service.Interfaces;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;

namespace appVelas.Service
{
    public class MechaService : IMechaService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public MechaService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:BaseUrl"];

            _httpClient.BaseAddress = new Uri(_baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<CustomApiResponse<List<Mecha>>> GetMechasAsync()
        {
            var response = await Helper.ParseApiResponse<List<Mecha>>(
                await _httpClient.GetAsync("/api/GetMechas")
            );

            return response;
        }

        public async Task<CustomApiResponse<Mecha>> BuscarMechaAsync(Guid idMecha)
        {
            var response = await Helper.ParseApiResponse<Mecha>(
                await _httpClient.GetAsync($"/api/BuscarMecha/{idMecha}")
            );

            return response;
        }

        public async Task<CustomApiResponse<Mecha>> InsertarMechaAsync(Mecha mecha)
        {
            var response = await Helper.ParseApiResponse<Mecha>(
                await _httpClient.PostAsJsonAsync("/api/InsertarMecha", mecha)
            );

            return response;
        }

        public async Task<CustomApiResponse<Mecha>> ActualizarMechaAsync(Mecha mecha)
        {
            var response = await Helper.ParseApiResponse<Mecha>(
                await _httpClient.PutAsJsonAsync("/api/ActualizarMecha", mecha)
            );

            return response;
        }
    }
}
