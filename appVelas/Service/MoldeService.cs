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
    public class MoldeService : IMoldeService
    {
        private readonly HttpClient _httpClient;

        public MoldeService(HttpClient httpClient)
        {
             
            _httpClient = httpClient;
        }

        public async Task<CustomApiResponse<List<Molde>>> GetMoldesAsync()
        {
            var respons = await _httpClient.GetAsync($"/api/Molde/GetMoldes");

            var dos = await respons.Content.ReadAsStringAsync();

            var response = await Helper.ParseApiResponse<List<Molde>>(
              dos
            );

            return response;
        }

        public async Task<CustomApiResponse<Molde>> BuscarMoldeAsync(Guid idMolde)
        {
            var respons = await _httpClient.GetAsync($"/api/Molde/BuscarMolde/{idMolde}");

            var dos = await respons.Content.ReadAsStringAsync();

            var response = await Helper.ParseApiResponse<Molde>(
              dos
            );

            return response;
        }

        public async Task<CustomApiResponse<Molde>> InsertarMoldeAsync(Molde molde)
        {
            var respons = await _httpClient.PostAsJsonAsync($"/api/Molde/InsertarMolde", molde);

            var dos = await respons.Content.ReadAsStringAsync();

            var response = await Helper.ParseApiResponse<Molde>(
              dos
            );

            return response;
        }

        public async Task<CustomApiResponse<Molde>> ActualizarMoldeAsync(Molde molde)
        {
            var respons = await _httpClient.PutAsJsonAsync($"/api/Molde/ActualizarMolde", molde);

            var dos = await respons.Content.ReadAsStringAsync();

            var response = await Helper.ParseApiResponse<Molde>(
              dos
            );

            return response;
        }
    }
}
