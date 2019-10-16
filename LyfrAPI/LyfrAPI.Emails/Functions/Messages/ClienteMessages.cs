using LyfrAPI.Models.ModelsEmail;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


namespace LyfrAPI.Emails.Functions
{
    public class ClienteMessages
    {
        //variavel que será injetada com o diretorio de execução do projeto
        //usada pra pegar os templates dos emails
        private PhysicalFileProvider _provider;

        public ClienteMessages(PhysicalFileProvider provider)
        {
            _provider = provider;
        }

        public bool WelcomeEmail(string emailCliente, string nomeCliente)
        {
            string diretorioEmail = _provider.GetFileInfo("wwwroot/Email/Templates/Welcome/Welcome.html").PhysicalPath;
            string conteudoEmail = File.ReadAllText(diretorioEmail);
            
            try
            {
                var email = new Email
                {
                    ClienteEmail = emailCliente,
                    AssuntoEmail = "Seja bem - vindo ao Lyfr",
                    ConteudoEmail = conteudoEmail.Replace("{0}", nomeCliente)
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
;            }
        }

        public string ForgotPasswordEmail(RecoveryPassword recuperarSenha)
        {
            try
            {
                var email = new Email
                {
                    ClienteEmail = recuperarSenha.Email,

                    AssuntoEmail = "Lyfr - Esqueceu sua senha?",

                    ConteudoEmail = String.Format("</strong>Você esqueceu sua senha jovem leitor?<br><br></strong>" +
                    " Alguém requisitou a opção \"Esqueci senha\" e foi passado seu email," +
                    " caso seja você não se preocupe. <br><br><strong>Aqui está seu código de recuperação: {0}</strong><br><br>" +
                    "<br>"+
                    " Você pode mudar sua senha acessando nosso aplicativo ou plataforma web.<br>" +
                    " <br>Caso não tenha sido você desconsidere esse email!<br><br><br><br>" +
                    "<strong>Atenciosamente, equipe Lyfr!</strong>", recuperarSenha.CodigoGerado)
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
