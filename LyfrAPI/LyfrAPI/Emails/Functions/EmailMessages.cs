using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LyfrAPI.Emails.Functions
{
    public class EmailMessages
    {

        public bool WelcomeEmail(string usuarioEmail)
        {
            try
            {
                //variavel vai ate o arquivo.html, lê todo o conteúdo e retorna em formato de texto
                string conteudoTemplate = System.IO.File.ReadAllText(@"../LyfrAPI/Emails/Templates/Welcome/Welcome.html");

                var sucesso = SendEmail(usuarioEmail, conteudoTemplate);
                if (sucesso)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (global::System.Exception)
            {
                return false;
            }
        }

        public bool SendEmail(string usuarioEmail, string conteudoTemplate)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("contato.Lyfr@gmail.com");
                message.To.Add(new MailAddress(usuarioEmail));
                message.Subject = "Test";
                message.IsBodyHtml = true;
                message.Body = conteudoTemplate;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("contato.Lyfr@gmail.com", "l1fr_endeavour");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

                return true;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
