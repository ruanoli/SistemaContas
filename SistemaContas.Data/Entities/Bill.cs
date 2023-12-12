using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContas.Data.Entities
{
    public class Bill
    {
        public Guid IdBill { get; set; }
        public string? Name { get; set; }
        public DateTime DataBill { get; set; }
        public decimal? ValueBill { get; set; }
        public int TypeBill { get; set; }
        public string? Comments { get; set; }
        public Guid IdCategory { get; set; }
        public Guid IdUser { get; set; }
    }
}
