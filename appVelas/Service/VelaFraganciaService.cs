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
        private readonly string _baseUrl;

        public VelaFraganciaService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:BaseUrl"];

            _httpClient.BaseAddress = new Uri(_baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<CustomApiResponse<VelaFragancia>> ActualizarVelaFraganciaAsync(VelaFragancia velaFragancia)
        {
            var response = await Helper.ParseApiResponse<VelaFragancia>(
               await _httpClient.PutAsJsonAsync("/api/ActualizarVelaFragancia", velaFragancia)
           );
            return response;
        }

        public async Task<CustomApiResponse<VelaFragancia>> BuscarVelaFraganciaAsync(Guid idVelaFragancia)
        {
            var response = await Helper.ParseApiResponse<VelaFragancia>(
               await _httpClient.GetAsync($"/api/BuscarVelaFragancia/{idVelaFragancia}")
           );

            return response;
        }

        public async Task<CustomApiResponse<VelaFragancia>> EliminarRelacionesFraganciaAsync(Guid idvelaFragancia)
        {
            var response = await Helper.ParseApiResponse<VelaFragancia>(
              await _httpClient.GetAsync($"/api/EliminarVelaFragancia/{idvelaFragancia}")
          );

            return response;
        }

        public async Task<CustomApiResponse<List<VelaFragancia>>> GetFraganciasPorVelaAsync()
        {
            var response = await Helper.ParseApiResponse<List<VelaFragancia>>(
                await _httpClient.GetAsync("/api/GetFraganciasPorVela")
            );

            return response;
        }

        public async Task<CustomApiResponse<VelaFragancia>> InsertarVelaFraganciaAsync(VelaFragancia velaFragancia)
        {
            var response = await Helper.ParseApiResponse<VelaFragancia>(
                await _httpClient.PostAsJsonAsync("/api/InsertarVelaFragancia", velaFragancia)
            );

            return response;
        }

    }
}
