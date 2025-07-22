using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace appVelas.Models
{
    [Table("Costes")]

    public class Costes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDCoste")]
        public int IDCoste { get; set; }

        [Column("TiempoProp")]
        public float TiempoProp { get; set; }

        [Column("HorasLuz")]
        public float HorasLuz{  get; set; }

        [Column("CosteLuz")]
        public float CosteLuz { get; set; }

        [Column("CosteTarj")]
        public float CosteTarj { get; set; }

        [Column("CosteFrag")]
        public float CosteFrag { get; set; }

        [Column("CosteMecha")]
        public float CosteMecha { get; set; }

        [Column("CostePack")]
        public float CostePack { get; set; }

        [Column("CosteCera")]
        public float CosteCera { get; set; }

        [Column("CosteMolde")]
        public float CosteMolde { get; set; }

        [Column("CosteVela")]
        public float CosteVela { get; set; }

        [Column("IDVela")]
        public int IDVela { get; set; }

        [Column("IDFrag")]
        public int IDFrag { get; set; }

        [Column("IDPig")]
        public int IDPig { get; set; }

        [Column("IDMolde")]
        public int IDMolde { get; set; }

        [Column("IDMecha")]
        public int IDMecha { get; set; }
    }
}
