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

        public MechaService()
        {
        }
        public async Task<CustomApiResponse<List<Mecha>>> GetMechasAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    client.BaseAddress = new Uri("http://localhost:5000");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                           new MediaTypeWithQualityHeaderValue("application/json"));

                    var respons = await client.GetAsync($"/api/Mecha/GetMechas");

                    var dos = await respons.Content.ReadAsStringAsync();

                    var response = await Helper.ParseApiResponse<List<Mecha>>(
                        dos
                    );

                    return response;

                }
            }
            catch (Exception e)
            {

                string url2 = _httpClient.BaseAddress.ToString();

                throw;
            }
                                                
        }

        public async Task<CustomApiResponse<Mecha>> BuscarMechaAsync(Guid idMecha)
        {
            var respons = await _httpClient.GetAsync($"/api/Mecha/BuscarMecha/{idMecha}");

            var dos = await respons.Content.ReadAsStringAsync();

            var response = await Helper.ParseApiResponse<Mecha>(
                dos
            );

            return response;
        }

        public async Task<CustomApiResponse<Mecha>> InsertarMechaAsync(Mecha mecha)
        {
            var respons = await _httpClient.PostAsJsonAsync($"/api/Mecha/InsertarMecha", mecha);

            var dos = await respons.Content.ReadAsStringAsync();

            var response = await Helper.ParseApiResponse<Mecha>(
              dos
            );

            return response;
        }

        public async Task<CustomApiResponse<Mecha>> ActualizarMechaAsync(Mecha mecha)
        {
            var respons = await _httpClient.PutAsJsonAsync($"/api/Mecha/ActualizarMecha", mecha);

            var dos = await respons.Content.ReadAsStringAsync();

            var response = await Helper.ParseApiResponse<Mecha>(
              dos
            );

            return response;
        }
    }
}
