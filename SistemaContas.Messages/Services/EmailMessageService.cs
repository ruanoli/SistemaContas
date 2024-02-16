using SistemaContas.Messages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContas.Messages.Services
{
    /// <summary>
    /// Classe que implementa o serviço de envio de email
    /// </summary>
    public class EmailMessageService
    {
        private static string? _email = null;
        private static string? _smtp = null;
        private static string? _password = null;
        private static int? _port = null;

        /// <summary>
        /// Método para executar o envio de e-mail
        /// </summary>
        /// <param name="model"></param>
        public static void SendMessage(EmailMessageModel model)
        {
            #region Montando conteúdo do e-mail
            var mailMessage = new MailMessage(_email, model.EmailReceiver);

            mailMessage.Subject = model.Subject;
            mailMessage.Body = model.Message;
            mailMessage.IsBodyHtml = true; //formata o email caso receba código html.
            #endregion

            #region Enviando Email
            var smtpClient = new SmtpClient(_smtp, _port.Value);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(_email, _password);
            smtpClient.Send(mailMessage);
            #endregion
        }
    }
}
