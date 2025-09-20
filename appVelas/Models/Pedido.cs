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

        [JsonProperty("IDVela")]
        public Guid? IDVela { get; set; }

        [JsonProperty("IDCliente")]
        public Guid IDCliente { get; set; }
    }
}
