using SistemaContas.Presentations.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace SistemaContas.Presentations.Models
{
    public class AccountRegisterModel
    {
        [RegularExpression("^[A-Za-zà-üÀ-Ü\\s]{3,150}$", ErrorMessage = "Por favor, informe um nome válido.")]
        [Required(ErrorMessage = "O nome é um campo obrigatório!")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "O e-mail é um campo obrigatório!")]
        [EmailAddress(ErrorMessage = "Por favor, informe um e-mail válido.")]
        public string? Email { get; set; }

        [StrongPassword(ErrorMessage = "Essa senha é muito fraca, querido.")]
        [Required(ErrorMessage = "A senha é um campo obrigatório!")]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "As senhas são divergentes. Por favor, confirme sua senha.")]
        [Required(ErrorMessage = "Por favor, confirme a sua senha!")]
        public string? PasswordConfirmed { get; set; }
    }
}
