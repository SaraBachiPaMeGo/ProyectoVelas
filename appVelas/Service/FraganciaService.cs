using appVelas.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using appVelas.Service.Interfaces;
using System.Net.Http.Json;

namespace appVelas.Service
{
    public class FraganciaService : IFraganciaService
    {
        private readonly HttpClient _httpClient;

        public FraganciaService(HttpClient httpClient)
        {
             
            _httpClient = httpClient;
        }

        public async Task<CustomApiResponse<List<Fragancia>>> GetFraganciasAsync()
        {
            var response = new CustomApiResponse<List<Fragancia>>();

            try
            {
                var respons = await _httpClient.GetAsync($"/api/Fragancia/GetFragancias");

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<List<Fragancia>>(
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

        public async Task<CustomApiResponse<Fragancia>> BuscarFraganciaAsync(Guid idFragancia)
        {
            var response = new CustomApiResponse<Fragancia>();
            try
            {
                var respons = await _httpClient.GetAsync($"/api/Fragancia/BuscarFragancia/{idFragancia}");

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<Fragancia>(
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

        public async Task<CustomApiResponse<Fragancia>> InsertarFraganciaAsync(Fragancia fragancia)
        {
            var response = new CustomApiResponse<Fragancia>();

            try
            {
                var respons = await _httpClient.PostAsJsonAsync($"/api/Fragancia/InsertarFragancia", fragancia);

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<Fragancia>(
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

        public async Task<CustomApiResponse<Fragancia>> ActualizarFraganciaAsync(Guid id, Fragancia fragancia)
        {
            var response = new CustomApiResponse<Fragancia>();

            try
            {
                var respons = await _httpClient.PutAsJsonAsync($"/api/Fragancia/ActualizarFragancia/{id}", fragancia);

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<Fragancia>(
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
