using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace appVelas.Models
{
    public class VelaFragancia
    {
        
        [JsonProperty("IDVela")]
        public Guid IDVela { get; set; }

        [JsonProperty("Vela")]
        public Vela Vela { get; set; }

        
        [JsonProperty("IDFrag")]
        public Guid IDFrag { get; set; }

        [JsonProperty("Fragancia")]
        public Fragancia Fragancia { get; set; }

        [JsonProperty("Cantidad")]
        public decimal? Cantidad { get; set; }

        [JsonProperty("Coste")]
        public decimal? Coste { get; set; }
    }

}
