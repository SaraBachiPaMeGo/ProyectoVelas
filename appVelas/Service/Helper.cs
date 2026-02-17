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
using Microsoft.AspNetCore.Http;

namespace appVelas.Service
{
    public class Helper
    {

        public static HttpClient ConexionApi(HttpClient _httpClient)
        {
            string _baseUrl = "http://localhost:5000/";//configuration["ApiSettings: BaseUrl"];

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

        public static MultipartFormDataContent CreateMultipartFormData<T>(T obj, IFormFile file = null)
        {
            var form = new MultipartFormDataContent();

            // 🔹 Agregar propiedades del objeto dinámicamente
            var properties = typeof(T).GetProperties();

            foreach (var prop in properties)
            {
                var value = prop.GetValue(obj);

                if (value != null)
                {
                    form.Add(
                        new StringContent(value.ToString()),
                        prop.Name
                    );
                }
            }

            // 🔹 Agregar archivo si existe
            if (file != null && file.Length > 0)
            {
                var streamContent = new StreamContent(file.OpenReadStream());
                streamContent.Headers.ContentType =
                    new MediaTypeHeaderValue(file.ContentType);

                form.Add(streamContent, "file", file.FileName);
            }

            return form;
        }


    }
}
