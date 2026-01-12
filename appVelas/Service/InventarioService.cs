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
    public class InventarioService : IInventarioService
    {
        private readonly HttpClient _httpClient;

        public InventarioService(HttpClient httpClient)
        {

            _httpClient = httpClient;
        }

        public async Task<CustomApiResponse<List<Inventario>>> GetInventariosAsync()
        {
            var response = new CustomApiResponse<List<Inventario>>();
            try
            {
                var respons = await _httpClient.GetAsync("/api/Inventario/GetInventarios");

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<List<Inventario>>(
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

        public async Task<CustomApiResponse<Inventario>> BuscarInventarioAsync(Guid idInventario)
        {
            var response = new CustomApiResponse<Inventario>();

            try
            {
                var respons = await _httpClient.GetAsync($"/api/Inventario/BuscarInventario/{idInventario}");

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<Inventario>(
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

        public async Task<CustomApiResponse<Inventario>> InsertarInventarioAsync(Inventario Inventario)
        {
            var response = new CustomApiResponse<Inventario>();

            try
            {
                var respons = await _httpClient.PostAsJsonAsync($"/api/Inventario/InsertarInventario", Inventario);

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<Inventario>(
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

        public async Task<CustomApiResponse<Inventario>> ActualizarInventarioAsync(Guid id, Inventario Inventario)
        {
            var response = new CustomApiResponse<Inventario>();

            try
            {
                var respons = await _httpClient.PutAsJsonAsync($"/api/Inventario/ActualizarInventario/{id}", Inventario);

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<Inventario>(
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

        public async Task<CustomApiResponse<bool>> EliminarInventarioAsync(Guid id)
        {
            var response = new CustomApiResponse<bool>();

            try
            {
                var respons = await _httpClient.DeleteAsync($"/api/Inventario/Eliminar/{id}");

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
