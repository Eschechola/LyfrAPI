using LyfrAPI.Models.ModelsEmail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


namespace LyfrAPI.Emails.Functions
{
    public class EmailMessages
    {

        public bool WelcomeEmail(string emailCliente)
        {
            try
            {

                var email = new Email
                {
                    ClienteEmail = emailCliente,
                    AssuntoEmail = "Seja bem - vindo ao Lyfr",
                    //variavel vai ate o arquivo.html, lê todo o conteúdo e retorna em formato de texto
                    ConteudoEmail = System.IO.File.ReadAllText(@"../LyfrAPI/Emails/Templates/Welcome/Welcome.html")
                };

                var sucesso = SendEmail(email);
                if (sucesso)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string UpdatePasswordEmail(string emailCliente, string senhaCliente)
        {
            try
            {
                var email = new Email
                {
                    ClienteEmail = emailCliente,

                    AssuntoEmail = "Lyfr - Esqueceu a senha?",

                    ConteudoEmail = String.Format("</strong>Você esqueceu sua senha jovem leitor?<br><br></strong>" +
                    " Alguém requisitou a opção \"Esqueci senha\" e foi passado seu email," +
                    " caso seja você não se preocupe. <br><br><strong>Aqui está sua senha: {0}</strong><br><br>" +
                    "<br>Por favor anote ou guarde - a em algum lugar seguro," +
                    " você pode muda - la acessando nosso aplicativo ou plataforma web.<br>" +
                    " <br>Caso não tenha sido você desconsidere esse email!<br><br><br><br>" +
                    "<strong>Atenciosamente, equipe Lyfr!</strong>", senhaCliente)
                };

                var sucesso = SendEmail(email);
                if (sucesso)
                {
                    return "Email enviado com sucesso!";
                }
                else
                {
                    return "Ocorreu algum erro na hora de enviarmos o e-mail, por favor tente novamente!";
                }
            }
            catch (Exception)
            {

                return "Tivemos alguns problemas de conexão. Por favor tente novamente mais tarde.";
            }
        }


        private bool SendEmail(Email email)
        {
            try
            {
                //cria a mensagem de email
                MailMessage message = new MailMessage();
                //instancia o cliente smtp
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                //email que vai enviar (no caso nós)
                message.From = new MailAddress("contato.Lyfr@gmail.com");
                //para quem vai enviar (no caso o usuario)
                message.To.Add(new MailAddress(email.ClienteEmail));
                //atribui assundo do email recebido como parametro na classe email
                message.Subject = email.AssuntoEmail;
                //habilita usar html na mensagem
                message.IsBodyHtml = true;
                //atribui a mensagem recebida como parametro na classe email
                message.Body = email.ConteudoEmail;
                //porta que usarei pra enviar o email
                smtp.Port = 587;    
                //habilita camada de segurança
                smtp.EnableSsl = true;
                //desabilita as credenciais padrao
                smtp.UseDefaultCredentials = false;
                //da as credenciais do nosso para poder acessar e enviar
                smtp.Credentials = new NetworkCredential("contato.Lyfr@gmail.com", "l1fr_endeavour");
                //define o método de envio, no caso web
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //envia a mensagem
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
