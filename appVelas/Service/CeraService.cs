using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using appVelas.Models;
using appVelas.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using NPOI.SS.Formula.Functions;

namespace appVelas.Service
{
    public class CeraService : ICeraService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public CeraService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:BaseUrl"];

            _httpClient.BaseAddress = new Uri(_baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<CustomApiResponse<List<Cera>>> GetCerasAsync()
        {
            var response = await Helper.ParseApiResponse<List<Cera>>(await _httpClient.GetAsync("/api/GetCeras"));

            return response;
        }

        public async Task<CustomApiResponse<Cera>> BuscarCeraAsync(Guid idCera)
        {
            var response = await Helper.ParseApiResponse<Cera>(await _httpClient.GetAsync($"/api/BuscarCera/{idCera}"));

            return response;
        }

        public async Task<CustomApiResponse<Cera>> InsertarCeraAsync(Cera cera)
        {
            var response = await Helper.ParseApiResponse<Cera>(await _httpClient.PostAsJsonAsync("/api/InsertarCera", cera));

            return response;
        }

        public async Task<CustomApiResponse<Cera>> ActualizarCeraAsync(Cera cera)
        {
            var response = await Helper.ParseApiResponse<Cera>(await _httpClient.PutAsJsonAsync("/api/ActualizarCera", cera));

            return response;
        }
    }
}

