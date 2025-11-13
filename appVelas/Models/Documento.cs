using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Models
{
    public class Documento
    {
        public Guid IDDoc { get; set; }
                
        public Guid IDVela { get; set; }

        public Guid IDPack { get; set; }

        public Guid IDEndurecedor { get; set; }

        public Guid IDMecha { get; set; }

        public Guid IDCera { get; set; }

        public Guid IDFrag { get; set; }

        public Guid IDPig { get; set; }

        public Guid IDMolde { get; set; }

        public Vela Vela { get; set; }

        public Pack pack { get; set; }

        public Endurecedor endurecedor { get; set; }

        public Mecha mecha { get; set; }

        public Cera cera { get; set; }

        public Fragancia fragancia { get; set; }

        public Pigmento pigmento { get; set; }

        public Molde molde { get; set; }
                
        public string NombreDoc { get; set; }

        public string TipoDoc { get; set; }

        public string? Ruta { get; set; }

        public byte[]? Datos { get; set; }

        public DateTime FechaSubida { get; set; }
    }
}
