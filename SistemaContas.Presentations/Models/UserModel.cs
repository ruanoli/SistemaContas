namespace SistemaContas.Presentations.Models
{
    /// <summary>
    /// Dados usados para armazenar no arquivo de cookie
    /// </summary>
    public class UserModel
    {
        public Guid IdUser { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime DateTimeAcess { get; set; }
    }
}
