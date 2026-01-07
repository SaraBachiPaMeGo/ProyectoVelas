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
        [JsonProperty("IDFrag")]
        public Guid IDFrag { get; set; }

        [JsonProperty("NombreFragancia")]
        public string NombreFragancia { get; set; }

        [JsonProperty("Cantidad")]
        public decimal? Cantidad { get; set; }

        [JsonProperty("Coste")]
        public decimal? Coste { get; set; }
    }

}
