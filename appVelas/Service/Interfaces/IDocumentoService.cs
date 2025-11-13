using appVelas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Service.Interfaces
{
    public interface IDocumentoService
    {
        Task<CustomApiResponse<List<Documento>>> GetDocumentosAsync();
        Task<CustomApiResponse<Documento>> BuscarDocumentoAsync(Guid idDocumento);
        Task<CustomApiResponse<Documento>> InsertarDocumentoAsync(Documento Documento);
        Task<CustomApiResponse<Documento>> ActualizarDocumentoAsync(Guid id, Documento Documento);
    }
}
}
