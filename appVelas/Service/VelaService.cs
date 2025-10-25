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

        //(IHttpClientFactory httpClientFactory)
        //{
        //    _httpClient = httpClientFactory.CreateClient("ApiClient");
        //}

        public async Task<CustomApiResponse<List<Vela>>> GetVelasAsync()
        {
            var response = await Helper.ParseApiResponse<List<Vela>>(await _httpClient.GetAsync("/Vela/GetVelas"));
            return response;
        }

        public async Task<CustomApiResponse<Vela>> BuscarVelaAsync(Guid idVela)
        {
            var response = await Helper.ParseApiResponse<Vela>(
                await _httpClient.GetAsync($"/Vela/BuscarVela/{idVela}")
            );
            return response;
        }

        public async Task<CustomApiResponse<Vela>> InsertarVelaAsync(Vela vela)
        {
            var response = await Helper.ParseApiResponse<Vela>(
                await _httpClient.PostAsJsonAsync("/Vela/InsertarVela", vela)
            );
            return response;
        }

        public async Task<CustomApiResponse<Vela>> ActualizarVelaAsync(Vela vela)
        {
            var response = await Helper.ParseApiResponse<Vela>(
                await _httpClient.PutAsJsonAsync("/Vela/ActualizarVela", vela)
            );
            return response;
        }
    }
}
