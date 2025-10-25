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
            var response = await Helper.ParseApiResponse<List<VelaPigmento>>(
                await _httpClient.GetAsync("/api/GetPigmentosPorVela")
            );

            return response;
        }


        public async Task<CustomApiResponse<VelaPigmento>> InsertarVelaPigmentoAsync(VelaPigmento velaPigmento)
        {
            var response = await Helper.ParseApiResponse<VelaPigmento>(
                await _httpClient.PostAsJsonAsync("/api/InsertarVelaPigmento", velaPigmento)
            );

            return response;
        }


        public async Task<CustomApiResponse<VelaPigmento>> ActualizarVelaPigmentoAsync(VelaPigmento velaPigmento)
        {
            var response = await Helper.ParseApiResponse<VelaPigmento>(
             await _httpClient.PutAsJsonAsync("/api/ActualizarVelaPigmento", velaPigmento)
         );
            return response;
        }

        public async Task<CustomApiResponse<VelaPigmento>> EliminarRelacionesPigmentosAsync(Guid idVelaPigmento)
        {
            var response = await Helper.ParseApiResponse<VelaPigmento>(
              await _httpClient.GetAsync($"/api/EliminarVelaPigmento/{idVelaPigmento}")
          );

            return response;
        }

        public async Task<CustomApiResponse<VelaPigmento>> BuscarVelaPigmentoAsync(Guid idVelaPigmento)
        {
            var response = await Helper.ParseApiResponse<VelaPigmento>(
               await _httpClient.GetAsync($"/api/BuscarVelaPigmento/{idVelaPigmento}")
           );

            return response;
        }
    }
}
