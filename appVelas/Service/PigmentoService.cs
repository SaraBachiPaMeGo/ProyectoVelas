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
    public class PigmentoService : IPigmentoService
    {
        private readonly HttpClient _httpClient;

        public PigmentoService(HttpClient httpClient)
        {
             
            _httpClient = httpClient;
        }

        public async Task<CustomApiResponse<List<Pigmento>>> GetPigmentosAsync()
        {
            var response = new CustomApiResponse<List<Pigmento>>();

            try
            {
                var respons = await _httpClient.GetAsync("/api/Pigmento/GetPigmentos");

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<List<Pigmento>>(
                    dos
                );

                return response;           }
            catch (Exception ex)
            {
                response.Error = new ErrorViewModel { Mensaje = ex.Message };

                return response;

            }            
        }

        public async Task<CustomApiResponse<Pigmento>> BuscarPigmentoAsync(Guid idPigmento)
        {
            var response = new CustomApiResponse<Pigmento>();

            try
            {
                var respons = await _httpClient.GetAsync($"/api/Pigmento/BuscarPigmento/{idPigmento}");

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<Pigmento>(
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

        public async Task<CustomApiResponse<Pigmento>> InsertarPigmentoAsync(Pigmento pigmento)
        {
            var response = new CustomApiResponse<Pigmento>();

            try
            {
                var respons = await _httpClient.PostAsJsonAsync("/api/Pigmento/InsertarPigmento", pigmento);

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<Pigmento>(
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

        public async Task<CustomApiResponse<Pigmento>> ActualizarPigmentoAsync(Guid id, Pigmento pigmento)
        {
            var response = new CustomApiResponse<Pigmento>();

            try
            {
                var respons = await _httpClient.PutAsJsonAsync($"/api/Pigmento/ActualizarPigmento/{id}", pigmento);

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<Pigmento>(
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
