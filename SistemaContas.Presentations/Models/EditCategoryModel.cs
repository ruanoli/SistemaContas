using System.ComponentModel.DataAnnotations;

namespace SistemaContas.Presentations.Models
{
    public class EditCategoryModel
    {
        public Guid IdCategory { get; set; }

        [Required(ErrorMessage = "Informe o nome da categoria")]
        [MinLength(3, ErrorMessage = "O nome deve ter no mínimo {1} caracteres.")]
        [MaxLength(100, ErrorMessage = "O nome deve ter no mínimo {1} caracteres.")]
        public string? Name { get; set; }
    }
}
