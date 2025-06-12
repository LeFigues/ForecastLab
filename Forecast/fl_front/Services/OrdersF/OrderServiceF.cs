using fl_front.Dtos.OrdersF;
using fl_front.Interfaces;
using System.Net.Http.Json;

namespace fl_front.Services.OrdersF
{
    public class OrderServiceF : IOrderServiceF
    {
        private readonly HttpClient _http;

        public OrderServiceF(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<PurchaseOrderDtoF>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<PurchaseOrderDtoF>>("api/orders") ?? new();
        }

        public async Task<PurchaseOrderDetailF?> GetByIdAsync(string id)
        {
            return await _http.GetFromJsonAsync<PurchaseOrderDetailF>($"api/orders/{id}");
        }

        public async Task<bool> CreateAsync(CreateOrderDtoF dto)
        {
            var payload = new
            {
                proveedor = dto.Proveedor,
                notas = dto.Notas,
                items = dto.Items.Select(i => new
                {
                    insumo = i.Insumo,
                    cantidad = i.Cantidad,
                    precioUnitario = i.PrecioUnitario,
                    fechaEntregaDeseada = i.FechaEntregaDeseada.ToString("yyyy-MM-dd")
                })
            };
            var response = await _http.PostAsJsonAsync("api/orders", payload);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CambiarEstadoAsync(string id, string accion)
        {
            var response = await _http.PutAsync($"api/orders/{id}/{accion}", null);
            return response.IsSuccessStatusCode;
        }
    }
}
