using fl_api.Dtos.Guides;
using fl_api.Interfaces.Guides;
using fl_api.Interfaces.University;
using fl_api.Models.Guides;
using fl_api.Models.University;

namespace fl_api.Services.Guides
{
    public class LabGuideVerificationService : ILabGuideVerificationService
    {
        private readonly INormalizedSupplyRepository _supplyRepo;

        public LabGuideVerificationService(INormalizedSupplyRepository supplyRepo)
        {
            _supplyRepo = supplyRepo;
        }

        public async Task<GuideVerificationResult> VerifyAgainstStockAsync(LabGuide guide)
        {
            var supplies = await _supplyRepo.GetAllAsync();

            // Acumular cantidades por descripción
            var requeridos = new Dictionary<string, (int Total, string Unidad)>(StringComparer.OrdinalIgnoreCase);

            foreach (var practice in guide.Practices)
            {
                var grupos = practice.GroupCount;

                foreach (var item in practice.Materials.Equipment.Concat(practice.Materials.Supplies))
                {
                    var clave = item.Description.Trim().ToLower();
                    var cantidad = item.QuantityPerGroup * grupos;

                    if (!requeridos.ContainsKey(clave))
                        requeridos[clave] = (cantidad, item.Unit);
                    else
                        requeridos[clave] = (
                            requeridos[clave].Total + cantidad,
                            requeridos[clave].Unidad
                        );
                }
            }

            var resultado = new GuideVerificationResult();

            foreach (var kv in requeridos)
            {
                var nombre = kv.Key;
                var cantidad = kv.Value.Total;
                var unidad = kv.Value.Unidad;

                var match = supplies.FirstOrDefault(s =>
                    s.NombreNormalizado.Trim().ToLower() == nombre ||
                    s.Nombre.Trim().ToLower() == nombre);

                if (match != null)
                {
                    resultado.Verificados.Add(new GuideMaterialVerificationItem
                    {
                        Nombre = match.Nombre,
                        CantidadTotal = cantidad,
                        Stock = match.StockTotal,
                        Unidad = match.UnidadMedida,
                        IdInsumo = match.IdInsumo,
                        EsSugerencia = false
                    });
                }
                else
                {
                    // Buscar sugerencia por Levenshtein o coincidencia parcial
                    var sugerido = supplies
                        .Select(s => new
                        {
                            Insumo = s,
                            Distancia = Levenshtein(nombre, s.NombreNormalizado.Trim().ToLower())
                        })
                        .OrderBy(x => x.Distancia)
                        .FirstOrDefault(x =>
                            x.Distancia <= 5 ||
                            x.Insumo.NombreNormalizado.Contains(nombre, StringComparison.OrdinalIgnoreCase) ||
                            nombre.Contains(x.Insumo.NombreNormalizado, StringComparison.OrdinalIgnoreCase)
                        );

                    if (sugerido != null)
                    {
                        resultado.Verificados.Add(new GuideMaterialVerificationItem
                        {
                            Nombre = sugerido.Insumo.Nombre,
                            CantidadTotal = cantidad,
                            Stock = sugerido.Insumo.StockTotal,
                            Unidad = sugerido.Insumo.UnidadMedida,
                            IdInsumo = sugerido.Insumo.IdInsumo,
                            EsSugerencia = true
                        });
                    }
                    else
                    {
                        resultado.NoEncontrados.Add(new UnmatchedMaterialItem
                        {
                            Nombre = nombre,
                            CantidadTotal = cantidad,
                            Unidad = unidad
                        });
                    }
                }
            }

            return resultado;
        }

        private int Levenshtein(string a, string b)
        {
            if (string.IsNullOrWhiteSpace(a) || string.IsNullOrWhiteSpace(b))
                return int.MaxValue;

            var costs = new int[b.Length + 1];
            for (int j = 0; j < costs.Length; j++) costs[j] = j;

            for (int i = 1; i <= a.Length; i++)
            {
                costs[0] = i;
                int nw = i - 1;
                for (int j = 1; j <= b.Length; j++)
                {
                    int cj = Math.Min(1 + Math.Min(costs[j], costs[j - 1]),
                                      a[i - 1] == b[j - 1] ? nw : nw + 1);
                    nw = costs[j];
                    costs[j] = cj;
                }
            }
            return costs[b.Length];
        }
    }
}
