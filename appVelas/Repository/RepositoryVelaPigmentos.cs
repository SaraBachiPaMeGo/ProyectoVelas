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
        public async Task<CustomApiResponse<List<VelaPigmento>>> GetVelaPigmentosAsync()
        {
            return await _velaPigmentoService.GetPigmentosPorVelaAsync();
        }

        public async Task<CustomApiResponse<VelaPigmento>> InsertarVelaPigmentoAsync(VelaPigmento velaPigmento)
        {
            return await _velaPigmentoService.InsertarVelaPigmentoAsync(velaPigmento);

        }

        public async Task<CustomApiResponse<VelaPigmento>> ActualizarVelaPigmentoAsync(VelaPigmento velaPigmento)
        {
            return await _velaPigmentoService.ActualizarVelaPigmentoAsync(velaPigmento);

        }

        public async Task<CustomApiResponse<VelaPigmento>> BuscarVelaPigmentoAsync(Guid idVelaPigmento)
        {
            return await _velaPigmentoService.BuscarVelaPigmentoAsync(idVelaPigmento);

        }

        public async Task<CustomApiResponse<VelaPigmento>> EliminarRelacionesPigmentosAsync(Guid idVelaPigmento)
        {
            return await _velaPigmentoService.EliminarRelacionesPigmentosAsync(idVelaPigmento);

        }
        
    }
}
