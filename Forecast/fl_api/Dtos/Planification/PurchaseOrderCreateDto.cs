namespace fl_api.Dtos.Planification
{
    public class PurchaseOrderCreateDto
    {
        public string Proveedor { get; set; } = null!;
        public List<PurchaseOrderItemDto> Items { get; set; } = new();
        public string? Notas { get; set; }
    }
}
