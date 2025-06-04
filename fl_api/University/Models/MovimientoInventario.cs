using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace University.Models
{
    public class MovimientoInventario
    {
        [JsonPropertyName("id_movimiento")]
        public int IdMovimiento { get; set; }

        [JsonPropertyName("tipo_movimiento")]
        public string TipoMovimiento { get; set; } = string.Empty;

        [JsonPropertyName("fecha_entregado")]
        public DateTime FechaEntregado { get; set; }

        [JsonPropertyName("fecha_devuelto")]
        public DateTime? FechaDevuelto { get; set; }

        [JsonPropertyName("cantidad")]
        public int Cantidad { get; set; }

        [JsonPropertyName("responsable")]
        public string Responsable { get; set; } = string.Empty;

        // <-- HACEMOS nullable int para no fallar si viene null
        [JsonPropertyName("id_solicitud")]
        public int? IdSolicitud { get; set; }

        [JsonPropertyName("insumo_nombre")]
        public string InsumoNombre { get; set; } = string.Empty;
    }

}
