using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using appVelas.Models;
using appVelas.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using appVelas.Models.DTO;

namespace appVelas.Service
{
    public class VelaService : IVelaService
    {
        private readonly HttpClient _httpClient;

        public VelaService(HttpClient httpClient)
        {
             
            _httpClient = httpClient;
        }

        public async Task<CustomApiResponse<List<VelaDTO>>> GetVelasAsync()        
        {
            var response = new CustomApiResponse<List<VelaDTO>>();
            try
            {
                var respons = await _httpClient.GetAsync("/api/Vela/GetVelas");

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<List<VelaDTO>>(
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

        public async Task<CustomApiResponse<VelaDTO>> BuscarVelaAsync(Guid idVela)
        {
            var response = new CustomApiResponse<VelaDTO>();

            try
            {
                var respons = await _httpClient.GetAsync($"/api/Vela/BuscarVela/{idVela}");

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<VelaDTO>(
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

        public async Task<CustomApiResponse<VelaDTO>> InsertarVelaAsync(MultipartFormDataContent vela)
        {
            var response = new CustomApiResponse<VelaDTO>();

            try
            {
                var respons = await _httpClient.PostAsync($"/api/Vela/InsertarVela", vela);

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<VelaDTO>(
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

        public async Task<CustomApiResponse<VelaDTO>> ActualizarVelaAsync(Guid id, MultipartFormDataContent vela)
        {
            var response = new CustomApiResponse<VelaDTO>();

            try
            {
                var respons = await _httpClient.PutAsync($"/api/Vela/ActualizarVela/{id}", vela);

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<VelaDTO>(
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

        public async Task<CustomApiResponse<bool>> EliminarVelaAsync(Guid id)
        {
            var response = new CustomApiResponse<bool>();

            try
            {
                var respons = await _httpClient.DeleteAsync($"/api/Vela/Eliminar/{id}");

                response.Data = respons.IsSuccessStatusCode;

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
