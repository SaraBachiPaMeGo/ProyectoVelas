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
    public class DocumentoService : IDocumentoService
    {
        private readonly HttpClient _httpClient;

        public DocumentoService(HttpClient httpClient)
        {

            _httpClient = httpClient;
        }
    
        public async Task<CustomApiResponse<Documento>> ActualizarDocumentoAsync(Guid id, Documento Documento)
        {
            var response = new CustomApiResponse<Documento>();

            try
            {
                var respons = await _httpClient.PutAsJsonAsync($"/api/Documento/ActualizarDocumento/{id}", Documento);

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<Documento>(
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

        public async Task<CustomApiResponse<Documento>> BuscarDocumentoAsync(Guid idDocumento)
        {
            var response = new CustomApiResponse<Documento>();

            try
            {
                var respons = await _httpClient.GetAsync($"/api/Documento/BuscarDocumento/{idDocumento}");

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<Documento>(
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

        public async Task<CustomApiResponse<List<Documento>>> GetDocumentosAsync()
        {
            var response = new CustomApiResponse<List<Documento>>();
            try
            {
                var respons = await _httpClient.GetAsync("/api/Documento/GetDocumentos");

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<List<Documento>>(
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

        public async Task<CustomApiResponse<Documento>> InsertarDocumentoAsync(Documento Documento)
        {
            var response = new CustomApiResponse<Documento>();

            try
            {
                var respons = await _httpClient.PostAsJsonAsync($"/api/Documento/InsertarDocumento", Documento);

                var dos = await respons.Content.ReadAsStringAsync();

                response = await Helper.ParseApiResponse<Documento>(
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
    }
}
