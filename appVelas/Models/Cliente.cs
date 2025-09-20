using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace appVelas.Models
{

    public class Cliente
    {
       
        
        [JsonProperty("IDCliente")]
        public Guid IDCliente { get; set; }

        [JsonProperty("Nombre")]
        public string Nombre { get; set; }

        [JsonProperty("Direccion")]
        public string Direccion { get; set; }

        [JsonProperty("Telf")]
        public string Telf { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("IDPedido")]
        public Guid IDPedido { get; set; }
    }
}
