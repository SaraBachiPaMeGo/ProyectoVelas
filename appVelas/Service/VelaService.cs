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
    public class VelaService : IVelaService
    {
        private readonly HttpClient _httpClient;

        public VelaService(HttpClient httpClient)
        {
             
            _httpClient = httpClient;
        }

        public async Task<CustomApiResponse<List<Vela>>> GetVelasAsync()        
        {
            var response = new CustomApiResponse<List<Vela>>();
            try
            {
                var respons = await _httpClient.GetAsync("/api/Vela/GetVelas");

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<List<Vela>>(
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

        public async Task<CustomApiResponse<Vela>> BuscarVelaAsync(Guid idVela)
        {
            var response = new CustomApiResponse<Vela>();

            try
            {
                var respons = await _httpClient.GetAsync($"/api/Vela/BuscarVela/{idVela}");

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<Vela>(
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

        public async Task<CustomApiResponse<Vela>> InsertarVelaAsync(Vela vela)
        {
            var response = new CustomApiResponse<Vela>();

            try
            {
                var respons = await _httpClient.PostAsJsonAsync($"/api/Vela/InsertarVela", vela);

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<Vela>(
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

        public async Task<CustomApiResponse<Vela>> ActualizarVelaAsync(Guid idVela, Vela vela)
        {
            var response = new CustomApiResponse<Vela>();

            try
            {
                var respons = await _httpClient.PutAsJsonAsync($"/api/Vela/ActualizarVela/{idVela}", vela);

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<Vela>(
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
