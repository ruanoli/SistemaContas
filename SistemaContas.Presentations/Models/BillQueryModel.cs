using System.ComponentModel.DataAnnotations;

namespace SistemaContas.Presentations.Models
{
    public class BillQueryModel
    {
        [Required(ErrorMessage = "Informe a data de ínicio")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "Informe a data de fim")]
        public DateTime? EndDate { get; set; }
    }
}
