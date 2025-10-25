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
    public class PackService : IPackService
    {
        private readonly HttpClient _httpClient;

        public PackService(HttpClient httpClient)
        {
             
            _httpClient = httpClient;
        }

        public async Task<CustomApiResponse<List<Pack>>> GetPacksAsync()
        {
            var response = await Helper.ParseApiResponse<List<Pack>>(
                await _httpClient.GetAsync("/api/GetPacks")
            );

            return response;
        }

        public async Task<CustomApiResponse<Pack>> BuscarPackAsync(Guid idPack)
        {
            var response = await Helper.ParseApiResponse<Pack>(
                await _httpClient.GetAsync($"/api/BuscarPack/{idPack}")
            );

            return response;
        }

        public async Task<CustomApiResponse<Pack>> InsertarPackAsync(Pack pack)
        {
            var response = await Helper.ParseApiResponse<Pack>(
                await _httpClient.PostAsJsonAsync("/api/InsertarPack", pack)
            );

            return response;
        }

        public async Task<CustomApiResponse<Pack>> ActualizarPackAsync(Pack pack)
        {
            var response = await Helper.ParseApiResponse<Pack>(
                await _httpClient.PutAsJsonAsync("/api/ActualizarPack", pack)
            );

            return response;
        }
    }
}
