using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using appVelas.Models;
using appVelas.Service.Interfaces;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;

namespace appVelas.Service
{
    public class MechaService : IMechaService
    {
        private readonly HttpClient _httpClient;

        public MechaService(HttpClient httpClient)
        {

            _httpClient = httpClient;
        }

        public async Task<CustomApiResponse<List<Mecha>>> GetMechasAsync()
        {
            var response = new CustomApiResponse<List<Mecha>>();

            try
            {
                var respons = await _httpClient.GetAsync("/api/Mecha/GetMechas");

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<List<Mecha>>(
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

        public async Task<CustomApiResponse<Mecha>> BuscarMechaAsync(Guid idMecha)
        {
            var response = new CustomApiResponse<Mecha>();

            try
            {
                var respons = await _httpClient.GetAsync($"/api/Mecha/BuscarMecha/{idMecha}");

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<Mecha>(
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

        public async Task<CustomApiResponse<Mecha>> InsertarMechaAsync(Mecha mecha)
        {
            var response = new CustomApiResponse<Mecha>();

            try
            {
                var respons = await _httpClient.PostAsJsonAsync($"/api/Mecha/InsertarMecha", mecha);

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<Mecha>(
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

        public async Task<CustomApiResponse<Mecha>> ActualizarMechaAsync(Guid id, Mecha mecha)
        {
            var response = new CustomApiResponse<Mecha>();

            try
            {
                var respons = await _httpClient.PutAsJsonAsync($"/api/Mecha/ActualizarMecha/{id}", mecha);

                var dos = await respons.Content.ReadAsStringAsync();

                 response = await Helper.ParseApiResponse<Mecha>(
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

        public async Task<CustomApiResponse<bool>> EliminarMechaAsync(Guid id)
        {
            var response = new CustomApiResponse<bool>();

            try
            {
                var respons = await _httpClient.DeleteAsync($"/api/Mecha/Eliminar/{id}");

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
