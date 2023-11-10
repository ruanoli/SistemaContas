using SistemaContas.Data.Entities;

namespace SistemaContas.Presentations.Models
{
    /// <summary>
    /// As categorias são categorias das contas que podem ser: 
    /// Despesa de Alimentos, Despesa Cartão de Crédito, Renda Pessoal
    /// Que podem ser relacionadas as contas:
    /// Jogo FIFA (Conta) Despesa Cartão de Crédito(Categoria)
    /// </summary>
    public class Category
    {
        public Guid IdCategory { get; set; }
        public string? Name { get; set; }
        public Guid IdUser { get; set; }

        public UserRegister? User { get; set; }    
    }
}
