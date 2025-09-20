using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appVelas.Models;
using appVelas.Service.Interfaces;

namespace appVelas.Service
{
    public class VelaPigmentoService : IVelaPigmentoService
    {
        public Task<List<VelaPigmento>> GetPigmentosPorVelaAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertarVelaPigmentoAsync(VelaPigmento velaPigmento)
        {
            throw new NotImplementedException();
        }
    }
}
