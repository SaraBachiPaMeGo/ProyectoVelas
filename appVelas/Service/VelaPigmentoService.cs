using appVelas.Models;
using appVelas.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace appVelas.Service
{
    public class VelaPigmentoService : IVelaPigmentoService
    {
        private readonly HttpClient _httpClient;

        public VelaPigmentoService(HttpClient httpClient)
        {
             
            _httpClient = httpClient;
        }

        public async Task<CustomApiResponse<List<VelaPigmento>>> GetPigmentosPorVelaAsync()
        {
            var response = new CustomApiResponse<List<VelaPigmento>>();

            try
            {
                var respons = await _httpClient.GetAsync("/api/VelaPigmento/GetPigmentosPorVela");

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<List<VelaPigmento>>(
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


        public async Task<CustomApiResponse<List<VelaPigmento>>> InsertarVelaPigmentoAsync(List<VelaPigmento> velaPigmento)
        {
            var response = new CustomApiResponse<List<VelaPigmento>>();

            try
            {
                var respons = await _httpClient.PostAsJsonAsync("/api/VelaPigmento/InsertarVelaPigmento", velaPigmento);

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<List<VelaPigmento>>(
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

        public async Task<CustomApiResponse<List<VelaPigmento>>> ActualizarVelaPigmentoAsync(List<VelaPigmento> velaPigmento)
        {
            var response = new CustomApiResponse<List<VelaPigmento>>();

            try
            {
                var respons = await _httpClient.PutAsJsonAsync("/api/VelaPigmento/ActualizarVelaPigmento", velaPigmento);

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<List<VelaPigmento>>(
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

        public async Task<CustomApiResponse<List<VelaPigmento>>> EliminarRelacionesPigmentosAsync(List<VelaPigmento> pig)
        {
            var response = new CustomApiResponse<List<VelaPigmento>>();

            try
            {
                //Queremos enviar una lista para que elimine esos registros
                var respons = await _httpClient.DeleteAsync($"/api/VelaPigmento/EliminarVelaPigmento"/*, pig*/);

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<List<VelaPigmento>>(
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

        public async Task<CustomApiResponse<VelaPigmento>> BuscarVelaPigmentoAsync(Guid idVelaPigmento)
        {
            var response = new CustomApiResponse<VelaPigmento>();

            try
            {
                var respons = await _httpClient.GetAsync($"/api/VelaPigmento/BuscarVelaPigmento/{idVelaPigmento}");

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<VelaPigmento>(
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
