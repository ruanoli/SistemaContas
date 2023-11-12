namespace SistemaContas.Presentations.Models
{
    /// <summary>
    /// As contas terão um tipo (pagar e receber) onde será possível cadastrar gastos e dinheiro que entra na conta.
    /// </summary>
    public class BillModel
    {
        public Guid IdBill { get; set; }
        public string? Name { get; set; }
        public string? Observation { get; set; }
        public decimal? Value { get; set; }
        public int Type { get; set; }
        public DateTime Date { get; set; }
        public Guid IdCategory { get; set; }

        public CategoryModel? Category { get; set; }
    }
}
