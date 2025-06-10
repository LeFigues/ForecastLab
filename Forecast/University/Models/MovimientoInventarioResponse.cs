using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace University.Models
{
    public class MovimientoInventarioResponse
    {
        public List<MovimientoInventario> Data { get; set; } = new();
        public object Paginacion { get; set; } = null!;
    }

}
