﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace appVelas.Models
{
    [Table("Fragancia")]

    public class Fragancia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("IDFrag")]
        public Guid IDFrag { get; set; }

        [Column("FragNombre")]
        public string FragNombre { get; set; }

        [Column("Tipo")]
        public string Tipo { get; set; }

        [Column("CompradoEn")]
        public string CompradoEn { get; set; }

        [Column("Firma")]
        public string Firma { get; set; }

        [Column("IDVela")]
        public Guid IDVela { get; set; }

        [Column("Cantidad")]
        public decimal Cantidad { get; set; }

        [Column("Coste")]        
        public decimal Coste { get; set; }
    }
}
