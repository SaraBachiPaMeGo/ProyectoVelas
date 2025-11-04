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
    public class EndurecedorService : IEndurecedorService
    {
        private readonly HttpClient _httpClient;

        public EndurecedorService(HttpClient httpClient)
        {
             
            _httpClient = httpClient;
        }

        public async Task<CustomApiResponse<List<Endurecedor>>> GetEndurecedorsAsync()
        {
            var response = new CustomApiResponse<List<Endurecedor>>();

            try
            {
                var respons = await _httpClient.GetAsync($"/api/Endurecedor/GetEndurecedores");

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<List<Endurecedor>>(
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

        public async Task<CustomApiResponse<Endurecedor>> BuscarEndurecedorAsync(Guid idEndurecedor)
        {
            var response = new CustomApiResponse<Endurecedor>();

            try
            {
                var respons = await _httpClient.GetAsync($"/api/Endurecedor/BuscarEndurecedor/{idEndurecedor}");

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<Endurecedor>(
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

        public async Task<CustomApiResponse<Endurecedor>> InsertarEndurecedorAsync(Endurecedor endurecedor)
        {
            var response = new CustomApiResponse<Endurecedor>();

            try
            {
                var respons = await _httpClient.PostAsJsonAsync($"/api/Endurecedor/InsertarEndurecedor", endurecedor);

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<Endurecedor>(
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

        public async Task<CustomApiResponse<Endurecedor>> ActualizarEndurecedorAsync(Guid id, Endurecedor endurecedor)
        {
            var response = new CustomApiResponse<Endurecedor>();

            try
            {
                var respons = await _httpClient.PutAsJsonAsync($"/api/Endurecedor/ActualizarEndurecedor/{id}", endurecedor);

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<Endurecedor>(
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
