using appVelas.Models;
using appVelas.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http.Json;


namespace appVelas.Service
{
    public class VelaFraganciaService : IVelaFraganciaService
    {
        private readonly HttpClient _httpClient;

        public VelaFraganciaService(HttpClient httpClient)
        {
             
            _httpClient = httpClient;
        }

        public async Task<CustomApiResponse<VelaFragancia>> ActualizarVelaFraganciaAsync(VelaFragancia velaFragancia)
        {
            var response = new CustomApiResponse<VelaFragancia>();

            try
            {
                var respons = await _httpClient.PutAsJsonAsync("/api/VelaFragancia/ActualizarVelaFragancia", velaFragancia);

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<VelaFragancia>(
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

        public async Task<CustomApiResponse<VelaFragancia>> BuscarVelaFraganciaAsync(Guid idVelaFragancia)
        {
            var response = new CustomApiResponse<VelaFragancia>();

            try
            {
                var respons = await _httpClient.GetAsync($"/api/VelaFragancia/BuscarVelaFragancia/{idVelaFragancia}");

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<VelaFragancia>(
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

        public async Task<CustomApiResponse<VelaFragancia>> EliminarRelacionesFraganciaAsync(Guid idvelaFragancia)
        {
            var response = new CustomApiResponse<VelaFragancia>();

            try
            {
                var respons = await _httpClient.GetAsync($"/api/EliminarVelaFragancia/{idvelaFragancia}");

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<VelaFragancia>(
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

        public async Task<CustomApiResponse<List<VelaFragancia>>> GetFraganciasPorVelaAsync()
        {
            var response = new CustomApiResponse<List<VelaFragancia>>();

            try
            {
                var respons = await _httpClient.GetAsync("/api/VelaFragancia/GetFraganciasPorVela");

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<List<VelaFragancia>>(
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

        public async Task<CustomApiResponse<VelaFragancia>> InsertarVelaFraganciaAsync(VelaFragancia velaFragancia)
        {
            var response = new CustomApiResponse<VelaFragancia>();

            try
            {
                var respons = await _httpClient.PostAsJsonAsync("/api/VelaFragancia/InsertarVelaFragancia", velaFragancia);

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<VelaFragancia>(
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
