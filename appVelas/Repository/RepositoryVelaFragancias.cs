using appVelas.Models;
using appVelas.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Repository
{
    public class RepositoryVelaFragancias
    {
        private readonly IVelaFraganciaService _velaFraganciaService;


        public RepositoryVelaFragancias(IVelaFraganciaService fraganciaService)
        {
            _velaFraganciaService = fraganciaService;
        }

        // ------------------------------------- Fragancia ---------------------------------------------
        public async Task<CustomApiResponse<List<VelaFragancia>>> GetVelaFraganciasAsync()
        {
            return await _velaFraganciaService.GetFraganciasPorVelaAsync();
        }

        public async Task<CustomApiResponse<VelaFragancia>> InsertarVelaFraganciaAsync(VelaFragancia velaFragancia)
        {
            return await _velaFraganciaService.InsertarVelaFraganciaAsync(velaFragancia);

        }

        public async Task<CustomApiResponse<VelaFragancia>> ActualizarVelaFraganciaAsync(VelaFragancia velaFragancia)
        {
            return await _velaFraganciaService.ActualizarVelaFraganciaAsync(velaFragancia);

        }

        public async Task<CustomApiResponse<VelaFragancia>> BuscarVelaFraganciaAsync(Guid idVelaFragancia)
        {
            return await _velaFraganciaService.BuscarVelaFraganciaAsync(idVelaFragancia);

        }

        public async Task<CustomApiResponse<VelaFragancia>> EliminarRelacionesFraganciaAsync(Guid idVelaFragancia)
        {
            return await _velaFraganciaService.EliminarRelacionesFraganciaAsync(idVelaFragancia);

        }
    }
}
