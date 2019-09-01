using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LyfrAPI.Validations
{
    public class ValidationFields
    {
        private Regex expressaoRegular;

        public bool ValidateCpf(string cpf)
        {
            expressaoRegular = new Regex(@"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$", RegexOptions.None);

            if (expressaoRegular.IsMatch(cpf))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidateEmail(string email)
        {
            expressaoRegular = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.None);

            if(string.IsNullOrEmpty(email)|| string.IsNullOrWhiteSpace(email)|| email == string.Empty)
            {
                return false;
            }
            else if (expressaoRegular.IsMatch(email))
            {
                return true;
            }
            else if (email.Length < 10)
            {
                return false;
            }
            else
            {
                return false;
            }
        }

        public bool ValidateLogin(string login)
        {
            expressaoRegular = new Regex(@"\w\d*", RegexOptions.None);

            if (string.IsNullOrEmpty(login) || string.IsNullOrWhiteSpace(login) || login == string.Empty)
            {
                return false;
            }
            else if (expressaoRegular.IsMatch(login))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidateNome(string nome)
        {
            expressaoRegular = new Regex(@"\w\D*", RegexOptions.None);

            if (string.IsNullOrEmpty(nome) || string.IsNullOrWhiteSpace(nome) || nome == string.Empty)
            {
                return false;
            }
            else if (expressaoRegular.IsMatch(nome))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
