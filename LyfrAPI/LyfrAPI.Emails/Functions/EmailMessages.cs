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

        public bool WelcomeEmail(string emailCliente, string nomeCliente)
        {
            try
            {

                var email = new Email
                {
                    ClienteEmail = emailCliente,
                    AssuntoEmail = "Seja bem - vindo ao Lyfr",
                    //variavel vai ate o arquivo.html, lê todo o conteúdo e retorna em formato de texto
                    ConteudoEmail = String.Format(System.IO.File.ReadAllText(@"../LyfrAPI/Emails/Templates/Welcome/Welcome.html"), nomeCliente)
                };

                var sucesso = new EmailSend().SendEmail(email);
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

                var sucesso = new EmailSend().SendEmail(email);
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
    }
}
