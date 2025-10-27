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
            var respons = await _httpClient.GetAsync("/api/VelaPigmento/GetPigmentosPorVela");

            var dos = await respons.Content.ReadAsStringAsync();

            var response = await Helper.ParseApiResponse<List<VelaPigmento>>(
                dos
            );

            return response;
        }


        public async Task<CustomApiResponse<VelaPigmento>> InsertarVelaPigmentoAsync(VelaPigmento velaPigmento)
        {
            var respons = await _httpClient.PostAsJsonAsync("/api/VelaPigmento/InsertarVelaPigmento", velaPigmento);

            var dos = await respons.Content.ReadAsStringAsync();

            var response = await Helper.ParseApiResponse<VelaPigmento>(
                dos
            );

            return response;
        }


        public async Task<CustomApiResponse<VelaPigmento>> ActualizarVelaPigmentoAsync(VelaPigmento velaPigmento)
        {
            var respons = await _httpClient.PutAsJsonAsync("/api/VelaPigmento/ActualizarVelaPigmento", velaPigmento);

            var dos = await respons.Content.ReadAsStringAsync();

            var response = await Helper.ParseApiResponse<VelaPigmento>(
                dos
            );

            return response;
        }

        public async Task<CustomApiResponse<VelaPigmento>> EliminarRelacionesPigmentosAsync(Guid idVelaPigmento)
        {
            var respons = await _httpClient.GetAsync($"/api/VelaPigmento/EliminarVelaPigmento/{idVelaPigmento}");

            var dos = await respons.Content.ReadAsStringAsync();

            var response = await Helper.ParseApiResponse<VelaPigmento>(
                dos
            );
                        
            return response;
        }

        public async Task<CustomApiResponse<VelaPigmento>> BuscarVelaPigmentoAsync(Guid idVelaPigmento)
        {
            var respons = await _httpClient.GetAsync($"/api/VelaPigmento/BuscarVelaPigmento/{idVelaPigmento}");

            var dos = await respons.Content.ReadAsStringAsync();

            var response = await Helper.ParseApiResponse<VelaPigmento>(
              dos
            );

            return response;
        }
    }
}
