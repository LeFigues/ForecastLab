namespace fl_api.Dtos.Forecast
{
    public class ForecastResponseDto
    {
        // Serie histórica: lista de { Periodo, UnidadesConsumidas }
        public List<MonthValueDto> Historico { get; set; } = new();

        // Forecast de GPT: lista de { Periodo, UnidadesPronosticadas }
        public List<ForecastResultDto> PronosticoFuturo { get; set; } = new();

        public int StockActual { get; set; }
        public int TotalPronosticadoSemestre { get; set; }
        public int UnidadesAComprar { get; set; }
        public decimal CostoEstimado { get; set; }   // en la misma moneda que uses
    }
}
