namespace fl_api.Models.Validation
{
    public class ForecastValidationItem
    {
        public string Insumo { get; set; } = null!;
        public string Mes { get; set; } = null!;
        public int CantidadPronosticada { get; set; }
        public int CantidadConsumida { get; set; }
        public int ErrorAbsoluto => Math.Abs(CantidadConsumida - CantidadPronosticada);
        public double ErrorPorcentual =>
            CantidadConsumida == 0 ? 0 : Math.Abs(CantidadConsumida - CantidadPronosticada) * 100.0 / CantidadConsumida;
    }
}
