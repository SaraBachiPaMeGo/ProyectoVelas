using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace appVelas.Models
{

    public class Pedido
    {
       
        
        [JsonProperty("IDPedido")]
        public Guid IDPedido { get; set; }

        [JsonProperty("FechaPedi")]
        public DateTime FechaPedi { get; set; }

        [JsonProperty("FechaEntrega")]
        public DateTime FechaEntrega { get; set; }

        [JsonProperty("IDVelaFin")]
        public Guid IDVelaFin { get; set; }

        [JsonProperty("Vendido")]
        public bool? Vendido { get; set; }

        [JsonProperty("IDCliente")]
        public Guid IDCliente { get; set; }


        public Cliente Cliente { get; set; }

        // 🔗 Relación con Velas (un pedido -> muchas velas)
        public virtual ICollection<VelaFinalizada> VelaFin { get; set; } = new List<VelaFinalizada>();


        public ICollection<Documento>? Documentos { get; set; }

    }
}
