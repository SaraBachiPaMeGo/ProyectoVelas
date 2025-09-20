using appVelas.Models;
using appVelas.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Repository
{
    public class RepositoryVelaPigmentos
    {
        private readonly IVelaPigmentoService _velaPigmentoService;


        public RepositoryVelaPigmentos(IVelaPigmentoService pigmentoService)
        {
            _velaPigmentoService = pigmentoService;
        }

        // ------------------------------------- Pigmento ---------------------------------------------
        public async Task<List<VelaPigmento>> GetVelaPigmentosAsync()
        {
            return await _velaPigmentoService.GetPigmentosPorVelaAsync();
        }

        public async Task<bool> InsertarVelaPigmentoAsync(VelaPigmento velaPigmento)
        {
            return await _velaPigmentoService.InsertarVelaPigmentoAsync(velaPigmento);

        }
    }
}
