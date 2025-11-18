
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

        //public CustomApiResponse<Vela> ActualizarVela(Vela vel)
        //{
        //    var response = new CustomApiResponse<Vela>();

        //    try
        //    {
        //        //var vela = context.Vela
        //        //    .Include(v => v.VelaPigmentos)
        //        //    .Include(v => v.VelaFragancias)
        //        //    .Include(v => v.VelaCeras)
        //        //    .Include(v => v.VelaMechas)
        //        //    .Include(v => v.VelaEndurecedores)
        //        //    .Include(v => v.VelaMoldes)
        //        //    .SingleOrDefault(v => v.IDVela == vel.IDVela);

        //        //if (vela == null)
        //        //    throw new Exception("La vela no existe");

        //        //// ------------------------------
        //        //// 🔥 ACTUALIZAR MATERIALES
        //        //// ------------------------------

        //        //ActualizarMaterial<VelaPigmento, Pigmento>(
        //        //    vela.VelaPigmentos,
        //        //    vel.VelaPigmentos,
        //        //    p => p.IDPig,
        //        //    (pBD, usado) => CalcularCosteUso(usado.Cantidad, pBD.CantidadCompra, pBD.CosteCompra)
        //        //);

        //        //ActualizarMaterial<VelaFragancia, Fragancia>(
        //        //    vela.VelaFragancias,
        //        //    vel.VelaFragancias,
        //        //    f => f.IDFrag,
        //        //    (fBD, usado) => CalcularCosteUso(usado.Cantidad, fBD.CantidadCompra, fBD.CosteCompra)
        //        //);

        //        //ActualizarMaterial<VelaCera, Cera>(
        //        //    vela.VelaCeras,
        //        //    vel.VelaCeras,
        //        //    c => c.IDCera,
        //        //    (cBD, usado) => CalcularCosteUso(usado.Cantidad, cBD.CantidadCompra, cBD.CosteCompra)
        //        //);

        //        //ActualizarMaterial<VelaMecha, Mecha>(
        //        //    vela.VelaMechas,
        //        //    vel.VelaMechas,
        //        //    m => m.IDMecha,
        //        //    (mBD, usado) => CalcularCosteUso(usado.Cantidad, mBD.CantidadCompra, mBD.CosteCompra)
        //        //);

        //        //ActualizarMaterial<VelaEndurecedor, Endurecedor>(
        //        //    vela.VelaEndurecedores,
        //        //    vel.VelaEndurecedores,
        //        //    e => e.IDEndurecedor,
        //        //    (eBD, usado) => CalcularCosteUso(usado.Cantidad, eBD.CantidadCompra, eBD.CosteCompra)
        //        //);

        //        //ActualizarMaterial<VelaMolde, Molde>(
        //        //    vela.VelaMoldes,
        //        //    vel.VelaMoldes,
        //        //    m => m.IDMolde,
        //        //    (mBD, usado) => CalcularCosteUso(usado.Cantidad, mBD.CantidadCompra, mBD.CosteCompra)
        //        //);

        //        // ---------------------------------
        //        // 🔥 CALCULAR EL COSTE TOTAL DE LA VELA
        //        // ---------------------------------
        //        //vela.CosteTotal =
        //        //    vela.VelaPigmentos.Sum(p => p.CosteUso) +
        //        //    vela.VelaFragancias.Sum(f => f.CosteUso) +
        //        //    vela.VelaCeras.Sum(c => c.CosteUso) +
        //        //    vela.VelaMechas.Sum(m => m.CosteUso) +
        //        //    vela.VelaEndurecedores.Sum(e => e.CosteUso) +
        //        //    vela.VelaMoldes.Sum(mo => mo.CosteUso);

        //        context.SaveChanges();

        //        response.Object = vela;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Error = new ErrorViewModel { Mensaje = ex.Message };
        //    }

        //    return response;
        //}


    }

}

