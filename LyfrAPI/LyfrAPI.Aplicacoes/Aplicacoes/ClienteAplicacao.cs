using LyfrAPI.Context;
using LyfrAPI.Emails.Functions;
using LyfrAPI.Models;
using LyfrAPI.Models.ModelsEmail;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LyfrAPI.Aplicacoes
{
    public class ClienteAplicacao
    {
        private LyfrDBContext _context;

        //variavel para poder acessar a pasta wwwroot nas funçoes que necessitam de email
        //dependencia será injetada na classe ClienteMessages
        private PhysicalFileProvider _provedorDiretoriosArquivos;

        //construtor usado para quando NÂO FOR UTILIZAR ARQUIVOS
        public ClienteAplicacao(LyfrDBContext context)
        {
            _context = context; 
        }

        //construtor usado para quando FORMOS UTILIZAR ARQUIVOS, COMO NO CASO DO EMAIL
        public ClienteAplicacao(LyfrDBContext context, PhysicalFileProvider provedorDiretoriosArquivos)
        {
            _context = context;
            _provedorDiretoriosArquivos = provedorDiretoriosArquivos;
        }

        public string Insert(Cliente cliente)
        {
            try
            {
                if (cliente != null)
                {
                    if (GetClienteByEmail(cliente.Email) != null)
                    {
                        return "Email indisponível para cadastro. Por favor, tente outro.";
                    }
                    else if (GetClienteByCPF(cliente.Cpf) != null)
                    {
                        return "CPF indisponível para cadastro. Por favor, tente outro.";
                    }
                    else
                    {
                        _context.Add(cliente);
                        //
                        _context.SaveChanges();
                        //
                        //chama a função que irá enviar um email de boas vindas
                        new ClienteMessages(_provedorDiretoriosArquivos).WelcomeEmail(cliente.Email, cliente.Nome);


                        return "Usuário cadastrado com sucesso!";
                    }
                }
                else
                {
                    return "Usuário é nulo! Por - favor preencha todos os campos e tente novamente!";
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }

        public string DeleteByEmail(string email)
        {
            try
            {
                if (email == string.Empty || email == null || email == "" || string.IsNullOrWhiteSpace(email))
                {
                    return "Email inválido! Por favor tente novamente.";
                }
                else
                {
                    var cliente = GetClienteByEmail(email);

                    if (cliente != null)
                    {
                        _context.Cliente.Remove(cliente);
                        _context.SaveChanges();

                        return "Usuário " + cliente.Nome + " deletado com sucesso!";
                    }
                    else
                    {
                        return "Usuário não encontrado!";
                    }
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }

        public string DeleteByCPF(string CPF)
        {
            try
            {
                if (CPF == string.Empty || CPF == null || CPF == "" || string.IsNullOrWhiteSpace(CPF))
                {
                    return "Email inválido! Por favor tente novamente.";
                }
                else
                {
                    var cliente = GetClienteByCPF(CPF);

                    if (cliente != null)
                    {
                        _context.Cliente.Remove(cliente);
                        _context.SaveChanges();

                        return "Usuário " + cliente.Nome + " deletado com sucesso!";
                    }
                    else
                    {
                        return "Usuário não encontrado!";
                    }
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }

        public string Update(Cliente cliente)
        {
            try
            {
                if (cliente == null)
                {
                    return "Dados inválidos! Por favor tente novamente.";
                }
                else
                {
                    if (cliente != null)
                    {
                        _context.Cliente.Update(cliente);
                        _context.SaveChanges();

                        return "Usuário " + cliente.Nome + " alterado com sucesso!";
                    }
                    else
                    {
                        return "Usuário não encontrado!";
                    }
                }
            }
            catch (Exception)
            {
                return "Já existe um usuário cadastrado com seu Email e/ou CPF.";
            }
        }

        public Cliente GetClienteByEmail(string email)
        {
            Cliente primeiroCliente = new Cliente();

            try
            {
                if (email == string.Empty || email == null || email == "" || string.IsNullOrWhiteSpace(email))
                {
                    return null;
                }

                var cliente = _context.Cliente.Where(x => x.Email == email).ToList();
                primeiroCliente = cliente.FirstOrDefault();


                if (primeiroCliente != null)
                {
                    return primeiroCliente;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Cliente GetClienteByCPF(string cpf)
        {
            Cliente primeiroCliente = new Cliente();

            try
            {
                if (cpf == string.Empty || cpf == null || cpf == "" || string.IsNullOrWhiteSpace(cpf))
                {
                    return null;
                }

                var cliente = _context.Cliente.Where(x => x.Cpf == cpf).ToList();
                primeiroCliente = cliente.FirstOrDefault();

                if (primeiroCliente != null)
                {
                    return primeiroCliente;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Cliente> GetAllClientes(int numeroDeClientes = 0)
        {
            List<Cliente> listaDeClientes = new List<Cliente>();
            try
            {

                listaDeClientes = _context.Cliente.Select(x => x).ToList();

                if (listaDeClientes != null)
                {
                    //caso o numero passado for igual a 0 ele vai retornar todos
                    if (numeroDeClientes != 0 && numeroDeClientes > 0)
                    {
                        listaDeClientes = listaDeClientes.TakeLast(numeroDeClientes).ToList();
                    }

                    return listaDeClientes;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string ForgotPassword(RecoveryPassword recuperarSenha)
        {
            try
            {
                var cliente = GetClienteByEmail(recuperarSenha.Email.ToLower());

                if (cliente != null)
                {
                    var resposta = new ClienteMessages(_provedorDiretoriosArquivos).ForgotPasswordEmail(recuperarSenha);
                    return resposta;
                }
                else
                {
                    return "Usuário não encontrado!";
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }
    }
}
