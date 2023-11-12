using System.ComponentModel.DataAnnotations;

namespace SistemaContas.Presentations.Models
{
    /// <summary>
    /// As categorias são categorias das contas que podem ser: 
    /// Despesa de Alimentos, Despesa Cartão de Crédito, Renda Pessoal
    /// Que podem ser relacionadas as contas:
    /// Jogo FIFA (Conta) Despesa Cartão de Crédito(Categoria)
    /// </summary>
    public class CategoryModel
    {
        [Required(ErrorMessage = "Informe o nome da categoria")]
        [MinLength(3, ErrorMessage = "O nome deve ter no mínimo {1} caracteres.")]
        [MaxLength(100, ErrorMessage = "O nome deve ter no mínimo {1} caracteres.")]
        public string? Name { get; set; }
    }
}
