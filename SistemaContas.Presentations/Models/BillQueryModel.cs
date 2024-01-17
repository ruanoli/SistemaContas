using SistemaContas.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace SistemaContas.Presentations.Models
{
    public class BillQueryModel
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Irá retornar a listagem das contas após a realização da cconsulta.
        /// </summary>
        public IList<Bill>? Bills { get; set; }
    }
}
