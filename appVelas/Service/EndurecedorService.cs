using appVelas.Models;
using appVelas.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace appVelas.Service
{
    public class EndurecedorService : IEndurecedorService
    {
        private readonly HttpClient _httpClient;

        public EndurecedorService(HttpClient httpClient)
        {
            Helper.ConexionApi(httpClient);
            _httpClient = httpClient;
        }

        public async Task<CustomApiResponse<List<Endurecedor>>> GetEndurecedorsAsync()
        {
            var response = await Helper.ParseApiResponse<List<Endurecedor>>(
                await _httpClient.GetAsync("/api/GetEndurecedors")
            );

            return response;
        }

        public async Task<CustomApiResponse<Endurecedor>> BuscarEndurecedorAsync(Guid idEndurecedor)
        {
            var response = await Helper.ParseApiResponse<Endurecedor>(
                await _httpClient.GetAsync($"/api/BuscarEndurecedor/{idEndurecedor}")
            );

            return response;
        }

        public async Task<CustomApiResponse<Endurecedor>> InsertarEndurecedorAsync(Endurecedor endurecedor)
        {
            var response = await Helper.ParseApiResponse<Endurecedor>(
                await _httpClient.PostAsJsonAsync("/api/InsertarEndurecedor", endurecedor)
            );

            return response;
        }

        public async Task<CustomApiResponse<Endurecedor>> ActualizarEndurecedorAsync(Endurecedor endurecedor)
        {
            var response = await Helper.ParseApiResponse<Endurecedor>(
                await _httpClient.PutAsJsonAsync("/api/ActualizarEndurecedor", endurecedor)
            );

            return response;
        }
    }
}
