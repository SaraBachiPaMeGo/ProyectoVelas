using appVelas.Models;
using appVelas.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Repository
{
    public class RepositoryDocumentos
    {
        private readonly IDocumentoService _DocumentoService;


        public RepositoryDocumentos(IDocumentoService DocumentoService)
        {
            _DocumentoService = DocumentoService;
        }

        // ------------------------------------- Documento ---------------------------------------------
        public async Task<CustomApiResponse<List<Documento>>> GetDocumentosAsync()
        {
            return await _DocumentoService.GetDocumentosAsync();
        }

        public async Task<CustomApiResponse<Documento>> BuscarDocumentoAsync(Guid id)
        {
            return await _DocumentoService.BuscarDocumentoAsync(id);
        }

        public async Task<CustomApiResponse<Documento>> InsertarDocumentoAsync(Documento Documento)
        {
            return await _DocumentoService.InsertarDocumentoAsync(Documento);
        }

        public async Task<CustomApiResponse<Documento>> ActualizarDocumentoAsync(Guid id, Documento Documento)
        {
            return await _DocumentoService.ActualizarDocumentoAsync(id, Documento);
        }

        public async Task<CustomApiResponse<bool>> EliminarAsync(Guid id)
        {
            return await _DocumentoService.EliminarDocumentoAsync(id);
        }
    }
}
