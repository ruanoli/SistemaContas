using System.ComponentModel.DataAnnotations;

namespace SistemaContas.Presentations.Models
{
    public class AccountPasswordRecoverModel
    {
        [Required(ErrorMessage = "Informe seu E-mail.")]
        [EmailAddress(ErrorMessage = "Informe um E-mail válido.")]
        public string? Email { get; set; }
    }
}
