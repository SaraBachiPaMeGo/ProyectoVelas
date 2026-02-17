using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using appVelas.Models;
using appVelas.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;

namespace appVelas.Service
{
    public class MoldeService : IMoldeService
    {
        private readonly HttpClient _httpClient;

        public MoldeService(HttpClient httpClient)
        {
             
            _httpClient = httpClient;
        }

        public async Task<CustomApiResponse<List<Molde>>> GetMoldesAsync()
        {
            var response = new CustomApiResponse<List<Molde>>();

            try
            {
                var respons = await _httpClient.GetAsync($"/api/Molde/GetMoldes");

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<List<Molde>>(
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

        public async Task<CustomApiResponse<Molde>> BuscarMoldeAsync(Guid idMolde)
        {
            var response = new CustomApiResponse<Molde>();

            try
            {
                var respons = await _httpClient.GetAsync($"/api/Molde/BuscarMolde/{idMolde}");

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<Molde>(
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

        public async Task<CustomApiResponse<Molde>> InsertarMoldeAsync(MultipartFormDataContent form)
        {
            var respons = await _httpClient.PostAsync($"/api/Molde/InsertarMolde", form);

            var dos = await respons.Content.ReadAsStringAsync(); 

            var response = await Helper.ParseApiResponse<Molde>(
              dos
            );

            return response;
        }

        public async Task<CustomApiResponse<Molde>> ActualizarMoldeAsync(Guid id, MultipartFormDataContent form)
        {
            var response = new CustomApiResponse<Molde>();

            try
            {
                var respons = await _httpClient.PutAsync($"/api/Molde/ActualizarMolde/{id}", form);

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<Molde>(
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

        public async Task<CustomApiResponse<bool>> EliminarMoldeAsync(Guid id)
        {
            var response = new CustomApiResponse<bool>();

            try
            {
                var respons = await _httpClient.DeleteAsync($"/api/Molde/Eliminar/{id}");

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
