using appVelas.Data;
using appVelas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Repository
{
    public class RepositoryVelas
    {
        Contexto context;

        public RepositoryVelas(Contexto context)
        {
            this.context = context;
        }

        // ------------------------------------- VELA ---------------------------------------------

        public List<Vela> GetVelas()
        {
            List<Vela> velas= this.context.Vela.ToList();
            return velas;
        }

        public Vela BuscarVela(Guid idVela)
        {
            return this.context.Vela.SingleOrDefault
                (x => x.IDVela == idVela);
        }

        public void InsertarVela(Vela vel)
        {
            Vela vela = new Vela();

            //int? count = (from datos in context.Vela
            //              select datos.IDVela).Count();

                vela.IDVela =Guid.NewGuid();
            

            vela.VelaNombre = vel.VelaNombre;
            vela.Endurecedor = vel.Endurecedor;
            vela.FechaVenta = DateTime.Now;
            vela.FechaReal = DateTime.Now;
            vela.GradFrag = vel.GradFrag;
            vela.GradPig = vel.GradPig;
            vela.IDCera = Guid.NewGuid();
            vela.IDMecha = Guid.NewGuid();
            vela.Coste = vel.Coste;
            vela.IDMolde = Guid.NewGuid();
            vela.IDFrag = Guid.NewGuid();
            vela.IDPedido = Guid.NewGuid();
            vela.IDPig = Guid.NewGuid();
            vela.Observ = vel.Observ;

            //LLAMADA A STORE PROCEDURE PARA QUE ME CALCULE EL GASTO DE UNA VELA

            //        await context.Database.ExecuteSqlRawAsync(
            //"EXEC CalcularCosteVela @idVela = {0}", idVela);

            //            var resultado = context.Database.SqlQuery<ResultadoCosteVela>(
            //    "EXEC CalcularCosteVela @idVela = @id",
            //    new SqlParameter("@id", idVela)
            //).FirstOrDefault();

                                //        var resultado = await context
                                //.ResultadoCosteVela
                                //.FromSqlInterpolated($"EXEC CalcularCosteVela @idVela = {idVela}")
                                //.ToListAsync();

            //decimal coste = resultado.FirstOrDefault()?.CosteTotal ?? 0;


            //Controlar que la vuelta es un ok todo ha salido bien (200 en http)
            this.context.Vela.Add(vela);
            this.context.SaveChanges();
        }

        public void Actualizarvela(Vela vel)
        {
            Vela vela = BuscarVela(vel.IDVela);

            //MIRAR SI LOS CAMPOS ANTIGUOS SON LOS MISMOS QUE LOS DATOS QUE VIENEN
            if (vel.Endurecedor != null)
            {
                vela.Endurecedor = vel.Endurecedor;

            }

            if (vel.GradFrag != null)
            {
                vela.GradFrag = vel.GradFrag;
            }

            if (vel.GradPig != null)
            {
                vela.GradPig = vel.GradPig;
            }

            vela.FechaReal = DateTime.UtcNow;
            vela.FechaVenta = vel.FechaVenta; 

            if (vel.IDCera != null)
            {
                vela.IDCera = vel.IDCera;
            }

            if (vel.IDMecha != null)
            {
                vela.IDMecha = vel.IDMecha;
            }

            if (vel.Coste != null)
            {
                vela.Coste = vel.Coste;
            }

            if (vel.IDFrag != null)
            {
                vela.IDFrag = vel.IDFrag;
            }
            
            if (vel.IDPedido != null)
            {
                vela.IDPedido = vel.IDPedido;

            }

            this.context.SaveChanges();
        }

        // ------------------------------------- PIGMENTO ---------------------------------------------
        public void InsertarPigmento(Pigmento pi)  
        {
            Pigmento pig = new Pigmento();

            //int? count = (from datos in context.Pigmento
            //              select datos.IDPig).Count();

            //if (count == 0)
            //{
            //    pig.IDPig =Guid.NewGuid();
            //}
            //else
            //{
            //    //Error
            //}

            pig.IDPig = Guid.NewGuid();
            pig.Firma = pi.Firma;
            pig.Tipo = pi.Tipo;
            pig.ColorNombre = pi.ColorNombre;
            pig.CompradoEn = pi.CompradoEn;
            pig.IDVela = pi.IDVela;
            pig.Coste = pi.Coste;
            pig.Cantidad = pi.Cantidad;

            this.context.Pigmento.Add(pig);
            this.context.SaveChanges();
        }

        public void ActualizarPigmento(Pigmento pi)
        {
            Pigmento pig = BuscarPigmento(pi.IDPig);

            if (pi.Firma !=  null)
            {
                pig.Firma = pi.Firma;

            }

            if (pi.Tipo != null)
            {
                pig.Tipo = pi.Tipo;

            }

            if (pi.ColorNombre != null)
            {
                pig.ColorNombre = pi.ColorNombre;
            }

            if (pi.CompradoEn != null)
            {
                pig.CompradoEn = pi.CompradoEn;
            }

            if (pi.IDVela != null)
            {
                pig.IDVela = pi.IDVela;
            }

            if (pi.Coste != null)
            {
                pig.Coste = pi.Coste;
            }

            if (pi.Cantidad != null)
            {
                pig.Cantidad = pi.Cantidad;
            }

            this.context.SaveChanges();
        }
        public List<Pigmento> GetPigmentos()
        {
            return this.context.Pigmento.ToList();
        }
        public Pigmento BuscarPigmento(Guid idPig)
        {
            return this.context.Pigmento.SingleOrDefault
                (x => x.IDPig == idPig);
        }

        // ------------------------------------- FRAGANCIA ---------------------------------------------

        public void InsertarFragancia(Fragancia fragan)  
        {
            //String fragNombre, String tipo,
            //String compradoEn, String firma, int iDVela, int idCoste
            Fragancia frag = new Fragancia();

            //int? count = (from datos in context.Fragancia
            //              select datos.IDFrag).Count();

            //if (count == 0)
            //{
            //    frag.IDFrag = Guid.NewGuid();
            //}
            //else
            //{
            //    //Error
            //}

            frag.IDFrag = Guid.NewGuid();
            frag.FragNombre = fragan.FragNombre;
            frag.Tipo = fragan.Tipo;
            frag.CompradoEn = fragan.CompradoEn;
            frag.Firma = fragan.Firma;
            frag.IDVela = fragan.IDVela;
            frag.Coste = fragan.Coste;
            frag.Cantidad = fragan.Cantidad;


            this.context.Fragancia.Add(frag);
            this.context.SaveChanges();
        }

        public void ActualizarFragancia(Fragancia fragan)
        {
            Fragancia frag = BuscarFragancia(fragan.IDFrag);

            if (fragan.Firma != null)
            {
                frag.Firma = fragan.Firma;

            }

            if (fragan.Tipo != null)
            {
                frag.Tipo = fragan.Tipo;

            }

            if (fragan.CompradoEn != null)
            {
                frag.CompradoEn = fragan.CompradoEn;
            }

            if (fragan.IDVela != null)
            {
                frag.IDVela = fragan.IDVela;
            }

            if (fragan.FragNombre != null)
            {
                frag.FragNombre = fragan.FragNombre;
            }

            if (fragan.Coste != null)
            {
                frag.Coste = fragan.Coste;
            }

            if (fragan.Cantidad != null)
            {
                frag.Cantidad = fragan.Cantidad;
            }


            this.context.SaveChanges();
        }

        public List<Fragancia> GetFragancias()
        {
            int? count = (from datos in context.Fragancia
                         select datos.IDFrag).Count();

            Fragancia frag = this.context.Fragancia.FirstOrDefault();

            List<Fragancia> frags = this.context.Fragancia.ToList();

            return frags;
        }

        public Fragancia BuscarFragancia(Guid idFragancia)
        {
            return this.context.Fragancia.SingleOrDefault
                (x => x.IDFrag == idFragancia);
        }

        // ------------------------------------- MECHA ---------------------------------------------

        public void InsertarMecha(Mecha mech)  
        {
            Mecha mecha = new Mecha();

            int? count = (from datos in context.Mecha
                          select datos.IDMecha).Count();

            if (count == 0)
            {
                mecha.IDMecha =Guid.NewGuid();
            }
            else
            {
                //Error
            }

            mecha.Tipo = mech.Tipo;
            mecha.CompradoEn = mech.CompradoEn;
            mecha.Firma = mech.Firma;
            mecha.IDVela = mech.IDVela;
            mecha.Cantidad = mech.Cantidad;
            mecha.Coste = mech.Coste;

            this.context.Mecha.Add(mecha);
            this.context.SaveChanges();
        }

        public void ActualizarMecha(Mecha mech)
        {
            Mecha mecha = BuscarMecha(mech.IDMecha);

            if (mech.Firma != null)
            {
                mecha.Firma = mech.Firma;

            }

            if (mech.Tipo != null)
            {
                mecha.Tipo = mech.Tipo;

            }

            if (mech.CompradoEn != null)
            {
                mecha.CompradoEn = mech.CompradoEn;
            }

            if (mech.IDVela != null)
            {
                mecha.IDVela = mech.IDVela;
            }

            if (mech.Cantidad != null)
            {
                mecha.Cantidad = mech.Cantidad;
            }

            if (mech.Coste != null)
            {
                mecha.Coste = mech.Coste;
            }


            this.context.SaveChanges();
        }

        public List<Mecha> GetMechas()
        {
            return this.context.Mecha.ToList();
        }

        public Mecha BuscarMecha(Guid idMecha)
        {
            return this.context.Mecha.SingleOrDefault
                (x => x.IDMecha == idMecha);
        }

        // ------------------------------------- MOLDE ---------------------------------------------

        public void InsertarMolde(Molde mol)  
        {
            Molde molde = new Molde();

            int? count = (from datos in context.Molde
                          select datos.IDMolde).Count();

            if (count == 0)
            {
                molde.IDMolde = Guid.NewGuid();
            }
            else
            {
                //Error
            }

            molde.MoldeNombre = mol.MoldeNombre;
            molde.CMMecha = mol.CMMecha;
            molde.GramCera = mol.GramCera;
            molde.Medidas = mol.Medidas;
            molde.Duracion = mol.Duracion;
            molde.Observ = mol.Observ;
            molde.CompradoEn = mol.CompradoEn;
            molde.Firma = mol.Firma;
            molde.IDVela = mol.IDVela;
            molde.MilAgua = mol.MilAgua;
            molde.Tipo = mol.Tipo;
            molde.Coste = mol.Coste;

            this.context.Molde.Add(molde);
            this.context.SaveChanges();
        }

        public void ActualizarMolde(Molde mol)
        {
            Molde molde = BuscarMolde(mol.IDMolde);

            if (mol.Firma != null)
            {
                molde.Firma = mol.Firma;
            }

            if (mol.Tipo != null)
            {
                molde.Tipo = mol.Tipo;

            }

            if (mol.CompradoEn != null)
            {
                molde.CompradoEn = mol.CompradoEn;
            }

            if (mol.IDVela != null)
            {
                molde.IDVela = mol.IDVela;
            }

            if (mol.CMMecha != null)
            {
                molde.CMMecha = mol.CMMecha;
            }

            if (mol.GramCera != null)
            {
                molde.GramCera = mol.GramCera;
            }

            if (mol.Medidas != null)
            {
                molde.Medidas = mol.Medidas;
            }

            if (mol.Duracion != null)
            {
                molde.Duracion = mol.Duracion;
            }

            if (mol.Observ != null)
            {
                molde.Observ = mol.Observ;
            }

            if (mol.CompradoEn != null)
            {
                molde.CompradoEn = mol.CompradoEn;
            }

            if (mol.Coste != null)
            {
                molde.Coste = mol.Coste;
            }


            molde.Coste = mol.Coste;
            this.context.SaveChanges();
        }

        public List<Molde> GetMoldes()
        {
            return this.context.Molde.ToList();
        }

        public Molde BuscarMolde(Guid idMolde)
        {
            return this.context.Molde.SingleOrDefault
                (x => x.IDMolde == idMolde);
        }

        // ------------------------------------- CERA ---------------------------------------------
        public void InsertarCera(Cera cer)  
        {
            Cera cera = new Cera();

            //int? count = (from datos in context.Mecha
            //              select datos.IDMecha).Count();

            //if (count == 0)
            //{
            //    cera.IDCera =Guid.NewGuid();
            //}
            //else
            //{
            //    //Error
            //}
            cera.IDCera = Guid.NewGuid();

            cera.Tipo = cer.Tipo;
            cera.CompradoEn = cer.CompradoEn;
            cera.Firma = cer.Firma;
            //cera.IDVela = Guid.NewGuid();
            cera.Cantidad = cer.Cantidad;
            cera.Coste = cer.Coste;

            this.context.Cera.Add(cera);
            this.context.SaveChanges();
        }

        public void ActualizarCera(Cera cer)
        {
            Cera cera = BuscarCera(cer.IDCera);


            if (cer.Firma != null)
            {
                cera.Firma = cer.Firma;

            }

            if (cer.Tipo != null)
            {
                cera.Tipo = cer.Tipo;

            }

            if (cer.CompradoEn != null)
            {
                cera.CompradoEn = cer.CompradoEn;
            }

            if (cer.IDVela != null)
            {
                cera.IDVela = cer.IDVela;
            }

            if (cer.Coste != null)
            {
                cera.Coste = cer.Coste;

            }

            if (cer.Cantidad != null)
            {
                cera.Cantidad = cer.Cantidad;

            }

            this.context.SaveChanges();
        }

        public List<Cera> GetCeras()
        {
            return this.context.Cera.ToList();
        }

        public Cera BuscarCera(Guid idCera)
        {
            return this.context.Cera.SingleOrDefault
                (x => x.IDCera == idCera);
        }

        // ------------------------------------- CLIENTE ---------------------------------------------
        public void InsertarCliente(Cliente clie)
        {
            Cliente cli = new Cliente();

            int? count = (from datos in context.Cliente
                          select datos.IDCliente).Count();

            if (count == 0)
            {
                cli.IDCliente =Guid.NewGuid();
            }
            else
            {
                //Error
            }

            cli.Nombre = clie.Nombre;
            cli.Direccion = clie.Direccion;
            cli.Telf = clie.Telf;
            cli.Email = clie.Email;
            cli.IDPedido = clie.IDPedido;

            this.context.Cliente.Add(cli);
            this.context.SaveChanges();
        }

        public void ActualizarCliente(Cliente clie)
        {
            Cliente cli = BuscarCliente(clie.IDCliente);


            if (clie.Nombre != null)
            {
                cli.Nombre = clie.Nombre;

            }

            if (clie.Direccion != null)
            {
                cli.Direccion = clie.Direccion;

            }

            if (clie.Telf != null)
            {
                cli.Telf = clie.Telf;
            }

            if (clie.Email != null)
            {
                cli.Email = clie.Email;
            }

            if (clie.IDPedido != null)
            {
                cli.IDPedido = clie.IDPedido;
            }

            this.context.SaveChanges();
        }

        public List<Cliente> GetClientes()
        {
            return this.context.Cliente.ToList();
        }

        public Cliente BuscarCliente(Guid idCliente)
        {
            return this.context.Cliente.SingleOrDefault
                (x => x.IDCliente == idCliente);
        }

        // ------------------------------------- COSTES ---------------------------------------------
        public void InsertarCoste(float tiempoprop, float horasLuz, float costeLuz, 
            float costeTarj, float costeFrag, float costeMecha, float costePack, float costeCera,
            float costeMolde, float costeVela, 
            int idPig, int idFrag, int iDVela, int idMolde, int idMecha)
        {
            Costes coste = new Costes();

            int? count = (from datos in context.Costes
                          select datos.IDMecha).Count();

            if (count == 0)
            {
                //coste.Coste = Guid.NewGuid();
            }
            else
            {
                //Error
            }

            coste.TiempoProp = tiempoprop;
            coste.HorasLuz = horasLuz;
            coste.CosteLuz = costeLuz;
            coste.CosteTarj = costeTarj;
            coste.CosteFrag = costeFrag;
            coste.CosteMecha = costeMecha;
            coste.CostePack = costePack;
            coste.CosteCera = costeCera;
            coste.IDPig = idPig;
            coste.IDFrag = idFrag;
            coste.IDMolde = idMolde;
            coste.IDMecha = idMecha;
            coste.CosteMolde = costeMolde;
            coste.CosteVela = costeVela;            
            coste.IDVela = iDVela;

            this.context.Costes.Add(coste);
            this.context.SaveChanges();
        }

        public void ActualizarCoste(int idCoste, float tiempoprop, float horasLuz, float costeLuz,
            float costeTarj, float costeFrag, float costeMecha, float costePack, float costeCera,
            float costeMolde, float costeVela,
            int idPig, int idFrag, int iDVela, int idMolde, int idMecha)
        {
            Costes coste = BuscarCoste(idCoste);

            if (tiempoprop != null)
            {
                coste.TiempoProp = tiempoprop;
            }

            if (horasLuz != null)
            {
                coste.HorasLuz = horasLuz;
            }
            coste.CosteCera = costeCera;

            if (costeLuz != null)
            {
                coste.CosteLuz = costeLuz;
            }

            if (costeTarj != null)
            {
                coste.CosteTarj = costeTarj;
            }

            if (costeFrag != null)
            {
                coste.CosteFrag = costeFrag;
            }

            if (costeMecha != null)
            {
                coste.CosteMecha = costeMecha;
            }

            if (costePack != null)
            {
                coste.CostePack = costePack;
            }

            if (idPig != null)
            {
                coste.IDPig = idPig;
            }

            if (idFrag != null)
            {
                coste.IDFrag = idFrag;
            }

            if (idMolde != null)
            {
                coste.IDMolde = idMolde;
            }

            if (idMecha != null)
            {
                coste.IDMecha = idMecha;
            }


            if (costeMolde != null)
            {
                coste.CosteMolde = costeMolde;
            }

            if (costeVela != null)
            {
                coste.CosteVela = costeVela;
            }

            if (iDVela != null)
            {
                coste.IDVela = iDVela;
            }

            this.context.SaveChanges();
        }

        public List<Costes> GetCostes()
        {
            return this.context.Costes.ToList();
        }

        public Costes BuscarCoste(int idCoste)
        {
            return this.context.Costes.SingleOrDefault
                (x => x.IDCoste == idCoste);
        }

        // ------------------------------------- PEDIDO ---------------------------------------------
        public void InsertarPedido(Guid idCliente, Guid iDVela)
        {
            Pedido pedi = new Pedido();

            int? count = (from datos in context.Pedido
                          select datos.IDPedido).Count();

            if (count == 0)
            {
                pedi.IDPedido = Guid.NewGuid();
            }
            else
            {
                //Error
            }

            pedi.IDCliente = idCliente;
            pedi.IDVela = iDVela;

            this.context.Pedido.Add(pedi);
            this.context.SaveChanges();
        }

        public void ActualizarPedido(Guid idPedo, DateTime fechaEntrega, Guid idCliente,
            Guid iDVela)
        {
            Pedido pedi = BuscarPedido(idPedo);


            if (fechaEntrega != null)
            {
                pedi.FechaEntrega = fechaEntrega;

            }

            if (idCliente != null)
            {
                pedi.IDCliente = idCliente;

            }

            if (iDVela != null)
            {
                pedi.IDVela = iDVela;
            }

            this.context.SaveChanges();
        }

        public List<Pedido> GetPedidos()
        {
            return this.context.Pedido.ToList();
        }

        public Pedido BuscarPedido(Guid idPedo)
        {
            return this.context.Pedido.SingleOrDefault
                (x => x.IDPedido == idPedo);
        }

       
        // ------------------------------------- ENDURECEDOR ---------------------------------------------
        public void InsertarEndurecedor(Endurecedor cer)
        {
            Endurecedor cera = new Endurecedor();

            //int? count = (from datos in context.Mecha
            //              select datos.IDMecha).Count();

            //if (count == 0)
            //{
            //    cera.IDEndurecedor =Guid.NewGuid();
            //}
            //else
            //{
            //    //Error
            //}
            cera.IDEndurecedor = Guid.NewGuid();

            cera.Tipo = cer.Tipo;
            cera.CompradoEn = cer.CompradoEn;
            cera.Firma = cer.Firma;
            //cera.IDVela = Guid.NewGuid();
            cera.Cantidad = cer.Cantidad;
            cera.Coste = cer.Coste;

            this.context.Endurecedor.Add(cera);
            this.context.SaveChanges();
        }

        public void ActualizarEndurecedor(Endurecedor cer)
        {
            Endurecedor cera = BuscarEndurecedor(cer.IDEndurecedor);


            if (cer.Firma != null)
            {
                cera.Firma = cer.Firma;

            }

            if (cer.Tipo != null)
            {
                cera.Tipo = cer.Tipo;

            }

            if (cer.CompradoEn != null)
            {
                cera.CompradoEn = cer.CompradoEn;
            }

            if (cer.IDVela != null)
            {
                cera.IDVela = cer.IDVela;
            }

            if (cer.Coste != null)
            {
                cera.Coste = cer.Coste;

            }

            if (cer.Cantidad != null)
            {
                cera.Cantidad = cer.Cantidad;

            }

            this.context.SaveChanges();
        }

        public List<Endurecedor> GetEndurecedor()
        {
            return this.context.Endurecedor.ToList();
        }

        public Endurecedor BuscarEndurecedor(Guid idEndurecedor)
        {
            return this.context.Endurecedor.SingleOrDefault
                (x => x.IDEndurecedor == idEndurecedor);
        }

        // ------------------------------------- PACK ---------------------------------------------
        public void InsertarPack(Pack pack)
        {
            Pack packa = new Pack();

            //int? count = (from datos in context.Mecha
            //              select datos.IDMecha).Count();

            //if (count == 0)
            //{
            //    packa.IDCera =Guid.NewGuid();
            //}
            //else
            //{
            //    //Error
            //}
            packa.IDPack = Guid.NewGuid();

            packa.Tipo = pack.Tipo;
            packa.CompradoEn = pack.CompradoEn;
            packa.Firma = pack.Firma;
            //packa.IDVela = Guid.NewGuid();
            packa.Cantidad = pack.Cantidad;
            packa.Coste = pack.Coste;

            this.context.Pack.Add(packa);
            this.context.SaveChanges();
        }

        public void ActualizarPack(Pack pack)
        {
            Pack packa = BuscarPack(pack.IDPack);


            if (pack.Firma != null)
            {
                packa.Firma = pack.Firma;

            }

            if (pack.Tipo != null)
            {
                packa.Tipo = pack.Tipo;

            }

            if (pack.CompradoEn != null)
            {
                packa.CompradoEn = pack.CompradoEn;
            }

            if (pack.IDVela != null)
            {
                packa.IDVela = pack.IDVela;
            }

            if (pack.Coste != null)
            {
                packa.Coste = pack.Coste;

            }

            if (pack.Cantidad != null)
            {
                packa.Cantidad = pack.Cantidad;

            }

            this.context.SaveChanges();
        }

        public List<Pack> GetPacks()
        {
            return this.context.Pack.ToList();
        }

        public Pack BuscarPack(Guid idPack)
        {
            return this.context.Pack.SingleOrDefault
                (x => x.IDPack == idPack);
        }

    }
}
