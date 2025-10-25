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
            Helper.ConexionApi(httpClient);
            _httpClient = httpClient;
        }

        public async Task<CustomApiResponse<List<Mecha>>> GetMechasAsync()
        {
            //string url = _httpClient.BaseAddress.ToString();
            try
            {
                var mechas = await _httpClient.GetAsync("/Mecha/GetMechas");
                var response = await Helper.ParseApiResponse<List<Mecha>>(
                mechas
            );


                Console.WriteLine($"StatusCode: {mechas.StatusCode}");

                var content = await mechas.Content.ReadAsStringAsync();
                Console.WriteLine($"Contenido: {content}");


                return response;
            }
            catch (Exception e)
            {
                
                string url2 = _httpClient.BaseAddress.ToString();

                throw ; 
            }
                        
        }

        public async Task<CustomApiResponse<Mecha>> BuscarMechaAsync(Guid idMecha)
        {
            var response = await Helper.ParseApiResponse<Mecha>(
                await _httpClient.GetAsync($"Mecha/BuscarMecha/{idMecha}")
            );

            return response;
        }

        public async Task<CustomApiResponse<Mecha>> InsertarMechaAsync(Mecha mecha)
        {
            var response = await Helper.ParseApiResponse<Mecha>(
                await _httpClient.PostAsJsonAsync("Mecha/InsertarMecha", mecha)
            );

            return response;
        }

        public async Task<CustomApiResponse<Mecha>> ActualizarMechaAsync(Mecha mecha)
        {
            var response = await Helper.ParseApiResponse<Mecha>(
                await _httpClient.PutAsJsonAsync("Mecha/ActualizarMecha", mecha)
            );

            return response;
        }
    }
}
