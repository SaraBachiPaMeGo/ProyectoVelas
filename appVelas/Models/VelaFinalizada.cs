using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Models
{
    public class VelaFinalizada
    {
        [JsonProperty("IDVelaFin")]
        public Guid IDVelaFin { get; set; }

        [JsonProperty("IDVela")]
        public Guid IDVela { get; set; }

        [JsonProperty("IDPack")]
        public Guid? IDPack { get; set; }

        [JsonProperty("Coste")]
        public decimal? Coste { get; set; }

        [JsonProperty("Beneficio")]
        public decimal? Beneficio { get; set; }

        [JsonProperty("PVP")]
        public decimal? PVP { get; set; }

        [JsonProperty("IDPedido")]
        public Guid? IDPedido { get; set; }

        [JsonProperty("FechaFin")]
        public DateTime FechaFin { get; set; }

        [JsonProperty("Pack")]

        public List<Pack> Pack { get; set; }

        //// 🔗 NAVEGACIÓN A PEDIDO
        //public Pedido? Pedido { get; set; }
    }
}
