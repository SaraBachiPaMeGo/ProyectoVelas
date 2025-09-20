using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace appVelas.Models
{

    public class Costes
    {
       
        
        [JsonProperty("IDCoste")]
        public int IDCoste { get; set; }

        [JsonProperty("TiempoProp")]
        public float TiempoProp { get; set; }

        [JsonProperty("HorasLuz")]
        public float HorasLuz{  get; set; }

        [JsonProperty("CosteLuz")]
        public float CosteLuz { get; set; }

        [JsonProperty("CosteTarj")]
        public float CosteTarj { get; set; }

        [JsonProperty("CosteFrag")]
        public float CosteFrag { get; set; }

        [JsonProperty("CosteMecha")]
        public float CosteMecha { get; set; }

        [JsonProperty("CostePack")]
        public float CostePack { get; set; }

        [JsonProperty("CosteCera")]
        public float CosteCera { get; set; }

        [JsonProperty("CosteMolde")]
        public float CosteMolde { get; set; }

        [JsonProperty("CosteVela")]
        public float CosteVela { get; set; }

        [JsonProperty("IDVela")]
        public int IDVela { get; set; }

        [JsonProperty("IDFrag")]
        public int IDFrag { get; set; }

        [JsonProperty("IDPig")]
        public int IDPig { get; set; }

        [JsonProperty("IDMolde")]
        public int IDMolde { get; set; }

        [JsonProperty("IDMecha")]
        public int IDMecha { get; set; }
    }
}
