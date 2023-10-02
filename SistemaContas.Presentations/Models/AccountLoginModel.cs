using System.ComponentModel.DataAnnotations;

namespace SistemaContas.Presentations.Models
{
    public class AccountLoginModel
    {
        [Required(ErrorMessage = "Informe seu E-mail.")]
        [EmailAddress(ErrorMessage = "Informe um E-mail válido.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Informe sua Senha.")]
        [MinLength(8, ErrorMessage ="A senha deve ter no mínimo {1} caractere.")]
        [MaxLength(20, ErrorMessage ="A senha deve ter no máximo {1} caractere.")]
        public string? Password { get; set; }
    }
}
