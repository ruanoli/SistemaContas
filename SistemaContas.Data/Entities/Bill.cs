using SistemaContas.Presentations.Models;
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
        public DateTime? Date { get; set; }
        public decimal? Value { get; set; }
        public int Type { get; set; }
        public string? Observation { get; set; }
        public Guid IdCategory { get; set; }
        public Guid IdUser { get; set; }

        public Category? Category { get; set; }
    }
}
