using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContas.Messages.Models
{
    /// <summary>
    /// Modelo de dados para envio de emails
    /// </summary>
    public class EmailMessageModel
    {
        public string? EmailReceiver { get; set; } //destinatário
        public string? Message { get; set; } 
        public string? Subject { get; set; } //assunto
    }
}
