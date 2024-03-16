using SistemaContas.Messages.Models;
using System.Net;
using System.Net.Mail;


namespace SistemaContas.Messages.Services
{
    /// <summary>
    /// Classe que implementa o serviço de envio de email
    /// </summary>
    public class EmailMessageService
    {
        private static string _email = "ruan1415oliveira@outlook.com";
        private static string _smtp = "smtp-mail.outlook.com";
        private static string _password = "Ru@n22099910";
        private static int _port = 587;

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
            var smtpClient = new SmtpClient(_smtp, _port);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(_email, _password);
            smtpClient.Send(mailMessage);
            #endregion 
        }
    }
}
