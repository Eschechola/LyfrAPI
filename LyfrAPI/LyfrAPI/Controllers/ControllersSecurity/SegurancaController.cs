using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APILyfr.Encryption;
using APILyfr.Models;
using APILyfr.Models.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace APILyfr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SegurancaController : ControllerBase
    {
        private IConfiguration _config;
        public SegurancaController(IConfiguration Configuration)
        {
            _config = Configuration;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody]LoginToken login)
        {
            //verifica se o usuario tem uma credencial válida
            bool resultado = ValidarUsuario(login);
            if (resultado)
            {
                //gera o token e retorna um HTTP com STATUS 200 (SUCESSO)
                var tokenDeSeguranca = GerarTokenJWT();
                //gera o o dia e a hora que será finalizado pegando como padrao o horario de brasilia
                var horaExpiracao = DateTime.Now.AddHours(2).ToString();
                //retorna a hora que irá expirar e o token de segurança
                return Ok(new Token { HoraExpiracao = horaExpiracao, TokenString = tokenDeSeguranca});
            }
            else
            {
                //retorna um HTTP com STATUS 404 (SEM ACESSO)
                return Unauthorized();
            }
        }

        private string GerarTokenJWT()
        {
            //define quem é o remetente (API)
            var remetente = _config["Jwt:Issuer"];
            //define pra quem será enviado (Cliente)
            var alvo = _config["Jwt:Audience"];
            //define o tempo de vida do token (no caso 2 horas)
            var tempoDeVida = DateTime.Now.AddMinutes(120);
            //gera uma key de segurança baseado na senha definida no appsettings
            var keyDeSeguranca = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            //gera as credenciais com a key ja criada
            var credenciais = new SigningCredentials(keyDeSeguranca, SecurityAlgorithms.HmacSha256);

            //cria o token usando os dados ja gerados anteriormente
            var token = new JwtSecurityToken(
                issuer: remetente,
                audience: alvo,
                expires: tempoDeVida,
                signingCredentials: credenciais
                );

            //escreve o token na API e retorna uma string criptografada para acesso a API.
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDeSeguranca = tokenHandler.WriteToken(token);
            return tokenDeSeguranca;
        }

        private bool ValidarUsuario(LoginToken login)
        {
            //criptografa as credenciais que estao na appsettings.json
            string usuarioLogiCriptografado = new Criptografia().Encrypt(_config["Autenticacao:Usuario"]);
            string senhaLoginCriptografada = new Criptografia().Encrypt(_config["Autenticacao:Senha"]);

            //criptografa os dados enviados pelo usuario
            string usuarioEnviado = new Criptografia().Encrypt(login.Usuario);
            string senhaEnviada = new Criptografia().Encrypt(login.Senha);

            //compara pra ver se o usuario pode se autenticar
            if (usuarioLogiCriptografado.Equals(usuarioEnviado) && senhaLoginCriptografada.Equals(senhaEnviada))
            {
                if(login.TipoUsuario == "M" || login.TipoUsuario == "W")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}