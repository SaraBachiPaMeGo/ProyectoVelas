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
        public async Task<List<VelaFragancia>> GetVelaFraganciasAsync()
        {
            return await _velaFraganciaService.GetFraganciasPorVelaAsync();
        }

        public async Task<bool> InsertarVelaFraganciaAsync(VelaFragancia velaFragancia)
        {
            return await _velaFraganciaService.InsertarVelaFraganciaAsync(velaFragancia);

        }
    }
}
