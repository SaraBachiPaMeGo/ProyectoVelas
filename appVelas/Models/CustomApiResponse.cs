using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Models
{
    public class CustomApiResponse
    {
        public List<T> Object { get; set; }

        public ErrorViewModel Error { get; set; }
    }
}
