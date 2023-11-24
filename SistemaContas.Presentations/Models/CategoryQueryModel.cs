namespace SistemaContas.Presentations.Models
{
    /// <summary>
    /// Modelo de dados para a página de consulta de categoria.
    /// SOMENTE CONSULTA.
    /// </summary>
    public class CategoryQueryModel
    {
        public Guid IdCategory { get; set; }
        public string? Name { get; set; }
    }
}
