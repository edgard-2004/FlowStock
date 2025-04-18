namespace FlowStock.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Imagen { get; set; } = string.Empty;
        public decimal CostoUnitario { get; set; }
        public decimal Precio { get; set; }
        public int StockActual { get; set; }
        public bool Activo { get; set; }
        public string Categoria { get; set; } = string.Empty;
    }
}