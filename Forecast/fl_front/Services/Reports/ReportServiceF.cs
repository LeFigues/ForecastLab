using fl_front.Dtos.Reports;
using fl_front.Interfaces;
using System.Net.Http.Json;

namespace fl_front.Services.Reports
{
    public class ReportServiceF : IReportServiceF
    {
        private readonly HttpClient _http;

        public ReportServiceF(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ConsumoVsPronosticoDtoF>> GetConsumoVsPronosticoAsync(ReportFilterDtoF filter)
        {
            var response = await _http.PostAsJsonAsync("api/reports/consumo-vs-pronostico", filter);
            return await response.Content.ReadFromJsonAsync<List<ConsumoVsPronosticoDtoF>>() ?? new();
        }

        public async Task<List<UnidadesAComprarDtoF>> GetUnidadesAComprarAsync(ReportFilterDtoF filter)
        {
            var response = await _http.PostAsJsonAsync("api/reports/unidades-a-comprar", filter);
            return await response.Content.ReadFromJsonAsync<List<UnidadesAComprarDtoF>>() ?? new();
        }
    }
}
