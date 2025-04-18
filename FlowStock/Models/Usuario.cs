namespace FlowStock.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string Clave { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;
    }
}