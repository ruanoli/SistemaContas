using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SistemaContas.Presentations.Models
{
    public class BillEditModel
    {
        public Guid IdBill { get; set; }

        [Required(ErrorMessage = "Insira o nome da sua conta.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Insira a data da sua conta.")]
        public DateTime? DateBill { get; set; }

        [Required(ErrorMessage = "Insira o valor da sua conta.")]
        public decimal ValueBill { get; set; }

        [Required(ErrorMessage = "Insira o tipo da sua conta.")]
        public int Type { get; set; }
        public string? Comments { get; set; }

        //Captura a categoria que foi escolida na combo
        [Required(ErrorMessage = "Insira a categoria da sua conta.")]
        public Guid IdCategory { get; set; }

        //Exibe as opções.
        public List<SelectListItem>? CategoryItems { get; set; }
    }
}
