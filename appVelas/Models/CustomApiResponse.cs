using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appVelas.Models
{
    public class CustomApiResponse<T>
    {
        public T Data { get; set; }

        public ErrorViewModel Error { get; set; }
    }
}
