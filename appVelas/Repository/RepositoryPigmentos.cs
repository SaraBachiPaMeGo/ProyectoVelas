using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using appVelas.Models;
using appVelas.Service.Interfaces;

namespace appVelas.Repository
{
    public class RepositoryPigmentos
    {
        private readonly IPigmentoService _pigmentoService;


        public RepositoryPigmentos(IPigmentoService pigmentoService)
        {
            _pigmentoService = pigmentoService;
        }

        // ------------------------------------- Pigmento ---------------------------------------------
        public async Task<List<Pigmento>> GetPigmentosAsync()
        {
            return await _pigmentoService.GetPigmentosAsync();
        }

        public async Task<Pigmento> BuscarPigmentoAsync(Guid id)
        {
            return await _pigmentoService.BuscarPigmentoAsync(id);
        }

        public async Task<bool> InsertarPigmentoAsync(Pigmento pigmento)
        {
            return await _pigmentoService.InsertarPigmentoAsync(pigmento);
        }

        public async Task<bool> ActualizarPigmentoAsync(Pigmento pigmento)
        {
            return await _pigmentoService.ActualizarPigmentoAsync(pigmento);
        }
    }
}
