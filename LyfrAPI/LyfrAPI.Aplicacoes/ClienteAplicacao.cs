using LyfrAPI.Context;
using LyfrAPI.Emails.Functions;
using LyfrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LyfrAPI.Aplicacoes
{
    public class ClienteAplicacao
    {
        private LyfrDBContext _context;

        public ClienteAplicacao(LyfrDBContext context)
        {
            _context = context;
        }

        public string Insert(Cliente cliente)
        {
            try
            {
                if (cliente != null)
                {
                    if (GetClienteByEmail(cliente.Email) != null)
                    {
                        return "Email já cadastrado na base de dados!";
                    }
                    else if (GetClienteByCPF(cliente.Cpf) != null)
                    {
                        return "CPF já cadastrado na base de dados!";
                    }
                    else
                    {
                        //atribui a data atual na variavel data de cadastro
                        cliente.Data_Cadastro = DateTime.Now.AddHours(-1).ToString();
                        //
                        _context.Add(cliente);
                        //
                        //retorna 0 quando nao consegue inserir
                        _context.SaveChanges();

                        //chama a função que irá enviar um email de boas vindas
                        new EmailMessages().WelcomeEmail(cliente.Email, cliente.Nome);


                        return "Cliente cadastrado com sucesso!";
                    }
                }
                else
                {
                    return "Cliente é nulo! Por - favor preencha todos os campos e tente novamente!";
                }
            }
            catch (Exception ex)
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

                        return "Cliente " + cliente.Nome + " deletado com sucesso!";
                    }
                    else
                    {
                        return "Cliente não cadastrado!";
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

                        return "Cliente " + cliente.Nome + " deletado com sucesso!";
                    }
                    else
                    {
                        return "Cliente não cadastrado!";
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

                        return "Cliente " + cliente.Nome + " alterado com sucesso!";
                    }
                    else
                    {
                        return "Cliente não cadastrado!";
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
                    if (numeroDeClientes != 0)
                    {
                        //lista auxiliar caso tenha sido passado uma limitação, por exemplo retornar os 5 ou os 6 ultimos clientes
                        var listaDeClientesComNumeroDeClientes = new List<Cliente>();

                        //contador ja começa com o número do ultimo cliente da lista
                        int indiceUltimoCliente = listaDeClientes.Count - 1;
                        //contador para se comparar com o número passado
                        int i = 0;
                        while (i < numeroDeClientes)
                        {
                            listaDeClientesComNumeroDeClientes.Add(listaDeClientes[indiceUltimoCliente]);
                            indiceUltimoCliente--;
                            i++;
                        }

                        return listaDeClientesComNumeroDeClientes;
                    }
                    else
                    {
                        return listaDeClientes;
                    }
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

        public string ForgotPassword(string emailCliente)
        {
            try
            {
                var cliente = GetClienteByEmail(emailCliente.ToLower());

                if (cliente != null)
                {
                    var resposta = new EmailMessages().UpdatePasswordEmail(cliente.Email, cliente.Senha);
                    return resposta;
                }
                else
                {
                    return "Cliente não encontrado!";
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }
    }
}
