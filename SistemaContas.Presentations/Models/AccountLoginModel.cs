using System.ComponentModel.DataAnnotations;

namespace SistemaContas.Presentations.Models
{
    public class AccountLoginModel
    {
        [Required(ErrorMessage = "O e-mail é um campo obrigatório!")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido, por favor.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A senha é um campo obrigatório.")]
        [MinLength(8, ErrorMessage ="A senha deve ter no mínimo {1} caractere.")]
        [MaxLength(20, ErrorMessage ="A senha deve ter no máximo {1} caractere.")]
        public string? Password { get; set; }
    }
}
