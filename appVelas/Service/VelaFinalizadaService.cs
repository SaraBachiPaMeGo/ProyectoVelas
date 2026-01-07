using appVelas.Models;
using appVelas.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace appVelas.Service
{
    public class VelaFinalizadaService : IVelaFinService
    {
        private readonly HttpClient _httpClient;

        public VelaFinalizadaService(HttpClient httpClient)
        {

            _httpClient = httpClient;
        }

        public async Task<CustomApiResponse<VelaFinalizada>> ActualizarVelaFinalizadaAsync(Guid idVelaFinalizada, VelaFinalizada velaFinalizada)
        {
            var response = new CustomApiResponse<VelaFinalizada>();

            try
            {
                var respons = await _httpClient.PutAsJsonAsync($"/api/VelaFinalizada/ActualizarVelaFinalizada/{idVelaFinalizada}", velaFinalizada);

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<VelaFinalizada>(
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

        public async Task<CustomApiResponse<VelaFinalizada>> BuscarVelaFinalizadaAsync(Guid idVelaFinalizada)
        {
            var response = new CustomApiResponse<VelaFinalizada>();

            try
            {
                var respons = await _httpClient.GetAsync($"/api/VelaFinalizada/BuscarVelaFinalizada/{idVelaFinalizada}");

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<VelaFinalizada>(
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

        public async Task<CustomApiResponse<List<VelaFinalizada>>> GetVelaFinalizadasAsync()
        {
            var response = new CustomApiResponse<List<VelaFinalizada>>();

            try
            {
                var respons = await _httpClient.GetAsync("/api/VelaFinalizada/GetVelaFinalizadas");

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<List<VelaFinalizada>>(
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

        public async Task<CustomApiResponse<VelaFinalizada>> InsertarVelaFinalizadaAsync(VelaFinalizada VelaFinalizada)
        {
            var response = new CustomApiResponse<VelaFinalizada>();

            try
            {
                var respons = await _httpClient.PostAsJsonAsync($"/api/VelaFinalizada/InsertarVelaFinalizada", VelaFinalizada);

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<VelaFinalizada>(
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
