using appVelas.Models;
using appVelas.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace appVelas.Service
{
    public class Helper
    {
        public static async Task<CustomApiResponse<T>> ParseApiResponse<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<CustomApiResponse<T>>();
                return data ?? new CustomApiResponse<T>
                {
                    Error = new ErrorViewModel
                    {
                        RequestId = "EmptyResponse",
                        Mensaje = "La API devolvió una respuesta vacía, no ha podido 'Parsearlo'"
                    }
                };
            }

            var error = await response.Content.ReadFromJsonAsync<ErrorViewModel>();
            return new CustomApiResponse<T>
            {
                Error = error ?? new ErrorViewModel
                {
                    RequestId = response.StatusCode.ToString(),
                    Mensaje = "Error desconocido"
                }
            };
        }

    }
}
