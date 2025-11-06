
using appVelas.Models;
using appVelas.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Repository
{
    public class RepositoryVelas
    {
        private readonly IVelaService _velaService;


        public RepositoryVelas(IVelaService velaService)
        {
            _velaService = velaService;
        }

        public List<VelaPigmento> Pigmentos { get; set; }
        public List<VelaFragancia> Fragancias { get; set; }


        // ------------------------------------- VELA ---------------------------------------------
        

        public async Task<CustomApiResponse<List<Vela>>> GetVelasAsync()
        {
            return await _velaService.GetVelasAsync();
        }

        public async Task<CustomApiResponse<Vela>> BuscarVelaAsync(Guid id)
        {
            return await _velaService.BuscarVelaAsync(id);
        }

        public async Task<CustomApiResponse<Vela>> InsertarVelaAsync(Vela vela)
        {
            return await _velaService.InsertarVelaAsync(vela);
        }

        public async Task<CustomApiResponse<Vela>> ActualizarVelaAsync(Guid idVela, Vela vela)
        {
            return await _velaService.ActualizarVelaAsync(idVela,vela);
        }
    }

}

