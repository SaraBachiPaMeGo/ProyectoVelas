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
            var response = new CustomApiResponse<List<Pack>>();

            try
            {
                var respons = await _httpClient.GetAsync($"/api/Pack/GetPacks");

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<List<Pack>>(
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

        public async Task<CustomApiResponse<Pack>> BuscarPackAsync(Guid idPack)
        {
            var response = new CustomApiResponse<Pack>();

            try
            {
                var respons = await _httpClient.GetAsync($"/api/Pack/BuscarPack/{idPack}");

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<Pack>(
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

        public async Task<CustomApiResponse<Pack>> InsertarPackAsync(Pack pack)
        {
            var response = new CustomApiResponse<Pack>();

            try
            {
                var respons = await _httpClient.PostAsJsonAsync($"/api/Pack/InsertarPack", pack);

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<Pack>(
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

        public async Task<CustomApiResponse<Pack>> ActualizarPackAsync(Guid id, Pack pack)
        {
            var response = new CustomApiResponse<Pack>();

            try
            {
                var respons = await _httpClient.PutAsJsonAsync($"/api/Pack/ActualizarPack/{id}", pack);

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<Pack>(
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
