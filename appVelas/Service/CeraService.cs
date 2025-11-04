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

        public CeraService(HttpClient httpClient)
        {
             
            _httpClient = httpClient;
        }

        public async Task<CustomApiResponse<List<Cera>>> GetCerasAsync()
        {
            var response = new CustomApiResponse<List<Cera>>();
            try
            {
                var respons = await _httpClient.GetAsync("/api/Cera/GetCeras");

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<List<Cera>>(
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

        public async Task<CustomApiResponse<Cera>> BuscarCeraAsync(Guid idCera)
        {
            var response = new CustomApiResponse<Cera>();

            try
            {
                var respons = await _httpClient.GetAsync($"/api/Cera/BuscarCera/{idCera}");

                var dos = await respons.Content.ReadAsStringAsync();

                 response = await Helper.ParseApiResponse<Cera>(
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

        public async Task<CustomApiResponse<Cera>> InsertarCeraAsync(Cera cera)
        {
            var response = new CustomApiResponse<Cera>();

            try
            {
                var respons = await _httpClient.PostAsJsonAsync($"/api/Cera/InsertarCera", cera);

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<Cera>(
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

        public async Task<CustomApiResponse<Cera>> ActualizarCeraAsync(Guid id, Cera cera)
        {
            var response = new CustomApiResponse<Cera>();

            try
            {
                var respons = await _httpClient.PutAsJsonAsync($"/api/Cera/ActualizarCera/{id}", cera);

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<Cera>(
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

