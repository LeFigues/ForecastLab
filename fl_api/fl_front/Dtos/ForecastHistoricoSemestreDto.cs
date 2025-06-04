using fl_front.Models;

namespace fl_front.Dtos
{
    public class ForecastHistoricoSemestreDto
    {
        // MongoDB ObjectId simulado como objeto anidado
        public IdDto Id { get; set; } = new IdDto();

        public string InsumoNombre { get; set; } = string.Empty;
        public int StockActual { get; set; }
        public int TotalPronosticadoSemestre { get; set; }
        public int UnidadesAComprar { get; set; }
        public double CostoEstimado { get; set; }

        // Lista de pronósticos mensuales (puede venir vacía)
        public PronosticoMensualDto[] PronosticoMensual { get; set; } = Array.Empty<PronosticoMensualDto>();

        public DateTime FechaRegistro { get; set; }
    }
}
