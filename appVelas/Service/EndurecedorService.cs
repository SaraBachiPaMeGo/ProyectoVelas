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
             
            _httpClient = httpClient;
        }

        public async Task<CustomApiResponse<List<Endurecedor>>> GetEndurecedorsAsync()
        {
            var respons = await _httpClient.GetAsync($"/api/Endurecedor/GetEndurecedores");

            var dos = await respons.Content.ReadAsStringAsync();

            var response = await Helper.ParseApiResponse<List<Endurecedor>>(
              dos
            );

            return response;
        }

        public async Task<CustomApiResponse<Endurecedor>> BuscarEndurecedorAsync(Guid idEndurecedor)
        {
            var respons = await _httpClient.GetAsync($"/api/Endurecedor/BuscarEndurecedor/{idEndurecedor}");

            var dos = await respons.Content.ReadAsStringAsync();

            var response = await Helper.ParseApiResponse<Endurecedor>(
              dos
            );

            return response;
        }

        public async Task<CustomApiResponse<Endurecedor>> InsertarEndurecedorAsync(Endurecedor endurecedor)
        {
            var respons = await _httpClient.PostAsJsonAsync($"/api/Endurecedor/InsertarEndurecedor", endurecedor);

            var dos = await respons.Content.ReadAsStringAsync();

            var response = await Helper.ParseApiResponse<Endurecedor>(
              dos
            );
            
            return response;
        }

        public async Task<CustomApiResponse<Endurecedor>> ActualizarEndurecedorAsync(Endurecedor endurecedor)
        {
            var respons = await _httpClient.PutAsJsonAsync($"/api/Endurecedor/ActualizarEndurecedor", endurecedor);

            var dos = await respons.Content.ReadAsStringAsync();

            var response = await Helper.ParseApiResponse<Endurecedor>(
              dos
            );

            return response;
        }
    }
}
