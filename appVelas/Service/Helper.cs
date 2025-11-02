using appVelas.Models;
using appVelas.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text.Json;

namespace appVelas.Service
{
    public class Helper
    {

        public static HttpClient ConexionApi(HttpClient _httpClient)
        {
            string _baseUrl = "http://localhost:44346/api";//configuration["ApiSettings: BaseUrl"];

            _httpClient.BaseAddress = new Uri(_baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return _httpClient;
        }

        public static async Task<CustomApiResponse<T>> ParseApiResponse<T>(string response)
        {
            CustomApiResponse<T> respon = new CustomApiResponse<T>();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var data = JsonSerializer.Deserialize<T>(response, options);

            respon.Data = data;


            return respon;
            
        }

    }
}
