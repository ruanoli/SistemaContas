using SistemaContas.Presentations.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace SistemaContas.Presentations.Models
{
    public class ChangePasswordModel
    {
        [StrongPassword(ErrorMessage = "Sua senha deve ter entre 8 e 16 caracteres, pelo menos 1 letra maiúscula, 1 minúscula, numerais e caracteres especiais.")]
        [Required(ErrorMessage = "A senha é um campo obrigatório!")]
        public string? NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "As senhas são divergentes. Por favor, confirme sua senha.")]
        [Required(ErrorMessage = "Por favor, confirme a sua senha!")]
        public string? NewPasswordConfirmed { get; set; }
    }
}
