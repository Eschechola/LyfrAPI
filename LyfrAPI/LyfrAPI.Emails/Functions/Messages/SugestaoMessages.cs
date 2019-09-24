using LyfrAPI.Models;
using LyfrAPI.Models.ModelsEmail;
using System;
using System.Collections.Generic;
using System.Text;

namespace LyfrAPI.Emails.Functions.Messages
{
    public class SugestaoMessages
    {
        public bool RespostaSugestao(SugestaoResposta resposta)
        {
            try
            {
                var email = new Email
                {
                    ClienteEmail = resposta.Email,
                    AssuntoEmail = String.Format("Resposta a sugestão #{0}", resposta.Id),
                    ConteudoEmail = resposta.Mensagem
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
    }
}
